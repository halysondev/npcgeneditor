using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace LBLIBRARY.Components;

public class ComboBoxA : ComboBox
{
	public enum MyEnum
	{
		True,
		False,
		None
	}

	private Color BorderColor;

	private Font TextFont = new Font("Segue UI", 9f, FontStyle.Regular);

	private MyEnum IsBlack;

	public new DrawMode DrawMode { get; set; }

	public Color SelectionColor { get; set; }

	public Color ArrowColor { get; set; }

	public Font SetFont
	{
		get
		{
			return TextFont;
		}
		set
		{
			TextFont = value;
		}
	}

	public MyEnum SetColorBlack
	{
		get
		{
			return IsBlack;
		}
		set
		{
			switch (value)
			{
			case MyEnum.True:
				BackColor = Color.FromArgb(20, 20, 20);
				ArrowColor = Color.Silver;
				SelectionColor = Color.Green;
				ForeColor = Color.FromArgb(235, 235, 235);
				BorderColor = Color.FromArgb(80, 80, 80);
				IsBlack = MyEnum.True;
				break;
			case MyEnum.False:
				BackColor = SystemColors.Window;
				ArrowColor = Color.FromArgb(70, 70, 70);
				SelectionColor = Color.FromArgb(128, 128, 255);
				ForeColor = SystemColors.WindowText;
				BorderColor = Color.FromArgb(70, 70, 70);
				IsBlack = MyEnum.False;
				break;
			default:
				IsBlack = MyEnum.None;
				break;
			}
			Invalidate();
		}
	}

	public ComboBoxA()
	{
		base.DrawMode = DrawMode.OwnerDrawFixed;
		base.FlatStyle = FlatStyle.Flat;
		base.DropDownStyle = ComboBoxStyle.DropDownList;
		SelectionColor = Color.Red;
		BorderColor = Color.Black;
		ArrowColor = Color.Black;
		SetStyle(ControlStyles.UserPaint, value: true);
		base.DrawItem += AdvancedComboBox_DrawItem;
	}

	private void AdvancedComboBox_DrawItem(object sender, DrawItemEventArgs e)
	{
		if (e.Index >= 0)
		{
			ComboBox comboBox = sender as ComboBox;
			if ((e.State & DrawItemState.Selected) == DrawItemState.Selected)
			{
				e.Graphics.FillRectangle(new SolidBrush(SelectionColor), e.Bounds);
			}
			else
			{
				e.Graphics.FillRectangle(new SolidBrush(comboBox.BackColor), e.Bounds);
			}
			e.Graphics.DrawString(comboBox.Items[e.Index].ToString(), TextFont, new SolidBrush(comboBox.ForeColor), new Point(1, e.Bounds.Y - 1));
			e.DrawFocusRectangle();
		}
	}

	protected override void OnPaint(PaintEventArgs e)
	{
		base.OnPaint(e);
		int verticalScrollBarWidth = SystemInformation.VerticalScrollBarWidth;
		Color controlLightLight = SystemColors.ControlLightLight;
		Color controlDark = SystemColors.ControlDark;
		LinearGradientBrush linearGradientBrush = new LinearGradientBrush(new Rectangle(base.Width, 0, verticalScrollBarWidth, base.Height), controlLightLight, controlDark, LinearGradientMode.Vertical);
		e.Graphics.FillRectangle(linearGradientBrush, new Rectangle(base.Width, 0, verticalScrollBarWidth, base.Height));
		linearGradientBrush.Dispose();
		Pen pen = new Pen(SystemColors.ButtonShadow, 1f);
		e.Graphics.DrawRectangle(pen, new Rectangle(base.Width, 0, verticalScrollBarWidth, base.Height).X, new Rectangle(base.Width, 0, verticalScrollBarWidth, base.Height).Y, new Rectangle(base.Width, 0, verticalScrollBarWidth, base.Height).Width - 2, new Rectangle(base.Width, 0, verticalScrollBarWidth, base.Height).Height - 2);
		pen.Dispose();
		SolidBrush solidBrush = new SolidBrush(ArrowColor);
		Point[] points = new Point[3]
		{
			new Point(base.Width - 15, base.Height / 2 - 2),
			new Point(base.Width - 4, base.Height / 2 - 2),
			new Point(base.Width - 10, base.Height / 2 + 4)
		};
		e.Graphics.FillPolygon(solidBrush, points);
		solidBrush.Dispose();
		if (base.SelectedItem != null)
		{
			e.Graphics.DrawString(Text, TextFont, new SolidBrush(ForeColor), 1f, 2f);
		}
		e.Graphics.DrawRectangle(new Pen(BorderColor, 2f), 0f, 0f, base.Width, base.Height);
	}
}
