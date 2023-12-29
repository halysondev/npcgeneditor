using System;
using System.IO;
using System.Text;

namespace LBLIBRARY;

public class PCKFileEntry
{
	public string Path { get; set; }

	public uint Offset { get; set; }

	public int Size { get; set; }

	public int CompressedSize { get; set; }

	public PCKFileEntry()
	{
	}

	public PCKFileEntry(byte[] bytes)
	{
		Read(bytes);
	}

	public void Read(byte[] bytes)
	{
		if (bytes.Length < 276)
		{
			bytes = PCKZlib.Decompress(bytes, 276);
		}
		BinaryReader binaryReader = new BinaryReader(new MemoryStream(bytes));
		Path = Encoding.GetEncoding(936).GetString(binaryReader.ReadBytes(260)).Split(new string[1] { "\0" }, StringSplitOptions.RemoveEmptyEntries)[0].Replace("/", "\\").ToLower();
		Offset = binaryReader.ReadUInt32();
		Size = binaryReader.ReadInt32();
		CompressedSize = binaryReader.ReadInt32();
		binaryReader.Close();
	}

	public byte[] Write(int CompressionLevel)
	{
		byte[] array = new byte[276];
		BinaryWriter binaryWriter = new BinaryWriter(new MemoryStream(array));
		binaryWriter.Write(Encoding.GetEncoding("GB2312").GetBytes(Path.Replace("/", "\\")));
		binaryWriter.BaseStream.Seek(260L, SeekOrigin.Begin);
		binaryWriter.Write(Offset);
		binaryWriter.Write(Size);
		binaryWriter.Write(CompressedSize);
		binaryWriter.Write(0);
		binaryWriter.BaseStream.Seek(0L, SeekOrigin.Begin);
		binaryWriter.Close();
		byte[] array2 = PCKZlib.Compress(array, CompressionLevel);
		if (array2.Length >= 276)
		{
			return array;
		}
		return array2;
	}
}
