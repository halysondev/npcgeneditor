using DevIL.Unmanaged;

namespace DevIL;

public sealed class SgiSaveState : SaveState
{
	private bool m_useSgiRle;

	public bool UseSgiRle
	{
		get
		{
			return m_useSgiRle;
		}
		set
		{
			m_useSgiRle = value;
		}
	}

	public override void Apply()
	{
		base.Apply();
		if (IL.IsInitialized)
		{
			IL.SetBoolean(ILBooleanMode.SgiRLE, m_useSgiRle);
		}
	}
}
