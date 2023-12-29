using System;

namespace DevIL.Unmanaged;

public struct ImageID : IEquatable<ImageID>
{
	private int m_id;

	public int ID => m_id;

	public ImageID(int id)
	{
		m_id = id;
	}

	public static implicit operator ImageID(int id)
	{
		return new ImageID(id);
	}

	public static implicit operator int(ImageID id)
	{
		return id.m_id;
	}

	public static bool operator <(ImageID a, ImageID b)
	{
		return a.m_id < b.m_id;
	}

	public static bool operator >(ImageID a, ImageID b)
	{
		return a.m_id > b.m_id;
	}

	public static bool operator ==(ImageID a, ImageID b)
	{
		return a.m_id == b.m_id;
	}

	public static bool operator !=(ImageID a, ImageID b)
	{
		return a.m_id != b.m_id;
	}

	public bool Equals(ImageID other)
	{
		return m_id == other.m_id;
	}

	public override bool Equals(object obj)
	{
		if (obj is ImageID imageID)
		{
			return m_id == imageID.m_id;
		}
		return false;
	}

	public override int GetHashCode()
	{
		return m_id.GetHashCode();
	}

	public override string ToString()
	{
		return $"ImageID: {m_id.ToString()}";
	}
}
