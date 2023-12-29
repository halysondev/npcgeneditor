using System;
using System.IO;
using System.Runtime.InteropServices;

namespace DevIL.Unmanaged;

public static class IL
{
	private const string ILDLL = "DevIL.dll";

	private static bool _init = false;

	private static object s_sync = new object();

	private static int s_ref = 0;

	public static bool IsInitialized => _init;

	internal static void AddRef()
	{
		lock (s_sync)
		{
			if (s_ref == 0)
			{
				Initialize();
				ILU.Initialize();
			}
			s_ref++;
		}
	}

	internal static void Release()
	{
		lock (s_sync)
		{
			if (s_ref != 0)
			{
				s_ref--;
				if (s_ref == 0)
				{
					Shutdown();
				}
			}
		}
	}

	public static bool ActiveFace(int faceNum)
	{
		if (faceNum >= 0)
		{
			return ilActiveFace((uint)faceNum);
		}
		return false;
	}

	public static bool ActiveImage(int imageNum)
	{
		if (imageNum >= 0)
		{
			return ilActiveImage((uint)imageNum);
		}
		return false;
	}

	public static bool ActiveLayer(int layerNum)
	{
		if (layerNum >= 0)
		{
			return ilActiveLayer((uint)layerNum);
		}
		return false;
	}

	public static bool ActiveMipMap(int mipMapNum)
	{
		if (mipMapNum >= 0)
		{
			return ilActiveMipmap((uint)mipMapNum);
		}
		return false;
	}

	[DllImport("DevIL.dll", CallingConvention = CallingConvention.StdCall, EntryPoint = "ilApplyPal")]
	[return: MarshalAs(UnmanagedType.U1)]
	public static extern bool ApplyPalette([In][MarshalAs(UnmanagedType.LPStr)] string FileName);

	public static void BindImage(ImageID imageID)
	{
		if (imageID.ID >= 0)
		{
			ilBindImage((uint)imageID.ID);
		}
	}

	public static bool Blit(ImageID srcImageID, int destX, int destY, int destZ, int srcX, int srcY, int srcZ, int width, int height, int depth)
	{
		if (srcImageID.ID >= 0)
		{
			return ilBlit((uint)srcImageID.ID, destX, destY, destZ, (uint)srcX, (uint)srcY, (uint)srcZ, (uint)width, (uint)height, (uint)depth);
		}
		return false;
	}

	[DllImport("DevIL.dll", CallingConvention = CallingConvention.StdCall, EntryPoint = "ilClampNTSC")]
	[return: MarshalAs(UnmanagedType.U1)]
	public static extern bool ClampNTSC();

	[DllImport("DevIL.dll", CallingConvention = CallingConvention.StdCall, EntryPoint = "ilClearColour")]
	public static extern void ClearColor(float red, float green, float blue, float alpha);

	[DllImport("DevIL.dll", CallingConvention = CallingConvention.StdCall, EntryPoint = "ilClearImage")]
	[return: MarshalAs(UnmanagedType.U1)]
	public static extern bool ClearImage();

	public static int CloneCurrentImage()
	{
		return (int)ilCloneCurImage();
	}

	public static bool ConvertImage(DataFormat destFormat, DataType destType)
	{
		return ilConvertImage((uint)destFormat, (uint)destType);
	}

	public static bool ConvertPalette(PaletteType palType)
	{
		return ilConvertPal((uint)palType);
	}

	public static bool CopyImage(ImageID srcImageID)
	{
		return ilCopyImage((uint)srcImageID.ID);
	}

	public unsafe static byte[] CopyPixels(int xOffset, int yOffset, int zOffset, int width, int height, int depth, DataFormat format, DataType dataType)
	{
		int dataSize = MemoryHelper.GetDataSize(width, height, depth, format, dataType);
		byte[] array = new byte[dataSize];
		fixed (byte* value = array)
		{
			if (ilCopyPixels((uint)xOffset, (uint)yOffset, (uint)zOffset, (uint)width, (uint)height, (uint)depth, (uint)format, (uint)dataType, new IntPtr(value)) == 0)
			{
				return null;
			}
		}
		return array;
	}

	public static bool CopyPixels(int xOffset, int yOffset, int zOffset, int width, int height, int depth, DataFormat format, DataType dataType, IntPtr data)
	{
		if (data == IntPtr.Zero)
		{
			return false;
		}
		return ilCopyPixels((uint)xOffset, (uint)yOffset, (uint)zOffset, (uint)width, (uint)height, (uint)depth, (uint)format, (uint)dataType, data) != 0;
	}

