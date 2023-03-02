using LoadOrderToolTwo.Domain;

using SlickControls;

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoadOrderToolTwo.UserInterface;
internal class ModsListControl : SlickStackedListControl<Mod>
{
	protected override void OnPaintItem(ItemPaintEventArgs<Mod> e)
	{
		e.Graphics.DrawString(e.Item.Name, Font, new SolidBrush(ForeColor), e.ClipRectangle);
	}
}
