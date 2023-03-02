using LoadOrderToolTwo.Utilities.Managers;

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoadOrderToolTwo.Utilities;
internal class ContentUtil
{
	public static IEnumerable<ulong> GetSubscribedItems()
	{
		foreach (var path in GetSubscribedItemPaths())
		{
			yield return ulong.Parse(Path.GetFileName(path));
		}
	}

	public static IEnumerable<string> GetSubscribedItemPaths()
	{
		if (!Directory.Exists(LocationManager.WorkshopContentPath))
		{
			yield break;
		}

		foreach (var path in Directory.EnumerateDirectories(LocationManager.WorkshopContentPath))
		{
			if (ulong.TryParse(Path.GetFileName(path), out _))
				yield return path;
		}
	}

	public static string GetSubscribedItemPath(ulong id) => Path.Combine(LocationManager.WorkshopContentPath, id.ToString());
}
