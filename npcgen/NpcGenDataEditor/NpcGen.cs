using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace NpcGenDataEditor;

internal class NpcGen
{
	public int File_version;

	public int NpcMobsAmount;

	public int ResourcesAmount;

	public int DynobjectAmount;

	public int TriggersAmount;

	public List<ClassDefaultMonsters> NpcMobList = new List<ClassDefaultMonsters>();

	public List<ClassDefaultResources> ResourcesList = new List<ClassDefaultResources>();

	public List<ClassDynamicObject> DynamicsList = new List<ClassDynamicObject>();

	public List<ClassTrigger> TriggersList = new List<ClassTrigger>();

	public void ReadNpcgen(BinaryReader br)
	{
		File_version = br.ReadInt32();
		NpcMobsAmount = br.ReadInt32();
		ResourcesAmount = br.ReadInt32();
		DynobjectAmount = br.ReadInt32();
		if (File_version > 6)
		{
			TriggersAmount = br.ReadInt32();
		}
		for (int i = 0; i < NpcMobsAmount; i++)
		{
			NpcMobList.Add(ReadExistence(br, File_version));
		}
		for (int j = 0; j < ResourcesAmount; j++)
		{
			ResourcesList.Add(ReadResource(br, File_version));
		}
		for (int k = 0; k < DynobjectAmount; k++)
		{
			DynamicsList.Add(ReadDynObjects(br, File_version));
		}
		if (File_version > 6)
		{
			for (int l = 0; l < TriggersAmount; l++)
			{
				TriggersList.Add(ReadTrigger(br, File_version));
			}
		}
	}

	public void WriteNpcgen(BinaryWriter bw, int Version)
	{
		bw.Write(Version);
		bw.Write(NpcMobsAmount);
		bw.Write(ResourcesAmount);
		bw.Write(DynobjectAmount);
		if (Version > 6)
		{
			bw.Write(TriggersAmount);
		}
		for (int i = 0; i < NpcMobsAmount; i++)
		{
			WriteExistence(bw, Version, i);
		}
		for (int j = 0; j < ResourcesAmount; j++)
		{
			WriteResource(bw, Version, j);
		}
		for (int k = 0; k < DynobjectAmount; k++)
		{
			WriteDynamic(bw, Version, k);
		}
		if (Version > 6)
		{
			for (int l = 0; l < TriggersAmount; l++)
			{
				WriteTrigger(bw, Version, l);
			}
		}
	}

	public void ExportExistence(string Path, List<int> SelectedExistence)
	{
		BinaryWriter binaryWriter = new BinaryWriter(File.Create(Path));
		binaryWriter.Write("Npcgen Editor by Luka||Existence");
		binaryWriter.Write(SelectedExistence.Count);
		foreach (int item in SelectedExistence)
		{
			WriteExistence(binaryWriter, 15, item);
		}
		binaryWriter.Close();
	}

	public void ExportResource(string Path, List<int> SelectedResources)
	{
		BinaryWriter binaryWriter = new BinaryWriter(File.Create(Path));
		binaryWriter.Write("Npcgen Editor by Luka||Resources");
		binaryWriter.Write(SelectedResources.Count);
		foreach (int SelectedResource in SelectedResources)
		{
			WriteResource(binaryWriter, 15, SelectedResource);
		}
		binaryWriter.Close();
	}

	public void ExportDynamics(string Path, List<int> SelectedDynamics)
	{
		BinaryWriter binaryWriter = new BinaryWriter(File.Create(Path));
		binaryWriter.Write("Npcgen Editor by Luka||DynObject");
		binaryWriter.Write(SelectedDynamics.Count);
		foreach (int SelectedDynamic in SelectedDynamics)
		{
			WriteDynamic(binaryWriter, 15, SelectedDynamic);
		}
		binaryWriter.Close();
	}

	public void ExportTriggers(string Path, List<int> SelectedTriggers)
	{
		BinaryWriter binaryWriter = new BinaryWriter(File.Create(Path));
		binaryWriter.Write("Npcgen Editor by Luka||Triggerss");
		binaryWriter.Write(SelectedTriggers.Count);
		foreach (int SelectedTrigger in SelectedTriggers)
		{
			WriteTrigger(binaryWriter, 15, SelectedTrigger);
		}
		binaryWriter.Close();
	}

