using Extensions;

using LoadOrderToolTwo.Domain.Steam;

using Newtonsoft.Json.Linq;

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Policy;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;

namespace LoadOrderToolTwo.Utilities;

public static class SteamUtil
{
	public static async Task<bool> CheckForInternetConnectionAsync()
	{
		return await Task.Run(CheckForInternetConnection);
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

	public static void ExecuteSteam(string args)
	{
		//ContentUtil.Execute(DataLocation.SteamPath, DataLocation.SteamExe, args).WaitForExit();
		Thread.Sleep(30);
	}

	public static void ReDownload(IEnumerable<ulong> ids)
	{
		try
		{
			//Log.Called(ids);
			ExecuteSteam("steam://open/console");// so that user can see what is happening.
			Thread.Sleep(200); // wait for steam to be ready.
			foreach (var id in ids)
			{
				ExecuteSteam($"+workshop_download_item 255710 {id}");
			}

			ExecuteSteam("steam://open/downloads");
		}
		catch (Exception) { }
	}

	public static string ExtractPersonaNameFromHTML(string html)
	{
		//Log.Called(/*html*/);
		var pattern = "<span class=\"actual_persona_name\">([^<>]+)</span>";
		var match = Regex.Matches(html, "<span class=\"actual_persona_name\">([^<>]+)</span>").Cast<Match>().FirstOrDefault();
		if (match != null)
		{
			var ret = match.Groups[1].Value;
			//ret.LogRet(match.Groups[0].Value);
			return ret;
		}
		else
		{
			throw new Exception(
				$"No match found!\n" +
				$"Pattern= {pattern}\n" +
				$"html={html}");
		}
	}

	public static async Task<Dictionary<ulong, SteamWorkshopItem>> LoadDataAsyncInChunks(ulong[] ids, int chunkSize = 1000)
	{
		int i;
		var list = new Dictionary<ulong, SteamWorkshopItem>();
		for (i = 0; i + chunkSize < ids.Length; i += chunkSize)
		{
			var buffer = new ulong[chunkSize];
			Array.Copy(ids, i, buffer, 0, chunkSize);
			var data = await LoadDataAsync(buffer);
			list.AddRange(data);
		}
		var r = ids.Length - i;
		if (r > 0)
		{
			var buffer = new ulong[r];
			Array.Copy(ids, i, buffer, 0, r);
			var data = await LoadDataAsync(buffer);
			list.AddRange(data);
		}

		return list;
	}

	public static async Task<Dictionary<ulong, SteamWorkshopItem>> LoadDataAsync(ulong[] ids)
	{
		var url = @"https://api.steampowered.com/ISteamRemoteStorage/GetPublishedFileDetails/v1/";

		var bodyDictionary = new Dictionary<string, string>
		{
			["itemcount"] = ids.Length.ToString()
		};

		for (var i = 0; i < ids.Length; ++i)
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
				.Where(x => x.result == 1)
				.Select(x => new SteamWorkshopItem(x))
				.ToList() ?? new();

			var users = await GetSteamUsers(data.Select(x => x.AuthorID));

			foreach (var item in data)
			{
				if (users.ContainsKey(item.AuthorID))
					item.Author = new(users[item.AuthorID]);
			}

			return data.ToDictionary(x => ulong.Parse(x.PublishedFileID));
		}

		Log.Error("failed to get steam data: " + httpResponse.RequestMessage);

		return new();
	}

	public static async Task<Dictionary<string, SteamUserEntry>> GetSteamUsers(IEnumerable<string> steamId64s)
	{
		var idString = string.Join(",", steamId64s.Distinct());
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
}