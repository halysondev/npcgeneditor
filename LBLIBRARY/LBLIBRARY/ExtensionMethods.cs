using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;

namespace LBLIBRARY;

public static class ExtensionMethods
{
	private static string[] sp = new string[1] { ":" };

	public static byte[] GetBytesFromString(this string str, int l, Encoding e)
	{
		str = str.Split(default(char))[0];
		byte[] array = new byte[l];
		if (e.GetByteCount(str) > l)
		{
			Array.Copy(e.GetBytes(str), 0, array, 0, l);
		}
		else
		{
			Array.Copy(e.GetBytes(str), array, e.GetByteCount(str));
		}
		return array;
	}

	public static string Decode(this byte[] array, Encoding e)
	{
		return e.GetString(array);
	}

	public static string GetIconNameFromString(this string Source)
	{
		return Source.Split('\\').Last();
	}

	public static string ToString(this byte[] Array, Encoding e)
	{
		return e.GetString(Array).TrimEnd(default(char));
	}

	public static Bitmap ResizeImage(this Bitmap Source, int Width, int Height)
	{
		Bitmap bitmap = new Bitmap(Width, Height);
		using Graphics graphics = Graphics.FromImage(bitmap);
		graphics.DrawImage(Source, 0, 0, Width, Height);
		graphics.Dispose();
		return bitmap;
	}

	public static int SumBytes(this List<string> Types)
	{
		int num = 0;
		foreach (string Type in Types)
		{
			if (Type.Contains("int32") || Type.Contains("link") || Type.Contains("combo") || Type.Contains("float"))
			{
				num += 4;
			}
			else if (Type.Contains("byte") && !Type.Contains("AUTO"))
			{
				num += Convert.ToInt32(Type.Split(':')[1]);
			}
			else if (Type.Contains("wstring"))
			{
				num += Convert.ToInt32(Type.Split(':')[1]);
			}
			else if (Type.Contains("string"))
			{
				num += Convert.ToInt32(Type.Split(':')[1]);
			}
			else if (Type.Contains("byte:AUTO"))
			{
				num = -1;
				break;
			}
		}
		return num;
	}

	public static PWHelper.Elements RemoveNonObjectList(this PWHelper.Elements Source, List<int> ls)
	{
		for (int num = Source.ElementsLists.Count - 1; num >= ls.Min(); num--)
		{
			if (ls.Contains(num))
			{
				Source.ElementsLists.RemoveAt(num);
			}
		}
		return Source;
	}

	public static List<string> ToLines(this MemoryStream ms, Encoding e)
	{
		if (ms != null)
		{
			return new StreamReader(ms, e).ReadToEnd().Split(new string[1] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries).ToList();
		}
		return null;
	}

	public static string GetEcmLineValue(this string Line)
	{
		return Line.Split(sp, StringSplitOptions.RemoveEmptyEntries)[1].Replace(" ", "");
	}

	public static string[] SplitEcmLine(this string Line)
	{
		return (from e in Line.Split(sp, StringSplitOptions.RemoveEmptyEntries)
			select e.Replace(" ", "")).ToArray();
	}

	public static int ToInt32(this string value)
	{
		return Convert.ToInt32(value);
	}

	public static float ToSingle(this string value)
	{
		return Convert.ToSingle(value.Replace('.', ','));
	}

	public static bool ToBoolean(this string value)
	{
		if (!(value == "1"))
		{
			return false;
		}
		return true;
	}
}
