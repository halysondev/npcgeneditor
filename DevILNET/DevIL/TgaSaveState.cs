using DevIL.Unmanaged;

namespace DevIL;

public sealed class TgaSaveState : SaveState
{
	private string m_tgaId = string.Empty;

	private string m_tgaAuthName = string.Empty;

	private string m_tgaAuthComment = string.Empty;

	private bool m_useTgaRle;

	private bool m_tgaCreateTimeStamp;

	public string TgaID
	{
		get
		{
			return m_tgaId;
		}
		set
		{
			m_tgaId = value;
		}
	}

	public string TgaAuthorName
	{
		get
		{
			return m_tgaAuthName;
		}
		set
		{
			m_tgaAuthName = value;
		}
	}

	public string TgaAuthorComment
	{
		get
		{
			return m_tgaAuthComment;
		}
		set
		{
			m_tgaAuthComment = value;
		}
	}

	public bool UseTgaRle
	{
		get
		{
			return m_useTgaRle;
		}
		set
		{
			m_useTgaRle = value;
		}
	}

	public override void Apply()
	{
		base.Apply();
		if (IL.IsInitialized)
		{
			IL.SetString(ILStringMode.TgaID, m_tgaId);
			IL.SetString(ILStringMode.TgaAuthorName, m_tgaAuthName);
			IL.SetString(ILStringMode.TgaAuthorComment, m_tgaAuthComment);
			IL.SetBoolean(ILBooleanMode.TgaRLE, m_useTgaRle);
			IL.SetBoolean(ILBooleanMode.TgaCreateStamp, m_tgaCreateTimeStamp);
		}
	}
}
