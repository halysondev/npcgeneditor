using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace LBLIBRARY.Components;

public class TabControlZ : TabControl
{
	private Color SelectedColor;

	private Color TabDefaultBack;

	private Color TabDefaultFore;

	private Point textloc;

	private Color SelectedTextColor;

	private Color bo;

	public override Color BackColor
	{
		get
		{
			return Color.Transparent;
		}
		set
		{
			base.BackColor = Color.Transparent;
		}
	}

	[Category("LBLIBRARY")]
	public Color TabDefaultForeground
	{
		get
		{
			return TabDefaultFore;
		}
		set
		{
			TabDefaultFore = value;
			Invalidate();
		}
	}

	[Category("LBLIBRARY")]
	public Color TabDefaultBackground
	{
		get
		{
			return TabDefaultBack;
		}
		set
		{
			TabDefaultBack = value;
			Invalidate();
		}
	}

	[Category("LBLIBRARY")]
	public Point TextPosition
	{
		get
		{
			return textloc;
		}
		set
		{
			textloc = value;
			Invalidate();
		}
	}

	[Category("LBLIBRARY")]
	public Color SelectedTabBackground
	{
		get
		{
			return SelectedColor;
		}
		set
		{
			SelectedColor = value;
			Invalidate();
		}
	}

	[Category("LBLIBRARY")]
	public Color SelectedTabForeground
	{
		get
		{
			return SelectedTextColor;
		}
		set
		{
			SelectedTextColor = value;
			Invalidate();
		}
	}

	[Category("LBLIBRARY")]
	public Color BorderColor
	{
		get
		{
			return bo;
		}
		set
		{
			bo = value;
			Invalidate();
		}
	}

	public TabControlZ()
	{
		SetStyle(ControlStyles.UserPaint | ControlStyles.SupportsTransparentBackColor, value: true);
		DoubleBuffered = (base.ResizeRedraw = true);
		base.DrawMode = TabDrawMode.OwnerDrawFixed;
	}

	protected virtual void DrawTabRectangle(Graphics g, int index, Rectangle r)
	{
		if (index == 0)
		{
			r = new Rectangle(r.Left - 2, r.Top, r.Width + 2, r.Height);
		}
		if (index != base.SelectedIndex)
		{
			r = new Rectangle(r.Left, r.Top + 2, r.Width, r.Height - 2);
		}
		Color color = ((index != base.SelectedIndex) ? TabDefaultBack : SelectedColor);
		using SolidBrush brush = new SolidBrush(color);
		g.FillRectangle(brush, r);
	}

	protected virtual void DrawTab(Graphics g, int index, Rectangle r, Color co)
	{
		if (index != -1)
		{
			r.Inflate(-1, -1);
			TextRenderer.DrawText(g, base.TabPages[index].Text, Font, r, co, TextFormatFlags.Default);
			g.FillRectangle(new SolidBrush(BorderColor), 0, 23, base.Width, 2);
			g.FillRectangle(new SolidBrush(BorderColor), 0, base.Height - 4, base.Width, 4);
			g.FillRectangle(new SolidBrush(BorderColor), 0, 25, 4, base.Height);
			g.FillRectangle(new SolidBrush(BorderColor), base.Width - 4, 25, 4, base.Height);
		}
	}

	protected override void OnPaint(PaintEventArgs e)
	{
		if (base.TabCount <= 0)
		{
			return;
		}
		Rectangle clientRectangle = base.ClientRectangle;
		int bottom = GetTabRect(0).Bottom;
		using (SolidBrush brush = new SolidBrush(Color.FromKnownColor(KnownColor.Window)))
		{
			e.Graphics.FillRectangle(brush, new Rectangle(clientRectangle.Left, bottom, clientRectangle.Width, clientRectangle.Height - bottom));
		}
		for (int i = 0; i < base.TabCount; i++)
		{
			clientRectangle = GetTabRect(i);
			DrawTabRectangle(e.Graphics, i, clientRectangle);
			if (i == base.SelectedIndex)
			{
				DrawTab(e.Graphics, i, clientRectangle, SelectedTextColor);
				clientRectangle.Inflate(-1, -1);
				ControlPaint.DrawFocusRectangle(e.Graphics, clientRectangle);
			}
			else
			{
				DrawTab(e.Graphics, i, clientRectangle, TabDefaultFore);
			}
		}
	}
}
