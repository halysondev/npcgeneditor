using DevIL.Unmanaged;

namespace DevIL;

public sealed class LanguageState : IImageState
{
	private Language m_language = Language.English;

	public Language Language
	{
		get
		{
			return m_language;
		}
		set
		{
			m_language = value;
		}
	}

	public void Apply()
	{
		if (ILU.IsInitialized)
		{
			ILU.SetLanguage(m_language);
		}
	}
}
