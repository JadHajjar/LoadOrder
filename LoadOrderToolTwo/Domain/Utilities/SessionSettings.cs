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

	public string? CurrentProfile { get; set; }
    public Rectangle? WindowBounds { get; set; }
	public bool WindowIsMaximized { get; set; }
}
