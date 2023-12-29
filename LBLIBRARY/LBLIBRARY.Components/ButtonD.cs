using System;
using System.Drawing;
using System.Windows.Forms;

namespace LBLIBRARY.Components;

public class ButtonD : Button
{
	public enum CustomFormState
	{
		Normal,
		Maximize
	}

	private Color clr1;

	private Color color = Color.Gray;

	private Color m_hovercolor = Color.FromArgb(180, 200, 240);

	private Color clickcolor = Color.FromArgb(160, 180, 200);

	private int textX = 6;

	private int textY = -20;

	private string text = "_";

	private CustomFormState _customFormState;

	public CustomFormState CFormState
	{
		get
		{
			return _customFormState;
		}
		set
		{
			_customFormState = value;
			Invalidate();
		}
	}

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

	public ButtonD()
	{
		base.Size = new Size(31, 24);
		ForeColor = Color.White;
		base.FlatStyle = FlatStyle.Flat;
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
		switch (_customFormState)
		{
		case CustomFormState.Normal:
		{
			pe.Graphics.FillRectangle(new SolidBrush(color), base.ClientRectangle);
			for (int j = 0; j < 2; j++)
			{
				pe.Graphics.DrawRectangle(new Pen(ForeColor), textX + j + 1, textY, 10, 10);
				pe.Graphics.FillRectangle(new SolidBrush(ForeColor), textX + 1, textY - 1, 12, 4);
			}
			break;
		}
		case CustomFormState.Maximize:
		{
			pe.Graphics.FillRectangle(new SolidBrush(color), base.ClientRectangle);
			for (int i = 0; i < 2; i++)
			{
				pe.Graphics.DrawRectangle(new Pen(ForeColor), textX + 5, textY, 8, 8);
				pe.Graphics.FillRectangle(new SolidBrush(ForeColor), textX + 5, textY - 1, 9, 4);
				pe.Graphics.DrawRectangle(new Pen(ForeColor), textX + 2, textY + 5, 8, 8);
				pe.Graphics.FillRectangle(new SolidBrush(ForeColor), textX + 2, textY + 4, 9, 4);
			}
			break;
		}
		}
	}
}