	public ClassDefaultMonsters ReadExistence(BinaryReader br, int Version)
	{
		ClassDefaultMonsters classDefaultMonsters = new ClassDefaultMonsters();
		classDefaultMonsters.Location = br.ReadInt32();
		classDefaultMonsters.Amount_in_group = br.ReadInt32();
		classDefaultMonsters.X_position = br.ReadSingle();
		classDefaultMonsters.Y_position = br.ReadSingle();
		classDefaultMonsters.Z_position = br.ReadSingle();
		classDefaultMonsters.X_direction = br.ReadSingle();
		classDefaultMonsters.Y_direction = br.ReadSingle();
		classDefaultMonsters.Z_direction = br.ReadSingle();
		classDefaultMonsters.X_random = br.ReadSingle();
		classDefaultMonsters.Y_random = br.ReadSingle();
		classDefaultMonsters.Z_random = br.ReadSingle();
		classDefaultMonsters.Type = br.ReadInt32();
		classDefaultMonsters.iGroupType = br.ReadInt32();
		classDefaultMonsters.BInitGen = br.ReadByte();
		classDefaultMonsters.bAutoRevive = br.ReadByte();
		classDefaultMonsters.BValicOnce = br.ReadByte();
		classDefaultMonsters.dwGenId = br.ReadInt32();
		if (Version > 6)
		{
			classDefaultMonsters.Trigger_id = br.ReadInt32();
			classDefaultMonsters.Life_time = br.ReadInt32();
			classDefaultMonsters.MaxRespawnTime = br.ReadInt32();
		}
		classDefaultMonsters.MobDops = new List<ClassExtraMonsters>(classDefaultMonsters.Amount_in_group);
		for (int i = 0; i < classDefaultMonsters.Amount_in_group; i++)
		{
			ClassExtraMonsters classExtraMonsters = new ClassExtraMonsters();
			classExtraMonsters.Id = br.ReadInt32();
			classExtraMonsters.Amount = br.ReadInt32();
			classExtraMonsters.Respawn = br.ReadInt32();
			classExtraMonsters.Dead_amount = br.ReadInt32();
			classExtraMonsters.Agression = br.ReadInt32();
			classExtraMonsters.fOffsetWater = br.ReadSingle();
			classExtraMonsters.fOffsetTrn = br.ReadSingle();
			classExtraMonsters.Group = br.ReadInt32();
			classExtraMonsters.Group_help_sender = br.ReadInt32();
			classExtraMonsters.Group_help_Needer = br.ReadInt32();
			classExtraMonsters.bNeedHelp = br.ReadByte();
			classExtraMonsters.bFaction = br.ReadByte();
			classExtraMonsters.bFac_Helper = br.ReadByte();
			classExtraMonsters.bFac_Accept = br.ReadByte();
			classExtraMonsters.Path = br.ReadInt32();
			classExtraMonsters.Path_type = br.ReadInt32();
			classExtraMonsters.Speed = br.ReadInt32();
			classExtraMonsters.Dead_time = br.ReadInt32();
			if (Version >= 11)
			{
				classExtraMonsters.RefreshLower = br.ReadInt32();
			}
			classDefaultMonsters.MobDops.Add(classExtraMonsters);
		}
		return classDefaultMonsters;
	}

	public ClassDefaultResources ReadResource(BinaryReader br, int Version)
	{
		ClassDefaultResources classDefaultResources = new ClassDefaultResources();
		classDefaultResources.X_position = br.ReadSingle();
		classDefaultResources.Y_position = br.ReadSingle();
		classDefaultResources.Z_position = br.ReadSingle();
		classDefaultResources.X_Random = br.ReadSingle();
		classDefaultResources.Z_Random = br.ReadSingle();
		classDefaultResources.Amount_in_group = br.ReadInt32();
		classDefaultResources.bInitGen = br.ReadByte();
		classDefaultResources.bAutoRevive = br.ReadByte();
		classDefaultResources.bValidOnce = br.ReadByte();
		classDefaultResources.dwGenID = br.ReadInt32();
		classDefaultResources.InCline1 = br.ReadByte();
		classDefaultResources.InCline2 = br.ReadByte();
		classDefaultResources.Rotation = br.ReadByte();
		classDefaultResources.Trigger_id = br.ReadInt32();
		classDefaultResources.IMaxNum = br.ReadInt32();
		classDefaultResources.ResExtra = new List<ClassExtraResources>();
		for (int i = 0; i < classDefaultResources.Amount_in_group; i++)
		{
			ClassExtraResources classExtraResources = new ClassExtraResources();
			classExtraResources.ResourceType = br.ReadInt32();
			classExtraResources.Id = br.ReadInt32();
			classExtraResources.Respawntime = br.ReadInt32();
			classExtraResources.Amount = br.ReadInt32();
			classExtraResources.fHeiOff = br.ReadSingle();
			classDefaultResources.ResExtra.Add(classExtraResources);
		}
		return classDefaultResources;
	}