	public static bool CreateSubImage(SubImageType subImageType, int subImageCount)
	{
		if (ilCreateSubImage((uint)subImageType, (uint)subImageCount) != 0)
		{
			return true;
		}
		return false;
	}

	[DllImport("DevIL.dll", CallingConvention = CallingConvention.StdCall, EntryPoint = "ilDefaultImage")]
	[return: MarshalAs(UnmanagedType.U1)]
	public static extern bool DefaultImage();

	public static void DeleteImage(ImageID imageID)
	{
		if (!(imageID > 0))
		{
			ilDeleteImage((uint)imageID.ID);
		}
	}

	public static void DeleteImages(ImageID[] imageIDs)
	{
		uint[] array = new uint[imageIDs.Length];
		for (int i = 0; i < imageIDs.Length; i++)
		{
			array[i] = (uint)imageIDs[i].ID;
		}
		UIntPtr num = new UIntPtr((uint)array.Length);
		ilDeleteImages(num, array);
	}

	public static ImageType DetermineImageType(string fileName)
	{
		if (string.IsNullOrEmpty(fileName))
		{
			return ImageType.Unknown;
		}
		return (ImageType)ilDetermineType(fileName);
	}

	public unsafe static ImageType DetermineImageType(byte[] lump)
	{
		if (lump == null || lump.Length == 0)
		{
			return ImageType.Unknown;
		}
		uint size = (uint)lump.Length;
		fixed (byte* value = lump)
		{
			return (ImageType)ilDetermineTypeL(new IntPtr(value), size);
		}
	}

	public static ImageType DetermineImageTypeFromExtension(string extension)
	{
		if (string.IsNullOrEmpty(extension))
		{
			return ImageType.Unknown;
		}
		return (ImageType)ilTypeFromExt(extension);
	}

	public static bool Disable(ILEnable mode)
	{
		return ilDisable((uint)mode);
	}

	[DllImport("DevIL.dll", CallingConvention = CallingConvention.StdCall, EntryPoint = "ilDxtcDataToImage")]
	[return: MarshalAs(UnmanagedType.U1)]
	public static extern bool DxtcDataToImage();

	[DllImport("DevIL.dll", CallingConvention = CallingConvention.StdCall, EntryPoint = "ilDxtcDataToSurface")]
	[return: MarshalAs(UnmanagedType.U1)]
	public static extern bool DxtcDataToSurface();

	public static bool Enable(ILEnable mode)
	{
		return ilEnable((uint)mode);
	}

	[DllImport("DevIL.dll", CallingConvention = CallingConvention.StdCall, EntryPoint = "ilFlipSurfaceDxtcData")]
	public static extern void FlipSurfaceDxtcData();

	public static ImageID GenerateImage()
	{
		return new ImageID((int)ilGenImage());
	}

	public static ImageID[] GenerateImages(int count)
	{
		UIntPtr num = new UIntPtr((uint)count);
		uint[] array = new uint[count];
		ilGenImages(num, array);
		ImageID[] array2 = new ImageID[count];
		for (int i = 0; i < count; i++)
		{
			ref ImageID reference = ref array2[i];
			reference = new ImageID((int)array[i]);
		}
		return array2;
	}

	public static bool GetBoolean(ILBooleanMode mode)
	{
		if (ilGetInteger((uint)mode) != 0)
		{
			return true;
		}
		return false;
	}

	public static int GetInteger(ILIntegerMode mode)
	{
		return ilGetInteger((uint)mode);
	}

	public unsafe static byte[] GetDxtcData(CompressedDataFormat dxtcFormat)
	{
		uint num = ilGetDXTCData(IntPtr.Zero, 0u, (uint)dxtcFormat);
		if (num == 0)
		{
			return null;
		}
		byte[] array = new byte[num];
		fixed (byte* value = array)
		{
			ilGetDXTCData(new IntPtr(value), num, (uint)dxtcFormat);
		}
		return array;
	}

	public static ErrorType GetError()
	{
		return (ErrorType)ilGetError();
	}

	public static byte[] GetImageData()
	{
		IntPtr intPtr = ilGetData();
		if (intPtr == IntPtr.Zero)
		{
			return null;
		}
		int numBytes = ilGetInteger(3559u);
		return MemoryHelper.ReadByteBuffer(intPtr, numBytes);
	}

	public static IntPtr GetData()
	{
		return ilGetData();
	}

