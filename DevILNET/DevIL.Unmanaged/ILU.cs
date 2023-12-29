using System;
using System.Runtime.InteropServices;

namespace DevIL.Unmanaged;

public static class ILU
{
	private const string ILUDLL = "ILU.dll";

	private static bool _init;

	public static bool IsInitialized => _init;

	public static void Initialize()
	{
		if (!_init)
		{
			iluInit();
			_init = true;
		}
	}

	[DllImport("ILU.dll", CallingConvention = CallingConvention.StdCall, EntryPoint = "iluAlienify")]
	[return: MarshalAs(UnmanagedType.U1)]
	public static extern bool Alienify();

	public static bool BlurAverage(int iterations)
	{
		return iluBlurAverage((uint)iterations);
	}

	public static bool BlurGaussian(int iterations)
	{
		return iluBlurGaussian((uint)iterations);
	}

	public static bool CompareImages(int otherImageID)
	{
		return iluCompareImages((uint)otherImageID);
	}

	public static bool Crop(int xOffset, int yOffset, int zOffset, int width, int height, int depth)
	{
		return iluCrop((uint)xOffset, (uint)yOffset, (uint)zOffset, (uint)width, (uint)height, (uint)depth);
	}

	[DllImport("ILU.dll", CallingConvention = CallingConvention.StdCall, EntryPoint = "iluContrast")]
	[return: MarshalAs(UnmanagedType.U1)]
	public static extern bool Contrast(float contrast);

	[DllImport("ILU.dll", CallingConvention = CallingConvention.StdCall, EntryPoint = "iluEdgeDetectE")]
	[return: MarshalAs(UnmanagedType.U1)]
	public static extern bool EdgeDetectE();

	[DllImport("ILU.dll", CallingConvention = CallingConvention.StdCall, EntryPoint = "iluEdgeDetectP")]
	[return: MarshalAs(UnmanagedType.U1)]
	public static extern bool EdgeDetectP();

	[DllImport("ILU.dll", CallingConvention = CallingConvention.StdCall, EntryPoint = "iluEdgeDetectS")]
	[return: MarshalAs(UnmanagedType.U1)]
	public static extern bool EdgeDetectS();

	[DllImport("ILU.dll", CallingConvention = CallingConvention.StdCall, EntryPoint = "iluEmboss")]
	[return: MarshalAs(UnmanagedType.U1)]
	public static extern bool Emboss();

	public static bool EnlargeCanvas(int width, int height, int depth)
	{
		return iluEnlargeCanvas((uint)width, (uint)height, (uint)depth);
	}

	public static bool EnlargeImage(int xDimension, int yDimension, int zDimension)
	{
		return iluEnlargeImage((uint)xDimension, (uint)yDimension, (uint)zDimension);
	}

	[DllImport("ILU.dll", CallingConvention = CallingConvention.StdCall, EntryPoint = "iluEqualize")]
	[return: MarshalAs(UnmanagedType.U1)]
	public static extern bool Equalize();

	public static string GetErrorString(ErrorType error)
	{
		return Marshal.PtrToStringAnsi(iluGetErrorString((uint)error));
	}

	[DllImport("ILU.dll", CallingConvention = CallingConvention.StdCall, EntryPoint = "iluConvolution")]
	[return: MarshalAs(UnmanagedType.U1)]
	public static extern bool Convolution(int[] matrix, int scale, int bias);

	[DllImport("ILU.dll", CallingConvention = CallingConvention.StdCall, EntryPoint = "iluFlipImage")]
	[return: MarshalAs(UnmanagedType.U1)]
	public static extern bool FlipImage();

	[DllImport("ILU.dll", CallingConvention = CallingConvention.StdCall, EntryPoint = "iluBuildMipmaps")]
	[return: MarshalAs(UnmanagedType.U1)]
	public static extern bool BuildMipMaps();

	public static int ColorsUsed()
	{
		return (int)iluColorsUsed();
	}

	public static bool Scale(int width, int height, int depth)
	{
		return iluScale((uint)width, (uint)height, (uint)depth);
	}

