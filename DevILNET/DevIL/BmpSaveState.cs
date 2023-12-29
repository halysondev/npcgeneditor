using DevIL.Unmanaged;

namespace DevIL;

public sealed class BmpSaveState : SaveState
{
	private bool m_useBmpRle;

	private int m_pcdPicNumber = 2;

	public bool UseBmpRle
	{
		get
		{
			return m_useBmpRle;
		}
		set
		{
			m_useBmpRle = value;
		}
	}

	public int PcdPicNumber
	{
		get
		{
			return m_pcdPicNumber;
		}
		set
		{
			m_pcdPicNumber = value;
		}
	}

	public override void Apply()
	{
		base.Apply();
		if (IL.IsInitialized)
		{
			IL.SetBoolean(ILBooleanMode.BmpRLE, m_useBmpRle);
			IL.SetInteger(ILIntegerMode.PcdPicNumber, m_pcdPicNumber);
		}
	}
}
