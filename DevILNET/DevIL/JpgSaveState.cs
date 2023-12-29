using DevIL.Unmanaged;

namespace DevIL;

public sealed class JpgSaveState : SaveState
{
	private int m_jpgQuality = 99;

	private JpgSaveFormat m_jpgSaveFormat = JpgSaveFormat.Jpg;

	private bool m_jpgProgressive;

	public int JpgQuality
	{
		get
		{
			return m_jpgQuality;
		}
		set
		{
			m_jpgQuality = value;
		}
	}

	public JpgSaveFormat JpgSaveFormat
	{
		get
		{
			return m_jpgSaveFormat;
		}
		set
		{
			m_jpgSaveFormat = value;
		}
	}

	public bool UseJpgProgressive
	{
		get
		{
			return m_jpgProgressive;
		}
		set
		{
			m_jpgProgressive = value;
		}
	}

	public override void Apply()
	{
		if (IL.IsInitialized)
		{
			IL.SetInteger(ILIntegerMode.JpgQuality, m_jpgQuality);
			IL.SetJpgSaveFormat(m_jpgSaveFormat);
			if (m_jpgProgressive)
			{
				IL.Enable(ILEnable.JpgProgressive);
			}
			else
			{
				IL.Disable(ILEnable.JpgProgressive);
			}
		}
	}
}