	[DllImport("ILU.dll", CallingConvention = CallingConvention.StdCall, EntryPoint = "iluGammaCorrect")]
	[return: MarshalAs(UnmanagedType.U1)]
	public static extern bool GammaCorrect(float gamma);

	[DllImport("ILU.dll", CallingConvention = CallingConvention.StdCall, EntryPoint = "iluInvertAlpha")]
	[return: MarshalAs(UnmanagedType.U1)]
	public static extern bool InvertAlpha();

	[DllImport("ILU.dll", CallingConvention = CallingConvention.StdCall, EntryPoint = "iluMirror")]
	[return: MarshalAs(UnmanagedType.U1)]
	public static extern bool Mirror();

	[DllImport("ILU.dll", CallingConvention = CallingConvention.StdCall, EntryPoint = "iluNegative")]
	[return: MarshalAs(UnmanagedType.U1)]
	public static extern bool Negative();

	[DllImport("ILU.dll", CallingConvention = CallingConvention.StdCall)]
	[return: MarshalAs(UnmanagedType.U1)]
	private static extern bool iluNoisify(float tolerance);

	public static bool Noisify(float tolerance)
	{
		return iluNoisify(MemoryHelper.Clamp(tolerance, 0f, 1f));
	}

	public static bool Pixelize(int pixelSize)
	{
		return iluPixelize((uint)pixelSize);
	}

	[DllImport("ILU.dll", CallingConvention = CallingConvention.StdCall, EntryPoint = "iluReplaceColour")]
	[return: MarshalAs(UnmanagedType.U1)]
	public static extern bool ReplaceColor(byte red, byte green, byte blue, float tolerance);

	[DllImport("ILU.dll", CallingConvention = CallingConvention.StdCall, EntryPoint = "iluRotate")]
	[return: MarshalAs(UnmanagedType.U1)]
	public static extern bool Rotate(float angle);

	[DllImport("ILU.dll", CallingConvention = CallingConvention.StdCall, EntryPoint = "iluRotate3D")]
	[return: MarshalAs(UnmanagedType.U1)]
	public static extern bool Rotate3D(float x, float y, float z, float angle);

	[DllImport("ILU.dll", CallingConvention = CallingConvention.StdCall, EntryPoint = "iluSaturate1f")]
	[return: MarshalAs(UnmanagedType.U1)]
	public static extern bool Saturate(float saturation);

	[DllImport("ILU.dll", CallingConvention = CallingConvention.StdCall, EntryPoint = "iluSaturate4f")]
	[return: MarshalAs(UnmanagedType.U1)]
	public static extern bool Saturate(float red, float green, float blue, float saturation);

	[DllImport("ILU.dll", CallingConvention = CallingConvention.StdCall, EntryPoint = "iluScaleAlpha")]
	[return: MarshalAs(UnmanagedType.U1)]
	public static extern bool ScaleAlpha(float scale);

	[DllImport("ILU.dll", CallingConvention = CallingConvention.StdCall, EntryPoint = "iluScaleColours")]
	[return: MarshalAs(UnmanagedType.U1)]
	public static extern bool ScaleColors(float red, float green, float blue);

	[DllImport("ILU.dll", CallingConvention = CallingConvention.StdCall)]
	[return: MarshalAs(UnmanagedType.U1)]
	private static extern bool iluSetLanguage(uint language);

	public static bool SetLanguage(Language lang)
	{
		return iluSetLanguage((uint)lang);
	}

	public static bool Sharpen(float factor, int iterations)
	{
		return iluSharpen(factor, (uint)iterations);
	}

	[DllImport("ILU.dll", CallingConvention = CallingConvention.StdCall, EntryPoint = "iluSwapColours")]
	[return: MarshalAs(UnmanagedType.U1)]
	public static extern bool SwapColors();

	[DllImport("ILU.dll", CallingConvention = CallingConvention.StdCall, EntryPoint = "iluWave")]
	[return: MarshalAs(UnmanagedType.U1)]
	public static extern bool Wave(float angle);

	public static string GetVendorName()
	{
		return Marshal.PtrToStringAnsi(iluGetString(7936u));
	}

	public static string GetVersionNumber()
	{
		return Marshal.PtrToStringAnsi(iluGetString(3554u));
	}

