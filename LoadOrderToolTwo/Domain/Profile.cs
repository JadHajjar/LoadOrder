using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoadOrderToolTwo.Domain;
internal class Profile
{
	public Profile(string name)
	{
		Name = name;
		LaunchSettings = new();
	}

	public LaunchSettings LaunchSettings { get; set; }
	public string Name { get; }
}
