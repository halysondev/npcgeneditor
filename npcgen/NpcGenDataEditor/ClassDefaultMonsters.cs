using System.Collections.Generic;

namespace NpcGenDataEditor;

public class ClassDefaultMonsters
{
	public int Location;

	public int Amount_in_group;

	public float X_position;

	public float Y_position;

	public float Z_position;

	public float X_direction;

	public float Y_direction;

	public float Z_direction;

	public float X_random;

	public float Y_random;

	public float Z_random;

	public int Type;

	public int iGroupType;

	public byte BInitGen;

	public byte bAutoRevive;

	public byte BValicOnce;

	public int dwGenId;

	public int Trigger_id;

	public int Life_time;

	public int MaxRespawnTime;

	public List<ClassExtraMonsters> MobDops;
}
