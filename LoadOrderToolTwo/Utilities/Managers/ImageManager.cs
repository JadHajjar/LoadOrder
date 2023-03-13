using Extensions;

using SlickControls;

using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Net;
using System.Threading;

namespace LoadOrderToolTwo.Utilities.Managers;

public static class ImageManager
{
	private static readonly Dictionary<string, object> _lockObjects = new();

	private static object LockObj(string path)
	{
		lock (_lockObjects)
		{
			if (!_lockObjects.ContainsKey(path))
			{
				_lockObjects.Add(path, new object());
			}

			return _lockObjects[path];
		}
	}

	public static Bitmap GetIcon(string name)
	{
		return (Bitmap)Properties.Resources.ResourceManager.GetObject(UI.FontScale >= 1.25 ? name : $"{name}_16", Properties.Resources.Culture);
	}

	public static FileInfo File(string url)
	{
		var filePath = Path.Combine(ISave.DocsFolder, "Thumbs", Path.GetFileName(url.TrimEnd('/', '\\')) + ".png");

		return new FileInfo(filePath);
	}

	public static Bitmap? GetImage(string url) => GetImage(url, false);

	public static Bitmap? GetImage(string url, bool localOnly)
	{
		if (string.IsNullOrWhiteSpace(url))
		{
			return null;
		}

		var filePath = Path.Combine(ISave.DocsFolder, "Thumbs", Path.GetFileNameWithoutExtension(url.TrimEnd('/', '\\')) + Path.GetExtension(url).IfEmpty(".png"));

		lock (LockObj(url))
		{
			var imgPath = new FileInfo(filePath);

			if (imgPath.Exists)
			{
				try
				{
					var image = (Bitmap)Image.FromFile(filePath);

					return image;
				}
				catch { }
			}

			if (localOnly)
			{
				return null;
			}

			var tries = 1;
		start:

			if (!ConnectionHandler.IsConnected)
			{
				return null;
			}

			try
			{
				const int squareSize = 256;

				using var webClient = new WebClient();
				var imageData = webClient.DownloadData(url);

				using var ms = new MemoryStream(imageData);
				using var img = Image.FromStream(ms);

				var size = img.Size.GetProportionalDownscaledSize(squareSize);
				var image = new Bitmap(squareSize, squareSize);

				using (var imageGraphics = Graphics.FromImage(image))
				{
					imageGraphics.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
					imageGraphics.DrawImage(img, new Rectangle((squareSize - size.Width) / 2, (squareSize - size.Height) / 2, size.Width, size.Height));
				}

				Directory.GetParent(filePath).Create();

				if (filePath.EndsWith(".jpg", StringComparison.InvariantCultureIgnoreCase) || filePath.EndsWith(".jpeg", StringComparison.InvariantCultureIgnoreCase))
				{
					image.Save(filePath, System.Drawing.Imaging.ImageFormat.Jpeg);
				}
				else
				{
					image.Save(filePath);
				}

				return image;
			}
			catch (Exception ex)
			{
				if (ex is WebException we && we.Response is HttpWebResponse hwr && hwr.StatusCode == HttpStatusCode.BadGateway)
				{
					Thread.Sleep(1000);
					goto start;
				}
				else if (tries < 2)
				{
					tries++;
					goto start;
				}
				else
				{
					return null;
				}
			}
		}
	}
}