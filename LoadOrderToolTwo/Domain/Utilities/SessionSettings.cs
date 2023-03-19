using Extensions;

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoadOrderToolTwo.Domain.Utilities;
internal class SessionSettings : ISave
{
	public override string Name => nameof(SessionSettings) + ".tf";

    public bool FirstTimeSetupCompleted { get; set; }
    public string? CurrentProfile { get; set; }
    public Rectangle? WindowBounds { get; set; }
	public bool WindowIsMaximized { get; set; }

	public bool LinkModAssets { get; set; } = true;
	public bool LargeItemOnHover { get; set; }
	public bool ShowDatesRelatively { get; set; }
}
