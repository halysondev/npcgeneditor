using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace LBLIBRARY;

public class CheckBoxZ : CheckBox
{
	private Color clr1;

	private Color clr2;

	private Color color1 = Color.SteelBlue;

	private Color color2 = Color.DarkBlue;

	private Color m_hovercolor1 = Color.Yellow;

	private Color m_hovercolor2 = Color.DarkOrange;

	private int color1Transparent = 150;

	private int color2Transparent = 150;

	private int boxsize = 18;

	private int boxlocatx;

	private int boxlocaty;

	private int angle = 90;

	private int textX = 14;

	private int textY = 4;

	private string text = "";

	public string DisplayText
	{
		get
		{
			return text;
		}
		set
		{
			text = value;
			Invalidate();
		}
	}

	public Color StartColor
	{
		get
		{
			return color1;
		}
		set
		{
			color1 = value;
			Invalidate();
		}
	}

	public Color EndColor
	{
		get
		{
			return color2;
		}
		set
		{
			color2 = value;
			Invalidate();
		}
	}

	public Color MouseHoverColor1
	{
		get
		{
			return m_hovercolor1;
		}
		set
		{
			m_hovercolor1 = value;
			Invalidate();
		}
	}

	public Color MouseHoverColor2
	{
		get
		{
			return m_hovercolor2;
		}
		set
		{
			m_hovercolor2 = value;
			Invalidate();
		}
	}

	public int Transparent1
	{
		get
		{
			return color1Transparent;
		}
		set
		{
			color1Transparent = value;
			if (color1Transparent > 255)
			{
				color1Transparent = 255;
				Invalidate();
			}
			else
			{
				Invalidate();
			}
		}
	}

	public int Transparent2
	{
		get
		{
			return color2Transparent;
		}
		set
		{
			color2Transparent = value;
			if (color2Transparent > 255)
			{
				color2Transparent = 255;
				Invalidate();
			}
			else
			{
				Invalidate();
			}
		}
	}

	public int GradientAngle
	{
		get
		{
			return angle;
		}
		set
		{
			angle = value;
			Invalidate();
		}
	}

	public int TextLocation_X
	{
		get
		{
			return textX;
		}
		set
		{
			textX = value;
			Invalidate();
		}
	}

	public int TextLocation_Y
	{
		get
		{
			return textY;
		}
		set
		{
			textY = value;
			Invalidate();
		}
	}

	public int BoxSize
	{
		get
		{
			return boxsize;
		}
		set
		{
			boxsize = value;
			Invalidate();
		}
	}

	public int BoxLocation_X
	{
		get
		{
			return boxlocatx;
		}
		set
		{
			boxlocatx = value;
			Invalidate();
		}
	}

	public int BoxLocation_Y
	{
		get
		{
			return boxlocaty;
		}
		set
		{
			boxlocaty = value;
			Invalidate();
		}
	}

	public CheckBoxZ()
	{
		ForeColor = Color.White;
		AutoSize = false;
	}

	protected override void OnMouseEnter(EventArgs e)
	{
		base.OnMouseEnter(e);
		clr1 = color1;
		clr2 = color2;
		color1 = m_hovercolor1;
		color2 = m_hovercolor2;
	}

	protected override void OnMouseLeave(EventArgs e)
	{
		base.OnMouseLeave(e);
		color1 = clr1;
		color2 = clr2;
	}

	protected override void OnPaint(PaintEventArgs e)
	{
		base.OnPaint(e);
		AutoSize = false;
		text = Text;
		if (textX == 100 && textY == 25)
		{
			textX = base.Width / 3 + 10;
			textY = base.Height / 2 - 1;
		}
		Color color = Color.FromArgb(color1Transparent, color1);
		Color color2 = Color.FromArgb(color2Transparent, this.color2);
		Brush brush = new LinearGradientBrush(base.ClientRectangle, color, color2, angle);
		Point point = new Point(textX, textY);
		SolidBrush brush2 = new SolidBrush(ForeColor);
		e.Graphics.FillRectangle(brush, base.ClientRectangle);
		e.Graphics.DrawString(text, Font, brush2, point);
		ControlPaint.DrawCheckBox(rectangle: new Rectangle(boxlocatx, boxlocaty, boxsize, boxsize), graphics: e.Graphics, state: base.Checked ? ButtonState.Checked : ButtonState.Normal);
		brush.Dispose();
	}
}