	public ClassDynamicObject ReadDynObjects(BinaryReader br, int Version)
	{
		ClassDynamicObject classDynamicObject = new ClassDynamicObject();
		classDynamicObject.Id = br.ReadInt32();
		classDynamicObject.X_position = br.ReadSingle();
		classDynamicObject.Y_position = br.ReadSingle();
		classDynamicObject.Z_position = br.ReadSingle();
		classDynamicObject.InCline1 = br.ReadByte();
		classDynamicObject.InCline2 = br.ReadByte();
		classDynamicObject.Rotation = br.ReadByte();
		classDynamicObject.TriggerId = br.ReadInt32();
		classDynamicObject.Scale = br.ReadByte();
		return classDynamicObject;
	}

	public ClassTrigger ReadTrigger(BinaryReader br, int Version)
	{
		ClassTrigger classTrigger = new ClassTrigger();
		classTrigger.Id = br.ReadInt32();
		classTrigger.GmID = br.ReadInt32();
		classTrigger.TriggerName = Encoding.GetEncoding(936).GetString(br.ReadBytes(128)).TrimEnd(default(char));
		classTrigger.AutoStart = br.ReadByte();
		classTrigger.WaitWhileStart = br.ReadInt32();
		classTrigger.WaitWhileStop = br.ReadInt32();
		classTrigger.DontStartOnSchedule = br.ReadByte();
		if (classTrigger.DontStartOnSchedule == 0)
		{
			classTrigger.DontStartOnSchedule = 1;
		}
		else if (classTrigger.DontStartOnSchedule == 1)
		{
			classTrigger.DontStartOnSchedule = 0;
		}
		classTrigger.DontStopOnSchedule = br.ReadByte();
		if (classTrigger.DontStopOnSchedule == 1)
		{
			classTrigger.DontStopOnSchedule = 0;
		}
		else if (classTrigger.DontStopOnSchedule == 0)
		{
			classTrigger.DontStopOnSchedule = 1;
		}
		classTrigger.StartYear = br.ReadInt32();
		classTrigger.StartMonth = br.ReadInt32();
		classTrigger.StartWeekDay = br.ReadInt32();
		classTrigger.StartDay = br.ReadInt32();
		classTrigger.StartHour = br.ReadInt32();
		classTrigger.StartMinute = br.ReadInt32();
		classTrigger.StopYear = br.ReadInt32();
		classTrigger.StopMonth = br.ReadInt32();
		classTrigger.StopWeekDay = br.ReadInt32();
		classTrigger.StopDay = br.ReadInt32();
		classTrigger.StopHour = br.ReadInt32();
		classTrigger.StopMinute = br.ReadInt32();
		if (File_version > 7)
		{
			classTrigger.Duration = br.ReadInt32();
		}
		return classTrigger;
	}

	public void WriteExistence(BinaryWriter bw, int Version, int i)
	{
		bw.Write(NpcMobList[i].Location);
		bw.Write(NpcMobList[i].Amount_in_group);
		bw.Write(NpcMobList[i].X_position);
		bw.Write(NpcMobList[i].Y_position);
		bw.Write(NpcMobList[i].Z_position);
		bw.Write(NpcMobList[i].X_direction);
		bw.Write(NpcMobList[i].Y_direction);
		bw.Write(NpcMobList[i].Z_direction);
		bw.Write(NpcMobList[i].X_random);
		bw.Write(NpcMobList[i].Y_random);
		bw.Write(NpcMobList[i].Z_random);
		bw.Write(NpcMobList[i].Type);
		bw.Write(NpcMobList[i].iGroupType);
		bw.Write(NpcMobList[i].BInitGen);
		bw.Write(NpcMobList[i].bAutoRevive);
		bw.Write(NpcMobList[i].BValicOnce);
		bw.Write(NpcMobList[i].dwGenId);
		if (Version > 6)
		{
			bw.Write(NpcMobList[i].Trigger_id);
			bw.Write(NpcMobList[i].Life_time);
			bw.Write(NpcMobList[i].MaxRespawnTime);
		}
		for (int j = 0; j < NpcMobList[i].Amount_in_group; j++)
		{
			bw.Write(NpcMobList[i].MobDops[j].Id);
			bw.Write(NpcMobList[i].MobDops[j].Amount);
			bw.Write(NpcMobList[i].MobDops[j].Respawn);
			bw.Write(NpcMobList[i].MobDops[j].Dead_amount);
			bw.Write(NpcMobList[i].MobDops[j].Agression);
			bw.Write(NpcMobList[i].MobDops[j].fOffsetWater);
			bw.Write(NpcMobList[i].MobDops[j].fOffsetTrn);
			bw.Write(NpcMobList[i].MobDops[j].Group);
			bw.Write(NpcMobList[i].MobDops[j].Group_help_sender);
			bw.Write(NpcMobList[i].MobDops[j].Group_help_Needer);
			bw.Write(NpcMobList[i].MobDops[j].bNeedHelp);
			bw.Write(NpcMobList[i].MobDops[j].bFaction);
			bw.Write(NpcMobList[i].MobDops[j].bFac_Helper);
			bw.Write(NpcMobList[i].MobDops[j].bFac_Accept);
			bw.Write(NpcMobList[i].MobDops[j].Path);
			bw.Write(NpcMobList[i].MobDops[j].Path_type);
			bw.Write(NpcMobList[i].MobDops[j].Speed);
			bw.Write(NpcMobList[i].MobDops[j].Dead_time);
			if (Version >= 11)
			{
				bw.Write(NpcMobList[i].MobDops[j].RefreshLower);
			}
		}
	}

