using DevIL.Unmanaged;

namespace DevIL;

public sealed class CompressionState : IImageState
{
	private CompressionAlgorithm m_compression = CompressionAlgorithm.ZLib;

	private CompressedDataFormat m_dxtcFormat = CompressedDataFormat.DXT1;

	private CompressedDataFormat m_vtfCompression = CompressedDataFormat.None;

	private bool m_keepDxtcData;

	public CompressionAlgorithm Compression
	{
		get
		{
			return m_compression;
		}
		set
		{
			m_compression = value;
		}
	}

	public CompressedDataFormat DxtcFormat
	{
		get
		{
			return m_dxtcFormat;
		}
		set
		{
			m_dxtcFormat = value;
		}
	}

	public CompressedDataFormat VtfCompression
	{
		get
		{
			return m_vtfCompression;
		}
		set
		{
			m_vtfCompression = value;
		}
	}

	public bool KeepDxtcData
	{
		get
		{
			return m_keepDxtcData;
		}
		set
		{
			m_keepDxtcData = value;
		}
	}

	public void Apply()
	{
		if (IL.IsInitialized)
		{
			IL.SetCompressionAlgorithm(m_compression);
			IL.SetDxtcFormat(m_dxtcFormat);
			IL.SetInteger(ILIntegerMode.VTFCompression, (int)m_vtfCompression);
			IL.SetBoolean(ILBooleanMode.KeepDxtcData, m_keepDxtcData);
		}
	}
}
