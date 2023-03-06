﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace LoadOrderToolTwo.Utilities.IO;
internal static class PlatformUtil
{
	public static Platform CurrentPlatform { get; }

	static PlatformUtil()
	{
		if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
		{
			CurrentPlatform = Platform.Linux;
			return;
		}

		if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
		{
			CurrentPlatform = Platform.MacOSX;
			return;
		}

		if (Environment.OSVersion.Platform == PlatformID.Unix)
		{
			// Check for Wine environment
			var winePrefix = Environment.GetEnvironmentVariable("WINEPREFIX");

			if (!string.IsNullOrEmpty(winePrefix))
			{
				CurrentPlatform = Platform.WineOnLinux;
			}
			else
			{
				CurrentPlatform = Platform.Linux;
			}
		}
	}

	public enum Platform
	{
		Windows,
		MacOSX, 
		Linux,
		WineOnLinux
	}
}
