using System;
using System.Text;

namespace sELedit;

public class talk_proc
{
	public int id_talk;

	public byte[] text;

	public int num_window;

	public window[] windows;

	public string GetText()
	{
		Encoding encoding = Encoding.GetEncoding("Unicode");
		return encoding.GetString(text);
	}

	public void SetText(string Value)
	{
		Encoding encoding = Encoding.GetEncoding("Unicode");
		byte[] array = new byte[128];
		byte[] bytes = encoding.GetBytes(Value);
		if (array.Length > bytes.Length)
		{
			Array.Copy(bytes, array, bytes.Length);
		}
		else
		{
			Array.Copy(bytes, array, array.Length);
		}
		text = array;
	}
}
