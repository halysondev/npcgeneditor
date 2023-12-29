namespace DevIL;

public enum CubeMapFace
{
	None = 0,
	PositiveX = 0x400,
	NegativeX = 0x800,
	PositiveY = 0x1000,
	NegativeY = 0x2000,
	PositiveZ = 0x4000,
	NegativeZ = 0x8000,
	SphereMap = 0x10000
}
