using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace LBLIBRARY;

public class ArchiveEngine : IDisposable
{
	private bool disposed;

	public PCKStream PckFile;

	public List<PCKFileEntry> Files;

	private int _CompressionLevel = 9;

	public int CompressionLevel
	{
		get
		{
			return _CompressionLevel;
		}
		set
		{
			if (_CompressionLevel != value)
			{
				_CompressionLevel = value;
			}
		}
	}

	public void Dispose()
	{
		Files = null;
		PckFile.Dispose();
		GC.SuppressFinalize(this);
	}

	~ArchiveEngine()
	{
		CleanUp(clean: true);
	}

	private void CleanUp(bool clean)
	{
		if (!disposed && clean)
		{
			PckFile.Dispose();
		}
		disposed = true;
	}

	public int GetFilesCount(PCKStream stream)
	{
		stream.Seek(-8L, SeekOrigin.End);
		return stream.ReadInt32();
	}

	public PCKFileEntry[] ReadFileTable(PCKStream stream)
	{
		stream.Seek(-8L, SeekOrigin.End);
		int num = stream.ReadInt32();
		stream.Seek(-272L, SeekOrigin.End);
		long offset = (uint)(stream.ReadUInt32() ^ stream.key.KEY_1);
		PCKFileEntry[] array = new PCKFileEntry[num];
		stream.Seek(offset, SeekOrigin.Begin);
		for (int i = 0; i < array.Length; i++)
		{
			int count = stream.ReadInt32() ^ stream.key.KEY_1;
			stream.ReadInt32();
			array[i] = new PCKFileEntry(stream.ReadBytes(count));
		}
		return array;
	}

	public ArchiveEngine(string path)
	{
		PckFile = new PCKStream(path);
		Files = ReadFileTable(PckFile).ToList();
	}

	public byte[] ReadFile(PCKStream stream, PCKFileEntry file)
	{
		if (file != null)
		{
			stream.Seek(file.Offset, SeekOrigin.Begin);
			byte[] array = stream.ReadBytes(file.CompressedSize);
			if (file.CompressedSize >= file.Size)
			{
				return array;
			}
			return PCKZlib.Decompress(array, file.Size);
		}
		return new byte[0];
	}
}