	public void WriteResource(BinaryWriter bw, int Version, int i)
	{
		bw.Write(ResourcesList[i].X_position);
		bw.Write(ResourcesList[i].Y_position);
		bw.Write(ResourcesList[i].Z_position);
		bw.Write(ResourcesList[i].X_Random);
		bw.Write(ResourcesList[i].Z_Random);
		bw.Write(ResourcesList[i].Amount_in_group);
		bw.Write(ResourcesList[i].bInitGen);
		bw.Write(ResourcesList[i].bAutoRevive);
		bw.Write(ResourcesList[i].bValidOnce);
		bw.Write(ResourcesList[i].dwGenID);
		bw.Write(ResourcesList[i].InCline1);
		bw.Write(ResourcesList[i].InCline2);
		bw.Write(ResourcesList[i].Rotation);
		bw.Write(ResourcesList[i].Trigger_id);
		bw.Write(ResourcesList[i].IMaxNum);
		for (int j = 0; j < ResourcesList[i].Amount_in_group; j++)
		{
			bw.Write(ResourcesList[i].ResExtra[j].ResourceType);
			bw.Write(ResourcesList[i].ResExtra[j].Id);
			bw.Write(ResourcesList[i].ResExtra[j].Respawntime);
			bw.Write(ResourcesList[i].ResExtra[j].Amount);
			bw.Write(ResourcesList[i].ResExtra[j].fHeiOff);
		}
	}

	public void WriteDynamic(BinaryWriter bw, int Version, int i)
	{
		bw.Write(DynamicsList[i].Id);
		bw.Write(DynamicsList[i].X_position);
		bw.Write(DynamicsList[i].Y_position);
		bw.Write(DynamicsList[i].Z_position);
		bw.Write(DynamicsList[i].InCline1);
		bw.Write(DynamicsList[i].InCline2);
		bw.Write(DynamicsList[i].Rotation);
		bw.Write(DynamicsList[i].TriggerId);
		bw.Write(DynamicsList[i].Scale);
	}

	public void WriteTrigger(BinaryWriter bw, int Version, int i)
	{
		bw.Write(TriggersList[i].Id);
		bw.Write(TriggersList[i].GmID);
		bw.Write(GetBytes(TriggersList[i].TriggerName, 128, Encoding.GetEncoding(936)));
		bw.Write(TriggersList[i].AutoStart);
		bw.Write(TriggersList[i].WaitWhileStart);
		bw.Write(TriggersList[i].WaitWhileStop);
		if (TriggersList[i].DontStartOnSchedule == 1)
		{
			bw.Write((byte)0);
		}
		else
		{
			bw.Write((byte)1);
		}
		if (TriggersList[i].DontStopOnSchedule == 1)
		{
			bw.Write((byte)0);
		}
		else
		{
			bw.Write((byte)1);
		}
		bw.Write(TriggersList[i].StartYear);
		bw.Write(TriggersList[i].StartMonth);
		bw.Write(TriggersList[i].StartWeekDay);
		bw.Write(TriggersList[i].StartDay);
		bw.Write(TriggersList[i].StartHour);
		bw.Write(TriggersList[i].StartMinute);
		bw.Write(TriggersList[i].StopYear);
		bw.Write(TriggersList[i].StopMonth);
		bw.Write(TriggersList[i].StopWeekDay);
		bw.Write(TriggersList[i].StopDay);
		bw.Write(TriggersList[i].StopHour);
		bw.Write(TriggersList[i].StopMinute);
		if (Version > 7)
		{
			bw.Write(TriggersList[i].Duration);
		}
	}

	public byte[] GetBytes(string Name, int NameLength, Encoding e)
	{
		Name = Name.Split(default(char))[0];
		byte[] array = new byte[NameLength];
		if (e.GetByteCount(Name) > NameLength)
		{
			Array.Copy(e.GetBytes(Name), 0, array, 0, NameLength);
		}
		else
		{
			Array.Copy(e.GetBytes(Name), array, e.GetByteCount(Name));
		}
		return array;
	}
}
