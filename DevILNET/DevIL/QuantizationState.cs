using DevIL.Unmanaged;

namespace DevIL;

public sealed class QuantizationState : IImageState
{
	private Quantization m_quantMode = Quantization.Wu;

	private int m_maxQuantIndices = 256;

	private int m_neuQuantSample = 15;

	public Quantization QuantizationMode
	{
		get
		{
			return m_quantMode;
		}
		set
		{
			m_quantMode = value;
		}
	}

	public int MaxQuantizationIndices
	{
		get
		{
			return m_maxQuantIndices;
		}
		set
		{
			m_maxQuantIndices = value;
		}
	}

	public int NeuQuantizationSamples
	{
		get
		{
			return m_neuQuantSample;
		}
		set
		{
			m_neuQuantSample = value;
		}
	}

	public void Apply()
	{
		if (IL.IsInitialized)
		{
			IL.SetQuantization(m_quantMode);
			IL.SetInteger(ILIntegerMode.MaxQuantizationIndices, m_maxQuantIndices);
			IL.SetInteger(ILIntegerMode.NeuQuantizationSample, m_neuQuantSample);
		}
	}
}