	public static byte[] GetPaletteData()
	{
		PaletteType palette = (PaletteType)ilGetInteger(3564u);
		int num = ilGetInteger(3567u);
		int paletteComponentCount = MemoryHelper.GetPaletteComponentCount(palette);
		int numBytes = paletteComponentCount * num;
		IntPtr pointer = ilGetPalette();
		return MemoryHelper.ReadByteBuffer(pointer, numBytes);
	}

	public static DataFormat GetDataFormat()
	{
		return (DataFormat)ilGetInteger(1553u);
	}

	public static CompressedDataFormat GetDxtcFormat()
	{
		return (CompressedDataFormat)ilGetInteger(1797u);
	}

	public static DataType GetDataType()
	{
		return (DataType)ilGetInteger(1555u);
	}

	public static JpgSaveFormat GetJpgSaveFormat()
	{
		return (JpgSaveFormat)ilGetInteger(1825u);
	}

	public static OriginLocation GetOriginLocation()
	{
		return (OriginLocation)ilGetInteger(1539u);
	}

	public static string GetString(ILStringMode mode)
	{
		IntPtr intPtr = ilGetString((uint)mode);
		if (intPtr != IntPtr.Zero)
		{
			return Marshal.PtrToStringAnsi(intPtr);
		}
		return string.Empty;
	}

	public static ImageInfo GetImageInfo()
	{
		ImageInfo result = default(ImageInfo);
		result.Format = (DataFormat)ilGetInteger(3562u);
		result.DxtcFormat = (CompressedDataFormat)ilGetInteger(1805u);
		result.DataType = (DataType)ilGetInteger(3563u);
		result.PaletteType = (PaletteType)ilGetInteger(3564u);
		result.PaletteBaseType = (DataFormat)ilGetInteger(3568u);
		result.CubeFlags = (CubeMapFace)ilGetInteger(3581u);
		result.Origin = (OriginLocation)ilGetInteger(3582u);
		result.Width = ilGetInteger(3556u);
		result.Height = ilGetInteger(3557u);
		result.Depth = ilGetInteger(3558u);
		result.BitsPerPixel = ilGetInteger(3561u);
		result.BytesPerPixel = ilGetInteger(3560u);
		result.Channels = ilGetInteger(3583u);
		result.Duration = ilGetInteger(3576u);
		result.SizeOfData = ilGetInteger(3559u);
		result.OffsetX = ilGetInteger(3579u);
		result.OffsetY = ilGetInteger(3580u);
		result.PlaneSize = ilGetInteger(3577u);
		result.FaceCount = ilGetInteger(3553u) + 1;
		result.ImageCount = ilGetInteger(3569u) + 1;
		result.LayerCount = ilGetInteger(3571u) + 1;
		result.MipMapCount = ilGetInteger(3570u) + 1;
		result.PaletteBytesPerPixel = ilGetInteger(3566u);
		result.PaletteColumnCount = ilGetInteger(3567u);
		return result;
	}

	public static Quantization GetQuantization()
	{
		return (Quantization)ilGetInteger(1600u);
	}

	[DllImport("DevIL.dll", CallingConvention = CallingConvention.StdCall, EntryPoint = "ilInvertSurfaceDxtcDataAlpha")]
	[return: MarshalAs(UnmanagedType.U1)]
	public static extern bool InvertSurfaceDxtcDataAlpha();

	public static void Initialize()
	{
		if (!_init)
		{
			ilInit();
			_init = true;
		}
	}

	public static bool IsDisabled(ILEnable mode)
	{
		return ilIsDisabled((uint)mode);
	}

	public static bool IsEnabled(ILEnable mode)
	{
		return ilIsEnabled((uint)mode);
	}

	public static bool ImageToDxtcData(CompressedDataFormat format)
	{
		return ilImageToDxtcData((uint)format);
	}

	public static bool IsImage(ImageID imageID)
	{
		if (imageID.ID < 0)
		{
			return false;
		}
		return ilIsImage((uint)imageID.ID);
	}

	public static bool IsValid(ImageType imageType, string filename)
	{
		if (imageType == ImageType.Unknown || string.IsNullOrEmpty(filename))
		{
			return false;
		}
		return ilIsValid((uint)imageType, filename);
	}

	public unsafe static bool IsValid(ImageType imageType, byte[] data)
	{
		if (imageType == ImageType.Unknown || data == null || data.Length == 0)
		{
			return false;
		}
		fixed (byte* value = data)
		{
			return ilIsValidL((uint)imageType, new IntPtr(value), (uint)data.Length);
		}
	}

