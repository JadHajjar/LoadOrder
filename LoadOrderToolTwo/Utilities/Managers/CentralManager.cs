using Extensions;

using LoadOrderToolTwo.Domain;
using LoadOrderToolTwo.UserInterface;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoadOrderToolTwo.Utilities.Managers;
internal static class CentralManager
{
	public static List<Mod>? Mods { get; private set; }

	public static event Action? ModsLoaded;
	public static event Action? ModsUpdated;

	public static void Start()
	{
		Mods = ModsUtil.GetMods().Where(x => x != null).ToList();

		ModsLoaded?.Invoke();

		ConnectionHandler.WhenConnected(async () =>
		{
			var result = await SteamUtil.LoadDataAsyncInChunks(Mods.Where(x => x.Workshop).Select(x => x.SteamId).ToArray());

			foreach (var item in Mods)
			{
				if (result.ContainsKey(item.SteamId))
					item.SetSteamInformation(result[item.SteamId]);
			}

			ModsUpdated?.Invoke();
		});
	}
}
