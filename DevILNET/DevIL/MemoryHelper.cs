using System;
using System.IO;
using System.Runtime.InteropServices;

namespace DevIL;

public static class MemoryHelper
{
	public static byte[] ReadByteBuffer(IntPtr pointer, int numBytes)
	{
		if (pointer == IntPtr.Zero)
		{
			return null;
		}
		byte[] array = new byte[numBytes];
		Marshal.Copy(pointer, array, 0, numBytes);
		return array;
	}

	public static T[] MarshalArray<T>(IntPtr pointer, int length) where T : struct
	{
		return MarshalArray<T>(pointer, length, pointerToPointer: false);
	}

	public static T[] MarshalArray<T>(IntPtr pointer, int length, bool pointerToPointer) where T : struct
	{
		if (pointer == IntPtr.Zero)
		{
			return null;
		}
		try
		{
			Type typeFromHandle = typeof(T);
			int num = (pointerToPointer ? IntPtr.Size : Marshal.SizeOf(typeof(T)));
			T[] array = new T[length];
			for (int i = 0; i < length; i++)
			{
				IntPtr ptr = pointer + num * i;
				if (pointerToPointer)
				{
					ptr = Marshal.ReadIntPtr(ptr);
				}
				array[i] = (T)Marshal.PtrToStructure(ptr, typeFromHandle);
			}
			return array;
		}
		catch (Exception)
		{
			return null;
		}
	}

	public static T MarshalStructure<T>(IntPtr ptr) where T : struct
	{
		if (ptr == IntPtr.Zero)
		{
			return default(T);
		}
		return (T)Marshal.PtrToStructure(ptr, typeof(T));
	}

	public static float Clamp(float value, float min, float max)
	{
		value = ((value > max) ? max : value);
		value = ((value < min) ? min : value);
		return value;
	}

	public static double Clamp(double value, double min, double max)
	{
		value = ((value > max) ? max : value);
		value = ((value < min) ? min : value);
		return value;
	}

	public static int RoundUpToPowerOfTwo(int value)
	{
		value--;
		value |= value >> 1;
		value |= value >> 2;
		value |= value >> 4;
		value |= value >> 8;
		value |= value >> 16;
		return value + 1;
	}

	public static int RoundDownToPowerOfTwo(int value)
	{
		value |= value >> 1;
		value |= value >> 2;
		value |= value >> 4;
		value |= value >> 8;
		value |= value >> 16;
		return value - (value >> 1);
	}

	public static int RoundToNearestPowerOfTwo(int value)
	{
		int num = RoundUpToPowerOfTwo(value);
		int num2 = RoundDownToPowerOfTwo(value);
		int num3 = Math.Abs(num - value);
		int num4 = Math.Abs(value - num2);
		if (num4 < num3)
		{
			return num2;
		}
		return num;
	}

	public static int GetFormatComponentCount(DataFormat format)
	{
		switch (format)
		{
		case DataFormat.ColorIndex:
		case DataFormat.Alpha:
		case DataFormat.Luminance:
			return 1;
		case DataFormat.LuminanceAlpha:
			return 2;
		case DataFormat.RGB:
		case DataFormat.BGR:
			return 3;
		case DataFormat.RGBA:
		case DataFormat.BGRA:
			return 4;
		default:
			return 0;
		}
	}

	public static int GetPaletteComponentCount(PaletteType palette)
	{
		switch (palette)
		{
		case PaletteType.RGB24:
		case PaletteType.BGR24:
			return 3;
		case PaletteType.RGB32:
		case PaletteType.RGBA32:
		case PaletteType.BGR32:
		case PaletteType.BGRA32:
			return 4;
		default:
			return 0;
		}
	}

	public static DataFormat GetPaletteBaseFormat(PaletteType palette)
	{
		return palette switch
		{
			PaletteType.RGB24 => DataFormat.RGB, 
			PaletteType.RGB32 => DataFormat.RGBA, 
			PaletteType.RGBA32 => DataFormat.RGBA, 
			PaletteType.BGR24 => DataFormat.BGR, 
			PaletteType.BGR32 => DataFormat.BGRA, 
			PaletteType.BGRA32 => DataFormat.BGRA, 
			_ => DataFormat.RGBA, 
		};
	}

	public static int GetDataTypeSize(DataType dataType)
	{
		switch (dataType)
		{
		case DataType.Byte:
		case DataType.UnsignedByte:
			return 1;
		case DataType.Short:
		case DataType.UnsignedShort:
		case DataType.Half:
			return 2;
		case DataType.Int:
		case DataType.UnsignedInt:
		case DataType.Float:
			return 4;
		case DataType.Double:
			return 8;
		default:
			return 0;
		}
	}

	public static int GetBpp(DataFormat format, DataType dataType)
	{
		return GetDataTypeSize(dataType) * GetFormatComponentCount(format);
	}

	public static int GetDataSize(int width, int height, int depth, DataFormat format, DataType dataType)
	{
		if (width <= 0)
		{
			width = 1;
		}
		if (height <= 0)
		{
			height = 1;
		}
		if (depth <= 0)
		{
			depth = 1;
		}
		return width * height * depth * GetBpp(format, dataType);
	}

	public static byte[] ReadStreamFully(Stream stream, int initialLength)
	{
		if (initialLength < 1)
		{
			initialLength = 32768;
		}
		byte[] array = new byte[initialLength];
		int num = 0;
		int num2;
		while ((num2 = stream.Read(array, num, array.Length - num)) > 0)
		{
			num += num2;
			if (num == array.Length)
			{
				int num3 = stream.ReadByte();
				if (num3 == -1)
				{
					return array;
				}
				byte[] array2 = new byte[array.Length * 2];
				Array.Copy(array, array2, array.Length);
				array2[num] = (byte)num3;
				array = array2;
				num++;
			}
		}
		byte[] array3 = new byte[num];
		Array.Copy(array, array3, num);
		return array3;
	}
}
