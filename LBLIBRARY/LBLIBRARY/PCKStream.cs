using System;
using System.IO;

namespace LBLIBRARY;

public class PCKStream : IDisposable
{
	protected BufferedStream pck;

	protected BufferedStream pkx;

	private string path = "";

	public long Position;

	public PCKKey key = new PCKKey();

	private const uint PCK_MAX_SIZE = 2147483392u;

	private const int BUFFER_SIZE = 33554432;

	public PCKStream(string path, PCKKey key = null)
	{
		this.path = path;
		if (key != null)
		{
			this.key = key;
		}
		pck = new BufferedStream(new FileStream(path, FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.ReadWrite), 33554432);
		if (File.Exists(path.Replace(".pck", ".pkx")) && Path.GetExtension(path) != ".cup")
		{
			pkx = new BufferedStream(new FileStream(path.Replace(".pck", ".pkx"), FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.ReadWrite), 33554432);
		}
	}

	public void Seek(long offset, SeekOrigin origin)
	{
		switch (origin)
		{
		case SeekOrigin.Begin:
			Position = offset;
			break;
		case SeekOrigin.Current:
			Position += offset;
			break;
		case SeekOrigin.End:
			Position = GetLenght() + offset;
			break;
		}
		if (Position < pck.Length)
		{
			pck.Seek(Position, SeekOrigin.Begin);
		}
		else
		{
			pkx.Seek(Position - pck.Length, SeekOrigin.Begin);
		}
	}

	public long GetLenght()
	{
		if (pkx == null)
		{
			return pck.Length;
		}
		return pck.Length + pkx.Length;
	}

	public byte[] ReadBytes(int count)
	{
		byte[] array = new byte[count];
		int num = 0;
		if (Position < pck.Length)
		{
			num = pck.Read(array, 0, count);
			if (num < count && pkx != null)
			{
				pkx.Seek(0L, SeekOrigin.Begin);
				num += pkx.Read(array, num, count - num);
			}
		}
		else if (Position > pck.Length && pkx != null)
		{
			num = pkx.Read(array, 0, count);
		}
		Position += count;
		return array;
	}

	public void WriteBytes(byte[] array)
	{
		if (Position + array.Length < 2147483392)
		{
			pck.Write(array, 0, array.Length);
		}
		else if (Position + array.Length > 2147483392)
		{
			if (pkx == null)
			{
				pkx = new BufferedStream(new FileStream(path.Replace(".pck", ".pkx"), FileMode.Create, FileAccess.ReadWrite), 33554432);
			}
			if (Position > 2147483392)
			{
				pkx.Write(array, 0, array.Length);
			}
			else
			{
				if (pkx == null)
				{
					pkx = new BufferedStream(new FileStream(path.Replace(".pck", ".pkx"), FileMode.Create, FileAccess.ReadWrite), 33554432);
				}
				pck.Write(array, 0, (int)(2147483392 - Position));
				pkx.Write(array, (int)(2147483392 - Position), array.Length - (int)(2147483392 - Position));
			}
		}
		Position += array.Length;
	}

	public uint ReadUInt32()
	{
		return BitConverter.ToUInt32(ReadBytes(4), 0);
	}

	public int ReadInt32()
	{
		return BitConverter.ToInt32(ReadBytes(4), 0);
	}

	public void WriteUInt32(uint value)
	{
		WriteBytes(BitConverter.GetBytes(value));
	}

	public void WriteInt32(int value)
	{
		WriteBytes(BitConverter.GetBytes(value));
	}

	public void WriteInt16(short value)
	{
		WriteBytes(BitConverter.GetBytes(value));
	}

	public void Dispose()
	{
		pck?.Close();
		pkx?.Close();
	}
}
