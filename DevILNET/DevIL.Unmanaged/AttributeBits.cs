using System;

namespace DevIL.Unmanaged;

[Flags]
public enum AttributeBits
{
	Origin = 1,
	File = 2,
	Palette = 4,
	Format = 8,
	Type = 0x10,
	Compress = 0x20,
	LoadFail = 0x40,
	FormatSpecific = 0x80,
	All = 0xFFFFF
}
