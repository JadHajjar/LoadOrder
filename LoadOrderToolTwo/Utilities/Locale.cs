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
	public static string Mods => _instance.GetText(nameof(Mods));
	public static string Local => _instance.GetText(nameof(Local));
	public static string Workshop => _instance.GetText(nameof(Workshop));
	public static string Enabled => _instance.GetText(nameof(Enabled));
	public static string Disabled => _instance.GetText(nameof(Disabled));
	public static string Included => _instance.GetText(nameof(Included));
	public static string Excluded => _instance.GetText(nameof(Excluded));
	public static string Loading => _instance.GetText(nameof(Loading));
}
