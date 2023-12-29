using System;
using System.Collections;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace sELedit;

public class eListCollection
{
	public short Version;

	public short Signature;

	public int ConversationListIndex;

	public string ConfigFile;

	public eList[] Lists;

	public eListCollection(string elFile)
	{
		Lists = Load(elFile);
	}

	public void RemoveItem(int ListIndex, int ElementIndex)
	{
		Lists[ListIndex].RemoveItem(ElementIndex);
	}

	public void AddItem(int ListIndex, object[] ItemValues)
	{
		Lists[ListIndex].AddItem(ItemValues);
	}

	public string GetOffset(int ListIndex)
	{
		return BitConverter.ToString(Lists[ListIndex].listOffset);
	}

	public void SetOffset(int ListIndex, string Offset)
	{
		if (Offset != "")
		{
			string[] array = Offset.Split('-');
			Lists[ListIndex].listOffset = new byte[array.Length];
			for (int i = 0; i < array.Length; i++)
			{
				Lists[ListIndex].listOffset[i] = Convert.ToByte(array[i], 16);
			}
		}
		else
		{
			Lists[ListIndex].listOffset = new byte[0];
		}
	}

	public string GetValue(int ListIndex, int ElementIndex, int FieldIndex)
	{
		return Lists[ListIndex].GetValue(ElementIndex, FieldIndex);
	}

	public void SetValue(int ListIndex, int ElementIndex, int FieldIndex, string Value)
	{
		Lists[ListIndex].SetValue(ElementIndex, FieldIndex, Value);
	}

	public string GetType(int ListIndex, int FieldIndex)
	{
		return Lists[ListIndex].GetType(FieldIndex);
	}

	private object readValue(BinaryReader br, string type)
	{
		switch (type)
		{
		case "int16":
			return br.ReadInt16();
		case "int32":
			return br.ReadInt32();
		case "int64":
			return br.ReadInt64();
		case "float":
			return br.ReadSingle();
		case "double":
			return br.ReadDouble();
		default:
			if (type.Contains("byte:"))
			{
				return br.ReadBytes(Convert.ToInt32(type.Substring(5)));
			}
			if (type.Contains("wstring:"))
			{
				return br.ReadBytes(Convert.ToInt32(type.Substring(8)));
			}
			if (type.Contains("string:"))
			{
				return br.ReadBytes(Convert.ToInt32(type.Substring(7)));
			}
			return null;
		}
	}

	private void writeValue(BinaryWriter bw, object value, string type)
	{
		switch (type)
		{
		case "int16":
			bw.Write((short)value);
			return;
		case "int32":
			bw.Write((int)value);
			return;
		case "int64":
			bw.Write((long)value);
			return;
		case "float":
			bw.Write((float)value);
			return;
		case "double":
			bw.Write((double)value);
			return;
		}
		if (type.Contains("byte:"))
		{
			bw.Write((byte[])value);
		}
		else if (type.Contains("wstring:"))
		{
			bw.Write((byte[])value);
		}
		else if (type.Contains("string:"))
		{
			bw.Write((byte[])value);
		}
	}

	private eList[] loadConfiguration(string file)
	{
		StreamReader streamReader = new StreamReader(file);
		eList[] array = new eList[Convert.ToInt32(streamReader.ReadLine())];
		try
		{
			ConversationListIndex = Convert.ToInt32(streamReader.ReadLine());
		}
		catch
		{
			ConversationListIndex = 58;
		}
		for (int i = 0; i < array.Length; i++)
		{
			Application.DoEvents();
			string listName;
			while ((listName = streamReader.ReadLine()) == "")
			{
			}
			array[i] = new eList();
			array[i].listName = listName;
			array[i].listOffset = null;
			string text = streamReader.ReadLine();
			if (text != "AUTO")
			{
				array[i].listOffset = new byte[Convert.ToInt32(text)];
			}
			array[i].elementFields = streamReader.ReadLine().Split(';');
			array[i].elementTypes = streamReader.ReadLine().Split(';');
		}
		streamReader.Close();
		return array;
	}

	private Hashtable loadRules(string file)
	{
		StreamReader streamReader = new StreamReader(file);
		Hashtable hashtable = new Hashtable();
		string text = "";
		string text2 = "";
		while (!streamReader.EndOfStream)
		{
			string text3 = streamReader.ReadLine();
			Application.DoEvents();
			if (text3 != "" && !text3.StartsWith("#"))
			{
				if (text3.Contains("|"))
				{
					text = text3.Split('|')[0];
					text2 = text3.Split('|')[1];
				}
				else
				{
					text = text3;
					text2 = "";
				}
				hashtable.Add(text, text2);
			}
		}
		streamReader.Close();
		if (!hashtable.ContainsKey("SETCONVERSATIONLISTINDEX"))
		{
			hashtable.Add("SETCONVERSATIONLISTINDEX", 58);
		}
		return hashtable;
	}

