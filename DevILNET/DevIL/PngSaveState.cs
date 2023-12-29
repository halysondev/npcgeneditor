using DevIL.Unmanaged;

namespace DevIL;

public sealed class PngSaveState : SaveState
{
	private int m_pngAlphaIndex = -1;

	private string m_pngAuthName = string.Empty;

	private string m_pngTitle = string.Empty;

	private string m_pngDescription = string.Empty;

	private bool m_usePngInterlace;

	public int PngAlphaIndex
	{
		get
		{
			return m_pngAlphaIndex;
		}
		set
		{
			m_pngAlphaIndex = value;
		}
	}

	public string PngAuthorName
	{
		get
		{
			return m_pngAuthName;
		}
		set
		{
			m_pngAuthName = value;
		}
	}

	public string PngTitle
	{
		get
		{
			return m_pngTitle;
		}
		set
		{
			m_pngTitle = value;
		}
	}

	public string PngDescription
	{
		get
		{
			return m_pngDescription;
		}
		set
		{
			m_pngDescription = value;
		}
	}

	public bool UsePngInterlace
	{
		get
		{
			return m_usePngInterlace;
		}
		set
		{
			m_usePngInterlace = value;
		}
	}

	public override void Apply()
	{
		base.Apply();
		if (IL.IsInitialized)
		{
			IL.SetInteger(ILIntegerMode.PngAlphaIndex, m_pngAlphaIndex);
			IL.SetString(ILStringMode.PngAuthorName, m_pngAuthName);
			IL.SetString(ILStringMode.PngTitle, m_pngTitle);
			IL.SetString(ILStringMode.PngDescription, m_pngDescription);
			IL.SetBoolean(ILBooleanMode.PngInterlace, m_usePngInterlace);
		}
	}
}
