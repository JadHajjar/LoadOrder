using Extensions;

using LoadOrderToolTwo.Domain.Interfaces;
using LoadOrderToolTwo.Domain.Steam;
using LoadOrderToolTwo.Utilities.IO;
using LoadOrderToolTwo.Utilities.Managers;

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace LoadOrderToolTwo.Utilities;

public static class SteamUtil
{
	private static string STEAM_CACHE_FILE = "SteamModsCache.json";

	private static void SaveCache(Dictionary<ulong, SteamWorkshopItem> list)
	{
		ISave.Save(list, STEAM_CACHE_FILE);
	}

	internal static Dictionary<ulong, SteamWorkshopItem>? GetCachedInfo()
	{
		try
		{
			var path = ISave.GetPath(STEAM_CACHE_FILE);

			if (DateTime.Now - File.GetLastWriteTime(path) > TimeSpan.FromDays(1.5))
			{
				return null;
			}

			ISave.Load(out Dictionary<ulong, SteamWorkshopItem>? dic, STEAM_CACHE_FILE);

			return dic;
		}
		catch { return null; }
	}

	public static bool CheckForInternetConnection()
	{
		try
		{
			using var client = new HttpClient();
			using var stream = client.GetStreamAsync("https://steamcommunity.com/").Result;
			return true;
		}
		catch
		{
			return false;
		}
	}

	public static async Task<bool> CheckForInternetConnectionAsync()
	{
		return await Task.Run(CheckForInternetConnection);
	}

	public static IEnumerable<IEnumerable<T>> Chunk<T>(this IEnumerable<T> source, int chunkSize)
	{
		while (source.Any())
		{
			yield return source.Take(chunkSize);
			source = source.Skip(chunkSize);
		}
	}

	public static async Task<Dictionary<T1, T2>> ConvertInChunks<T1, T2>(IEnumerable<T1> mainList, int chunkSize, Func<List<T1>, Task<Dictionary<T1, T2>>> converter)
	{
		var chunks = mainList.Chunk(chunkSize); // Split mainList into chunks of chunkSize
		var tasks = new List<Task<Dictionary<T1, T2>>>(); // A list of tasks to convert the chunks
		var results = new Dictionary<T1, T2>(); // A dictionary to store the results

		foreach (var chunk in chunks)
		{
			tasks.Add(converter(chunk.ToList())); // Convert the current chunk and add the task to the list
		}

		await Task.WhenAll(tasks); // Wait for all tasks to complete

		foreach (var task in tasks)
		{
			var chunkResults = await task; // Get the results of the completed task

			foreach (var kvp in chunkResults)
			{
				results[kvp.Key] = kvp.Value; // Add the results to the dictionary
			}
		}

		return results;
	}

	public static void ExecuteSteam(string args)
	{
		IOUtil.Execute(LocationManager.SteamPath, LocationManager.SteamExe, args)?.WaitForExit();
	}

	public static async Task<Dictionary<string, SteamUserEntry>> GetSteamUsers(List<string> steamId64s)
	{
		var idString = string.Join(",", steamId64s);
		var url = $"http://api.steampowered.com/ISteamUser/GetPlayerSummaries/v0002/?key=706303B24FA0E63B1FB25965E081C2E1&steamids={idString}";

		using var httpClient = new HttpClient();
		var httpResponse = await httpClient.GetAsync(url);

		if (httpResponse.IsSuccessStatusCode)
		{
			var response = await httpResponse.Content.ReadAsStringAsync();

			return Newtonsoft.Json.JsonConvert.DeserializeObject<SteamUserRootResponse>(response)?.response.players.ToDictionary(x => x.steamid) ?? new();
		}

		Log.Error("failed to get steam author data: " + httpResponse.RequestMessage);

		return new();
	}

	public static async Task<Dictionary<ulong, SteamWorkshopItem>> LoadDataAsync(ulong[] ids)
	{
		var results = await ConvertInChunks(ids, 1000, LoadDataImplAsync);

		SaveCache(results);

		return results;
	}

	public static async Task<Dictionary<ulong, SteamWorkshopItem>> LoadDataImplAsync(List<ulong> ids)
	{
		var url = @"https://api.steampowered.com/ISteamRemoteStorage/GetPublishedFileDetails/v1/";

		var bodyDictionary = new Dictionary<string, string>
		{
			["itemcount"] = ids.Count.ToString()
		};

		for (var i = 0; i < ids.Count; ++i)
		{
			bodyDictionary[$"publishedfileids[{i}]"] = ids[i].ToString();
		}

		using var httpClient = new HttpClient();
		var body = new FormUrlEncodedContent(bodyDictionary);
		var httpResponse = await httpClient.PostAsync(url, body);

		if (httpResponse.IsSuccessStatusCode)
		{
			var response = await httpResponse.Content.ReadAsStringAsync();

			var data = Newtonsoft.Json.JsonConvert.DeserializeObject<SteamWorkshopItemRootResponse>(response)?.response.publishedfiledetails
				.Where(x => x.result is 1 or 17 or 86 or 9)
				.Select(x => new SteamWorkshopItem(x))
				.ToList() ?? new();

			var users = await ConvertInChunks(data.Select(x => x.AuthorID).Distinct(), 100, GetSteamUsers);

			foreach (var item in data)
			{
				if (!string.IsNullOrEmpty(item.AuthorID) && users.ContainsKey(item.AuthorID))
				{
					item.Author = new(users[item.AuthorID]);
				}
			}

			return data.ToDictionary(x => ulong.Parse(x.PublishedFileID));
		}

		Log.Error("failed to get steam data: " + httpResponse.RequestMessage);

		return new();
	}

	public static void ReDownload(params ulong[] ids)
	{
		try
		{
			var steamArguments = new StringBuilder("'steam://open/console");

			for (var i = 0; i < ids.Length; i++)
			{
				steamArguments.AppendFormat(" +workshop_download_item 255710 {0}", ids[i]);
			}

			steamArguments.Append('\'');

			ExecuteSteam(steamArguments.ToString());

			Thread.Sleep(100);

			ExecuteSteam("steam://open/downloads");
		}
		catch (Exception) { }
	}

	public static void SetSteamInformation(this IPackage package, SteamWorkshopItem steamWorkshopItem, bool cache)
	{
		if (steamWorkshopItem.Removed)
		{
			package.RemovedFromSteam = true;
			return;
		}

		package.SteamInfoLoaded = true;
		package.Name = package.SteamId == 2040656402ul ? "Harmony" : steamWorkshopItem.Title;
		package.Author = steamWorkshopItem.Author;
		package.ServerTime = steamWorkshopItem.UpdatedUTC;
		package.Tags = steamWorkshopItem.Tags;
		package.Class = steamWorkshopItem.Class;
		package.ServerSize = steamWorkshopItem.Size;
		package.SteamDescription = steamWorkshopItem.Description;

		if (package.IconUrl != steamWorkshopItem.PreviewURL)
		{
			package.IconUrl = steamWorkshopItem.PreviewURL;
			package.IconImage?.Dispose();
			package.IconImage = null;
		}

		if (!cache)
		{
			package.Status = ModsUtil.GetStatus(package, out var reason);
			package.StatusReason = reason;

			CentralManager.InformationUpdate(package);
		}
	}
}