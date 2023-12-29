using System;
using System.IO;
using DevIL.Unmanaged;

namespace DevIL;

public sealed class ImageExporter : IDisposable
{
	private bool m_isDisposed;

	public bool IsDisposed => m_isDisposed;

	public ImageExporter()
	{
		m_isDisposed = false;
		IL.AddRef();
	}

	~ImageExporter()
	{
		Dispose(disposing: false);
	}

	public bool SaveImage(Image image, string filename)
	{
		if (!image.IsValid || string.IsNullOrEmpty(filename))
		{
			return false;
		}
		CheckDisposed();
		IL.BindImage(image.ImageID);
		return IL.SaveImage(filename);
	}

	public bool SaveImage(Image image, ImageType imageType, string filename)
	{
		if (!image.IsValid || imageType == ImageType.Unknown || string.IsNullOrEmpty(filename))
		{
			return false;
		}
		CheckDisposed();
		IL.BindImage(image.ImageID);
		return IL.SaveImage(imageType, filename);
	}

	public bool SaveImageToStream(Image image, ImageType imageType, Stream stream)
	{
		if (!image.IsValid || imageType == ImageType.Unknown || stream == null || !stream.CanWrite)
		{
			return false;
		}
		CheckDisposed();
		IL.BindImage(image.ImageID);
		return IL.SaveImageToStream(imageType, stream);
	}

	public string[] GetSupportedExtensions()
	{
		return IL.GetExportExtensions();
	}

	private void CheckDisposed()
	{
		if (m_isDisposed)
		{
			throw new ObjectDisposedException("Exporter has been disposed.");
		}
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
