using System;
using DevIL.Unmanaged;

namespace DevIL;

public sealed class Image : IDisposable, IEquatable<Image>
{
	private bool m_isDisposed;

	private ImageID m_id;

	private static Image s_default = new Image(new ImageID(0));

	internal ImageID ImageID => m_id;

	internal bool IsValid
	{
		get
		{
			if ((int)m_id >= 0)
			{
				return IL.IsInitialized;
			}
			return false;
		}
	}

	public static Image DefaultImage => s_default;

	public DataFormat Format
	{
		get
		{
			if (!IsValid)
			{
				return DataFormat.RGBA;
			}
			Bind();
			return (DataFormat)IL.ilGetInteger(3562u);
		}
	}

	public CompressedDataFormat DxtcFormat
	{
		get
		{
			if (!IsValid)
			{
				return CompressedDataFormat.None;
			}
			Bind();
			return (CompressedDataFormat)IL.ilGetInteger(1805u);
		}
	}

	public DataType DataType
	{
		get
		{
			if (!IsValid)
			{
				return DataType.UnsignedByte;
			}
			Bind();
			return (DataType)IL.ilGetInteger(3563u);
		}
	}

	public PaletteType PaletteType
	{
		get
		{
			if (!IsValid)
			{
				return PaletteType.None;
			}
			Bind();
			return (PaletteType)IL.ilGetInteger(3564u);
		}
	}

	public DataFormat PaletteBaseType
	{
		get
		{
			if (!IsValid)
			{
				return DataFormat.RGBA;
			}
			Bind();
			return (DataFormat)IL.ilGetInteger(3568u);
		}
	}

	public OriginLocation Origin
	{
		get
		{
			if (!IsValid)
			{
				return OriginLocation.UpperLeft;
			}
			Bind();
			return (OriginLocation)IL.ilGetInteger(3582u);
		}
	}

	public int Width
	{
		get
		{
			if (!IsValid)
			{
				return 0;
			}
			Bind();
			return IL.GetInteger(ILIntegerMode.ImageWidth);
		}
	}

	public int Height
	{
		get
		{
			if (!IsValid)
			{
				return 0;
			}
			Bind();
			return IL.GetInteger(ILIntegerMode.ImageHeight);
		}
	}

	public int Depth
	{
		get
		{
			if (!IsValid)
			{
				return 0;
			}
			Bind();
			return IL.GetInteger(ILIntegerMode.ImageDepth);
		}
	}

	public int BytesPerPixel
	{
		get
		{
			if (!IsValid)
			{
				return 0;
			}
			Bind();
			return IL.GetInteger(ILIntegerMode.ImageBytesPerPixel);
		}
	}

	public int BitsPerPixel
	{
		get
		{
			if (!IsValid)
			{
				return 0;
			}
			Bind();
			return IL.GetInteger(ILIntegerMode.ImageBitsPerPixel);
		}
	}

	public int ChannelCount
	{
		get
		{
			if (!IsValid)
			{
				return 0;
			}
			Bind();
			return IL.GetInteger(ILIntegerMode.ImageChannels);
		}
	}

	public int PaletteBytesPerPixel
	{
		get
		{
			if (!IsValid)
			{
				return 0;
			}
			Bind();
			return IL.GetInteger(ILIntegerMode.ImagePaletteBytesPerPixel);
		}
	}

	public int PaletteColumnCount
	{
		get
		{
			if (!IsValid)
			{
				return 0;
			}
			Bind();
			return IL.GetInteger(ILIntegerMode.ImagePaletteColumnCount);
		}
	}

	public int FaceCount
	{
		get
		{
			if (!IsValid)
			{
				return 0;
			}
			Bind();
			return IL.GetInteger(ILIntegerMode.ImageFaceCount) + 1;
		}
	}

	public int ImageArrayCount
	{
		get
		{
			if (!IsValid)
			{
				return 0;
			}
			Bind();
			return IL.GetInteger(ILIntegerMode.ImageArrayCount) + 1;
		}
	}

	public int MipMapCount
	{
		get
		{
			if (!IsValid)
			{
				return 0;
			}
			Bind();
			return IL.GetInteger(ILIntegerMode.ImageMipMapCount) + 1;
		}
	}

	public int LayerCount
	{
		get
		{
			if (!IsValid)
			{
				return 0;
			}
			Bind();
			return IL.GetInteger(ILIntegerMode.ImageLayerCount) + 1;
		}
	}

	public bool HasDXTCData
	{
		get
		{
			if (!IsValid)
			{
				return false;
			}
			return DxtcFormat != CompressedDataFormat.None;
		}
	}

	public bool HasPaletteData
	{
		get
		{
			if (!IsValid)
			{
				return false;
			}
			return PaletteType != PaletteType.None;
		}
	}

	public bool IsCubeMap
	{
		get
		{
			if (!IsValid)
			{
				return false;
			}
			CubeMapFace cubeMapFace = (CubeMapFace)IL.ilGetInteger(3581u);
			if (cubeMapFace != 0)
			{
				return cubeMapFace != CubeMapFace.SphereMap;
			}
			return false;
		}
	}

