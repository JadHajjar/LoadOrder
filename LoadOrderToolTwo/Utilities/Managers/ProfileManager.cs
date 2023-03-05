using LoadOrderToolTwo.Domain.Utilities;

namespace LoadOrderToolTwo.Utilities.Managers;
internal class ProfileManager
{
	public static LomProfile CurrentProfile { get; } = new LomProfile
	{
		Name = "Asset Editor"
	};
}