	public eList[] Load(string elFile)
	{
		eList[] array = new eList[0];
		FileStream fileStream = File.OpenRead(elFile);
		BinaryReader binaryReader = new BinaryReader(fileStream);
		Version = binaryReader.ReadInt16();
		Signature = binaryReader.ReadInt16();
		string[] files = Directory.GetFiles(Application.StartupPath + "\\configs", "PW_*_v" + Version + ".cfg");
		if (files.Length != 0)
		{
			ConfigFile = files[0];
			array = loadConfiguration(ConfigFile);
			for (int i = 0; i < array.Length; i++)
			{
				Application.DoEvents();
				if (array[i].listOffset != null)
				{
					if (array[i].listOffset.Length != 0)
					{
						array[i].listOffset = binaryReader.ReadBytes(array[i].listOffset.Length);
					}
				}
				else
				{
					if (i == 0)
					{
						byte[] array2 = binaryReader.ReadBytes(4);
						byte[] array3 = binaryReader.ReadBytes(4);
						byte[] array4 = binaryReader.ReadBytes(BitConverter.ToInt32(array3, 0));
						array[i].listOffset = new byte[array2.Length + array3.Length + array4.Length];
						Array.Copy(array2, 0, array[i].listOffset, 0, array2.Length);
						Array.Copy(array3, 0, array[i].listOffset, 4, array3.Length);
						Array.Copy(array4, 0, array[i].listOffset, 8, array4.Length);
					}
					if (i == 20)
					{
						byte[] array5 = binaryReader.ReadBytes(4);
						byte[] array6 = binaryReader.ReadBytes(4);
						byte[] array7 = binaryReader.ReadBytes(BitConverter.ToInt32(array6, 0));
						byte[] array8 = binaryReader.ReadBytes(4);
						array[i].listOffset = new byte[array5.Length + array6.Length + array7.Length + array8.Length];
						Array.Copy(array5, 0, array[i].listOffset, 0, array5.Length);
						Array.Copy(array6, 0, array[i].listOffset, 4, array6.Length);
						Array.Copy(array7, 0, array[i].listOffset, 8, array7.Length);
						Array.Copy(array8, 0, array[i].listOffset, 8 + array7.Length, array8.Length);
					}
					int num = 100;
					if (Version >= 191)
					{
						num = 99;
					}
					if (i == num)
					{
						byte[] array9 = binaryReader.ReadBytes(4);
						byte[] array10 = binaryReader.ReadBytes(4);
						byte[] array11 = binaryReader.ReadBytes(BitConverter.ToInt32(array10, 0));
						array[i].listOffset = new byte[array9.Length + array10.Length + array11.Length];
						Array.Copy(array9, 0, array[i].listOffset, 0, array9.Length);
						Array.Copy(array10, 0, array[i].listOffset, 4, array10.Length);
						Array.Copy(array11, 0, array[i].listOffset, 8, array11.Length);
					}
				}
				if (i == ConversationListIndex)
				{
					if (Version >= 191)
					{
						long position = binaryReader.BaseStream.Position;
						int num2 = 0;
						bool flag = true;
						while (flag)
						{
							flag = false;
							try
							{
								binaryReader.ReadByte();
								num2++;
								flag = true;
							}
							catch
							{
							}
						}
						binaryReader.BaseStream.Position = position;
						array[i].elementTypes[0] = "byte:" + num2;
					}
					else if (array[i].elementTypes[0].Contains("AUTO"))
					{
						byte[] bytes = Encoding.GetEncoding("GBK").GetBytes("facedata\\");
						long position2 = binaryReader.BaseStream.Position;
						int num3 = -72 - bytes.Length;
						bool flag2 = true;
						while (flag2)
						{
							flag2 = false;
							for (int j = 0; j < bytes.Length; j++)
							{
								num3++;
								if (binaryReader.ReadByte() != bytes[j])
								{
									flag2 = true;
									break;
								}
							}
						}
						binaryReader.BaseStream.Position = position2;
						array[i].elementTypes[0] = "byte:" + num3;
					}
					array[i].elementValues = new object[1][];
					array[i].elementValues[0] = new object[array[i].elementTypes.Length];
					array[i].elementValues[0][0] = readValue(binaryReader, array[i].elementTypes[0]);
					continue;
				}
				if (Version >= 191)
				{
					array[i].listType = binaryReader.ReadInt32();
				}
				array[i].elementValues = new object[binaryReader.ReadInt32()][];
				if (Version >= 191)
				{
					array[i].elementSize = binaryReader.ReadInt32();
				}
				for (int k = 0; k < array[i].elementValues.Length; k++)
				{
					array[i].elementValues[k] = new object[array[i].elementTypes.Length];
					for (int l = 0; l < array[i].elementValues[k].Length; l++)
					{
						array[i].elementValues[k][l] = readValue(binaryReader, array[i].elementTypes[l]);
					}
				}
			}
		}
		else
		{
			MessageBox.Show("No corressponding configuration file found!\nVersion: " + Version + "\nPattern: configs\\PW_*_v" + Version + ".cfg");
		}
		binaryReader.Close();
		fileStream.Close();
		return array;
	}