	[DllImport("DevIL.dll", CallingConvention = CallingConvention.StdCall, EntryPoint = "ilLoadImage")]
	[return: MarshalAs(UnmanagedType.U1)]
	public static extern bool LoadImage([In][MarshalAs(UnmanagedType.LPStr)] string FileName);

	public static bool LoadImage(ImageType imageType, string filename)
	{
		return ilLoad((uint)imageType, filename);
	}

	public unsafe static bool LoadImageFromStream(ImageType imageType, Stream stream)
	{
		if (imageType == ImageType.Unknown || stream == null || !stream.CanRead)
		{
			return false;
		}
		byte[] array = MemoryHelper.ReadStreamFully(stream, 0);
		uint size = (uint)array.Length;
		bool flag = false;
		fixed (byte* value = array)
		{
			flag = ilLoadL((uint)imageType, new IntPtr(value), size);
		}
		return flag;
	}

	public unsafe static bool LoadImageFromStream(Stream stream)
	{
		if (stream == null || !stream.CanRead)
		{
			return false;
		}
		byte[] array = MemoryHelper.ReadStreamFully(stream, 0);
		uint size = (uint)array.Length;
		bool flag = false;
		ImageType type = DetermineImageType(array);
		fixed (byte* value = array)
		{
			flag = ilLoadL((uint)type, new IntPtr(value), size);
		}
		return flag;
	}

	[DllImport("DevIL.dll", CallingConvention = CallingConvention.StdCall, EntryPoint = "ilLoadPal")]
	[return: MarshalAs(UnmanagedType.U1)]
	public static extern bool LoadPalette([In][MarshalAs(UnmanagedType.LPStr)] string fileName);

	public static bool LoadRawData(string filename, int width, int height, int depth, int componentCount)
	{
		if (string.IsNullOrEmpty(filename) || width < 1 || height < 1 || depth < 1)
		{
			return false;
		}
		if (componentCount != 1 || componentCount != 3 || componentCount != 4)
		{
			return false;
		}
		return ilLoadData(filename, (uint)width, (uint)height, (uint)depth, (byte)componentCount);
	}

	public unsafe static bool LoadRawData(byte[] data, int width, int height, int depth, int componentCount)
	{
		if (width < 1 || height < 1 || depth < 1)
		{
			return false;
		}
		if (componentCount != 1 || componentCount != 3 || componentCount != 4)
		{
			return false;
		}
		uint size = (uint)data.Length;
		fixed (byte* value = data)
		{
			return ilLoadDataL(new IntPtr(value), size, (uint)width, (uint)height, (uint)depth, (byte)componentCount);
		}
	}

	[DllImport("DevIL.dll", CallingConvention = CallingConvention.StdCall, EntryPoint = "ilModAlpha")]
	public static extern void ModulateAlpha(double alphaValue);

	public static bool OverlayImage(ImageID srcImageID, int destX, int destY, int destZ)
	{
		if (srcImageID.ID < 0)
		{
			return false;
		}
		return ilOverlayImage((uint)srcImageID.ID, destX, destY, destZ);
	}

	[DllImport("DevIL.dll", CallingConvention = CallingConvention.StdCall, EntryPoint = "ilPopAttrib")]
	public static extern void PopAttribute();

	public static void PushAttribute(AttributeBits bits)
	{
		ilPushAttrib((uint)bits);
	}

	[DllImport("DevIL.dll", CallingConvention = CallingConvention.StdCall, EntryPoint = "ilSaveImage")]
	public static extern bool SaveImage([In][MarshalAs(UnmanagedType.LPStr)] string fileName);

	public static bool SaveImage(ImageType type, string filename)
	{
		return ilSave((uint)type, filename);
	}

	public unsafe static bool SaveImageToStream(ImageType imageType, Stream stream)
	{
		if (imageType == ImageType.Unknown || stream == null || !stream.CanWrite)
		{
			return false;
		}
		uint num = ilSaveL((uint)imageType, IntPtr.Zero, 0u);
		if (num == 0)
		{
			return false;
		}
		byte[] array = new byte[num];
		fixed (byte* value = array)
		{
			if (ilSaveL((uint)imageType, new IntPtr(value), num) == 0)
			{
				return false;
			}
		}
		stream.Write(array, 0, array.Length);
		return true;
	}

	[DllImport("DevIL.dll", CallingConvention = CallingConvention.StdCall, EntryPoint = "ilSaveData")]
	[return: MarshalAs(UnmanagedType.U1)]
	public static extern bool SaveRawData([In][MarshalAs(UnmanagedType.LPStr)] string FileName);

