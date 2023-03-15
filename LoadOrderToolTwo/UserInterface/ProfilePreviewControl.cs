﻿using Extensions;

using LoadOrderToolTwo.Domain;
using LoadOrderToolTwo.Utilities;
using LoadOrderToolTwo.Utilities.Managers;

using SlickControls;

using System;
using System.Drawing;
using System.Windows.Forms;

namespace LoadOrderToolTwo.UserInterface;
internal class ProfilePreviewControl : SlickControl
{
	private Rectangle LoadRect;
	private Rectangle MergeRect;
	private Rectangle ExcludeRect;
	private Rectangle DisposeRect;

	public ProfilePreviewControl(Profile profile)
	{
		Profile = profile;
		Text = profile.Name;
	}

	public Profile Profile { get; }

	public event Action<Profile>? LoadProfile;
	public event Action<Profile>? MergeProfile;
	public event Action<Profile>? ExcludeProfile;
	public event Action<Profile>? DisposeProfile;

	protected override void UIChanged()
	{
		base.UIChanged();

		Padding = UI.Scale(new Padding(7), UI.FontScale);
		Margin = UI.Scale(new Padding(10), UI.FontScale);
		Size = UI.Scale(new Size(300, 50), UI.FontScale);
	}

	protected override void OnMouseClick(MouseEventArgs e)
	{
		base.OnMouseClick(e);

		if (e.Button != MouseButtons.Left)
			return;

		if (LoadRect.Contains(e.Location))
		{
			LoadProfile?.Invoke(Profile);
		}

		else if (MergeRect.Contains(e.Location))
		{
			MergeProfile?.Invoke(Profile);
		}

		else if (ExcludeRect.Contains(e.Location))
		{
			ExcludeProfile?.Invoke(Profile);
		}

		else if (DisposeRect.Contains(e.Location))
		{
			DisposeProfile?.Invoke(Profile);
		}
	}

	protected override void OnPaint(PaintEventArgs e)
	{
		e.Graphics.SetUp(BackColor);

		var back = FormDesign.Design.ButtonColor;

		if (Profile.Temporary)
		{
			back = back.MergeColor(FormDesign.Design.YellowColor, 65);
		}
		else if (Profile.IsMissingItems)
		{
			back = back.MergeColor(FormDesign.Design.RedColor, 90);
		}

		using var backBrush = ClientRectangle.Gradient(Color.FromArgb(200, back));
		e.Graphics.FillRoundedRectangle(backBrush, ClientRectangle.Pad(1), Padding.Left);

		var titleHeight = Math.Max(24, (int)e.Graphics.Measure(Text, UI.Font(9.75F, FontStyle.Bold), Width - Padding.Horizontal).Height);
		var iconRectangle = new Rectangle(Padding.Left, Padding.Top + ((titleHeight - 24) / 2), 24, 24);

		if (Loading)
		{
			DrawLoader(e.Graphics, iconRectangle);
		}
		else
		{
			using var image = Profile.GetIcon();

			e.Graphics.DrawImage(image.Color(FormDesign.Design.IconColor), iconRectangle);
		}

		e.Graphics.DrawString(Text, UI.Font(9.75F, FontStyle.Bold), new SolidBrush(FormDesign.Design.ForeColor), new Rectangle(24 + (Padding.Left * 2), Padding.Top, Width - Padding.Horizontal, titleHeight), new StringFormat { LineAlignment = StringAlignment.Center });

		var y = titleHeight + Padding.Vertical;

		if (!Profile.Temporary)
		{
			y = Math.Max(
				DrawValue(e, y, Profile.Mods.Count.ToString(), Profile.Mods.Count == 1 ? Locale.ModIncluded : Locale.ModIncludedPlural),
				DrawValue(e, y, Profile.Assets.Count.ToString(), Profile.Assets.Count == 1 ? Locale.AssetIncluded : Locale.AssetIncludedPlural, x: Width / 2));

			if (Profile.IsMissingItems)
			{
				e.Graphics.DrawString(Locale.IncludesItemsYouDoNotHave, Font, new SolidBrush(FormDesign.Design.RedColor), Padding.Left, y);

				y += (int)e.Graphics.Measure(Locale.IncludesItemsYouDoNotHave, Font).Height + Padding.Bottom;
			}
		}

		var hovered = false;

		hovered |= DrawButton(e, "LoadProfile", Properties.Resources.I_Import, ClientRectangle, ContentAlignment.BottomRight, out var loadRect);

		LoadRect = loadRect;

		if (!Profile.Temporary)
		{
			hovered |= DrawButton(e, string.Empty, Properties.Resources.I_Merge, ClientRectangle, ContentAlignment.BottomLeft, out var mergeRect);
			hovered |= DrawButton(e, string.Empty, Properties.Resources.I_Exclude, ClientRectangle.Pad(mergeRect.Width + Padding.Left, 0, 0, 0), ContentAlignment.BottomLeft, out var excludeRect);
			hovered |= DrawButton(e, string.Empty, Properties.Resources.I_Disposable, ClientRectangle, ContentAlignment.TopRight, out var disposeRect);

			MergeRect = mergeRect;
			ExcludeRect = excludeRect;
			DisposeRect = disposeRect;
		}

		Cursor = hovered ? Cursors.Hand : Cursors.Default;

		Height = y + loadRect.Height + Padding.Bottom;
	}

	private bool DrawButton(PaintEventArgs e, string text, Bitmap icon, Rectangle rectangle, ContentAlignment alignment, out Rectangle rect)
	{
		using (icon)
		{
			var size = SlickButton.GetSize(e.Graphics, icon, text, Font);
			rect = rectangle.Pad(Padding).Align(size, alignment);
			var hovered = rect.Contains(PointToClient(Cursor.Position));

			SlickButton.DrawButton(e, rect, text, Font, icon, HoverState: hovered ? HoverState & ~HoverState.Focused : HoverState.Normal);

			return hovered;
		}
	}

	private int DrawValue(PaintEventArgs e, int targetHeight, string value, string descriptor, int x = 0)
	{
		var valueSize = e.Graphics.Measure(value, UI.Font(8.25F, FontStyle.Bold), Width - Padding.Horizontal).ToSize();
		var descriptorSize = e.Graphics.Measure(descriptor, UI.Font(8.25F), Width - valueSize.Width - Padding.Right + 3).ToSize();

		e.Graphics.DrawString(value, UI.Font(8.25F, FontStyle.Bold), new SolidBrush(FormDesign.Design.ForeColor), new Rectangle(x + Padding.Left, targetHeight, valueSize.Width + 3, valueSize.Height));

		e.Graphics.DrawString(descriptor, UI.Font(8.25F), new SolidBrush(FormDesign.Design.ForeColor), new Rectangle(x + valueSize.Width + Padding.Left, targetHeight, Width - valueSize.Width - Padding.Right + 3, Height));

		return targetHeight + Math.Max(valueSize.Height, descriptorSize.Height) + Padding.Bottom;
	}
}
