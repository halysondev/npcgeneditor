using DevIL.Unmanaged;

namespace DevIL;

public sealed class ImageState : IImageState
{
	private bool m_useAbsoluteFormat;

	private bool m_useAbsoluteDataType;

	private bool m_useAbsoluteOrigin;

	private DataFormat m_absoluteFormat = DataFormat.BGRA;

	private DataType m_absoluteDataType = DataType.UnsignedByte;

	private OriginLocation m_absoluteOrigin = OriginLocation.LowerLeft;

	private Color m_keyColor = new Color(1f, 0f, 1f, 1f);

	private bool m_convertPalette;

	private bool m_defaultImageOnFail;

	private bool m_useColorKey;

	private bool m_blitBlend = true;

	public bool UseAbsoluteFormat
	{
		get
		{
			return m_useAbsoluteFormat;
		}
		set
		{
			m_useAbsoluteFormat = value;
		}
	}

	public bool UseAbsoluteDataType
	{
		get
		{
			return m_useAbsoluteDataType;
		}
		set
		{
			m_useAbsoluteDataType = value;
		}
	}

	public bool UseAbsoluteOrigin
	{
		get
		{
			return m_useAbsoluteOrigin;
		}
		set
		{
			m_useAbsoluteOrigin = value;
		}
	}

	public DataFormat AbsoluteFormat
	{
		get
		{
			return m_absoluteFormat;
		}
		set
		{
			m_absoluteFormat = value;
			UseAbsoluteFormat = true;
		}
	}

	public DataType AbsoluteDataType
	{
		get
		{
			return m_absoluteDataType;
		}
		set
		{
			m_absoluteDataType = value;
			UseAbsoluteDataType = true;
		}
	}

	public OriginLocation AbsoluteOrigin
	{
		get
		{
			return m_absoluteOrigin;
		}
		set
		{
			m_absoluteOrigin = value;
			UseAbsoluteOrigin = true;
		}
	}

	public bool ConvertPalette
	{
		get
		{
			return m_convertPalette;
		}
		set
		{
			m_convertPalette = value;
		}
	}

	public bool DefaultImageOnFail
	{
		get
		{
			return m_defaultImageOnFail;
		}
		set
		{
			m_defaultImageOnFail = value;
		}
	}

	public bool UseColorKey
	{
		get
		{
			return m_useColorKey;
		}
		set
		{
			m_useColorKey = value;
		}
	}

	public Color ColorKey
	{
		get
		{
			return m_keyColor;
		}
		set
		{
			m_keyColor = value;
			m_useColorKey = true;
		}
	}

	public bool BlitBlend
	{
		get
		{
			return m_blitBlend;
		}
		set
		{
			m_blitBlend = value;
		}
	}

	public void Apply()
	{
		if (IL.IsInitialized)
		{
			if (m_useAbsoluteFormat)
			{
				IL.Enable(ILEnable.AbsoluteFormat);
			}
			else
			{
				IL.Disable(ILEnable.AbsoluteFormat);
			}
			IL.SetDataFormat(m_absoluteFormat);
			if (m_useAbsoluteDataType)
			{
				IL.Enable(ILEnable.AbsoluteType);
			}
			else
			{
				IL.Disable(ILEnable.AbsoluteType);
			}
			IL.SetDataType(m_absoluteDataType);
			if (m_useAbsoluteOrigin)
			{
				IL.Enable(ILEnable.AbsoluteOrigin);
			}
			else
			{
				IL.Disable(ILEnable.AbsoluteOrigin);
			}
			IL.SetOriginLocation(m_absoluteOrigin);
			if (m_convertPalette)
			{
				IL.Enable(ILEnable.ConvertPalette);
			}
			else
			{
				IL.Disable(ILEnable.ConvertPalette);
			}
			if (m_defaultImageOnFail)
			{
				IL.Enable(ILEnable.DefaultOnFail);
			}
			else
			{
				IL.Disable(ILEnable.DefaultOnFail);
			}
			if (m_useColorKey)
			{
				IL.Enable(ILEnable.UseColorKey);
				IL.SetKeyColor(m_keyColor);
			}
			else
			{
				IL.Disable(ILEnable.UseColorKey);
			}
			if (m_blitBlend)
			{
				IL.Enable(ILEnable.BlitBlend);
			}
			else
			{
				IL.Disable(ILEnable.BlitBlend);
			}
		}
	}
}
