using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;

namespace LBLIBRARY.Components;

public class DateTimePickerA : DateTimePicker
{
	private Color _backDisabledColor;

	private Color OwnForeColor;

	private Color OwnBorderColor;

	private bool IsBlack;

	private Color DropDownButtonBackColor = Color.FromArgb(229, 241, 251);

	private Brush DropDownButtonForeColor = Brushes.Black;

	private Brush DropDownButtonMouseMoveBackColor = Brushes.LightBlue;

	private Graphics g;

	[Category("OwnProperties")]
	[Browsable(true)]
	public override Color BackColor
	{
		get
		{
			return base.BackColor;
		}
		set
		{
			base.BackColor = value;
		}
	}

	[Category("OwnProperties")]
	public Color SetColor
	{
		get
		{
			return OwnForeColor;
		}
		set
		{
			OwnForeColor = value;
		}
	}

	[Category("OwnProperties")]
	public Color BackDisabledColor
	{
		get
		{
			return _backDisabledColor;
		}
		set
		{
			_backDisabledColor = value;
		}
	}

	[Category("OwnProperties")]
	public Color SetBorderColor
	{
		get
		{
			return OwnBorderColor;
		}
		set
		{
			OwnBorderColor = value;
		}
	}

	[Category("OwnProperties")]
	public bool SetColorBlack
	{
		get
		{
			return IsBlack;
		}
		set
		{
			if (value)
			{
				IsBlack = true;
				BackColor = Color.FromArgb(20, 20, 20);
				OwnBorderColor = Color.FromArgb(120, 120, 120);
				OwnForeColor = Color.FromArgb(235, 235, 235);
				DropDownButtonBackColor = Color.FromArgb(20, 20, 20);
				DropDownButtonForeColor = new SolidBrush(Color.FromArgb(235, 235, 235));
				DropDownButtonMouseMoveBackColor = new SolidBrush(Color.FromArgb(50, 50, 50));
			}
			else
			{
				IsBlack = false;
				BackColor = Color.White;
				OwnBorderColor = Color.FromArgb(150, 150, 150);
				OwnForeColor = SystemColors.ControlText;
				DropDownButtonBackColor = Color.FromArgb(229, 241, 251);
				DropDownButtonForeColor = new SolidBrush(Color.FromArgb(70, 70, 70));
				DropDownButtonMouseMoveBackColor = new SolidBrush(Color.LightBlue);
			}
			Invalidate();
		}
	}

	public DateTimePickerA()
	{
		SetStyle(ControlStyles.UserPaint, value: true);
		_backDisabledColor = Color.FromKnownColor(KnownColor.Control);
	}

	protected override void OnMouseMove(MouseEventArgs e)
	{
		Rectangle rect = new Rectangle(base.ClientRectangle.Width - 17, 1, 17, 19);
		base.OnMouseMove(e);
		if (e.X >= base.ClientRectangle.Width - 17 && e.X < base.ClientRectangle.Width - 1)
		{
			g.FillRectangle(DropDownButtonMouseMoveBackColor, rect);
			g.DrawString("▾", new Font("Segui UI", 14f, FontStyle.Regular), DropDownButtonForeColor, new PointF(rect.X, rect.Y + 1));
		}
		else
		{
			g.FillRectangle(new SolidBrush(DropDownButtonBackColor), rect);
			g.DrawString("▾", new Font("Segui UI", 14f, FontStyle.Regular), DropDownButtonForeColor, new PointF(rect.X, rect.Y + 1));
		}
	}

	protected override void OnPaint(PaintEventArgs e)
	{
		g = CreateGraphics();
		Rectangle rectangle = new Rectangle(base.ClientRectangle.Width - 17, 0, 17, 20);
		Brush brush;
		ComboBoxState comboBoxState;
		if (base.Enabled)
		{
			brush = new SolidBrush(BackColor);
			comboBoxState = ComboBoxState.Hot;
		}
		else
		{
			brush = new SolidBrush(_backDisabledColor);
			comboBoxState = ComboBoxState.Disabled;
		}
		g.FillRectangle(brush, 0, 0, base.ClientRectangle.Width, base.ClientRectangle.Height);
		g.DrawString(Text, Font, new SolidBrush(OwnForeColor), 0f, 2f);
		ComboBoxRenderer.DrawDropDownButton(g, rectangle, comboBoxState);
		g.FillRectangle(new SolidBrush(DropDownButtonBackColor), rectangle);
		g.DrawRectangle(new Pen(OwnBorderColor), 0, 0, base.Width - 1, base.Height - 1);
		g.DrawString("▾", new Font("Segui UI", 14f, FontStyle.Regular), DropDownButtonForeColor, new PointF(rectangle.X, rectangle.Y + 1));
		brush.Dispose();
	}
}
