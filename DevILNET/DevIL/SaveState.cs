using DevIL.Unmanaged;

namespace DevIL;

public class SaveState : IImageState
{
	private bool m_saveInterlaced;

	private bool m_overwriteExistingFile;

	private CompressionLibrary m_compressionLibrary;

	public bool SaveInterlaced
	{
		get
		{
			return m_saveInterlaced;
		}
		set
		{
			m_saveInterlaced = value;
		}
	}

	public bool OverwriteExistingFile
	{
		get
		{
			return m_overwriteExistingFile;
		}
		set
		{
			m_overwriteExistingFile = value;
		}
	}

	public CompressionLibrary CompressionLibrary
	{
		get
		{
			return m_compressionLibrary;
		}
		set
		{
			m_compressionLibrary = value;
		}
	}

	public virtual void Apply()
	{
		if (IL.IsInitialized)
		{
			if (m_saveInterlaced)
			{
				IL.Enable(ILEnable.SaveInterlaced);
			}
			else
			{
				IL.Disable(ILEnable.SaveInterlaced);
			}
			if (m_overwriteExistingFile)
			{
				IL.Enable(ILEnable.OverwriteExistingFile);
			}
			else
			{
				IL.Disable(ILEnable.OverwriteExistingFile);
			}
			switch (m_compressionLibrary)
			{
			case CompressionLibrary.Default:
				IL.Disable(ILEnable.NvidiaCompression);
				IL.Disable(ILEnable.SquishCompression);
				break;
			case CompressionLibrary.Nvidia:
				IL.Disable(ILEnable.SquishCompression);
				IL.Enable(ILEnable.NvidiaCompression);
				break;
			case CompressionLibrary.Squish:
				IL.Disable(ILEnable.NvidiaCompression);
				IL.Enable(ILEnable.SquishCompression);
				break;
			}
		}
	}
}
