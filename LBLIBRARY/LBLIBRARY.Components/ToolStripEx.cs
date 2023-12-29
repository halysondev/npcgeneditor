using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace LBLIBRARY.Components;

public class ToolStripEx : ToolStrip
{
	private Color OwnBackColor;

	private Color OwnForeColor;

	private Color OwnSeparatorColor;

	private Color OwnLeftSideColor;

	private Color OwnMouseBackColor;

	private Color OwnMouseBorderColor;

	private Color ItemChecked;

	private Color ArrowColor;

	private bool ColorIsBlack;

	[Category("Внешний вид")]
	public bool SetColorBlack
	{
		get
		{
			return ColorIsBlack;
		}
		set
		{
			ColorIsBlack = value;
			if (value)
			{
				OwnBackColor = Color.FromArgb(20, 20, 20);
				OwnForeColor = Color.FromArgb(225, 225, 225);
				base.BackColor = Color.FromArgb(20, 20, 20);
				OwnSeparatorColor = Color.FromArgb(60, 60, 60);
				OwnLeftSideColor = Color.FromArgb(25, 25, 25);
				OwnMouseBackColor = Color.FromArgb(35, 35, 35);
				OwnMouseBorderColor = Color.Black;
				ItemChecked = Color.FromArgb(70, 70, 70);
				ArrowColor = Color.FromArgb(125, 125, 125);
			}
			else
			{
				OwnBackColor = Color.White;
				OwnForeColor = Color.Black;
				base.BackColor = Color.FromArgb(255, 245, 245, 245);
				OwnSeparatorColor = Color.FromArgb(150, 150, 150);
				OwnLeftSideColor = Color.FromArgb(242, 242, 242);
				OwnMouseBackColor = Color.FromArgb(255, 195, 234, 246);
				OwnMouseBorderColor = Color.Blue;
				ItemChecked = Color.LightSkyBlue;
				ArrowColor = SystemColors.ControlText;
			}
			base.Renderer = new MyToolStripRenderer(OwnBackColor, OwnForeColor, OwnSeparatorColor, OwnLeftSideColor, OwnMouseBackColor, OwnMouseBorderColor, ItemChecked, ArrowColor);
		}
	}
}
