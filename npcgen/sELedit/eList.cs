using System;
using System.Collections;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace sELedit;

public class eList
{
	public string listName;

	public byte[] listOffset;

	public int listType;

	public int elementSize;

	public string[] elementFields;

	public string[] elementTypes;

	public object[][] elementValues;

	public string GetValue(int ElementIndex, int FieldIndex)
	{
		if (FieldIndex > -1)
		{
			object obj = elementValues[ElementIndex][FieldIndex];
			string text = elementTypes[FieldIndex];
			switch (text)
			{
			case "int16":
				return Convert.ToString((short)obj);
			case "int32":
				return Convert.ToString((int)obj);
			case "int64":
				return Convert.ToString((long)obj);
			case "float":
				return ((float)obj).ToString("F6");
			case "double":
				return Convert.ToString((double)obj);
			}
			if (text.Contains("byte:"))
			{
				byte[] array = (byte[])obj;
				return BitConverter.ToString(array);
			}
			if (text.Contains("wstring:"))
			{
				Encoding encoding = Encoding.GetEncoding("Unicode");
				return encoding.GetString((byte[])obj).Split(default(char))[0];
			}
			if (text.Contains("string:"))
			{
				Encoding encoding2 = Encoding.GetEncoding("GBK");
				return encoding2.GetString((byte[])obj).Split(default(char))[0];
			}
		}
		return "";
	}

	public void SetValue(int ElementIndex, int FieldIndex, string Value)
	{
		string text = elementTypes[FieldIndex];
		switch (text)
		{
		case "int16":
			elementValues[ElementIndex][FieldIndex] = Convert.ToInt16(Value);
			return;
		case "int32":
			elementValues[ElementIndex][FieldIndex] = Convert.ToInt32(Value);
			return;
		case "int64":
			elementValues[ElementIndex][FieldIndex] = Convert.ToInt64(Value);
			return;
		case "float":
			elementValues[ElementIndex][FieldIndex] = Convert.ToSingle(Value);
			return;
		case "double":
			elementValues[ElementIndex][FieldIndex] = Convert.ToDouble(Value);
			return;
		}
		if (text.Contains("byte:"))
		{
			string[] array = Value.Split('-');
			byte[] array2 = new byte[Convert.ToInt32(text.Substring(5))];
			for (int i = 0; i < array.Length; i++)
			{
				array2[i] = Convert.ToByte(array[i], 16);
			}
			elementValues[ElementIndex][FieldIndex] = array2;
		}
		else if (text.Contains("wstring:"))
		{
			Encoding encoding = Encoding.GetEncoding("Unicode");
			byte[] array3 = new byte[Convert.ToInt32(text.Substring(8))];
			byte[] bytes = encoding.GetBytes(Value);
			if (array3.Length > bytes.Length)
			{
				Array.Copy(bytes, array3, bytes.Length);
			}
			else
			{
				Array.Copy(bytes, array3, array3.Length);
			}
			elementValues[ElementIndex][FieldIndex] = array3;
		}
		else if (text.Contains("string:"))
		{
			Encoding encoding2 = Encoding.GetEncoding("GBK");
			byte[] array4 = new byte[Convert.ToInt32(text.Substring(7))];
			byte[] bytes2 = encoding2.GetBytes(Value);
			if (array4.Length > bytes2.Length)
			{
				Array.Copy(bytes2, array4, bytes2.Length);
			}
			else
			{
				Array.Copy(bytes2, array4, array4.Length);
			}
			elementValues[ElementIndex][FieldIndex] = array4;
		}
	}

	public string GetType(int FieldIndex)
	{
		if (FieldIndex > -1)
		{
			return elementTypes[FieldIndex];
		}
		return "";
	}

	public void RemoveItem(int itemIndex)
	{
		object[][] array = new object[elementValues.Length - 1][];
		Array.Copy(elementValues, 0, array, 0, itemIndex);
		int num = array.Length - itemIndex;
		Array.Copy(elementValues, itemIndex + 1, array, itemIndex, array.Length - itemIndex);
		elementValues = array;
	}

	public void AddItem(object[] itemValues)
	{
		object[][] array = new object[elementValues.Length + 1][];
		Array.Resize(ref elementValues, elementValues.Length + 1);
		elementValues[elementValues.Length - 1] = itemValues;
	}

