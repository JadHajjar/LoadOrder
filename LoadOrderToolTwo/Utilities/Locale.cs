using Extensions;

namespace LoadOrderToolTwo.Utilities;
internal class Locale : LocaleHelper
{
	private static readonly Locale _instance = new();

	protected Locale() : base($"{nameof(LoadOrderToolTwo)}.Properties.Locale.csv") { }

	public static string Dashboard => _instance.GetText(nameof(Dashboard));
	public static string StartCities => _instance.GetText(nameof(StartCities));
	public static string StopCities => _instance.GetText(nameof(StopCities));
	public static string ProfileBubble => _instance.GetText(nameof(ProfileBubble));
	public static string AssetsBubble => _instance.GetText(nameof(AssetsBubble));
	public static string ModsBubble => _instance.GetText(nameof(ModsBubble));
	public static string ModOutOfDatePlural => _instance.GetText(nameof(ModOutOfDatePlural));
	public static string ModOutOfDate => _instance.GetText(nameof(ModOutOfDate));
	public static string ModIncompletePlural => _instance.GetText(nameof(ModIncompletePlural));
	public static string ModIncomplete => _instance.GetText(nameof(ModIncomplete));
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
	public static string ModEnabled => _instance.GetText(nameof(ModEnabled));
	public static string ModIncludedAndEnabled => _instance.GetText(nameof(ModIncludedAndEnabled));
	public static string ModEnabledPlural => _instance.GetText(nameof(ModEnabledPlural));
	public static string ModIncludedAndEnabledPlural => _instance.GetText(nameof(ModIncludedAndEnabledPlural));
	public static string ModIsLocal => _instance.GetText(nameof(ModIsLocal));
	public static string ModIsRemoved => _instance.GetText(nameof(ModIsRemoved));
	public static string ModIsUnknown => _instance.GetText(nameof(ModIsUnknown));
	public static string ModIsNotDownloaded => _instance.GetText(nameof(ModIsNotDownloaded));
	public static string ModIsOutOfDate => _instance.GetText(nameof(ModIsOutOfDate));
	public static string ModIsMaybeOutOfDate => _instance.GetText(nameof(ModIsMaybeOutOfDate));
	public static string ModIsIncomplete => _instance.GetText(nameof(ModIsIncomplete));
	public static string ModIsUpToDate => _instance.GetText(nameof(ModIsUpToDate));
	public static string AssetIsLocal => _instance.GetText(nameof(AssetIsLocal));
	public static string AssetIsRemoved => _instance.GetText(nameof(AssetIsRemoved));
	public static string AssetIsUnknown => _instance.GetText(nameof(AssetIsUnknown));
	public static string AssetIsNotDownloaded => _instance.GetText(nameof(AssetIsNotDownloaded));
	public static string AssetIsOutOfDate => _instance.GetText(nameof(AssetIsOutOfDate));
	public static string AssetIsMaybeOutOfDate => _instance.GetText(nameof(AssetIsMaybeOutOfDate));
	public static string AssetIsIncomplete => _instance.GetText(nameof(AssetIsIncomplete));
	public static string AssetIsUpToDate => _instance.GetText(nameof(AssetIsUpToDate));
	public static string Server => _instance.GetText(nameof(Server));
	public static string Assets => _instance.GetText(nameof(Assets));
	public static string Vanilla => _instance.GetText(nameof(Vanilla));
	public static string UpToDate => _instance.GetText(nameof(UpToDate));
	public static string StatusUnknown => _instance.GetText(nameof(StatusUnknown));
	public static string OutOfDate => _instance.GetText(nameof(OutOfDate));
	public static string PartiallyDownloaded => _instance.GetText(nameof(PartiallyDownloaded));
}
