using System.Collections.Generic;
using System.IO;
using System.Text;

namespace LBLIBRARY;

public class StreamReaderL : StreamReader
{
	public StreamReaderL(string path, Encoding e)
		: base(path, e)
	{
	}

	public string ReadNonEmptyLine()
	{
		string text = ReadLine();
		if (text.Contains(":") && !text.ToLower().StartsWith("float:"))
		{
			return text;
		}
		return ReadLine();
	}

	public string ReadWhile(string T, List<string> below)
	{
		string text = ReadLine();
		if (text.StartsWith(T))
		{
			return text;
		}
		below?.Add(text);
		return ReadWhile(T, below);
	}

	public string TryReadLine()
	{
		try
		{
			return ReadLine();
		}
		catch
		{
			return "";
		}
	}
}