	public void ExportItem(string file, int index)
	{
		StreamWriter streamWriter = new StreamWriter(file, append: false, Encoding.Unicode);
		for (int i = 0; i < elementTypes.Length; i++)
		{
			streamWriter.WriteLine(elementFields[i] + "(" + elementTypes[i] + ")=\"" + GetValue(index, i) + "\"");
		}
		streamWriter.Close();
	}

	public void ImportItem(string file, int index)
	{
		StreamReader streamReader = new StreamReader(file, Encoding.Unicode);
		for (int i = 0; i < elementTypes.Length; i++)
		{
			string text = streamReader.ReadLine();
			if (text.StartsWith("#") || text.StartsWith("//") || !(text != ""))
			{
				continue;
			}
			string text2 = text.Substring(text.IndexOf("=") + 2);
			if (!text2.EndsWith("\""))
			{
				string text3;
				do
				{
					text3 = streamReader.ReadLine();
					text2 = text2 + "\r\n" + text3;
				}
				while (!text3.EndsWith("\""));
			}
			text2 = text2.Replace("\"", "");
			SetValue(index, i, text2);
		}
		streamReader.Close();
	}

	public ArrayList JoinElements(eList newList, int listID, bool addNew, bool backupNew, bool replaceChanged, bool backupChanged, bool removeMissing, bool backupMissing, string dirBackupNew, string dirBackupChanged, string dirBackupMissing)
	{
		object[][] array = newList.elementValues;
		string[] array2 = newList.elementTypes;
		ArrayList arrayList = new ArrayList();
		for (int i = 0; i < elementValues.Length; i++)
		{
			Application.DoEvents();
			bool flag = false;
			for (int j = 0; j < array.Length; j++)
			{
				if (GetValue(i, 0) == newList.GetValue(j, 0))
				{
					flag = true;
				}
			}
			if (!flag)
			{
				if (dirBackupMissing != null && Directory.Exists(dirBackupMissing))
				{
					ExportItem(dirBackupMissing + "\\List_" + listID + "_Item_" + GetValue(i, 0) + ".txt", i);
				}
				if (removeMissing)
				{
					arrayList.Add("- MISSING ITEM (*removed): " + (int)elementValues[i][0]);
					RemoveItem(i);
					i--;
				}
				else
				{
					arrayList.Add("- MISSING ITEM (*not removed): " + (int)elementValues[i][0]);
				}
			}
		}
		for (int k = 0; k < array.Length; k++)
		{
			Application.DoEvents();
			bool flag = false;
			for (int l = 0; l < elementValues.Length; l++)
			{
				if (!(GetValue(l, 0) == newList.GetValue(k, 0)))
				{
					continue;
				}
				flag = true;
				if (elementValues[l].Length != newList.elementValues[k].Length)
				{
					arrayList.Add("<> DIFFERENT ITEM (*not replaced, invalid amount of values): " + GetValue(l, 0));
					break;
				}
				for (int m = 0; m < elementValues[l].Length; m++)
				{
					if (GetValue(l, m) != newList.GetValue(k, m))
					{
						if (backupChanged && Directory.Exists(dirBackupChanged))
						{
							ExportItem(dirBackupChanged + "\\List_" + listID + "_Item_" + GetValue(l, 0) + ".txt", l);
						}
						if (replaceChanged)
						{
							arrayList.Add("<> DIFFERENT ITEM (*replaced): " + GetValue(l, 0));
							elementValues[l] = newList.elementValues[k];
						}
						else
						{
							arrayList.Add("<> DIFFERENT ITEM (*not replaced): " + GetValue(l, 0));
						}
						break;
					}
				}
				break;
			}
			if (!flag)
			{
				if (backupNew && Directory.Exists(dirBackupNew))
				{
					newList.ExportItem(dirBackupNew + "\\List_" + listID + "_Item_" + newList.GetValue(k, 0) + ".txt", k);
				}
				if (addNew)
				{
					AddItem(array[k]);
					arrayList.Add("+ NEW ITEM (*added): " + GetValue(elementValues.Length - 1, 0));
				}
				else
				{
					arrayList.Add("+ NEW ITEM (*not added): " + GetValue(elementValues.Length - 1, 0));
				}
			}
		}
		return arrayList;
	}
}
