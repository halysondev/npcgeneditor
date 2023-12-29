using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace LBLIBRARY.Components;

public class NumericUpDownEx : Panel
{
	private bool IsBlack = true;

	private bool ThousandsSeparator;

	private int DecimalPlaces;

	private ButtonC Plus = new ButtonC
	{
		BackColor = Color.Transparent,
		BorderColor = Color.Transparent,
		BorderWidth = 1,
		ButtonShape = ButtonC.ButtonsShapes.Rect,
		ButtonText = "▴",
		StartColor = Color.DodgerBlue,
		EndColor = Color.MidnightBlue,
		FlatStyle = FlatStyle.Flat,
		Font = new Font("Segoe UI Semibold", 8.5f, FontStyle.Bold),
		ForeColor = Color.Black,
		GradientAngle = 90,
		MouseClickColor1 = Color.Turquoise,
		MouseClickColor2 = Color.Turquoise,
		MouseHoverColor1 = Color.Turquoise,
		MouseHoverColor2 = Color.DarkSlateGray,
		Name = "button1",
		ShowButtontext = true,
		Size = new Size(13, 9),
		TabIndex = 1,
		Text = "-",
		TextLocation_X = -1,
		TextLocation_Y = -5,
		Transparent1 = 250,
		Transparent2 = 250,
		UseVisualStyleBackColor = true,
		Location = new Point(142, 1),
		Anchor = (AnchorStyles.Top | AnchorStyles.Right)
	};

	private ButtonC Minus = new ButtonC
	{
		BackColor = Color.Transparent,
		BorderColor = Color.Transparent,
		BorderWidth = 1,
		ButtonShape = ButtonC.ButtonsShapes.Rect,
		ButtonText = "▾",
		EndColor = Color.MidnightBlue,
		FlatStyle = FlatStyle.Flat,
		Font = new Font("Segoe UI Semibold", 8.5f, FontStyle.Bold),
		ForeColor = Color.Black,
		GradientAngle = 90,
		MouseClickColor1 = Color.Turquoise,
		MouseClickColor2 = Color.Turquoise,
		MouseHoverColor1 = Color.Turquoise,
		MouseHoverColor2 = Color.DarkSlateGray,
		Name = "buttonZ1",
		ShowButtontext = true,
		Size = new Size(13, 9),
		StartColor = Color.DodgerBlue,
		TabIndex = 4,
		Text = "-",
		TextLocation_X = -1,
		TextLocation_Y = -3,
		Transparent1 = 250,
		Transparent2 = 250,
		UseVisualStyleBackColor = true,
		Location = new Point(142, 10),
		Anchor = (AnchorStyles.Top | AnchorStyles.Right)
	};

	private TextBoxEx Textbox = new TextBoxEx
	{
		Multiline = true,
		BorderStyle = BorderStyle.None,
		Location = new Point(0, 0),
		Anchor = (AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right),
		ForeColor = Color.Wheat,
		Size = new Size(142, 28),
		Font = new Font("Segue UI", 12f, FontStyle.Regular),
		Text = "0",
		BackColor = Color.Black,
		Name = "Textbox"
	};

	public int Increment { get; set; }

	public string TextBoxName
	{
		get
		{
			return Textbox.Name;
		}
		set
		{
			Textbox.Name = value;
		}
	}

	public bool SetBlack
	{
		get
		{
			return IsBlack;
		}
		set
		{
			if (value)
			{
				BackColor = Color.FromArgb(35, 35, 35);
				Textbox.BackColor = Color.Black;
				Textbox.ForeColor = Color.FromArgb(192, 255, 255);
				Minus.ForeColor = Color.Silver;
				Plus.ForeColor = Color.Silver;
				Minus.StartColor = Color.FromArgb(20, 20, 20);
				Minus.EndColor = Color.FromArgb(20, 20, 20);
				Plus.StartColor = Color.FromArgb(20, 20, 20);
				Plus.EndColor = Color.FromArgb(20, 20, 20);
				Plus.BorderColor = Color.FromArgb(20, 20, 80);
				Minus.BorderColor = Color.FromArgb(20, 20, 20);
			}
			else
			{
				BackColor = Color.White;
				Textbox.BackColor = SystemColors.Window;
				Textbox.ForeColor = SystemColors.ControlText;
				Minus.ForeColor = SystemColors.ControlText;
				Plus.ForeColor = SystemColors.ControlText;
				Minus.StartColor = Color.FromArgb(220, 220, 220);
				Plus.StartColor = Color.FromArgb(220, 220, 220);
				Minus.EndColor = Color.FromArgb(220, 220, 220);
				Plus.EndColor = Color.FromArgb(220, 220, 220);
				Plus.BorderColor = Color.Gray;
				Minus.BorderColor = Color.Gray;
			}
			IsBlack = value;
		}
	}

