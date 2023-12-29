using DevIL.Unmanaged;

namespace DevIL;

public class ImageData
{
	private ImageInfo m_info;

	private byte[] m_data;

	private byte[] m_compressedData;

	private byte[] m_paletteData;

	public DataFormat Format => m_info.Format;

	public CompressedDataFormat DxtcFormat => m_info.DxtcFormat;

	public DataType DataType => m_info.DataType;

	public PaletteType PaletteType => m_info.PaletteType;

	public DataFormat PaletteBaseType => m_info.PaletteBaseType;

	public CubeMapFace CubeFace => m_info.CubeFlags;

	public OriginLocation Origin => m_info.Origin;

	public int Width => m_info.Width;

	public int Height => m_info.Height;

	public int Depth => m_info.Depth;

	public int BitsPerPixel => m_info.BitsPerPixel;

	public int BytesPerPixel => m_info.BytesPerPixel;

	public int ChannelCount => m_info.Channels;

	public int Duration => m_info.Duration;

	public int SizeOfData => m_info.SizeOfData;

	public int OffsetX => m_info.OffsetX;

	public int OffsetY => m_info.OffsetY;

	public int PlaneSize => m_info.PlaneSize;

	public int PaletteBytesPerPixel => m_info.PaletteBytesPerPixel;

	public int PaletteColumnCount => m_info.PaletteColumnCount;

	public bool HasDXTCData
	{
		get
		{
			if (m_info.HasDXTC)
			{
				return m_compressedData != null;
			}
			return false;
		}
	}

	public bool HasPaletteData
	{
		get
		{
			if (m_info.HasPalette)
			{
				return m_paletteData != null;
			}
			return false;
		}
	}

	public bool IsCubeMap => m_info.IsCubeMap;

	public bool IsSphereMap => m_info.IsSphereMap;

	public byte[] Data => m_data;

	public byte[] CompressedData => m_compressedData;

	public byte[] PaletteData => m_paletteData;

	private ImageData()
	{
	}

	internal static ImageData Load(Subimage subimage)
	{
		if (!subimage.Activate())
		{
			return null;
		}
		ImageData imageData = new ImageData();
		imageData.m_info = IL.GetImageInfo();
		imageData.m_data = IL.GetImageData();
		if (imageData.m_data == null)
		{
			return null;
		}
		if (imageData.m_info.HasDXTC)
		{
			imageData.m_compressedData = IL.GetDxtcData(imageData.DxtcFormat);
		}
		if (imageData.m_info.HasPalette)
		{
			imageData.m_paletteData = IL.GetPaletteData();
		}
		return imageData;
	}
}
