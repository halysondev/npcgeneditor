using System;
using System.IO;
using zlib;

namespace LBLIBRARY;

public static class PCKZlib
{
	public static byte[] Decompress(byte[] bytes, int size)
	{
		byte[] array = new byte[size];
		ZOutputStream output = new ZOutputStream(new MemoryStream(array));
		try
		{
			CopyStream(new MemoryStream(bytes), output, size);
		}
		catch
		{
			Console.WriteLine("Bad zlib data");
		}
		return array;
	}

	public static byte[] Compress(byte[] bytes, int CompressionLevel)
	{
		MemoryStream memoryStream = new MemoryStream();
		ZOutputStream zOutputStream = new ZOutputStream(memoryStream, CompressionLevel);
		CopyStream(new MemoryStream(bytes), zOutputStream, bytes.Length);
		zOutputStream.finish();
		if (memoryStream.ToArray().Length >= bytes.Length)
		{
			return bytes;
		}
		return memoryStream.ToArray();
	}

	public static void CopyStream(Stream input, Stream output, int Size)
	{
		byte[] buffer = new byte[Size];
		int count;
		while ((count = input.Read(buffer, 0, Size)) > 0)
		{
			output.Write(buffer, 0, count);
		}
		output.Flush();
	}
}
