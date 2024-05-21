using System;
using System.Collections.Generic;
using System.Text;
using sELedit;
using UdE;

namespace NpcGenDataEditor;

internal class Elementsdata
{
    private static LiteDB.LiteDatabase Cache = new LiteDB.LiteDatabase(AppDomain.CurrentDomain.BaseDirectory + "Cache.db");

    public int MonsterdAmount = 0;

	public int NpcsAmount = 0;

	public List<NpcMonster> ExistenceLists = new List<NpcMonster>();

	public List<NpcMonster> ResourcesList = new List<NpcMonster>();

    public Elementsdata(string file)
	{
		if (!UdE.API.IsElementsEditorRunning)
		{
			eListCollection eListCollection = new eListCollection(file);
			eList[] lists = eListCollection.Lists;
			foreach (eList eList in lists)
			{
				int num = Array.IndexOf(eList.elementFields, "ID");
				int num2 = Array.IndexOf(eList.elementFields, "Name");
				if (eList.listName.Contains("MONSTER_ESSENCE"))
				{
					object[][] elementValues = eList.elementValues;
					foreach (object[] array in elementValues)
					{
						ExistenceLists.Add(new NpcMonster
						{
							Id = int.Parse(array[num].ToString()),
							Name = Encoding.Unicode.GetString((byte[])array[num2])
						});
						MonsterdAmount++;
					}
				}
				if (eList.listName.Contains("NPC_ESSENCE"))
				{
					object[][] elementValues2 = eList.elementValues;
					foreach (object[] array2 in elementValues2)
					{
						ExistenceLists.Add(new NpcMonster
						{
							Id = int.Parse(array2[num].ToString()),
							Name = Encoding.Unicode.GetString((byte[])array2[num2])
						});
						NpcsAmount++;
					}
				}
				if (eList.listName.Contains("MINE_ESSENCE"))
				{
					object[][] elementValues3 = eList.elementValues;
					foreach (object[] array3 in elementValues3)
					{
						ResourcesList.Add(new NpcMonster
						{
							Id = int.Parse(array3[num].ToString()),
							Name = Encoding.Unicode.GetString((byte[])array3[num2])
						});
					}
				}
			}
		}
    }

    public static string GetNpcName(int id)
    {
        string result;
        if (UdE.API.IsElementsEditorRunning)
        {
            if (id != 0)
            {
                try
                {
                    result = UdE.API.GetItemNameByID(UdE.ItemType.NpcOrMobOrMine, id);
                }
                catch
                {
                    result = "Unknown";
                }
            }
            else
                result = "Unknown";
        }
        else
        {
            return  "Unknown";
        }
        return result;
    }

    public static string GetMobName(int id)
    {
        string result;
        if (UdE.API.IsElementsEditorRunning)
        {
            if (id != 0)
            {
                try
                {
                    result = UdE.API.GetItemNameByID(UdE.ItemType.NpcOrMobOrMine, id);
                }
                catch
                {
                    result = "Unknown";
                }
            }
            else
                result = "Unknown";
        }
        else
        {
            return "Unknown";
        }
        return result;
    }

    public static string GetMineName(int id)
    {
        string result;
        if (UdE.API.IsElementsEditorRunning)
        {
            if (id != 0)
            {
                try
                {
                    result = UdE.API.GetItemNameByID(UdE.ItemType.NpcOrMobOrMine, id);
                }
                catch
                {
                    result = "Unknown";
                }
            }
            else
                result = "Unknown";
        }
        else
        {
            return "Unknown";
        }
        return result;
    }
}
