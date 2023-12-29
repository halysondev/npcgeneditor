using System.IO;

namespace sELedit;

public class eListConversation
{
	public int talk_proc_count;

	public talk_proc[] talk_procs;

	public eListConversation(byte[] Bytes)
	{
		MemoryStream memoryStream = new MemoryStream(Bytes);
		BinaryReader binaryReader = new BinaryReader(memoryStream);
		talk_proc_count = binaryReader.ReadInt32();
		talk_procs = new talk_proc[talk_proc_count];
		for (int i = 0; i < talk_proc_count; i++)
		{
			talk_procs[i] = new talk_proc();
			talk_procs[i].id_talk = binaryReader.ReadInt32();
			talk_procs[i].text = binaryReader.ReadBytes(128);
			talk_procs[i].num_window = binaryReader.ReadInt32();
			talk_procs[i].windows = new window[talk_procs[i].num_window];
			for (int j = 0; j < talk_procs[i].num_window; j++)
			{
				talk_procs[i].windows[j] = new window();
				talk_procs[i].windows[j].id = binaryReader.ReadInt32();
				talk_procs[i].windows[j].id_parent = binaryReader.ReadInt32();
				talk_procs[i].windows[j].talk_text_len = binaryReader.ReadInt32();
				talk_procs[i].windows[j].talk_text = binaryReader.ReadBytes(2 * talk_procs[i].windows[j].talk_text_len);
				talk_procs[i].windows[j].num_option = binaryReader.ReadInt32();
				talk_procs[i].windows[j].options = new option[talk_procs[i].windows[j].num_option];
				for (int k = 0; k < talk_procs[i].windows[j].num_option; k++)
				{
					talk_procs[i].windows[j].options[k] = new option();
					talk_procs[i].windows[j].options[k].param = binaryReader.ReadInt32();
					talk_procs[i].windows[j].options[k].text = binaryReader.ReadBytes(128);
					talk_procs[i].windows[j].options[k].id = binaryReader.ReadInt32();
				}
			}
		}
		binaryReader.Close();
		memoryStream.Close();
	}

	public byte[] GetBytes()
	{
		MemoryStream memoryStream = new MemoryStream(talk_proc_count);
		BinaryWriter binaryWriter = new BinaryWriter(memoryStream);
		binaryWriter.Write(talk_proc_count);
		for (int i = 0; i < talk_proc_count; i++)
		{
			binaryWriter.Write(talk_procs[i].id_talk);
			binaryWriter.Write(talk_procs[i].text);
			binaryWriter.Write(talk_procs[i].num_window);
			for (int j = 0; j < talk_procs[i].num_window; j++)
			{
				binaryWriter.Write(talk_procs[i].windows[j].id);
				binaryWriter.Write(talk_procs[i].windows[j].id_parent);
				binaryWriter.Write(talk_procs[i].windows[j].talk_text_len);
				binaryWriter.Write(talk_procs[i].windows[j].talk_text);
				binaryWriter.Write(talk_procs[i].windows[j].num_option);
				for (int k = 0; k < talk_procs[i].windows[j].num_option; k++)
				{
					binaryWriter.Write(talk_procs[i].windows[j].options[k].param);
					binaryWriter.Write(talk_procs[i].windows[j].options[k].text);
					binaryWriter.Write(talk_procs[i].windows[j].options[k].id);
				}
			}
		}
		byte[] result = memoryStream.ToArray();
		binaryWriter.Close();
		memoryStream.Close();
		return result;
	}
}
