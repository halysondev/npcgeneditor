using DevIL.Unmanaged;

namespace DevIL;

public class TransformEngine
{
	public Placement ImagePlacement
	{
		get
		{
			return ILU.GetImagePlacement();
		}
		set
		{
			ILU.SetImagePlacement(value);
		}
	}

	public bool Crop(Image image, int offsetX, int offsetY, int offsetZ, int width, int height, int depth)
	{
		if (image == null || !image.IsValid)
		{
			return false;
		}
		IL.BindImage(image.ImageID);
		return ILU.Crop(offsetX, offsetY, offsetZ, width, height, depth);
	}

	public bool EnlargeCanvas(Image image, int width, int height, int depth)
	{
		if (image == null || !image.IsValid)
		{
			return false;
		}
		IL.BindImage(image.ImageID);
		return ILU.EnlargeCanvas(width, height, depth);
	}

	public bool EnlargeImage(Image image, int xDim, int yDim, int zDim)
	{
		if (image == null || !image.IsValid)
		{
			return false;
		}
		IL.BindImage(image.ImageID);
		return ILU.EnlargeImage(xDim, yDim, zDim);
	}

	public bool Scale(Image image, int width, int height, int depth)
	{
		if (image == null || !image.IsValid)
		{
			return false;
		}
		IL.BindImage(image.ImageID);
		return ILU.Scale(width, height, depth);
	}

	public bool ScaleAlpha(Image image, float scale)
	{
		if (image == null || !image.IsValid)
		{
			return false;
		}
		IL.BindImage(image.ImageID);
		return ILU.ScaleAlpha(scale);
	}

	public bool ScaleColors(Image image, float red, float green, float blue)
	{
		if (image == null || !image.IsValid)
		{
			return false;
		}
		IL.BindImage(image.ImageID);
		return ILU.ScaleColors(red, green, blue);
	}

	public bool ReplaceColor(Image image, byte red, byte green, byte blue, float tolerance)
	{
		if (image == null || !image.IsValid)
		{
			return false;
		}
		IL.BindImage(image.ImageID);
		return ILU.ReplaceColor(red, green, blue, tolerance);
	}

	public bool SwapColors(Image image)
	{
		if (image == null || !image.IsValid)
		{
			return false;
		}
		IL.BindImage(image.ImageID);
		return ILU.SwapColors();
	}

	public bool FlipImage(Image image)
	{
		if (image == null || !image.IsValid)
		{
			return false;
		}
		IL.BindImage(image.ImageID);
		return ILU.FlipImage();
	}

	public bool Mirror(Image image)
	{
		if (image == null || !image.IsValid)
		{
			return false;
		}
		IL.BindImage(image.ImageID);
		return ILU.Mirror();
	}

	public bool Rotate(Image image, float angle)
	{
		if (image == null || !image.IsValid)
		{
			return false;
		}
		IL.BindImage(image.ImageID);
		return ILU.Rotate(angle);
	}

	public bool Rotate3D(Image image, float x, float y, float z, float angle)
	{
		if (image == null || !image.IsValid)
		{
			return false;
		}
		IL.BindImage(image.ImageID);
		return ILU.Rotate3D(x, y, z, angle);
	}
}
