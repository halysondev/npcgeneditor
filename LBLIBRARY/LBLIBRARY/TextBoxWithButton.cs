using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using LBLIBRARY.Components;

namespace LBLIBRARY;

public class TextBoxWithButton : Panel
{
	private TextBox te = new TextBox();

	private ButtonC bt = new ButtonC();

	private Bitmap ButtonImage;

	private Point ButtonImagePosition;

	[Category("LB")]
	public Bitmap Image
	{
		get
		{
			return ButtonImage;
		}
		set
		{
			ButtonImage = value;
			bt.Image = value;
		}
	}

	[Category("LB")]
	public Point ButtonIm_Position
	{
		get
		{
			return ButtonImagePosition;
		}
		set
		{
			ButtonImagePosition = value;
			bt.Image_Location = value;
		}
	}

	[Category("OwnEvents")]
	public event EventHandler Button_Click
	{
		add
		{
			bt.Click += value;
		}
		remove
		{
			bt.Click += null;
		}
	}

	public TextBoxWithButton()
	{
		base.BorderStyle = BorderStyle.FixedSingle;
		base.Height = 22;
		te.BorderStyle = BorderStyle.None;
		te.Height = base.Height;
		bt.Height = 22;
		bt.Width = 18;
		bt.Location = new Point(base.Width - 18, 2);
		bt.borderColor = te.BackColor;
		bt.EndColor = te.BackColor;
		bt.MouseClickColor1 = te.BackColor;
		bt.MouseClickColor2 = te.BackColor;
		bt.MouseHoverColor1 = te.BackColor;
		bt.MouseHoverColor2 = te.BackColor;
		bt.StartColor = te.BackColor;
		bt.BorderWidth = 0;
		base.Controls.Add(te);
		base.Controls.Add(bt);
	}

	protected override void OnSizeChanged(EventArgs e)
	{
		base.OnSizeChanged(e);
		te.Height = base.Height;
		te.Width = base.Width - 18;
	}
}
