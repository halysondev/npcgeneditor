using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using DevIL;
using DevIL.Unmanaged;

namespace LBLIBRARY;

public class PWHelper
{
	public class ShopIcon
	{
		public string Name;

		public System.Drawing.Image Icon;

		public ShopIcon(string d, System.Drawing.Image b)
		{
			Name = d;
			Icon = b;
		}
	}

	public class Elements
	{
		public class List
		{
			public int ItemsAmount;

			public List<Item> Items = new List<Item>();

			public string ListName { get; set; }

			public string ListType { get; set; }

			public List<string> TypesNames { get; set; }

			public List<string> Types { get; set; }
		}

		public class Item
		{
			public int Id;

			public string Name;

			public int MaxAmount;

			public string Icon;

			public List<object> Values = new List<object>();

			public System.Drawing.Image IconImage = Small;

			public System.Drawing.Image Standard_image = Big;
		}

		public List<List> ElementsLists = new List<List>();

		public List<Item> Items = new List<Item>();

		public int[] InListAmount;

		public int Version;

		public int ListsAmount;

		public int DialogsListPosition;

		public List NpcsList = new List();

		public Dictionary<int, int> NonObjectListBytesAmount;
	}

	public class Desc
	{
		public int Id;

		public string Description;

		public Desc(int i, string d)
		{
			Id = i;
			Description = d;
		}
	}

	public class Icon
	{
		public List<int> IDS;

		public string IconName;

		public Bitmap StandardImage;

		public Bitmap ResizedImage;
	}

	private static Bitmap Big;

	private static Bitmap Small;

	public PWHelper()
	{
		Small = new Bitmap(GetType().Assembly.GetManifestResourceStream("LBLIBRARY.Small.ico"));
		Big = new Bitmap(GetType().Assembly.GetManifestResourceStream("LBLIBRARY.Standard.ico"));
	}

	public static Bitmap LoadDDSImage(byte[] ByteArray)
	{
		Bitmap bitmap = null;
		IL.Initialize();
		IL.Enable(ILEnable.AbsoluteFormat);
		IL.SetDataFormat(DataFormat.BGRA);
		IL.Enable(ILEnable.AbsoluteType);
		IL.SetDataType(DataType.UnsignedByte);
		MemoryStream memoryStream = new MemoryStream(ByteArray);
		if (IL.LoadImageFromStream(memoryStream))
		{
			ImageInfo imageInfo = IL.GetImageInfo();
			bitmap = new Bitmap(imageInfo.Width, imageInfo.Height, PixelFormat.Format32bppArgb);
			Rectangle rect = new Rectangle(0, 0, imageInfo.Width, imageInfo.Height);
			BitmapData bitmapData = bitmap.LockBits(rect, ImageLockMode.WriteOnly, PixelFormat.Format32bppArgb);
			IL.CopyPixels(data: bitmapData.Scan0, xOffset: 0, yOffset: 0, zOffset: 0, width: imageInfo.Width, height: imageInfo.Height, depth: 1, format: DataFormat.BGRA, dataType: DataType.UnsignedByte);
			bitmap.UnlockBits(bitmapData);
		}
		IL.Shutdown();
		memoryStream.Close();
		return bitmap;
	}

	public static List<Icon> LoadIconList(Elements el, string Iconlist_dds, string iconlist_txt)
	{
		return LoadIconList(el, LoadDDSImage(File.ReadAllBytes(Iconlist_dds)), File.ReadAllLines(iconlist_txt, Encoding.GetEncoding(936)).ToList());
	}

	public static List<Icon> LoadIconList(Elements el, Bitmap img, List<string> Text)
	{
		List<Icon> list = new List<Icon>();
		Text.RemoveRange(0, 4);
		int num = 0;
		int num2 = 0;
		for (int i = 0; i < Text.Count; i++)
		{
			Icon icon = new Icon
			{
				IconName = Text[i],
				StandardImage = img.Clone(new Rectangle(num, num2, 32, 32), img.PixelFormat)
			};
			icon.ResizedImage = icon.StandardImage.ResizeImage(21, 21);
			list.Add(icon);
			num += 32;
			if (num == img.Width)
			{
				num2 += 32;
				num = 0;
			}
		}
		foreach (Elements.List elementsList in el.ElementsLists)
		{
			foreach (Elements.Item item in elementsList.Items)
			{
				string itn = item.Icon;
				if (item.Icon != null)
				{
					itn = itn.ToLower();
				}
				int num3 = list.FindIndex((Icon z) => z.IconName == itn);
				if (num3 != -1)
				{
					item.IconImage = list[num3].ResizedImage;
					item.Standard_image = list[num3].StandardImage;
				}
			}
		}
		return list;
	}

