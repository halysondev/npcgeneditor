using System;
using System.IO;
using DevIL.Unmanaged;

namespace DevIL;

public sealed class ImageImporter : IDisposable
{
	private FilterEngine m_filterEngine;

	private TransformEngine m_transformEngine;

	private bool m_isDisposed;

	public FilterEngine Filter => m_filterEngine;

	public TransformEngine Transform => m_transformEngine;

	public ImageImporter()
	{
		m_filterEngine = new FilterEngine();
		m_transformEngine = new TransformEngine();
		m_isDisposed = false;
		IL.AddRef();
	}

	~ImageImporter()
	{
		Dispose(disposing: false);
	}

	public Image LoadImage(string filename)
	{
		if (string.IsNullOrEmpty(filename))
		{
			throw new IOException("Failed to load image, file does not exist.");
		}
		CheckDisposed();
		ImageID id = GenImage();
		if (IL.LoadImage(filename))
		{
			return new Image(id);
		}
		throw new IOException($"Failed to loade image: {IL.GetError()}");
	}

	public Image LoadImage(ImageType imageType, string filename)
	{
		if (imageType == ImageType.Unknown || string.IsNullOrEmpty(filename))
		{
			throw new IOException("Failed to load image, invalid imagetype or file does not exist.");
		}
		CheckDisposed();
		ImageID id = GenImage();
		if (IL.LoadImage(imageType, filename))
		{
			return new Image(id);
		}
		throw new IOException($"Failed to loade image: {IL.GetError()}");
	}

	public Image LoadImageFromStream(Stream stream)
	{
		if (stream == null || !stream.CanRead)
		{
			throw new IOException("Failed to load image, Stream is null or write-only");
		}
		CheckDisposed();
		ImageID id = GenImage();
		if (IL.LoadImageFromStream(stream))
		{
			return new Image(id);
		}
		throw new IOException($"Failed to loade image: {IL.GetError()}");
	}

	public Image LoadImageFromStream(ImageType imageType, Stream stream)
	{
		if (imageType == ImageType.Unknown || stream == null || !stream.CanRead)
		{
			throw new IOException("Failed to load image, invalid imagetype or stream can't be read from.");
		}
		CheckDisposed();
		ImageID id = GenImage();
		if (IL.LoadImageFromStream(imageType, stream))
		{
			return new Image(id);
		}
		throw new IOException($"Failed to load image: {IL.GetError()}");
	}

	public string[] GetSupportedExtensions()
	{
		return IL.GetImportExtensions();
	}

	private void CheckDisposed()
	{
		if (m_isDisposed)
		{
			throw new ObjectDisposedException("Importer has been disposed.");
		}
	}

	private ImageID GenImage()
	{
		ImageID imageID = IL.GenerateImage();
		IL.BindImage(imageID);
		return imageID;
	}

	public void Dispose()
	{
		Dispose(disposing: true);
		GC.SuppressFinalize(this);
	}

	private void Dispose(bool disposing)
	{
		if (!m_isDisposed)
		{
			IL.Release();
			m_isDisposed = true;
		}
	}
}