	public static void SetImagePlacement(Placement placement)
	{
		iluImageParameter(1792u, (uint)placement);
	}

	public static Placement GetImagePlacement()
	{
		return (Placement)iluGetInteger(1792u);
	}

	public static void SetSamplingFilter(SamplingFilter filter)
	{
		iluImageParameter(9728u, (uint)filter);
	}

	public static SamplingFilter GetSamplingFilter()
	{
		return (SamplingFilter)iluGetInteger(9728u);
	}

	public static void Region(PointF[] points)
	{
		if (points != null && points.Length >= 3)
		{
			iluRegionf(points, (uint)points.Length);
		}
	}

	public static void Region(PointI[] points)
	{
		if (points != null && points.Length >= 3)
		{
			iluRegioni(points, (uint)points.Length);
		}
	}

	[DllImport("ILU.dll", CallingConvention = CallingConvention.StdCall)]
	private static extern void iluInit();

	[DllImport("ILU.dll", CallingConvention = CallingConvention.StdCall, EntryPoint = "iluBlurAvg")]
	[return: MarshalAs(UnmanagedType.U1)]
	private static extern bool iluBlurAverage(uint iterations);

	[DllImport("ILU.dll", CallingConvention = CallingConvention.StdCall)]
	[return: MarshalAs(UnmanagedType.U1)]
	private static extern bool iluBlurGaussian(uint iterations);

	[DllImport("ILU.dll", CallingConvention = CallingConvention.StdCall, EntryPoint = "iluCompareImage")]
	[return: MarshalAs(UnmanagedType.U1)]
	private static extern bool iluCompareImages(uint otherImage);

	[DllImport("ILU.dll", CallingConvention = CallingConvention.StdCall)]
	[return: MarshalAs(UnmanagedType.U1)]
	private static extern bool iluCrop(uint offsetX, uint offsetY, uint offsetZ, uint width, uint height, uint depth);

	[DllImport("ILU.dll", CallingConvention = CallingConvention.StdCall)]
	[return: MarshalAs(UnmanagedType.U1)]
	private static extern bool iluEnlargeCanvas(uint width, uint height, uint depth);

	[DllImport("ILU.dll", CallingConvention = CallingConvention.StdCall)]
	[return: MarshalAs(UnmanagedType.U1)]
	private static extern bool iluEnlargeImage(uint xDim, uint yDim, uint zDim);

	[DllImport("ILU.dll", CallingConvention = CallingConvention.StdCall, EntryPoint = "iluErrorString")]
	private static extern IntPtr iluGetErrorString(uint error);

	[DllImport("ILU.dll", CallingConvention = CallingConvention.StdCall, EntryPoint = "iluColoursUsed")]
	private static extern uint iluColorsUsed();

	[DllImport("ILU.dll", CallingConvention = CallingConvention.StdCall)]
	[return: MarshalAs(UnmanagedType.U1)]
	private static extern bool iluScale(uint width, uint height, uint depth);

	[DllImport("ILU.dll", CallingConvention = CallingConvention.StdCall)]
	[return: MarshalAs(UnmanagedType.U1)]
	private static extern bool iluPixelize(uint pixelSize);

	[DllImport("ILU.dll", CallingConvention = CallingConvention.StdCall)]
	[return: MarshalAs(UnmanagedType.U1)]
	private static extern bool iluSharpen(float factor, uint iterations);

	[DllImport("ILU.dll", CallingConvention = CallingConvention.StdCall)]
	private static extern IntPtr iluGetString(uint name);

	[DllImport("ILU.dll", CallingConvention = CallingConvention.StdCall)]
	private static extern void iluImageParameter(uint pName, uint param);

	[DllImport("ILU.dll", CallingConvention = CallingConvention.StdCall)]
	private static extern int iluGetInteger(uint mode);

	[DllImport("ILU.dll", CallingConvention = CallingConvention.StdCall, EntryPoint = "iluRegionfv")]
	private static extern void iluRegionf(PointF[] points, uint num);

	[DllImport("ILU.dll", CallingConvention = CallingConvention.StdCall, EntryPoint = "iluRegioniv")]
	private static extern void iluRegioni(PointI[] points, uint num);
}