	public static List<Icon> LoadIconList(Elements el, string PckPath)
	{
		ArchiveEngine archiveEngine = new ArchiveEngine(PckPath);
		Bitmap img = LoadDDSImage(archiveEngine.ReadFile(archiveEngine.PckFile, archiveEngine.Files.Where((PCKFileEntry d) => d.Path == "surfaces\\iconset\\iconlist_ivtrm.dds").ElementAt(0)).ToArray());
		return LoadIconList(el, img, CreateLines(archiveEngine));
	}

	public static List<Icon> LoadIconList(Elements el, ArchiveEngine pck)
	{
		Bitmap img = LoadDDSImage(pck.ReadFile(pck.PckFile, pck.Files.Where((PCKFileEntry d) => d.Path == "surfaces\\iconset\\iconlist_ivtrm.dds").ElementAt(0)).ToArray());
		return LoadIconList(el, img, CreateLines(pck));
	}

	public static List<ShopIcon> ReadSurfacesIcons(ArchiveEngine pck)
	{
		List<ShopIcon> list = new List<ShopIcon>();
		foreach (PCKFileEntry item in pck.Files.Where((PCKFileEntry i) => (i.Path.StartsWith("surfaces\\百宝阁\\") && i.Path.Contains(".dds")) || (i.Path.StartsWith("surfaces\\竞拍品\\") && i.Path.Contains(".dds"))).ToList())
		{
			list.Add(new ShopIcon(item.Path, LoadDDSImage(pck.ReadFile(pck.PckFile, item).ToArray())));
		}
		return list;
	}

	public static Bitmap LinkImages(List<ShopIcon> Surfaces_icons)
	{
		Bitmap bitmap = new Bitmap(1152, Surfaces_icons.Count * 128 / 9 + 55);
		Graphics graphics = Graphics.FromImage(bitmap);
		int num = 0;
		int num2 = 0;
		for (int i = 0; i < Surfaces_icons.Count; i++)
		{
			graphics.DrawImage(Surfaces_icons[i].Icon, num, num2);
			num += 128;
			if (bitmap.Width == num)
			{
				num2 += 128;
				num = 0;
			}
		}
		return bitmap;
	}

	public static Elements ReadElements(string FilePath, string ApplicationStartUpPath, bool RemoveNonItemLists)
	{
		Elements elements = new Elements();
		BinaryReader binaryReader = new BinaryReader(File.Open(FilePath, FileMode.Open));
		elements.Version = binaryReader.ReadInt16();
		ReadConfigFile(ApplicationStartUpPath, elements.Version, elements);
		elements.NonObjectListBytesAmount = new Dictionary<int, int>();
		List<int> list = new List<int>
		{
			187, 185, 183, 181, 180, 179, 178, 177, 176, 175,
			174, 173, 172, 170, 169, 168, 167, 166, 165, 164,
			163, 161, 160, 159, 158, 157, 156, 155, 153, 152,
			150, 149, 148, 147, 146, 145, 144, 143, 142, 139,
			138, 137, 136, 132, 131, 129, 128, 127, 126, 120,
			111, 110, 109, 105, 104, 103, 102, 101, 100, 94,
			93, 91, 90, 88, 87, 85, 84, 82, 81, 80,
			79, 78, 77, 76, 73, 72, 71, 70, 69, 68,
			67, 66, 65, 64, 63, 62, 61, 60, 59, 58,
			57, 56, 55, 54, 53, 52, 51, 50, 49, 48,
			47, 46, 45, 44, 43, 42, 41, 40, 39, 38,
			37, 36, 34, 32, 30, 20, 18, 16, 14, 13,
			11, 10, 8, 7, 5, 4, 2, 1, 0
		};
		if (RemoveNonItemLists)
		{
			for (int i = 0; i < elements.ElementsLists.Count; i++)
			{
				if (list.Contains(i))
				{
					elements.NonObjectListBytesAmount.Add(i, elements.ElementsLists[i].Types.SumBytes());
				}
			}
		}
		for (int j = 0; j < elements.ElementsLists.Count; j++)
		{
			if (j != 58)
			{
				elements.ElementsLists[j].TypesNames.RemoveAt(0);
				elements.ElementsLists[j].Types.RemoveAt(0);
			}
		}
		binaryReader.BaseStream.Seek(2L, SeekOrigin.Current);
		if (elements.Version >= 10)
		{
			binaryReader.BaseStream.Seek(4L, SeekOrigin.Current);
		}
		ReadElementLists(binaryReader, elements);
		binaryReader.Close();
		if (RemoveNonItemLists)
		{
			elements.RemoveNonObjectList(list);
		}
		elements.Items = elements.ElementsLists.SelectMany((Elements.List z) => z.Items).ToList();
		elements.InListAmount = new int[elements.ElementsLists.Count];
		for (int k = 0; k < elements.ElementsLists.Count; k++)
		{
			List<int> list2 = elements.ElementsLists.Select((Elements.List v) => v.ItemsAmount).ToList();
			list2.RemoveRange(k + 1, elements.ElementsLists.Count - (k + 1));
			elements.InListAmount[k] = list2.Sum();
		}
		return elements;
	}

