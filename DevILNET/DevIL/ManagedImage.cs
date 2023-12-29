using DevIL.Unmanaged;

namespace DevIL;

public class ManagedImage
{
	private MipMapChainCollection m_faces;

	private AnimationChainCollection m_animChain;

	public MipMapChainCollection Faces => m_faces;

	public AnimationChainCollection AnimationChain => m_animChain;

	public ManagedImage(Image image)
	{
		m_faces = new MipMapChainCollection();
		m_animChain = new AnimationChainCollection();
		if (image.IsValid)
		{
			ImageID imageID = image.ImageID;
			LoadFaces(imageID, 0);
			LoadAnimationChain(imageID);
		}
	}

	private ManagedImage(ImageID imageID, int imageNum)
	{
		m_faces = new MipMapChainCollection();
		m_animChain = new AnimationChainCollection();
		LoadFaces(imageID, imageNum);
	}

	private void LoadAnimationChain(ImageID imageID)
	{
		IL.BindImage(imageID);
		int num = IL.ilGetInteger(3569u) + 1;
		if (num <= 1)
		{
			return;
		}
		m_animChain.Add(this);
		for (int i = 1; i < num; i++)
		{
			ManagedImage managedImage = new ManagedImage(imageID, i);
			if (managedImage.Faces.Count != 0)
			{
				m_animChain.Add(managedImage);
			}
		}
	}

	private void LoadFaces(ImageID imageID, int imageNum)
	{
		IL.BindImage(imageID);
		if (!IL.ActiveImage(imageNum))
		{
			return;
		}
		int num = IL.ilGetInteger(3553u) + 1;
		for (int i = 0; i < num; i++)
		{
			MipMapChain mipMapChain = CreateMipMapChain(imageID, imageNum, i);
			if (mipMapChain == null)
			{
				break;
			}
			m_faces.Add(mipMapChain);
		}
	}

	private MipMapChain CreateMipMapChain(ImageID imageID, int imageNum, int faceNum)
	{
		IL.BindImage(imageID);
		if (!IL.ActiveImage(imageNum))
		{
			return null;
		}
		if (!IL.ActiveFace(faceNum))
		{
			return null;
		}
		int num = IL.ilGetInteger(3570u) + 1;
		MipMapChain mipMapChain = new MipMapChain();
		for (int i = 0; i < num; i++)
		{
			ImageData imageData = ImageData.Load(new Subimage(imageID, imageNum, faceNum, 0, i));
			if (imageData == null)
			{
				break;
			}
			mipMapChain.Add(imageData);
		}
		mipMapChain.TrimExcess();
		return mipMapChain;
	}
}