	[DllImport("DevIL.dll", CallingConvention = CallingConvention.StdCall, EntryPoint = "ilSavePal")]
	[return: MarshalAs(UnmanagedType.U1)]
	public static extern bool SavePalette([In][MarshalAs(UnmanagedType.LPStr)] string FileName);

	[DllImport("DevIL.dll", CallingConvention = CallingConvention.StdCall, EntryPoint = "ilSetAlpha")]
	[return: MarshalAs(UnmanagedType.U1)]
	public static extern bool SetAlpha(double alphaValue);

	public static void SetBoolean(ILBooleanMode mode, bool value)
	{
		ilSetInteger((uint)mode, value ? 1 : 0);
	}

	public static bool SetCompressionAlgorithm(CompressionAlgorithm compressAlgorithm)
	{
		return ilCompressFunc((uint)compressAlgorithm);
	}

	public static bool SetDataFormat(DataFormat dataFormat)
	{
		return ilFormatFunc((uint)dataFormat);
	}

	public unsafe static bool SetImageData(byte[] data)
	{
		fixed (byte* value = data)
		{
			return ilSetData(new IntPtr(value));
		}
	}

	public static bool SetDuration(int duration)
	{
		if (duration < 0)
		{
			return false;
		}
		return ilSetDuration((uint)duration);
	}

	public static void SetDxtcFormat(CompressedDataFormat format)
	{
		ilSetInteger(1797u, (int)format);
	}

	public static bool SetDataType(DataType dataType)
	{
		return ilTypeFunc((uint)dataType);
	}

	[DllImport("DevIL.dll", CallingConvention = CallingConvention.StdCall, EntryPoint = "ilKeyColour")]
	public static extern void SetKeyColor(float red, float green, float blue, float alpha);

	public static void SetKeyColor(Color color)
	{
		SetKeyColor(color.R, color.G, color.B, color.A);
	}

	public static void SetMemoryHint(MemoryHint hint)
	{
		ilHint(1637u, (uint)hint);
	}

	public static void SetCompressionHint(CompressionHint hint)
	{
		ilHint(1640u, (uint)hint);
	}

	public static void SetJpgSaveFormat(JpgSaveFormat format)
	{
		ilSetInteger(1825u, (int)format);
	}

	public static void SetInteger(ILIntegerMode mode, int value)
	{
		ilSetInteger((uint)mode, value);
	}

	public static void SetOriginLocation(OriginLocation origin)
	{
		ilOriginFunc((uint)origin);
	}

	public static void SetString(ILStringMode mode, string value)
	{
		if (value == null)
		{
			value = string.Empty;
		}
		ilSetString((uint)mode, value);
	}

	public static void SetQuantization(Quantization mode)
	{
		ilSetInteger(1600u, (int)mode);
	}

	public unsafe static bool SetPixels(int xOffset, int yOffset, int zOffset, int width, int height, int depth, DataFormat format, DataType dataType, byte[] data)
	{
		if (data == null || data.Length == 0)
		{
			return false;
		}
		if (xOffset < 0 || yOffset < 0 || zOffset < 0 || width < 1 || height < 1 || depth < 1)
		{
			return false;
		}
		fixed (byte* value = data)
		{
			ilSetPixels(xOffset, yOffset, zOffset, (uint)width, (uint)height, (uint)depth, (uint)format, (uint)dataType, new IntPtr(value));
		}
		return true;
	}

	public static void Shutdown()
	{
		if (_init)
		{
			ilShutDown();
			_init = false;
		}
	}

	public static bool SurfaceToDxtcData(CompressedDataFormat format)
	{
		return ilSurfaceToDxtcData((uint)format);
	}

	public unsafe static bool SetTexImage(int width, int height, int depth, DataFormat format, DataType dataType, byte[] data)
	{
		if (data == null || data.Length == 0)
		{
			return false;
		}
		byte bpp = (byte)MemoryHelper.GetFormatComponentCount(format);
		fixed (byte* value = data)
		{
			return ilTexImage((uint)width, (uint)height, (uint)depth, bpp, (uint)format, (uint)dataType, new IntPtr(value));
		}
	}

	public unsafe static bool SetTexImageDxtc(int width, int height, int depth, CompressedDataFormat format, byte[] data)
	{
		if (data == null || data.Length == 0)
		{
			return false;
		}
		fixed (byte* value = data)
		{
			return ilTexImageDxtc(width, height, depth, (uint)format, new IntPtr(value));
		}
	}

	public static string GetVendorName()
	{
		IntPtr intPtr = ilGetString(7936u);
		if (intPtr != IntPtr.Zero)
		{
			return Marshal.PtrToStringAnsi(intPtr);
		}
		return "DevIL";
	}

