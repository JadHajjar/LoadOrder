using Extensions;

using LoadOrderToolTwo.Utilities;
using LoadOrderToolTwo.Utilities.Managers;

using System;
using System.Linq;
using System.Windows.Forms;

namespace LoadOrderToolTwo.UserInterface.StatusBubbles;

internal class AssetsBubble : StatusBubbleBase
{
	public AssetsBubble()
	{
		Image = Properties.Resources.I_Assets;
		Text = Locale.AssetsBubble;
	}

	protected override void OnHandleCreated(EventArgs e)
	{
		base.OnHandleCreated(e);

		if (!Live)
		{
			return;
		}

		if (!CentralManager.IsContentLoaded)
		{
			Loading = true;

			CentralManager.ContentLoaded += Invalidate;
		}

		CentralManager.WorkshopInfoUpdated += CentralManager_WorkshopInfoUpdated;
	}

	private void CentralManager_WorkshopInfoUpdated()
	{
		if (Loading)
		{
			Loading = false;
		}
		else
		{
			Invalidate();
		}
	}

	protected override void CustomDraw(PaintEventArgs e, ref int targetHeight)
	{
		if (!CentralManager.IsContentLoaded)
		{
			DrawText(e, ref targetHeight, Locale.Loading, FormDesign.Design.InfoColor);
			return;
		}

		var assets = CentralManager.Assets.Count(x => x.IsIncluded);
		var assetsSize = CentralManager.Assets.Where(x => x.IsIncluded).Sum(x => x.FileSize).SizeString();

		DrawValue(e, ref targetHeight, assets.ToString(), "assets included");
		DrawValue(e, ref targetHeight, assetsSize, "total size");
	}
}
