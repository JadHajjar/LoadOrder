using Extensions;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoadOrderToolTwo.Utilities;
internal class Locale : LocaleHelper
{
	private static readonly Locale _instance = new();

	protected Locale() : base($"{nameof(LoadOrderToolTwo)}.Properties.Locale.csv") { }

	public static string Dashboard => _instance.GetText(nameof(Dashboard));
	public static string StartCities => _instance.GetText(nameof(StartCities));
	public static string ProfileBubble => _instance.GetText(nameof(ProfileBubble));
	public static string AssetsBubble => _instance.GetText(nameof(AssetsBubble));
	public static string ModsBubble => _instance.GetText(nameof(ModsBubble));
	public static string ModOutOfDatePlural => _instance.GetText(nameof(ModOutOfDatePlural));
	public static string ModOutOfDate => _instance.GetText(nameof(ModOutOfDate));
	public static string MultipleModsIncluded => _instance.GetText(nameof(MultipleModsIncluded));
	public static string ModIncluded => _instance.GetText(nameof(ModIncluded));
	public static string ModIncludedPlural => _instance.GetText(nameof(ModIncludedPlural));
	public static object Mods => _instance.GetText(nameof(ModIncludedPlural));
}
