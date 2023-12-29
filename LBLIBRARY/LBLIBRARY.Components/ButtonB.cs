using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace LBLIBRARY.Components;

public class ButtonB : Button
{
	private Color clr1;

	private Color color = Color.Teal;

	private Color m_hovercolor = Color.FromArgb(0, 0, 140);

	private Color clickcolor = Color.FromArgb(160, 180, 200);

	private int textX = 6;

	private int textY = -20;

	private string text = "_";

	private Bitmap im;

	private Point imagepos;

	public bool ImToHeight;

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

	public Color BZBackColor
	{
		get
		{
			return color;
		}
		set
		{
			color = value;
			Invalidate();
		}
	}

	public Color MouseHoverColor
	{
		get
		{
			return m_hovercolor;
		}
		set
		{
			m_hovercolor = value;
			Invalidate();
		}
	}

	public Color MouseClickColor1
	{
		get
		{
			return clickcolor;
		}
		set
		{
			clickcolor = value;
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

	[Category("Image")]
	public new Bitmap Image
	{
		get
		{
			return im;
		}
		set
		{
			im = value;
		}
	}

	[Category("Image")]
	public Point Image_Location
	{
		get
		{
			return imagepos;
		}
		set
		{
			imagepos = value;
			Invalidate();
		}
	}

	[Category("Image")]
	public bool ImageToHeight
	{
		get
		{
			return ImToHeight;
		}
		set
		{
			ImToHeight = value;
			Invalidate();
		}
	}

	public ButtonB()
	{
		base.Size = new Size(31, 24);
		ForeColor = Color.White;
		base.FlatStyle = FlatStyle.Flat;
		Font = new Font("Microsoft YaHei UI", 20.25f, FontStyle.Bold, GraphicsUnit.Point, 0);
		Text = "_";
		text = Text;
	}

	protected override void OnMouseEnter(EventArgs e)
	{
		base.OnMouseEnter(e);
		clr1 = color;
		color = m_hovercolor;
	}

	protected override void OnMouseLeave(EventArgs e)
	{
		base.OnMouseLeave(e);
		color = clr1;
	}

	protected override void OnMouseDown(MouseEventArgs mevent)
	{
		base.OnMouseDown(mevent);
		color = clickcolor;
	}

	protected override void OnMouseUp(MouseEventArgs mevent)
	{
		base.OnMouseUp(mevent);
		color = clr1;
	}

	protected override void OnPaint(PaintEventArgs pe)
	{
		base.OnPaint(pe);
		text = Text;
		if (textX == 100 && textY == 25)
		{
			textX = base.Width / 3 + 10;
			textY = base.Height / 2 - 1;
		}
		Point point = new Point(textX, textY);
		pe.Graphics.FillRectangle(new SolidBrush(color), base.ClientRectangle);
		pe.Graphics.DrawString(text, Font, new SolidBrush(ForeColor), point);
		if (im != null)
		{
			Bitmap source = im;
			if (ImToHeight)
			{
				source = source.ResizeImage(base.Height - 2, base.Height - 2);
			}
			pe.Graphics.DrawImage(source, imagepos.X + 1, imagepos.Y + 1);
		}
	}
}
