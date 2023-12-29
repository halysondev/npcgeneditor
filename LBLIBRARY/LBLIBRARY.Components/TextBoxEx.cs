using System;
using System.Globalization;
using System.Linq;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace LBLIBRARY.Components;

public class TextBoxEx : TextBox
{
	private string First = "";

	private string Second = "";

	private bool allowSpace;

	public string FormatedText;

	public bool ThousandsSeparator;

	public int DecimalPlaces;

	private bool BackSpace;

	public decimal LastValue;

	public decimal MinimalValue { get; set; }

	public decimal MaximalValue { get; set; }

	public int SetDecimalPlaces
	{
		get
		{
			return DecimalPlaces;
		}
		set
		{
			DecimalPlaces = value;
			if (value != 0)
			{
				Text = "0," + new string('0', value);
				FormatedText = "{0:#,##0.";
				for (int i = 0; i < value; i++)
				{
					FormatedText += "0";
				}
				FormatedText += "}";
			}
			else
			{
				Text = Text.Split(',')[0];
			}
		}
	}

	[DllImport("user32.dll", CharSet = CharSet.Auto)]
	internal static extern IntPtr GetFocus();

	protected override void OnKeyPress(KeyPressEventArgs e)
	{
		base.OnKeyPress(e);
		if (e.KeyChar != '\b')
		{
			BackSpace = true;
			NumberFormatInfo numberFormat = CultureInfo.CurrentCulture.NumberFormat;
			string numberDecimalSeparator = numberFormat.NumberDecimalSeparator;
			string numberGroupSeparator = numberFormat.NumberGroupSeparator;
			string negativeSign = numberFormat.NegativeSign;
			string text = e.KeyChar.ToString();
			if (!char.IsDigit(e.KeyChar) && !text.Equals(numberDecimalSeparator) && !text.Equals(numberGroupSeparator) && !text.Equals(negativeSign) && e.KeyChar != '\b' && (!allowSpace || e.KeyChar != ' '))
			{
				e.Handled = true;
			}
		}
	}

	protected override void OnKeyDown(KeyEventArgs e)
	{
		base.OnKeyDown(e);
		if (e.Control & (e.KeyCode == Keys.A))
		{
			SelectAll();
		}
	}

	protected override void OnLeave(EventArgs e)
	{
		Control control = Control.FromHandle(GetFocus());
		try
		{
			if (Convert.ToDecimal(Text) < MinimalValue)
			{
				Text = MinimalValue.ToString();
			}
		}
		catch
		{
		}
		if (control.Name.ToString().Length == 0)
		{
			return;
		}
		string text = control.Name.ToString();
		if (!(text != First) || !(text != Second))
		{
			return;
		}
		if (string.IsNullOrWhiteSpace(Text))
		{
			Text = "0," + new string('0', DecimalPlaces);
		}
		if (Text.Where((char z) => z == '-').Count() > 1)
		{
			Text = LastValue.ToString();
		}
		base.OnLeave(e);
		if (ThousandsSeparator && DecimalPlaces != 0)
		{
			if (Text.Contains(","))
			{
				string[] array = Text.Split(',');
				Text = array[0] + "," + array[1];
			}
			Text = string.Format(FormatedText, decimal.Parse(Text.Replace("\u00a0", "")));
		}
	}

	protected override void OnTextChanged(EventArgs e)
	{
		base.OnTextChanged(e);
		if (DecimalPlaces == 0 && Text.Contains(","))
		{
			Text = Text.Remove(Text.IndexOf(','), 1);
		}
	}

	protected override void OnEnter(EventArgs e)
	{
		base.OnEnter(e);
		try
		{
			LastValue = Convert.ToDecimal(Text);
		}
		catch
		{
		}
	}

	public void FormatValue()
	{
		if (ThousandsSeparator && DecimalPlaces != 0)
		{
			Text = string.Format(FormatedText, decimal.Parse(Text.Replace("\u00a0", "")));
		}
	}

	public void SetButtonsNames(string s1, string s2)
	{
		First = s1;
		Second = s2;
	}
}
