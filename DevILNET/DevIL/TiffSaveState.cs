using DevIL.Unmanaged;

namespace DevIL;

public sealed class TiffSaveState : SaveState
{
	private string m_tifAuthName = string.Empty;

	private string m_tifDescription = string.Empty;

	private string m_tifDocumentName = string.Empty;

	private string m_tifHostComputer = string.Empty;

	public string TifAuthorName
	{
		get
		{
			return m_tifAuthName;
		}
		set
		{
			m_tifAuthName = value;
		}
	}

	public string TifDescription
	{
		get
		{
			return m_tifDescription;
		}
		set
		{
			m_tifDescription = value;
		}
	}

	public string TifDocumentName
	{
		get
		{
			return m_tifDocumentName;
		}
		set
		{
			m_tifDocumentName = value;
		}
	}

	public string TifHostComputer
	{
		get
		{
			return m_tifHostComputer;
		}
		set
		{
			m_tifHostComputer = value;
		}
	}

	public override void Apply()
	{
		base.Apply();
		if (IL.IsInitialized)
		{
			IL.SetString(ILStringMode.TifAuthorName, m_tifAuthName);
			IL.SetString(ILStringMode.TifDescription, m_tifDescription);
			IL.SetString(ILStringMode.TifDocumentName, m_tifDocumentName);
			IL.SetString(ILStringMode.TifHostComputer, m_tifHostComputer);
		}
	}
}
