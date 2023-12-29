namespace DevIL.Unmanaged;

public struct ImageInfo
{
	public DataFormat Format;

	public CompressedDataFormat DxtcFormat;

	public DataType DataType;

	public PaletteType PaletteType;

	public DataFormat PaletteBaseType;

	public CubeMapFace CubeFlags;

	public OriginLocation Origin;

	public int Width;

	public int Height;

	public int Depth;

	public int BitsPerPixel;

	public int BytesPerPixel;

	public int Channels;

	public int Duration;

	public int SizeOfData;

	public int OffsetX;

	public int OffsetY;

	public int PlaneSize;

	public int FaceCount;

	public int ImageCount;

	public int LayerCount;

	public int MipMapCount;

	public int PaletteBytesPerPixel;

	public int PaletteColumnCount;

	public bool HasDXTC => DxtcFormat != CompressedDataFormat.None;

	public bool HasPalette => PaletteType != PaletteType.None;

	public bool IsCubeMap
	{
		get
		{
			if (CubeFlags != 0)
			{
				return CubeFlags != CubeMapFace.SphereMap;
			}
			return false;
		}
	}

	public bool IsSphereMap => CubeFlags == CubeMapFace.SphereMap;
}
