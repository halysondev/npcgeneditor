using DevIL.Unmanaged;

namespace DevIL;

public class FilterEngine
{
	public SamplingFilter SamplingFilter
	{
		get
		{
			return ILU.GetSamplingFilter();
		}
		set
		{
			ILU.SetSamplingFilter(value);
		}
	}

	public bool Alienify(Image image)
	{
		if (image == null || !image.IsValid)
		{
			return false;
		}
		IL.BindImage(image.ImageID);
		return ILU.Alienify();
	}

	public bool BlurAverage(Image image, int iterations)
	{
		if (image == null || !image.IsValid)
		{
			return false;
		}
		IL.BindImage(image.ImageID);
		return ILU.BlurAverage(iterations);
	}

	public bool BlurGaussian(Image image, int iterations)
	{
		if (image == null || !image.IsValid)
		{
			return false;
		}
		IL.BindImage(image.ImageID);
		return ILU.BlurGaussian(iterations);
	}

	public bool BuildMipMaps(Image image)
	{
		if (image == null || !image.IsValid)
		{
			return false;
		}
		IL.BindImage(image.ImageID);
		return ILU.BuildMipMaps();
	}

	public bool Contrast(Image image, float contrast)
	{
		if (image == null || !image.IsValid)
		{
			return false;
		}
		IL.BindImage(image.ImageID);
		return ILU.Contrast(contrast);
	}

	public bool Convolution(Image image, int[] matrix, int scale, int bias)
	{
		if (image == null || !image.IsValid)
		{
			return false;
		}
		IL.BindImage(image.ImageID);
		return ILU.Convolution(matrix, scale, bias);
	}

	public bool EdgeDetectE(Image image)
	{
		if (image == null || !image.IsValid)
		{
			return false;
		}
		IL.BindImage(image.ImageID);
		return ILU.EdgeDetectE();
	}

	public bool EdgeDetectP(Image image)
	{
		if (image == null || !image.IsValid)
		{
			return false;
		}
		IL.BindImage(image.ImageID);
		return ILU.EdgeDetectP();
	}

	public bool EdgeDetectS(Image image)
	{
		if (image == null || !image.IsValid)
		{
			return false;
		}
		IL.BindImage(image.ImageID);
		return ILU.EdgeDetectS();
	}

	public bool Emboss(Image image)
	{
		if (image == null || !image.IsValid)
		{
			return false;
		}
		IL.BindImage(image.ImageID);
		return ILU.Emboss();
	}

	public bool Equalize(Image image)
	{
		if (image == null || !image.IsValid)
		{
			return false;
		}
		IL.BindImage(image.ImageID);
		return ILU.Equalize();
	}

	public bool GammaCorrect(Image image, float gamma)
	{
		if (image == null || !image.IsValid)
		{
			return false;
		}
		IL.BindImage(image.ImageID);
		return ILU.GammaCorrect(gamma);
	}

	public bool InvertAlpha(Image image)
	{
		if (image == null || !image.IsValid)
		{
			return false;
		}
		IL.BindImage(image.ImageID);
		return ILU.InvertAlpha();
	}

	public bool Negative(Image image)
	{
		if (image == null || !image.IsValid)
		{
			return false;
		}
		IL.BindImage(image.ImageID);
		return ILU.Negative();
	}

	public bool Noisify(Image image, float tolerance)
	{
		if (image == null || !image.IsValid)
		{
			return false;
		}
		IL.BindImage(image.ImageID);
		return ILU.Noisify(tolerance);
	}

	public bool Pixelize(Image image, int pixelSize)
	{
		if (image == null || !image.IsValid)
		{
			return false;
		}
		IL.BindImage(image.ImageID);
		return ILU.Pixelize(pixelSize);
	}

	public bool Saturate(Image image, float saturation)
	{
		if (image == null || !image.IsValid)
		{
			return false;
		}
		IL.BindImage(image.ImageID);
		return ILU.Saturate(saturation);
	}

	public bool Saturate(Image image, float red, float green, float blue, float saturation)
	{
		if (image == null || !image.IsValid)
		{
			return false;
		}
		IL.BindImage(image.ImageID);
		return ILU.Saturate(red, green, blue, saturation);
	}

	public bool Sharpen(Image image, float factor, int iterations)
	{
		if (image == null || !image.IsValid)
		{
			return false;
		}
		IL.BindImage(image.ImageID);
		return ILU.Sharpen(factor, iterations);
	}

	public bool Wave(Image image, float angle)
	{
		if (image == null || !image.IsValid)
		{
			return false;
		}
		IL.BindImage(image.ImageID);
		return ILU.Wave(angle);
	}
}
