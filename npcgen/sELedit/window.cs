using System.Text;

namespace sELedit;

public class window
{
	public int id;

	public int id_parent;

	public int talk_text_len;

	public byte[] talk_text;

	public int num_option;

	public option[] options;

	public string GetText()
	{
		Encoding encoding = Encoding.GetEncoding("Unicode");
		return encoding.GetString(talk_text);
	}

	public void SetText(string Value)
	{
		Encoding encoding = Encoding.GetEncoding("Unicode");
		talk_text = encoding.GetBytes(Value + "\0");
		talk_text_len = talk_text.Length / 2;
	}
}