	public bool IsSphereMap
	{
		get
		{
			if (!IsValid)
			{
				return false;
			}
			CubeMapFace cubeMapFace = (CubeMapFace)IL.ilGetInteger(3581u);
			return cubeMapFace == CubeMapFace.SphereMap;
		}
	}

	internal Image(ImageID id)
	{
		m_id = id;
	}

	~Image()
	{
		Dispose(disposing: false);
	}

	public void Bind()
	{
		IL.BindImage(m_id);
	}

	public bool ConvertToDxtc(CompressedDataFormat compressedFormat)
	{
		if (!CheckValid(this))
		{
			return false;
		}
		Bind();
		return IL.ImageToDxtcData(compressedFormat);
	}

	public bool CopyFrom(Image srcImage)
	{
		if (!CheckValid(this) || !CheckValid(srcImage))
		{
			return false;
		}
		Bind();
		return IL.CopyImage(srcImage.ImageID);
	}

	public bool CopyTo(Image destImage)
	{
		if (!CheckValid(this) || !CheckValid(destImage))
		{
			return false;
		}
		destImage.Bind();
		return IL.CopyImage(ImageID);
	}

	public Image Clone()
	{
		ImageID imageID = IL.GenerateImage();
		Image result = new Image(imageID);
		IL.BindImage(imageID);
		IL.CopyImage(m_id);
		return result;
	}

	public void Resize(int width, int height, int depth, SamplingFilter filter, bool regenerateMipMaps)
	{
		width = Math.Max(1, width);
		height = Math.Max(1, height);
		depth = Math.Max(1, depth);
		Bind();
		SamplingFilter samplingFilter = ILU.GetSamplingFilter();
		ILU.SetSamplingFilter(filter);
		ILU.Scale(width, height, depth);
		if (regenerateMipMaps)
		{
			Bind();
			ILU.BuildMipMaps();
		}
		ILU.SetSamplingFilter(samplingFilter);
	}

	public void ResizeToNearestPowerOfTwo(SamplingFilter filter, bool regenerateMipMaps)
	{
		int width = Width;
		int height = Height;
		int depth = Depth;
		width = MemoryHelper.RoundToNearestPowerOfTwo(width);
		height = MemoryHelper.RoundToNearestPowerOfTwo(height);
		depth = MemoryHelper.RoundToNearestPowerOfTwo(depth);
		Bind();
		SamplingFilter samplingFilter = ILU.GetSamplingFilter();
		ILU.SetSamplingFilter(filter);
		ILU.Scale(width, height, depth);
		if (regenerateMipMaps)
		{
			Bind();
			ILU.BuildMipMaps();
		}
		ILU.SetSamplingFilter(samplingFilter);
	}

	public ImageInfo GetImageInfo()
	{
		ImageInfo result = default(ImageInfo);
		if (CheckValid(this))
		{
			Bind();
			return IL.GetImageInfo();
		}
		return result;
	}

	public ImageData GetImageData(int imageIndex, int faceIndex, int layerIndex, int mipmapIndex)
	{
		if (!IsValid || imageIndex < 0 || faceIndex < 0 || layerIndex < 0 || mipmapIndex < 0)
		{
			return null;
		}
		Subimage subimage = new Subimage(m_id, imageIndex, faceIndex, layerIndex, mipmapIndex);
		return ImageData.Load(subimage);
	}

	public ImageData GetImageData(CubeMapFace cubeMapFace, int mipmapIndex)
	{
		if (!IsValid || mipmapIndex < 0)
		{
			return null;
		}
		int faceCount = FaceCount;
		for (int i = 0; i < faceCount; i++)
		{
			Bind();
			IL.ActiveFace(i);
			CubeMapFace cubeMapFace2 = (CubeMapFace)IL.ilGetInteger(3581u);
			if (cubeMapFace2 == cubeMapFace)
			{
				return ImageData.Load(new Subimage(m_id, 0, i, 0, mipmapIndex));
			}
		}
		return null;
	}

	public ImageData GetImageData(int mipmapIndex)
	{
		if (!IsValid || mipmapIndex < 0)
		{
			return null;
		}
		Subimage subimage = new Subimage(m_id, 0, 0, 0, mipmapIndex);
		return ImageData.Load(subimage);
	}

	public ManagedImage ToManaged()
	{
		if (IsValid)
		{
			return new ManagedImage(this);
		}
		return null;
	}

	public static bool CheckValid(Image image)
	{
		if (image != null && image.IsValid)
		{
			return true;
		}
		return false;
	}

	public bool Equals(Image other)
	{
		if (other.ImageID == ImageID)
		{
			return true;
		}
		return false;
	}

	public override bool Equals(object obj)
	{
		if (!(obj is Image image))
		{
			return false;
		}
		return image.ImageID == ImageID;
	}

	public override int GetHashCode()
	{
		return m_id.GetHashCode();
	}

	public override string ToString()
	{
		return m_id.ToString();
	}

	public void Dispose()
	{
		Dispose(disposing: true);
		GC.SuppressFinalize(this);
	}

	private void Dispose(bool disposing)
	{
		if (m_isDisposed)
		{
			if (m_id > 0)
			{
				IL.DeleteImage(m_id);
			}
			m_isDisposed = true;
		}
	}
}