	public static string GetVersion()
	{
		IntPtr intPtr = ilGetString(3554u);
		if (intPtr != IntPtr.Zero)
		{
			return Marshal.PtrToStringAnsi(intPtr);
		}
		return "Unknown Version";
	}

	public static string[] GetImportExtensions()
	{
		IntPtr intPtr = ilGetString(7937u);
		if (intPtr != IntPtr.Zero)
		{
			string text = Marshal.PtrToStringAnsi(intPtr);
			string[] array = text.Split(new char[1] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
			for (int i = 0; i < array.Length; i++)
			{
				string text2 = array[i];
				if (text2.Equals("dcmdds"))
				{
					text2 = text2.Substring(0, "dcm".Length);
				}
				array[i] = "." + text2;
			}
			return array;
		}
		return new string[0];
	}

	public static string[] GetExportExtensions()
	{
		IntPtr intPtr = ilGetString(7938u);
		if (intPtr != IntPtr.Zero)
		{
			string text = Marshal.PtrToStringAnsi(intPtr);
			string[] array = text.Split(new char[1] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
			for (int i = 0; i < array.Length; i++)
			{
				array[i] = "." + array[i];
			}
			return array;
		}
		return new string[0];
	}

	[DllImport("DevIL.dll", CallingConvention = CallingConvention.StdCall)]
	[return: MarshalAs(UnmanagedType.U1)]
	private static extern bool ilActiveFace(uint Number);

	[DllImport("DevIL.dll", CallingConvention = CallingConvention.StdCall)]
	[return: MarshalAs(UnmanagedType.U1)]
	private static extern bool ilActiveImage(uint Number);

	[DllImport("DevIL.dll", CallingConvention = CallingConvention.StdCall)]
	[return: MarshalAs(UnmanagedType.U1)]
	private static extern bool ilActiveLayer(uint Number);

	[DllImport("DevIL.dll", CallingConvention = CallingConvention.StdCall)]
	[return: MarshalAs(UnmanagedType.U1)]
	private static extern bool ilActiveMipmap(uint Number);

	[DllImport("DevIL.dll", CallingConvention = CallingConvention.StdCall)]
	[return: MarshalAs(UnmanagedType.U1)]
	private static extern bool ilApplyProfile(IntPtr InProfile, IntPtr OutProfile);

	[DllImport("DevIL.dll", CallingConvention = CallingConvention.StdCall)]
	private static extern void ilBindImage(uint Image);

	[DllImport("DevIL.dll", CallingConvention = CallingConvention.StdCall)]
	[return: MarshalAs(UnmanagedType.U1)]
	private static extern bool ilBlit(uint Source, int DestX, int DestY, int DestZ, uint SrcX, uint SrcY, uint SrcZ, uint Width, uint Height, uint Depth);

	[DllImport("DevIL.dll", CallingConvention = CallingConvention.StdCall)]
	private static extern uint ilCloneCurImage();

	[DllImport("DevIL.dll", CallingConvention = CallingConvention.StdCall)]
	private static extern IntPtr ilCompressDXT(IntPtr Data, uint Width, uint Height, uint Depth, uint DXTCFormat, ref uint DXTCSize);

	[DllImport("DevIL.dll", CallingConvention = CallingConvention.StdCall)]
	[return: MarshalAs(UnmanagedType.U1)]
	private static extern bool ilCompressFunc(uint Mode);

	[DllImport("DevIL.dll", CallingConvention = CallingConvention.StdCall)]
	[return: MarshalAs(UnmanagedType.U1)]
	private static extern bool ilConvertImage(uint DestFormat, uint DestType);

	[DllImport("DevIL.dll", CallingConvention = CallingConvention.StdCall)]
	[return: MarshalAs(UnmanagedType.U1)]
	private static extern bool ilConvertPal(uint DestFormat);

	[DllImport("DevIL.dll", CallingConvention = CallingConvention.StdCall)]
	[return: MarshalAs(UnmanagedType.U1)]
	private static extern bool ilCopyImage(uint Src);

	[DllImport("DevIL.dll", CallingConvention = CallingConvention.StdCall)]
	private static extern uint ilCopyPixels(uint XOff, uint YOff, uint ZOff, uint Width, uint Height, uint Depth, uint Format, uint Type, IntPtr Data);

	[DllImport("DevIL.dll", CallingConvention = CallingConvention.StdCall)]
	private static extern uint ilCreateSubImage(uint Type, uint Num);

	[DllImport("DevIL.dll", CallingConvention = CallingConvention.StdCall)]
	private static extern void ilDeleteImage(uint Num);

	[DllImport("DevIL.dll", CallingConvention = CallingConvention.StdCall)]
	private static extern void ilDeleteImages(UIntPtr Num, uint[] Images);

	[DllImport("DevIL.dll", CallingConvention = CallingConvention.StdCall)]
	private static extern uint ilDetermineType([In][MarshalAs(UnmanagedType.LPStr)] string FileName);

	[DllImport("DevIL.dll", CallingConvention = CallingConvention.StdCall)]
	private static extern uint ilDetermineTypeL(IntPtr Lump, uint Size);

	[DllImport("DevIL.dll", CallingConvention = CallingConvention.StdCall)]
	[return: MarshalAs(UnmanagedType.U1)]
	private static extern bool ilDisable(uint Mode);

	[DllImport("DevIL.dll", CallingConvention = CallingConvention.StdCall)]
	[return: MarshalAs(UnmanagedType.U1)]
	private static extern bool ilEnable(uint Mode);

	[DllImport("DevIL.dll", CallingConvention = CallingConvention.StdCall)]
	[return: MarshalAs(UnmanagedType.U1)]
	private static extern bool ilFormatFunc(uint Mode);

	[DllImport("DevIL.dll", CallingConvention = CallingConvention.StdCall)]
	private static extern void ilGenImages(UIntPtr Num, uint[] Images);

	[DllImport("DevIL.dll", CallingConvention = CallingConvention.StdCall)]
	private static extern uint ilGenImage();

	[DllImport("DevIL.dll", CallingConvention = CallingConvention.StdCall)]
	private static extern IntPtr ilGetAlpha(uint Type);

	[DllImport("DevIL.dll", CallingConvention = CallingConvention.StdCall)]
	private static extern IntPtr ilGetData();

	[DllImport("DevIL.dll", CallingConvention = CallingConvention.StdCall)]
	private static extern uint ilGetDXTCData(IntPtr Buffer, uint BufferSize, uint DXTCFormat);

	[DllImport("DevIL.dll", CallingConvention = CallingConvention.StdCall)]
	private static extern uint ilGetError();

	[DllImport("DevIL.dll", CallingConvention = CallingConvention.StdCall)]
	internal static extern int ilGetInteger(uint Mode);

	[DllImport("DevIL.dll", CallingConvention = CallingConvention.StdCall)]
	private static extern IntPtr ilGetPalette();

	[DllImport("DevIL.dll", CallingConvention = CallingConvention.StdCall)]
	private static extern IntPtr ilGetString(uint StringName);

	[DllImport("DevIL.dll", CallingConvention = CallingConvention.StdCall)]
	private static extern void ilHint(uint Target, uint Mode);

	[DllImport("DevIL.dll", CallingConvention = CallingConvention.StdCall)]
	private static extern void ilInit();

	[DllImport("DevIL.dll", CallingConvention = CallingConvention.StdCall)]
	[return: MarshalAs(UnmanagedType.U1)]
	private static extern bool ilImageToDxtcData(uint Format);

	[DllImport("DevIL.dll", CallingConvention = CallingConvention.StdCall)]
	[return: MarshalAs(UnmanagedType.U1)]
	private static extern bool ilIsDisabled(uint Mode);

	[DllImport("DevIL.dll", CallingConvention = CallingConvention.StdCall)]
	[return: MarshalAs(UnmanagedType.U1)]
	private static extern bool ilIsEnabled(uint Mode);

	[DllImport("DevIL.dll", CallingConvention = CallingConvention.StdCall)]
	[return: MarshalAs(UnmanagedType.U1)]
	private static extern bool ilIsImage(uint Image);

	[DllImport("DevIL.dll", CallingConvention = CallingConvention.StdCall)]
	[return: MarshalAs(UnmanagedType.U1)]
	private static extern bool ilIsValid(uint Type, [In][MarshalAs(UnmanagedType.LPStr)] string FileName);

	[DllImport("DevIL.dll", CallingConvention = CallingConvention.StdCall)]
	[return: MarshalAs(UnmanagedType.U1)]
	private static extern bool ilIsValidL(uint Type, IntPtr Lump, uint Size);

	[DllImport("DevIL.dll", CallingConvention = CallingConvention.StdCall)]
	[return: MarshalAs(UnmanagedType.U1)]
	private static extern bool ilLoad(uint Type, [In][MarshalAs(UnmanagedType.LPStr)] string FileName);

	[DllImport("DevIL.dll", CallingConvention = CallingConvention.StdCall)]
	[return: MarshalAs(UnmanagedType.U1)]
	private static extern bool ilLoadL(uint Type, IntPtr Lump, uint Size);

	[DllImport("DevIL.dll", CallingConvention = CallingConvention.StdCall)]
	[return: MarshalAs(UnmanagedType.U1)]
	private static extern bool ilOriginFunc(uint Mode);

	[DllImport("DevIL.dll", CallingConvention = CallingConvention.StdCall)]
	[return: MarshalAs(UnmanagedType.U1)]
	private static extern bool ilOverlayImage(uint Source, int XCoord, int YCoord, int ZCoord);

	[DllImport("DevIL.dll", CallingConvention = CallingConvention.StdCall)]
	private static extern void ilPushAttrib(uint Bits);

	[DllImport("DevIL.dll", CallingConvention = CallingConvention.StdCall)]
	[return: MarshalAs(UnmanagedType.U1)]
	private static extern bool ilSave(uint Type, [In][MarshalAs(UnmanagedType.LPStr)] string FileName);

	[DllImport("DevIL.dll", CallingConvention = CallingConvention.StdCall)]
	[return: MarshalAs(UnmanagedType.U1)]
	private static extern bool ilSaveImage([In][MarshalAs(UnmanagedType.LPStr)] string FileName);

	[DllImport("DevIL.dll", CallingConvention = CallingConvention.StdCall)]
	private static extern uint ilSaveL(uint Type, IntPtr Lump, uint Size);

	[DllImport("DevIL.dll", CallingConvention = CallingConvention.StdCall)]
	[return: MarshalAs(UnmanagedType.U1)]
	private static extern bool ilSetData(IntPtr Data);

	[DllImport("DevIL.dll", CallingConvention = CallingConvention.StdCall)]
	[return: MarshalAs(UnmanagedType.U1)]
	private static extern bool ilSetDuration(uint Duration);

	[DllImport("DevIL.dll", CallingConvention = CallingConvention.StdCall)]
	private static extern void ilSetInteger(uint Mode, int Param);

	[DllImport("DevIL.dll", CallingConvention = CallingConvention.StdCall)]
	private static extern void ilSetPixels(int XOff, int YOff, int ZOff, uint Width, uint Height, uint Depth, uint Format, uint Type, IntPtr Data);

	[DllImport("DevIL.dll", CallingConvention = CallingConvention.StdCall)]
	private static extern void ilSetString(uint Mode, [In][MarshalAs(UnmanagedType.LPStr)] string String);

	[DllImport("DevIL.dll", CallingConvention = CallingConvention.StdCall)]
	private static extern void ilShutDown();

	[DllImport("DevIL.dll", CallingConvention = CallingConvention.StdCall)]
	[return: MarshalAs(UnmanagedType.U1)]
	private static extern bool ilSurfaceToDxtcData(uint Format);

	[DllImport("DevIL.dll", CallingConvention = CallingConvention.StdCall)]
	[return: MarshalAs(UnmanagedType.U1)]
	private static extern bool ilTexImage(uint Width, uint Height, uint Depth, byte Bpp, uint Format, uint Type, IntPtr Data);

	[DllImport("DevIL.dll", CallingConvention = CallingConvention.StdCall)]
	[return: MarshalAs(UnmanagedType.U1)]
	private static extern bool ilTexImageDxtc(int Width, int Height, int Depth, uint DxtFormat, IntPtr Data);

	[DllImport("DevIL.dll", CallingConvention = CallingConvention.StdCall)]
	private static extern uint ilTypeFromExt([In][MarshalAs(UnmanagedType.LPStr)] string FileName);

	[DllImport("DevIL.dll", CallingConvention = CallingConvention.StdCall)]
	[return: MarshalAs(UnmanagedType.U1)]
	private static extern bool ilTypeFunc(uint Mode);

	[DllImport("DevIL.dll", CallingConvention = CallingConvention.StdCall)]
	[return: MarshalAs(UnmanagedType.U1)]
	private static extern bool ilLoadData([In][MarshalAs(UnmanagedType.LPStr)] string FileName, uint Width, uint Height, uint Depth, byte Bpp);

	[DllImport("DevIL.dll", CallingConvention = CallingConvention.StdCall)]
	[return: MarshalAs(UnmanagedType.U1)]
	private static extern bool ilLoadDataL(IntPtr Lump, uint Size, uint Width, uint Height, uint Depth, byte Bpp);
}
