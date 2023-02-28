using LoadOrderToolTwo.Domain.Utilities;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoadOrderToolTwo.Utilities.Managers;
internal class ProfileManager
{
	public static LomProfile CurrentProfile { get; } = new LomProfile
	{
		Name = "Asset Editor"
	};
}