	public static List<Desc> LoadItemExtDesc(ArchiveEngine pck)
	{
		IEnumerable<PCKFileEntry> source = pck.Files.Where((PCKFileEntry i) => i.Path.StartsWith("configs\\item_ext_desc"));
		byte[] array = pck.ReadFile(pck.PckFile, source.ElementAt(0)).ToArray();
		StreamReader streamReader = new StreamReader(new MemoryStream(array), Encoding.GetEncoding(936));
		List<string> list = new List<string>();
		int num = 0;
		for (int j = 0; j < array.Count(); j++)
		{
			list.Add(streamReader.ReadLine());
			if (list[j] == null)
			{
				break;
			}
			num++;
		}
		list.RemoveAll((string v) => v == null);
		List<Desc> list2 = new List<Desc>();
		foreach (string item in list)
		{
			if (item.StartsWith("/") || item.StartsWith("#") || item.StartsWith("^") || !(item != "") || !item.Contains("\""))
			{
				continue;
			}
			string[] array2 = item.Split('"');
			if (array2.Count() > 1)
			{
				int.TryParse(array2[0], out var result);
				if (result != 0 && result != -1)
				{
					list2.Add(new Desc(result, array2[1]));
				}
			}
		}
		return list2;
	}

	private static object ReadValue(BinaryReader br, string type)
	{
		if (type.Contains("int32") || type.Contains("link") || type.Contains("combo"))
		{
			return br.ReadInt32();
		}
		if (type.Contains("float"))
		{
			return br.ReadSingle();
		}
		if (type.Contains("byte"))
		{
			return br.ReadBytes(Convert.ToInt32(type.Split(':')[1]));
		}
		if (type.Contains("wstring"))
		{
			return Encoding.Unicode.GetString(br.ReadBytes(Convert.ToInt32(type.Split(':')[1]))).ToString().Split(default(char))[0];
		}
		if (type.Contains("string"))
		{
			return Encoding.GetEncoding("GBK").GetString(br.ReadBytes(Convert.ToInt32(type.Split(':')[1]))).ToString()
				.Split(default(char))[0];
		}
		return "";
	}

	private static void ReadConfigFile(string AppStart, int vers, Elements el)
	{
		StreamReader streamReader = new StreamReader(Directory.GetFiles(AppStart + "\\configs", "PW_*_v" + vers + ".cfg")[0], Encoding.UTF8);
		el.ListsAmount = Convert.ToInt32(streamReader.ReadLine());
		el.DialogsListPosition = Convert.ToInt32(streamReader.ReadLine());
		for (int i = 0; i < el.ListsAmount; i++)
		{
			string text = "";
			while (text == "")
			{
				text = streamReader.ReadLine();
			}
			Elements.List list = new Elements.List();
			list.ListName = text;
			list.ListType = streamReader.ReadLine();
			list.TypesNames = streamReader.ReadLine().Split(new string[1] { ";" }, StringSplitOptions.RemoveEmptyEntries).ToList();
			list.Types = streamReader.ReadLine().Split(new string[1] { ";" }, StringSplitOptions.RemoveEmptyEntries).ToList();
			el.ElementsLists.Add(list);
		}
		streamReader.Close();
	}