	public void Save(string elFile)
	{
		if (File.Exists(elFile))
		{
			File.Delete(elFile);
		}
		FileStream fileStream = new FileStream(elFile, FileMode.Create, FileAccess.Write);
		BinaryWriter binaryWriter = new BinaryWriter(fileStream);
		binaryWriter.Write(Version);
		binaryWriter.Write(Signature);
		for (int i = 0; i < Lists.Length; i++)
		{
			Application.DoEvents();
			if (Lists[i].listOffset.Length != 0)
			{
				binaryWriter.Write(Lists[i].listOffset);
			}
			if (i != ConversationListIndex)
			{
				if (Version >= 191)
				{
					binaryWriter.Write(Lists[i].listType);
				}
				binaryWriter.Write(Lists[i].elementValues.Length);
				if (Version >= 191)
				{
					binaryWriter.Write(Lists[i].elementSize);
				}
			}
			for (int j = 0; j < Lists[i].elementValues.Length; j++)
			{
				for (int k = 0; k < Lists[i].elementValues[j].Length; k++)
				{
					writeValue(binaryWriter, Lists[i].elementValues[j][k], Lists[i].elementTypes[k]);
				}
			}
		}
		binaryWriter.Close();
		fileStream.Close();
	}

	public void Export(string RulesFile, string TargetFile)
	{
		Hashtable hashtable = loadRules(RulesFile);
		if (File.Exists(TargetFile))
		{
			File.Delete(TargetFile);
		}
		FileStream fileStream = new FileStream(TargetFile, FileMode.Create, FileAccess.Write);
		BinaryWriter binaryWriter = new BinaryWriter(fileStream);
		if (hashtable.ContainsKey("SETVERSION"))
		{
			binaryWriter.Write(Convert.ToInt16((string)hashtable["SETVERSION"]));
			if (hashtable.ContainsKey("SETSIGNATURE"))
			{
				binaryWriter.Write(Convert.ToInt16((string)hashtable["SETSIGNATURE"]));
				for (int i = 0; i < Lists.Length; i++)
				{
					if (Convert.ToInt16((string)hashtable["SETCONVERSATIONLISTINDEX"]) == i)
					{
						for (int j = 0; j < Lists[ConversationListIndex].elementValues.Length; j++)
						{
							for (int k = 0; k < Lists[ConversationListIndex].elementValues[j].Length; k++)
							{
								Application.DoEvents();
								writeValue(binaryWriter, Lists[ConversationListIndex].elementValues[j][k], Lists[ConversationListIndex].elementTypes[k]);
							}
						}
					}
					if (i == ConversationListIndex || hashtable.ContainsKey("REMOVELIST:" + i))
					{
						continue;
					}
					if (hashtable.ContainsKey("REPLACEOFFSET:" + i))
					{
						string[] array = ((string)hashtable["REPLACEOFFSET:" + i]).Split('-', ' ');
						if (array.Length > 1)
						{
							byte[] array2 = new byte[array.Length];
							for (int l = 0; l < array.Length; l++)
							{
								array2[l] = Convert.ToByte(array[l], 16);
							}
							if (array2.Length != 0)
							{
								binaryWriter.Write(array2);
							}
						}
					}
					else if (Lists[i].listOffset.Length != 0)
					{
						binaryWriter.Write(Lists[i].listOffset);
					}
					if (Convert.ToInt16((string)hashtable["SETVERSION"]) >= 191)
					{
						binaryWriter.Write(Lists[i].listType);
					}
					binaryWriter.Write(Lists[i].elementValues.Length);
					if (Convert.ToInt16((string)hashtable["SETVERSION"]) >= 191)
					{
						binaryWriter.Write(Lists[i].elementSize);
					}
					for (int m = 0; m < Lists[i].elementValues.Length; m++)
					{
						for (int n = 0; n < Lists[i].elementValues[m].Length; n++)
						{
							Application.DoEvents();
							if (!hashtable.ContainsKey("REMOVEVALUE:" + i + ":" + n))
							{
								writeValue(binaryWriter, Lists[i].elementValues[m][n], Lists[i].elementTypes[n]);
							}
						}
					}
				}
				binaryWriter.Close();
				fileStream.Close();
			}
			else
			{
				MessageBox.Show("Rules file is missing parameter\n\nSETSIGNATURE:", "Export Failed");
				binaryWriter.Close();
				fileStream.Close();
			}
		}
		else
		{
			MessageBox.Show("Rules file is missing parameter\n\nSETVERSION:", "Export Failed");
			binaryWriter.Close();
			fileStream.Close();
		}
	}
}
