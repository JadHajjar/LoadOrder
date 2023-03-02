using Newtonsoft.Json.Linq;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoadOrderToolTwo.Domain.Steam;
public class SteamWorkshopItem
{
	private static readonly DateTime _epoch = new(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);

	public SteamUser? Author { get; set; }
	public string Title { get; set; }
	public string PublishedFileID { get; set; }
	public int Size { get; set; }
	public string PreviewURL { get; set; }
	public string AuthorID { get; set; }
	public DateTime UpdatedUTC { get; set; }
	public string[]? Tags { get; set; }
	public string Class { get; set; }

	public SteamWorkshopItem(SteamWorkshopItemEntry entry)
	{
		Title = entry.title;
		PublishedFileID = entry.publishedfileid;
		Size = entry.file_size;
		PreviewURL = entry.preview_url;
		AuthorID = entry.creator;
		UpdatedUTC = _epoch.AddSeconds((ulong)entry.time_updated);
		Tags = (entry.tags
			?.Select(item => item.tag)
			?.Where(item => item.IndexOf("compatible", StringComparison.OrdinalIgnoreCase) >= 0)
			?.ToArray()) ?? new string[0];

		if (Tags.Any(tag => tag.ToLower() == "mod"))
		{
			Class = "Mod";
		}
		else if (Tags.Any())
		{
			Class = "Asset";
		}
		else
		{
			Class = "subscribed item";
		}
	}
}