	private static void ReadElementLists(BinaryReader br, Elements el)
	{
		for (int i = 0; i < el.ListsAmount; i++)
		{
			if (i == 20 && el.Version >= 10)
			{
				br.ReadBytes(4);
				byte[] value = br.ReadBytes(4);
				br.ReadBytes(BitConverter.ToInt32(value, 0));
				br.ReadBytes(4);
			}
			if (i == 100 && el.Version >= 10)
			{
				br.ReadBytes(4);
				byte[] value2 = br.ReadBytes(4);
				br.ReadBytes(BitConverter.ToInt32(value2, 0));
			}
			if (el.NonObjectListBytesAmount.Keys.Contains(i))
			{
				if (i == 57)
				{
					el.NpcsList.ItemsAmount = br.ReadInt32();
					for (int j = 0; j < el.NpcsList.ItemsAmount; j++)
					{
						Elements.Item item = new Elements.Item();
						item.Id = br.ReadInt32();
						item.Name = br.ReadBytes(64).ToString(Encoding.Unicode);
						br.BaseStream.Seek(el.NonObjectListBytesAmount[i] - 68, SeekOrigin.Current);
						el.NpcsList.Items.Add(item);
					}
					continue;
				}
				if (el.NonObjectListBytesAmount[i] != -1)
				{
					br.BaseStream.Seek(el.NonObjectListBytesAmount[i] * br.ReadInt32(), SeekOrigin.Current);
					continue;
				}
				byte[] bytes = Encoding.GetEncoding("GBK").GetBytes("facedata\\");
				long position = br.BaseStream.Position;
				int num = -72 - bytes.Length;
				bool flag = true;
				while (flag)
				{
					flag = false;
					for (int k = 0; k < bytes.Length; k++)
					{
						num++;
						if (br.ReadByte() != bytes[k])
						{
							flag = true;
							break;
						}
					}
				}
				br.BaseStream.Position = position;
				br.BaseStream.Seek(num, SeekOrigin.Current);
				continue;
			}
			el.ElementsLists[i].ItemsAmount = br.ReadInt32();
			el.ElementsLists[i].Items = new List<Elements.Item>();
			for (int l = 0; l < el.ElementsLists[i].ItemsAmount; l++)
			{
				Elements.Item item2 = new Elements.Item();
				item2.Id = br.ReadInt32();
				for (int m = 0; m < el.ElementsLists[i].Types.Count; m++)
				{
					if ((el.ElementsLists[i].TypesNames[m] == "Name") | (el.ElementsLists[i].TypesNames[m] == "Иконка") | (el.ElementsLists[i].TypesNames[m] == "file_icon") | (el.ElementsLists[i].TypesNames[m] == "Кол-во в ячейке") | (el.ElementsLists[i].TypesNames[m] == "pile_num_max"))
					{
						if (el.ElementsLists[i].TypesNames[m] == "Name")
						{
							item2.Name = br.ReadBytes(64).ToString(Encoding.Unicode);
						}
						else if ((el.ElementsLists[i].TypesNames[m] == "Иконка") | (el.ElementsLists[i].TypesNames[m] == "file_icon"))
						{
							item2.Icon = br.ReadBytes(128).ToString(Encoding.GetEncoding(936)).GetIconNameFromString();
						}
						else
						{
							item2.MaxAmount = br.ReadInt32();
						}
					}
					else
					{
						item2.Values.Add(ReadValue(br, el.ElementsLists[i].Types[m]));
					}
				}
				el.ElementsLists[i].Items.Add(item2);
			}
		}
	}

	private static List<string> CreateLines(ArchiveEngine pck)
	{
		byte[] array = pck.ReadFile(pck.PckFile, pck.Files.Where((PCKFileEntry i) => i.Path == "surfaces\\iconset\\iconlist_ivtrm.txt").ElementAt(0)).ToArray();
		List<string> list = new List<string>();
		StreamReader streamReader = new StreamReader(new MemoryStream(array), Encoding.GetEncoding(936));
		int num = 0;
		for (int j = 0; j < array.Count(); j++)
		{
			list.Add(streamReader.ReadLine());
			if (list[j] == null)
			{
				break;
			}
			num++;
		}
		list.RemoveAll((string v) => v == null);
		list.ForEach(delegate(string z)
		{
			z.ToLower();
		});
		return list;
	}
}
