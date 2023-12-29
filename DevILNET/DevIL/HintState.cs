using DevIL.Unmanaged;

namespace DevIL;

public sealed class HintState : IImageState
{
	private CompressionHint m_compressHint = CompressionHint.UseCompression;

	private MemoryHint m_memHint = MemoryHint.Fastest;

	public CompressionHint CompressionHint
	{
		get
		{
			return m_compressHint;
		}
		set
		{
			m_compressHint = value;
		}
	}

	public MemoryHint MemoryHint
	{
		get
		{
			return m_memHint;
		}
		set
		{
			m_memHint = value;
		}
	}

	public void Apply()
	{
		if (IL.IsInitialized)
		{
			IL.SetCompressionHint(m_compressHint);
			IL.SetMemoryHint(m_memHint);
		}
	}
}
