using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoadOrderToolTwo.Utilities.IO;
internal class IOUtil
{
	public static Process? Execute(string dir, string exeFile, string args)
	{
		try
		{
			ProcessStartInfo startInfo = new ProcessStartInfo
			{
				WorkingDirectory = dir,
				FileName = exeFile,
				Arguments = args,
				WindowStyle = ProcessWindowStyle.Normal,
				UseShellExecute = true,
				CreateNoWindow = false,
			};

			var process = new Process { StartInfo = startInfo };

			process.Start();

			return process;
		}
		catch (Exception ex)
		{
			Log.Exception(ex);
			return null;
		}
	}
}