	public bool SetThousandsSeparator
	{
		get
		{
			return ThousandsSeparator;
		}
		set
		{
			ThousandsSeparator = value;
			Textbox.ThousandsSeparator = value;
		}
	}

	public int SetDecimalPlaces
	{
		get
		{
			return DecimalPlaces;
		}
		set
		{
			DecimalPlaces = value;
			Textbox.SetDecimalPlaces = value;
		}
	}

	public Color TextBoxForeColor
	{
		get
		{
			return Textbox.ForeColor;
		}
		set
		{
			Textbox.ForeColor = value;
		}
	}

	public Color TextBoxBackGroundColor
	{
		get
		{
			return Textbox.BackColor;
		}
		set
		{
			Textbox.BackColor = value;
		}
	}

	public Font TextBoxFont
	{
		get
		{
			return Textbox.Font;
		}
		set
		{
			Textbox.Font = value;
			TextRenderer.MeasureText("1", Font);
		}
	}

	public decimal Value
	{
		get
		{
			if (Textbox.DecimalPlaces > 0)
			{
				if (Textbox.Text.Contains(","))
				{
					string[] array = Textbox.Text.Split(',');
					return Convert.ToDecimal(array[0] + "," + array[1]);
				}
				return Convert.ToDecimal(Textbox.Text);
			}
			return Convert.ToDecimal(Textbox.Text);
		}
		set
		{
			Textbox.Text = value.ToString();
			Textbox.FormatValue();
		}
	}

	public decimal MinimalValue
	{
		get
		{
			return Textbox.MinimalValue;
		}
		set
		{
			Textbox.MinimalValue = value;
		}
	}

	public decimal MaximalValue
	{
		get
		{
			return Textbox.MaximalValue;
		}
		set
		{
			Textbox.MaximalValue = value;
		}
	}

	[Category("OwnEvents")]
	public event EventHandler TextBoxLeave
	{
		add
		{
			Textbox.Leave += value;
		}
		remove
		{
			Textbox.Leave += null;
		}
	}

	[Category("OwnEvents")]
	public event EventHandler TextBoxEnter
	{
		add
		{
			Textbox.Enter += value;
		}
		remove
		{
			Textbox.Enter += null;
		}
	}

	[Category("OwnEvents")]
	public event EventHandler TextBoxTextChanged
	{
		add
		{
			Textbox.TextChanged += value;
		}
		remove
		{
			Textbox.TextChanged += null;
		}
	}

	[Category("OwnEvents")]
	public event EventHandler TextBoxDoubleClick
	{
		add
		{
			Textbox.DoubleClick += value;
		}
		remove
		{
			Textbox.DoubleClick += null;
		}
	}

	[Category("OwnEvents")]
	public event KeyEventHandler TextBoxKeyDown
	{
		add
		{
			Textbox.KeyDown += value;
		}
		remove
		{
			Textbox.KeyDown += null;
		}
	}

	public NumericUpDownEx()
	{
		base.BorderStyle = BorderStyle.FixedSingle;
		BackColor = Color.White;
		base.Size = new Size(158, 21);
		base.Controls.Add(Textbox);
		base.Controls.Add(Plus);
		base.Controls.Add(Minus);
		Plus.Click += Plus_Click;
		Minus.Click += Minus_Click;
		Textbox.SetButtonsNames(Plus.Name, Minus.Name);
		Increment = 1;
	}

	private void Minus_Click(object sender, EventArgs e)
	{
		Textbox.Focus();
		Textbox.SelectionLength = 0;
		try
		{
			Textbox.Text = (Convert.ToDecimal(Textbox.Text) - (decimal)Increment).ToString();
		}
		catch
		{
			Textbox.Text = (Textbox.LastValue - (decimal)Increment).ToString();
		}
		if (DecimalPlaces != 0 && ThousandsSeparator)
		{
			Textbox.Text = string.Format(Textbox.FormatedText, double.Parse(Textbox.Text.Replace("\u00a0", "")));
		}
		try
		{
			if (Convert.ToDecimal(Textbox.Text) < MinimalValue)
			{
				Textbox.Text = MinimalValue.ToString();
			}
		}
		catch
		{
		}
	}

	private void Plus_Click(object sender, EventArgs e)
	{
		Textbox.Focus();
		Textbox.SelectionLength = 0;
		try
		{
			Textbox.Text = (Convert.ToDecimal(Textbox.Text) + (decimal)Increment).ToString();
		}
		catch
		{
			Textbox.Text = (Textbox.LastValue + (decimal)Increment).ToString();
		}
		if (DecimalPlaces != 0 && ThousandsSeparator)
		{
			Textbox.Text = string.Format(Textbox.FormatedText, double.Parse(Textbox.Text.Replace("\u00a0", "")));
		}
	}
}
