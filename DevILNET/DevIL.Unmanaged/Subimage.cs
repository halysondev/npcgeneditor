namespace DevIL.Unmanaged;

public struct Subimage
{
	private ImageID m_rootImage;

	private int m_imageIndex;

	private int m_faceIndex;

	private int m_layerIndex;

	private int m_mipMapIndex;

	public ImageID RootImage => m_rootImage;

	public int ImageIndex => m_imageIndex;

	public int FaceIndex => m_faceIndex;

	public int LayerIndex => m_layerIndex;

	public int MipMapIndex => m_mipMapIndex;

	public Subimage(ImageID rootImage)
	{
		m_rootImage = rootImage;
		m_imageIndex = 0;
		m_faceIndex = 0;
		m_layerIndex = 0;
		m_mipMapIndex = 0;
	}

	public Subimage(ImageID rootImage, int imageIndex)
	{
		m_rootImage = rootImage;
		m_imageIndex = imageIndex;
		m_faceIndex = 0;
		m_layerIndex = 0;
		m_mipMapIndex = 0;
	}

	public Subimage(ImageID rootImage, int imageIndex, int faceIndex)
	{
		m_rootImage = rootImage;
		m_imageIndex = imageIndex;
		m_faceIndex = faceIndex;
		m_layerIndex = 0;
		m_mipMapIndex = 0;
	}

	public Subimage(ImageID rootImage, int imageIndex, int faceIndex, int layerIndex)
	{
		m_rootImage = rootImage;
		m_imageIndex = imageIndex;
		m_faceIndex = faceIndex;
		m_layerIndex = layerIndex;
		m_mipMapIndex = 0;
	}

	public Subimage(ImageID rootImage, int imageIndex, int faceIndex, int layerIndex, int mipMapIndex)
	{
		m_rootImage = rootImage;
		m_imageIndex = imageIndex;
		m_faceIndex = faceIndex;
		m_layerIndex = layerIndex;
		m_mipMapIndex = mipMapIndex;
	}

	public bool Activate()
	{
		if ((int)m_rootImage <= 0)
		{
			return false;
		}
		IL.BindImage(m_rootImage);
		if (m_imageIndex > 0 && !IL.ActiveImage(m_imageIndex))
		{
			return false;
		}
		if (m_faceIndex > 0 && !IL.ActiveFace(m_faceIndex))
		{
			return false;
		}
		if (m_layerIndex > 0 && !IL.ActiveLayer(m_layerIndex))
		{
			return false;
		}
		if (m_mipMapIndex > 0 && !IL.ActiveMipMap(m_mipMapIndex))
		{
			return false;
		}
		return true;
	}
}
