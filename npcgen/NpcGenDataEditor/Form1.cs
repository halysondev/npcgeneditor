using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using System.Xml;
using LBLIBRARY;
using NpcGenDataEditor.Properties;

namespace NpcGenDataEditor;

public class Form1 : Form
{
	public List<offsets> Offsets = new List<offsets>();

	private List<int> SearchMonsters;

	private List<int> SearchResources;

	private List<int> SearchDynamics;

	private List<int> SearchTriggers;

	private NpcGen Read;

	private Elementsdata Element;

	private ArchiveEngine PckFile;

	private ShowLocationWindow MapForm;

	private MobsNpcsForm ChooseFromElementsForm;

	private DynamicObjectsForm DynamicForm;

	private List<ClassDefaultMonsters> MonstersContact;

	private List<ClassDefaultResources> ResourcesContact;

	private List<ClassDynamicObject> DynamicsContact;

	private List<DefaultInformation> DynamicsListRu;

	private List<DefaultInformation> DynamicsListEn;

	private List<MapLoadedInformation> LoadedMapConfigs;

	private List<GameMapInfo> Maps;

	private List<int> NpcRowCollection;

	private List<int> UnderNpcRowCollection;

	private List<int> ResourcesRowCollection;

	private List<int> UnderResourcesRowCollection;

	private List<int> DynamicsRowCollection;

	private List<int> TriggersRowCollection;

	private List<IntDictionary> ErrorExistenceCollection = new List<IntDictionary>();

	private List<IntDictionary> ErrorResourcesCollection = new List<IntDictionary>();

	private List<IntDictionary> ErrorDynamicsCollection = new List<IntDictionary>();

	private Keys MonstersDefaultKey;

	private Keys MonstersExtraKey;

	private Keys ResourcesDefaultKey;

	private Keys ResourcesExtraKey;

	private Keys DynamicsDefaultKey;

	private Keys DynamicsExtraKey;

	private int NpcRowIndex;

	private int NpcGroupIndex;

	private int ResourcesRowIndex;

	private int ResourcesGroupIndex;

	private int DynamicRowIndex;

	private int TriggersRowIndex;

	private int Action;

	private int Language = 1;

	private int InterfaceColor = 1;

	private bool AllowCellChanging = true;

	private int ErrorsLanguage;

	private IContainer components = null;

	private Label label1;

	private TextBox Elements_textbox;

	private Label label2;

	private TextBox Npcgen_textbox;

	private Button Search_element;

	private Button Search_Npcgen;

	private Button OpenFiles;

	private Button SaveFile;

	private ProgressBar MainProgressBar;

	private TabControl MainTabControl;

	private TabPage ResourcesTab;

	private TabPage ExistenceTab;

	private TabPage DynObjectsTab;

	private TabPage TriggersTab;

	private Button ExistenceCloneButton;

	private Button ExistenceRemoveButton;

	private GroupBox MainGroupBox;

	private OpenFileDialog Element_dialog;

	private OpenFileDialog Npcgen_dialog;

	private ComboBox ExistenceLocating;

	private Label label3;

	private Label label4;

	private TextBox Surfaces_path;

	private Button Search_surfaces;

	private Button Open_surfaces;

	private TextBox Group_amount_textbox;

	private Label label5;

	private GroupBox groupBox1;

	private TextBox Z_scatter;

	private Label label12;

	private TextBox Y_scatter;

	private Label label13;

	private TextBox X_scatter;

	private Label label14;

	private TextBox Z_rotate;

	private Label label11;

	private TextBox Y_rotate;

	private Label label10;

	private TextBox X_rotate;

	private TextBox Z_position;

	private TextBox Y_position;

	private Label label9;

	private TextBox X_position;

	private Label label8;

	private Label label7;

	private Label label6;

	private ComboBox ExistenceType;

	private Label label15;

	private TextBox Group_type;

	private Label label16;

	private CheckBox ExistenceInitGen;

	private CheckBox ExistenceAutoRevive;

	private CheckBox BValicOnce;

	private TextBox Trigger;

	private Label label18;

	private TextBox dwGenId;

	private Label label17;

	private TextBox IMaxNuml;

	private Label label20;

	private TextBox Life_time;

	private Label label19;

	private Button ExistenceGroupRemoveButton;

	private Button ExistenceGroupCloneButton;

	private DataGridView NpcsGroupGrid;

	private NumericUpDown Id_numeric;

	private Label label21;

	private NumericUpDown Amount_numeric;

	private Label label22;

	private NumericUpDown Respawn_numeric;

	private Label label23;

	private NumericUpDown DeathAmount_numeric;

	private Label label24;

	private ComboBox Agression;

	private Label label25;

	private ComboBox Path_type;

	private Label label26;

	private Label label28;

	private NumericUpDown Path_numeric;

	private Label label27;

	private NumericUpDown Path_speed;

	private Label label29;

	private NumericUpDown Water_numeric;

	private NumericUpDown Turn_numeric;

	private Label label30;

	private NumericUpDown AskHelp_numeric;

	private Label label31;

	private NumericUpDown NeedHelp_numeric;

	private Label label32;

	private CheckBox bFac_Accept;

	private CheckBox bFac_Helper;

	private CheckBox bFaction;

	private CheckBox bNeedHelp;

	private Label label33;

	private NumericUpDown Deadtime_numeric;

	private Label label34;

	private NumericUpDown RefreshLower_numeric;

	private NumericUpDown Group_numeric;

	private Label label35;

	private OpenFileDialog Surfaces_search;

	private ComboBox Maps_combobox;

	private SaveFileDialog Npcgen_save_dialog;

	private DataGridView ResourcesGrid;

	private Button ResourcesRemoveButton;

	private Button ResourcesCloneButton;

	private DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;

	private DataGridViewTextBoxColumn dataGridViewTextBoxColumn4;

	private DataGridViewTextBoxColumn dataGridViewTextBoxColumn5;

	private Button ButtonShowMap;

	private ProgressBar MapProgress;

	private GroupBox groupBox3;

	private TextBox RIMaxNuml;

	private Label label36;

	private TextBox RTriggerID;

	private Label label37;

	private TextBox RdwGenID;

	private Label label38;

	private CheckBox RBValidOnce;

	private CheckBox ResourcesAutoRevive;

	private CheckBox ResourcesInitGen;

	private TextBox RRotation;

	private Label label41;

	private TextBox RZ_Random;

	private Label label42;

	private TextBox RX_Random;

	private Label label43;

	private TextBox RInCline2;

	private Label label44;

	private TextBox RInCline1;

	private Label label45;

	private TextBox RZ_position;

	private TextBox RY_position;

	private TextBox RX_position;

	private Label label47;

	private Label label48;

	private Label label49;

	private TextBox RGroup_amount_textbox;

	private Label label51;

	private GroupBox groupBox5;

	private Label label53;

	private NumericUpDown RfHeiOff_numeric;

	private Label label55;

	private NumericUpDown RRespawn_numeric;

	private Label label58;

	private NumericUpDown RAmount_numeric;

	private Label label59;

	private NumericUpDown RId_numeric;

	private Label label60;

	private Button ResourcesGroupRemoveButton;

	private Button ResourcesGroupCloneButton;

	private DataGridView ResourcesGroupGrid;

	private DataGridViewTextBoxColumn dataGridViewTextBoxColumn9;

	private DataGridViewTextBoxColumn dataGridViewTextBoxColumn10;

	private DataGridViewTextBoxColumn dataGridViewTextBoxColumn11;

	private Label label39;

	private DataGridViewTextBoxColumn dataGridViewTextBoxColumn6;

	private DataGridViewTextBoxColumn dataGridViewTextBoxColumn7;

	private DataGridViewTextBoxColumn dataGridViewTextBoxColumn8;

	private NumericUpDown RType_numeric;

	private ComboBox Version_combobox;

	private Label label52;

	private TabPage OptionsTab;

	private GroupBox groupBox6;

	private ComboBox ExtraMobButton_combobox;

	private Label label46;

	private ComboBox DefaultMobButton_combobox;

	private Label label50;

	private ToolTip toolTip1;

	private Button DynObjectsRemoveButton;

	private Button DynObjectsCloneButton;

	private DataGridView DynamicGrid;

	private GroupBox groupBox7;

	private Button DynObjectsInsertCordsFromGame;

	private GroupBox groupBox10;

	private NumericUpDown AddDynamicsID;

	private Label label61;

	private TextBox DZ_position;

	private TextBox DY_position;

	private TextBox DX_position;

	private Label label80;

	private Label label81;

	private Label label82;

	private TextBox DRotation;

	private Label label70;

	private TextBox DIncline2;

	private Label label71;

	private TextBox DIncline1;

	private Label label72;

	private Label label69;

	private TextBox DTrigger_id;

	private Label label73;

	private TextBox DScale;

	private Label label74;

	private NumericUpDown AddDynamicsTrigger;

	private Label label40;

	private GroupBox groupBox9;

	private PictureBox DynamicPictureBox;

	private Button ResourcesInsertCordsFromGame;

	private GroupBox groupBox11;

	private NumericUpDown AddResourceRespawnTime;

	private Label label54;

	private NumericUpDown AddResourceAmount;

	private Label label56;

	private NumericUpDown AddResourceID;

	private Label label57;

	private NumericUpDown AddResourcesTrigger;

	private Label label75;

	private OpenFileDialog Dynamics_dialog;

	private Label Label_DynamicName;

	private TextBox DId_numeric;

	private Button ConvertAndSaveButton;

	private Button InformationButton;

	private ComboBox ExtraResourceButton_combobox;

	private Label label62;

	private ComboBox DefaultResourceButton_combobox;

	private Label label77;

	private DataGridView TriggersGrid;

	private Button TriggersRemoveButton;

	private Button TriggersCloneButton;

	private DataGridView MUTrigger;

	private DataGridViewTextBoxColumn dataGridViewTextBoxColumn28;

	private DataGridViewTextBoxColumn dataGridViewTextBoxColumn29;

	private DataGridViewTextBoxColumn dataGridViewTextBoxColumn30;

	private GroupBox groupBox12;

	private TextBox TId_textbox;

	private Label label78;

	private Label label89;

	private TextBox TName_textbox;

	private TextBox TGmId_textbox;

	private Button GotoNpcMobsContacts;

	private CheckBox TStartBySchedule;

	private CheckBox TStopBySchedule;

	private TextBox TDuration;

	private Label label85;

	private TextBox TWaitStop_textbox;

	private Label label84;

	private TextBox TWaitStart_textbox;

	private CheckBox TAutoStart;

	private Label label79;

	private Label label83;

	private GroupBox groupBox15;

	private DataGridView DUTrigger;

	private DataGridViewTextBoxColumn dataGridViewTextBoxColumn25;

	private DataGridViewTextBoxColumn dataGridViewTextBoxColumn26;

	private DataGridViewTextBoxColumn dataGridViewTextBoxColumn27;

	private Button GotoDynamicsContacts;

	private GroupBox groupBox14;

	private DataGridView RUTrigger;

	private DataGridViewTextBoxColumn dataGridViewTextBoxColumn22;

	private DataGridViewTextBoxColumn dataGridViewTextBoxColumn23;

	private DataGridViewTextBoxColumn dataGridViewTextBoxColumn24;

	private Button GotoResourcesContacts;

	private GroupBox groupBox13;

	private GroupBox groupBox17;

	private TextBox TStopMinute;

	private Label label93;

	private TextBox TStopHour;

	private Label label94;

	private TextBox TStopDay;

	private Label label95;

	private ComboBox TStopWeekDay;

	private TextBox TStopMonth;

	private Label label96;

	private TextBox TStopYear;

	private Label label97;

	private Label label98;

	private GroupBox groupBox16;

	private TextBox TStartMinute;

	private Label label92;

	private TextBox TStartHour;

	private Label label91;

	private TextBox TStartDay;

	private Label label90;

	private ComboBox TStartWeekDay;

	private TextBox TStartMonth;

	private Label label87;

	private TextBox TStartYear;

	private Label label86;

	private Label label88;

	private Label label99;

	private DataGridViewTextBoxColumn dataGridViewTextBoxColumn15;

	private DataGridViewTextBoxColumn dataGridViewTextBoxColumn16;

	private DataGridViewTextBoxColumn dataGridViewTextBoxColumn21;

	private DataGridViewTextBoxColumn dataGridViewTextBoxColumn17;

	private ComboBox ExtraDynamicsButton_combobox;

	private Label label100;

	private ComboBox DefaultDynamicsButton_combobox;

	private Label label101;

	private RadioButton English;

	private RadioButton Russian;

	private ContextMenuStrip TriggersContext;

	private ToolStripMenuItem DeleteEmptyTrigger;

	private ComboBox ConvertComboboxVersion;

	private ContextMenuStrip ExistenceContext;

	private ToolStripMenuItem toolStripMenuItem1;

	private ToolStripMenuItem UpExistence;

	private ToolStripMenuItem DownExistence;

	private ToolStripMenuItem toolStripMenuItem4;

	private ToolStripMenuItem UpTrigger;

	private ToolStripMenuItem DownTrigger;

	private TabPage SearchTab;

	private ToolStripMenuItem LineUpX;

	private ToolStripMenuItem LineUpZ;

	private ToolStripSeparator toolStripSeparator1;

	private GroupBox groupBox18;

	private TextBox ExistenceSearchName;

	private Label label102;

	private RadioButton ExistenceSearchId_Radio;

	private TextBox ExistenceSearchId;

	private Label label76;

	private RadioButton ExistenceSearchName_Radio;

	private TextBox ExistenceSearchTrigger;

	private RadioButton ExistenceSearchTrigger_Radio;

	private GroupBox groupBox21;

	private TextBox TriggerSearchName;

	private RadioButton TriggerSearchName_Radio;

	private TextBox TriggerSearchGmID;

	private Label label107;

	private RadioButton TriggerSearchId_Radio;

	private TextBox TriggerSearchID;

	private Label label108;

	private RadioButton TriggerSearchGmId_Radio;

	private GroupBox groupBox20;

	private TextBox DynamicSearchTrigger;

	private RadioButton DynamicSearchTrigger_Radio;

	private TextBox DynamicSearchName;

	private Label label105;

	private RadioButton DynamicSearchId_Radio;

	private TextBox DynamicSearchId;

	private Label label106;

	private RadioButton DynamicSearchName_Radio;

	private GroupBox groupBox19;

	private TextBox ResourceSearchTrigger;

	private RadioButton ResourceSearchTrigger_Radio;

	private TextBox ResourceSearchName;

	private Label label103;

	private RadioButton ResourceSearchId_Radio;

	private TextBox ResourceSearchId;

	private Label label104;

	private RadioButton ResourceSearchName_Radio;

	private Button ExistenceSearchButton;

	private TextBox ExistenceSearchPath;

	private RadioButton ExistenceSearchPath_Radio;

	private Button TriggerSearchButton;

	private Button DynamicSearchButton;

	private Button ResourceSearchButton;

	private DataGridView SearchGrid;

	private ContextMenuStrip contextMenuStrip1;

	private ToolStripMenuItem MoveToSelected;

	private DataGridViewTextBoxColumn dataGridViewTextBoxColumn18;

	private DataGridViewTextBoxColumn dataGridViewTextBoxColumn19;

	private DataGridViewTextBoxColumn dataGridViewTextBoxColumn20;

	private DataGridViewTextBoxColumn Column2;

	private ContextMenuStrip contextMenuStrip2;

	private ToolStripMenuItem MoveToTrigger;

	private TabPage ErrorsTab;

	private Button SearchErrorsButton;

	private DataGridView ErrorsGrid;

	private DataGridViewTextBoxColumn Column3;

	private DataGridViewTextBoxColumn dataGridViewTextBoxColumn31;

	private DataGridViewTextBoxColumn dataGridViewTextBoxColumn32;

	private DataGridViewTextBoxColumn dataGridViewTextBoxColumn33;

	private DataGridView NpcMobsGrid;

	private DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;

	private DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;

	private DataGridViewTextBoxColumn Column4;

	private Button RemoveAllErrors;

	private GroupBox groupBox23;

	private RadioButton Clear;

	private RadioButton Dark;

	private GroupBox groupBox22;

	private Button ExistenceInsertCordsFromGame;

	private GroupBox groupBox8;

	private NumericUpDown AddMonsterRespawnTime;

	private Label label63;

	private NumericUpDown AddMonsterAmount;

	private Label label64;

	private NumericUpDown AddMonsterId;

	private Label label65;

	private ComboBox AddMonsterType;

	private Label label66;

	private ComboBox AddintExistenceType;

	private NumericUpDown AddMonsterTrigger;

	private Label label67;

	private Label label68;

	private ToolStrip ExistenceToolStrip;

	private ToolStripButton ExportExistence;

	private ToolStripButton ImportExistence;

	private ToolStripDropDownButton LineUpExistenceDropDown;

	private ToolStripMenuItem ToolStripLineUpX;

	private ToolStripMenuItem ToolStripLineUpZ;

	private ToolStripDropDownButton MoveExistenceDropDown;

	private ToolStripMenuItem MoveUpToolStripMenuItem;

	private ToolStripMenuItem MoveDownToolStripMenuItem;

	private ToolStrip toolStrip1;

	private ToolStripButton ExportResources;

	private ToolStripButton ImportResources;

	private ToolStripDropDownButton LineUpResource;

	private ToolStripMenuItem ResourcesOnX;

	private ToolStripMenuItem ResourcesOnZ;

	private ToolStripDropDownButton MoveResources;

	private ToolStripMenuItem ResourceUp;

	private ToolStripMenuItem ResourceDown;

	private ToolStrip toolStrip2;

	private ToolStripButton toolStripButton3;

	private ToolStripButton toolStripButton4;

	private ToolStripDropDownButton toolStripDropDownButton3;

	private ToolStripMenuItem toolStripMenuItem7;

	private ToolStripMenuItem toolStripMenuItem8;

	private ToolStripDropDownButton toolStripDropDownButton4;

	private ToolStripMenuItem toolStripMenuItem9;

	private ToolStripMenuItem toolStripMenuItem10;

	private ToolStrip toolStrip3;

	private ToolStripButton toolStripButton5;

	private ToolStripButton toolStripButton6;

	private ToolStripDropDownButton toolStripDropDownButton6;

	private ToolStripMenuItem toolStripMenuItem13;

	private ToolStripMenuItem toolStripMenuItem14;

	private ToolStripButton toolStripButton7;

	private ToolStripSeparator toolStripSeparator2;

	private ToolStripMenuItem экспортToolStripMenuItem;

	private ToolStripMenuItem импортToolStripMenuItem;

	private ToolStripSeparator toolStripSeparator3;

	private ToolStripMenuItem toolStripMenuItem12;

	private ToolStripMenuItem toolStripMenuItem11;

	private DataGridViewTextBoxColumn dataGridViewTextBoxColumn12;

	private DataGridViewTextBoxColumn dataGridViewTextBoxColumn13;

	private DataGridViewTextBoxColumn dataGridViewTextBoxColumn14;

	private DataGridViewTextBoxColumn Column1;

	private ToolStripSeparator toolStripSeparator4;

	private ToolStripMenuItem вНачалоToolStripMenuItem;

	private ToolStripMenuItem ExistenceToEnd;

	[DllImport("kernel32.dll")]
	public static extern IntPtr OpenProcess(int dwDesiredAccess, bool bInheritHandle, int dwProcessId);

	[DllImport("Kernel32.dll")]
	private static extern bool ReadProcessMemory(IntPtr hProcess, IntPtr lpBaseAddress, byte[] lpBuffer, int nSize, int lpNumberOfBytesRead);

	public Form1()
	{
		InitializeComponent();
		try
		{
			KBDHook.LocalHook = false;
			KBDHook.KeyDown += KBDHook_KeyDown;
			KBDHook.InstallHook();
		}
		catch
		{
		}
		StreamReader streamReader = new StreamReader(Path.Combine(Application.StartupPath, "offsets.txt"));
		while (!streamReader.EndOfStream)
		{
			string text = streamReader.ReadLine();
			if (text.StartsWith("Name"))
			{
				Version_combobox.Items.Add(text.Replace("Name=", ""));
				string[] array = streamReader.ReadLine().Split(' ');
				Offsets.Add(new offsets
				{
					baseChain = new long[3]
					{
						long.Parse(array[0], NumberStyles.HexNumber),
                        long.Parse(array[1], NumberStyles.HexNumber),
                        long.Parse(array[2], NumberStyles.HexNumber)
					},
					dirX = int.Parse(streamReader.ReadLine(), NumberStyles.HexNumber),
					dirY = int.Parse(streamReader.ReadLine(), NumberStyles.HexNumber),
					dirZ = int.Parse(streamReader.ReadLine(), NumberStyles.HexNumber),
					posX = int.Parse(streamReader.ReadLine(), NumberStyles.HexNumber),
					posY = int.Parse(streamReader.ReadLine(), NumberStyles.HexNumber),
					posZ = int.Parse(streamReader.ReadLine(), NumberStyles.HexNumber)
				});
			}
		}
	}

	private void KBDHook_KeyDown(LLKHEventArgs e)
	{
		ClassPosition classPosition = null;
		if ((e.Keys == MonstersExtraKey && Control.ModifierKeys == MonstersDefaultKey) || (e.Keys == ResourcesExtraKey && Control.ModifierKeys == ResourcesDefaultKey) || (e.Keys == DynamicsExtraKey && Control.ModifierKeys == DynamicsDefaultKey))
		{
			classPosition = GetCoordinates();
		}
		if (classPosition == null)
		{
			return;
		}
		if (e.Keys == MonstersExtraKey && Control.ModifierKeys == MonstersDefaultKey)
		{
			Read.NpcMobsAmount++;
			ClassDefaultMonsters dm3 = new ClassDefaultMonsters
			{
				X_position = classPosition.PosX,
				Y_position = classPosition.PosY,
				Z_position = classPosition.PosZ,
				X_direction = classPosition.DirX,
				Y_direction = classPosition.DirY,
				Z_direction = classPosition.DirZ,
				Amount_in_group = 1,
				Location = AddintExistenceType.SelectedIndex,
				Type = AddMonsterType.SelectedIndex,
				Trigger_id = Convert.ToInt32(AddMonsterTrigger.Value)
			};
			ClassExtraMonsters item = new ClassExtraMonsters
			{
				Id = Convert.ToInt32(AddMonsterId.Value),
				Amount = Convert.ToInt32(AddMonsterAmount.Value),
				Respawn = Convert.ToInt32(AddMonsterRespawnTime.Value)
			};
			dm3.MobDops = new List<ClassExtraMonsters> { item };
			Read.NpcMobList.Add(dm3);
			string text = "?";
			int num = Element.ExistenceLists.FindIndex((NpcMonster z) => z.Id == dm3.MobDops[0].Id);
			if (num != -1)
			{
				text = Element.ExistenceLists[num].Name;
			}
			NpcMobsGrid.Rows.Add(Read.NpcMobsAmount, dm3.MobDops[0].Id, text);
			NpcMobsGrid.CurrentCell = NpcMobsGrid.Rows[NpcMobsGrid.Rows.Count - 1].Cells[1];
		}
		if (e.Keys == ResourcesExtraKey && Control.ModifierKeys == ResourcesDefaultKey)
		{
			Read.ResourcesAmount++;
			ClassDefaultResources dm2 = new ClassDefaultResources
			{
				X_position = classPosition.PosX,
				Y_position = classPosition.PosY,
				Z_position = classPosition.PosZ,
				Amount_in_group = 1,
				Trigger_id = Convert.ToInt32(AddResourcesTrigger.Value)
			};
			ClassExtraResources item2 = new ClassExtraResources
			{
				Id = Convert.ToInt32(AddResourceID.Value),
				Amount = Convert.ToInt32(AddResourceAmount.Value),
				Respawntime = Convert.ToInt32(AddResourceRespawnTime.Value)
			};
			dm2.ResExtra = new List<ClassExtraResources> { item2 };
			Read.ResourcesList.Add(dm2);
			string text2 = "?";
			int num2 = Element.ResourcesList.FindIndex((NpcMonster z) => z.Id == dm2.ResExtra[0].Id);
			if (num2 != -1)
			{
				text2 = Element.ResourcesList[num2].Name;
			}
			ResourcesGrid.Rows.Add(Read.NpcMobsAmount, dm2.ResExtra[0].Id, text2);
			ResourcesGrid.CurrentCell = ResourcesGrid.Rows[ResourcesGrid.Rows.Count - 1].Cells[1];
		}
		if (e.Keys != DynamicsExtraKey || Control.ModifierKeys != DynamicsDefaultKey)
		{
			return;
		}
		ClassDynamicObject dm = new ClassDynamicObject
		{
			Id = Convert.ToInt32(AddDynamicsTrigger.Value),
			TriggerId = Convert.ToInt32(AddDynamicsID.Value),
			X_position = classPosition.PosX,
			Y_position = classPosition.PosY,
			Z_position = classPosition.PosZ
		};
		Read.DynobjectAmount++;
		Read.DynamicsList.Add(dm);
		string text3 = "?";
		if (Language == 1)
		{
			int num3 = DynamicsListRu.FindIndex((DefaultInformation z) => z.Id == dm.Id);
			if (num3 != -1)
			{
				text3 = DynamicsListRu[num3].Name;
			}
		}
		else if (Language == 2)
		{
			int num4 = DynamicsListEn.FindIndex((DefaultInformation z) => z.Id == dm.Id);
			if (num4 != -1)
			{
				text3 = DynamicsListEn[num4].Name;
			}
		}
		DynamicGrid.Rows.Add(Read.DynobjectAmount, dm.Id, text3, dm.TriggerId);
		DynamicGrid.CurrentCell = DynamicGrid.Rows[DynamicGrid.Rows.Count - 1].Cells[1];
	}

	public ClassPosition GetCoordinates()
	{
		Process[] processesByName = Process.GetProcessesByName("elementclient_64");
		if (processesByName.Length == 0)
		{
			if (Language == 1)
			{
				MessageBox.Show("Клиент игры не запущен!!...", "Npcgen Editor", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
			}
			else if (Language == 2)
			{
				MessageBox.Show("Game isn't running!!...", "Npcgen Editor", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
			}
			return null;
		}
		offsets offsets2 = Offsets[Version_combobox.SelectedIndex];
		VAMemory vAMemory = new VAMemory("elementclient_64");
		long num = 0;
		for (int i = 0; i < 3; i++)
		{
			long num2 = num + offsets2.baseChain[i];
			num = vAMemory.ReadInt64((IntPtr)num2);
		}
		float num3 = vAMemory.ReadFloat((IntPtr)num + offsets2.posX);
		float num4 = vAMemory.ReadFloat((IntPtr)num + offsets2.posY);
		float z = vAMemory.ReadFloat((IntPtr)num + offsets2.posZ);
		float dX = vAMemory.ReadFloat((IntPtr)num + offsets2.dirX);
		float dY = vAMemory.ReadFloat((IntPtr)num + offsets2.dirY);
		float dZ = vAMemory.ReadFloat((IntPtr)num + offsets2.dirZ);
		return new ClassPosition(num3, num4, z, dX, dY, dZ);
	}

	private List<PointF> GetPoint(int Action)
	{
		List<PointF> list = new List<PointF>();
		switch (Action)
		{
		case 1:
			foreach (int item in NpcRowCollection)
			{
				list.Add(new PointF(Read.NpcMobList[item].X_position, Read.NpcMobList[item].Z_position));
			}
			break;
		case 2:
			foreach (int item2 in ResourcesRowCollection)
			{
				list.Add(new PointF(Read.ResourcesList[item2].X_position, Read.ResourcesList[item2].Z_position));
			}
			break;
		case 3:
			foreach (int item3 in DynamicsRowCollection)
			{
				list.Add(new PointF(Read.DynamicsList[item3].X_position, Read.DynamicsList[item3].Z_position));
			}
			break;
		}
		return list;
	}

	private void LinkMaps(List<PCKFileEntry> l, string MapName)
	{
		int num = 256;
		Bitmap bm = null;
		int num2 = LoadedMapConfigs.FindIndex((MapLoadedInformation z) => z.Name == MapName);
		if (num2 != -1)
		{
			if (l.Count == 88)
			{
				num = 1024;
			}
			bm = new Bitmap(LoadedMapConfigs[num2].Width, LoadedMapConfigs[num2].Height);
			int num3 = 0;
			int num4 = 0;
			Graphics graphics = Graphics.FromImage(bm);
			MapProgress.BeginInvoke((MethodInvoker)delegate
			{
				MapProgress.Maximum = l.Count;
				MapProgress.Value = 0;
			});
			l = l.OrderBy((PCKFileEntry t) => t.Path).ToList();
			for (int i = 0; i < l.Count; i++)
			{
				graphics.DrawImage(PWHelper.LoadDDSImage(PckFile.ReadFile(PckFile.PckFile, l[i]).ToArray()), new Point(num3, num4));
				num3 += num;
				if (num3 == bm.Width)
				{
					num4 += num;
					num3 = 0;
				}
				MapProgress.BeginInvoke((MethodInvoker)delegate
				{
					MapProgress.Value++;
				});
			}
			BeginInvoke((MethodInvoker)delegate
			{
				MapProgress.Value = 0;
				if (MapForm == null)
				{
					MapForm = new ShowLocationWindow(this, bm);
					MapForm.Show(this);
				}
				else if (!MapForm.Visible)
				{
					MapForm = new ShowLocationWindow(this, bm);
					MapForm.Show(this);
				}
				else
				{
					MapForm.SetPicture(bm);
				}
			});
		}
		else if (Language == 1)
		{
			MessageBox.Show("Настройки карты не найдены в Maps.conf", "Npcgen Editor", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
		}
		else
		{
			MessageBox.Show("Map options haven't been found in Maps.conf", "Npcgen Editor", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
		}
	}

	public void SetId(int Id, int act, int Window)
	{
		if (act == 1 && Window == 1)
		{
			Id_numeric.Value = Id;
			UnderNpcAndMobs_Leave(Id_numeric, null);
		}
		else if (act == 1 && Window == 2)
		{
			ExistenceSearchId.Text = Id.ToString();
			ExistenceSearchId_TextChanged(null, null);
		}
		else if (act == 2 && Window == 1)
		{
			RId_numeric.Value = Id;
			UnderResourcesLeave(RId_numeric, null);
		}
		else if (act == 2 && Window == 2)
		{
			ResourceSearchId.Text = Id.ToString();
			ResourceSearchId_TextChanged(null, null);
		}
		else if (act == 3 && Window == 1)
		{
			DId_numeric.Text = Id.ToString();
			DynamicsLeave(DId_numeric, null);
		}
		else if (act == 3 && Window == 2)
		{
			DynamicSearchId.Text = Id.ToString();
			DynamicSearchId_TextChanged(null, null);
		}
	}

	private void DefaultMobButton_combobox_SelectedIndexChanged(object sender, EventArgs e)
	{
		Control control = sender as Control;
		KeysConverter keysConverter = new KeysConverter();
		switch (control.Name)
		{
		case "DefaultMobButton_combobox":
			MonstersDefaultKey = (Keys)keysConverter.ConvertFromString(DefaultMobButton_combobox.SelectedItem.ToString());
			break;
		case "ExtraMobButton_combobox":
			MonstersExtraKey = (Keys)keysConverter.ConvertFromString(ExtraMobButton_combobox.SelectedItem.ToString());
			break;
		case "DefaultResourceButton_combobox":
			ResourcesDefaultKey = (Keys)keysConverter.ConvertFromString(DefaultResourceButton_combobox.SelectedItem.ToString());
			break;
		case "ExtraResourceButton_combobox":
			ResourcesExtraKey = (Keys)keysConverter.ConvertFromString(ExtraResourceButton_combobox.SelectedItem.ToString());
			break;
		case "DefaultDynamicsButton_combobox":
			DynamicsDefaultKey = (Keys)keysConverter.ConvertFromString(DefaultDynamicsButton_combobox.SelectedItem.ToString());
			break;
		case "ExtraDynamicsButton_combobox":
			DynamicsExtraKey = (Keys)keysConverter.ConvertFromString(ExtraDynamicsButton_combobox.SelectedItem.ToString());
			break;
		}
	}

	private void SearchElementButton(object sender, EventArgs e)
	{
		if (Element_dialog.ShowDialog() == DialogResult.OK)
		{
			Elements_textbox.Text = Element_dialog.FileName;
		}
	}

	private void SearchNpcgenButton(object sender, EventArgs e)
	{
		if (Npcgen_dialog.ShowDialog() == DialogResult.OK)
		{
			Npcgen_textbox.Text = Npcgen_dialog.FileName;
		}
	}

	private void SearchSurfacesButton(object sender, EventArgs e)
	{
		if (Surfaces_search.ShowDialog() == DialogResult.OK)
		{
			Surfaces_path.Text = Surfaces_search.FileName;
		}
	}

	private void OpenElementAndNpcgen(object sender, EventArgs e)
	{
		if (File.Exists(Npcgen_textbox.Text) && File.Exists(Elements_textbox.Text))
		{
			Read = new NpcGen();
			BinaryReader binaryReader = new BinaryReader(File.Open(Npcgen_textbox.Text, FileMode.Open));
			Read.ReadNpcgen(binaryReader);
			binaryReader.Close();
			Element = new Elementsdata(Elements_textbox.Text);
			Text = Npcgen_textbox.Text + "  -  Version " + Read.File_version + "  -  Npcgen Editor By Luka v1.7.4 and Updated by Haly";
			NpcMobsGrid.ScrollBars = ScrollBars.None;
			ResourcesGrid.ScrollBars = ScrollBars.None;
			DynamicGrid.ScrollBars = ScrollBars.None;
			TriggersGrid.ScrollBars = ScrollBars.None;
			new Thread((ThreadStart)delegate
			{
				ChooseFromElementsForm = new MobsNpcsForm(this, Element.ExistenceLists, Element.ResourcesList, Element.MonsterdAmount, Element.NpcsAmount);
				ChooseFromElementsForm.RefreshLanguage(Language);
			}).Start();
			NpcMobsGrid.Rows.Clear();
			ResourcesGrid.Rows.Clear();
			DynamicGrid.Rows.Clear();
			TriggersGrid.Rows.Clear();
			ErrorsGrid.Rows.Clear();
			MainProgressBar.Maximum = Read.NpcMobsAmount + Read.ResourcesAmount + Read.DynobjectAmount + Read.TriggersAmount;
			SortNpcGen();
			SortDynamicObjects();
			SortTriggers();
			NpcMobsGrid.ScrollBars = ScrollBars.Vertical;
			ResourcesGrid.ScrollBars = ScrollBars.Vertical;
			DynamicGrid.ScrollBars = ScrollBars.Vertical;
			TriggersGrid.ScrollBars = ScrollBars.Vertical;
			if (Language == 1)
			{
				ExistenceTab.Text = $"Мобы и Нипы 1/{Read.NpcMobsAmount}";
				ResourcesTab.Text = $"Ресурсы 1/{Read.ResourcesAmount}";
				DynObjectsTab.Text = $"Динамические Объекты 1/{Read.DynobjectAmount}";
				TriggersTab.Text = $"Тригеры 1/{Read.TriggersAmount}";
			}
			else
			{
				ExistenceTab.Text = $"Mobs and Npcs 1/{Read.NpcMobsAmount}";
				ResourcesTab.Text = $"Resources 1/{Read.ResourcesAmount}";
				DynObjectsTab.Text = $"Dynamic Objects 1/{Read.DynobjectAmount}";
				TriggersTab.Text = $"Triggers 1/{Read.TriggersAmount}";
			}
			if (Read.File_version <= 6)
			{
				if (Language == 1)
				{
					MessageBox.Show("Обратите внимание,в этой версии триггеры не были доступны,но их можно редактировать для конвертирования в другую версию!!...", "Npcgen Editor", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
				}
				else if (Language == 2)
				{
					MessageBox.Show("Make attention,triggers didn't exist in this file version,but you can edit them for converting to another version!!...", "Npcgen Editor", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
				}
			}
			string text = "Проверить файл на ошибки?";
			if (Language == 2)
			{
				text = "Do you want to check file on errors?";
			}
			DialogResult dialogResult = MessageBox.Show(text, "Npcgen Editor", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
			if (dialogResult == DialogResult.Yes)
			{
				SearchErrorsButton_Click(null, null);
				MainTabControl.SelectedIndex = 5;
			}
		}
		else if (Language == 1)
		{
			MessageBox.Show("Файл не существует!!...", "Npcgen Editor", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
		}
		else if (Language == 2)
		{
			MessageBox.Show("File doesn't exist!!...", "Npcgen Editor", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
		}
	}

	public void SortTriggers()
	{
		for (int i = 0; i < Read.TriggersAmount; i++)
		{
			TriggersGrid.Rows.Add(i + 1, Read.TriggersList[i].Id, Read.TriggersList[i].GmID, Read.TriggersList[i].TriggerName);
		}
		if (TriggersGrid.Rows.Count != 0)
		{
			TriggersGrid.CurrentCell = TriggersGrid.Rows[0].Cells[1];
		}
		TriggersGrid_CurrentCellChanged(null, null);
	}

	public void SortNpcGen()
	{
		for (int k = 0; k < Read.NpcMobsAmount; k++)
		{
			int[] Id_joined2 = new int[Read.NpcMobList[k].Amount_in_group];
			string[] array = new string[Read.NpcMobList[k].Amount_in_group];
			int j;
			for (j = 0; j < Read.NpcMobList[k].Amount_in_group; j++)
			{
				Id_joined2[j] = Read.NpcMobList[k].MobDops[j].Id;
				int num = Element.ExistenceLists.FindIndex((NpcMonster e) => e.Id == Id_joined2[j]);
				if (num != -1)
				{
					array[j] = Element.ExistenceLists[num].Name;
				}
				else
				{
					array[j] = "?";
				}
			}
			NpcMobsGrid.Rows.Add(k + 1, string.Join(",", Id_joined2), string.Join(",", array));
			if (Read.NpcMobList[k].Type == 1)
			{
				NpcMobsGrid.Rows[k].Cells[1].Style.ForeColor = Color.FromArgb(251, 251, 107);
				NpcMobsGrid.Rows[k].Cells[2].Style.ForeColor = Color.FromArgb(251, 251, 107);
			}
			MainProgressBar.Value++;
		}
		ExistenceGrid_CellChanged(null, null);
		for (int l = 0; l < Read.ResourcesAmount; l++)
		{
			int[] Id_joined = new int[Read.ResourcesList[l].Amount_in_group];
			string[] array2 = new string[Read.ResourcesList[l].Amount_in_group];
			int i;
			for (i = 0; i < Read.ResourcesList[l].Amount_in_group; i++)
			{
				Id_joined[i] = Read.ResourcesList[l].ResExtra[i].Id;
				int num2 = Element.ResourcesList.FindIndex((NpcMonster e) => e.Id == Id_joined[i]);
				if (num2 != -1)
				{
					array2[i] = Element.ResourcesList[num2].Name;
				}
				else
				{
					array2[i] = "?";
				}
			}
			ResourcesGrid.Rows.Add(l + 1, string.Join(",", Id_joined), string.Join(",", array2));
			MainProgressBar.Value++;
		}
		MainProgressBar.Value = 0;
		if (ResourcesGrid.Rows.Count != 0)
		{
			ResourcesGrid.CurrentCell = ResourcesGrid.Rows[0].Cells[1];
		}
		if (ResourcesGroupGrid.Rows.Count != 0)
		{
			ResourcesGroupGrid.CurrentCell = ResourcesGroupGrid.Rows[0].Cells[1];
		}
	}

	public void SortDynamicObjects()
	{
		for (int i = 0; i < Read.DynobjectAmount; i++)
		{
			DynamicGrid.Rows.Add(i + 1, Read.DynamicsList[i].Id, GetDynamicName(Read.DynamicsList[i].Id), Read.DynamicsList[i].TriggerId);
		}
		if (DynamicGrid.Rows.Count != 0)
		{
			DynamicGrid.CurrentCell = DynamicGrid.Rows[0].Cells[1];
			DynamicGrid_CurrentCellChanged(null, null);
		}
	}

	public string GetDynamicName(int Id)
	{
		string result = "?";
		if (Language == 1)
		{
			int num = DynamicsListRu.FindIndex((DefaultInformation z) => z.Id == Id);
			if (num != -1)
			{
				result = DynamicsListRu[num].Name;
			}
		}
		else if (Language == 2)
		{
			int num2 = DynamicsListEn.FindIndex((DefaultInformation z) => z.Id == Id);
			if (num2 != -1)
			{
				result = DynamicsListEn[num2].Name;
			}
		}
		return result;
	}

	private void Open_surfaces_Click(object sender, EventArgs e)
	{
		if (File.Exists(Surfaces_path.Text))
		{
			try
			{
				PckFile = new ArchiveEngine(Surfaces_path.Text);
				PckFile.ReadFileTable(PckFile.PckFile);
				Maps = new List<GameMapInfo>();
				List<PCKFileEntry> list = PckFile.Files.Where((PCKFileEntry z) => z.Path.Contains("minimaps") && !z.Path.Contains("surfaces\\minimaps\\world")).ToList();
				List<string> list2 = new List<string>();
				foreach (PCKFileEntry item in list)
				{
					GameMapInfo map = new GameMapInfo();
					List<string> st = item.Path.Split('\\').ToList();
					map.MapName = st.ElementAt(st.Count - 2);
					st.RemoveAt(st.Count - 1);
					map.MapPath = string.Join("\\", st);
					if (list2.FindIndex((string v) => v == string.Join("\\", st)) == -1)
					{
						map.MapFragments = (from z in list.Where((PCKFileEntry z) => z.Path.Contains(map.MapPath)).ToList()
							orderby z.Path
							select z).ToList();
						Maps.Add(map);
						list2.Add(map.MapPath);
					}
				}
				if (Maps.Count != 0)
				{
					GameMapInfo map2 = new GameMapInfo
					{
						MapName = "World",
						MapPath = "surfaces\\maps\\"
					};
					map2.MapFragments = PckFile.Files.Where((PCKFileEntry z) => z.Path.Contains(map2.MapPath)).ToList();
					Maps.Add(map2);
					Maps_combobox.Items.Clear();
					for (int i = 0; i < Maps.Count; i++)
					{
						Maps_combobox.Items.Add(Maps[i].MapName);
					}
					Maps_combobox.SelectedIndex = Maps_combobox.Items.Count - 1;
				}
				return;
			}
			catch
			{
				if (Language == 1)
				{
					MessageBox.Show("Загружен неверный файл!!...", "Npcgen Editor", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
				}
				else if (Language == 2)
				{
					MessageBox.Show("Loaded wrong file!!...", "Npcgen Editor", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
				}
				PckFile = null;
				Maps_combobox.Items.Clear();
				Maps = null;
				MapForm = null;
				return;
			}
		}
		if (Language == 1)
		{
			MessageBox.Show("Указанный surfaces.pck не существует!!...", "Npcgen Editor", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
		}
		else if (Language == 2)
		{
			MessageBox.Show("Selected surfaces.pck doesn't exist!!...", "Npcgen Editor", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
		}
	}

	private void SaveFile_Click(object sender, EventArgs e)
	{
		if (File.Exists(Npcgen_textbox.Text))
		{
			BinaryWriter binaryWriter = new BinaryWriter(File.Create(Npcgen_textbox.Text));
			Read.WriteNpcgen(binaryWriter, Read.File_version);
			binaryWriter.Close();
			if (Language == 1)
			{
				MessageBox.Show("Файл успешно сохранен!!...", "Npcgen Editor", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
			}
			else if (Language == 2)
			{
				MessageBox.Show("File has been successfully saved!!...", "Npcgen Editor", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
			}
		}
		else if (Language == 1)
		{
			MessageBox.Show("Файл не существует!!...", "Npcgen Editor", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
		}
		else if (Language == 2)
		{
			MessageBox.Show("File doesn't exist!!...", "Npcgen Editor", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
		}
	}

	private void ConvertAndSaveButton_Click(object sender, EventArgs e)
	{
		if (Read != null && Npcgen_save_dialog.ShowDialog() == DialogResult.OK)
		{
			BinaryWriter binaryWriter = new BinaryWriter(File.Create(Npcgen_save_dialog.FileName));
			Read.WriteNpcgen(binaryWriter, Convert.ToInt32(ConvertComboboxVersion.SelectedItem));
			binaryWriter.Close();
			if (Language == 1)
			{
				MessageBox.Show("Файл успешно сохранен!!", "Npcgen Editor", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
			}
			else if (Language == 2)
			{
				MessageBox.Show("File has been successfully saved!!", "Npcgen Editor", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
			}
		}
	}

	private void ShowOnMapsButton_Click(object sender, EventArgs e)
	{
		if (Maps != null)
		{
			int SelectedIndex = Maps_combobox.SelectedIndex;
			Thread thread = new Thread((ThreadStart)delegate
			{
				LinkMaps(Maps[SelectedIndex].MapFragments, Maps[SelectedIndex].MapName);
			});
			thread.Start();
		}
	}

	private void Form1_FormClosed(object sender, FormClosedEventArgs e)
	{
		XmlTextWriter xmlTextWriter = new XmlTextWriter(string.Format(Application.StartupPath + "\\Npcgen Editor Settings.xml"), Encoding.UTF8)
		{
			Formatting = Formatting.Indented,
			IndentChar = '\t',
			Indentation = 1,
			QuoteChar = '\''
		};
		xmlTextWriter.WriteStartDocument();
		xmlTextWriter.WriteStartElement("root");
		xmlTextWriter.WriteStartAttribute("ProductName");
		xmlTextWriter.WriteString("NpcGen Editor By Luka and Updated by Haly");
		xmlTextWriter.WriteEndAttribute();
		xmlTextWriter.WriteStartElement("Settings");
		xmlTextWriter.WriteElementString("Version", "1.5");
		xmlTextWriter.WriteElementString("Language", Language.ToString());
		xmlTextWriter.WriteElementString("Interface_Color", InterfaceColor.ToString());
		if (!string.IsNullOrWhiteSpace(Elements_textbox.Text))
		{
			xmlTextWriter.WriteElementString("ElementPath", Elements_textbox.Text);
		}
		else
		{
			xmlTextWriter.WriteElementString("ElementPath", "Not Loaded");
		}
		if (!string.IsNullOrWhiteSpace(Npcgen_textbox.Text))
		{
			xmlTextWriter.WriteElementString("NpcgenPath", Npcgen_textbox.Text);
		}
		else
		{
			xmlTextWriter.WriteElementString("NpcgenPath", "Not Loaded");
		}
		if (!string.IsNullOrWhiteSpace(Surfaces_path.Text))
		{
			xmlTextWriter.WriteElementString("SurfacesPath", Surfaces_path.Text);
		}
		else
		{
			xmlTextWriter.WriteElementString("SurfacesPath", "Not Loaded");
		}
		if (DefaultMobButton_combobox.SelectedIndex == -1)
		{
			DefaultMobButton_combobox.SelectedIndex = 0;
		}
		if (ExtraMobButton_combobox.SelectedIndex == -1)
		{
			ExtraMobButton_combobox.SelectedIndex = 0;
		}
		if (DefaultResourceButton_combobox.SelectedIndex == -1)
		{
			DefaultResourceButton_combobox.SelectedIndex = 0;
		}
		if (ExtraResourceButton_combobox.SelectedIndex == -1)
		{
			ExtraResourceButton_combobox.SelectedIndex = 0;
		}
		if (DefaultDynamicsButton_combobox.SelectedIndex == -1)
		{
			DefaultDynamicsButton_combobox.SelectedIndex = 0;
		}
		if (ExtraDynamicsButton_combobox.SelectedIndex == -1)
		{
			ExtraDynamicsButton_combobox.SelectedIndex = 0;
		}
		xmlTextWriter.WriteElementString("ClientVersion", Version_combobox.SelectedIndex.ToString());
		xmlTextWriter.WriteElementString("MobsOrNpcsHotKey", $"{DefaultMobButton_combobox.SelectedIndex}+{ExtraMobButton_combobox.SelectedIndex}");
		xmlTextWriter.WriteElementString("ResourcesHotKey", $"{DefaultResourceButton_combobox.SelectedIndex}+{ExtraResourceButton_combobox.SelectedIndex}");
		xmlTextWriter.WriteElementString("DynamicsHotKey", $"{DefaultDynamicsButton_combobox.SelectedIndex}+{ExtraDynamicsButton_combobox.SelectedIndex}");
		xmlTextWriter.WriteEndElement();
		xmlTextWriter.WriteStartElement("Existence_properties");
		xmlTextWriter.WriteElementString("Existence_ID", AddMonsterId.Value.ToString());
		xmlTextWriter.WriteElementString("Existence_Amount", AddMonsterAmount.Value.ToString());
		xmlTextWriter.WriteElementString("Existence_RespawnTime", AddMonsterRespawnTime.Value.ToString());
		xmlTextWriter.WriteElementString("Existence_Trigger", AddMonsterTrigger.Value.ToString());
		xmlTextWriter.WriteElementString("Existence_Location", AddintExistenceType.SelectedIndex.ToString());
		xmlTextWriter.WriteElementString("Existence_Type", AddMonsterType.SelectedIndex.ToString());
		xmlTextWriter.WriteEndElement();
		xmlTextWriter.WriteStartElement("Resources_properties");
		xmlTextWriter.WriteElementString("Resources_ID", AddResourceID.Value.ToString());
		xmlTextWriter.WriteElementString("Resources_Amount", AddResourceAmount.Value.ToString());
		xmlTextWriter.WriteElementString("Resources_RespawnTime", AddResourceRespawnTime.Value.ToString());
		xmlTextWriter.WriteElementString("Resources_Trigger", AddResourcesTrigger.Value.ToString());
		xmlTextWriter.WriteEndElement();
		xmlTextWriter.WriteStartElement("Dynamics_properties");
		xmlTextWriter.WriteElementString("Dynamics_Trigger", AddDynamicsID.Value.ToString());
		xmlTextWriter.WriteElementString("Dynamics_ID", AddDynamicsTrigger.Value.ToString());
		xmlTextWriter.WriteEndElement();
		xmlTextWriter.WriteEndDocument();
		xmlTextWriter.Close();
	}

	private void Form1_Load(object sender, EventArgs e)
	{
		DynamicsListRu = new List<DefaultInformation>();
		DynamicsListEn = new List<DefaultInformation>();
		try
		{
			if (File.Exists(Application.StartupPath + "\\DynObjectInfo.RU"))
			{
				StreamReader streamReader = new StreamReader(Application.StartupPath + "\\DynObjectInfo.RU");
				do
				{
					string[] array = streamReader.ReadLine().Split(new string[1] { "->" }, StringSplitOptions.None);
					DefaultInformation item = new DefaultInformation
					{
						Id = Convert.ToInt32(array[0]),
						Name = array[1]
					};
					DynamicsListRu.Add(item);
				}
				while (!streamReader.EndOfStream);
			}
			if (File.Exists(Application.StartupPath + "\\DynObjectInfo.EN"))
			{
				StreamReader streamReader2 = new StreamReader(Application.StartupPath + "\\DynObjectInfo.EN");
				do
				{
					string[] array2 = streamReader2.ReadLine().Split(new string[1] { "->" }, StringSplitOptions.None);
					DefaultInformation item2 = new DefaultInformation
					{
						Id = Convert.ToInt32(array2[0]),
						Name = array2[1]
					};
					DynamicsListEn.Add(item2);
				}
				while (!streamReader2.EndOfStream);
			}
		}
		catch
		{
		}
		ConvertComboboxVersion.SelectedIndex = 0;
		try
		{
			if (File.Exists(Application.StartupPath + "\\Npcgen Editor Settings.xml"))
			{
				using XmlTextReader xmlTextReader = new XmlTextReader(string.Format(Application.StartupPath + "\\Npcgen Editor Settings.xml"));
				xmlTextReader.ReadToFollowing("Language");
				int num = Convert.ToInt32(xmlTextReader.ReadElementContentAsString());
				if (num == 2)
				{
					English.Checked = true;
					ChangeLanguage(English, null);
				}
				xmlTextReader.ReadToFollowing("Interface_Color");
				int num2 = Convert.ToInt32(xmlTextReader.ReadElementContentAsString());
				if (num2 == 2)
				{
					Dark.Checked = true;
					InterfaceColorChanged(Dark, null);
				}
				xmlTextReader.ReadToFollowing("ElementPath");
				Elements_textbox.Text = xmlTextReader.ReadElementContentAsString();
				xmlTextReader.ReadToFollowing("NpcgenPath");
				Npcgen_textbox.Text = xmlTextReader.ReadElementContentAsString();
				xmlTextReader.ReadToFollowing("SurfacesPath");
				Surfaces_path.Text = xmlTextReader.ReadElementContentAsString();
				xmlTextReader.ReadToFollowing("ClientVersion");
				Version_combobox.SelectedIndex = Convert.ToInt32(xmlTextReader.ReadElementContentAsString());
				xmlTextReader.ReadToFollowing("MobsOrNpcsHotKey");
				string[] array3 = xmlTextReader.ReadElementContentAsString().Split('+');
				DefaultMobButton_combobox.SelectedIndex = Convert.ToInt32(array3[0]);
				ExtraMobButton_combobox.SelectedIndex = Convert.ToInt32(array3[1]);
				xmlTextReader.ReadToFollowing("ResourcesHotKey");
				string[] array4 = xmlTextReader.ReadElementContentAsString().Split('+');
				DefaultResourceButton_combobox.SelectedIndex = Convert.ToInt32(array4[0]);
				ExtraResourceButton_combobox.SelectedIndex = Convert.ToInt32(array4[1]);
				xmlTextReader.ReadToFollowing("DynamicsHotKey");
				string[] array5 = xmlTextReader.ReadElementContentAsString().Split('+');
				DefaultDynamicsButton_combobox.SelectedIndex = Convert.ToInt32(array5[0]);
				ExtraDynamicsButton_combobox.SelectedIndex = Convert.ToInt32(array5[1]);
				DefaultMobButton_combobox_SelectedIndexChanged(DefaultMobButton_combobox, null);
				DefaultMobButton_combobox_SelectedIndexChanged(ExtraMobButton_combobox, null);
				DefaultMobButton_combobox_SelectedIndexChanged(DefaultResourceButton_combobox, null);
				DefaultMobButton_combobox_SelectedIndexChanged(ExtraResourceButton_combobox, null);
				DefaultMobButton_combobox_SelectedIndexChanged(DefaultDynamicsButton_combobox, null);
				DefaultMobButton_combobox_SelectedIndexChanged(ExtraDynamicsButton_combobox, null);
				xmlTextReader.ReadToFollowing("Existence_ID");
				AddMonsterId.Value = Convert.ToDecimal(xmlTextReader.ReadElementContentAsString());
				xmlTextReader.ReadToFollowing("Existence_Amount");
				AddMonsterAmount.Value = Convert.ToDecimal(xmlTextReader.ReadElementContentAsString());
				xmlTextReader.ReadToFollowing("Existence_RespawnTime");
				AddMonsterRespawnTime.Value = Convert.ToDecimal(xmlTextReader.ReadElementContentAsString());
				xmlTextReader.ReadToFollowing("Existence_Trigger");
				AddMonsterTrigger.Value = Convert.ToDecimal(xmlTextReader.ReadElementContentAsString());
				xmlTextReader.ReadToFollowing("Existence_Location");
				int num3 = Convert.ToInt32(xmlTextReader.ReadElementContentAsString());
				if (num3 < 0)
				{
					num3 = 0;
				}
				AddintExistenceType.SelectedIndex = num3;
				xmlTextReader.ReadToFollowing("Existence_Type");
				int num4 = Convert.ToInt32(xmlTextReader.ReadElementContentAsString());
				if (num4 < 0)
				{
					num4 = 0;
				}
				AddMonsterType.SelectedIndex = num4;
				xmlTextReader.ReadToFollowing("Resources_ID");
				AddResourceID.Value = Convert.ToDecimal(xmlTextReader.ReadElementContentAsString());
				xmlTextReader.ReadToFollowing("Resources_Amount");
				AddResourceAmount.Value = Convert.ToDecimal(xmlTextReader.ReadElementContentAsString());
				xmlTextReader.ReadToFollowing("Resources_RespawnTime");
				AddResourceRespawnTime.Value = Convert.ToDecimal(xmlTextReader.ReadElementContentAsString());
				xmlTextReader.ReadToFollowing("Resources_Trigger");
				AddResourcesTrigger.Value = Convert.ToDecimal(xmlTextReader.ReadElementContentAsString());
				xmlTextReader.ReadToFollowing("Dynamics_Trigger");
				AddDynamicsID.Value = Convert.ToDecimal(xmlTextReader.ReadElementContentAsString());
				xmlTextReader.ReadToFollowing("Dynamics_ID");
				AddDynamicsTrigger.Value = Convert.ToDecimal(xmlTextReader.ReadElementContentAsString());
			}
			else
			{
				AddintExistenceType.SelectedIndex = 0;
				AddMonsterType.SelectedIndex = 0;
				Version_combobox.SelectedIndex = 0;
				DefaultMobButton_combobox.SelectedIndex = 0;
				ExtraMobButton_combobox.SelectedIndex = 1;
				DefaultResourceButton_combobox.SelectedIndex = 0;
				ExtraResourceButton_combobox.SelectedIndex = 2;
				DefaultDynamicsButton_combobox.SelectedIndex = 0;
				ExtraDynamicsButton_combobox.SelectedIndex = 3;
				DefaultMobButton_combobox_SelectedIndexChanged(DefaultMobButton_combobox, null);
				DefaultMobButton_combobox_SelectedIndexChanged(ExtraMobButton_combobox, null);
				DefaultMobButton_combobox_SelectedIndexChanged(DefaultResourceButton_combobox, null);
				DefaultMobButton_combobox_SelectedIndexChanged(ExtraResourceButton_combobox, null);
				DefaultMobButton_combobox_SelectedIndexChanged(DefaultDynamicsButton_combobox, null);
				DefaultMobButton_combobox_SelectedIndexChanged(ExtraDynamicsButton_combobox, null);
			}
		}
		catch
		{
			if (Language == 1)
			{
				MessageBox.Show("Ошибка при загрузке настроек", "Npcgen Editor", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
			}
			else
			{
				MessageBox.Show("Error when reading options", "Npcgen Editor", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
			}
		}
		LoadedMapConfigs = new List<MapLoadedInformation>();
		if (File.Exists(Application.StartupPath + "\\Maps.conf"))
		{
			StreamReader streamReader3 = new StreamReader(Application.StartupPath + "\\Maps.conf");
			do
			{
				string[] array6 = streamReader3.ReadLine().Split(new string[1] { "->" }, StringSplitOptions.None);
				MapLoadedInformation mapLoadedInformation = new MapLoadedInformation
				{
					Name = array6[0]
				};
				string[] array7 = array6[1].Split(new string[1] { "," }, StringSplitOptions.RemoveEmptyEntries);
				int.TryParse(array7[0], out mapLoadedInformation.Width);
				int.TryParse(array7[1], out mapLoadedInformation.Height);
				LoadedMapConfigs.Add(mapLoadedInformation);
			}
			while (!streamReader3.EndOfStream);
		}
	}

	private void InformationButton_Click(object sender, EventArgs e)
	{
		MessageBox.Show("Perfect world: Npcgen.data Editor \rVersion: v1.7.4\rSkype:Luka007789\r                                         27.10.2023\r                                              © Luka and Updated by Haly", "Npcgen Editor By Luka and Updated by Haly", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
	}

	private void UpObjects(object sender, EventArgs e)
	{
		if (Read == null)
		{
			return;
		}
		DataGridView dataGridView = null;
		int selectedIndex = MainTabControl.SelectedIndex;
		if (MainTabControl.SelectedIndex == 0)
		{
			dataGridView = NpcMobsGrid;
		}
		else if (MainTabControl.SelectedIndex == 1)
		{
			dataGridView = ResourcesGrid;
		}
		else if (MainTabControl.SelectedIndex == 2)
		{
			dataGridView = DynamicGrid;
		}
		else if (MainTabControl.SelectedIndex == 3)
		{
			dataGridView = TriggersGrid;
		}
		List<DataGridViewRow> list = (from DataGridViewRow i in dataGridView.SelectedRows
			orderby i.Index
			select i).ToList();
		List<DataGridViewRow> list2 = (from DataGridViewRow i in dataGridView.SelectedRows
			orderby i.Index
			select i).ToList();
		if (list.Count == 0 || list[0].Index == 0)
		{
			return;
		}
		AllowCellChanging = false;
		foreach (DataGridViewRow item5 in list)
		{
			int index = item5.Index;
			int num = item5.Index - 1;
			dataGridView.Rows.Remove(item5);
			dataGridView.Rows.Insert(num, item5);
			switch (selectedIndex)
			{
			case 0:
			{
				ClassDefaultMonsters item4 = Read.NpcMobList[index];
				Read.NpcMobList.RemoveAt(index);
				Read.NpcMobList.Insert(num, item4);
				break;
			}
			case 1:
			{
				ClassDefaultResources item3 = Read.ResourcesList[index];
				Read.ResourcesList.RemoveAt(index);
				Read.ResourcesList.Insert(num, item3);
				break;
			}
			case 2:
			{
				ClassDynamicObject item2 = Read.DynamicsList[index];
				Read.DynamicsList.RemoveAt(index);
				Read.DynamicsList.Insert(num, item2);
				break;
			}
			case 3:
			{
				ClassTrigger item = Read.TriggersList[index];
				Read.TriggersList.RemoveAt(index);
				Read.TriggersList.Insert(num, item);
				break;
			}
			}
		}
		AllowCellChanging = true;
		dataGridView.CurrentCell = dataGridView.Rows[list[list.Count() - 1].Index].Cells[1];
		foreach (DataGridViewRow item6 in list2)
		{
			dataGridView.Rows[item6.Index].Selected = true;
		}
	}

	private void DownObjects(object sender, EventArgs e)
	{
		if (Read == null)
		{
			return;
		}
		DataGridView dataGridView = null;
		int selectedIndex = MainTabControl.SelectedIndex;
		if (MainTabControl.SelectedIndex == 0)
		{
			dataGridView = NpcMobsGrid;
		}
		else if (MainTabControl.SelectedIndex == 1)
		{
			dataGridView = ResourcesGrid;
		}
		else if (MainTabControl.SelectedIndex == 2)
		{
			dataGridView = DynamicGrid;
		}
		else if (MainTabControl.SelectedIndex == 3)
		{
			dataGridView = TriggersGrid;
		}
		List<DataGridViewRow> list = (from DataGridViewRow i in dataGridView.SelectedRows
			orderby i.Index descending
			select i).ToList();
		List<DataGridViewRow> list2 = (from DataGridViewRow i in dataGridView.SelectedRows
			orderby i.Index descending
			select i).ToList();
		if (list.Count == 0 || list[0].Index == dataGridView.Rows.Count - 1)
		{
			return;
		}
		AllowCellChanging = false;
		foreach (DataGridViewRow item5 in list)
		{
			int index = item5.Index;
			int num = item5.Index + 1;
			dataGridView.Rows.Remove(item5);
			dataGridView.Rows.Insert(num, item5);
			switch (selectedIndex)
			{
			case 0:
			{
				ClassDefaultMonsters item4 = Read.NpcMobList[index];
				Read.NpcMobList.RemoveAt(index);
				Read.NpcMobList.Insert(num, item4);
				break;
			}
			case 1:
			{
				ClassDefaultResources item3 = Read.ResourcesList[index];
				Read.ResourcesList.RemoveAt(index);
				Read.ResourcesList.Insert(num, item3);
				break;
			}
			case 2:
			{
				ClassDynamicObject item2 = Read.DynamicsList[index];
				Read.DynamicsList.RemoveAt(index);
				Read.DynamicsList.Insert(num, item2);
				break;
			}
			case 3:
			{
				ClassTrigger item = Read.TriggersList[index];
				Read.TriggersList.RemoveAt(index);
				Read.TriggersList.Insert(num, item);
				break;
			}
			}
		}
		AllowCellChanging = true;
		dataGridView.CurrentCell = dataGridView.Rows[list[list.Count() - 1].Index].Cells[1];
		foreach (DataGridViewRow item6 in list2)
		{
			dataGridView.Rows[item6.Index].Selected = true;
		}
	}

	private void GridsKeyDown(object sender, KeyEventArgs e)
	{
		if (e.KeyCode == Keys.W && Control.ModifierKeys == Keys.Shift)
		{
			UpObjects(null, null);
		}
		if (e.KeyCode == Keys.S && Control.ModifierKeys == Keys.Shift)
		{
			DownObjects(null, null);
		}
	}

	private void MoveToTrigger_Click(object sender, EventArgs e)
	{
		if (Read != null)
		{
			int TriggerId = 0;
			if (MainTabControl.SelectedIndex == 0)
			{
				int.TryParse(Trigger.Text, out TriggerId);
			}
			else if (MainTabControl.SelectedIndex == 1)
			{
				int.TryParse(RTriggerID.Text, out TriggerId);
			}
			else if (MainTabControl.SelectedIndex == 2)
			{
				int.TryParse(DTrigger_id.Text, out TriggerId);
			}
			int num = Read.TriggersList.FindIndex((ClassTrigger z) => z.Id == TriggerId);
			if (num != -1)
			{
				TriggersGrid.CurrentCell = TriggersGrid.Rows[num].Cells[1];
				MainTabControl.SelectedIndex = 3;
			}
			else if (Language == 1)
			{
				MessageBox.Show("Операция не удалась!!...", "Npcgen Editor", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
			}
			else if (Language == 2)
			{
				MessageBox.Show("Invalid action!!...", "Npcgen Editor", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
			}
		}
	}

	private void LineUpX_Click(object sender, EventArgs e)
	{
		if (Read == null)
		{
			return;
		}
		if (MainTabControl.SelectedIndex == 0)
		{
			foreach (int item in NpcRowCollection)
			{
				Read.NpcMobList[item].X_position = Read.NpcMobList[NpcRowIndex].X_position;
			}
			return;
		}
		if (MainTabControl.SelectedIndex == 1)
		{
			foreach (int item2 in ResourcesRowCollection)
			{
				Read.ResourcesList[item2].X_position = Read.ResourcesList[NpcRowIndex].X_position;
			}
			return;
		}
		if (MainTabControl.SelectedIndex != 2)
		{
			return;
		}
		foreach (int item3 in DynamicsRowCollection)
		{
			Read.DynamicsList[item3].X_position = Read.DynamicsList[NpcRowIndex].X_position;
		}
	}

	private void LineUpZ_Click(object sender, EventArgs e)
	{
		if (Read == null)
		{
			return;
		}
		if (MainTabControl.SelectedIndex == 0)
		{
			foreach (int item in NpcRowCollection)
			{
				Read.NpcMobList[item].Z_position = Read.NpcMobList[NpcRowIndex].Z_position;
			}
			return;
		}
		if (MainTabControl.SelectedIndex == 1)
		{
			foreach (int item2 in ResourcesRowCollection)
			{
				Read.ResourcesList[item2].Z_position = Read.ResourcesList[NpcRowIndex].Z_position;
			}
			return;
		}
		if (MainTabControl.SelectedIndex != 2)
		{
			return;
		}
		foreach (int item3 in DynamicsRowCollection)
		{
			Read.DynamicsList[item3].Z_position = Read.DynamicsList[NpcRowIndex].Z_position;
		}
	}

	private void ChangeLanguage(object sender, EventArgs e)
	{
		int selectedIndex = ExistenceLocating.SelectedIndex;
		int selectedIndex2 = ExistenceType.SelectedIndex;
		int selectedIndex3 = AddintExistenceType.SelectedIndex;
		int selectedIndex4 = AddMonsterType.SelectedIndex;
		int selectedIndex5 = TStartWeekDay.SelectedIndex;
		int selectedIndex6 = TStopWeekDay.SelectedIndex;
		int selectedIndex7 = Agression.SelectedIndex;
		int selectedIndex8 = Path_type.SelectedIndex;
		Control control = sender as Control;
		if (control.Name == "Russian")
		{
			Language = 1;
			OpenFiles.Text = "Открыть";
			Open_surfaces.Text = "Открыть";
			ButtonShowMap.Text = "Показать карту";
			if (Read == null)
			{
				ExistenceTab.Text = "Мобы и Нипы";
				ResourcesTab.Text = "Ресурсы";
				DynObjectsTab.Text = "Динамические Объекты";
				TriggersTab.Text = "Тригеры";
			}
			else
			{
				ExistenceTab.Text = $"Мобы и Нипы {NpcRowCollection.Count}/{Read.NpcMobsAmount}";
				ResourcesTab.Text = $"Ресурсы {ResourcesRowCollection.Count}/{Read.ResourcesAmount}";
				DynObjectsTab.Text = $"Динамические Объекты {DynamicsRowCollection.Count}/{Read.DynobjectAmount}";
				TriggersTab.Text = $"Тригеры {TriggersRowCollection.Count}/{Read.TriggersAmount}";
			}
			OptionsTab.Text = "Настройки и сохранение";
			SearchTab.Text = "Поиск";
			ErrorsTab.Text = "Ошибки";
			ExistenceLocating.Items.Clear();
			ComboBox.ObjectCollection items = ExistenceLocating.Items;
			object[] items2 = new string[2] { "Наземный", "Свободный" };
			items.AddRange(items2);
			ExistenceLocating.SelectedIndex = selectedIndex;
			AddintExistenceType.Items.Clear();
			ComboBox.ObjectCollection items3 = AddintExistenceType.Items;
			items2 = new string[2] { "Наземный", "Свободный" };
			items3.AddRange(items2);
			AddintExistenceType.SelectedIndex = selectedIndex3;
			ExistenceType.Items.Clear();
			ComboBox.ObjectCollection items4 = ExistenceType.Items;
			items2 = new string[2] { "Моб", "Нпс" };
			items4.AddRange(items2);
			ExistenceType.SelectedIndex = selectedIndex2;
			AddMonsterType.Items.Clear();
			ComboBox.ObjectCollection items5 = AddMonsterType.Items;
			items2 = new string[2] { "Моб", "Нпс" };
			items5.AddRange(items2);
			AddMonsterType.SelectedIndex = selectedIndex4;
			MainGroupBox.Text = "Основное";
			groupBox1.Text = "Мобы|Нипы";
			groupBox3.Text = "Основное";
			groupBox7.Text = "Основное";
			groupBox9.Text = "Изображение";
			groupBox8.Text = "Настройки для добавления новых существ";
			label3.Text = "Расположе:";
			label3.Location = new Point(2, 14);
			label9.Text = "Поворот X:";
			label9.Location = new Point(5, 106);
			label10.Text = "Поворот Y:";
			label10.Location = new Point(5, 131);
			label11.Text = "Поворот Z:";
			label11.Location = new Point(5, 154);
			label14.Text = "Разброс X:";
			label14.Location = new Point(6, 178);
			label13.Text = "Разброс Y:";
			label13.Location = new Point(6, 201);
			label12.Text = "Разброс Z:";
			label12.Location = new Point(6, 224);
			label15.Text = "Тип:";
			label15.Location = new Point(215, 12);
			label5.Text = "В группе:";
			label5.Location = new Point(186, 39);
			label16.Text = "Тип группы:";
			label16.Location = new Point(172, 62);
			label18.Text = "Триггер:";
			label18.Location = new Point(191, 109);
			label19.Text = "Врем.Жизни:";
			label19.Location = new Point(165, 132);
			label20.Text = "Макс.колво:";
			label20.Location = new Point(169, 155);
			ExistenceAutoRevive.Text = "Мгновенный респавн";
			ExistenceAutoRevive.Location = new Point(201, 200);
			ExistenceInitGen.Text = "Активировать генератор";
			ExistenceInitGen.Location = new Point(184, 178);
			ExistenceCloneButton.Text = "Клонировать";
			ExistenceRemoveButton.Text = "Удалить";
			ExistenceGroupCloneButton.Text = "Клонировать";
			ExistenceGroupRemoveButton.Text = "Удалить";
			label22.Text = "Количество:";
			label22.Location = new Point(32, 37);
			label23.Text = "Время респавна:";
			label23.Location = new Point(5, 60);
			label24.Text = "Кол-во смертей:";
			label24.Location = new Point(8, 83);
			label25.Text = "Агрессия:";
			label25.Location = new Point(49, 106);
			label26.Text = "Тип пути:";
			label26.Location = new Point(51, 130);
			label27.Text = "Скорость:";
			label27.Location = new Point(45, 154);
			label28.Text = "Путь:";
			label28.Location = new Point(72, 177);
			label29.Text = "Оффсет воды:";
			label29.Location = new Point(16, 201);
			label30.Text = "Оффсет повор:";
			label30.Location = new Point(196, 14);
			label31.Text = "Просит помощь:";
			label31.Location = new Point(193, 60);
			label32.Text = "Нужна помощь:";
			label32.Location = new Point(199, 83);
			label33.Text = "Показ трупа(Сек):";
			label33.Location = new Point(184, 107);
			label35.Text = "Группа:";
			label35.Location = new Point(245, 37);
			ExistenceInsertCordsFromGame.Text = "Вставить координаты с игры";
			label67.Text = "ID триггера:";
			label67.Location = new Point(89, 18);
			label68.Text = "Расположение:";
			label68.Location = new Point(62, 39);
			label66.Text = "Тип существа:";
			label66.Location = new Point(70, 62);
			label65.Text = "ID создаваемого существа:";
			label65.Location = new Point(8, 89);
			label64.Text = "Количество существ:";
			label64.Location = new Point(42, 112);
			label63.Text = "Время респавна:";
			label63.Location = new Point(62, 135);
			ResourcesCloneButton.Text = "Клонировать";
			ResourcesRemoveButton.Text = "Удалить";
			label43.Text = "Разброс X:";
			label43.Location = new Point(8, 82);
			label42.Text = "Разброс Z:";
			label42.Location = new Point(9, 105);
			label45.Text = "Наклон 1:";
			label45.Location = new Point(14, 128);
			label44.Text = "Наклон 2:";
			label44.Location = new Point(14, 151);
			label41.Text = "Поворот:";
			label41.Location = new Point(16, 174);
			label51.Text = "В группе:";
			label51.Location = new Point(189, 15);
			label37.Text = "Триггер:";
			label37.Location = new Point(192, 60);
			groupBox11.Text = "Настройки для добавления новых ресурсов";
			label75.Text = "ID триггера:";
			label75.Location = new Point(90, 18);
			label57.Text = "ID создаваемого ресурса:";
			label57.Location = new Point(18, 42);
			label56.Text = "Количество ресурсов:";
			label56.Location = new Point(40, 65);
			label54.Text = "Время респавна:";
			label54.Location = new Point(65, 88);
			ResourcesInsertCordsFromGame.Text = "Вставить координаты с игры";
			ResourcesGroupCloneButton.Text = "Клонировать";
			ResourcesGroupRemoveButton.Text = "Удалить";
			groupBox5.Text = "Ресурсы";
			ResourcesInitGen.Text = "Активировать генератор";
			ResourcesInitGen.Location = new Point(187, 107);
			ResourcesAutoRevive.Text = "Мгновенный респавн";
			ResourcesAutoRevive.Location = new Point(204, 129);
			label59.Text = "Количество:";
			label59.Location = new Point(224, 41);
			label58.Text = "Время респа:";
			label58.Location = new Point(218, 67);
			label55.Text = "Тип:";
			label55.Location = new Point(272, 93);
			label53.Text = "Над землей:";
			label53.Location = new Point(224, 119);
			DynObjectsCloneButton.Text = "Клонировать";
			DynObjectsRemoveButton.Text = "Удалить";
			label72.Text = "Наклон 1:";
			label72.Location = new Point(4, 107);
			label71.Text = "Наклон 2:";
			label71.Location = new Point(184, 38);
			label70.Text = "Поворот:";
			label70.Location = new Point(187, 61);
			label73.Text = "Триггер:";
			label73.Location = new Point(192, 83);
			label74.Text = "Увеличение:";
			label74.Location = new Point(167, 107);
			groupBox10.Text = "Настройки для добавления Дин.Объектов";
			label61.Text = "ID Дин.Объекта:";
			label61.Location = new Point(69, 18);
			label40.Text = "ID триггера:";
			label40.Location = new Point(89, 43);
			DynObjectsInsertCordsFromGame.Text = "Вставить координаты с игры";
			TriggersCloneButton.Text = "Клонировать";
			TriggersRemoveButton.Text = "Удалить";
			groupBox12.Text = "Основное";
			groupBox16.Text = "Запуск";
			groupBox17.Text = "Выключение";
			groupBox13.Text = "Используется в существах";
			groupBox14.Text = "Используется в ресурсах";
			groupBox15.Text = "Используется в дин.объектах";
			GotoNpcMobsContacts.Text = "Перейти к выбранному";
			GotoResourcesContacts.Text = "Перейти к выбранному";
			GotoDynamicsContacts.Text = "Перейти к выбранному";
			label89.Text = "ID Триггера:";
			label89.Location = new Point(71, 16);
			label79.Text = "ID в панели ГМ:";
			label79.Location = new Point(50, 39);
			label99.Text = "Название:";
			label99.Location = new Point(81, 61);
			label83.Text = "Задержка включения:";
			label83.Location = new Point(14, 84);
			label84.Text = "Задержка выключения:";
			label84.Location = new Point(5, 106);
			TAutoStart.Text = "Запускать автоматически";
			label85.Text = "Продолжительность:";
			label85.Location = new Point(7, 147);
			TStartBySchedule.Text = "Запускать по графику";
			TStartBySchedule.Location = new Point(178, 170);
			TStopBySchedule.Text = "Выключать по графику";
			TStopBySchedule.Location = new Point(174, 190);
			label86.Text = "Год:";
			label86.Location = new Point(55, 16);
			label87.Text = "Месяц:";
			label87.Location = new Point(38, 40);
			label88.Text = "День недели:";
			label88.Location = new Point(1, 65);
			label90.Text = "День:";
			label90.Location = new Point(168, 15);
			label91.Text = "Час:";
			label91.Location = new Point(177, 41);
			label92.Text = "Минута:";
			label92.Location = new Point(154, 65);
			label97.Text = "Год:";
			label97.Location = new Point(55, 16);
			label96.Text = "Месяц:";
			label96.Location = new Point(38, 40);
			label98.Text = "День недели:";
			label98.Location = new Point(1, 65);
			label94.Text = "Час:";
			label94.Location = new Point(177, 41);
			label95.Text = "День:";
			label95.Location = new Point(168, 15);
			label93.Text = "Минута:";
			label93.Location = new Point(154, 65);
			label52.Text = "Версия клиента для захвата координат из игры:";
			groupBox6.Text = "Горячие клавиши";
			label50.Text = "Существо:";
			label50.Location = new Point(20, 19);
			label77.Text = "Ресурс:";
			label77.Location = new Point(37, 46);
			label101.Text = "Дин.Объект:";
			label101.Location = new Point(7, 73);
			SaveFile.Text = "Сохранить Npcgen.data";
			ConvertComboboxVersion.Location = new Point(335, 435);
			ConvertAndSaveButton.Text = "Конвертировать в     версию и сохранить";
			TStartWeekDay.Items.Clear();
			ComboBox.ObjectCollection items6 = TStartWeekDay.Items;
			items2 = new string[8] { "Все", "Воскресенье", "Понедельник", "Вторник", "Среда", "Четверг", "Пятница", "Суббота" };
			items6.AddRange(items2);
			TStartWeekDay.SelectedIndex = selectedIndex5;
			TStopWeekDay.Items.Clear();
			ComboBox.ObjectCollection items7 = TStopWeekDay.Items;
			items2 = new string[8] { "Все", "Воскресенье", "Понедельник", "Вторник", "Среда", "Четверг", "Пятница", "Суббота" };
			items7.AddRange(items2);
			TStopWeekDay.SelectedIndex = selectedIndex6;
			DeleteEmptyTrigger.Text = "Удалить пустые триггеры";
			MoveToTrigger.Text = "Перейти к триггеру";
			toolStripMenuItem1.Text = "Переместить";
			UpExistence.Text = "Выше   Shift+W";
			DownExistence.Text = "Ниже    Shift+S";
			toolStripMenuItem4.Text = "Переместить";
			UpTrigger.Text = "Выше   Shift+W";
			DownTrigger.Text = "Ниже    Shift+S";
			Agression.Items[0] = "Нет";
			Agression.Items[1] = "Агрессивный";
			Agression.Items[2] = "Защита";
			Path_type.Items[0] = "Нет";
			Path_type.Items[1] = "Обычный";
			Path_type.Items[2] = "Зацикленный";
			LineUpX.Text = "Выстроить в ряд по X";
			LineUpZ.Text = "Выстроить в ряд по Z";
			groupBox18.Text = "Поиск в существах";
			groupBox19.Text = "Поиск в ресурсах";
			groupBox20.Text = "Поиск в Дин.Объектах";
			groupBox21.Text = "Поиск в Триггерах";
			MoveToSelected.Text = "Перейти к выбранному";
			ExistenceSearchName_Radio.Text = "Название";
			ExistenceSearchName_Radio.Location = new Point(1, 43);
			ExistenceSearchTrigger_Radio.Text = "Триггер";
			ExistenceSearchTrigger_Radio.Location = new Point(10, 64);
			ExistenceSearchPath_Radio.Text = "Путь";
			ExistenceSearchPath_Radio.Location = new Point(27, 86);
			ExistenceSearchButton.Text = "Найти";
			ResourceSearchName_Radio.Text = "Название";
			ResourceSearchName_Radio.Location = new Point(1, 43);
			ResourceSearchTrigger_Radio.Text = "Триггер";
			ResourceSearchTrigger_Radio.Location = new Point(10, 64);
			ResourceSearchButton.Text = "Найти";
			DynamicSearchName_Radio.Text = "Название";
			DynamicSearchName_Radio.Location = new Point(1, 43);
			DynamicSearchTrigger_Radio.Text = "Триггер";
			DynamicSearchTrigger_Radio.Location = new Point(10, 64);
			DynamicSearchButton.Text = "Найти";
			TriggerSearchName_Radio.Text = "Название";
			TriggerSearchName_Radio.Location = new Point(1, 64);
			TriggerSearchButton.Text = "Найти";
			SearchErrorsButton.Text = "Найти ошибки";
			RemoveAllErrors.Text = "Удалить все объекты";
			ExportExistence.Text = "Экспорт";
			ImportExistence.Text = "Импорт";
			LineUpExistenceDropDown.Text = "Выстроить";
			ToolStripLineUpX.Text = "По Х";
			ToolStripLineUpZ.Text = "По Z";
			MoveExistenceDropDown.Text = "Переместить";
			MoveUpToolStripMenuItem.Text = "Выше   Shift+W";
			MoveDownToolStripMenuItem.Text = "Ниже    Shift+S";
			ExportResources.Text = "Экспорт";
			ImportResources.Text = "Импорт";
			LineUpResource.Text = "Выстроить";
			ResourcesOnX.Text = "По X";
			ResourcesOnZ.Text = "По Z";
			MoveResources.Text = "Переместить";
			ResourceUp.Text = "Выше   Shift+W";
			ResourceDown.Text = "Ниже Shift+S";
			toolStripButton3.Text = "Экспорт";
			toolStripButton4.Text = "Импорт";
			toolStripDropDownButton3.Text = "Выстроить";
			toolStripMenuItem7.Text = "По X";
			toolStripMenuItem8.Text = "По Z";
			toolStripDropDownButton4.Text = "Переместить";
			toolStripMenuItem9.Text = "Выше   Shift+W";
			toolStripMenuItem10.Text = "Ниже Shift+S";
			toolStripButton5.Text = "Экспорт";
			toolStripButton6.Text = "Импорт";
			toolStripDropDownButton6.Text = "Переместить";
			toolStripMenuItem13.Text = "Выше   Shift+W";
			toolStripMenuItem14.Text = "Ниже Shift+S";
			toolStripButton7.Text = "Очистить";
			экспортToolStripMenuItem.Text = "Экспорт";
			импортToolStripMenuItem.Text = "Импорт";
			toolStripMenuItem11.Text = "Импорт";
			toolStripMenuItem12.Text = "Экспорт";
			DynamicForm = new DynamicObjectsForm(DynamicsListRu, this);
			DynamicForm.LanguageChange(1);
		}
		else if (control.Name == "English")
		{
			Language = 2;
			OpenFiles.Text = "Open";
			Open_surfaces.Text = "Open";
			ButtonShowMap.Text = "Show map";
			if (Read == null)
			{
				ExistenceTab.Text = "Mobs and Npcs";
				ResourcesTab.Text = "Resources";
				DynObjectsTab.Text = "Dynamic Objects";
				TriggersTab.Text = "Triggers";
			}
			else
			{
				ExistenceTab.Text = $"Mobs and Npcs {NpcRowCollection.Count}/{Read.NpcMobsAmount}";
				ResourcesTab.Text = $"Resources {ResourcesRowCollection.Count}/{Read.ResourcesAmount}";
				DynObjectsTab.Text = $"Dynamic Objects {DynamicsRowCollection.Count}/{Read.DynobjectAmount}";
				TriggersTab.Text = $"Triggers {TriggersRowCollection.Count}/{Read.TriggersAmount}";
			}
			OptionsTab.Text = "Options and saving";
			SearchTab.Text = "Search";
			ErrorsTab.Text = "Errors";
			ExistenceLocating.Items.Clear();
			ComboBox.ObjectCollection items8 = ExistenceLocating.Items;
			object[] items2 = new string[2] { "Ground", "Free" };
			items8.AddRange(items2);
			ExistenceLocating.SelectedIndex = selectedIndex;
			AddintExistenceType.Items.Clear();
			ComboBox.ObjectCollection items9 = AddintExistenceType.Items;
			items2 = new string[2] { "Ground", "Free" };
			items9.AddRange(items2);
			AddintExistenceType.SelectedIndex = selectedIndex3;
			ExistenceType.Items.Clear();
			ComboBox.ObjectCollection items10 = ExistenceType.Items;
			items2 = new string[2] { "Mob", "Npc" };
			items10.AddRange(items2);
			ExistenceType.SelectedIndex = selectedIndex2;
			AddMonsterType.Items.Clear();
			ComboBox.ObjectCollection items11 = AddMonsterType.Items;
			items2 = new string[2] { "Mob", "Npc" };
			items11.AddRange(items2);
			AddMonsterType.SelectedIndex = selectedIndex4;
			MainGroupBox.Text = "Default";
			groupBox8.Text = "Options for adding new existence";
			groupBox1.Text = "Mobs|Npcs";
			label3.Text = "Location:";
			label3.Location = new Point(21, 14);
			label9.Text = "Rotation X:";
			label9.Location = new Point(11, 106);
			label10.Text = "Rotation Y:";
			label10.Location = new Point(11, 131);
			label11.Text = "Rotation Z:";
			label11.Location = new Point(11, 154);
			label14.Text = "Scatter X:";
			label14.Location = new Point(18, 178);
			label13.Text = "Scatter Y:";
			label13.Location = new Point(18, 201);
			label12.Text = "Scatter Z:";
			label12.Location = new Point(18, 224);
			label15.Text = "Type:";
			label15.Location = new Point(210, 12);
			label5.Text = "In group:";
			label5.Location = new Point(192, 39);
			label16.Text = "Group type:";
			label16.Location = new Point(178, 62);
			label18.Text = "Trigger:";
			label18.Location = new Point(197, 109);
			label19.Text = "Life time:";
			label19.Location = new Point(189, 132);
			label20.Text = "Max amount:";
			label20.Location = new Point(168, 155);
			ExistenceAutoRevive.Text = "Instant respawn";
			ExistenceAutoRevive.Location = new Point(235, 201);
			ExistenceInitGen.Text = "Activate generator";
			ExistenceInitGen.Location = new Point(223, 178);
			ResourcesInitGen.Text = "Activate generator";
			ResourcesInitGen.Location = new Point(226, 107);
			ResourcesAutoRevive.Text = "Instant respawn";
			ResourcesAutoRevive.Location = new Point(238, 129);
			ExistenceCloneButton.Text = "Clone";
			ExistenceRemoveButton.Text = "Delete";
			ExistenceGroupCloneButton.Text = "Clone";
			ExistenceGroupRemoveButton.Text = "Delete";
			label22.Text = "Amount:";
			label22.Location = new Point(59, 37);
			label23.Text = "Respawn time:";
			label23.Location = new Point(22, 60);
			label24.Text = "Death amount:";
			label24.Location = new Point(23, 83);
			label25.Text = "Agression:";
			label25.Location = new Point(47, 106);
			label26.Text = "Path type:";
			label26.Location = new Point(51, 130);
			label27.Text = "Speed:";
			label27.Location = new Point(65, 154);
			label28.Text = "Type:";
			label28.Location = new Point(75, 177);
			label29.Text = "Water offset:";
			label29.Location = new Point(37, 201);
			label30.Text = "Turn offset:";
			label30.Location = new Point(228, 14);
			label31.Text = "Ask help:";
			label31.Location = new Point(239, 60);
			label32.Text = "Need help:";
			label32.Location = new Point(227, 83);
			label33.Text = "Corpse(sec):";
			label33.Location = new Point(219, 107);
			label35.Text = "Group:";
			label35.Location = new Point(251, 37);
			ExistenceInsertCordsFromGame.Text = "Insert Coordinates from game";
			label67.Text = "Trigger ID:";
			label67.Location = new Point(101, 18);
			label68.Text = "Location:";
			label68.Location = new Point(100, 39);
			label66.Text = "Existence type:";
			label66.Location = new Point(68, 62);
			label65.Text = "Existence ID:";
			label65.Location = new Point(86, 89);
			label64.Text = "Existence amount:";
			label64.Location = new Point(62, 112);
			label63.Text = "Respawn time:";
			label63.Location = new Point(79, 135);
			groupBox3.Text = "Default";
			ResourcesCloneButton.Text = "Clone";
			ResourcesRemoveButton.Text = "Delete";
			label43.Text = "Spread X:";
			label43.Location = new Point(15, 82);
			label42.Text = "Spread Z:";
			label42.Location = new Point(16, 105);
			label45.Text = "Incline 1:";
			label45.Location = new Point(20, 128);
			label44.Text = "Incline 2:";
			label44.Location = new Point(20, 151);
			label41.Text = "Rotation:";
			label41.Location = new Point(20, 174);
			label51.Text = "In group:";
			label51.Location = new Point(194, 15);
			label37.Text = "Trigger:";
			label37.Location = new Point(200, 60);
			groupBox11.Text = "Options for adding new resources";
			label75.Text = "Trigger ID:";
			label75.Location = new Point(101, 18);
			label57.Text = "Resource ID:";
			label57.Location = new Point(87, 42);
			label56.Text = "Resources amount:";
			label56.Location = new Point(58, 65);
			label54.Text = "Respawn time:";
			label54.Location = new Point(80, 88);
			label53.Text = "Above ground:";
			label53.Location = new Point(218, 119);
			ResourcesInsertCordsFromGame.Text = "Insert Coordinates from game";
			ResourcesGroupCloneButton.Text = "Clone";
			ResourcesGroupRemoveButton.Text = "Delete";
			groupBox5.Text = "Resources";
			label59.Text = "Amount:";
			label59.Location = new Point(251, 41);
			label58.Text = "Respa. time:";
			label58.Location = new Point(227, 67);
			label55.Text = "Type:";
			label55.Location = new Point(267, 93);
			DynObjectsCloneButton.Text = "Clone";
			DynObjectsRemoveButton.Text = "Delete";
			groupBox7.Text = "Default";
			groupBox9.Text = "Image";
			label72.Text = "Incline 1:";
			label72.Location = new Point(11, 107);
			label71.Text = "Incline 2:";
			label71.Location = new Point(192, 38);
			label70.Text = "Rotation:";
			label70.Location = new Point(190, 61);
			label73.Text = "Trigger:";
			label73.Location = new Point(198, 83);
			label74.Text = "Scale:";
			label74.Location = new Point(206, 107);
			groupBox10.Text = "Options for adding new dyn.Objects";
			label61.Text = "Dyn.Object ID:";
			label61.Location = new Point(81, 18);
			label40.Text = "Trigger ID:";
			label40.Location = new Point(100, 43);
			DynObjectsInsertCordsFromGame.Text = "Insert Coordinates from game";
			TriggersCloneButton.Text = "Clone";
			TriggersRemoveButton.Text = "Delete";
			groupBox12.Text = "Default";
			groupBox16.Text = "Start schedule";
			groupBox17.Text = "Stop schedule";
			groupBox13.Text = "Using in existence";
			groupBox14.Text = "Using in resources";
			groupBox15.Text = "Using in Dyn.Objects";
			GotoNpcMobsContacts.Text = "Move to selected";
			GotoResourcesContacts.Text = "Move to selected";
			GotoDynamicsContacts.Text = "Move to selected";
			label89.Text = "Trigger ID:";
			label89.Location = new Point(85, 16);
			label79.Text = "ID in GM console:";
			label79.Location = new Point(44, 39);
			label99.Text = "Name:";
			label99.Location = new Point(103, 61);
			label83.Text = "Start delay:";
			label83.Location = new Point(80, 84);
			label84.Text = "Stop delay:";
			label84.Location = new Point(81, 106);
			TAutoStart.Text = "Start automatically";
			label85.Text = "During:";
			label85.Location = new Point(95, 147);
			TStartBySchedule.Text = "Start on schedule";
			TStartBySchedule.Location = new Point(209, 170);
			TStopBySchedule.Text = "Stop on schedule";
			TStopBySchedule.Location = new Point(209, 190);
			label86.Text = "Year:";
			label86.Location = new Point(52, 16);
			label87.Text = "Month:";
			label87.Location = new Point(41, 40);
			label88.Text = "Week day:";
			label88.Location = new Point(20, 65);
			label90.Text = "Day:";
			label90.Location = new Point(177, 15);
			label91.Text = "Hour:";
			label91.Location = new Point(171, 41);
			label92.Text = "Minute:";
			label92.Location = new Point(160, 65);
			label97.Text = "Year:";
			label97.Location = new Point(50, 16);
			label96.Text = "Month:";
			label96.Location = new Point(40, 40);
			label98.Text = "Week day:";
			label98.Location = new Point(22, 65);
			label95.Text = "Day:";
			label95.Location = new Point(177, 15);
			label94.Text = "Hour:";
			label94.Location = new Point(171, 41);
			label93.Text = "Minute:";
			label93.Location = new Point(160, 65);
			label52.Text = "Client version for catching coordinates from game:";
			groupBox6.Text = "Hot keys";
			label50.Text = "Existence:";
			label50.Location = new Point(24, 19);
			label77.Text = "Resource:";
			label77.Location = new Point(23, 46);
			label101.Text = "Dyn.object:";
			label101.Location = new Point(18, 73);
			SaveFile.Text = "Save Npcgen.data";
			ConvertComboboxVersion.Location = new Point(274, 435);
			ConvertAndSaveButton.Text = "Convert to               version and save";
			TStartWeekDay.Items.Clear();
			ComboBox.ObjectCollection items12 = TStartWeekDay.Items;
			items2 = new string[8] { "All", "Sunday", "Monday", "Tuesday", "Wednesday", "Thursday", "Friday", "Saturday" };
			items12.AddRange(items2);
			TStartWeekDay.SelectedIndex = selectedIndex5;
			TStopWeekDay.Items.Clear();
			ComboBox.ObjectCollection items13 = TStopWeekDay.Items;
			items2 = new string[8] { "All", "Sunday", "Monday", "Tuesday", "Wednesday", "Thursday", "Friday", "Saturday" };
			items13.AddRange(items2);
			TStopWeekDay.SelectedIndex = selectedIndex6;
			DeleteEmptyTrigger.Text = "Delete empty triggers";
			MoveToTrigger.Text = "Move to trigger";
			toolStripMenuItem1.Text = "Move";
			UpExistence.Text = "Up           Shift+W";
			DownExistence.Text = "Down      Shift+S";
			toolStripMenuItem4.Text = "Move";
			UpTrigger.Text = "Up           Shift+W";
			DownTrigger.Text = "Down      Shift+S";
			Agression.Items[0] = "No";
			Agression.Items[1] = "Aggressive";
			Agression.Items[2] = "Defend";
			Path_type.Items[0] = "No";
			Path_type.Items[1] = "Default";
			Path_type.Items[2] = "Cycle";
			LineUpX.Text = "Line up on X";
			LineUpZ.Text = "Line up on Z";
			groupBox18.Text = "Search in existence";
			groupBox19.Text = "Search in resources";
			groupBox20.Text = "Search in Dyn.Objects";
			groupBox21.Text = "Search in triggers";
			MoveToSelected.Text = "Move to selected";
			ExistenceSearchName_Radio.Text = "Name";
			ExistenceSearchName_Radio.Location = new Point(22, 43);
			ExistenceSearchTrigger_Radio.Text = "Trigger";
			ExistenceSearchTrigger_Radio.Location = new Point(17, 64);
			ExistenceSearchPath_Radio.Text = "Path";
			ExistenceSearchPath_Radio.Location = new Point(28, 86);
			ExistenceSearchButton.Text = "Search";
			ResourceSearchName_Radio.Text = "Name";
			ResourceSearchName_Radio.Location = new Point(22, 43);
			ResourceSearchTrigger_Radio.Text = "Trigger";
			ResourceSearchTrigger_Radio.Location = new Point(17, 64);
			ResourceSearchButton.Text = "Search";
			DynamicSearchName_Radio.Text = "Name";
			DynamicSearchName_Radio.Location = new Point(22, 43);
			DynamicSearchTrigger_Radio.Text = "Trigger";
			DynamicSearchTrigger_Radio.Location = new Point(17, 64);
			DynamicSearchButton.Text = "Search";
			TriggerSearchName_Radio.Text = "Name";
			TriggerSearchName_Radio.Location = new Point(22, 64);
			TriggerSearchButton.Text = "Search";
			SearchErrorsButton.Text = "Search errors";
			RemoveAllErrors.Text = "Remove all objects";
			ExportExistence.Text = "Export";
			ImportExistence.Text = "Import";
			LineUpExistenceDropDown.Text = "Line up";
			ToolStripLineUpX.Text = "On Х";
			ToolStripLineUpZ.Text = "On Z";
			MoveExistenceDropDown.Text = "Move objects";
			MoveUpToolStripMenuItem.Text = "Up           Shift+W";
			MoveDownToolStripMenuItem.Text = "Down      Shift+S";
			ExportResources.Text = "Export";
			ImportResources.Text = "Import";
			LineUpResource.Text = "Line up";
			ResourcesOnX.Text = "On X";
			ResourcesOnZ.Text = "On Z";
			MoveResources.Text = "Move objects";
			ResourceUp.Text = "Up           Shift+W";
			ResourceDown.Text = "Down      Shift+S";
			toolStripButton3.Text = "Export";
			toolStripButton4.Text = "Import";
			toolStripDropDownButton3.Text = "Line up";
			toolStripMenuItem7.Text = "On X";
			toolStripMenuItem8.Text = "On Z";
			toolStripDropDownButton4.Text = "Move objects";
			toolStripMenuItem9.Text = "Up   Shift+W";
			toolStripMenuItem10.Text = "Down Shift+S";
			toolStripButton5.Text = "Export";
			toolStripButton6.Text = "Import";
			toolStripDropDownButton6.Text = "Move objects";
			toolStripMenuItem13.Text = "Up   Shift+W";
			toolStripMenuItem14.Text = "Down Shift+S";
			toolStripButton7.Text = "Clear";
			экспортToolStripMenuItem.Text = "Export";
			импортToolStripMenuItem.Text = "Import";
			toolStripMenuItem11.Text = "Import";
			toolStripMenuItem12.Text = "Export";
			DynamicForm = new DynamicObjectsForm(DynamicsListEn, this);
			DynamicForm.LanguageChange(2);
		}
		if (Read != null)
		{
			DynamicGrid.Rows.Clear();
			SortDynamicObjects();
		}
		if (ChooseFromElementsForm != null)
		{
			ChooseFromElementsForm.RefreshLanguage(Language);
		}
	}

	private void NpcMobsGrid_CellMouseEnter(object sender, DataGridViewCellEventArgs e)
	{
		if ((e.ColumnIndex > 0) & (e.RowIndex >= 0))
		{
			string caption = Convert.ToString(NpcMobsGrid.Rows[e.RowIndex].Cells[e.ColumnIndex].Value);
			toolTip1.SetToolTip(NpcMobsGrid, caption);
		}
	}

	private void ExistenceGrid_CellChanged(object sender, EventArgs e)
	{
		if (!AllowCellChanging)
		{
			return;
		}
		NpcsGroupGrid.Rows.Clear();
		NpcRowCollection = (from DataGridViewRow q in NpcMobsGrid.SelectedRows
			select q.Index into r
			orderby r descending
			select r).ToList();
		if (NpcMobsGrid.CurrentRow != null)
		{
			NpcRowIndex = NpcMobsGrid.CurrentRow.Index;
			if (NpcRowIndex != -1)
			{
				ExistenceLocating.SelectedIndex = Read.NpcMobList[NpcRowIndex].Location;
				Group_amount_textbox.Text = Read.NpcMobList[NpcRowIndex].Amount_in_group.ToString();
				X_position.Text = Read.NpcMobList[NpcRowIndex].X_position.ToString();
				Y_position.Text = Read.NpcMobList[NpcRowIndex].Y_position.ToString();
				Z_position.Text = Read.NpcMobList[NpcRowIndex].Z_position.ToString();
				X_rotate.Text = Read.NpcMobList[NpcRowIndex].X_direction.ToString();
				Y_rotate.Text = Read.NpcMobList[NpcRowIndex].Y_direction.ToString();
				Z_rotate.Text = Read.NpcMobList[NpcRowIndex].Z_direction.ToString();
				X_scatter.Text = Read.NpcMobList[NpcRowIndex].X_random.ToString();
				Y_scatter.Text = Read.NpcMobList[NpcRowIndex].Y_random.ToString();
				Z_scatter.Text = Read.NpcMobList[NpcRowIndex].Z_random.ToString();
				ExistenceType.SelectedIndex = Read.NpcMobList[NpcRowIndex].Type;
				Group_type.Text = Read.NpcMobList[NpcRowIndex].iGroupType.ToString();
				ExistenceInitGen.Checked = Convert.ToBoolean(Read.NpcMobList[NpcRowIndex].BInitGen);
				ExistenceAutoRevive.Checked = Convert.ToBoolean(Read.NpcMobList[NpcRowIndex].bAutoRevive);
				BValicOnce.Checked = Convert.ToBoolean(Read.NpcMobList[NpcRowIndex].BValicOnce);
				dwGenId.Text = Read.NpcMobList[NpcRowIndex].dwGenId.ToString();
				Trigger.Text = Read.NpcMobList[NpcRowIndex].Trigger_id.ToString();
				Life_time.Text = Read.NpcMobList[NpcRowIndex].Life_time.ToString();
				IMaxNuml.Text = Read.NpcMobList[NpcRowIndex].MaxRespawnTime.ToString();
				int i;
				for (i = 0; i < Read.NpcMobList[NpcRowIndex].Amount_in_group; i++)
				{
					string text = "?";
					int num = Element.ExistenceLists.FindIndex((NpcMonster v) => v.Id == Read.NpcMobList[NpcRowIndex].MobDops[i].Id);
					if (num != -1)
					{
						text = Element.ExistenceLists[num].Name;
					}
					NpcsGroupGrid.Rows.Add(i + 1, Read.NpcMobList[NpcRowIndex].MobDops[i].Id, text);
					if (Read.NpcMobList[i].Type == 1)
					{
						NpcsGroupGrid.Rows[i].Cells[1].Style.ForeColor = Color.FromArgb(251, 251, 107);
						NpcsGroupGrid.Rows[i].Cells[2].Style.ForeColor = Color.FromArgb(251, 251, 107);
					}
				}
			}
			if (MapForm != null && MainProgressBar.Value == 0 && NpcRowCollection.Count != 0 && MapForm.Visible)
			{
				MapForm.GetCoordinates(GetPoint(1));
			}
			UnderExistenceGrid_CellChanged(null, null);
		}
		if (Language == 1)
		{
			ExistenceTab.Text = $"Мобы и Нипы {NpcRowCollection.Count}/{Read.NpcMobsAmount}";
		}
		else
		{
			ExistenceTab.Text = $"Mobs and Npcs {NpcRowCollection.Count}/{Read.NpcMobsAmount}";
		}
	}

	private void UnderExistenceGrid_CellChanged(object sender, EventArgs e)
	{
		if (NpcsGroupGrid.CurrentRow != null && NpcsGroupGrid.CurrentRow.Index != -1)
		{
			UnderNpcRowCollection = (from DataGridViewRow b in NpcsGroupGrid.SelectedRows
				select b.Index).ToList();
			NpcGroupIndex = NpcsGroupGrid.CurrentRow.Index;
			Id_numeric.Value = Read.NpcMobList[NpcRowIndex].MobDops[NpcGroupIndex].Id;
			Amount_numeric.Value = Read.NpcMobList[NpcRowIndex].MobDops[NpcGroupIndex].Amount;
			Respawn_numeric.Value = Read.NpcMobList[NpcRowIndex].MobDops[NpcGroupIndex].Respawn;
			DeathAmount_numeric.Value = Read.NpcMobList[NpcRowIndex].MobDops[NpcGroupIndex].Dead_amount;
			Agression.SelectedIndex = Read.NpcMobList[NpcRowIndex].MobDops[NpcGroupIndex].Agression;
			Path_type.SelectedIndex = Read.NpcMobList[NpcRowIndex].MobDops[NpcGroupIndex].Path_type;
			Path_speed.Value = Read.NpcMobList[NpcRowIndex].MobDops[NpcGroupIndex].Speed;
			Path_numeric.Value = Read.NpcMobList[NpcRowIndex].MobDops[NpcGroupIndex].Path;
			Water_numeric.Value = Convert.ToDecimal(Read.NpcMobList[NpcRowIndex].MobDops[NpcGroupIndex].fOffsetWater);
			Turn_numeric.Value = Convert.ToDecimal(Read.NpcMobList[NpcRowIndex].MobDops[NpcGroupIndex].fOffsetTrn);
			Group_numeric.Value = Read.NpcMobList[NpcRowIndex].MobDops[NpcGroupIndex].Group;
			AskHelp_numeric.Value = Read.NpcMobList[NpcRowIndex].MobDops[NpcGroupIndex].Group_help_sender;
			NeedHelp_numeric.Value = Read.NpcMobList[NpcRowIndex].MobDops[NpcGroupIndex].Group_help_Needer;
			bNeedHelp.Checked = Convert.ToBoolean(Read.NpcMobList[NpcRowIndex].MobDops[NpcGroupIndex].bNeedHelp);
			bFaction.Checked = Convert.ToBoolean(Read.NpcMobList[NpcRowIndex].MobDops[NpcGroupIndex].bFaction);
			bFac_Helper.Checked = Convert.ToBoolean(Read.NpcMobList[NpcRowIndex].MobDops[NpcGroupIndex].bFac_Helper);
			bFac_Accept.Checked = Convert.ToBoolean(Read.NpcMobList[NpcRowIndex].MobDops[NpcGroupIndex].bFac_Accept);
			Deadtime_numeric.Value = Read.NpcMobList[NpcRowIndex].MobDops[NpcGroupIndex].Dead_time;
			RefreshLower_numeric.Value = Read.NpcMobList[NpcRowIndex].MobDops[NpcGroupIndex].RefreshLower;
		}
	}

	private void NpcAndMobsDefaultLeave(object sender, EventArgs e)
	{
		if (NpcRowCollection == null || Read == null)
		{
			return;
		}
		Control control = sender as Control;
		float result2;
		int result;
		switch (control.Name)
		{
		case "ExistenceLocating":
		{
			foreach (int item in NpcRowCollection)
			{
				Read.NpcMobList[item].Location = ExistenceLocating.SelectedIndex;
			}
			break;
		}
		case "X_position":
			float.TryParse(X_position.Text, out result2);
			{
				foreach (int item2 in NpcRowCollection)
				{
					Read.NpcMobList[item2].X_position = result2;
				}
				break;
			}
		case "Y_position":
			float.TryParse(Y_position.Text, out result2);
			{
				foreach (int item3 in NpcRowCollection)
				{
					Read.NpcMobList[item3].Y_position = result2;
				}
				break;
			}
		case "Z_position":
			float.TryParse(Z_position.Text, out result2);
			{
				foreach (int item4 in NpcRowCollection)
				{
					Read.NpcMobList[item4].Z_position = result2;
				}
				break;
			}
		case "X_rotate":
			float.TryParse(X_rotate.Text, out result2);
			{
				foreach (int item5 in NpcRowCollection)
				{
					Read.NpcMobList[item5].X_direction = result2;
				}
				break;
			}
		case "Y_rotate":
			float.TryParse(Y_rotate.Text, out result2);
			{
				foreach (int item6 in NpcRowCollection)
				{
					Read.NpcMobList[item6].Y_direction = result2;
				}
				break;
			}
		case "Z_rotate":
			float.TryParse(Z_rotate.Text, out result2);
			{
				foreach (int item7 in NpcRowCollection)
				{
					Read.NpcMobList[item7].Z_direction = result2;
				}
				break;
			}
		case "X_scatter":
			float.TryParse(X_scatter.Text, out result2);
			{
				foreach (int item8 in NpcRowCollection)
				{
					Read.NpcMobList[item8].X_random = result2;
				}
				break;
			}
		case "Y_scatter":
			float.TryParse(Y_scatter.Text, out result2);
			{
				foreach (int item9 in NpcRowCollection)
				{
					Read.NpcMobList[item9].Y_random = result2;
				}
				break;
			}
		case "Z_scatter":
			float.TryParse(Z_scatter.Text, out result2);
			{
				foreach (int item10 in NpcRowCollection)
				{
					Read.NpcMobList[item10].Z_random = result2;
				}
				break;
			}
		case "ExistenceType":
		{
			foreach (int item11 in NpcRowCollection)
			{
				Read.NpcMobList[item11].Type = ExistenceType.SelectedIndex;
				if (ExistenceType.SelectedIndex == 1)
				{
					NpcMobsGrid.Rows[item11].Cells[1].Style.ForeColor = Color.FromArgb(251, 251, 107);
					NpcMobsGrid.Rows[item11].Cells[2].Style.ForeColor = Color.FromArgb(251, 251, 107);
				}
				else
				{
					NpcMobsGrid.Rows[item11].Cells[1].Style.ForeColor = Color.FromArgb(77, 255, 143);
					NpcMobsGrid.Rows[item11].Cells[2].Style.ForeColor = Color.White;
				}
			}
			break;
		}
		case "Group_type":
			int.TryParse(Group_type.Text, out result);
			{
				foreach (int item12 in NpcRowCollection)
				{
					Read.NpcMobList[item12].iGroupType = result;
				}
				break;
			}
		case "ExistenceInitGen":
		{
			foreach (int item13 in NpcRowCollection)
			{
				Read.NpcMobList[item13].BInitGen = Convert.ToByte(ExistenceInitGen.Checked);
			}
			break;
		}
		case "ExistenceAutoRevive":
		{
			foreach (int item14 in NpcRowCollection)
			{
				Read.NpcMobList[item14].bAutoRevive = Convert.ToByte(ExistenceAutoRevive.Checked);
			}
			break;
		}
		case "BValicOnce":
		{
			foreach (int item15 in NpcRowCollection)
			{
				Read.NpcMobList[item15].BValicOnce = Convert.ToByte(BValicOnce.Checked);
			}
			break;
		}
		case "dwGenId":
			int.TryParse(dwGenId.Text, out result);
			{
				foreach (int item16 in NpcRowCollection)
				{
					Read.NpcMobList[item16].dwGenId = result;
				}
				break;
			}
		case "Trigger":
			int.TryParse(Trigger.Text, out result);
			{
				foreach (int item17 in NpcRowCollection)
				{
					Read.NpcMobList[item17].Trigger_id = result;
				}
				break;
			}
		case "Life_time":
			int.TryParse(Life_time.Text, out result);
			{
				foreach (int item18 in NpcRowCollection)
				{
					Read.NpcMobList[item18].Life_time = result;
				}
				break;
			}
		case "IMaxNuml":
			int.TryParse(IMaxNuml.Text, out result);
			{
				foreach (int item19 in NpcRowCollection)
				{
					Read.NpcMobList[item19].MaxRespawnTime = result;
				}
				break;
			}
		}
	}

	private void NpcAndMobsDefault_EnterPress(object sender, KeyEventArgs e)
	{
		if (e.KeyCode == Keys.Return)
		{
			NpcAndMobsDefaultLeave(sender, null);
		}
	}

	private void UnderNpcAndMobs_Leave(object sender, EventArgs e)
	{
		if (NpcRowCollection == null || Read == null)
		{
			return;
		}
		Control control = sender as Control;
		switch (control.Name)
		{
		case "Id_numeric":
		{
			foreach (int item in NpcRowCollection)
			{
				foreach (int item2 in UnderNpcRowCollection)
				{
					if (Read.NpcMobList[item].Amount_in_group >= item2 + 1)
					{
						Read.NpcMobList[item].MobDops[item2].Id = Convert.ToInt32(Id_numeric.Value);
						NpcsGroupGrid.Rows[item2].Cells[1].Value = Convert.ToInt32(Id_numeric.Value);
						int num = Element.ExistenceLists.FindIndex((NpcMonster c) => c.Id == Convert.ToInt32(Id_numeric.Value));
						if (num != -1)
						{
							NpcsGroupGrid.Rows[item2].Cells[2].Value = Element.ExistenceLists[num].Name;
						}
						else
						{
							NpcsGroupGrid.Rows[item2].Cells[2].Value = "?";
						}
					}
				}
				RefreshRowNpcAndMobs(item);
			}
			break;
		}
		case "Amount_numeric":
		{
			foreach (int item3 in NpcRowCollection)
			{
				foreach (int item4 in UnderNpcRowCollection)
				{
					if (Read.NpcMobList[item3].Amount_in_group >= item4 + 1)
					{
						Read.NpcMobList[item3].MobDops[item4].Amount = Convert.ToInt32(Amount_numeric.Value);
					}
				}
			}
			break;
		}
		case "Respawn_numeric":
		{
			foreach (int item5 in NpcRowCollection)
			{
				foreach (int item6 in UnderNpcRowCollection)
				{
					if (Read.NpcMobList[item5].Amount_in_group >= item6 + 1)
					{
						Read.NpcMobList[item5].MobDops[item6].Respawn = Convert.ToInt32(Respawn_numeric.Value);
					}
				}
			}
			break;
		}
		case "DeathAmount_numeric":
		{
			foreach (int item7 in NpcRowCollection)
			{
				foreach (int item8 in UnderNpcRowCollection)
				{
					if (Read.NpcMobList[item7].Amount_in_group >= item8 + 1)
					{
						Read.NpcMobList[item7].MobDops[item8].Dead_amount = Convert.ToInt32(DeathAmount_numeric.Value);
					}
				}
			}
			break;
		}
		case "Agression":
		{
			foreach (int item9 in NpcRowCollection)
			{
				foreach (int item10 in UnderNpcRowCollection)
				{
					if (Read.NpcMobList[item9].Amount_in_group >= item10 + 1)
					{
						Read.NpcMobList[item9].MobDops[item10].Agression = Agression.SelectedIndex;
					}
				}
			}
			break;
		}
		case "Path_type":
		{
			foreach (int item11 in NpcRowCollection)
			{
				foreach (int item12 in UnderNpcRowCollection)
				{
					if (Read.NpcMobList[item11].Amount_in_group >= item12 + 1)
					{
						Read.NpcMobList[item11].MobDops[item12].Path_type = Path_type.SelectedIndex;
					}
				}
			}
			break;
		}
		case "Path_speed":
		{
			foreach (int item13 in NpcRowCollection)
			{
				foreach (int item14 in UnderNpcRowCollection)
				{
					if (Read.NpcMobList[item13].Amount_in_group >= item14 + 1)
					{
						Read.NpcMobList[item13].MobDops[item14].Speed = Convert.ToInt32(Path_speed.Value);
					}
				}
			}
			break;
		}
		case "Path_numeric":
		{
			foreach (int item15 in NpcRowCollection)
			{
				foreach (int item16 in UnderNpcRowCollection)
				{
					if (Read.NpcMobList[item15].Amount_in_group >= item16 + 1)
					{
						Read.NpcMobList[item15].MobDops[item16].Path = Convert.ToInt32(Path_numeric.Value);
					}
				}
			}
			break;
		}
		case "Water_numeric":
		{
			foreach (int item17 in NpcRowCollection)
			{
				foreach (int item18 in UnderNpcRowCollection)
				{
					if (Read.NpcMobList[item17].Amount_in_group >= item18 + 1)
					{
						Read.NpcMobList[item17].MobDops[item18].fOffsetWater = Convert.ToSingle(Water_numeric.Value);
					}
				}
			}
			break;
		}
		case "Turn_numeric":
		{
			foreach (int item19 in NpcRowCollection)
			{
				foreach (int item20 in UnderNpcRowCollection)
				{
					if (Read.NpcMobList[item19].Amount_in_group >= item20 + 1)
					{
						Read.NpcMobList[item19].MobDops[item20].fOffsetTrn = Convert.ToSingle(Turn_numeric.Value);
					}
				}
			}
			break;
		}
		case "Group_numeric":
		{
			foreach (int item21 in NpcRowCollection)
			{
				foreach (int item22 in UnderNpcRowCollection)
				{
					if (Read.NpcMobList[item21].Amount_in_group >= item22 + 1)
					{
						Read.NpcMobList[item21].MobDops[item22].Group = Convert.ToInt32(Group_numeric.Value);
					}
				}
			}
			break;
		}
		case "AskHelp_numeric":
		{
			foreach (int item23 in NpcRowCollection)
			{
				foreach (int item24 in UnderNpcRowCollection)
				{
					if (Read.NpcMobList[item23].Amount_in_group >= item24 + 1)
					{
						Read.NpcMobList[item23].MobDops[item24].Group_help_sender = Convert.ToInt32(AskHelp_numeric.Value);
					}
				}
			}
			break;
		}
		case "NeedHelp_numeric":
		{
			foreach (int item25 in NpcRowCollection)
			{
				foreach (int item26 in UnderNpcRowCollection)
				{
					if (Read.NpcMobList[item25].Amount_in_group >= item26 + 1)
					{
						Read.NpcMobList[item25].MobDops[item26].Group_help_Needer = Convert.ToInt32(NeedHelp_numeric.Value);
					}
				}
			}
			break;
		}
		case "bNeedHelp":
		{
			foreach (int item27 in NpcRowCollection)
			{
				foreach (int item28 in UnderNpcRowCollection)
				{
					if (Read.NpcMobList[item27].Amount_in_group >= item28 + 1)
					{
						Read.NpcMobList[item27].MobDops[item28].bNeedHelp = Convert.ToByte(bNeedHelp.Checked);
					}
				}
			}
			break;
		}
		case "bFac_Accept":
		{
			foreach (int item29 in NpcRowCollection)
			{
				foreach (int item30 in UnderNpcRowCollection)
				{
					if (Read.NpcMobList[item29].Amount_in_group >= item30 + 1)
					{
						Read.NpcMobList[item29].MobDops[item30].bFac_Accept = Convert.ToByte(bFac_Accept.Checked);
					}
				}
			}
			break;
		}
		case "bFac_Helper":
		{
			foreach (int item31 in NpcRowCollection)
			{
				foreach (int item32 in UnderNpcRowCollection)
				{
					if (Read.NpcMobList[item31].Amount_in_group >= item32 + 1)
					{
						Read.NpcMobList[item31].MobDops[item32].bFac_Helper = Convert.ToByte(bFac_Helper.Checked);
					}
				}
			}
			break;
		}
		case "bFaction":
		{
			foreach (int item33 in NpcRowCollection)
			{
				foreach (int item34 in UnderNpcRowCollection)
				{
					if (Read.NpcMobList[item33].Amount_in_group >= item34 + 1)
					{
						Read.NpcMobList[item33].MobDops[item34].bFaction = Convert.ToByte(bFaction.Checked);
					}
				}
			}
			break;
		}
		case "Deadtime_numeric":
		{
			foreach (int item35 in NpcRowCollection)
			{
				foreach (int item36 in UnderNpcRowCollection)
				{
					if (Read.NpcMobList[item35].Amount_in_group >= item36 + 1)
					{
						Read.NpcMobList[item35].MobDops[item36].Dead_time = Convert.ToInt32(Deadtime_numeric.Value);
					}
				}
			}
			break;
		}
		case "RefreshLower_numeric":
		{
			foreach (int item37 in NpcRowCollection)
			{
				foreach (int item38 in UnderNpcRowCollection)
				{
					if (Read.NpcMobList[item37].Amount_in_group >= item38 + 1)
					{
						Read.NpcMobList[item37].MobDops[item38].RefreshLower = Convert.ToInt32(RefreshLower_numeric.Value);
					}
				}
			}
			break;
		}
		}
	}

	private void UnderNpcAndMobs_EnterPress(object sender, KeyEventArgs e)
	{
		if (e.KeyCode == Keys.Return)
		{
			UnderNpcAndMobs_Leave(sender, null);
		}
	}

	private void CloneNpcAndMobFull(object sender, EventArgs e)
	{
		if (NpcMobsGrid.Rows.Count > 0)
		{
			NpcMobsGrid.ScrollBars = ScrollBars.None;
			NpcRowCollection = NpcRowCollection.OrderBy((int z) => z).ToList();
			foreach (int item3 in NpcRowCollection)
			{
				Read.NpcMobsAmount++;
				ClassDefaultMonsters classDefaultMonsters = new ClassDefaultMonsters
				{
					Amount_in_group = Read.NpcMobList[item3].Amount_in_group,
					bAutoRevive = Read.NpcMobList[item3].bAutoRevive,
					BInitGen = Read.NpcMobList[item3].BInitGen,
					BValicOnce = Read.NpcMobList[item3].BValicOnce,
					dwGenId = Read.NpcMobList[item3].dwGenId,
					iGroupType = Read.NpcMobList[item3].iGroupType,
					MaxRespawnTime = Read.NpcMobList[item3].MaxRespawnTime,
					Type = Read.NpcMobList[item3].Type,
					Life_time = Read.NpcMobList[item3].Life_time,
					Trigger_id = Read.NpcMobList[item3].Trigger_id,
					Location = Read.NpcMobList[item3].Location,
					X_direction = Read.NpcMobList[item3].X_direction,
					X_position = Read.NpcMobList[item3].X_position,
					X_random = Read.NpcMobList[item3].X_random,
					Y_direction = Read.NpcMobList[item3].Y_direction,
					Y_position = Read.NpcMobList[item3].Y_position,
					Y_random = Read.NpcMobList[item3].Y_random,
					Z_direction = Read.NpcMobList[item3].Z_direction,
					Z_position = Read.NpcMobList[item3].Z_position,
					Z_random = Read.NpcMobList[item3].Z_random,
					MobDops = new List<ClassExtraMonsters>()
				};
				for (int i = 0; i < Read.NpcMobList[item3].Amount_in_group; i++)
				{
					ClassExtraMonsters item = new ClassExtraMonsters
					{
						Agression = Read.NpcMobList[item3].MobDops[i].Agression,
						Amount = Read.NpcMobList[item3].MobDops[i].Amount,
						bFac_Accept = Read.NpcMobList[item3].MobDops[i].bFac_Accept,
						bFac_Helper = Read.NpcMobList[item3].MobDops[i].bFac_Helper,
						bFaction = Read.NpcMobList[item3].MobDops[i].bFaction,
						bNeedHelp = Read.NpcMobList[item3].MobDops[i].bNeedHelp,
						Dead_amount = Read.NpcMobList[item3].MobDops[i].Dead_amount,
						Dead_time = Read.NpcMobList[item3].MobDops[i].Dead_time,
						fOffsetTrn = Read.NpcMobList[item3].MobDops[i].fOffsetTrn,
						fOffsetWater = Read.NpcMobList[item3].MobDops[i].fOffsetWater,
						Group = Read.NpcMobList[item3].MobDops[i].Group,
						Group_help_Needer = Read.NpcMobList[item3].MobDops[i].Group_help_Needer,
						Group_help_sender = Read.NpcMobList[item3].MobDops[i].Group_help_sender,
						Id = Read.NpcMobList[item3].MobDops[i].Id,
						Path = Read.NpcMobList[item3].MobDops[i].Path,
						Path_type = Read.NpcMobList[item3].MobDops[i].Path_type,
						RefreshLower = Read.NpcMobList[item3].MobDops[i].RefreshLower,
						Respawn = Read.NpcMobList[item3].MobDops[i].Respawn,
						Speed = Read.NpcMobList[item3].MobDops[i].Speed
					};
					classDefaultMonsters.MobDops.Add(item);
				}
				Read.NpcMobList.Add(classDefaultMonsters);
				NpcMobsGrid.Rows.Add(NpcMobsGrid.Rows.Count, NpcMobsGrid.Rows[item3].Cells[1].Value, NpcMobsGrid.Rows[item3].Cells[2].Value);
			}
			NpcMobsGrid.ScrollBars = ScrollBars.Vertical;
			List<int> npcRowCollection = NpcRowCollection;
			NpcMobsGrid.CurrentCell = NpcMobsGrid.Rows[NpcMobsGrid.Rows.Count - 1].Cells[1];
			for (int j = 1; j <= npcRowCollection.Count; j++)
			{
				NpcMobsGrid.Rows[NpcMobsGrid.Rows.Count - j].Selected = true;
			}
			ExistenceGrid_CellChanged(null, null);
		}
		else if (Read != null)
		{
			Read.NpcMobsAmount++;
			ClassDefaultMonsters classDefaultMonsters2 = new ClassDefaultMonsters
			{
				Location = 0,
				Type = 0,
				Amount_in_group = 1,
				MobDops = new List<ClassExtraMonsters>()
			};
			ClassExtraMonsters item2 = new ClassExtraMonsters
			{
				Id = 16,
				Amount = 1,
				Respawn = 30
			};
			classDefaultMonsters2.MobDops.Add(item2);
			Read.NpcMobList.Add(classDefaultMonsters2);
			if (Language == 1)
			{
				NpcMobsGrid.Rows.Add(1, 16, "Зеленый мотыль");
			}
			else
			{
				NpcMobsGrid.Rows.Add(1, 16, "Green WaterBeetle");
			}
			ExistenceGrid_CellChanged(null, null);
		}
		if (Language == 1)
		{
			ExistenceTab.Text = $"Мобы и Нипы {NpcRowCollection.Count}/{Read.NpcMobsAmount}";
		}
		else
		{
			ExistenceTab.Text = $"Mobs and Npcs {NpcRowCollection.Count}/{Read.NpcMobsAmount}";
		}
	}

	private void RemoveNpcAndMobFull(object sender, EventArgs e)
	{
		if (Read == null || NpcRowCollection.Count == 0)
		{
			return;
		}
		string text = "Вы уверены,что хотите удалить выбранные объекты?";
		if (Language == 2)
		{
			text = "Are you sure that you want to delete selected objects?";
		}
		DialogResult dialogResult = MessageBox.Show(text, "Npcgen Editor", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);
		if (dialogResult != DialogResult.Yes)
		{
			return;
		}
		int num = NpcRowCollection.Min();
		NpcMobsGrid.ScrollBars = ScrollBars.None;
		ErrorsGrid.ScrollBars = ScrollBars.None;
		AllowCellChanging = false;
		NpcMobsGrid.ClearSelection();
		MainProgressBar.Maximum = NpcRowCollection.Count;
		Read.NpcMobsAmount -= NpcRowCollection.Count;
		foreach (int i in NpcRowCollection)
		{
			List<IntDictionary> Matched = (from f in ErrorExistenceCollection
				where f.GridIndex == i
				orderby f.ErrorInex descending
				select f).ToList();
			foreach (IntDictionary item in Matched)
			{
				ErrorsGrid.Rows.RemoveAt(item.ErrorInex);
				ErrorExistenceCollection.RemoveAt(item.ErrorInex);
			}
			ErrorExistenceCollection.Where((IntDictionary b) => b.GridIndex > i).ToList().ForEach(delegate(IntDictionary s)
			{
				s.ErrorInex -= Matched.Count;
			});
			ErrorExistenceCollection.Where((IntDictionary a) => a.GridIndex > i).ToList().ForEach(delegate(IntDictionary s)
			{
				s.GridIndex--;
			});
			ErrorResourcesCollection.ForEach(delegate(IntDictionary s)
			{
				s.ErrorInex -= Matched.Count;
			});
			ErrorDynamicsCollection.ForEach(delegate(IntDictionary s)
			{
				s.ErrorInex -= Matched.Count;
			});
			Read.NpcMobList.RemoveAt(i);
			NpcMobsGrid.Rows.RemoveAt(i);
			MainProgressBar.Value++;
		}
		if (ErrorsGrid.Rows.Count != 0)
		{
			for (int j = 0; j < ErrorExistenceCollection.Count; j++)
			{
				ErrorsGrid.Rows[j].Cells[0].Value = j + 1;
				ErrorsGrid.Rows[j].Cells[1].Value = ErrorExistenceCollection[j].GridIndex + 1;
			}
			int num2 = ((ErrorResourcesCollection.Count != 0) ? ErrorResourcesCollection.Min((IntDictionary f) => f.ErrorInex) : 0);
			for (int k = num2; k <= num2; k++)
			{
				ErrorsGrid.Rows[k].Cells[0].Value = k + 1;
			}
			int num3 = ((ErrorDynamicsCollection.Count != 0) ? ErrorDynamicsCollection.Min((IntDictionary f) => f.ErrorInex) : 0);
			for (int l = num3; l <= num3; l++)
			{
				ErrorsGrid.Rows[l].Cells[0].Value = l + 1;
			}
		}
		AllowCellChanging = true;
		MainProgressBar.Value = 0;
		NpcMobsGrid.ScrollBars = ScrollBars.Vertical;
		ErrorsGrid.ScrollBars = ScrollBars.Vertical;
		if (NpcMobsGrid.Rows.Count > num)
		{
			NpcMobsGrid.CurrentCell = NpcMobsGrid.Rows[num].Cells[1];
			NpcMobsGrid.FirstDisplayedScrollingRowIndex = num;
		}
		else if (NpcMobsGrid.Rows.Count != 0)
		{
			NpcMobsGrid.CurrentCell = NpcMobsGrid.Rows[NpcMobsGrid.Rows.Count - 1].Cells[1];
			NpcMobsGrid.FirstDisplayedScrollingRowIndex = NpcMobsGrid.Rows.Count - 1;
		}
		ExistenceGrid_CellChanged(null, null);
		for (int m = 0; m < NpcMobsGrid.Rows.Count; m++)
		{
			NpcMobsGrid.Rows[m].Cells[0].Value = m + 1;
		}
		if (Language == 1)
		{
			ExistenceTab.Text = $"Мобы и Нипы 1/{Read.NpcMobsAmount}";
		}
		else
		{
			ExistenceTab.Text = $"Mobs and Npcs 1/{Read.NpcMobsAmount}";
		}
	}

	private void Id_numeric_DoubleClick(object sender, EventArgs e)
	{
		if (ChooseFromElementsForm == null)
		{
			return;
		}
		ChooseFromElementsForm.SetAction = 1;
		ChooseFromElementsForm.SetWindow = 1;
		int num = Element.ExistenceLists.FindIndex((NpcMonster z) => z.Id == Convert.ToInt32(Id_numeric.Value));
		if (num != -1)
		{
			if (num >= Element.MonsterdAmount)
			{
				ChooseFromElementsForm.FindRow(num - Element.MonsterdAmount, "Npc");
			}
			else
			{
				ChooseFromElementsForm.FindRow(num, "Mob");
			}
		}
		else if (Language == 1)
		{
			MessageBox.Show("ID не найдено в elements.data!!...", "Npcgen Editor", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
		}
		else if (Language == 2)
		{
			MessageBox.Show("ID not found in elements.data!!...", "Npcgen Editor", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
		}
		ChooseFromElementsForm.ShowDialog(this);
	}

	private void CloneNpcinGroupButton_Click(object sender, EventArgs e)
	{
		if (NpcMobsGrid.Rows.Count > 0 && NpcsGroupGrid.Rows.Count > 0)
		{
			UnderNpcRowCollection = UnderNpcRowCollection.OrderBy((int z) => z).ToList();
			foreach (int item2 in UnderNpcRowCollection)
			{
				Read.NpcMobList[NpcRowIndex].Amount_in_group++;
				ClassExtraMonsters item = new ClassExtraMonsters
				{
					Agression = Read.NpcMobList[NpcRowIndex].MobDops[item2].Agression,
					Amount = Read.NpcMobList[NpcRowIndex].MobDops[item2].Agression,
					bFac_Accept = Read.NpcMobList[NpcRowIndex].MobDops[item2].bFac_Accept,
					bFac_Helper = Read.NpcMobList[NpcRowIndex].MobDops[item2].bFac_Helper,
					bFaction = Read.NpcMobList[NpcRowIndex].MobDops[item2].bFaction,
					bNeedHelp = Read.NpcMobList[NpcRowIndex].MobDops[item2].bNeedHelp,
					Dead_amount = Read.NpcMobList[NpcRowIndex].MobDops[item2].Dead_amount,
					Dead_time = Read.NpcMobList[NpcRowIndex].MobDops[item2].Dead_time,
					fOffsetTrn = Read.NpcMobList[NpcRowIndex].MobDops[item2].fOffsetTrn,
					fOffsetWater = Read.NpcMobList[NpcRowIndex].MobDops[item2].fOffsetWater,
					Group = Read.NpcMobList[NpcRowIndex].MobDops[item2].Group,
					Group_help_Needer = Read.NpcMobList[NpcRowIndex].MobDops[item2].Group_help_Needer,
					Group_help_sender = Read.NpcMobList[NpcRowIndex].MobDops[item2].Group_help_sender,
					Id = Read.NpcMobList[NpcRowIndex].MobDops[item2].Id,
					Path = Read.NpcMobList[NpcRowIndex].MobDops[item2].Path,
					Path_type = Read.NpcMobList[NpcRowIndex].MobDops[item2].Path_type,
					RefreshLower = Read.NpcMobList[NpcRowIndex].MobDops[item2].RefreshLower,
					Respawn = Read.NpcMobList[NpcRowIndex].MobDops[item2].Respawn,
					Speed = Read.NpcMobList[NpcRowIndex].MobDops[item2].Speed
				};
				Read.NpcMobList[NpcRowIndex].MobDops.Add(item);
				NpcsGroupGrid.Rows.Add(Read.NpcMobList[NpcRowIndex].Amount_in_group, NpcsGroupGrid.Rows[item2].Cells[1].Value, NpcsGroupGrid.Rows[item2].Cells[2].Value);
			}
			RefreshRowNpcAndMobs(NpcRowIndex);
			NpcsGroupGrid.ClearSelection();
			for (int i = 1; i <= UnderNpcRowCollection.Count; i++)
			{
				NpcsGroupGrid.Rows[NpcsGroupGrid.Rows.Count - i].Selected = true;
			}
			NpcsGroupGrid.CurrentCell = NpcsGroupGrid.Rows[NpcsGroupGrid.Rows.Count - 1].Cells[1];
			NpcsGroupGrid.FirstDisplayedScrollingRowIndex = NpcsGroupGrid.Rows.Count - 1;
		}
		else
		{
			ClassExtraMonsters classExtraMonsters = new ClassExtraMonsters();
			Read.NpcMobList[NpcRowIndex].Amount_in_group++;
			classExtraMonsters.Id = 16;
			classExtraMonsters.Amount = 1;
			classExtraMonsters.Respawn = 60;
			Read.NpcMobList[NpcRowIndex].MobDops.Add(classExtraMonsters);
			if (Language == 1)
			{
				NpcsGroupGrid.Rows.Add(1, 16, "Зеленый мотыль");
			}
			else if (Language == 2)
			{
				NpcsGroupGrid.Rows.Add(1, 16, "Green WaterBeetle");
			}
			RefreshRowNpcAndMobs(NpcRowIndex);
			UnderExistenceGrid_CellChanged(null, null);
		}
	}

	private void DeleteNpcinGroupButton_Click(object sender, EventArgs e)
	{
		if (UnderNpcRowCollection == null || NpcMobsGrid.Rows.Count <= 0 || NpcsGroupGrid.Rows.Count <= 0)
		{
			return;
		}
		string text = "Вы уверены,что хотите удалить выбранные объекты?";
		if (Language == 2)
		{
			text = "Are you sure that you want to delete selected objects?";
		}
		DialogResult dialogResult = MessageBox.Show(text, "Npcgen Editor", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);
		if (dialogResult != DialogResult.Yes)
		{
			return;
		}
		UnderNpcRowCollection = UnderNpcRowCollection.OrderByDescending((int z) => z).ToList();
		int num = UnderNpcRowCollection.Min();
		NpcsGroupGrid.ClearSelection();
		foreach (int item in UnderNpcRowCollection)
		{
			Read.NpcMobList[NpcRowIndex].Amount_in_group--;
			Read.NpcMobList[NpcRowIndex].MobDops.RemoveAt(item);
			NpcsGroupGrid.Rows.RemoveAt(item);
		}
		RefreshRowNpcAndMobs(NpcRowIndex);
		if (NpcsGroupGrid.Rows.Count > num)
		{
			NpcsGroupGrid.CurrentCell = NpcsGroupGrid.Rows[num].Cells[1];
			NpcsGroupGrid.FirstDisplayedScrollingRowIndex = num;
		}
		else if (NpcsGroupGrid.Rows.Count != 0)
		{
			NpcsGroupGrid.CurrentCell = NpcsGroupGrid.Rows[NpcsGroupGrid.Rows.Count - 1].Cells[1];
			NpcsGroupGrid.FirstDisplayedScrollingRowIndex = NpcsGroupGrid.Rows.Count - 1;
		}
		UnderExistenceGrid_CellChanged(null, null);
	}

	public void RefreshRowNpcAndMobs(int index)
	{
		int[] Id_joined = new int[Read.NpcMobList[index].Amount_in_group];
		string[] array = new string[Read.NpcMobList[index].Amount_in_group];
		int i;
		for (i = 0; i < Read.NpcMobList[index].Amount_in_group; i++)
		{
			Id_joined[i] = Read.NpcMobList[index].MobDops[i].Id;
			int num = Element.ExistenceLists.FindIndex((NpcMonster v) => v.Id == Id_joined[i]);
			if (num != -1)
			{
				array[i] = Element.ExistenceLists[num].Name;
			}
			else
			{
				array[i] = "?";
			}
		}
		NpcMobsGrid.Rows[index].Cells[1].Value = string.Join(",", Id_joined);
		NpcMobsGrid.Rows[index].Cells[2].Value = string.Join(",", array);
	}

	private void InsertCordsFromGame_Click(object sender, EventArgs e)
	{
		ClassPosition coordinates = GetCoordinates();
		if (coordinates != null)
		{
			X_position.Text = coordinates.PosX.ToString();
			Y_position.Text = coordinates.PosY.ToString();
			Z_position.Text = coordinates.PosZ.ToString();
			X_rotate.Text = coordinates.DirX.ToString();
			Y_rotate.Text = coordinates.DirY.ToString();
			Z_rotate.Text = coordinates.DirZ.ToString();
			NpcAndMobsDefaultLeave(X_position, null);
			NpcAndMobsDefaultLeave(Y_position, null);
			NpcAndMobsDefaultLeave(Z_position, null);
			NpcAndMobsDefaultLeave(X_rotate, null);
			NpcAndMobsDefaultLeave(Y_rotate, null);
			NpcAndMobsDefaultLeave(Z_rotate, null);
		}
	}

	private void ResourcesGrid_CellMouseEnter(object sender, DataGridViewCellEventArgs e)
	{
		if ((e.ColumnIndex > 0) & (e.RowIndex >= 0))
		{
			string caption = Convert.ToString(ResourcesGrid.Rows[e.RowIndex].Cells[e.ColumnIndex].Value);
			toolTip1.SetToolTip(ResourcesGrid, caption);
		}
	}

	private void ResourcesGrid_CurrentCellChanged(object sender, EventArgs e)
	{
		if (AllowCellChanging)
		{
			ResourcesRowCollection = (from DataGridViewRow f in ResourcesGrid.SelectedRows
				select f.Index into t
				orderby t descending
				select t).ToList();
			if (ResourcesGrid.CurrentRow != null)
			{
				ResourcesRowIndex = ResourcesGrid.CurrentRow.Index;
				if (ResourcesRowIndex != -1)
				{
					RX_position.Text = Read.ResourcesList[ResourcesRowIndex].X_position.ToString();
					RY_position.Text = Read.ResourcesList[ResourcesRowIndex].Y_position.ToString();
					RZ_position.Text = Read.ResourcesList[ResourcesRowIndex].Z_position.ToString();
					RX_Random.Text = Read.ResourcesList[ResourcesRowIndex].X_Random.ToString();
					RZ_Random.Text = Read.ResourcesList[ResourcesRowIndex].Z_Random.ToString();
					RInCline1.Text = Read.ResourcesList[ResourcesRowIndex].InCline1.ToString();
					RInCline2.Text = Read.ResourcesList[ResourcesRowIndex].InCline2.ToString();
					RRotation.Text = Read.ResourcesList[ResourcesRowIndex].Rotation.ToString();
					RGroup_amount_textbox.Text = Read.ResourcesList[ResourcesRowIndex].Amount_in_group.ToString();
					RdwGenID.Text = Read.ResourcesList[ResourcesRowIndex].dwGenID.ToString();
					RTriggerID.Text = Read.ResourcesList[ResourcesRowIndex].Trigger_id.ToString();
					RIMaxNuml.Text = Read.ResourcesList[ResourcesRowIndex].IMaxNum.ToString();
					ResourcesInitGen.Checked = Convert.ToBoolean(Read.ResourcesList[ResourcesRowIndex].bInitGen);
					ResourcesAutoRevive.Checked = Convert.ToBoolean(Read.ResourcesList[ResourcesRowIndex].bAutoRevive);
					RBValidOnce.Checked = Convert.ToBoolean(Read.ResourcesList[ResourcesRowIndex].bValidOnce);
					ResourcesGroupGrid.Rows.Clear();
					int i;
					for (i = 0; i < Read.ResourcesList[ResourcesRowIndex].Amount_in_group; i++)
					{
						string text = "?";
						int num = Element.ResourcesList.FindIndex((NpcMonster v) => v.Id == Read.ResourcesList[ResourcesRowIndex].ResExtra[i].Id);
						if (num != -1)
						{
							text = Element.ResourcesList[num].Name;
						}
						ResourcesGroupGrid.Rows.Add(i + 1, Read.ResourcesList[ResourcesRowIndex].ResExtra[i].Id, text);
					}
					if (MapForm != null && MainProgressBar.Value == 0 && ResourcesRowCollection.Count != 0 && MapForm.Visible)
					{
						MapForm.GetCoordinates(GetPoint(2));
					}
					ResourcesGroupGrid_CurrentCellChanged(null, null);
				}
			}
		}
		if (Language == 1)
		{
			ResourcesTab.Text = $"Ресурсы {ResourcesRowCollection.Count}/{Read.ResourcesAmount}";
		}
		else
		{
			ResourcesTab.Text = $"Resources {ResourcesRowCollection.Count}/{Read.ResourcesAmount}";
		}
	}

	private void ResourcesGroupGrid_CurrentCellChanged(object sender, EventArgs e)
	{
		if (ResourcesGroupGrid.CurrentRow == null || ResourcesGroupGrid.CurrentRow.Index == -1)
		{
			return;
		}
		UnderResourcesRowCollection = new List<int>(ResourcesGroupGrid.SelectedRows.Count);
		foreach (DataGridViewRow selectedRow in ResourcesGroupGrid.SelectedRows)
		{
			UnderResourcesRowCollection.Add(selectedRow.Index);
		}
		ResourcesGroupIndex = ResourcesGroupGrid.CurrentRow.Index;
		RId_numeric.Value = Read.ResourcesList[ResourcesRowIndex].ResExtra[ResourcesGroupIndex].Id;
		RAmount_numeric.Value = Read.ResourcesList[ResourcesRowIndex].ResExtra[ResourcesGroupIndex].Amount;
		RRespawn_numeric.Value = Read.ResourcesList[ResourcesRowIndex].ResExtra[ResourcesGroupIndex].Respawntime;
		RType_numeric.Value = Read.ResourcesList[ResourcesRowIndex].ResExtra[ResourcesGroupIndex].ResourceType;
		RfHeiOff_numeric.Value = Convert.ToDecimal(Read.ResourcesList[ResourcesRowIndex].ResExtra[ResourcesGroupIndex].fHeiOff);
	}

	private void ResourcesDefault_EnterPress(object sender, KeyEventArgs e)
	{
		if (e.KeyCode == Keys.Return)
		{
			ResourcesDefaultLeave(sender, null);
		}
	}

	private void ResourcesDefaultLeave(object sender, EventArgs e)
	{
		Control control = sender as Control;
		float result3;
		byte result2;
		int result;
		switch (control.Name)
		{
		case "RX_position":
			float.TryParse(RX_position.Text, out result3);
			{
				foreach (int item in ResourcesRowCollection)
				{
					Read.ResourcesList[item].X_position = result3;
				}
				break;
			}
		case "RY_position":
			float.TryParse(RY_position.Text, out result3);
			{
				foreach (int item2 in ResourcesRowCollection)
				{
					Read.ResourcesList[item2].Y_position = result3;
				}
				break;
			}
		case "RZ_position":
			float.TryParse(RZ_position.Text, out result3);
			{
				foreach (int item3 in ResourcesRowCollection)
				{
					Read.ResourcesList[item3].Z_position = result3;
				}
				break;
			}
		case "RX_Random":
			float.TryParse(RX_Random.Text, out result3);
			{
				foreach (int item4 in ResourcesRowCollection)
				{
					Read.ResourcesList[item4].X_Random = result3;
				}
				break;
			}
		case "RZ_Random":
			float.TryParse(RZ_Random.Text, out result3);
			{
				foreach (int item5 in ResourcesRowCollection)
				{
					Read.ResourcesList[item5].Z_Random = result3;
				}
				break;
			}
		case "RInCline1":
			byte.TryParse(RInCline1.Text, out result2);
			{
				foreach (int item6 in ResourcesRowCollection)
				{
					Read.ResourcesList[item6].InCline1 = result2;
				}
				break;
			}
		case "RInCline2":
			byte.TryParse(RInCline2.Text, out result2);
			{
				foreach (int item7 in ResourcesRowCollection)
				{
					Read.ResourcesList[item7].InCline2 = result2;
				}
				break;
			}
		case "RRotation":
			byte.TryParse(RRotation.Text, out result2);
			{
				foreach (int item8 in ResourcesRowCollection)
				{
					Read.ResourcesList[item8].Rotation = result2;
				}
				break;
			}
		case "RdwGenID":
			int.TryParse(RdwGenID.Text, out result);
			{
				foreach (int item9 in ResourcesRowCollection)
				{
					Read.ResourcesList[item9].dwGenID = result;
				}
				break;
			}
		case "RTriggerID":
			int.TryParse(RTriggerID.Text, out result);
			{
				foreach (int item10 in ResourcesRowCollection)
				{
					Read.ResourcesList[item10].Trigger_id = result;
				}
				break;
			}
		case "RIMaxNuml":
			int.TryParse(RIMaxNuml.Text, out result);
			{
				foreach (int item11 in ResourcesRowCollection)
				{
					Read.ResourcesList[item11].IMaxNum = result;
				}
				break;
			}
		case "ResourcesInitGen":
		{
			foreach (int item12 in ResourcesRowCollection)
			{
				Read.ResourcesList[item12].bInitGen = Convert.ToByte(ResourcesInitGen.Checked);
			}
			break;
		}
		case "ResourcesAutoRevive":
		{
			foreach (int item13 in ResourcesRowCollection)
			{
				Read.ResourcesList[item13].bAutoRevive = Convert.ToByte(ResourcesAutoRevive.Checked);
			}
			break;
		}
		case "RBValidOnce":
		{
			foreach (int item14 in ResourcesRowCollection)
			{
				Read.ResourcesList[item14].bValidOnce = Convert.ToByte(RBValidOnce.Checked);
			}
			break;
		}
		}
	}

	private void UnderResources_EnterPress(object sender, KeyEventArgs e)
	{
		if (e.KeyCode == Keys.Return)
		{
			UnderResourcesLeave(sender, null);
		}
	}

	private void UnderResourcesLeave(object sender, EventArgs e)
	{
		Control control = sender as Control;
		if (UnderResourcesRowCollection == null)
		{
			return;
		}
		switch (control.Name)
		{
		case "RId_numeric":
		{
			foreach (int item in ResourcesRowCollection)
			{
				foreach (int item2 in UnderResourcesRowCollection)
				{
					if (Read.ResourcesList[item].Amount_in_group >= item2 + 1)
					{
						Read.ResourcesList[item].ResExtra[item2].Id = Convert.ToInt32(RId_numeric.Value);
						ResourcesGroupGrid.Rows[item2].Cells[1].Value = Convert.ToInt32(RId_numeric.Value);
						int num = Element.ResourcesList.FindIndex((NpcMonster c) => c.Id == Convert.ToInt32(RId_numeric.Value));
						if (num != -1)
						{
							ResourcesGroupGrid.Rows[item2].Cells[2].Value = Element.ResourcesList[num].Name;
						}
						else
						{
							ResourcesGroupGrid.Rows[item2].Cells[2].Value = "?";
						}
					}
				}
				RefreshResourcesRow(item);
			}
			break;
		}
		case "RAmount_numeric":
		{
			foreach (int item3 in ResourcesRowCollection)
			{
				foreach (int item4 in UnderResourcesRowCollection)
				{
					if (Read.ResourcesList[item3].Amount_in_group >= item4 + 1)
					{
						Read.ResourcesList[item3].ResExtra[item4].Amount = Convert.ToInt32(RAmount_numeric.Value);
					}
				}
			}
			break;
		}
		case "RRespawn_numeric":
		{
			foreach (int item5 in ResourcesRowCollection)
			{
				foreach (int item6 in UnderResourcesRowCollection)
				{
					if (Read.ResourcesList[item5].Amount_in_group >= item6 + 1)
					{
						Read.ResourcesList[item5].ResExtra[item6].Respawntime = Convert.ToInt32(RRespawn_numeric.Value);
					}
				}
			}
			break;
		}
		case "RType_numeric":
		{
			foreach (int item7 in ResourcesRowCollection)
			{
				foreach (int item8 in UnderResourcesRowCollection)
				{
					if (Read.ResourcesList[item7].Amount_in_group >= item8 + 1)
					{
						Read.ResourcesList[item7].ResExtra[item8].ResourceType = Convert.ToInt32(RType_numeric.Value);
					}
				}
			}
			break;
		}
		case "RfHeiOff_numeric":
		{
			foreach (int item9 in ResourcesRowCollection)
			{
				foreach (int item10 in UnderResourcesRowCollection)
				{
					if (Read.ResourcesList[item9].Amount_in_group >= item10 + 1)
					{
						Read.ResourcesList[item9].ResExtra[item10].fHeiOff = Convert.ToSingle(RfHeiOff_numeric.Value);
					}
				}
			}
			break;
		}
		}
	}

	public void RefreshResourcesRow(int index)
	{
		int[] Id_joined = new int[Read.ResourcesList[index].Amount_in_group];
		string[] array = new string[Read.ResourcesList[index].Amount_in_group];
		int i;
		for (i = 0; i < Read.ResourcesList[index].Amount_in_group; i++)
		{
			Id_joined[i] = Read.ResourcesList[index].ResExtra[i].Id;
			int num = Element.ResourcesList.FindIndex((NpcMonster v) => v.Id == Id_joined[i]);
			if (num != -1)
			{
				array[i] = Element.ResourcesList[num].Name;
			}
			else
			{
				array[i] = "?";
			}
		}
		ResourcesGrid.Rows[index].Cells[1].Value = string.Join(",", Id_joined);
		ResourcesGrid.Rows[index].Cells[2].Value = string.Join(",", array);
	}

	private void CloneResurcesFull_Click(object sender, EventArgs e)
	{
		if (ResourcesGrid.Rows.Count > 0)
		{
			ResourcesGrid.ScrollBars = ScrollBars.None;
			ResourcesRowCollection = ResourcesRowCollection.OrderBy((int z) => z).ToList();
			foreach (int item3 in ResourcesRowCollection)
			{
				Read.ResourcesAmount++;
				ClassDefaultResources classDefaultResources = new ClassDefaultResources
				{
					Amount_in_group = Read.ResourcesList[item3].Amount_in_group,
					X_position = Read.ResourcesList[item3].X_position,
					Y_position = Read.ResourcesList[item3].Y_position,
					Z_position = Read.ResourcesList[item3].Z_position,
					bAutoRevive = Read.ResourcesList[item3].bAutoRevive,
					bInitGen = Read.ResourcesList[item3].bInitGen,
					bValidOnce = Read.ResourcesList[item3].bValidOnce,
					dwGenID = Read.ResourcesList[item3].dwGenID,
					IMaxNum = Read.ResourcesList[item3].IMaxNum,
					Trigger_id = Read.ResourcesList[item3].Trigger_id,
					InCline1 = Read.ResourcesList[item3].InCline1,
					X_Random = Read.ResourcesList[item3].X_Random,
					InCline2 = Read.ResourcesList[item3].InCline2,
					Z_Random = Read.ResourcesList[item3].Z_Random,
					Rotation = Read.ResourcesList[item3].Rotation,
					ResExtra = new List<ClassExtraResources>()
				};
				for (int i = 0; i < Read.ResourcesList[item3].Amount_in_group; i++)
				{
					ClassExtraResources item = new ClassExtraResources
					{
						Id = Read.ResourcesList[item3].ResExtra[i].Id,
						ResourceType = Read.ResourcesList[item3].ResExtra[i].ResourceType,
						Respawntime = Read.ResourcesList[item3].ResExtra[i].Respawntime,
						Amount = Read.ResourcesList[item3].ResExtra[i].Amount,
						fHeiOff = Read.ResourcesList[item3].ResExtra[i].fHeiOff
					};
					classDefaultResources.ResExtra.Add(item);
				}
				Read.ResourcesList.Add(classDefaultResources);
				ResourcesGrid.Rows.Add(Read.ResourcesAmount, ResourcesGrid.Rows[item3].Cells[1].Value, ResourcesGrid.Rows[item3].Cells[2].Value);
			}
			ResourcesGrid.ScrollBars = ScrollBars.Vertical;
			List<int> resourcesRowCollection = ResourcesRowCollection;
			ResourcesGrid.CurrentCell = ResourcesGrid.Rows[ResourcesGrid.Rows.Count - 1].Cells[1];
			for (int j = 1; j <= resourcesRowCollection.Count; j++)
			{
				ResourcesGrid.Rows[ResourcesGrid.Rows.Count - j].Selected = true;
			}
			ResourcesGrid_CurrentCellChanged(null, null);
		}
		else
		{
			Read.ResourcesAmount++;
			ClassDefaultResources classDefaultResources2 = new ClassDefaultResources
			{
				Amount_in_group = 1,
				ResExtra = new List<ClassExtraResources>()
			};
			ClassExtraResources item2 = new ClassExtraResources
			{
				Id = 3074,
				Amount = 1,
				Respawntime = 60,
				ResourceType = 80
			};
			classDefaultResources2.ResExtra.Add(item2);
			Read.ResourcesList.Add(classDefaultResources2);
			if (Language == 1)
			{
				ResourcesGrid.Rows.Add(1, 3074, "Высохший древесный корень");
			}
			else
			{
				ResourcesGrid.Rows.Add(1, 3074, "Withered root");
			}
			ResourcesGrid_CurrentCellChanged(null, null);
		}
		if (Language == 1)
		{
			ResourcesTab.Text = $"Ресурсы 1/{Read.ResourcesAmount}";
		}
		else
		{
			ResourcesTab.Text = $"Resources 1/{Read.ResourcesAmount}";
		}
	}

	private void RemoveResourceFull_Click(object sender, EventArgs e)
	{
		if (Read == null || ResourcesRowCollection.Count == 0)
		{
			return;
		}
		string text = "Вы уверены,что хотите удалить выбранные объекты?";
		if (Language == 2)
		{
			text = "Are you sure that you want to delete selected objects?";
		}
		DialogResult dialogResult = MessageBox.Show(text, "Npcgen Editor", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);
		if (dialogResult != DialogResult.Yes)
		{
			return;
		}
		int num = ResourcesRowCollection.Min();
		ResourcesGrid.ScrollBars = ScrollBars.None;
		ErrorsGrid.ScrollBars = ScrollBars.None;
		AllowCellChanging = false;
		ResourcesGrid.ClearSelection();
		MainProgressBar.Maximum = ResourcesRowCollection.Count;
		Read.ResourcesAmount -= ResourcesRowCollection.Count;
		foreach (int j in ResourcesRowCollection)
		{
			List<IntDictionary> Matched = (from f in ErrorResourcesCollection
				where f.GridIndex == j
				orderby f.ErrorInex descending
				select f).ToList();
			foreach (IntDictionary item in Matched)
			{
				ErrorsGrid.Rows.RemoveAt(item.ErrorInex);
				ErrorResourcesCollection.RemoveAt(ErrorResourcesCollection.FindIndex((IntDictionary t) => t.ErrorInex == item.ErrorInex));
			}
			ErrorResourcesCollection.Where((IntDictionary b) => b.GridIndex > j).ToList().ForEach(delegate(IntDictionary s)
			{
				s.ErrorInex -= Matched.Count;
			});
			ErrorResourcesCollection.Where((IntDictionary a) => a.GridIndex > j).ToList().ForEach(delegate(IntDictionary s)
			{
				s.GridIndex--;
			});
			ErrorDynamicsCollection.ForEach(delegate(IntDictionary s)
			{
				s.ErrorInex -= Matched.Count;
			});
			Read.ResourcesList.RemoveAt(j);
			ResourcesGrid.Rows.RemoveAt(j);
			MainProgressBar.Value++;
		}
		if (ErrorsGrid.Rows.Count != 0)
		{
			int num2 = ((ErrorResourcesCollection.Count != 0) ? ErrorResourcesCollection.Min((IntDictionary f) => f.ErrorInex) : 0);
			int num3 = ((ErrorResourcesCollection.Count != 0) ? ErrorResourcesCollection.Max((IntDictionary f) => f.ErrorInex) : 0);
			int i;
			for (i = num2; i <= num3; i++)
			{
				ErrorsGrid.Rows[i].Cells[0].Value = i + 1;
				ErrorsGrid.Rows[i].Cells[1].Value = ErrorResourcesCollection.Find((IntDictionary f) => f.ErrorInex == i).GridIndex + 1;
			}
			int num4 = ((ErrorDynamicsCollection.Count != 0) ? ErrorDynamicsCollection.Min((IntDictionary f) => f.ErrorInex) : 0);
			for (int k = num4; k <= num4; k++)
			{
				ErrorsGrid.Rows[k].Cells[0].Value = k + 1;
			}
		}
		AllowCellChanging = true;
		MainProgressBar.Value = 0;
		ResourcesGrid.ScrollBars = ScrollBars.Vertical;
		ErrorsGrid.ScrollBars = ScrollBars.Vertical;
		if (ResourcesGrid.Rows.Count > num)
		{
			ResourcesGrid.CurrentCell = ResourcesGrid.Rows[num].Cells[1];
			ResourcesGrid.FirstDisplayedScrollingRowIndex = num;
		}
		else if (ResourcesGrid.Rows.Count != 0)
		{
			ResourcesGrid.CurrentCell = ResourcesGrid.Rows[ResourcesGrid.Rows.Count - 1].Cells[1];
			ResourcesGrid.FirstDisplayedScrollingRowIndex = ResourcesGrid.Rows.Count - 1;
		}
		ResourcesGrid_CurrentCellChanged(null, null);
		for (int l = 0; l < ResourcesGrid.Rows.Count; l++)
		{
			ResourcesGrid.Rows[l].Cells[0].Value = l + 1;
		}
		if (Language == 1)
		{
			ResourcesTab.Text = $"Ресурсы 1/{Read.ResourcesAmount}";
		}
		else
		{
			ResourcesTab.Text = $"Resources 1/{Read.ResourcesAmount}";
		}
	}

	private void CloneResourcesInGroup_Click(object sender, EventArgs e)
	{
		if (ResourcesGrid.Rows.Count <= 0)
		{
			return;
		}
		if (ResourcesGroupGrid.Rows.Count > 0)
		{
			UnderResourcesRowCollection = UnderResourcesRowCollection.OrderBy((int z) => z).ToList();
			foreach (int item2 in UnderResourcesRowCollection)
			{
				Read.ResourcesList[ResourcesRowIndex].Amount_in_group++;
				ClassExtraResources item = new ClassExtraResources
				{
					Id = Read.ResourcesList[ResourcesRowIndex].ResExtra[item2].Id,
					Amount = Read.ResourcesList[ResourcesRowIndex].ResExtra[item2].Amount,
					ResourceType = Read.ResourcesList[ResourcesRowIndex].ResExtra[item2].ResourceType,
					Respawntime = Read.ResourcesList[ResourcesRowIndex].ResExtra[item2].Respawntime,
					fHeiOff = Read.ResourcesList[ResourcesRowIndex].ResExtra[item2].fHeiOff
				};
				Read.ResourcesList[ResourcesRowIndex].ResExtra.Add(item);
				ResourcesGroupGrid.Rows.Add(Read.ResourcesList[ResourcesRowIndex].Amount_in_group, ResourcesGroupGrid.Rows[item2].Cells[1].Value, ResourcesGroupGrid.Rows[item2].Cells[2].Value);
			}
			RefreshResourcesRow(ResourcesRowIndex);
			ResourcesGroupGrid.ClearSelection();
			for (int i = 1; i <= UnderResourcesRowCollection.Count; i++)
			{
				ResourcesGroupGrid.Rows[ResourcesGroupGrid.Rows.Count - i].Selected = true;
			}
			ResourcesGroupGrid.CurrentCell = ResourcesGroupGrid.Rows[ResourcesGroupGrid.Rows.Count - 1].Cells[1];
			ResourcesGroupGrid.FirstDisplayedScrollingRowIndex = ResourcesGroupGrid.Rows.Count - 1;
		}
		else
		{
			ClassExtraResources classExtraResources = new ClassExtraResources();
			Read.ResourcesList[ResourcesRowIndex].Amount_in_group++;
			classExtraResources.Id = 3074;
			classExtraResources.Amount = 1;
			classExtraResources.Respawntime = 60;
			classExtraResources.ResourceType = 80;
			Read.ResourcesList[ResourcesRowIndex].ResExtra.Add(classExtraResources);
			if (Language == 1)
			{
				ResourcesGroupGrid.Rows.Add(1, 3074, "Высохший древесный корень");
			}
			else
			{
				ResourcesGroupGrid.Rows.Add(1, 3074, "Withered root");
			}
			RefreshResourcesRow(ResourcesRowIndex);
			ResourcesGroupGrid_CurrentCellChanged(null, null);
		}
	}

	private void RemoveResourcesInGroup_Click(object sender, EventArgs e)
	{
		UnderResourcesRowCollection = UnderResourcesRowCollection.OrderByDescending((int z) => z).ToList();
		if (UnderResourcesRowCollection.Count == 0 || ResourcesGrid.Rows.Count <= 0 || ResourcesGroupGrid.Rows.Count <= 0)
		{
			return;
		}
		string text = "Вы уверены,что хотите удалить выбранные ресурсы?";
		if (Language == 2)
		{
			text = "Are you sure that you want to delete selected resources?";
		}
		DialogResult dialogResult = MessageBox.Show(text, "Npcgen Editor", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);
		if (dialogResult != DialogResult.Yes)
		{
			return;
		}
		int num = UnderResourcesRowCollection.Min();
		ResourcesGroupGrid.ClearSelection();
		foreach (int item in UnderResourcesRowCollection)
		{
			Read.ResourcesList[ResourcesRowIndex].Amount_in_group--;
			Read.ResourcesList[ResourcesRowIndex].ResExtra.RemoveAt(item);
			ResourcesGroupGrid.Rows.RemoveAt(item);
		}
		RefreshResourcesRow(ResourcesRowIndex);
		if (ResourcesGroupGrid.Rows.Count > num)
		{
			ResourcesGroupGrid.CurrentCell = ResourcesGroupGrid.Rows[num].Cells[1];
			ResourcesGroupGrid.FirstDisplayedScrollingRowIndex = num;
		}
		else if (ResourcesGroupGrid.Rows.Count != 0)
		{
			ResourcesGroupGrid.CurrentCell = ResourcesGroupGrid.Rows[ResourcesGroupGrid.Rows.Count - 1].Cells[1];
			ResourcesGroupGrid.FirstDisplayedScrollingRowIndex = ResourcesGroupGrid.Rows.Count - 1;
		}
		ResourcesGroupGrid_CurrentCellChanged(null, null);
	}

	private void RId_numeric_DoubleClick(object sender, EventArgs e)
	{
		if (ChooseFromElementsForm != null)
		{
			ChooseFromElementsForm.SetAction = 2;
			ChooseFromElementsForm.SetWindow = 1;
			int num = Element.ResourcesList.FindIndex((NpcMonster z) => z.Id == Convert.ToInt32(RId_numeric.Value));
			if (num != -1)
			{
				ChooseFromElementsForm.FindRow(num, "Resource");
			}
			ChooseFromElementsForm.ShowDialog(this);
		}
	}

	private void RInsterCordsFromGame_Click(object sender, EventArgs e)
	{
		ClassPosition coordinates = GetCoordinates();
		if (coordinates != null)
		{
			RX_position.Text = coordinates.PosX.ToString();
			RY_position.Text = coordinates.PosY.ToString();
			RZ_position.Text = coordinates.PosZ.ToString();
			ResourcesDefaultLeave(RX_position, null);
			ResourcesDefaultLeave(RY_position, null);
			ResourcesDefaultLeave(RZ_position, null);
		}
	}

	private void DynamicGrid_CurrentCellChanged(object sender, EventArgs e)
	{
		if (AllowCellChanging)
		{
			DynamicsRowCollection = (from DataGridViewRow f in DynamicGrid.SelectedRows
				select f.Index into v
				orderby v descending
				select v).ToList();
			if (DynamicGrid.CurrentRow != null)
			{
				DynamicRowIndex = DynamicGrid.CurrentRow.Index;
				if (DynamicRowIndex != -1)
				{
					DId_numeric.Text = Read.DynamicsList[DynamicRowIndex].Id.ToString();
					Label_DynamicName.Text = DynamicGrid.Rows[DynamicRowIndex].Cells[2].Value.ToString();
					DX_position.Text = Read.DynamicsList[DynamicRowIndex].X_position.ToString();
					DY_position.Text = Read.DynamicsList[DynamicRowIndex].Y_position.ToString();
					DZ_position.Text = Read.DynamicsList[DynamicRowIndex].Z_position.ToString();
					DIncline1.Text = Read.DynamicsList[DynamicRowIndex].InCline1.ToString();
					DIncline2.Text = Read.DynamicsList[DynamicRowIndex].InCline2.ToString();
					DRotation.Text = Read.DynamicsList[DynamicRowIndex].Rotation.ToString();
					DTrigger_id.Text = Read.DynamicsList[DynamicRowIndex].TriggerId.ToString();
					DScale.Text = Read.DynamicsList[DynamicRowIndex].Scale.ToString();
					string text = $"{Application.StartupPath}\\DynamicObjects\\d{DynamicGrid.Rows[DynamicRowIndex].Cells[1].Value}.jpg";
					if (File.Exists(text))
					{
						DynamicPictureBox.Image = Image.FromFile(text);
					}
					if (MapForm != null && MainProgressBar.Value == 0 && DynamicsRowCollection.Count != 0 && MapForm.Visible)
					{
						MapForm.GetCoordinates(GetPoint(3));
					}
				}
			}
		}
		if (Language == 1)
		{
			DynObjectsTab.Text = $"Динамические Объекты {DynamicsRowCollection.Count}/{Read.DynobjectAmount}";
		}
		else
		{
			DynObjectsTab.Text = $"Dynamic Objects {DynamicsRowCollection.Count}/{Read.DynobjectAmount}";
		}
	}

	private void IdFind(object sender, EventArgs e)
	{
		if (!string.IsNullOrWhiteSpace(DId_numeric.Text))
		{
			Label_DynamicName.Text = GetDynamicName(Convert.ToInt32(DId_numeric.Text));
			string text = $"{Application.StartupPath}\\DynamicObjects\\d{Convert.ToInt32(DId_numeric.Text)}.jpg";
			if (File.Exists(text))
			{
				DynamicPictureBox.Image = Image.FromFile(text);
			}
			if (Label_DynamicName.Text == "?")
			{
				DynamicPictureBox.Image = null;
			}
		}
	}

	private void DynamicsLeave(object sender, EventArgs e)
	{
		Control control = sender as Control;
		if (DynamicsRowCollection == null)
		{
			return;
		}
		int result2;
		float result3;
		byte result;
		switch (control.Name)
		{
		case "DId_numeric":
			int.TryParse(DId_numeric.Text, out result2);
			{
				foreach (int item in DynamicsRowCollection)
				{
					Read.DynamicsList[item].Id = result2;
					DynamicGrid.Rows[item].Cells[1].Value = result2;
					DynamicGrid.Rows[item].Cells[2].Value = Label_DynamicName.Text;
				}
				break;
			}
		case "DX_position":
			float.TryParse(DX_position.Text, out result3);
			{
				foreach (int item2 in DynamicsRowCollection)
				{
					Read.DynamicsList[item2].X_position = result3;
				}
				break;
			}
		case "DY_position":
			float.TryParse(DY_position.Text, out result3);
			{
				foreach (int item3 in DynamicsRowCollection)
				{
					Read.DynamicsList[item3].Y_position = result3;
				}
				break;
			}
		case "DZ_position":
			float.TryParse(DZ_position.Text, out result3);
			{
				foreach (int item4 in DynamicsRowCollection)
				{
					Read.DynamicsList[item4].Z_position = result3;
				}
				break;
			}
		case "DIncline1":
			byte.TryParse(DIncline1.Text, out result);
			{
				foreach (int item5 in DynamicsRowCollection)
				{
					Read.DynamicsList[item5].InCline1 = result;
				}
				break;
			}
		case "DIncline2":
			byte.TryParse(DIncline2.Text, out result);
			{
				foreach (int item6 in DynamicsRowCollection)
				{
					Read.DynamicsList[item6].InCline2 = result;
				}
				break;
			}
		case "DRotation":
			byte.TryParse(DRotation.Text, out result);
			{
				foreach (int item7 in DynamicsRowCollection)
				{
					Read.DynamicsList[item7].Rotation = result;
				}
				break;
			}
		case "DTrigger_id":
			int.TryParse(DTrigger_id.Text, out result2);
			{
				foreach (int item8 in DynamicsRowCollection)
				{
					DynamicGrid.Rows[item8].Cells[3].Value = result2;
					Read.DynamicsList[item8].TriggerId = result2;
				}
				break;
			}
		case "DScale":
			byte.TryParse(DScale.Text, out result);
			{
				foreach (int item9 in DynamicsRowCollection)
				{
					Read.DynamicsList[item9].Scale = result;
				}
				break;
			}
		}
	}

	private void DynamicsKeyDown(object sender, KeyEventArgs e)
	{
		if (e.KeyCode == Keys.Return)
		{
			DynamicsLeave(sender, null);
		}
	}

	private void DClone_button_Click(object sender, EventArgs e)
	{
		if (DynamicGrid.Rows.Count > 0)
		{
			DynamicGrid.ScrollBars = ScrollBars.None;
			DynamicsRowCollection = DynamicsRowCollection.OrderBy((int z) => z).ToList();
			foreach (int item3 in DynamicsRowCollection)
			{
				Read.DynobjectAmount++;
				ClassDynamicObject item = new ClassDynamicObject
				{
					Id = Read.DynamicsList[item3].Id,
					InCline1 = Read.DynamicsList[item3].InCline1,
					InCline2 = Read.DynamicsList[item3].InCline2,
					Rotation = Read.DynamicsList[item3].Rotation,
					Scale = Read.DynamicsList[item3].Scale,
					TriggerId = Read.DynamicsList[item3].TriggerId,
					X_position = Read.DynamicsList[item3].X_position,
					Y_position = Read.DynamicsList[item3].Y_position,
					Z_position = Read.DynamicsList[item3].Z_position
				};
				Read.DynamicsList.Add(item);
				DynamicGrid.Rows.Add(Read.DynobjectAmount, DynamicGrid.Rows[item3].Cells[1].Value, DynamicGrid.Rows[item3].Cells[2].Value, DynamicGrid.Rows[item3].Cells[3].Value);
			}
			DynamicGrid.ScrollBars = ScrollBars.Vertical;
			List<int> dynamicsRowCollection = DynamicsRowCollection;
			DynamicGrid.CurrentCell = DynamicGrid.Rows[DynamicGrid.Rows.Count - 1].Cells[1];
			for (int i = 1; i <= dynamicsRowCollection.Count; i++)
			{
				DynamicGrid.Rows[DynamicGrid.Rows.Count - i].Selected = true;
			}
			DynamicGrid_CurrentCellChanged(null, null);
		}
		else
		{
			Read.DynobjectAmount++;
			ClassDynamicObject item2 = new ClassDynamicObject
			{
				Id = 16
			};
			Read.DynamicsList.Add(item2);
			if (Language == 1)
			{
				DynamicGrid.Rows.Add(1, 9, "Ворота", 0);
			}
			else
			{
				DynamicGrid.Rows.Add(1, 9, "Gates", 0);
			}
			DynamicGrid_CurrentCellChanged(null, null);
		}
		if (Language == 1)
		{
			DynObjectsTab.Text = $"Динамические Объекты 1/{Read.DynobjectAmount}";
		}
		else
		{
			DynObjectsTab.Text = $"Dynamic Objects 1/{Read.DynobjectAmount}";
		}
	}

	private void DDelete_button_Click(object sender, EventArgs e)
	{
		if (Read == null || DynamicsRowCollection.Count == 0)
		{
			return;
		}
		string text = "Вы уверены,что хотите удалить выбранные объекты?";
		if (Language == 2)
		{
			text = "Are you sure that you want to delete selected objects?";
		}
		DialogResult dialogResult = MessageBox.Show(text, "Npcgen Editor", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);
		if (dialogResult != DialogResult.Yes)
		{
			return;
		}
		int num = DynamicsRowCollection.Min();
		DynamicGrid.ScrollBars = ScrollBars.None;
		ErrorsGrid.ScrollBars = ScrollBars.None;
		AllowCellChanging = false;
		DynamicGrid.ClearSelection();
		MainProgressBar.Maximum = DynamicsRowCollection.Count;
		Read.DynobjectAmount -= DynamicsRowCollection.Count;
		foreach (int j in DynamicsRowCollection)
		{
			List<IntDictionary> Matched = (from f in ErrorDynamicsCollection
				where f.GridIndex == j
				orderby f.ErrorInex descending
				select f).ToList();
			foreach (IntDictionary item in Matched)
			{
				ErrorsGrid.Rows.RemoveAt(item.ErrorInex);
				ErrorDynamicsCollection.RemoveAt(ErrorDynamicsCollection.FindIndex((IntDictionary t) => t.ErrorInex == item.ErrorInex));
			}
			ErrorDynamicsCollection.Where((IntDictionary b) => b.GridIndex > j).ToList().ForEach(delegate(IntDictionary s)
			{
				s.ErrorInex -= Matched.Count;
			});
			ErrorDynamicsCollection.Where((IntDictionary a) => a.GridIndex > j).ToList().ForEach(delegate(IntDictionary s)
			{
				s.GridIndex--;
			});
			Read.DynamicsList.RemoveAt(j);
			DynamicGrid.Rows.RemoveAt(j);
			MainProgressBar.Value++;
		}
		if (ErrorsGrid.Rows.Count != 0)
		{
			int num2 = ((ErrorDynamicsCollection.Count != 0) ? ErrorDynamicsCollection.Min((IntDictionary f) => f.ErrorInex) : 0);
			int num3 = ((ErrorDynamicsCollection.Count != 0) ? ErrorDynamicsCollection.Max((IntDictionary f) => f.ErrorInex) : 0);
			int i;
			for (i = num2; i <= num3; i++)
			{
				ErrorsGrid.Rows[i].Cells[0].Value = i + 1;
				ErrorsGrid.Rows[i].Cells[1].Value = ErrorDynamicsCollection.Find((IntDictionary f) => f.ErrorInex == i).GridIndex + 1;
			}
		}
		AllowCellChanging = true;
		MainProgressBar.Value = 0;
		DynamicGrid.ScrollBars = ScrollBars.Vertical;
		ErrorsGrid.ScrollBars = ScrollBars.Vertical;
		if (DynamicGrid.Rows.Count > num)
		{
			DynamicGrid.CurrentCell = DynamicGrid.Rows[num].Cells[1];
			DynamicGrid.FirstDisplayedScrollingRowIndex = num;
		}
		else if (DynamicGrid.Rows.Count != 0)
		{
			DynamicGrid.CurrentCell = DynamicGrid.Rows[DynamicGrid.Rows.Count - 1].Cells[1];
			DynamicGrid.FirstDisplayedScrollingRowIndex = DynamicGrid.Rows.Count - 1;
		}
		DynamicGrid_CurrentCellChanged(null, null);
		for (int k = 0; k < DynamicGrid.Rows.Count; k++)
		{
			DynamicGrid.Rows[k].Cells[0].Value = k + 1;
		}
		if (Language == 1)
		{
			DynObjectsTab.Text = $"Динамические объекты 1/{Read.DynobjectAmount}";
		}
		else
		{
			DynObjectsTab.Text = $"Dynamic objects 1/{Read.DynobjectAmount}";
		}
	}

	private void DInsterCordsFromGame_Click(object sender, EventArgs e)
	{
		ClassPosition coordinates = GetCoordinates();
		if (coordinates != null)
		{
			DX_position.Text = coordinates.PosX.ToString();
			DY_position.Text = coordinates.PosY.ToString();
			DZ_position.Text = coordinates.PosZ.ToString();
			DynamicsLeave(DX_position, null);
			DynamicsLeave(DY_position, null);
			DynamicsLeave(DZ_position, null);
		}
	}

	private void TriggerUsingInMobsAndNpcsGrid_CellMouseEnter(object sender, DataGridViewCellEventArgs e)
	{
		if ((e.ColumnIndex > 0) & (e.RowIndex >= 0))
		{
			string caption = Convert.ToString(MUTrigger.Rows[e.RowIndex].Cells[e.ColumnIndex].Value);
			toolTip1.SetToolTip(MUTrigger, caption);
		}
	}

	private void TriggersGrid_CurrentCellChanged(object sender, EventArgs e)
	{
		if (AllowCellChanging)
		{
			TriggersRowCollection = (from DataGridViewRow f in TriggersGrid.SelectedRows
				select f.Index into v
				orderby v descending
				select v).ToList();
			if (TriggersGrid.CurrentRow != null)
			{
				TriggersRowIndex = TriggersGrid.CurrentRow.Index;
				if (TriggersRowIndex != -1)
				{
					TId_textbox.Text = Read.TriggersList[TriggersRowIndex].Id.ToString();
					TGmId_textbox.Text = Read.TriggersList[TriggersRowIndex].GmID.ToString();
					TName_textbox.Text = Read.TriggersList[TriggersRowIndex].TriggerName.ToString();
					TWaitStart_textbox.Text = Read.TriggersList[TriggersRowIndex].WaitWhileStart.ToString();
					TWaitStop_textbox.Text = Read.TriggersList[TriggersRowIndex].WaitWhileStop.ToString();
					TAutoStart.Checked = Convert.ToBoolean(Read.TriggersList[TriggersRowIndex].AutoStart);
					TDuration.Text = Read.TriggersList[TriggersRowIndex].Duration.ToString();
					TStartBySchedule.Checked = Convert.ToBoolean(Read.TriggersList[TriggersRowIndex].DontStartOnSchedule);
					TStopBySchedule.Checked = Convert.ToBoolean(Read.TriggersList[TriggersRowIndex].DontStopOnSchedule);
					TStartYear.Text = Read.TriggersList[TriggersRowIndex].StartYear.ToString();
					TStartMonth.Text = Read.TriggersList[TriggersRowIndex].StartMonth.ToString();
					TStartWeekDay.SelectedIndex = Read.TriggersList[TriggersRowIndex].StartWeekDay + 1;
					TStartDay.Text = Read.TriggersList[TriggersRowIndex].StartDay.ToString();
					TStartHour.Text = Read.TriggersList[TriggersRowIndex].StartHour.ToString();
					TStartMinute.Text = Read.TriggersList[TriggersRowIndex].StartMinute.ToString();
					TStopYear.Text = Read.TriggersList[TriggersRowIndex].StopYear.ToString();
					TStopMonth.Text = Read.TriggersList[TriggersRowIndex].StopMonth.ToString();
					TStopWeekDay.SelectedIndex = Read.TriggersList[TriggersRowIndex].StopWeekDay + 1;
					TStopDay.Text = Read.TriggersList[TriggersRowIndex].StopDay.ToString();
					TStopHour.Text = Read.TriggersList[TriggersRowIndex].StopHour.ToString();
					TStopMinute.Text = Read.TriggersList[TriggersRowIndex].StopMinute.ToString();
					MUTrigger.ScrollBars = ScrollBars.None;
					RUTrigger.ScrollBars = ScrollBars.None;
					DUTrigger.ScrollBars = ScrollBars.None;
					MonstersContact = Read.NpcMobList.Where((ClassDefaultMonsters z) => z.Trigger_id == Read.TriggersList[TriggersRowIndex].Id).ToList();
					MUTrigger.Rows.Clear();
					for (int i = 0; i < MonstersContact.Count; i++)
					{
						string text = Convert.ToString(NpcMobsGrid.Rows[Read.NpcMobList.IndexOf(MonstersContact[i])].Cells[1].Value);
						string text2 = Convert.ToString(NpcMobsGrid.Rows[Read.NpcMobList.IndexOf(MonstersContact[i])].Cells[2].Value);
						MUTrigger.Rows.Add(i + 1, text, text2);
					}
					ResourcesContact = Read.ResourcesList.Where((ClassDefaultResources z) => z.Trigger_id == Read.TriggersList[TriggersRowIndex].Id).ToList();
					RUTrigger.Rows.Clear();
					for (int j = 0; j < ResourcesContact.Count; j++)
					{
						string text3 = Convert.ToString(ResourcesGrid.Rows[Read.ResourcesList.IndexOf(ResourcesContact[j])].Cells[1].Value);
						string text4 = Convert.ToString(ResourcesGrid.Rows[Read.ResourcesList.IndexOf(ResourcesContact[j])].Cells[2].Value);
						RUTrigger.Rows.Add(j + 1, text3, text4);
					}
					DynamicsContact = Read.DynamicsList.Where((ClassDynamicObject z) => z.TriggerId == Read.TriggersList[TriggersRowIndex].Id).ToList();
					DUTrigger.Rows.Clear();
					for (int k = 0; k < DynamicsContact.Count; k++)
					{
						string text5 = Convert.ToString(DynamicGrid.Rows[Read.DynamicsList.IndexOf(DynamicsContact[k])].Cells[1].Value);
						string text6 = Convert.ToString(DynamicGrid.Rows[Read.DynamicsList.IndexOf(DynamicsContact[k])].Cells[2].Value);
						DUTrigger.Rows.Add(k + 1, text5, text6);
					}
					MUTrigger.ScrollBars = ScrollBars.Vertical;
					RUTrigger.ScrollBars = ScrollBars.Vertical;
					DUTrigger.ScrollBars = ScrollBars.Vertical;
				}
			}
		}
		if (Language == 1)
		{
			TriggersTab.Text = $"Триггеры {TriggersRowCollection.Count}/{Read.TriggersAmount}";
		}
		else
		{
			TriggersTab.Text = $"Triggers {TriggersRowCollection.Count}/{Read.TriggersAmount}";
		}
	}

	private void TId_textbox_KeyDown(object sender, KeyEventArgs e)
	{
		if (e.KeyCode == Keys.Return)
		{
			TId_textbox_Leave(sender, new EventArgs());
		}
	}

	private void TId_textbox_Leave(object sender, EventArgs e)
	{
		Control control = sender as Control;
		int result;
		switch (control.Name)
		{
		case "TId_textbox":
			int.TryParse(TId_textbox.Text, out result);
			{
				foreach (int item in TriggersRowCollection)
				{
					Read.TriggersList[item].Id = result;
					TriggersGrid.Rows[item].Cells[1].Value = result;
				}
				break;
			}
		case "TGmId_textbox":
			int.TryParse(TGmId_textbox.Text, out result);
			{
				foreach (int item2 in TriggersRowCollection)
				{
					Read.TriggersList[item2].GmID = result;
					TriggersGrid.Rows[item2].Cells[2].Value = result;
				}
				break;
			}
		case "TName_textbox":
		{
			foreach (int item3 in TriggersRowCollection)
			{
				Read.TriggersList[item3].TriggerName = TName_textbox.Text;
				TriggersGrid.Rows[item3].Cells[3].Value = TName_textbox.Text;
			}
			break;
		}
		case "TWaitStart_textbox":
			int.TryParse(TWaitStart_textbox.Text, out result);
			{
				foreach (int item4 in TriggersRowCollection)
				{
					Read.TriggersList[item4].WaitWhileStart = result;
				}
				break;
			}
		case "TWaitStop_textbox":
			int.TryParse(TWaitStop_textbox.Text, out result);
			{
				foreach (int item5 in TriggersRowCollection)
				{
					Read.TriggersList[item5].WaitWhileStop = result;
				}
				break;
			}
		case "TDuration":
			int.TryParse(TDuration.Text, out result);
			{
				foreach (int item6 in TriggersRowCollection)
				{
					Read.TriggersList[item6].Duration = result;
				}
				break;
			}
		case "TAutoStart":
		{
			foreach (int item7 in TriggersRowCollection)
			{
				Read.TriggersList[item7].AutoStart = Convert.ToByte(TAutoStart.Checked);
			}
			break;
		}
		case "TStartBySchedule":
		{
			foreach (int item8 in TriggersRowCollection)
			{
				Read.TriggersList[item8].DontStartOnSchedule = Convert.ToByte(TStartBySchedule.Checked);
			}
			break;
		}
		case "TStopBySchedule":
		{
			foreach (int item9 in TriggersRowCollection)
			{
				Read.TriggersList[item9].DontStopOnSchedule = Convert.ToByte(TStopBySchedule.Checked);
			}
			break;
		}
		case "TStartYear":
			int.TryParse(TStartYear.Text, out result);
			{
				foreach (int item10 in TriggersRowCollection)
				{
					Read.TriggersList[item10].StartYear = result;
				}
				break;
			}
		case "TStartMonth":
			int.TryParse(TStartMonth.Text, out result);
			{
				foreach (int item11 in TriggersRowCollection)
				{
					Read.TriggersList[item11].StartMonth = result;
				}
				break;
			}
		case "TStartWeekDay":
		{
			foreach (int item12 in TriggersRowCollection)
			{
				Read.TriggersList[item12].StartWeekDay = TStartWeekDay.SelectedIndex - 1;
			}
			break;
		}
		case "TStartDay":
			int.TryParse(TStartDay.Text, out result);
			{
				foreach (int item13 in TriggersRowCollection)
				{
					Read.TriggersList[item13].StartDay = result;
				}
				break;
			}
		case "TStartHour":
			int.TryParse(TStartHour.Text, out result);
			{
				foreach (int item14 in TriggersRowCollection)
				{
					Read.TriggersList[item14].StartHour = result;
				}
				break;
			}
		case "TStartMinute":
			int.TryParse(TStartMinute.Text, out result);
			{
				foreach (int item15 in TriggersRowCollection)
				{
					Read.TriggersList[item15].StartMinute = result;
				}
				break;
			}
		case "TStopYear":
			int.TryParse(TStopYear.Text, out result);
			{
				foreach (int item16 in TriggersRowCollection)
				{
					Read.TriggersList[item16].StopYear = result;
				}
				break;
			}
		case "TStopMonth":
			int.TryParse(TStopMonth.Text, out result);
			{
				foreach (int item17 in TriggersRowCollection)
				{
					Read.TriggersList[item17].StopMonth = result;
				}
				break;
			}
		case "TStopWeekDay":
		{
			foreach (int item18 in TriggersRowCollection)
			{
				Read.TriggersList[item18].StopWeekDay = TStopWeekDay.SelectedIndex - 1;
			}
			break;
		}
		case "TStopDay":
			int.TryParse(TStopDay.Text, out result);
			{
				foreach (int item19 in TriggersRowCollection)
				{
					Read.TriggersList[item19].StopDay = result;
				}
				break;
			}
		case "TStopHour":
			int.TryParse(TStopHour.Text, out result);
			{
				foreach (int item20 in TriggersRowCollection)
				{
					Read.TriggersList[item20].StopHour = result;
				}
				break;
			}
		case "TStopMinute":
			int.TryParse(TStopMinute.Text, out result);
			{
				foreach (int item21 in TriggersRowCollection)
				{
					Read.TriggersList[item21].StopMinute = result;
				}
				break;
			}
		}
	}

	private void CloneTrigger_Click(object sender, EventArgs e)
	{
		if (TriggersGrid.Rows.Count > 0)
		{
			TriggersGrid.ScrollBars = ScrollBars.None;
			TriggersRowCollection = TriggersRowCollection.OrderBy((int z) => z).ToList();
			foreach (int item2 in TriggersRowCollection)
			{
				Read.TriggersAmount++;
				ClassTrigger classTrigger = new ClassTrigger
				{
					Id = Read.TriggersList.Max((ClassTrigger z) => z.Id) + 1,
					GmID = Read.TriggersList.Max((ClassTrigger z) => z.GmID) + 1,
					TriggerName = Read.TriggersList[item2].TriggerName,
					AutoStart = Read.TriggersList[item2].AutoStart,
					DontStartOnSchedule = Read.TriggersList[item2].DontStartOnSchedule,
					DontStopOnSchedule = Read.TriggersList[item2].DontStopOnSchedule,
					Duration = Read.TriggersList[item2].Duration,
					StartDay = Read.TriggersList[item2].StartDay,
					StartHour = Read.TriggersList[item2].StartHour,
					StartMinute = Read.TriggersList[item2].StartMinute,
					StartMonth = Read.TriggersList[item2].StartMonth,
					StartWeekDay = Read.TriggersList[item2].StartWeekDay,
					StartYear = Read.TriggersList[item2].StartYear,
					StopDay = Read.TriggersList[item2].StopDay,
					StopHour = Read.TriggersList[item2].StopHour,
					StopMinute = Read.TriggersList[item2].StopMinute,
					StopMonth = Read.TriggersList[item2].StopMonth,
					StopWeekDay = Read.TriggersList[item2].StopWeekDay,
					StopYear = Read.TriggersList[item2].StopYear,
					WaitWhileStart = Read.TriggersList[item2].WaitWhileStart,
					WaitWhileStop = Read.TriggersList[item2].WaitWhileStop
				};
				Read.TriggersList.Add(classTrigger);
				TriggersGrid.Rows.Add(Read.TriggersAmount, classTrigger.Id, classTrigger.GmID, TriggersGrid.Rows[item2].Cells[3].Value);
			}
			TriggersGrid.ScrollBars = ScrollBars.Vertical;
			List<int> triggersRowCollection = TriggersRowCollection;
			TriggersGrid.CurrentCell = TriggersGrid.Rows[TriggersGrid.Rows.Count - 1].Cells[1];
			for (int i = 1; i <= triggersRowCollection.Count; i++)
			{
				TriggersGrid.Rows[TriggersGrid.Rows.Count - i].Selected = true;
			}
			TriggersGrid_CurrentCellChanged(null, null);
		}
		else
		{
			Read.TriggersAmount++;
			ClassTrigger item = new ClassTrigger
			{
				Id = 1,
				GmID = 2,
				TriggerName = "TriggerOne",
				Duration = 60,
				StartYear = -1,
				StartMonth = -1,
				StartWeekDay = -1,
				StartDay = -1,
				StartHour = -1,
				StartMinute = -1,
				StopYear = -1,
				StopMonth = -1,
				StopWeekDay = -1,
				StopDay = -1,
				StopHour = -1,
				StopMinute = -1
			};
			Read.TriggersList.Add(item);
			TriggersGrid.Rows.Add(1, 1, 2, "TriggerOne");
			TriggersGrid_CurrentCellChanged(null, null);
		}
		if (Language == 1)
		{
			TriggersTab.Text = $"Триггеры 1/{Read.TriggersAmount}";
		}
		else
		{
			TriggersTab.Text = $"Triggers 1/{Read.TriggersAmount}";
		}
	}

	private void DeleteTrigger_Click(object sender, EventArgs e)
	{
		if (TriggersRowCollection.Count == 0)
		{
			return;
		}
		string text = "Вы уверены,что хотите удалить выбранные триггера?";
		if (Language == 2)
		{
			text = "Are you sure that you want to delete selected triggers?";
		}
		DialogResult dialogResult = MessageBox.Show(text, "Npcgen Editor", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);
		if (dialogResult != DialogResult.Yes)
		{
			return;
		}
		TriggersRowCollection = TriggersRowCollection.OrderByDescending((int z) => z).ToList();
		TriggersGrid.ScrollBars = ScrollBars.None;
		int num = TriggersRowCollection.Min();
		TriggersGrid.ClearSelection();
		Read.TriggersAmount -= TriggersRowCollection.Count;
		foreach (int item in TriggersRowCollection)
		{
			Read.TriggersList.RemoveAt(item);
			TriggersGrid.Rows.RemoveAt(item);
		}
		TriggersGrid.ScrollBars = ScrollBars.Vertical;
		if (TriggersGrid.Rows.Count > num)
		{
			TriggersGrid.CurrentCell = TriggersGrid.Rows[num].Cells[1];
			TriggersGrid.FirstDisplayedScrollingRowIndex = num;
		}
		else if (TriggersGrid.Rows.Count != 0)
		{
			TriggersGrid.CurrentCell = TriggersGrid.Rows[TriggersGrid.Rows.Count - 1].Cells[1];
			TriggersGrid.FirstDisplayedScrollingRowIndex = TriggersGrid.Rows.Count - 1;
		}
		if (TriggersGrid.Rows.Count == 0)
		{
			TriggersGrid.Rows.Clear();
		}
		TriggersGrid_CurrentCellChanged(null, null);
		if (Language == 1)
		{
			TriggersTab.Text = $"Триггеры 1/{Read.TriggersAmount}";
		}
		else
		{
			TriggersTab.Text = $"Triggers 1/{Read.TriggersAmount}";
		}
	}

	private void GotoNpcMobsContacts_Click(object sender, EventArgs e)
	{
		try
		{
			if (MUTrigger.SelectedRows.Count != 0)
			{
				NpcMobsGrid.ClearSelection();
				NpcMobsGrid.CurrentCell = NpcMobsGrid.Rows[Read.NpcMobList.IndexOf(MonstersContact[MUTrigger.SelectedRows[MUTrigger.SelectedRows.Count - 1].Index])].Cells[1];
				for (int i = 0; i < MUTrigger.SelectedRows.Count; i++)
				{
					NpcMobsGrid.Rows[Read.NpcMobList.IndexOf(MonstersContact[MUTrigger.SelectedRows[i].Index])].Selected = true;
				}
				ExistenceGrid_CellChanged(null, null);
				MainTabControl.SelectedIndex = 0;
			}
		}
		catch
		{
			if (Language == 1)
			{
				MessageBox.Show("Действие невозможно", "Npcgen Editor", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
			}
			else
			{
				MessageBox.Show("Wrong action", "Npcgen Editor", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
			}
		}
	}

	private void GotoResourcesContacts_Click(object sender, EventArgs e)
	{
		try
		{
			if (RUTrigger.SelectedRows.Count != 0)
			{
				ResourcesGrid.ClearSelection();
				ResourcesGrid.CurrentCell = ResourcesGrid.Rows[Read.ResourcesList.IndexOf(ResourcesContact[RUTrigger.SelectedRows[RUTrigger.SelectedRows.Count - 1].Index])].Cells[1];
				for (int i = 0; i < RUTrigger.SelectedRows.Count; i++)
				{
					ResourcesGrid.Rows[Read.ResourcesList.IndexOf(ResourcesContact[RUTrigger.SelectedRows[i].Index])].Selected = true;
				}
				MainTabControl.SelectedIndex = 1;
			}
		}
		catch
		{
			if (Language == 1)
			{
				MessageBox.Show("Действие невозможно", "Npcgen Editor", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
			}
			else
			{
				MessageBox.Show("Wrong action", "Npcgen Editor", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
			}
		}
	}

	private void GotoDynamicsContacts_Click(object sender, EventArgs e)
	{
		try
		{
			if (DUTrigger.SelectedRows.Count != 0)
			{
				DynamicGrid.ClearSelection();
				DynamicGrid.CurrentCell = DynamicGrid.Rows[Read.DynamicsList.IndexOf(DynamicsContact[DUTrigger.SelectedRows[DUTrigger.SelectedRows.Count - 1].Index])].Cells[1];
				for (int i = 0; i < DUTrigger.SelectedRows.Count; i++)
				{
					DynamicGrid.Rows[Read.DynamicsList.IndexOf(DynamicsContact[DUTrigger.SelectedRows[i].Index])].Selected = true;
				}
				MainTabControl.SelectedIndex = 2;
			}
		}
		catch
		{
			if (Language == 1)
			{
				MessageBox.Show("Действие невозможно", "Npcgen Editor", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
			}
			else
			{
				MessageBox.Show("Wrong action", "Npcgen Editor", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
			}
		}
	}

	private void DeleteEmptyTrigger_Click(object sender, EventArgs e)
	{
		if (Read == null)
		{
			return;
		}
		List<int> list = new List<int>();
		int i;
		for (i = 0; i < Read.TriggersAmount; i++)
		{
			int num = Read.NpcMobList.FindIndex((ClassDefaultMonsters z) => z.Trigger_id == Read.TriggersList[i].Id);
			int num2 = Read.ResourcesList.FindIndex((ClassDefaultResources z) => z.Trigger_id == Read.TriggersList[i].Id);
			int num3 = Read.DynamicsList.FindIndex((ClassDynamicObject z) => z.TriggerId == Read.TriggersList[i].Id);
			if (num == -1 && num2 == -1 && num3 == -1)
			{
				list.Add(Read.TriggersList.IndexOf(Read.TriggersList[i]));
			}
		}
		TriggersGrid.ScrollBars = ScrollBars.None;
		list = list.OrderByDescending((int z) => z).ToList();
		string text = "Вы уверены,что хотите удалить неиспользуемые триггеры?";
		if (Language == 2)
		{
			text = "Are you sure that you want to delete empty triggers?";
		}
		DialogResult dialogResult = MessageBox.Show(text, "Npcgen Editor", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);
		if (dialogResult != DialogResult.Yes)
		{
			return;
		}
		foreach (int item in list)
		{
			Read.TriggersAmount--;
			Read.TriggersList.RemoveAt(item);
			TriggersGrid.Rows.RemoveAt(item);
		}
		TriggersGrid.ScrollBars = ScrollBars.Vertical;
		if (Language == 1)
		{
			TriggersTab.Text = $"Триггеры 1/{Read.TriggersAmount}";
		}
		else
		{
			TriggersTab.Text = $"Triggers 1/{Read.TriggersAmount}";
		}
		if (Language == 1)
		{
			MessageBox.Show($"Удалено {list.Count} триггеров", "Npcgen Editor", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
		}
		else
		{
			MessageBox.Show($"Deleted {list.Count} triggers", "Npcgen Editor", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
		}
	}

	private void MUTrigger_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
	{
		GotoNpcMobsContacts_Click(null, null);
	}

	private void RUTrigger_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
	{
		GotoResourcesContacts_Click(null, null);
	}

	private void DUTrigger_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
	{
		GotoDynamicsContacts_Click(null, null);
	}

	private void ExistenceSearchButton_Click(object sender, EventArgs e)
	{
		if (Read == null)
		{
			return;
		}
		Action = 1;
		string text = "Существа";
		if (Language == 2)
		{
			text = "Existence";
		}
		SearchMonsters = new List<int>();
		SearchGrid.ScrollBars = ScrollBars.None;
		SearchGrid.Rows.Clear();
		if (ExistenceSearchId_Radio.Checked)
		{
			int.TryParse(ExistenceSearchId.Text, out var IdIndex3);
			SearchMonsters = (from o in Read.NpcMobList.Select((ClassDefaultMonsters item, int index) => new
				{
					Item = item,
					Index = index
				})
				where o.Item.MobDops.Any((ClassExtraMonsters x) => x.Id == IdIndex3)
				select o.Index).ToList();
			for (int i = 0; i < SearchMonsters.Count(); i++)
			{
				SearchGrid.Rows.Add(i + 1, NpcMobsGrid.Rows[SearchMonsters[i]].Cells[1].Value, NpcMobsGrid.Rows[SearchMonsters[i]].Cells[2].Value, text);
			}
		}
		else if (ExistenceSearchName_Radio.Checked)
		{
			int num = 1;
			for (int j = 0; j < NpcMobsGrid.Rows.Count; j++)
			{
				if (NpcMobsGrid.Rows[j].Cells[2].Value.ToString().ToLower().Contains(ExistenceSearchName.Text.ToLower()))
				{
					SearchMonsters.Add(j);
					SearchGrid.Rows.Add(num, NpcMobsGrid.Rows[j].Cells[1].Value, NpcMobsGrid.Rows[j].Cells[2].Value, text);
					num++;
				}
			}
		}
		else if (ExistenceSearchTrigger_Radio.Checked)
		{
			int.TryParse(ExistenceSearchId.Text, out var IdIndex2);
			SearchMonsters = (from z in Read.NpcMobList.Select((ClassDefaultMonsters item, int index) => new
				{
					Item = item,
					Index = index
				})
				where z.Item.Trigger_id == IdIndex2
				select z into o
				select o.Index).ToList();
			for (int k = 0; k < SearchMonsters.Count; k++)
			{
				SearchGrid.Rows.Add(k + 1, NpcMobsGrid.Rows[SearchMonsters[k]].Cells[1].Value, NpcMobsGrid.Rows[SearchMonsters[k]].Cells[2].Value, text);
			}
		}
		else if (ExistenceSearchPath_Radio.Checked)
		{
			int.TryParse(ExistenceSearchPath.Text, out var IdIndex);
			SearchMonsters = (from o in Read.NpcMobList.Select((ClassDefaultMonsters item, int index) => new
				{
					Item = item,
					Index = index
				})
				where o.Item.MobDops.Any((ClassExtraMonsters x) => x.Path == IdIndex)
				select o.Index).ToList();
			for (int l = 0; l < SearchMonsters.Count; l++)
			{
				SearchGrid.Rows.Add(l + 1, NpcMobsGrid.Rows[SearchMonsters[l]].Cells[1].Value, NpcMobsGrid.Rows[SearchMonsters[l]].Cells[2].Value, text);
			}
		}
		SearchGrid.ScrollBars = ScrollBars.Vertical;
	}

	private void ResourceSearchButton_Click(object sender, EventArgs e)
	{
		if (Read == null)
		{
			return;
		}
		Action = 2;
		string text = "Ресерсы";
		if (Language == 2)
		{
			text = "Resources";
		}
		SearchResources = new List<int>();
		SearchGrid.ScrollBars = ScrollBars.None;
		SearchGrid.Rows.Clear();
		if (ResourceSearchId_Radio.Checked)
		{
			int.TryParse(ResourceSearchId.Text, out var IdIndex2);
			SearchResources = (from o in Read.ResourcesList.Select((ClassDefaultResources item, int index) => new
				{
					Item = item,
					Index = index
				})
				where o.Item.ResExtra.Any((ClassExtraResources x) => x.Id == IdIndex2)
				select o.Index).ToList();
			for (int i = 0; i < SearchResources.Count(); i++)
			{
				SearchGrid.Rows.Add(i + 1, ResourcesGrid.Rows[SearchResources[i]].Cells[1].Value, ResourcesGrid.Rows[SearchResources[i]].Cells[2].Value, text);
			}
		}
		else if (ResourceSearchName_Radio.Checked)
		{
			int num = 1;
			for (int j = 0; j < ResourcesGrid.Rows.Count; j++)
			{
				if (ResourcesGrid.Rows[j].Cells[2].Value.ToString().ToLower().Contains(ResourceSearchName.Text.ToLower()))
				{
					SearchResources.Add(j);
					SearchGrid.Rows.Add(num, ResourcesGrid.Rows[j].Cells[1].Value, ResourcesGrid.Rows[j].Cells[2].Value, text);
					num++;
				}
			}
		}
		else if (ResourceSearchTrigger_Radio.Checked)
		{
			int.TryParse(ResourceSearchTrigger.Text, out var IdIndex);
			SearchResources = (from z in Read.ResourcesList.Select((ClassDefaultResources item, int index) => new
				{
					Item = item,
					Index = index
				})
				where z.Item.Trigger_id == IdIndex
				select z into o
				select o.Index).ToList();
			for (int k = 0; k < SearchResources.Count; k++)
			{
				SearchGrid.Rows.Add(k + 1, ResourcesGrid.Rows[SearchResources[k]].Cells[1].Value, ResourcesGrid.Rows[SearchResources[k]].Cells[2].Value, text);
			}
		}
		SearchGrid.ScrollBars = ScrollBars.Vertical;
	}

	private void DynamicSearchButton_Click(object sender, EventArgs e)
	{
		if (Read == null)
		{
			return;
		}
		Action = 3;
		string text = "Дин.Объекты";
		if (Language == 2)
		{
			text = "Dyn.Objects";
		}
		SearchDynamics = new List<int>();
		DynamicGrid.ScrollBars = ScrollBars.None;
		SearchGrid.Rows.Clear();
		if (DynamicSearchId_Radio.Checked)
		{
			int.TryParse(DynamicSearchId.Text, out var IdIndex2);
			SearchDynamics = (from o in Read.DynamicsList.Select((ClassDynamicObject item, int index) => new
				{
					Item = item,
					Index = index
				})
				where o.Item.Id == IdIndex2
				select o.Index).ToList();
			for (int i = 0; i < SearchDynamics.Count(); i++)
			{
				SearchGrid.Rows.Add(i + 1, DynamicGrid.Rows[SearchDynamics[i]].Cells[1].Value, DynamicGrid.Rows[SearchDynamics[i]].Cells[2].Value, text);
			}
		}
		else if (DynamicSearchName_Radio.Checked)
		{
			int num = 1;
			for (int j = 0; j < DynamicGrid.Rows.Count; j++)
			{
				if (DynamicGrid.Rows[j].Cells[2].Value.ToString().ToLower().Contains(DynamicSearchName.Text.ToLower()))
				{
					SearchDynamics.Add(j);
					SearchGrid.Rows.Add(num, DynamicGrid.Rows[j].Cells[1].Value, DynamicGrid.Rows[j].Cells[2].Value, text);
					num++;
				}
			}
		}
		else if (DynamicSearchTrigger_Radio.Checked)
		{
			int.TryParse(DynamicSearchTrigger.Text, out var IdIndex);
			SearchDynamics = (from z in Read.DynamicsList.Select((ClassDynamicObject item, int index) => new
				{
					Item = item,
					Index = index
				})
				where z.Item.TriggerId == IdIndex
				select z into o
				select o.Index).ToList();
			for (int k = 0; k < SearchDynamics.Count; k++)
			{
				SearchGrid.Rows.Add(k + 1, DynamicGrid.Rows[SearchDynamics[k]].Cells[1].Value, DynamicGrid.Rows[SearchDynamics[k]].Cells[2].Value, text);
			}
		}
		DynamicGrid.ScrollBars = ScrollBars.Vertical;
	}

	private void TriggerSearchButton_Click(object sender, EventArgs e)
	{
		if (Read == null)
		{
			return;
		}
		Action = 4;
		string text = "Триггеры";
		if (Language == 2)
		{
			text = "Triggers";
		}
		SearchTriggers = new List<int>();
		SearchGrid.ScrollBars = ScrollBars.None;
		SearchGrid.Rows.Clear();
		if (TriggerSearchId_Radio.Checked)
		{
			int.TryParse(TriggerSearchID.Text, out var IdIndex2);
			SearchTriggers = (from o in Read.TriggersList.Select((ClassTrigger item, int index) => new
				{
					Item = item,
					Index = index
				})
				where o.Item.Id == IdIndex2
				select o.Index).ToList();
			for (int i = 0; i < SearchTriggers.Count(); i++)
			{
				SearchGrid.Rows.Add(i + 1, TriggersGrid.Rows[SearchTriggers[i]].Cells[1].Value, TriggersGrid.Rows[SearchTriggers[i]].Cells[3].Value, text);
			}
		}
		else if (TriggerSearchGmId_Radio.Checked)
		{
			int.TryParse(TriggerSearchGmID.Text, out var IdIndex);
			SearchTriggers = (from z in Read.TriggersList.Select((ClassTrigger item, int index) => new
				{
					Item = item,
					Index = index
				})
				where z.Item.GmID == IdIndex
				select z into o
				select o.Index).ToList();
			for (int j = 0; j < SearchTriggers.Count; j++)
			{
				SearchGrid.Rows.Add(j + 1, TriggersGrid.Rows[SearchTriggers[j]].Cells[1].Value, TriggersGrid.Rows[SearchTriggers[j]].Cells[3].Value, text);
			}
		}
		else if (TriggerSearchName_Radio.Checked)
		{
			int num = 1;
			for (int k = 0; k < TriggersGrid.Rows.Count; k++)
			{
				if (TriggersGrid.Rows[k].Cells[3].Value.ToString().ToLower().Contains(TriggerSearchName.Text.ToLower()))
				{
					SearchTriggers.Add(k);
					SearchGrid.Rows.Add(num, TriggersGrid.Rows[k].Cells[1].Value, TriggersGrid.Rows[k].Cells[3].Value, text);
					num++;
				}
			}
		}
		SearchGrid.ScrollBars = ScrollBars.Vertical;
	}

	private void MoveToSelected_Click(object sender, EventArgs e)
	{
		try
		{
			if (Read == null)
			{
				return;
			}
			if (Action == 1 && SearchGrid.Rows.Count != 0)
			{
				NpcMobsGrid.CurrentCell = NpcMobsGrid.Rows[SearchMonsters[SearchGrid.SelectedRows[SearchGrid.SelectedRows.Count - 1].Index]].Cells[1];
				foreach (DataGridViewRow selectedRow in SearchGrid.SelectedRows)
				{
					NpcMobsGrid.Rows[SearchMonsters[selectedRow.Index]].Selected = true;
				}
				MainTabControl.SelectedIndex = 0;
				ExistenceGrid_CellChanged(null, null);
			}
			else if (Action == 2 && SearchGrid.Rows.Count != 0)
			{
				ResourcesGrid.CurrentCell = ResourcesGrid.Rows[SearchResources[SearchGrid.SelectedRows[SearchGrid.SelectedRows.Count - 1].Index]].Cells[1];
				foreach (DataGridViewRow selectedRow2 in SearchGrid.SelectedRows)
				{
					ResourcesGrid.Rows[SearchResources[selectedRow2.Index]].Selected = true;
				}
				MainTabControl.SelectedIndex = 1;
				ResourcesGrid_CurrentCellChanged(null, null);
			}
			else if (Action == 3 && SearchGrid.Rows.Count != 0)
			{
				DynamicGrid.CurrentCell = DynamicGrid.Rows[SearchDynamics[SearchGrid.SelectedRows[SearchGrid.SelectedRows.Count - 1].Index]].Cells[1];
				foreach (DataGridViewRow selectedRow3 in SearchGrid.SelectedRows)
				{
					DynamicGrid.Rows[SearchDynamics[selectedRow3.Index]].Selected = true;
				}
				MainTabControl.SelectedIndex = 2;
				DynamicGrid_CurrentCellChanged(null, null);
			}
			else
			{
				if (Action != 4 || SearchGrid.Rows.Count == 0)
				{
					return;
				}
				TriggersGrid.CurrentCell = TriggersGrid.Rows[SearchTriggers[SearchGrid.SelectedRows[SearchGrid.SelectedRows.Count - 1].Index]].Cells[1];
				foreach (DataGridViewRow selectedRow4 in SearchGrid.SelectedRows)
				{
					TriggersGrid.Rows[SearchTriggers[selectedRow4.Index]].Selected = true;
				}
				MainTabControl.SelectedIndex = 3;
				TriggersGrid_CurrentCellChanged(null, null);
			}
		}
		catch
		{
			if (Language == 1)
			{
				MessageBox.Show("Invalid operation!!...", "NpcGen Editor", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
			}
			else
			{
				MessageBox.Show("Неверное действие!!...", "NpcGen Editor", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
			}
		}
	}

	private void TriggerSearchID_TextChanged(object sender, EventArgs e)
	{
		if (Read != null)
		{
			int.TryParse(TriggerSearchID.Text, out var Parsed);
			int num = Read.TriggersList.FindIndex((ClassTrigger z) => z.Id == Parsed);
			if (num != -1)
			{
				TriggerSearchName.Text = TriggersGrid.Rows[num].Cells[3].Value.ToString();
			}
		}
	}

	private void TriggerSearchGmID_TextChanged(object sender, EventArgs e)
	{
		if (Read != null)
		{
			int.TryParse(TriggerSearchGmID.Text, out var Parsed);
			int num = Read.TriggersList.FindIndex((ClassTrigger z) => z.GmID == Parsed);
			if (num != -1)
			{
				TriggerSearchName.Text = TriggersGrid.Rows[num].Cells[3].Value.ToString();
			}
		}
	}

	private void ExistenceSearchId_TextChanged(object sender, EventArgs e)
	{
		if (Read != null)
		{
			int.TryParse(ExistenceSearchId.Text, out var Parsed);
			int num = Element.ExistenceLists.FindIndex((NpcMonster z) => z.Id == Parsed);
			if (num != -1)
			{
				ExistenceSearchName.Text = Element.ExistenceLists[num].Name;
			}
			else
			{
				ExistenceSearchName.Text = "";
			}
		}
	}

	private void ResourceSearchId_TextChanged(object sender, EventArgs e)
	{
		if (Read != null)
		{
			int.TryParse(ResourceSearchId.Text, out var Parsed);
			int num = Element.ResourcesList.FindIndex((NpcMonster z) => z.Id == Parsed);
			if (num != -1)
			{
				ResourceSearchName.Text = Element.ResourcesList[num].Name;
			}
			else
			{
				ResourceSearchName.Text = "";
			}
		}
	}

	private void DynamicSearchId_TextChanged(object sender, EventArgs e)
	{
		if (Read == null)
		{
			return;
		}
		int.TryParse(DynamicSearchId.Text, out var Parsed);
		if (Language == 1)
		{
			int num = DynamicsListRu.FindIndex((DefaultInformation z) => z.Id == Parsed);
			if (num != -1)
			{
				DynamicSearchName.Text = DynamicsListRu[num].Name;
			}
			else
			{
				DynamicSearchName.Text = "";
			}
		}
		else if (Language == 2)
		{
			int num2 = DynamicsListEn.FindIndex((DefaultInformation z) => z.Id == Parsed);
			if (num2 != -1)
			{
				DynamicSearchName.Text = DynamicsListEn[num2].Name;
			}
			else
			{
				DynamicSearchName.Text = "";
			}
		}
	}

	private void MainTabControl_Click(object sender, EventArgs e)
	{
		Control control = sender as Control;
		switch (control.Name)
		{
		case "ExistenceSearchId":
			ExistenceSearchId_Radio.Checked = true;
			break;
		case "ExistenceSearchName":
			ExistenceSearchName_Radio.Checked = true;
			break;
		case "ExistenceSearchTrigger":
			ExistenceSearchTrigger_Radio.Checked = true;
			break;
		case "ExistenceSearchPath":
			ExistenceSearchPath_Radio.Checked = true;
			break;
		case "ResourceSearchId":
			ResourceSearchId_Radio.Checked = true;
			break;
		case "ResourceSearchName":
			ResourceSearchName_Radio.Checked = true;
			break;
		case "ResourceSearchTrigger":
			ResourceSearchTrigger_Radio.Checked = true;
			break;
		case "DynamicSearchId":
			DynamicSearchId_Radio.Checked = true;
			break;
		case "DynamicSearchName":
			DynamicSearchName_Radio.Checked = true;
			break;
		case "DynamicSearchTrigger":
			DynamicSearchTrigger_Radio.Checked = true;
			break;
		case "TriggerSearchID":
			TriggerSearchId_Radio.Checked = true;
			break;
		case "TriggerSearchGmID":
			TriggerSearchGmId_Radio.Checked = true;
			break;
		case "TriggerSearchName":
			TriggerSearchName_Radio.Checked = true;
			break;
		}
	}

	private void DId_numeric_DoubleClick(object sender, EventArgs e)
	{
		DynamicForm.SetWindow = 1;
		DynamicForm.ShowDialog(this);
	}

	private void SearchGrid_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
	{
		MoveToSelected_Click(null, null);
	}

	private void SearchErrorsButton_Click(object sender, EventArgs e)
	{
		if (Read == null)
		{
			return;
		}
		ErrorExistenceCollection = new List<IntDictionary>();
		ErrorResourcesCollection = new List<IntDictionary>();
		ErrorDynamicsCollection = new List<IntDictionary>();
		ErrorsLanguage = Language;
		string text = "Мобы и Нпс";
		string text2 = "Ресурсы";
		string text3 = "Динамические объекты";
		string text4 = "Количество объектов в группе не может быть равно 0";
		string text5 = "Триггер не найден";
		string text6 = "Неизвестный ID";
		string text7 = "Количество объекта не может быть равно 0";
		if (Language == 2)
		{
			text = "Mobs And Npcs";
			text2 = "Resources";
			text3 = "Dynamic Objects";
			text4 = "Objects amount in group can't be equal to 0";
			text5 = "Trigger not found";
			text6 = "Unknown ID";
			text7 = "Object amount can't be equal to 0";
		}
		MainProgressBar.Maximum = Read.NpcMobsAmount + Read.ResourcesAmount + Read.DynobjectAmount;
		ErrorsGrid.ScrollBars = ScrollBars.None;
		ErrorsGrid.Rows.Clear();
		int num = 1;
		int k;
		for (k = 0; k < Read.NpcMobsAmount; k++)
		{
			if (Read.NpcMobList[k].Amount_in_group == 0)
			{
				ErrorsGrid.Rows.Add(num, k + 1, text, text4);
				ErrorExistenceCollection.Add(new IntDictionary(num - 1, k));
				num++;
			}
			if (Read.NpcMobList[k].Trigger_id != 0 && Read.TriggersList.FindIndex((ClassTrigger f) => f.Id == Read.NpcMobList[k].Trigger_id) == -1)
			{
				ErrorsGrid.Rows.Add(num, k + 1, text, text5);
				ErrorExistenceCollection.Add(new IntDictionary(num - 1, k));
				num++;
			}
			int z3;
			for (z3 = 0; z3 < Read.NpcMobList[k].Amount_in_group; z3++)
			{
				if (Element.ExistenceLists.FindIndex((NpcMonster v) => v.Id == Read.NpcMobList[k].MobDops[z3].Id) == -1)
				{
					ErrorsGrid.Rows.Add(num, k + 1, text, text6);
					ErrorExistenceCollection.Add(new IntDictionary(num - 1, k));
					num++;
					break;
				}
			}
			for (int l = 0; l < Read.NpcMobList[k].Amount_in_group; l++)
			{
				if (Read.NpcMobList[k].MobDops[l].Amount == 0)
				{
					ErrorsGrid.Rows.Add(num, k + 1, text, text7);
					ErrorExistenceCollection.Add(new IntDictionary(num - 1, k));
					num++;
					break;
				}
			}
			MainProgressBar.Value++;
		}
		int j;
		for (j = 0; j < Read.ResourcesAmount; j++)
		{
			if (Read.ResourcesList[j].Amount_in_group == 0)
			{
				ErrorsGrid.Rows.Add(num, j + 1, text2, text4);
				ErrorResourcesCollection.Add(new IntDictionary(num - 1, j));
				num++;
			}
			if (Read.ResourcesList[j].Trigger_id != 0 && Read.TriggersList.FindIndex((ClassTrigger f) => f.Id == Read.ResourcesList[j].Trigger_id) == -1)
			{
				ErrorsGrid.Rows.Add(num, j + 1, text2, text5);
				ErrorResourcesCollection.Add(new IntDictionary(num - 1, j));
				num++;
			}
			int z2;
			for (z2 = 0; z2 < Read.ResourcesList[j].Amount_in_group; z2++)
			{
				if (Element.ResourcesList.FindIndex((NpcMonster v) => v.Id == Read.ResourcesList[j].ResExtra[z2].Id) == -1)
				{
					ErrorsGrid.Rows.Add(num, j + 1, text2, text6);
					ErrorResourcesCollection.Add(new IntDictionary(num - 1, j));
					num++;
					break;
				}
			}
			for (int m = 0; m < Read.ResourcesList[j].Amount_in_group; m++)
			{
				if (Read.ResourcesList[j].ResExtra[m].Amount == 0)
				{
					ErrorsGrid.Rows.Add(num, j + 1, text2, text7);
					ErrorResourcesCollection.Add(new IntDictionary(num - 1, j));
					num++;
					break;
				}
			}
			MainProgressBar.Value++;
		}
		int i;
		for (i = 0; i < Read.DynobjectAmount; i++)
		{
			if (Read.DynamicsList[i].TriggerId != 0 && Read.TriggersList.FindIndex((ClassTrigger z) => z.Id == Read.DynamicsList[i].TriggerId) == -1)
			{
				ErrorsGrid.Rows.Add(num, i + 1, text3, text5);
				ErrorDynamicsCollection.Add(new IntDictionary(num - 1, i));
				num++;
			}
			MainProgressBar.Value++;
		}
		MainProgressBar.Value = 0;
		ErrorsGrid.ScrollBars = ScrollBars.Vertical;
	}

	private void ErrorsGrid_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
	{
		try
		{
			string text = ErrorsGrid.Rows[e.RowIndex].Cells[2].Value.ToString();
			if (text == "Мобы и Нпс" || text == "Mobs And Npcs")
			{
				NpcMobsGrid.CurrentCell = NpcMobsGrid.Rows[Convert.ToInt32(ErrorsGrid.Rows[e.RowIndex].Cells[1].Value) - 1].Cells[1];
				MainTabControl.SelectedIndex = 0;
			}
			else if (text == "Ресурсы" || text == "Resources")
			{
				ResourcesGrid.CurrentCell = ResourcesGrid.Rows[Convert.ToInt32(ErrorsGrid.Rows[e.RowIndex].Cells[1].Value) - 1].Cells[1];
				MainTabControl.SelectedIndex = 1;
			}
			else if (text == "Динамические объекты" || text == "Dynamic Objects")
			{
				DynamicGrid.CurrentCell = DynamicGrid.Rows[Convert.ToInt32(ErrorsGrid.Rows[e.RowIndex].Cells[1].Value) - 1].Cells[1];
				MainTabControl.SelectedIndex = 2;
			}
		}
		catch
		{
			if (Language == 1)
			{
				MessageBox.Show("Объект не существует!!...", "Npcgen Editor", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
			}
			else
			{
				MessageBox.Show("Object doesn't exist!!...", "Npcgen Editor", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
			}
		}
	}

	private void RemoveAllErrors_Click(object sender, EventArgs e)
	{
		string text = "Вы уверены,что хотите удалить все объекты с ошибками?";
		if (Language == 2)
		{
			text = "Are you sure that you want to delete all objects with errors?";
		}
		DialogResult dialogResult = MessageBox.Show(text, "Npcgen Editor", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);
		if (dialogResult != DialogResult.Yes)
		{
			return;
		}
		NpcMobsGrid.ScrollBars = ScrollBars.None;
		ResourcesGrid.ScrollBars = ScrollBars.None;
		DynamicGrid.ScrollBars = ScrollBars.None;
		ErrorsGrid.Rows.Clear();
		List<int> list = new List<int>();
		List<int> list2 = new List<int>();
		List<int> list3 = new List<int>();
		ErrorExistenceCollection = ErrorExistenceCollection.OrderByDescending((IntDictionary r) => r.GridIndex).ToList();
		MainProgressBar.Maximum = ErrorExistenceCollection.Count + ErrorResourcesCollection.Count + ErrorDynamicsCollection.Count;
		foreach (IntDictionary k in ErrorExistenceCollection)
		{
			if (list.FindIndex((int f) => f == k.GridIndex) == -1)
			{
				NpcMobsGrid.Rows.RemoveAt(k.GridIndex);
				Read.NpcMobList.RemoveAt(k.GridIndex);
				list.Add(k.GridIndex);
			}
			MainProgressBar.Value++;
		}
		Read.NpcMobsAmount = Read.NpcMobList.Count;
		ErrorExistenceCollection.Clear();
		ExistenceGrid_CellChanged(null, null);
		ErrorResourcesCollection = ErrorResourcesCollection.OrderByDescending((IntDictionary r) => r.GridIndex).ToList();
		foreach (IntDictionary j in ErrorResourcesCollection)
		{
			if (list2.FindIndex((int f) => f == j.GridIndex) == -1)
			{
				ResourcesGrid.Rows.RemoveAt(j.GridIndex);
				Read.ResourcesList.RemoveAt(j.GridIndex);
				list2.Add(j.GridIndex);
			}
			MainProgressBar.Value++;
		}
		Read.ResourcesAmount = Read.ResourcesList.Count;
		ErrorResourcesCollection.Clear();
		ResourcesGrid_CurrentCellChanged(null, null);
		ErrorDynamicsCollection = ErrorDynamicsCollection.OrderByDescending((IntDictionary r) => r.GridIndex).ToList();
		foreach (IntDictionary i in ErrorDynamicsCollection)
		{
			if (list3.FindIndex((int f) => f == i.GridIndex) == -1)
			{
				DynamicGrid.Rows.RemoveAt(i.GridIndex);
				Read.DynamicsList.RemoveAt(i.GridIndex);
				list3.Add(i.GridIndex);
			}
			MainProgressBar.Value++;
		}
		NpcMobsGrid.ScrollBars = ScrollBars.Vertical;
		ResourcesGrid.ScrollBars = ScrollBars.Vertical;
		DynamicGrid.ScrollBars = ScrollBars.Vertical;
		if (NpcMobsGrid.CurrentCell != null)
		{
			NpcMobsGrid.FirstDisplayedScrollingRowIndex = NpcMobsGrid.CurrentCell.RowIndex;
		}
		if (ResourcesGrid.CurrentCell != null)
		{
			ResourcesGrid.FirstDisplayedScrollingRowIndex = ResourcesGrid.CurrentCell.RowIndex;
		}
		if (DynamicGrid.CurrentCell != null)
		{
			DynamicGrid.FirstDisplayedScrollingRowIndex = DynamicGrid.CurrentCell.RowIndex;
		}
		Read.DynobjectAmount = Read.DynamicsList.Count;
		ErrorDynamicsCollection.Clear();
		DynamicGrid_CurrentCellChanged(null, null);
		MainProgressBar.Value = 0;
	}

	private void GetAllControls(Control container, ref List<Control> ControlList)
	{
		foreach (Control control in container.Controls)
		{
			GetAllControls(control, ref ControlList);
			if (control is TabPage || control is GroupBox || control is RadioButton)
			{
				ControlList.Add(control);
			}
		}
	}

	private void GetAllLabels(Control container, ref List<Control> ControlList)
	{
		foreach (Control control in container.Controls)
		{
			GetAllLabels(control, ref ControlList);
			if (control is Label || control is RadioButton || control is GroupBox)
			{
				ControlList.Add(control);
			}
		}
	}

	private void GetAllTextBoxs(Control container, ref List<Control> ControlList)
	{
		foreach (Control control in container.Controls)
		{
			GetAllTextBoxs(control, ref ControlList);
			if (control is TextBox)
			{
				ControlList.Add(control);
			}
		}
	}

	private void InterfaceColorChanged(object sender, EventArgs e)
	{
		if ((sender as Control).Name == "Dark")
		{
			InterfaceColor = 2;
			BackColor = Color.FromArgb(58, 58, 58);
			List<Control> ControlList = new List<Control>();
			GetAllControls(this, ref ControlList);
			foreach (Control item in ControlList)
			{
				item.BackColor = Color.FromArgb(58, 58, 58);
			}
			List<Control> ControlList2 = new List<Control>();
			GetAllLabels(this, ref ControlList2);
			foreach (Control item2 in ControlList2)
			{
				item2.ForeColor = Color.FromArgb(220, 220, 220);
			}
			DynamicPictureBox.BackColor = Color.FromArgb(58, 58, 58);
			List<Control> ControlList3 = new List<Control>();
			GetAllTextBoxs(this, ref ControlList3);
			foreach (Control item3 in ControlList3)
			{
				item3.BackColor = Color.FromArgb(75, 75, 75);
				item3.ForeColor = Color.FromArgb(243, 243, 243);
			}
			ExistenceToolStrip.BackColor = Color.FromArgb(58, 58, 58);
			toolStrip1.BackColor = Color.FromArgb(58, 58, 58);
			toolStrip2.BackColor = Color.FromArgb(58, 58, 58);
			toolStrip3.BackColor = Color.FromArgb(58, 58, 58);
			ExportExistence.ForeColor = Color.FromArgb(240, 240, 240);
			ImportExistence.ForeColor = Color.FromArgb(240, 240, 240);
			ExportResources.ForeColor = Color.FromArgb(240, 240, 240);
			ImportResources.ForeColor = Color.FromArgb(240, 240, 240);
			LineUpResource.ForeColor = Color.FromArgb(240, 240, 240);
			MoveResources.ForeColor = Color.FromArgb(240, 240, 240);
			toolStripButton3.ForeColor = Color.FromArgb(240, 240, 240);
			toolStripButton4.ForeColor = Color.FromArgb(240, 240, 240);
			toolStripDropDownButton3.ForeColor = Color.FromArgb(240, 240, 240);
			toolStripDropDownButton4.ForeColor = Color.FromArgb(240, 240, 240);
			toolStripButton5.ForeColor = Color.FromArgb(240, 240, 240);
			toolStripButton6.ForeColor = Color.FromArgb(240, 240, 240);
			toolStripButton7.ForeColor = Color.FromArgb(240, 240, 240);
			toolStripDropDownButton6.ForeColor = Color.FromArgb(240, 240, 240);
			LineUpExistenceDropDown.ForeColor = Color.FromArgb(240, 240, 240);
			MoveExistenceDropDown.ForeColor = Color.FromArgb(240, 240, 240);
			Agression.BackColor = Color.FromArgb(90, 90, 90);
			Path_type.BackColor = Color.FromArgb(90, 90, 90);
			Agression.ForeColor = Color.FromArgb(245, 245, 245);
			Path_type.ForeColor = Color.FromArgb(245, 245, 245);
		}
		else
		{
			InterfaceColor = 1;
			BackColor = Color.FromArgb(239, 244, 250);
			List<Control> ControlList4 = new List<Control>();
			GetAllControls(this, ref ControlList4);
			foreach (Control item4 in ControlList4)
			{
				item4.BackColor = Color.FromArgb(239, 244, 250);
			}
			List<Control> ControlList5 = new List<Control>();
			GetAllLabels(this, ref ControlList5);
			foreach (Control item5 in ControlList5)
			{
				item5.ForeColor = SystemColors.ControlText;
			}
			DynamicPictureBox.BackColor = Color.FromArgb(239, 244, 250);
			List<Control> ControlList6 = new List<Control>();
			GetAllTextBoxs(this, ref ControlList6);
			foreach (Control item6 in ControlList6)
			{
				item6.BackColor = SystemColors.Window;
				item6.ForeColor = SystemColors.ControlText;
			}
			ExistenceToolStrip.BackColor = Color.FromArgb(239, 244, 250);
			toolStrip1.BackColor = Color.FromArgb(239, 244, 250);
			toolStrip2.BackColor = Color.FromArgb(239, 244, 250);
			toolStrip3.BackColor = Color.FromArgb(239, 244, 250);
			ExportExistence.ForeColor = SystemColors.ControlText;
			ImportExistence.ForeColor = SystemColors.ControlText;
			ExportResources.ForeColor = SystemColors.ControlText;
			ImportResources.ForeColor = SystemColors.ControlText;
			LineUpResource.ForeColor = SystemColors.ControlText;
			MoveResources.ForeColor = SystemColors.ControlText;
			toolStripButton3.ForeColor = SystemColors.ControlText;
			toolStripButton4.ForeColor = SystemColors.ControlText;
			toolStripDropDownButton3.ForeColor = SystemColors.ControlText;
			toolStripDropDownButton4.ForeColor = SystemColors.ControlText;
			toolStripButton5.ForeColor = SystemColors.ControlText;
			toolStripButton6.ForeColor = SystemColors.ControlText;
			toolStripButton7.ForeColor = SystemColors.ControlText;
			toolStripDropDownButton6.ForeColor = SystemColors.ControlText;
			LineUpExistenceDropDown.ForeColor = SystemColors.ControlText;
			MoveExistenceDropDown.ForeColor = SystemColors.ControlText;
			Agression.BackColor = Color.FromArgb(255, 192, 128);
			Path_type.BackColor = Color.FromArgb(255, 192, 128);
			Agression.ForeColor = SystemColors.ControlText;
			Path_type.ForeColor = SystemColors.ControlText;
		}
		Id_numeric.BackColor = Color.FromArgb(128, 255, 128);
		RId_numeric.BackColor = Color.FromArgb(128, 255, 128);
		DId_numeric.BackColor = Color.FromArgb(128, 255, 128);
		Id_numeric.ForeColor = Color.Black;
		RId_numeric.ForeColor = Color.Black;
		DId_numeric.ForeColor = Color.Black;
		groupBox23.BackColor = Color.FromArgb(128, 255, 128);
		groupBox23.ForeColor = Color.Black;
		Clear.ForeColor = Color.Black;
		Dark.ForeColor = Color.Black;
		Clear.BackColor = Color.Transparent;
		Dark.BackColor = Color.Transparent;
		Label_DynamicName.ForeColor = Color.Red;
		if (DynamicForm != null)
		{
			DynamicForm.SetColor(InterfaceColor);
		}
	}

	public void ShowWrongFile()
	{
		if (Language == 1)
		{
			MessageBox.Show("Неверный файл!!...", "Npcgen Editor", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
		}
		else
		{
			MessageBox.Show("Wrong file!!...", "Npcgen Editor", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
		}
	}

	private void ExportExistence_Click(object sender, EventArgs e)
	{
		if (Read != null)
		{
			SaveFileDialog saveFileDialog = new SaveFileDialog();
			if (MainTabControl.SelectedIndex == 0)
			{
				if (NpcRowCollection.Count != 0)
				{
					saveFileDialog.FileName = $"Npcgen Existences[{NpcRowCollection.Count}]";
					saveFileDialog.Filter = "Npcgen Existence | *.nblee";
					if (saveFileDialog.ShowDialog() == DialogResult.OK)
					{
						Read.ExportExistence(saveFileDialog.FileName, NpcRowCollection);
					}
				}
			}
			else if (MainTabControl.SelectedIndex == 1)
			{
				if (ResourcesRowCollection.Count != 0)
				{
					saveFileDialog.FileName = $"Npcgen Resources[{ResourcesRowCollection.Count}]";
					saveFileDialog.Filter = "Npcgen Resource | *.nbler";
					if (saveFileDialog.ShowDialog() == DialogResult.OK)
					{
						Read.ExportResource(saveFileDialog.FileName, ResourcesRowCollection);
					}
				}
			}
			else if (MainTabControl.SelectedIndex == 2)
			{
				if (DynamicsRowCollection.Count != 0)
				{
					saveFileDialog.FileName = $"Npcgen Dynamic Objects[{DynamicsRowCollection.Count}]";
					saveFileDialog.Filter = "Npcgen Dynamic Objects | *.nbled";
					if (saveFileDialog.ShowDialog() == DialogResult.OK)
					{
						Read.ExportDynamics(saveFileDialog.FileName, DynamicsRowCollection);
					}
				}
			}
			else if (MainTabControl.SelectedIndex == 3 && TriggersRowCollection.Count != 0)
			{
				saveFileDialog.FileName = $"Npcgen Triggers[{TriggersRowCollection.Count}]";
				saveFileDialog.Filter = "Npcgen Triggers | *.nblet";
				if (saveFileDialog.ShowDialog() == DialogResult.OK)
				{
					Read.ExportTriggers(saveFileDialog.FileName, TriggersRowCollection);
				}
			}
		}
		else if (Language == 1)
		{
			MessageBox.Show("Файл не загружен!!...", "Npcgen Editor", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
		}
		else
		{
			MessageBox.Show("File isn't loaded!!...", "Npcgen Editor", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
		}
	}

	private void ImportExistence_Click(object sender, EventArgs e)
	{
		if (Read != null)
		{
			OpenFileDialog openFileDialog = new OpenFileDialog();
			if (MainTabControl.SelectedIndex == 0)
			{
				openFileDialog.FileName = "Npcgen Exported Existence";
				openFileDialog.Filter = "Npcgen Existence | *.nblee";
				if (openFileDialog.ShowDialog() != DialogResult.OK)
				{
					return;
				}
				BinaryReader binaryReader = new BinaryReader(File.Open(openFileDialog.FileName, FileMode.Open));
				if (Encoding.Default.GetString(binaryReader.ReadBytes(33)).Split(new string[1] { "||" }, StringSplitOptions.None).ElementAt(1) == "Existence")
				{
					int num = binaryReader.ReadInt32();
					for (int k = 0; k < num; k++)
					{
						ClassDefaultMonsters classDefaultMonsters = Read.ReadExistence(binaryReader, 15);
						int[] Id_joined2 = new int[classDefaultMonsters.Amount_in_group];
						string[] array = new string[classDefaultMonsters.Amount_in_group];
						int j;
						for (j = 0; j < classDefaultMonsters.Amount_in_group; j++)
						{
							Id_joined2[j] = classDefaultMonsters.MobDops[j].Id;
							int num2 = Element.ExistenceLists.FindIndex((NpcMonster v) => v.Id == Id_joined2[j]);
							if (num2 != -1)
							{
								array[j] = Element.ExistenceLists[num2].Name;
							}
							else
							{
								array[j] = "?";
							}
						}
						NpcMobsGrid.Rows.Add(NpcMobsGrid.Rows.Count + 1, string.Join(",", Id_joined2), string.Join(",", array));
						Read.NpcMobList.Add(classDefaultMonsters);
						Read.NpcMobsAmount++;
					}
					NpcMobsGrid.CurrentCell = NpcMobsGrid.Rows[NpcMobsGrid.Rows.Count - 1].Cells[1];
					for (int l = 0; l < num; l++)
					{
						NpcMobsGrid.Rows[NpcMobsGrid.Rows.Count - 1 - l].Selected = true;
					}
					ExistenceGrid_CellChanged(null, null);
					binaryReader.Close();
				}
				else
				{
					ShowWrongFile();
				}
			}
			else if (MainTabControl.SelectedIndex == 1)
			{
				openFileDialog.FileName = "Npcgen Exported Resources";
				openFileDialog.Filter = "Npcgen Resources | *.nbler";
				if (openFileDialog.ShowDialog() != DialogResult.OK)
				{
					return;
				}
				BinaryReader binaryReader2 = new BinaryReader(File.Open(openFileDialog.FileName, FileMode.Open));
				if (Encoding.Default.GetString(binaryReader2.ReadBytes(33)).Split(new string[1] { "||" }, StringSplitOptions.None).ElementAt(1) == "Resources")
				{
					int num3 = binaryReader2.ReadInt32();
					for (int m = 0; m < num3; m++)
					{
						ClassDefaultResources classDefaultResources = Read.ReadResource(binaryReader2, 15);
						int[] Id_joined = new int[classDefaultResources.Amount_in_group];
						string[] array2 = new string[classDefaultResources.Amount_in_group];
						int i;
						for (i = 0; i < classDefaultResources.Amount_in_group; i++)
						{
							Id_joined[i] = classDefaultResources.ResExtra[i].Id;
							int num4 = Element.ResourcesList.FindIndex((NpcMonster v) => v.Id == Id_joined[i]);
							if (num4 != -1)
							{
								array2[i] = Element.ResourcesList[num4].Name;
							}
							else
							{
								array2[i] = "?";
							}
						}
						ResourcesGrid.Rows.Add(ResourcesGrid.Rows.Count + 1, string.Join(",", Id_joined), string.Join(",", array2));
						Read.ResourcesList.Add(classDefaultResources);
						Read.ResourcesAmount++;
					}
					ResourcesGrid.CurrentCell = ResourcesGrid.Rows[ResourcesGrid.Rows.Count - 1].Cells[1];
					for (int n = 0; n < num3; n++)
					{
						ResourcesGrid.Rows[ResourcesGrid.Rows.Count - 1 - n].Selected = true;
					}
					ResourcesGrid_CurrentCellChanged(null, null);
				}
				else
				{
					ShowWrongFile();
				}
			}
			else if (MainTabControl.SelectedIndex == 2)
			{
				openFileDialog.FileName = "Npcgen Exported Dynamic Objects";
				openFileDialog.Filter = "Npcgen Dynamic Objects | *.nbled";
				if (openFileDialog.ShowDialog() != DialogResult.OK)
				{
					return;
				}
				BinaryReader binaryReader3 = new BinaryReader(File.Open(openFileDialog.FileName, FileMode.Open));
				if (Encoding.Default.GetString(binaryReader3.ReadBytes(33)).Split(new string[1] { "||" }, StringSplitOptions.None).ElementAt(1) == "DynObject")
				{
					int num5 = binaryReader3.ReadInt32();
					for (int num6 = 0; num6 < num5; num6++)
					{
						ClassDynamicObject classDynamicObject = Read.ReadDynObjects(binaryReader3, 15);
						DynamicGrid.Rows.Add(DynamicGrid.Rows.Count + 1, classDynamicObject.Id, GetDynamicName(classDynamicObject.Id), classDynamicObject.TriggerId);
						Read.DynamicsList.Add(classDynamicObject);
						Read.DynobjectAmount++;
					}
					DynamicGrid.CurrentCell = DynamicGrid.Rows[DynamicGrid.Rows.Count - 1].Cells[1];
					for (int num7 = 0; num7 < num5; num7++)
					{
						DynamicGrid.Rows[DynamicGrid.Rows.Count - 1 - num7].Selected = true;
					}
					DynamicGrid_CurrentCellChanged(null, null);
				}
			}
			else
			{
				if (MainTabControl.SelectedIndex != 3)
				{
					return;
				}
				openFileDialog.FileName = "Npcgen Exported Triggers";
				openFileDialog.Filter = "Npcgen Triggers | *.nblet";
				if (openFileDialog.ShowDialog() != DialogResult.OK)
				{
					return;
				}
				BinaryReader binaryReader4 = new BinaryReader(File.Open(openFileDialog.FileName, FileMode.Open));
				if (Encoding.Default.GetString(binaryReader4.ReadBytes(33)).Split(new string[1] { "||" }, StringSplitOptions.None).ElementAt(1) == "Triggerss")
				{
					int num8 = binaryReader4.ReadInt32();
					for (int num9 = 0; num9 < num8; num9++)
					{
						ClassTrigger classTrigger = Read.ReadTrigger(binaryReader4, 15);
						TriggersGrid.Rows.Add(TriggersGrid.Rows.Count + 1, classTrigger.Id, classTrigger.GmID, classTrigger.TriggerName);
						Read.TriggersList.Add(classTrigger);
						Read.TriggersAmount++;
					}
					TriggersGrid.CurrentCell = TriggersGrid.Rows[TriggersGrid.Rows.Count - 1].Cells[1];
					for (int num10 = 0; num10 < num8; num10++)
					{
						TriggersGrid.Rows[TriggersGrid.Rows.Count - 1 - num10].Selected = true;
					}
					TriggersGrid_CurrentCellChanged(null, null);
				}
			}
		}
		else if (Language == 1)
		{
			MessageBox.Show("Файл не загружен!!...", "Npcgen Editor", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
		}
		else
		{
			MessageBox.Show("File isn't loaded!!...", "Npcgen Editor", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
		}
	}

	private void ExistenceSearchId_DoubleClick(object sender, EventArgs e)
	{
		if (ChooseFromElementsForm == null)
		{
			return;
		}
		int.TryParse(ExistenceSearchId.Text, out var b);
		int num = Element.ExistenceLists.FindIndex((NpcMonster z) => z.Id == b);
		ChooseFromElementsForm.SetAction = 1;
		ChooseFromElementsForm.SetWindow = 2;
		if (num != -1)
		{
			if (num >= Element.MonsterdAmount)
			{
				ChooseFromElementsForm.FindRow(num - Element.MonsterdAmount, "Npc");
			}
			else
			{
				ChooseFromElementsForm.FindRow(num, "Mob");
			}
		}
		ChooseFromElementsForm.ShowDialog(this);
	}

	private void ResourceSearchId_DoubleClick(object sender, EventArgs e)
	{
		if (ChooseFromElementsForm != null)
		{
			ChooseFromElementsForm.SetAction = 2;
			ChooseFromElementsForm.SetWindow = 2;
			int.TryParse(ResourceSearchId.Text, out var b);
			int num = Element.ResourcesList.FindIndex((NpcMonster z) => z.Id == b);
			if (num != -1)
			{
				ChooseFromElementsForm.FindRow(num, "Resource");
			}
			ChooseFromElementsForm.ShowDialog(this);
		}
	}

	private void DynamicSearchId_DoubleClick(object sender, EventArgs e)
	{
		DynamicForm.SetWindow = 2;
		DynamicForm.ShowDialog(this);
	}

	private void ExistenceToEnd_Click(object sender, EventArgs e)
	{
		if (Read != null && NpcMobsGrid.CurrentRow != null && NpcMobsGrid.CurrentRow.Index != -1)
		{
			ClassDefaultMonsters item = Read.NpcMobList[NpcMobsGrid.CurrentRow.Index];
			Read.NpcMobList.RemoveAt(NpcMobsGrid.CurrentRow.Index);
			Read.NpcMobList.Insert(Read.NpcMobList.Count, item);
			DataGridViewRow dataGridViewRow = NpcMobsGrid.Rows[NpcMobsGrid.CurrentRow.Index];
			NpcMobsGrid.Rows.Remove(dataGridViewRow);
			NpcMobsGrid.Rows.Insert(NpcMobsGrid.Rows.Count, dataGridViewRow);
			NpcMobsGrid.CurrentCell = NpcMobsGrid.Rows[NpcMobsGrid.Rows.Count - 1].Cells[1];
		}
	}

	protected override void Dispose(bool disposing)
	{
		if (disposing && components != null)
		{
			components.Dispose();
		}
		base.Dispose(disposing);
	}

	private void InitializeComponent()
	{
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle11 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle9 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle10 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle12 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle16 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle13 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle14 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle15 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle17 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle21 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle18 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle19 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle20 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle25 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle22 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle23 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle24 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle29 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle26 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle27 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle28 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle33 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle30 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle31 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle32 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle34 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle38 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle35 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle36 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle37 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle42 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle39 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle40 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle41 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle43 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle46 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle44 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle45 = new System.Windows.Forms.DataGridViewCellStyle();
            this.label1 = new System.Windows.Forms.Label();
            this.Elements_textbox = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.Npcgen_textbox = new System.Windows.Forms.TextBox();
            this.MainProgressBar = new System.Windows.Forms.ProgressBar();
            this.MainTabControl = new System.Windows.Forms.TabControl();
            this.ExistenceTab = new System.Windows.Forms.TabPage();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.Group_numeric = new System.Windows.Forms.NumericUpDown();
            this.label35 = new System.Windows.Forms.Label();
            this.RefreshLower_numeric = new System.Windows.Forms.NumericUpDown();
            this.Deadtime_numeric = new System.Windows.Forms.NumericUpDown();
            this.bFac_Accept = new System.Windows.Forms.CheckBox();
            this.bFaction = new System.Windows.Forms.CheckBox();
            this.bFac_Helper = new System.Windows.Forms.CheckBox();
            this.NeedHelp_numeric = new System.Windows.Forms.NumericUpDown();
            this.bNeedHelp = new System.Windows.Forms.CheckBox();
            this.label32 = new System.Windows.Forms.Label();
            this.AskHelp_numeric = new System.Windows.Forms.NumericUpDown();
            this.NpcsGroupGrid = new System.Windows.Forms.DataGridView();
            this.dataGridViewTextBoxColumn3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ExistenceGroupCloneButton = new System.Windows.Forms.Button();
            this.label31 = new System.Windows.Forms.Label();
            this.ExistenceGroupRemoveButton = new System.Windows.Forms.Button();
            this.Turn_numeric = new System.Windows.Forms.NumericUpDown();
            this.label30 = new System.Windows.Forms.Label();
            this.Water_numeric = new System.Windows.Forms.NumericUpDown();
            this.Path_numeric = new System.Windows.Forms.NumericUpDown();
            this.Path_speed = new System.Windows.Forms.NumericUpDown();
            this.Path_type = new System.Windows.Forms.ComboBox();
            this.Agression = new System.Windows.Forms.ComboBox();
            this.DeathAmount_numeric = new System.Windows.Forms.NumericUpDown();
            this.Respawn_numeric = new System.Windows.Forms.NumericUpDown();
            this.label23 = new System.Windows.Forms.Label();
            this.Amount_numeric = new System.Windows.Forms.NumericUpDown();
            this.label22 = new System.Windows.Forms.Label();
            this.Id_numeric = new System.Windows.Forms.NumericUpDown();
            this.label21 = new System.Windows.Forms.Label();
            this.label29 = new System.Windows.Forms.Label();
            this.label34 = new System.Windows.Forms.Label();
            this.label24 = new System.Windows.Forms.Label();
            this.label27 = new System.Windows.Forms.Label();
            this.label26 = new System.Windows.Forms.Label();
            this.label28 = new System.Windows.Forms.Label();
            this.label25 = new System.Windows.Forms.Label();
            this.label33 = new System.Windows.Forms.Label();
            this.NpcMobsGrid = new System.Windows.Forms.DataGridView();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ExistenceContext = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.LineUpX = new System.Windows.Forms.ToolStripMenuItem();
            this.LineUpZ = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.UpExistence = new System.Windows.Forms.ToolStripMenuItem();
            this.DownExistence = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.вНачалоToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ExistenceToEnd = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.экспортToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.импортToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.MainGroupBox = new System.Windows.Forms.GroupBox();
            this.ExistenceInsertCordsFromGame = new System.Windows.Forms.Button();
            this.groupBox8 = new System.Windows.Forms.GroupBox();
            this.AddMonsterRespawnTime = new System.Windows.Forms.NumericUpDown();
            this.label63 = new System.Windows.Forms.Label();
            this.AddMonsterAmount = new System.Windows.Forms.NumericUpDown();
            this.label64 = new System.Windows.Forms.Label();
            this.AddMonsterId = new System.Windows.Forms.NumericUpDown();
            this.label65 = new System.Windows.Forms.Label();
            this.AddMonsterType = new System.Windows.Forms.ComboBox();
            this.label66 = new System.Windows.Forms.Label();
            this.AddintExistenceType = new System.Windows.Forms.ComboBox();
            this.AddMonsterTrigger = new System.Windows.Forms.NumericUpDown();
            this.label67 = new System.Windows.Forms.Label();
            this.label68 = new System.Windows.Forms.Label();
            this.IMaxNuml = new System.Windows.Forms.TextBox();
            this.Life_time = new System.Windows.Forms.TextBox();
            this.Trigger = new System.Windows.Forms.TextBox();
            this.contextMenuStrip2 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.MoveToTrigger = new System.Windows.Forms.ToolStripMenuItem();
            this.label18 = new System.Windows.Forms.Label();
            this.dwGenId = new System.Windows.Forms.TextBox();
            this.label17 = new System.Windows.Forms.Label();
            this.BValicOnce = new System.Windows.Forms.CheckBox();
            this.ExistenceAutoRevive = new System.Windows.Forms.CheckBox();
            this.ExistenceInitGen = new System.Windows.Forms.CheckBox();
            this.Group_type = new System.Windows.Forms.TextBox();
            this.label16 = new System.Windows.Forms.Label();
            this.ExistenceType = new System.Windows.Forms.ComboBox();
            this.label15 = new System.Windows.Forms.Label();
            this.Z_scatter = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.Y_scatter = new System.Windows.Forms.TextBox();
            this.label13 = new System.Windows.Forms.Label();
            this.X_scatter = new System.Windows.Forms.TextBox();
            this.label14 = new System.Windows.Forms.Label();
            this.Z_rotate = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.Y_rotate = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.X_rotate = new System.Windows.Forms.TextBox();
            this.Z_position = new System.Windows.Forms.TextBox();
            this.Y_position = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.X_position = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.Group_amount_textbox = new System.Windows.Forms.TextBox();
            this.ExistenceLocating = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label19 = new System.Windows.Forms.Label();
            this.label20 = new System.Windows.Forms.Label();
            this.ExistenceRemoveButton = new System.Windows.Forms.Button();
            this.ExistenceCloneButton = new System.Windows.Forms.Button();
            this.ExistenceToolStrip = new System.Windows.Forms.ToolStrip();
            this.ExportExistence = new System.Windows.Forms.ToolStripButton();
            this.ImportExistence = new System.Windows.Forms.ToolStripButton();
            this.LineUpExistenceDropDown = new System.Windows.Forms.ToolStripDropDownButton();
            this.ToolStripLineUpX = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripLineUpZ = new System.Windows.Forms.ToolStripMenuItem();
            this.MoveExistenceDropDown = new System.Windows.Forms.ToolStripDropDownButton();
            this.MoveUpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.MoveDownToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ResourcesTab = new System.Windows.Forms.TabPage();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.RType_numeric = new System.Windows.Forms.NumericUpDown();
            this.label39 = new System.Windows.Forms.Label();
            this.RfHeiOff_numeric = new System.Windows.Forms.NumericUpDown();
            this.label55 = new System.Windows.Forms.Label();
            this.RRespawn_numeric = new System.Windows.Forms.NumericUpDown();
            this.RAmount_numeric = new System.Windows.Forms.NumericUpDown();
            this.label59 = new System.Windows.Forms.Label();
            this.RId_numeric = new System.Windows.Forms.NumericUpDown();
            this.label60 = new System.Windows.Forms.Label();
            this.ResourcesGroupRemoveButton = new System.Windows.Forms.Button();
            this.ResourcesGroupCloneButton = new System.Windows.Forms.Button();
            this.ResourcesGroupGrid = new System.Windows.Forms.DataGridView();
            this.dataGridViewTextBoxColumn9 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn10 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn11 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.label53 = new System.Windows.Forms.Label();
            this.label58 = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.groupBox11 = new System.Windows.Forms.GroupBox();
            this.ResourcesInsertCordsFromGame = new System.Windows.Forms.Button();
            this.AddResourceRespawnTime = new System.Windows.Forms.NumericUpDown();
            this.label54 = new System.Windows.Forms.Label();
            this.AddResourceAmount = new System.Windows.Forms.NumericUpDown();
            this.label56 = new System.Windows.Forms.Label();
            this.AddResourceID = new System.Windows.Forms.NumericUpDown();
            this.label57 = new System.Windows.Forms.Label();
            this.AddResourcesTrigger = new System.Windows.Forms.NumericUpDown();
            this.label75 = new System.Windows.Forms.Label();
            this.RIMaxNuml = new System.Windows.Forms.TextBox();
            this.label36 = new System.Windows.Forms.Label();
            this.RTriggerID = new System.Windows.Forms.TextBox();
            this.label37 = new System.Windows.Forms.Label();
            this.RdwGenID = new System.Windows.Forms.TextBox();
            this.label38 = new System.Windows.Forms.Label();
            this.RBValidOnce = new System.Windows.Forms.CheckBox();
            this.ResourcesAutoRevive = new System.Windows.Forms.CheckBox();
            this.ResourcesInitGen = new System.Windows.Forms.CheckBox();
            this.RRotation = new System.Windows.Forms.TextBox();
            this.label41 = new System.Windows.Forms.Label();
            this.RZ_Random = new System.Windows.Forms.TextBox();
            this.label42 = new System.Windows.Forms.Label();
            this.RX_Random = new System.Windows.Forms.TextBox();
            this.label43 = new System.Windows.Forms.Label();
            this.RInCline2 = new System.Windows.Forms.TextBox();
            this.label44 = new System.Windows.Forms.Label();
            this.RInCline1 = new System.Windows.Forms.TextBox();
            this.label45 = new System.Windows.Forms.Label();
            this.RZ_position = new System.Windows.Forms.TextBox();
            this.RY_position = new System.Windows.Forms.TextBox();
            this.RX_position = new System.Windows.Forms.TextBox();
            this.label47 = new System.Windows.Forms.Label();
            this.label48 = new System.Windows.Forms.Label();
            this.label49 = new System.Windows.Forms.Label();
            this.RGroup_amount_textbox = new System.Windows.Forms.TextBox();
            this.label51 = new System.Windows.Forms.Label();
            this.ResourcesGrid = new System.Windows.Forms.DataGridView();
            this.dataGridViewTextBoxColumn6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn7 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn8 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.ExportResources = new System.Windows.Forms.ToolStripButton();
            this.ImportResources = new System.Windows.Forms.ToolStripButton();
            this.LineUpResource = new System.Windows.Forms.ToolStripDropDownButton();
            this.ResourcesOnX = new System.Windows.Forms.ToolStripMenuItem();
            this.ResourcesOnZ = new System.Windows.Forms.ToolStripMenuItem();
            this.MoveResources = new System.Windows.Forms.ToolStripDropDownButton();
            this.ResourceUp = new System.Windows.Forms.ToolStripMenuItem();
            this.ResourceDown = new System.Windows.Forms.ToolStripMenuItem();
            this.ResourcesRemoveButton = new System.Windows.Forms.Button();
            this.ResourcesCloneButton = new System.Windows.Forms.Button();
            this.DynObjectsTab = new System.Windows.Forms.TabPage();
            this.groupBox9 = new System.Windows.Forms.GroupBox();
            this.DynamicPictureBox = new System.Windows.Forms.PictureBox();
            this.groupBox7 = new System.Windows.Forms.GroupBox();
            this.DId_numeric = new System.Windows.Forms.TextBox();
            this.Label_DynamicName = new System.Windows.Forms.Label();
            this.DynObjectsInsertCordsFromGame = new System.Windows.Forms.Button();
            this.DScale = new System.Windows.Forms.TextBox();
            this.groupBox10 = new System.Windows.Forms.GroupBox();
            this.AddDynamicsTrigger = new System.Windows.Forms.NumericUpDown();
            this.label40 = new System.Windows.Forms.Label();
            this.AddDynamicsID = new System.Windows.Forms.NumericUpDown();
            this.label61 = new System.Windows.Forms.Label();
            this.label74 = new System.Windows.Forms.Label();
            this.DTrigger_id = new System.Windows.Forms.TextBox();
            this.label73 = new System.Windows.Forms.Label();
            this.DRotation = new System.Windows.Forms.TextBox();
            this.label70 = new System.Windows.Forms.Label();
            this.DIncline2 = new System.Windows.Forms.TextBox();
            this.label71 = new System.Windows.Forms.Label();
            this.DIncline1 = new System.Windows.Forms.TextBox();
            this.label72 = new System.Windows.Forms.Label();
            this.label69 = new System.Windows.Forms.Label();
            this.DZ_position = new System.Windows.Forms.TextBox();
            this.DY_position = new System.Windows.Forms.TextBox();
            this.DX_position = new System.Windows.Forms.TextBox();
            this.label80 = new System.Windows.Forms.Label();
            this.label81 = new System.Windows.Forms.Label();
            this.label82 = new System.Windows.Forms.Label();
            this.DynamicGrid = new System.Windows.Forms.DataGridView();
            this.dataGridViewTextBoxColumn12 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn13 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn14 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DynObjectsRemoveButton = new System.Windows.Forms.Button();
            this.DynObjectsCloneButton = new System.Windows.Forms.Button();
            this.toolStrip2 = new System.Windows.Forms.ToolStrip();
            this.toolStripButton3 = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton4 = new System.Windows.Forms.ToolStripButton();
            this.toolStripDropDownButton3 = new System.Windows.Forms.ToolStripDropDownButton();
            this.toolStripMenuItem7 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem8 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripDropDownButton4 = new System.Windows.Forms.ToolStripDropDownButton();
            this.toolStripMenuItem9 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem10 = new System.Windows.Forms.ToolStripMenuItem();
            this.TriggersTab = new System.Windows.Forms.TabPage();
            this.groupBox15 = new System.Windows.Forms.GroupBox();
            this.DUTrigger = new System.Windows.Forms.DataGridView();
            this.dataGridViewTextBoxColumn25 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn26 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn27 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.GotoDynamicsContacts = new System.Windows.Forms.Button();
            this.groupBox14 = new System.Windows.Forms.GroupBox();
            this.RUTrigger = new System.Windows.Forms.DataGridView();
            this.dataGridViewTextBoxColumn22 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn23 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn24 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.GotoResourcesContacts = new System.Windows.Forms.Button();
            this.groupBox13 = new System.Windows.Forms.GroupBox();
            this.MUTrigger = new System.Windows.Forms.DataGridView();
            this.dataGridViewTextBoxColumn28 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn29 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn30 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.GotoNpcMobsContacts = new System.Windows.Forms.Button();
            this.groupBox12 = new System.Windows.Forms.GroupBox();
            this.groupBox17 = new System.Windows.Forms.GroupBox();
            this.TStopMinute = new System.Windows.Forms.TextBox();
            this.label93 = new System.Windows.Forms.Label();
            this.TStopHour = new System.Windows.Forms.TextBox();
            this.label94 = new System.Windows.Forms.Label();
            this.TStopDay = new System.Windows.Forms.TextBox();
            this.label95 = new System.Windows.Forms.Label();
            this.TStopWeekDay = new System.Windows.Forms.ComboBox();
            this.TStopMonth = new System.Windows.Forms.TextBox();
            this.label96 = new System.Windows.Forms.Label();
            this.TStopYear = new System.Windows.Forms.TextBox();
            this.label97 = new System.Windows.Forms.Label();
            this.label98 = new System.Windows.Forms.Label();
            this.groupBox16 = new System.Windows.Forms.GroupBox();
            this.TStartMinute = new System.Windows.Forms.TextBox();
            this.label92 = new System.Windows.Forms.Label();
            this.TStartHour = new System.Windows.Forms.TextBox();
            this.label91 = new System.Windows.Forms.Label();
            this.TStartDay = new System.Windows.Forms.TextBox();
            this.label90 = new System.Windows.Forms.Label();
            this.TStartWeekDay = new System.Windows.Forms.ComboBox();
            this.TStartMonth = new System.Windows.Forms.TextBox();
            this.label87 = new System.Windows.Forms.Label();
            this.TStartYear = new System.Windows.Forms.TextBox();
            this.label86 = new System.Windows.Forms.Label();
            this.label88 = new System.Windows.Forms.Label();
            this.TStartBySchedule = new System.Windows.Forms.CheckBox();
            this.TStopBySchedule = new System.Windows.Forms.CheckBox();
            this.TDuration = new System.Windows.Forms.TextBox();
            this.label85 = new System.Windows.Forms.Label();
            this.TWaitStop_textbox = new System.Windows.Forms.TextBox();
            this.label84 = new System.Windows.Forms.Label();
            this.TWaitStart_textbox = new System.Windows.Forms.TextBox();
            this.TAutoStart = new System.Windows.Forms.CheckBox();
            this.TId_textbox = new System.Windows.Forms.TextBox();
            this.label78 = new System.Windows.Forms.Label();
            this.label89 = new System.Windows.Forms.Label();
            this.TName_textbox = new System.Windows.Forms.TextBox();
            this.TGmId_textbox = new System.Windows.Forms.TextBox();
            this.label79 = new System.Windows.Forms.Label();
            this.label83 = new System.Windows.Forms.Label();
            this.label99 = new System.Windows.Forms.Label();
            this.TriggersGrid = new System.Windows.Forms.DataGridView();
            this.dataGridViewTextBoxColumn15 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn16 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn21 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn17 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TriggersContext = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.DeleteEmptyTrigger = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem4 = new System.Windows.Forms.ToolStripMenuItem();
            this.UpTrigger = new System.Windows.Forms.ToolStripMenuItem();
            this.DownTrigger = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripMenuItem12 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem11 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStrip3 = new System.Windows.Forms.ToolStrip();
            this.toolStripButton5 = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton6 = new System.Windows.Forms.ToolStripButton();
            this.toolStripDropDownButton6 = new System.Windows.Forms.ToolStripDropDownButton();
            this.toolStripMenuItem13 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem14 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripButton7 = new System.Windows.Forms.ToolStripButton();
            this.TriggersRemoveButton = new System.Windows.Forms.Button();
            this.TriggersCloneButton = new System.Windows.Forms.Button();
            this.SearchTab = new System.Windows.Forms.TabPage();
            this.SearchGrid = new System.Windows.Forms.DataGridView();
            this.dataGridViewTextBoxColumn18 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn19 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn20 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.MoveToSelected = new System.Windows.Forms.ToolStripMenuItem();
            this.groupBox21 = new System.Windows.Forms.GroupBox();
            this.TriggerSearchButton = new System.Windows.Forms.Button();
            this.TriggerSearchName = new System.Windows.Forms.TextBox();
            this.TriggerSearchName_Radio = new System.Windows.Forms.RadioButton();
            this.TriggerSearchGmID = new System.Windows.Forms.TextBox();
            this.label107 = new System.Windows.Forms.Label();
            this.TriggerSearchId_Radio = new System.Windows.Forms.RadioButton();
            this.TriggerSearchID = new System.Windows.Forms.TextBox();
            this.label108 = new System.Windows.Forms.Label();
            this.TriggerSearchGmId_Radio = new System.Windows.Forms.RadioButton();
            this.groupBox20 = new System.Windows.Forms.GroupBox();
            this.DynamicSearchButton = new System.Windows.Forms.Button();
            this.DynamicSearchTrigger = new System.Windows.Forms.TextBox();
            this.DynamicSearchTrigger_Radio = new System.Windows.Forms.RadioButton();
            this.DynamicSearchName = new System.Windows.Forms.TextBox();
            this.label105 = new System.Windows.Forms.Label();
            this.DynamicSearchId_Radio = new System.Windows.Forms.RadioButton();
            this.DynamicSearchId = new System.Windows.Forms.TextBox();
            this.label106 = new System.Windows.Forms.Label();
            this.DynamicSearchName_Radio = new System.Windows.Forms.RadioButton();
            this.groupBox19 = new System.Windows.Forms.GroupBox();
            this.ResourceSearchButton = new System.Windows.Forms.Button();
            this.ResourceSearchTrigger = new System.Windows.Forms.TextBox();
            this.ResourceSearchTrigger_Radio = new System.Windows.Forms.RadioButton();
            this.ResourceSearchName = new System.Windows.Forms.TextBox();
            this.label103 = new System.Windows.Forms.Label();
            this.ResourceSearchId_Radio = new System.Windows.Forms.RadioButton();
            this.ResourceSearchId = new System.Windows.Forms.TextBox();
            this.label104 = new System.Windows.Forms.Label();
            this.ResourceSearchName_Radio = new System.Windows.Forms.RadioButton();
            this.groupBox18 = new System.Windows.Forms.GroupBox();
            this.ExistenceSearchButton = new System.Windows.Forms.Button();
            this.ExistenceSearchPath = new System.Windows.Forms.TextBox();
            this.ExistenceSearchPath_Radio = new System.Windows.Forms.RadioButton();
            this.ExistenceSearchTrigger = new System.Windows.Forms.TextBox();
            this.ExistenceSearchTrigger_Radio = new System.Windows.Forms.RadioButton();
            this.ExistenceSearchName = new System.Windows.Forms.TextBox();
            this.label102 = new System.Windows.Forms.Label();
            this.ExistenceSearchId_Radio = new System.Windows.Forms.RadioButton();
            this.ExistenceSearchId = new System.Windows.Forms.TextBox();
            this.label76 = new System.Windows.Forms.Label();
            this.ExistenceSearchName_Radio = new System.Windows.Forms.RadioButton();
            this.ErrorsTab = new System.Windows.Forms.TabPage();
            this.RemoveAllErrors = new System.Windows.Forms.Button();
            this.SearchErrorsButton = new System.Windows.Forms.Button();
            this.ErrorsGrid = new System.Windows.Forms.DataGridView();
            this.Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn31 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn32 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn33 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.OptionsTab = new System.Windows.Forms.TabPage();
            this.groupBox23 = new System.Windows.Forms.GroupBox();
            this.Clear = new System.Windows.Forms.RadioButton();
            this.Dark = new System.Windows.Forms.RadioButton();
            this.groupBox22 = new System.Windows.Forms.GroupBox();
            this.Russian = new System.Windows.Forms.RadioButton();
            this.English = new System.Windows.Forms.RadioButton();
            this.ConvertComboboxVersion = new System.Windows.Forms.ComboBox();
            this.label52 = new System.Windows.Forms.Label();
            this.groupBox6 = new System.Windows.Forms.GroupBox();
            this.ExtraDynamicsButton_combobox = new System.Windows.Forms.ComboBox();
            this.label100 = new System.Windows.Forms.Label();
            this.DefaultDynamicsButton_combobox = new System.Windows.Forms.ComboBox();
            this.label101 = new System.Windows.Forms.Label();
            this.ExtraResourceButton_combobox = new System.Windows.Forms.ComboBox();
            this.label62 = new System.Windows.Forms.Label();
            this.DefaultResourceButton_combobox = new System.Windows.Forms.ComboBox();
            this.label77 = new System.Windows.Forms.Label();
            this.ExtraMobButton_combobox = new System.Windows.Forms.ComboBox();
            this.label46 = new System.Windows.Forms.Label();
            this.DefaultMobButton_combobox = new System.Windows.Forms.ComboBox();
            this.label50 = new System.Windows.Forms.Label();
            this.Version_combobox = new System.Windows.Forms.ComboBox();
            this.ConvertAndSaveButton = new System.Windows.Forms.Button();
            this.SaveFile = new System.Windows.Forms.Button();
            this.Maps_combobox = new System.Windows.Forms.ComboBox();
            this.Element_dialog = new System.Windows.Forms.OpenFileDialog();
            this.Npcgen_dialog = new System.Windows.Forms.OpenFileDialog();
            this.label4 = new System.Windows.Forms.Label();
            this.Surfaces_path = new System.Windows.Forms.TextBox();
            this.Surfaces_search = new System.Windows.Forms.OpenFileDialog();
            this.Npcgen_save_dialog = new System.Windows.Forms.SaveFileDialog();
            this.ButtonShowMap = new System.Windows.Forms.Button();
            this.MapProgress = new System.Windows.Forms.ProgressBar();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.Dynamics_dialog = new System.Windows.Forms.OpenFileDialog();
            this.InformationButton = new System.Windows.Forms.Button();
            this.Open_surfaces = new System.Windows.Forms.Button();
            this.Search_surfaces = new System.Windows.Forms.Button();
            this.OpenFiles = new System.Windows.Forms.Button();
            this.Search_Npcgen = new System.Windows.Forms.Button();
            this.Search_element = new System.Windows.Forms.Button();
            this.MainTabControl.SuspendLayout();
            this.ExistenceTab.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Group_numeric)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.RefreshLower_numeric)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Deadtime_numeric)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.NeedHelp_numeric)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.AskHelp_numeric)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.NpcsGroupGrid)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Turn_numeric)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Water_numeric)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Path_numeric)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Path_speed)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.DeathAmount_numeric)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Respawn_numeric)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Amount_numeric)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Id_numeric)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.NpcMobsGrid)).BeginInit();
            this.ExistenceContext.SuspendLayout();
            this.MainGroupBox.SuspendLayout();
            this.groupBox8.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.AddMonsterRespawnTime)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.AddMonsterAmount)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.AddMonsterId)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.AddMonsterTrigger)).BeginInit();
            this.contextMenuStrip2.SuspendLayout();
            this.ExistenceToolStrip.SuspendLayout();
            this.ResourcesTab.SuspendLayout();
            this.groupBox5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.RType_numeric)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.RfHeiOff_numeric)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.RRespawn_numeric)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.RAmount_numeric)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.RId_numeric)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ResourcesGroupGrid)).BeginInit();
            this.groupBox3.SuspendLayout();
            this.groupBox11.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.AddResourceRespawnTime)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.AddResourceAmount)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.AddResourceID)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.AddResourcesTrigger)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ResourcesGrid)).BeginInit();
            this.toolStrip1.SuspendLayout();
            this.DynObjectsTab.SuspendLayout();
            this.groupBox9.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DynamicPictureBox)).BeginInit();
            this.groupBox7.SuspendLayout();
            this.groupBox10.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.AddDynamicsTrigger)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.AddDynamicsID)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.DynamicGrid)).BeginInit();
            this.toolStrip2.SuspendLayout();
            this.TriggersTab.SuspendLayout();
            this.groupBox15.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DUTrigger)).BeginInit();
            this.groupBox14.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.RUTrigger)).BeginInit();
            this.groupBox13.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.MUTrigger)).BeginInit();
            this.groupBox12.SuspendLayout();
            this.groupBox17.SuspendLayout();
            this.groupBox16.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.TriggersGrid)).BeginInit();
            this.TriggersContext.SuspendLayout();
            this.toolStrip3.SuspendLayout();
            this.SearchTab.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.SearchGrid)).BeginInit();
            this.contextMenuStrip1.SuspendLayout();
            this.groupBox21.SuspendLayout();
            this.groupBox20.SuspendLayout();
            this.groupBox19.SuspendLayout();
            this.groupBox18.SuspendLayout();
            this.ErrorsTab.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ErrorsGrid)).BeginInit();
            this.OptionsTab.SuspendLayout();
            this.groupBox23.SuspendLayout();
            this.groupBox22.SuspendLayout();
            this.groupBox6.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(5, 33);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(72, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Npcgen.data:";
            // 
            // Elements_textbox
            // 
            this.Elements_textbox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Elements_textbox.Location = new System.Drawing.Point(75, 6);
            this.Elements_textbox.Name = "Elements_textbox";
            this.Elements_textbox.Size = new System.Drawing.Size(687, 20);
            this.Elements_textbox.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(1, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(77, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Elements.data:";
            // 
            // Npcgen_textbox
            // 
            this.Npcgen_textbox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Npcgen_textbox.Location = new System.Drawing.Point(75, 30);
            this.Npcgen_textbox.Name = "Npcgen_textbox";
            this.Npcgen_textbox.Size = new System.Drawing.Size(687, 20);
            this.Npcgen_textbox.TabIndex = 3;
            // 
            // MainProgressBar
            // 
            this.MainProgressBar.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.MainProgressBar.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.MainProgressBar.Location = new System.Drawing.Point(4, 590);
            this.MainProgressBar.Name = "MainProgressBar";
            this.MainProgressBar.Size = new System.Drawing.Size(978, 23);
            this.MainProgressBar.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
            this.MainProgressBar.TabIndex = 9;
            // 
            // MainTabControl
            // 
            this.MainTabControl.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.MainTabControl.Controls.Add(this.ExistenceTab);
            this.MainTabControl.Controls.Add(this.ResourcesTab);
            this.MainTabControl.Controls.Add(this.DynObjectsTab);
            this.MainTabControl.Controls.Add(this.TriggersTab);
            this.MainTabControl.Controls.Add(this.SearchTab);
            this.MainTabControl.Controls.Add(this.ErrorsTab);
            this.MainTabControl.Controls.Add(this.OptionsTab);
            this.MainTabControl.Location = new System.Drawing.Point(4, 81);
            this.MainTabControl.Name = "MainTabControl";
            this.MainTabControl.SelectedIndex = 0;
            this.MainTabControl.Size = new System.Drawing.Size(974, 509);
            this.MainTabControl.TabIndex = 11;
            this.MainTabControl.Click += new System.EventHandler(this.MainTabControl_Click);
            // 
            // ExistenceTab
            // 
            this.ExistenceTab.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(239)))), ((int)(((byte)(244)))), ((int)(((byte)(250)))));
            this.ExistenceTab.Controls.Add(this.groupBox1);
            this.ExistenceTab.Controls.Add(this.NpcMobsGrid);
            this.ExistenceTab.Controls.Add(this.MainGroupBox);
            this.ExistenceTab.Controls.Add(this.ExistenceRemoveButton);
            this.ExistenceTab.Controls.Add(this.ExistenceCloneButton);
            this.ExistenceTab.Controls.Add(this.ExistenceToolStrip);
            this.ExistenceTab.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.ExistenceTab.Location = new System.Drawing.Point(4, 22);
            this.ExistenceTab.Name = "ExistenceTab";
            this.ExistenceTab.Padding = new System.Windows.Forms.Padding(3);
            this.ExistenceTab.Size = new System.Drawing.Size(966, 483);
            this.ExistenceTab.TabIndex = 0;
            this.ExistenceTab.Text = "Мобы и Нипы";
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(239)))), ((int)(((byte)(244)))), ((int)(((byte)(250)))));
            this.groupBox1.Controls.Add(this.Group_numeric);
            this.groupBox1.Controls.Add(this.label35);
            this.groupBox1.Controls.Add(this.RefreshLower_numeric);
            this.groupBox1.Controls.Add(this.Deadtime_numeric);
            this.groupBox1.Controls.Add(this.bFac_Accept);
            this.groupBox1.Controls.Add(this.bFaction);
            this.groupBox1.Controls.Add(this.bFac_Helper);
            this.groupBox1.Controls.Add(this.NeedHelp_numeric);
            this.groupBox1.Controls.Add(this.bNeedHelp);
            this.groupBox1.Controls.Add(this.label32);
            this.groupBox1.Controls.Add(this.AskHelp_numeric);
            this.groupBox1.Controls.Add(this.NpcsGroupGrid);
            this.groupBox1.Controls.Add(this.ExistenceGroupCloneButton);
            this.groupBox1.Controls.Add(this.label31);
            this.groupBox1.Controls.Add(this.ExistenceGroupRemoveButton);
            this.groupBox1.Controls.Add(this.Turn_numeric);
            this.groupBox1.Controls.Add(this.label30);
            this.groupBox1.Controls.Add(this.Water_numeric);
            this.groupBox1.Controls.Add(this.Path_numeric);
            this.groupBox1.Controls.Add(this.Path_speed);
            this.groupBox1.Controls.Add(this.Path_type);
            this.groupBox1.Controls.Add(this.Agression);
            this.groupBox1.Controls.Add(this.DeathAmount_numeric);
            this.groupBox1.Controls.Add(this.Respawn_numeric);
            this.groupBox1.Controls.Add(this.label23);
            this.groupBox1.Controls.Add(this.Amount_numeric);
            this.groupBox1.Controls.Add(this.label22);
            this.groupBox1.Controls.Add(this.Id_numeric);
            this.groupBox1.Controls.Add(this.label21);
            this.groupBox1.Controls.Add(this.label29);
            this.groupBox1.Controls.Add(this.label34);
            this.groupBox1.Controls.Add(this.label24);
            this.groupBox1.Controls.Add(this.label27);
            this.groupBox1.Controls.Add(this.label26);
            this.groupBox1.Controls.Add(this.label28);
            this.groupBox1.Controls.Add(this.label25);
            this.groupBox1.Controls.Add(this.label33);
            this.groupBox1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.groupBox1.Location = new System.Drawing.Point(364, 253);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(599, 229);
            this.groupBox1.TabIndex = 14;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Мобы|Нипы";
            // 
            // Group_numeric
            // 
            this.Group_numeric.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Group_numeric.Location = new System.Drawing.Point(293, 37);
            this.Group_numeric.Maximum = new decimal(new int[] {
            2147483647,
            0,
            0,
            0});
            this.Group_numeric.Minimum = new decimal(new int[] {
            -2147483648,
            0,
            0,
            -2147483648});
            this.Group_numeric.Name = "Group_numeric";
            this.Group_numeric.Size = new System.Drawing.Size(80, 20);
            this.Group_numeric.TabIndex = 65;
            this.Group_numeric.KeyDown += new System.Windows.Forms.KeyEventHandler(this.UnderNpcAndMobs_EnterPress);
            this.Group_numeric.Leave += new System.EventHandler(this.UnderNpcAndMobs_Leave);
            // 
            // label35
            // 
            this.label35.AutoSize = true;
            this.label35.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label35.Location = new System.Drawing.Point(245, 37);
            this.label35.Name = "label35";
            this.label35.Size = new System.Drawing.Size(50, 15);
            this.label35.TabIndex = 66;
            this.label35.Text = "Группа:";
            // 
            // RefreshLower_numeric
            // 
            this.RefreshLower_numeric.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.RefreshLower_numeric.Location = new System.Drawing.Point(293, 130);
            this.RefreshLower_numeric.Maximum = new decimal(new int[] {
            2147483647,
            0,
            0,
            0});
            this.RefreshLower_numeric.Minimum = new decimal(new int[] {
            -2147483648,
            0,
            0,
            -2147483648});
            this.RefreshLower_numeric.Name = "RefreshLower_numeric";
            this.RefreshLower_numeric.Size = new System.Drawing.Size(80, 20);
            this.RefreshLower_numeric.TabIndex = 63;
            this.RefreshLower_numeric.KeyDown += new System.Windows.Forms.KeyEventHandler(this.UnderNpcAndMobs_EnterPress);
            this.RefreshLower_numeric.Leave += new System.EventHandler(this.UnderNpcAndMobs_Leave);
            // 
            // Deadtime_numeric
            // 
            this.Deadtime_numeric.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Deadtime_numeric.Location = new System.Drawing.Point(293, 106);
            this.Deadtime_numeric.Maximum = new decimal(new int[] {
            2147483647,
            0,
            0,
            0});
            this.Deadtime_numeric.Minimum = new decimal(new int[] {
            -2147483648,
            0,
            0,
            -2147483648});
            this.Deadtime_numeric.Name = "Deadtime_numeric";
            this.Deadtime_numeric.Size = new System.Drawing.Size(80, 20);
            this.Deadtime_numeric.TabIndex = 61;
            this.Deadtime_numeric.KeyDown += new System.Windows.Forms.KeyEventHandler(this.UnderNpcAndMobs_EnterPress);
            this.Deadtime_numeric.Leave += new System.EventHandler(this.UnderNpcAndMobs_Leave);
            // 
            // bFac_Accept
            // 
            this.bFac_Accept.AutoSize = true;
            this.bFac_Accept.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.bFac_Accept.Location = new System.Drawing.Point(283, 206);
            this.bFac_Accept.Name = "bFac_Accept";
            this.bFac_Accept.Size = new System.Drawing.Size(90, 17);
            this.bFac_Accept.TabIndex = 60;
            this.bFac_Accept.Text = "bFac_Accept";
            this.bFac_Accept.TextImageRelation = System.Windows.Forms.TextImageRelation.TextBeforeImage;
            this.bFac_Accept.UseVisualStyleBackColor = true;
            this.bFac_Accept.KeyDown += new System.Windows.Forms.KeyEventHandler(this.UnderNpcAndMobs_EnterPress);
            this.bFac_Accept.Leave += new System.EventHandler(this.UnderNpcAndMobs_Leave);
            // 
            // bFaction
            // 
            this.bFaction.AutoSize = true;
            this.bFaction.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.bFaction.Location = new System.Drawing.Point(306, 188);
            this.bFaction.Name = "bFaction";
            this.bFaction.Size = new System.Drawing.Size(67, 17);
            this.bFaction.TabIndex = 38;
            this.bFaction.Text = "bFaction";
            this.bFaction.TextImageRelation = System.Windows.Forms.TextImageRelation.TextBeforeImage;
            this.bFaction.UseVisualStyleBackColor = true;
            this.bFaction.KeyDown += new System.Windows.Forms.KeyEventHandler(this.UnderNpcAndMobs_EnterPress);
            this.bFaction.Leave += new System.EventHandler(this.UnderNpcAndMobs_Leave);
            // 
            // bFac_Helper
            // 
            this.bFac_Helper.AutoSize = true;
            this.bFac_Helper.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.bFac_Helper.Location = new System.Drawing.Point(286, 152);
            this.bFac_Helper.Name = "bFac_Helper";
            this.bFac_Helper.Size = new System.Drawing.Size(87, 17);
            this.bFac_Helper.TabIndex = 39;
            this.bFac_Helper.Text = "bFac_Helper";
            this.bFac_Helper.TextImageRelation = System.Windows.Forms.TextImageRelation.TextBeforeImage;
            this.bFac_Helper.UseVisualStyleBackColor = true;
            this.bFac_Helper.KeyDown += new System.Windows.Forms.KeyEventHandler(this.UnderNpcAndMobs_EnterPress);
            this.bFac_Helper.Leave += new System.EventHandler(this.UnderNpcAndMobs_Leave);
            // 
            // NeedHelp_numeric
            // 
            this.NeedHelp_numeric.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.NeedHelp_numeric.Location = new System.Drawing.Point(293, 83);
            this.NeedHelp_numeric.Maximum = new decimal(new int[] {
            2147483647,
            0,
            0,
            0});
            this.NeedHelp_numeric.Minimum = new decimal(new int[] {
            -2147483648,
            0,
            0,
            -2147483648});
            this.NeedHelp_numeric.Name = "NeedHelp_numeric";
            this.NeedHelp_numeric.Size = new System.Drawing.Size(80, 20);
            this.NeedHelp_numeric.TabIndex = 58;
            this.NeedHelp_numeric.KeyDown += new System.Windows.Forms.KeyEventHandler(this.UnderNpcAndMobs_EnterPress);
            this.NeedHelp_numeric.Leave += new System.EventHandler(this.UnderNpcAndMobs_Leave);
            // 
            // bNeedHelp
            // 
            this.bNeedHelp.AutoSize = true;
            this.bNeedHelp.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.bNeedHelp.Location = new System.Drawing.Point(293, 170);
            this.bNeedHelp.Name = "bNeedHelp";
            this.bNeedHelp.Size = new System.Drawing.Size(80, 17);
            this.bNeedHelp.TabIndex = 37;
            this.bNeedHelp.Text = "bNeedHelp";
            this.bNeedHelp.TextImageRelation = System.Windows.Forms.TextImageRelation.TextBeforeImage;
            this.bNeedHelp.UseVisualStyleBackColor = true;
            this.bNeedHelp.KeyDown += new System.Windows.Forms.KeyEventHandler(this.UnderNpcAndMobs_EnterPress);
            this.bNeedHelp.Leave += new System.EventHandler(this.UnderNpcAndMobs_Leave);
            // 
            // label32
            // 
            this.label32.AutoSize = true;
            this.label32.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label32.Location = new System.Drawing.Point(199, 83);
            this.label32.Name = "label32";
            this.label32.Size = new System.Drawing.Size(96, 15);
            this.label32.TabIndex = 59;
            this.label32.Text = "Нужна помощь:";
            // 
            // AskHelp_numeric
            // 
            this.AskHelp_numeric.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.AskHelp_numeric.Location = new System.Drawing.Point(293, 60);
            this.AskHelp_numeric.Maximum = new decimal(new int[] {
            2147483647,
            0,
            0,
            0});
            this.AskHelp_numeric.Minimum = new decimal(new int[] {
            -2147483648,
            0,
            0,
            -2147483648});
            this.AskHelp_numeric.Name = "AskHelp_numeric";
            this.AskHelp_numeric.Size = new System.Drawing.Size(80, 20);
            this.AskHelp_numeric.TabIndex = 56;
            this.AskHelp_numeric.KeyDown += new System.Windows.Forms.KeyEventHandler(this.UnderNpcAndMobs_EnterPress);
            this.AskHelp_numeric.Leave += new System.EventHandler(this.UnderNpcAndMobs_Leave);
            // 
            // NpcsGroupGrid
            // 
            this.NpcsGroupGrid.AllowUserToAddRows = false;
            this.NpcsGroupGrid.AllowUserToDeleteRows = false;
            this.NpcsGroupGrid.AllowUserToResizeColumns = false;
            this.NpcsGroupGrid.AllowUserToResizeRows = false;
            this.NpcsGroupGrid.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.NpcsGroupGrid.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(15)))), ((int)(((byte)(15)))));
            this.NpcsGroupGrid.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None;
            this.NpcsGroupGrid.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            this.NpcsGroupGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.NpcsGroupGrid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewTextBoxColumn3,
            this.dataGridViewTextBoxColumn4,
            this.dataGridViewTextBoxColumn5});
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(15)))), ((int)(((byte)(15)))));
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            dataGridViewCellStyle3.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.NpcsGroupGrid.DefaultCellStyle = dataGridViewCellStyle3;
            this.NpcsGroupGrid.EnableHeadersVisualStyles = false;
            this.NpcsGroupGrid.Location = new System.Drawing.Point(379, 11);
            this.NpcsGroupGrid.Name = "NpcsGroupGrid";
            this.NpcsGroupGrid.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            this.NpcsGroupGrid.RowHeadersVisible = false;
            this.NpcsGroupGrid.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.NpcsGroupGrid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.NpcsGroupGrid.ShowCellErrors = false;
            this.NpcsGroupGrid.ShowCellToolTips = false;
            this.NpcsGroupGrid.ShowEditingIcon = false;
            this.NpcsGroupGrid.ShowRowErrors = false;
            this.NpcsGroupGrid.Size = new System.Drawing.Size(218, 186);
            this.NpcsGroupGrid.TabIndex = 15;
            this.NpcsGroupGrid.CurrentCellChanged += new System.EventHandler(this.UnderExistenceGrid_CellChanged);
            // 
            // dataGridViewTextBoxColumn3
            // 
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.dataGridViewTextBoxColumn3.DefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridViewTextBoxColumn3.FillWeight = 6.912442F;
            this.dataGridViewTextBoxColumn3.HeaderText = "#";
            this.dataGridViewTextBoxColumn3.Name = "dataGridViewTextBoxColumn3";
            this.dataGridViewTextBoxColumn3.ReadOnly = true;
            this.dataGridViewTextBoxColumn3.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.dataGridViewTextBoxColumn3.Width = 47;
            // 
            // dataGridViewTextBoxColumn4
            // 
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.dataGridViewTextBoxColumn4.DefaultCellStyle = dataGridViewCellStyle2;
            this.dataGridViewTextBoxColumn4.FillWeight = 44.7233F;
            this.dataGridViewTextBoxColumn4.HeaderText = "  ID";
            this.dataGridViewTextBoxColumn4.Name = "dataGridViewTextBoxColumn4";
            this.dataGridViewTextBoxColumn4.ReadOnly = true;
            this.dataGridViewTextBoxColumn4.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.dataGridViewTextBoxColumn4.Width = 50;
            // 
            // dataGridViewTextBoxColumn5
            // 
            this.dataGridViewTextBoxColumn5.HeaderText = "Name";
            this.dataGridViewTextBoxColumn5.Name = "dataGridViewTextBoxColumn5";
            this.dataGridViewTextBoxColumn5.ReadOnly = true;
            this.dataGridViewTextBoxColumn5.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.dataGridViewTextBoxColumn5.Width = 125;
            // 
            // ExistenceGroupCloneButton
            // 
            this.ExistenceGroupCloneButton.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.ExistenceGroupCloneButton.ForeColor = System.Drawing.Color.Black;
            this.ExistenceGroupCloneButton.Image = ((System.Drawing.Image)(resources.GetObject("ExistenceGroupCloneButton.Image")));
            this.ExistenceGroupCloneButton.Location = new System.Drawing.Point(378, 198);
            this.ExistenceGroupCloneButton.Name = "ExistenceGroupCloneButton";
            this.ExistenceGroupCloneButton.Size = new System.Drawing.Size(110, 29);
            this.ExistenceGroupCloneButton.TabIndex = 15;
            this.ExistenceGroupCloneButton.Text = "Клонировать";
            this.ExistenceGroupCloneButton.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.ExistenceGroupCloneButton.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.ExistenceGroupCloneButton.UseVisualStyleBackColor = true;
            this.ExistenceGroupCloneButton.Click += new System.EventHandler(this.CloneNpcinGroupButton_Click);
            // 
            // label31
            // 
            this.label31.AutoSize = true;
            this.label31.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label31.Location = new System.Drawing.Point(193, 60);
            this.label31.Name = "label31";
            this.label31.Size = new System.Drawing.Size(102, 15);
            this.label31.TabIndex = 57;
            this.label31.Text = "Просит помощь:";
            // 
            // ExistenceGroupRemoveButton
            // 
            this.ExistenceGroupRemoveButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.ExistenceGroupRemoveButton.ForeColor = System.Drawing.Color.Black;
            this.ExistenceGroupRemoveButton.Image = ((System.Drawing.Image)(resources.GetObject("ExistenceGroupRemoveButton.Image")));
            this.ExistenceGroupRemoveButton.Location = new System.Drawing.Point(488, 198);
            this.ExistenceGroupRemoveButton.Name = "ExistenceGroupRemoveButton";
            this.ExistenceGroupRemoveButton.Size = new System.Drawing.Size(110, 29);
            this.ExistenceGroupRemoveButton.TabIndex = 16;
            this.ExistenceGroupRemoveButton.Text = "Удалить";
            this.ExistenceGroupRemoveButton.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.ExistenceGroupRemoveButton.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.ExistenceGroupRemoveButton.UseVisualStyleBackColor = true;
            this.ExistenceGroupRemoveButton.Click += new System.EventHandler(this.DeleteNpcinGroupButton_Click);
            // 
            // Turn_numeric
            // 
            this.Turn_numeric.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Turn_numeric.DecimalPlaces = 3;
            this.Turn_numeric.Increment = new decimal(new int[] {
            3,
            0,
            0,
            0});
            this.Turn_numeric.Location = new System.Drawing.Point(293, 14);
            this.Turn_numeric.Maximum = new decimal(new int[] {
            2147483647,
            0,
            0,
            0});
            this.Turn_numeric.Minimum = new decimal(new int[] {
            -2147483648,
            0,
            0,
            -2147483648});
            this.Turn_numeric.Name = "Turn_numeric";
            this.Turn_numeric.Size = new System.Drawing.Size(80, 20);
            this.Turn_numeric.TabIndex = 54;
            this.Turn_numeric.KeyDown += new System.Windows.Forms.KeyEventHandler(this.UnderNpcAndMobs_EnterPress);
            this.Turn_numeric.Leave += new System.EventHandler(this.UnderNpcAndMobs_Leave);
            // 
            // label30
            // 
            this.label30.AutoSize = true;
            this.label30.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label30.Location = new System.Drawing.Point(196, 14);
            this.label30.Name = "label30";
            this.label30.Size = new System.Drawing.Size(99, 15);
            this.label30.TabIndex = 55;
            this.label30.Text = "Оффсет повор:";
            // 
            // Water_numeric
            // 
            this.Water_numeric.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Water_numeric.DecimalPlaces = 3;
            this.Water_numeric.Location = new System.Drawing.Point(108, 200);
            this.Water_numeric.Maximum = new decimal(new int[] {
            2147483647,
            0,
            0,
            0});
            this.Water_numeric.Minimum = new decimal(new int[] {
            -2147483648,
            0,
            0,
            -2147483648});
            this.Water_numeric.Name = "Water_numeric";
            this.Water_numeric.Size = new System.Drawing.Size(80, 20);
            this.Water_numeric.TabIndex = 52;
            this.Water_numeric.KeyDown += new System.Windows.Forms.KeyEventHandler(this.UnderNpcAndMobs_EnterPress);
            this.Water_numeric.Leave += new System.EventHandler(this.UnderNpcAndMobs_Leave);
            // 
            // Path_numeric
            // 
            this.Path_numeric.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Path_numeric.Location = new System.Drawing.Point(108, 177);
            this.Path_numeric.Maximum = new decimal(new int[] {
            2147483647,
            0,
            0,
            0});
            this.Path_numeric.Minimum = new decimal(new int[] {
            -2147483648,
            0,
            0,
            -2147483648});
            this.Path_numeric.Name = "Path_numeric";
            this.Path_numeric.Size = new System.Drawing.Size(80, 20);
            this.Path_numeric.TabIndex = 50;
            this.Path_numeric.KeyDown += new System.Windows.Forms.KeyEventHandler(this.UnderNpcAndMobs_EnterPress);
            this.Path_numeric.Leave += new System.EventHandler(this.UnderNpcAndMobs_Leave);
            // 
            // Path_speed
            // 
            this.Path_speed.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Path_speed.Location = new System.Drawing.Point(108, 154);
            this.Path_speed.Maximum = new decimal(new int[] {
            2147483647,
            0,
            0,
            0});
            this.Path_speed.Minimum = new decimal(new int[] {
            -2147483648,
            0,
            0,
            -2147483648});
            this.Path_speed.Name = "Path_speed";
            this.Path_speed.Size = new System.Drawing.Size(80, 20);
            this.Path_speed.TabIndex = 48;
            this.Path_speed.KeyDown += new System.Windows.Forms.KeyEventHandler(this.UnderNpcAndMobs_EnterPress);
            this.Path_speed.Leave += new System.EventHandler(this.UnderNpcAndMobs_Leave);
            // 
            // Path_type
            // 
            this.Path_type.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(128)))));
            this.Path_type.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.Path_type.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Path_type.FormattingEnabled = true;
            this.Path_type.Items.AddRange(new object[] {
            "Нет",
            "Обычный",
            "Зацикленный"});
            this.Path_type.Location = new System.Drawing.Point(108, 130);
            this.Path_type.Name = "Path_type";
            this.Path_type.Size = new System.Drawing.Size(80, 21);
            this.Path_type.TabIndex = 46;
            this.Path_type.KeyDown += new System.Windows.Forms.KeyEventHandler(this.UnderNpcAndMobs_EnterPress);
            this.Path_type.Leave += new System.EventHandler(this.UnderNpcAndMobs_Leave);
            // 
            // Agression
            // 
            this.Agression.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(128)))));
            this.Agression.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.Agression.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Agression.FormattingEnabled = true;
            this.Agression.Items.AddRange(new object[] {
            "Нет",
            "Агрессивный",
            "Защита"});
            this.Agression.Location = new System.Drawing.Point(108, 106);
            this.Agression.Name = "Agression";
            this.Agression.Size = new System.Drawing.Size(80, 21);
            this.Agression.TabIndex = 37;
            this.Agression.KeyDown += new System.Windows.Forms.KeyEventHandler(this.UnderNpcAndMobs_EnterPress);
            this.Agression.Leave += new System.EventHandler(this.UnderNpcAndMobs_Leave);
            // 
            // DeathAmount_numeric
            // 
            this.DeathAmount_numeric.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.DeathAmount_numeric.Location = new System.Drawing.Point(108, 83);
            this.DeathAmount_numeric.Maximum = new decimal(new int[] {
            2147483647,
            0,
            0,
            0});
            this.DeathAmount_numeric.Minimum = new decimal(new int[] {
            -2147483648,
            0,
            0,
            -2147483648});
            this.DeathAmount_numeric.Name = "DeathAmount_numeric";
            this.DeathAmount_numeric.Size = new System.Drawing.Size(80, 20);
            this.DeathAmount_numeric.TabIndex = 44;
            this.DeathAmount_numeric.KeyDown += new System.Windows.Forms.KeyEventHandler(this.UnderNpcAndMobs_EnterPress);
            this.DeathAmount_numeric.Leave += new System.EventHandler(this.UnderNpcAndMobs_Leave);
            // 
            // Respawn_numeric
            // 
            this.Respawn_numeric.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Respawn_numeric.Location = new System.Drawing.Point(108, 60);
            this.Respawn_numeric.Maximum = new decimal(new int[] {
            2147483647,
            0,
            0,
            0});
            this.Respawn_numeric.Minimum = new decimal(new int[] {
            -2147483648,
            0,
            0,
            -2147483648});
            this.Respawn_numeric.Name = "Respawn_numeric";
            this.Respawn_numeric.Size = new System.Drawing.Size(80, 20);
            this.Respawn_numeric.TabIndex = 42;
            this.Respawn_numeric.KeyDown += new System.Windows.Forms.KeyEventHandler(this.UnderNpcAndMobs_EnterPress);
            this.Respawn_numeric.Leave += new System.EventHandler(this.UnderNpcAndMobs_Leave);
            // 
            // label23
            // 
            this.label23.AutoSize = true;
            this.label23.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label23.Location = new System.Drawing.Point(5, 60);
            this.label23.Name = "label23";
            this.label23.Size = new System.Drawing.Size(106, 15);
            this.label23.TabIndex = 41;
            this.label23.Text = "Время респавна:";
            // 
            // Amount_numeric
            // 
            this.Amount_numeric.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Amount_numeric.Location = new System.Drawing.Point(108, 37);
            this.Amount_numeric.Maximum = new decimal(new int[] {
            2147483647,
            0,
            0,
            0});
            this.Amount_numeric.Minimum = new decimal(new int[] {
            -2147483648,
            0,
            0,
            -2147483648});
            this.Amount_numeric.Name = "Amount_numeric";
            this.Amount_numeric.Size = new System.Drawing.Size(80, 20);
            this.Amount_numeric.TabIndex = 40;
            this.Amount_numeric.KeyDown += new System.Windows.Forms.KeyEventHandler(this.UnderNpcAndMobs_EnterPress);
            this.Amount_numeric.Leave += new System.EventHandler(this.UnderNpcAndMobs_Leave);
            // 
            // label22
            // 
            this.label22.AutoSize = true;
            this.label22.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label22.Location = new System.Drawing.Point(32, 37);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(79, 15);
            this.label22.TabIndex = 39;
            this.label22.Text = "Количество:";
            // 
            // Id_numeric
            // 
            this.Id_numeric.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.Id_numeric.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Id_numeric.Location = new System.Drawing.Point(108, 14);
            this.Id_numeric.Maximum = new decimal(new int[] {
            2147483647,
            0,
            0,
            0});
            this.Id_numeric.Minimum = new decimal(new int[] {
            -2147483648,
            0,
            0,
            -2147483648});
            this.Id_numeric.Name = "Id_numeric";
            this.Id_numeric.Size = new System.Drawing.Size(80, 20);
            this.Id_numeric.TabIndex = 38;
            this.Id_numeric.DoubleClick += new System.EventHandler(this.Id_numeric_DoubleClick);
            this.Id_numeric.KeyDown += new System.Windows.Forms.KeyEventHandler(this.UnderNpcAndMobs_EnterPress);
            this.Id_numeric.Leave += new System.EventHandler(this.UnderNpcAndMobs_Leave);
            // 
            // label21
            // 
            this.label21.AutoSize = true;
            this.label21.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label21.Location = new System.Drawing.Point(89, 15);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(22, 15);
            this.label21.TabIndex = 37;
            this.label21.Text = "ID:";
            // 
            // label29
            // 
            this.label29.AutoSize = true;
            this.label29.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label29.Location = new System.Drawing.Point(16, 201);
            this.label29.Name = "label29";
            this.label29.Size = new System.Drawing.Size(94, 15);
            this.label29.TabIndex = 53;
            this.label29.Text = "Оффсет воды:\r\n";
            // 
            // label34
            // 
            this.label34.AutoSize = true;
            this.label34.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label34.Location = new System.Drawing.Point(206, 130);
            this.label34.Name = "label34";
            this.label34.Size = new System.Drawing.Size(87, 15);
            this.label34.TabIndex = 64;
            this.label34.Text = "RefreshLower:";
            // 
            // label24
            // 
            this.label24.AutoSize = true;
            this.label24.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label24.Location = new System.Drawing.Point(8, 83);
            this.label24.Name = "label24";
            this.label24.Size = new System.Drawing.Size(103, 15);
            this.label24.TabIndex = 43;
            this.label24.Text = "Кол-во смертей:";
            // 
            // label27
            // 
            this.label27.AutoSize = true;
            this.label27.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label27.Location = new System.Drawing.Point(45, 154);
            this.label27.Name = "label27";
            this.label27.Size = new System.Drawing.Size(65, 15);
            this.label27.TabIndex = 49;
            this.label27.Text = "Скорость:";
            // 
            // label26
            // 
            this.label26.AutoSize = true;
            this.label26.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label26.Location = new System.Drawing.Point(51, 130);
            this.label26.Name = "label26";
            this.label26.Size = new System.Drawing.Size(60, 15);
            this.label26.TabIndex = 47;
            this.label26.Text = "Тип пути:";
            // 
            // label28
            // 
            this.label28.AutoSize = true;
            this.label28.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label28.Location = new System.Drawing.Point(72, 177);
            this.label28.Name = "label28";
            this.label28.Size = new System.Drawing.Size(38, 15);
            this.label28.TabIndex = 51;
            this.label28.Text = "Путь:";
            // 
            // label25
            // 
            this.label25.AutoSize = true;
            this.label25.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label25.Location = new System.Drawing.Point(49, 106);
            this.label25.Name = "label25";
            this.label25.Size = new System.Drawing.Size(62, 15);
            this.label25.TabIndex = 45;
            this.label25.Text = "Агрессия:";
            // 
            // label33
            // 
            this.label33.AutoSize = true;
            this.label33.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label33.Location = new System.Drawing.Point(184, 107);
            this.label33.Name = "label33";
            this.label33.Size = new System.Drawing.Size(110, 15);
            this.label33.TabIndex = 62;
            this.label33.Text = "Показ трупа(Сек):";
            // 
            // NpcMobsGrid
            // 
            this.NpcMobsGrid.AllowUserToAddRows = false;
            this.NpcMobsGrid.AllowUserToDeleteRows = false;
            this.NpcMobsGrid.AllowUserToResizeColumns = false;
            this.NpcMobsGrid.AllowUserToResizeRows = false;
            this.NpcMobsGrid.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.NpcMobsGrid.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(15)))), ((int)(((byte)(15)))));
            this.NpcMobsGrid.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None;
            this.NpcMobsGrid.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.NpcMobsGrid.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle4;
            this.NpcMobsGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.NpcMobsGrid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewTextBoxColumn1,
            this.dataGridViewTextBoxColumn2,
            this.Column4});
            this.NpcMobsGrid.ContextMenuStrip = this.ExistenceContext;
            dataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle8.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(15)))), ((int)(((byte)(15)))));
            dataGridViewCellStyle8.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            dataGridViewCellStyle8.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            dataGridViewCellStyle8.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle8.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle8.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.NpcMobsGrid.DefaultCellStyle = dataGridViewCellStyle8;
            this.NpcMobsGrid.EnableHeadersVisualStyles = false;
            this.NpcMobsGrid.Location = new System.Drawing.Point(1, 29);
            this.NpcMobsGrid.Name = "NpcMobsGrid";
            this.NpcMobsGrid.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            this.NpcMobsGrid.RowHeadersVisible = false;
            this.NpcMobsGrid.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.NpcMobsGrid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.NpcMobsGrid.ShowCellErrors = false;
            this.NpcMobsGrid.ShowCellToolTips = false;
            this.NpcMobsGrid.ShowEditingIcon = false;
            this.NpcMobsGrid.ShowRowErrors = false;
            this.NpcMobsGrid.Size = new System.Drawing.Size(359, 425);
            this.NpcMobsGrid.TabIndex = 15;
            this.NpcMobsGrid.CellMouseEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.NpcMobsGrid_CellMouseEnter);
            this.NpcMobsGrid.CurrentCellChanged += new System.EventHandler(this.ExistenceGrid_CellChanged);
            this.NpcMobsGrid.KeyDown += new System.Windows.Forms.KeyEventHandler(this.GridsKeyDown);
            // 
            // dataGridViewTextBoxColumn1
            // 
            dataGridViewCellStyle5.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.dataGridViewTextBoxColumn1.DefaultCellStyle = dataGridViewCellStyle5;
            this.dataGridViewTextBoxColumn1.FillWeight = 6.912442F;
            this.dataGridViewTextBoxColumn1.HeaderText = "#";
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            this.dataGridViewTextBoxColumn1.ReadOnly = true;
            this.dataGridViewTextBoxColumn1.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridViewTextBoxColumn1.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.dataGridViewTextBoxColumn1.Width = 45;
            // 
            // dataGridViewTextBoxColumn2
            // 
            dataGridViewCellStyle6.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            dataGridViewCellStyle6.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(77)))), ((int)(((byte)(255)))), ((int)(((byte)(143)))));
            this.dataGridViewTextBoxColumn2.DefaultCellStyle = dataGridViewCellStyle6;
            this.dataGridViewTextBoxColumn2.FillWeight = 44.7233F;
            this.dataGridViewTextBoxColumn2.HeaderText = "  ID";
            this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            this.dataGridViewTextBoxColumn2.ReadOnly = true;
            this.dataGridViewTextBoxColumn2.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridViewTextBoxColumn2.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.dataGridViewTextBoxColumn2.Width = 95;
            // 
            // Column4
            // 
            dataGridViewCellStyle7.ForeColor = System.Drawing.Color.White;
            this.Column4.DefaultCellStyle = dataGridViewCellStyle7;
            this.Column4.HeaderText = "Name";
            this.Column4.Name = "Column4";
            this.Column4.ReadOnly = true;
            this.Column4.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.Column4.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.Column4.Width = 217;
            // 
            // ExistenceContext
            // 
            this.ExistenceContext.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.LineUpX,
            this.LineUpZ,
            this.toolStripSeparator1,
            this.toolStripMenuItem1,
            this.toolStripSeparator2,
            this.экспортToolStripMenuItem,
            this.импортToolStripMenuItem});
            this.ExistenceContext.Name = "TriggersContext";
            this.ExistenceContext.Size = new System.Drawing.Size(192, 126);
            this.ExistenceContext.Text = "Дополнительно";
            // 
            // LineUpX
            // 
            this.LineUpX.Image = ((System.Drawing.Image)(resources.GetObject("LineUpX.Image")));
            this.LineUpX.Name = "LineUpX";
            this.LineUpX.Size = new System.Drawing.Size(191, 22);
            this.LineUpX.Text = "Выстроить в ряд по X";
            this.LineUpX.Click += new System.EventHandler(this.LineUpX_Click);
            // 
            // LineUpZ
            // 
            this.LineUpZ.Image = ((System.Drawing.Image)(resources.GetObject("LineUpZ.Image")));
            this.LineUpZ.Name = "LineUpZ";
            this.LineUpZ.Size = new System.Drawing.Size(191, 22);
            this.LineUpZ.Text = "Выстроить в ряд по Z";
            this.LineUpZ.Click += new System.EventHandler(this.LineUpZ_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(188, 6);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.UpExistence,
            this.DownExistence,
            this.toolStripSeparator4,
            this.вНачалоToolStripMenuItem,
            this.ExistenceToEnd});
            this.toolStripMenuItem1.Image = ((System.Drawing.Image)(resources.GetObject("toolStripMenuItem1.Image")));
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(191, 22);
            this.toolStripMenuItem1.Text = "Переместить";
            // 
            // UpExistence
            // 
            this.UpExistence.Image = ((System.Drawing.Image)(resources.GetObject("UpExistence.Image")));
            this.UpExistence.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.UpExistence.Name = "UpExistence";
            this.UpExistence.Size = new System.Drawing.Size(180, 22);
            this.UpExistence.Text = "Выше   Shift+W";
            this.UpExistence.Click += new System.EventHandler(this.UpObjects);
            // 
            // DownExistence
            // 
            this.DownExistence.Image = ((System.Drawing.Image)(resources.GetObject("DownExistence.Image")));
            this.DownExistence.Name = "DownExistence";
            this.DownExistence.Size = new System.Drawing.Size(180, 22);
            this.DownExistence.Text = "Ниже    Shift+S";
            this.DownExistence.Click += new System.EventHandler(this.DownObjects);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(177, 6);
            // 
            // вНачалоToolStripMenuItem
            // 
            this.вНачалоToolStripMenuItem.Name = "вНачалоToolStripMenuItem";
            this.вНачалоToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.вНачалоToolStripMenuItem.Text = "To the begining";
            // 
            // ExistenceToEnd
            // 
            this.ExistenceToEnd.Name = "ExistenceToEnd";
            this.ExistenceToEnd.Size = new System.Drawing.Size(180, 22);
            this.ExistenceToEnd.Text = "In the end";
            this.ExistenceToEnd.Click += new System.EventHandler(this.ExistenceToEnd_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(188, 6);
            // 
            // экспортToolStripMenuItem
            // 
            this.экспортToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("экспортToolStripMenuItem.Image")));
            this.экспортToolStripMenuItem.Name = "экспортToolStripMenuItem";
            this.экспортToolStripMenuItem.Size = new System.Drawing.Size(191, 22);
            this.экспортToolStripMenuItem.Text = "Экспорт";
            this.экспортToolStripMenuItem.Click += new System.EventHandler(this.ExportExistence_Click);
            // 
            // импортToolStripMenuItem
            // 
            this.импортToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("импортToolStripMenuItem.Image")));
            this.импортToolStripMenuItem.Name = "импортToolStripMenuItem";
            this.импортToolStripMenuItem.Size = new System.Drawing.Size(191, 22);
            this.импортToolStripMenuItem.Text = "Импорт";
            this.импортToolStripMenuItem.Click += new System.EventHandler(this.ImportExistence_Click);
            // 
            // MainGroupBox
            // 
            this.MainGroupBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.MainGroupBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(239)))), ((int)(((byte)(244)))), ((int)(((byte)(250)))));
            this.MainGroupBox.Controls.Add(this.ExistenceInsertCordsFromGame);
            this.MainGroupBox.Controls.Add(this.groupBox8);
            this.MainGroupBox.Controls.Add(this.IMaxNuml);
            this.MainGroupBox.Controls.Add(this.Life_time);
            this.MainGroupBox.Controls.Add(this.Trigger);
            this.MainGroupBox.Controls.Add(this.label18);
            this.MainGroupBox.Controls.Add(this.dwGenId);
            this.MainGroupBox.Controls.Add(this.label17);
            this.MainGroupBox.Controls.Add(this.BValicOnce);
            this.MainGroupBox.Controls.Add(this.ExistenceAutoRevive);
            this.MainGroupBox.Controls.Add(this.ExistenceInitGen);
            this.MainGroupBox.Controls.Add(this.Group_type);
            this.MainGroupBox.Controls.Add(this.label16);
            this.MainGroupBox.Controls.Add(this.ExistenceType);
            this.MainGroupBox.Controls.Add(this.label15);
            this.MainGroupBox.Controls.Add(this.Z_scatter);
            this.MainGroupBox.Controls.Add(this.label12);
            this.MainGroupBox.Controls.Add(this.Y_scatter);
            this.MainGroupBox.Controls.Add(this.label13);
            this.MainGroupBox.Controls.Add(this.X_scatter);
            this.MainGroupBox.Controls.Add(this.label14);
            this.MainGroupBox.Controls.Add(this.Z_rotate);
            this.MainGroupBox.Controls.Add(this.label11);
            this.MainGroupBox.Controls.Add(this.Y_rotate);
            this.MainGroupBox.Controls.Add(this.label10);
            this.MainGroupBox.Controls.Add(this.X_rotate);
            this.MainGroupBox.Controls.Add(this.Z_position);
            this.MainGroupBox.Controls.Add(this.Y_position);
            this.MainGroupBox.Controls.Add(this.label9);
            this.MainGroupBox.Controls.Add(this.X_position);
            this.MainGroupBox.Controls.Add(this.label8);
            this.MainGroupBox.Controls.Add(this.label7);
            this.MainGroupBox.Controls.Add(this.label6);
            this.MainGroupBox.Controls.Add(this.Group_amount_textbox);
            this.MainGroupBox.Controls.Add(this.ExistenceLocating);
            this.MainGroupBox.Controls.Add(this.label3);
            this.MainGroupBox.Controls.Add(this.label5);
            this.MainGroupBox.Controls.Add(this.label19);
            this.MainGroupBox.Controls.Add(this.label20);
            this.MainGroupBox.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.MainGroupBox.Location = new System.Drawing.Point(364, 1);
            this.MainGroupBox.Name = "MainGroupBox";
            this.MainGroupBox.Size = new System.Drawing.Size(599, 250);
            this.MainGroupBox.TabIndex = 12;
            this.MainGroupBox.TabStop = false;
            this.MainGroupBox.Text = "Основное";
            // 
            // ExistenceInsertCordsFromGame
            // 
            this.ExistenceInsertCordsFromGame.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.ExistenceInsertCordsFromGame.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.ExistenceInsertCordsFromGame.ForeColor = System.Drawing.Color.Maroon;
            this.ExistenceInsertCordsFromGame.Location = new System.Drawing.Point(342, 187);
            this.ExistenceInsertCordsFromGame.Name = "ExistenceInsertCordsFromGame";
            this.ExistenceInsertCordsFromGame.Size = new System.Drawing.Size(252, 52);
            this.ExistenceInsertCordsFromGame.TabIndex = 67;
            this.ExistenceInsertCordsFromGame.Text = "Вставить координаты с игры";
            this.ExistenceInsertCordsFromGame.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.ExistenceInsertCordsFromGame.UseVisualStyleBackColor = true;
            this.ExistenceInsertCordsFromGame.Click += new System.EventHandler(this.InsertCordsFromGame_Click);
            // 
            // groupBox8
            // 
            this.groupBox8.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(239)))), ((int)(((byte)(244)))), ((int)(((byte)(250)))));
            this.groupBox8.Controls.Add(this.AddMonsterRespawnTime);
            this.groupBox8.Controls.Add(this.label63);
            this.groupBox8.Controls.Add(this.AddMonsterAmount);
            this.groupBox8.Controls.Add(this.label64);
            this.groupBox8.Controls.Add(this.AddMonsterId);
            this.groupBox8.Controls.Add(this.label65);
            this.groupBox8.Controls.Add(this.AddMonsterType);
            this.groupBox8.Controls.Add(this.label66);
            this.groupBox8.Controls.Add(this.AddintExistenceType);
            this.groupBox8.Controls.Add(this.AddMonsterTrigger);
            this.groupBox8.Controls.Add(this.label67);
            this.groupBox8.Controls.Add(this.label68);
            this.groupBox8.Location = new System.Drawing.Point(342, 11);
            this.groupBox8.Name = "groupBox8";
            this.groupBox8.Size = new System.Drawing.Size(252, 171);
            this.groupBox8.TabIndex = 18;
            this.groupBox8.TabStop = false;
            this.groupBox8.Text = "Настройки для добавления новых существ";
            // 
            // AddMonsterRespawnTime
            // 
            this.AddMonsterRespawnTime.BackColor = System.Drawing.SystemColors.Window;
            this.AddMonsterRespawnTime.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.AddMonsterRespawnTime.Location = new System.Drawing.Point(157, 135);
            this.AddMonsterRespawnTime.Maximum = new decimal(new int[] {
            2147483647,
            0,
            0,
            0});
            this.AddMonsterRespawnTime.Minimum = new decimal(new int[] {
            -2147483648,
            0,
            0,
            -2147483648});
            this.AddMonsterRespawnTime.Name = "AddMonsterRespawnTime";
            this.AddMonsterRespawnTime.Size = new System.Drawing.Size(76, 20);
            this.AddMonsterRespawnTime.TabIndex = 49;
            this.AddMonsterRespawnTime.Value = new decimal(new int[] {
            60,
            0,
            0,
            0});
            // 
            // label63
            // 
            this.label63.AutoSize = true;
            this.label63.Location = new System.Drawing.Point(57, 135);
            this.label63.Name = "label63";
            this.label63.Size = new System.Drawing.Size(94, 13);
            this.label63.TabIndex = 48;
            this.label63.Text = "Время респавна:";
            // 
            // AddMonsterAmount
            // 
            this.AddMonsterAmount.BackColor = System.Drawing.SystemColors.Window;
            this.AddMonsterAmount.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.AddMonsterAmount.Location = new System.Drawing.Point(157, 112);
            this.AddMonsterAmount.Maximum = new decimal(new int[] {
            2147483647,
            0,
            0,
            0});
            this.AddMonsterAmount.Minimum = new decimal(new int[] {
            -2147483648,
            0,
            0,
            -2147483648});
            this.AddMonsterAmount.Name = "AddMonsterAmount";
            this.AddMonsterAmount.Size = new System.Drawing.Size(76, 20);
            this.AddMonsterAmount.TabIndex = 47;
            this.AddMonsterAmount.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // label64
            // 
            this.label64.AutoSize = true;
            this.label64.Location = new System.Drawing.Point(37, 112);
            this.label64.Name = "label64";
            this.label64.Size = new System.Drawing.Size(115, 13);
            this.label64.TabIndex = 46;
            this.label64.Text = "Количество существ:";
            // 
            // AddMonsterId
            // 
            this.AddMonsterId.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.AddMonsterId.Location = new System.Drawing.Point(157, 88);
            this.AddMonsterId.Maximum = new decimal(new int[] {
            2147483647,
            0,
            0,
            0});
            this.AddMonsterId.Minimum = new decimal(new int[] {
            -2147483648,
            0,
            0,
            -2147483648});
            this.AddMonsterId.Name = "AddMonsterId";
            this.AddMonsterId.Size = new System.Drawing.Size(76, 20);
            this.AddMonsterId.TabIndex = 45;
            this.AddMonsterId.Value = new decimal(new int[] {
            16,
            0,
            0,
            0});
            // 
            // label65
            // 
            this.label65.AutoSize = true;
            this.label65.Location = new System.Drawing.Point(3, 89);
            this.label65.Name = "label65";
            this.label65.Size = new System.Drawing.Size(149, 13);
            this.label65.TabIndex = 44;
            this.label65.Text = "ID создаваемого существа:";
            // 
            // AddMonsterType
            // 
            this.AddMonsterType.BackColor = System.Drawing.Color.Silver;
            this.AddMonsterType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.AddMonsterType.FormattingEnabled = true;
            this.AddMonsterType.Items.AddRange(new object[] {
            "Моб",
            "Нпс"});
            this.AddMonsterType.Location = new System.Drawing.Point(157, 63);
            this.AddMonsterType.Name = "AddMonsterType";
            this.AddMonsterType.Size = new System.Drawing.Size(76, 21);
            this.AddMonsterType.TabIndex = 43;
            // 
            // label66
            // 
            this.label66.AutoSize = true;
            this.label66.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label66.Location = new System.Drawing.Point(65, 62);
            this.label66.Name = "label66";
            this.label66.Size = new System.Drawing.Size(88, 15);
            this.label66.TabIndex = 42;
            this.label66.Text = "Тип существа:";
            // 
            // AddintExistenceType
            // 
            this.AddintExistenceType.BackColor = System.Drawing.Color.Silver;
            this.AddintExistenceType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.AddintExistenceType.FormattingEnabled = true;
            this.AddintExistenceType.Items.AddRange(new object[] {
            "Наземный",
            "Свободный"});
            this.AddintExistenceType.Location = new System.Drawing.Point(157, 40);
            this.AddintExistenceType.Name = "AddintExistenceType";
            this.AddintExistenceType.Size = new System.Drawing.Size(76, 21);
            this.AddintExistenceType.TabIndex = 41;
            // 
            // AddMonsterTrigger
            // 
            this.AddMonsterTrigger.BackColor = System.Drawing.SystemColors.Window;
            this.AddMonsterTrigger.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.AddMonsterTrigger.Location = new System.Drawing.Point(157, 17);
            this.AddMonsterTrigger.Maximum = new decimal(new int[] {
            2147483647,
            0,
            0,
            0});
            this.AddMonsterTrigger.Minimum = new decimal(new int[] {
            -2147483648,
            0,
            0,
            -2147483648});
            this.AddMonsterTrigger.Name = "AddMonsterTrigger";
            this.AddMonsterTrigger.Size = new System.Drawing.Size(76, 20);
            this.AddMonsterTrigger.TabIndex = 39;
            // 
            // label67
            // 
            this.label67.AutoSize = true;
            this.label67.Location = new System.Drawing.Point(84, 18);
            this.label67.Name = "label67";
            this.label67.Size = new System.Drawing.Size(69, 13);
            this.label67.TabIndex = 0;
            this.label67.Text = "ID триггера:";
            // 
            // label68
            // 
            this.label68.AutoSize = true;
            this.label68.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label68.Location = new System.Drawing.Point(57, 39);
            this.label68.Name = "label68";
            this.label68.Size = new System.Drawing.Size(96, 15);
            this.label68.TabIndex = 40;
            this.label68.Text = "Расположение:";
            // 
            // IMaxNuml
            // 
            this.IMaxNuml.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.IMaxNuml.Location = new System.Drawing.Point(245, 153);
            this.IMaxNuml.Name = "IMaxNuml";
            this.IMaxNuml.Size = new System.Drawing.Size(91, 20);
            this.IMaxNuml.TabIndex = 35;
            this.IMaxNuml.KeyDown += new System.Windows.Forms.KeyEventHandler(this.NpcAndMobsDefault_EnterPress);
            this.IMaxNuml.Leave += new System.EventHandler(this.NpcAndMobsDefaultLeave);
            // 
            // Life_time
            // 
            this.Life_time.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Life_time.Location = new System.Drawing.Point(245, 130);
            this.Life_time.Name = "Life_time";
            this.Life_time.Size = new System.Drawing.Size(91, 20);
            this.Life_time.TabIndex = 33;
            this.Life_time.KeyDown += new System.Windows.Forms.KeyEventHandler(this.NpcAndMobsDefault_EnterPress);
            this.Life_time.Leave += new System.EventHandler(this.NpcAndMobsDefaultLeave);
            // 
            // Trigger
            // 
            this.Trigger.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Trigger.ContextMenuStrip = this.contextMenuStrip2;
            this.Trigger.Location = new System.Drawing.Point(245, 107);
            this.Trigger.Name = "Trigger";
            this.Trigger.Size = new System.Drawing.Size(91, 20);
            this.Trigger.TabIndex = 31;
            this.Trigger.KeyDown += new System.Windows.Forms.KeyEventHandler(this.NpcAndMobsDefault_EnterPress);
            this.Trigger.Leave += new System.EventHandler(this.NpcAndMobsDefaultLeave);
            // 
            // contextMenuStrip2
            // 
            this.contextMenuStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MoveToTrigger});
            this.contextMenuStrip2.Name = "TriggersContext";
            this.contextMenuStrip2.Size = new System.Drawing.Size(182, 26);
            this.contextMenuStrip2.Text = "Дополнительно";
            // 
            // MoveToTrigger
            // 
            this.MoveToTrigger.Image = ((System.Drawing.Image)(resources.GetObject("MoveToTrigger.Image")));
            this.MoveToTrigger.Name = "MoveToTrigger";
            this.MoveToTrigger.Size = new System.Drawing.Size(181, 22);
            this.MoveToTrigger.Text = "Перейти к триггеру";
            this.MoveToTrigger.Click += new System.EventHandler(this.MoveToTrigger_Click);
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label18.Location = new System.Drawing.Point(191, 109);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(55, 15);
            this.label18.TabIndex = 32;
            this.label18.Text = "Триггер:";
            // 
            // dwGenId
            // 
            this.dwGenId.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.dwGenId.Location = new System.Drawing.Point(245, 83);
            this.dwGenId.Name = "dwGenId";
            this.dwGenId.Size = new System.Drawing.Size(91, 20);
            this.dwGenId.TabIndex = 29;
            this.dwGenId.KeyDown += new System.Windows.Forms.KeyEventHandler(this.NpcAndMobsDefault_EnterPress);
            this.dwGenId.Leave += new System.EventHandler(this.NpcAndMobsDefaultLeave);
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label17.Location = new System.Drawing.Point(187, 85);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(59, 15);
            this.label17.TabIndex = 30;
            this.label17.Text = "dwGenId:";
            // 
            // BValicOnce
            // 
            this.BValicOnce.AutoSize = true;
            this.BValicOnce.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.BValicOnce.Location = new System.Drawing.Point(261, 221);
            this.BValicOnce.Name = "BValicOnce";
            this.BValicOnce.Size = new System.Drawing.Size(75, 17);
            this.BValicOnce.TabIndex = 28;
            this.BValicOnce.Text = "ValidOnce";
            this.BValicOnce.TextImageRelation = System.Windows.Forms.TextImageRelation.TextBeforeImage;
            this.BValicOnce.UseVisualStyleBackColor = true;
            this.BValicOnce.KeyDown += new System.Windows.Forms.KeyEventHandler(this.NpcAndMobsDefault_EnterPress);
            this.BValicOnce.Leave += new System.EventHandler(this.NpcAndMobsDefaultLeave);
            // 
            // ExistenceAutoRevive
            // 
            this.ExistenceAutoRevive.AutoSize = true;
            this.ExistenceAutoRevive.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.ExistenceAutoRevive.Location = new System.Drawing.Point(201, 200);
            this.ExistenceAutoRevive.Name = "ExistenceAutoRevive";
            this.ExistenceAutoRevive.Size = new System.Drawing.Size(135, 17);
            this.ExistenceAutoRevive.TabIndex = 27;
            this.ExistenceAutoRevive.Text = "Мгновенный респавн";
            this.ExistenceAutoRevive.TextImageRelation = System.Windows.Forms.TextImageRelation.TextBeforeImage;
            this.ExistenceAutoRevive.UseVisualStyleBackColor = true;
            this.ExistenceAutoRevive.KeyDown += new System.Windows.Forms.KeyEventHandler(this.NpcAndMobsDefault_EnterPress);
            this.ExistenceAutoRevive.Leave += new System.EventHandler(this.NpcAndMobsDefaultLeave);
            // 
            // ExistenceInitGen
            // 
            this.ExistenceInitGen.AutoSize = true;
            this.ExistenceInitGen.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.ExistenceInitGen.Location = new System.Drawing.Point(184, 178);
            this.ExistenceInitGen.Name = "ExistenceInitGen";
            this.ExistenceInitGen.Size = new System.Drawing.Size(152, 17);
            this.ExistenceInitGen.TabIndex = 26;
            this.ExistenceInitGen.Text = "Активировать генератор";
            this.ExistenceInitGen.TextImageRelation = System.Windows.Forms.TextImageRelation.TextBeforeImage;
            this.ExistenceInitGen.UseVisualStyleBackColor = true;
            this.ExistenceInitGen.KeyDown += new System.Windows.Forms.KeyEventHandler(this.NpcAndMobsDefault_EnterPress);
            this.ExistenceInitGen.Leave += new System.EventHandler(this.NpcAndMobsDefaultLeave);
            // 
            // Group_type
            // 
            this.Group_type.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Group_type.Location = new System.Drawing.Point(245, 60);
            this.Group_type.Name = "Group_type";
            this.Group_type.Size = new System.Drawing.Size(91, 20);
            this.Group_type.TabIndex = 24;
            this.Group_type.KeyDown += new System.Windows.Forms.KeyEventHandler(this.NpcAndMobsDefault_EnterPress);
            this.Group_type.Leave += new System.EventHandler(this.NpcAndMobsDefaultLeave);
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label16.Location = new System.Drawing.Point(172, 62);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(74, 15);
            this.label16.TabIndex = 25;
            this.label16.Text = "Тип группы:";
            // 
            // ExistenceType
            // 
            this.ExistenceType.BackColor = System.Drawing.Color.Silver;
            this.ExistenceType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ExistenceType.FormattingEnabled = true;
            this.ExistenceType.Items.AddRange(new object[] {
            "Моб",
            "Нпс"});
            this.ExistenceType.Location = new System.Drawing.Point(245, 11);
            this.ExistenceType.Name = "ExistenceType";
            this.ExistenceType.Size = new System.Drawing.Size(91, 21);
            this.ExistenceType.TabIndex = 23;
            this.ExistenceType.KeyDown += new System.Windows.Forms.KeyEventHandler(this.NpcAndMobsDefault_EnterPress);
            this.ExistenceType.Leave += new System.EventHandler(this.NpcAndMobsDefaultLeave);
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label15.Location = new System.Drawing.Point(215, 12);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(31, 15);
            this.label15.TabIndex = 22;
            this.label15.Text = "Тип:";
            // 
            // Z_scatter
            // 
            this.Z_scatter.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Z_scatter.Location = new System.Drawing.Point(77, 222);
            this.Z_scatter.Name = "Z_scatter";
            this.Z_scatter.Size = new System.Drawing.Size(91, 20);
            this.Z_scatter.TabIndex = 21;
            this.Z_scatter.KeyDown += new System.Windows.Forms.KeyEventHandler(this.NpcAndMobsDefault_EnterPress);
            this.Z_scatter.Leave += new System.EventHandler(this.NpcAndMobsDefaultLeave);
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label12.Location = new System.Drawing.Point(6, 224);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(68, 15);
            this.label12.TabIndex = 20;
            this.label12.Text = "Разброс Z:";
            // 
            // Y_scatter
            // 
            this.Y_scatter.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Y_scatter.Location = new System.Drawing.Point(77, 199);
            this.Y_scatter.Name = "Y_scatter";
            this.Y_scatter.Size = new System.Drawing.Size(91, 20);
            this.Y_scatter.TabIndex = 19;
            this.Y_scatter.KeyDown += new System.Windows.Forms.KeyEventHandler(this.NpcAndMobsDefault_EnterPress);
            this.Y_scatter.Leave += new System.EventHandler(this.NpcAndMobsDefaultLeave);
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label13.Location = new System.Drawing.Point(6, 201);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(68, 15);
            this.label13.TabIndex = 18;
            this.label13.Text = "Разброс Y:";
            // 
            // X_scatter
            // 
            this.X_scatter.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.X_scatter.Location = new System.Drawing.Point(77, 176);
            this.X_scatter.Name = "X_scatter";
            this.X_scatter.Size = new System.Drawing.Size(91, 20);
            this.X_scatter.TabIndex = 17;
            this.X_scatter.KeyDown += new System.Windows.Forms.KeyEventHandler(this.NpcAndMobsDefault_EnterPress);
            this.X_scatter.Leave += new System.EventHandler(this.NpcAndMobsDefaultLeave);
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label14.Location = new System.Drawing.Point(6, 178);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(69, 15);
            this.label14.TabIndex = 16;
            this.label14.Text = "Разброс X:";
            // 
            // Z_rotate
            // 
            this.Z_rotate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Z_rotate.Location = new System.Drawing.Point(77, 152);
            this.Z_rotate.Name = "Z_rotate";
            this.Z_rotate.Size = new System.Drawing.Size(91, 20);
            this.Z_rotate.TabIndex = 15;
            this.Z_rotate.KeyDown += new System.Windows.Forms.KeyEventHandler(this.NpcAndMobsDefault_EnterPress);
            this.Z_rotate.Leave += new System.EventHandler(this.NpcAndMobsDefaultLeave);
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label11.Location = new System.Drawing.Point(5, 154);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(71, 15);
            this.label11.TabIndex = 14;
            this.label11.Text = "Поворот Z:";
            // 
            // Y_rotate
            // 
            this.Y_rotate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Y_rotate.Location = new System.Drawing.Point(77, 129);
            this.Y_rotate.Name = "Y_rotate";
            this.Y_rotate.Size = new System.Drawing.Size(91, 20);
            this.Y_rotate.TabIndex = 13;
            this.Y_rotate.KeyDown += new System.Windows.Forms.KeyEventHandler(this.NpcAndMobsDefault_EnterPress);
            this.Y_rotate.Leave += new System.EventHandler(this.NpcAndMobsDefaultLeave);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label10.Location = new System.Drawing.Point(5, 131);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(71, 15);
            this.label10.TabIndex = 12;
            this.label10.Text = "Поворот Y:";
            // 
            // X_rotate
            // 
            this.X_rotate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.X_rotate.Location = new System.Drawing.Point(77, 106);
            this.X_rotate.Name = "X_rotate";
            this.X_rotate.Size = new System.Drawing.Size(91, 20);
            this.X_rotate.TabIndex = 11;
            this.X_rotate.KeyDown += new System.Windows.Forms.KeyEventHandler(this.NpcAndMobsDefault_EnterPress);
            this.X_rotate.Leave += new System.EventHandler(this.NpcAndMobsDefaultLeave);
            // 
            // Z_position
            // 
            this.Z_position.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Z_position.Location = new System.Drawing.Point(77, 83);
            this.Z_position.Name = "Z_position";
            this.Z_position.Size = new System.Drawing.Size(91, 20);
            this.Z_position.TabIndex = 10;
            this.Z_position.KeyDown += new System.Windows.Forms.KeyEventHandler(this.NpcAndMobsDefault_EnterPress);
            this.Z_position.Leave += new System.EventHandler(this.NpcAndMobsDefaultLeave);
            // 
            // Y_position
            // 
            this.Y_position.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Y_position.Location = new System.Drawing.Point(77, 60);
            this.Y_position.Name = "Y_position";
            this.Y_position.Size = new System.Drawing.Size(91, 20);
            this.Y_position.TabIndex = 9;
            this.Y_position.KeyDown += new System.Windows.Forms.KeyEventHandler(this.NpcAndMobsDefault_EnterPress);
            this.Y_position.Leave += new System.EventHandler(this.NpcAndMobsDefaultLeave);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label9.Location = new System.Drawing.Point(5, 106);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(72, 15);
            this.label9.TabIndex = 8;
            this.label9.Text = "Поворот X:";
            // 
            // X_position
            // 
            this.X_position.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.X_position.Location = new System.Drawing.Point(77, 37);
            this.X_position.Name = "X_position";
            this.X_position.Size = new System.Drawing.Size(91, 20);
            this.X_position.TabIndex = 7;
            this.X_position.KeyDown += new System.Windows.Forms.KeyEventHandler(this.NpcAndMobsDefault_EnterPress);
            this.X_position.Leave += new System.EventHandler(this.NpcAndMobsDefaultLeave);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label8.Location = new System.Drawing.Point(60, 84);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(17, 15);
            this.label8.TabIndex = 6;
            this.label8.Text = "Z:";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label7.Location = new System.Drawing.Point(60, 62);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(17, 15);
            this.label7.TabIndex = 5;
            this.label7.Text = "Y:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label6.Location = new System.Drawing.Point(60, 39);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(18, 15);
            this.label6.TabIndex = 4;
            this.label6.Text = "X:";
            // 
            // Group_amount_textbox
            // 
            this.Group_amount_textbox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Group_amount_textbox.Enabled = false;
            this.Group_amount_textbox.Location = new System.Drawing.Point(245, 37);
            this.Group_amount_textbox.Name = "Group_amount_textbox";
            this.Group_amount_textbox.Size = new System.Drawing.Size(91, 20);
            this.Group_amount_textbox.TabIndex = 2;
            this.Group_amount_textbox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // ExistenceLocating
            // 
            this.ExistenceLocating.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.ExistenceLocating.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ExistenceLocating.FormattingEnabled = true;
            this.ExistenceLocating.Items.AddRange(new object[] {
            "Наземный",
            "Свободный"});
            this.ExistenceLocating.Location = new System.Drawing.Point(77, 11);
            this.ExistenceLocating.Name = "ExistenceLocating";
            this.ExistenceLocating.Size = new System.Drawing.Size(91, 21);
            this.ExistenceLocating.TabIndex = 1;
            this.ExistenceLocating.KeyDown += new System.Windows.Forms.KeyEventHandler(this.NpcAndMobsDefault_EnterPress);
            this.ExistenceLocating.Leave += new System.EventHandler(this.NpcAndMobsDefaultLeave);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label3.Location = new System.Drawing.Point(2, 14);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(75, 15);
            this.label3.TabIndex = 0;
            this.label3.Text = "Расположе:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label5.Location = new System.Drawing.Point(186, 39);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(59, 15);
            this.label5.TabIndex = 3;
            this.label5.Text = "В группе:";
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label19.Location = new System.Drawing.Point(164, 132);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(82, 15);
            this.label19.TabIndex = 34;
            this.label19.Text = "Врем.Жизни:";
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label20.Location = new System.Drawing.Point(169, 155);
            this.label20.Name = "label20";
            this.label20.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.label20.Size = new System.Drawing.Size(77, 15);
            this.label20.TabIndex = 36;
            this.label20.Text = "Макс.колво:";
            this.toolTip1.SetToolTip(this.label20, "Максимальное количество респящихся мобов\r\nMaximum amount of respawn existence");
            // 
            // ExistenceRemoveButton
            // 
            this.ExistenceRemoveButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.ExistenceRemoveButton.ForeColor = System.Drawing.Color.Black;
            this.ExistenceRemoveButton.Image = ((System.Drawing.Image)(resources.GetObject("ExistenceRemoveButton.Image")));
            this.ExistenceRemoveButton.Location = new System.Drawing.Point(180, 454);
            this.ExistenceRemoveButton.Name = "ExistenceRemoveButton";
            this.ExistenceRemoveButton.Size = new System.Drawing.Size(181, 29);
            this.ExistenceRemoveButton.TabIndex = 11;
            this.ExistenceRemoveButton.TabStop = false;
            this.ExistenceRemoveButton.Text = "Удалить";
            this.ExistenceRemoveButton.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.ExistenceRemoveButton.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.ExistenceRemoveButton.UseVisualStyleBackColor = true;
            this.ExistenceRemoveButton.Click += new System.EventHandler(this.RemoveNpcAndMobFull);
            // 
            // ExistenceCloneButton
            // 
            this.ExistenceCloneButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.ExistenceCloneButton.ForeColor = System.Drawing.Color.Black;
            this.ExistenceCloneButton.Image = ((System.Drawing.Image)(resources.GetObject("ExistenceCloneButton.Image")));
            this.ExistenceCloneButton.Location = new System.Drawing.Point(0, 454);
            this.ExistenceCloneButton.Name = "ExistenceCloneButton";
            this.ExistenceCloneButton.Size = new System.Drawing.Size(181, 29);
            this.ExistenceCloneButton.TabIndex = 9;
            this.ExistenceCloneButton.TabStop = false;
            this.ExistenceCloneButton.Text = "Клонировать";
            this.ExistenceCloneButton.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.ExistenceCloneButton.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.ExistenceCloneButton.UseVisualStyleBackColor = true;
            this.ExistenceCloneButton.Click += new System.EventHandler(this.CloneNpcAndMobFull);
            // 
            // ExistenceToolStrip
            // 
            this.ExistenceToolStrip.AutoSize = false;
            this.ExistenceToolStrip.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(239)))), ((int)(((byte)(244)))), ((int)(((byte)(250)))));
            this.ExistenceToolStrip.GripMargin = new System.Windows.Forms.Padding(0);
            this.ExistenceToolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ExportExistence,
            this.ImportExistence,
            this.LineUpExistenceDropDown,
            this.MoveExistenceDropDown});
            this.ExistenceToolStrip.Location = new System.Drawing.Point(3, 3);
            this.ExistenceToolStrip.Name = "ExistenceToolStrip";
            this.ExistenceToolStrip.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.ExistenceToolStrip.Size = new System.Drawing.Size(960, 25);
            this.ExistenceToolStrip.TabIndex = 16;
            this.ExistenceToolStrip.Text = "toolStrip1";
            // 
            // ExportExistence
            // 
            this.ExportExistence.Image = ((System.Drawing.Image)(resources.GetObject("ExportExistence.Image")));
            this.ExportExistence.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.ExportExistence.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.ExportExistence.Name = "ExportExistence";
            this.ExportExistence.Size = new System.Drawing.Size(74, 22);
            this.ExportExistence.Text = "Экспорт";
            this.ExportExistence.ToolTipText = "Export";
            this.ExportExistence.Click += new System.EventHandler(this.ExportExistence_Click);
            // 
            // ImportExistence
            // 
            this.ImportExistence.Image = ((System.Drawing.Image)(resources.GetObject("ImportExistence.Image")));
            this.ImportExistence.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.ImportExistence.Name = "ImportExistence";
            this.ImportExistence.Size = new System.Drawing.Size(71, 22);
            this.ImportExistence.Text = "Импорт";
            this.ImportExistence.ToolTipText = "Import";
            this.ImportExistence.Click += new System.EventHandler(this.ImportExistence_Click);
            // 
            // LineUpExistenceDropDown
            // 
            this.LineUpExistenceDropDown.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ToolStripLineUpX,
            this.ToolStripLineUpZ});
            this.LineUpExistenceDropDown.Image = ((System.Drawing.Image)(resources.GetObject("LineUpExistenceDropDown.Image")));
            this.LineUpExistenceDropDown.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.LineUpExistenceDropDown.Name = "LineUpExistenceDropDown";
            this.LineUpExistenceDropDown.Size = new System.Drawing.Size(95, 22);
            this.LineUpExistenceDropDown.Text = "Выстроить";
            this.LineUpExistenceDropDown.ToolTipText = "Line up";
            // 
            // ToolStripLineUpX
            // 
            this.ToolStripLineUpX.Image = ((System.Drawing.Image)(resources.GetObject("ToolStripLineUpX.Image")));
            this.ToolStripLineUpX.Name = "ToolStripLineUpX";
            this.ToolStripLineUpX.Size = new System.Drawing.Size(100, 22);
            this.ToolStripLineUpX.Text = "По X";
            this.ToolStripLineUpX.Click += new System.EventHandler(this.LineUpX_Click);
            // 
            // ToolStripLineUpZ
            // 
            this.ToolStripLineUpZ.Image = ((System.Drawing.Image)(resources.GetObject("ToolStripLineUpZ.Image")));
            this.ToolStripLineUpZ.Name = "ToolStripLineUpZ";
            this.ToolStripLineUpZ.Size = new System.Drawing.Size(100, 22);
            this.ToolStripLineUpZ.Text = "По Z";
            this.ToolStripLineUpZ.Click += new System.EventHandler(this.LineUpZ_Click);
            // 
            // MoveExistenceDropDown
            // 
            this.MoveExistenceDropDown.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MoveUpToolStripMenuItem,
            this.MoveDownToolStripMenuItem});
            this.MoveExistenceDropDown.Image = ((System.Drawing.Image)(resources.GetObject("MoveExistenceDropDown.Image")));
            this.MoveExistenceDropDown.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.MoveExistenceDropDown.Name = "MoveExistenceDropDown";
            this.MoveExistenceDropDown.Size = new System.Drawing.Size(108, 22);
            this.MoveExistenceDropDown.Text = "Переместить";
            this.MoveExistenceDropDown.ToolTipText = "Move";
            // 
            // MoveUpToolStripMenuItem
            // 
            this.MoveUpToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("MoveUpToolStripMenuItem.Image")));
            this.MoveUpToolStripMenuItem.Name = "MoveUpToolStripMenuItem";
            this.MoveUpToolStripMenuItem.Size = new System.Drawing.Size(159, 22);
            this.MoveUpToolStripMenuItem.Text = "Выше   Shift+W";
            this.MoveUpToolStripMenuItem.Click += new System.EventHandler(this.UpObjects);
            // 
            // MoveDownToolStripMenuItem
            // 
            this.MoveDownToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("MoveDownToolStripMenuItem.Image")));
            this.MoveDownToolStripMenuItem.Name = "MoveDownToolStripMenuItem";
            this.MoveDownToolStripMenuItem.Size = new System.Drawing.Size(159, 22);
            this.MoveDownToolStripMenuItem.Text = "Ниже    Shift+S";
            this.MoveDownToolStripMenuItem.Click += new System.EventHandler(this.DownObjects);
            // 
            // ResourcesTab
            // 
            this.ResourcesTab.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(239)))), ((int)(((byte)(244)))), ((int)(((byte)(250)))));
            this.ResourcesTab.Controls.Add(this.groupBox5);
            this.ResourcesTab.Controls.Add(this.groupBox3);
            this.ResourcesTab.Controls.Add(this.ResourcesGrid);
            this.ResourcesTab.Controls.Add(this.toolStrip1);
            this.ResourcesTab.Controls.Add(this.ResourcesRemoveButton);
            this.ResourcesTab.Controls.Add(this.ResourcesCloneButton);
            this.ResourcesTab.Location = new System.Drawing.Point(4, 22);
            this.ResourcesTab.Name = "ResourcesTab";
            this.ResourcesTab.Padding = new System.Windows.Forms.Padding(3);
            this.ResourcesTab.Size = new System.Drawing.Size(966, 483);
            this.ResourcesTab.TabIndex = 1;
            this.ResourcesTab.Text = "Ресурсы";
            // 
            // groupBox5
            // 
            this.groupBox5.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(239)))), ((int)(((byte)(244)))), ((int)(((byte)(250)))));
            this.groupBox5.Controls.Add(this.RType_numeric);
            this.groupBox5.Controls.Add(this.label39);
            this.groupBox5.Controls.Add(this.RfHeiOff_numeric);
            this.groupBox5.Controls.Add(this.label55);
            this.groupBox5.Controls.Add(this.RRespawn_numeric);
            this.groupBox5.Controls.Add(this.RAmount_numeric);
            this.groupBox5.Controls.Add(this.label59);
            this.groupBox5.Controls.Add(this.RId_numeric);
            this.groupBox5.Controls.Add(this.label60);
            this.groupBox5.Controls.Add(this.ResourcesGroupRemoveButton);
            this.groupBox5.Controls.Add(this.ResourcesGroupCloneButton);
            this.groupBox5.Controls.Add(this.ResourcesGroupGrid);
            this.groupBox5.Controls.Add(this.label53);
            this.groupBox5.Controls.Add(this.label58);
            this.groupBox5.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.groupBox5.Location = new System.Drawing.Point(364, 253);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(599, 229);
            this.groupBox5.TabIndex = 18;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "Ресурсы";
            // 
            // RType_numeric
            // 
            this.RType_numeric.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.RType_numeric.Location = new System.Drawing.Point(300, 93);
            this.RType_numeric.Maximum = new decimal(new int[] {
            2147483647,
            0,
            0,
            0});
            this.RType_numeric.Minimum = new decimal(new int[] {
            -2147483648,
            0,
            0,
            -2147483648});
            this.RType_numeric.Name = "RType_numeric";
            this.RType_numeric.Size = new System.Drawing.Size(80, 20);
            this.RType_numeric.TabIndex = 53;
            this.RType_numeric.KeyDown += new System.Windows.Forms.KeyEventHandler(this.UnderResources_EnterPress);
            this.RType_numeric.Leave += new System.EventHandler(this.UnderResourcesLeave);
            // 
            // label39
            // 
            this.label39.AutoSize = true;
            this.label39.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label39.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.label39.Location = new System.Drawing.Point(386, 16);
            this.label39.Name = "label39";
            this.label39.Size = new System.Drawing.Size(0, 15);
            this.label39.TabIndex = 52;
            // 
            // RfHeiOff_numeric
            // 
            this.RfHeiOff_numeric.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.RfHeiOff_numeric.DecimalPlaces = 3;
            this.RfHeiOff_numeric.Location = new System.Drawing.Point(300, 119);
            this.RfHeiOff_numeric.Maximum = new decimal(new int[] {
            2147483647,
            0,
            0,
            0});
            this.RfHeiOff_numeric.Minimum = new decimal(new int[] {
            -2147483648,
            0,
            0,
            -2147483648});
            this.RfHeiOff_numeric.Name = "RfHeiOff_numeric";
            this.RfHeiOff_numeric.Size = new System.Drawing.Size(80, 20);
            this.RfHeiOff_numeric.TabIndex = 50;
            this.RfHeiOff_numeric.KeyDown += new System.Windows.Forms.KeyEventHandler(this.UnderResources_EnterPress);
            this.RfHeiOff_numeric.Leave += new System.EventHandler(this.UnderResourcesLeave);
            // 
            // label55
            // 
            this.label55.AutoSize = true;
            this.label55.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label55.Location = new System.Drawing.Point(242, 93);
            this.label55.Name = "label55";
            this.label55.Size = new System.Drawing.Size(61, 15);
            this.label55.TabIndex = 47;
            this.label55.Text = "          Тип:";
            // 
            // RRespawn_numeric
            // 
            this.RRespawn_numeric.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.RRespawn_numeric.Location = new System.Drawing.Point(300, 67);
            this.RRespawn_numeric.Maximum = new decimal(new int[] {
            2147483647,
            0,
            0,
            0});
            this.RRespawn_numeric.Minimum = new decimal(new int[] {
            -2147483648,
            0,
            0,
            -2147483648});
            this.RRespawn_numeric.Name = "RRespawn_numeric";
            this.RRespawn_numeric.Size = new System.Drawing.Size(80, 20);
            this.RRespawn_numeric.TabIndex = 42;
            this.RRespawn_numeric.KeyDown += new System.Windows.Forms.KeyEventHandler(this.UnderResources_EnterPress);
            this.RRespawn_numeric.Leave += new System.EventHandler(this.UnderResourcesLeave);
            // 
            // RAmount_numeric
            // 
            this.RAmount_numeric.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.RAmount_numeric.Location = new System.Drawing.Point(300, 41);
            this.RAmount_numeric.Maximum = new decimal(new int[] {
            2147483647,
            0,
            0,
            0});
            this.RAmount_numeric.Minimum = new decimal(new int[] {
            -2147483648,
            0,
            0,
            -2147483648});
            this.RAmount_numeric.Name = "RAmount_numeric";
            this.RAmount_numeric.Size = new System.Drawing.Size(80, 20);
            this.RAmount_numeric.TabIndex = 40;
            this.RAmount_numeric.KeyDown += new System.Windows.Forms.KeyEventHandler(this.UnderResources_EnterPress);
            this.RAmount_numeric.Leave += new System.EventHandler(this.UnderResourcesLeave);
            // 
            // label59
            // 
            this.label59.AutoSize = true;
            this.label59.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label59.Location = new System.Drawing.Point(224, 41);
            this.label59.Name = "label59";
            this.label59.Size = new System.Drawing.Size(79, 15);
            this.label59.TabIndex = 39;
            this.label59.Text = "Количество:";
            // 
            // RId_numeric
            // 
            this.RId_numeric.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.RId_numeric.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.RId_numeric.Location = new System.Drawing.Point(300, 15);
            this.RId_numeric.Maximum = new decimal(new int[] {
            2147483647,
            0,
            0,
            0});
            this.RId_numeric.Minimum = new decimal(new int[] {
            -2147483648,
            0,
            0,
            -2147483648});
            this.RId_numeric.Name = "RId_numeric";
            this.RId_numeric.Size = new System.Drawing.Size(80, 20);
            this.RId_numeric.TabIndex = 38;
            this.RId_numeric.DoubleClick += new System.EventHandler(this.RId_numeric_DoubleClick);
            this.RId_numeric.KeyDown += new System.Windows.Forms.KeyEventHandler(this.UnderResources_EnterPress);
            this.RId_numeric.Leave += new System.EventHandler(this.UnderResourcesLeave);
            // 
            // label60
            // 
            this.label60.AutoSize = true;
            this.label60.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label60.Location = new System.Drawing.Point(281, 16);
            this.label60.Name = "label60";
            this.label60.Size = new System.Drawing.Size(22, 15);
            this.label60.TabIndex = 37;
            this.label60.Text = "ID:";
            // 
            // ResourcesGroupRemoveButton
            // 
            this.ResourcesGroupRemoveButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.ResourcesGroupRemoveButton.ForeColor = System.Drawing.Color.Black;
            this.ResourcesGroupRemoveButton.Image = ((System.Drawing.Image)(resources.GetObject("ResourcesGroupRemoveButton.Image")));
            this.ResourcesGroupRemoveButton.Location = new System.Drawing.Point(110, 201);
            this.ResourcesGroupRemoveButton.Name = "ResourcesGroupRemoveButton";
            this.ResourcesGroupRemoveButton.Size = new System.Drawing.Size(110, 27);
            this.ResourcesGroupRemoveButton.TabIndex = 16;
            this.ResourcesGroupRemoveButton.Text = "Удалить";
            this.ResourcesGroupRemoveButton.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.ResourcesGroupRemoveButton.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.ResourcesGroupRemoveButton.UseVisualStyleBackColor = true;
            this.ResourcesGroupRemoveButton.Click += new System.EventHandler(this.RemoveResourcesInGroup_Click);
            // 
            // ResourcesGroupCloneButton
            // 
            this.ResourcesGroupCloneButton.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.ResourcesGroupCloneButton.ForeColor = System.Drawing.Color.Black;
            this.ResourcesGroupCloneButton.Image = ((System.Drawing.Image)(resources.GetObject("ResourcesGroupCloneButton.Image")));
            this.ResourcesGroupCloneButton.Location = new System.Drawing.Point(0, 201);
            this.ResourcesGroupCloneButton.Name = "ResourcesGroupCloneButton";
            this.ResourcesGroupCloneButton.Size = new System.Drawing.Size(110, 27);
            this.ResourcesGroupCloneButton.TabIndex = 15;
            this.ResourcesGroupCloneButton.Text = "Клонировать";
            this.ResourcesGroupCloneButton.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.ResourcesGroupCloneButton.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.ResourcesGroupCloneButton.UseVisualStyleBackColor = true;
            this.ResourcesGroupCloneButton.Click += new System.EventHandler(this.CloneResourcesInGroup_Click);
            // 
            // ResourcesGroupGrid
            // 
            this.ResourcesGroupGrid.AllowUserToAddRows = false;
            this.ResourcesGroupGrid.AllowUserToDeleteRows = false;
            this.ResourcesGroupGrid.AllowUserToResizeColumns = false;
            this.ResourcesGroupGrid.AllowUserToResizeRows = false;
            this.ResourcesGroupGrid.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.ResourcesGroupGrid.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(15)))), ((int)(((byte)(15)))));
            this.ResourcesGroupGrid.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None;
            this.ResourcesGroupGrid.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            this.ResourcesGroupGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.ResourcesGroupGrid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewTextBoxColumn9,
            this.dataGridViewTextBoxColumn10,
            this.dataGridViewTextBoxColumn11});
            dataGridViewCellStyle11.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle11.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(15)))), ((int)(((byte)(15)))));
            dataGridViewCellStyle11.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            dataGridViewCellStyle11.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle11.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            dataGridViewCellStyle11.SelectionForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle11.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.ResourcesGroupGrid.DefaultCellStyle = dataGridViewCellStyle11;
            this.ResourcesGroupGrid.EnableHeadersVisualStyles = false;
            this.ResourcesGroupGrid.Location = new System.Drawing.Point(1, 13);
            this.ResourcesGroupGrid.Name = "ResourcesGroupGrid";
            this.ResourcesGroupGrid.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            this.ResourcesGroupGrid.RowHeadersVisible = false;
            this.ResourcesGroupGrid.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.ResourcesGroupGrid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.ResourcesGroupGrid.ShowCellErrors = false;
            this.ResourcesGroupGrid.ShowCellToolTips = false;
            this.ResourcesGroupGrid.ShowEditingIcon = false;
            this.ResourcesGroupGrid.ShowRowErrors = false;
            this.ResourcesGroupGrid.Size = new System.Drawing.Size(218, 189);
            this.ResourcesGroupGrid.TabIndex = 15;
            this.ResourcesGroupGrid.CurrentCellChanged += new System.EventHandler(this.ResourcesGroupGrid_CurrentCellChanged);
            // 
            // dataGridViewTextBoxColumn9
            // 
            dataGridViewCellStyle9.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.dataGridViewTextBoxColumn9.DefaultCellStyle = dataGridViewCellStyle9;
            this.dataGridViewTextBoxColumn9.FillWeight = 6.912442F;
            this.dataGridViewTextBoxColumn9.HeaderText = "#";
            this.dataGridViewTextBoxColumn9.Name = "dataGridViewTextBoxColumn9";
            this.dataGridViewTextBoxColumn9.ReadOnly = true;
            this.dataGridViewTextBoxColumn9.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.dataGridViewTextBoxColumn9.Width = 47;
            // 
            // dataGridViewTextBoxColumn10
            // 
            dataGridViewCellStyle10.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.dataGridViewTextBoxColumn10.DefaultCellStyle = dataGridViewCellStyle10;
            this.dataGridViewTextBoxColumn10.FillWeight = 44.7233F;
            this.dataGridViewTextBoxColumn10.HeaderText = "  ID";
            this.dataGridViewTextBoxColumn10.Name = "dataGridViewTextBoxColumn10";
            this.dataGridViewTextBoxColumn10.ReadOnly = true;
            this.dataGridViewTextBoxColumn10.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.dataGridViewTextBoxColumn10.Width = 50;
            // 
            // dataGridViewTextBoxColumn11
            // 
            this.dataGridViewTextBoxColumn11.HeaderText = "Name";
            this.dataGridViewTextBoxColumn11.Name = "dataGridViewTextBoxColumn11";
            this.dataGridViewTextBoxColumn11.ReadOnly = true;
            this.dataGridViewTextBoxColumn11.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.dataGridViewTextBoxColumn11.Width = 125;
            // 
            // label53
            // 
            this.label53.AutoSize = true;
            this.label53.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label53.Location = new System.Drawing.Point(224, 119);
            this.label53.Name = "label53";
            this.label53.Size = new System.Drawing.Size(79, 15);
            this.label53.TabIndex = 51;
            this.label53.Text = "Над землей:";
            // 
            // label58
            // 
            this.label58.AutoSize = true;
            this.label58.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label58.Location = new System.Drawing.Point(218, 67);
            this.label58.Name = "label58";
            this.label58.Size = new System.Drawing.Size(85, 15);
            this.label58.TabIndex = 41;
            this.label58.Text = "Время респа:";
            // 
            // groupBox3
            // 
            this.groupBox3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(239)))), ((int)(((byte)(244)))), ((int)(((byte)(250)))));
            this.groupBox3.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.groupBox3.Controls.Add(this.groupBox11);
            this.groupBox3.Controls.Add(this.RIMaxNuml);
            this.groupBox3.Controls.Add(this.label36);
            this.groupBox3.Controls.Add(this.RTriggerID);
            this.groupBox3.Controls.Add(this.label37);
            this.groupBox3.Controls.Add(this.RdwGenID);
            this.groupBox3.Controls.Add(this.label38);
            this.groupBox3.Controls.Add(this.RBValidOnce);
            this.groupBox3.Controls.Add(this.ResourcesAutoRevive);
            this.groupBox3.Controls.Add(this.ResourcesInitGen);
            this.groupBox3.Controls.Add(this.RRotation);
            this.groupBox3.Controls.Add(this.label41);
            this.groupBox3.Controls.Add(this.RZ_Random);
            this.groupBox3.Controls.Add(this.label42);
            this.groupBox3.Controls.Add(this.RX_Random);
            this.groupBox3.Controls.Add(this.label43);
            this.groupBox3.Controls.Add(this.RInCline2);
            this.groupBox3.Controls.Add(this.label44);
            this.groupBox3.Controls.Add(this.RInCline1);
            this.groupBox3.Controls.Add(this.label45);
            this.groupBox3.Controls.Add(this.RZ_position);
            this.groupBox3.Controls.Add(this.RY_position);
            this.groupBox3.Controls.Add(this.RX_position);
            this.groupBox3.Controls.Add(this.label47);
            this.groupBox3.Controls.Add(this.label48);
            this.groupBox3.Controls.Add(this.label49);
            this.groupBox3.Controls.Add(this.RGroup_amount_textbox);
            this.groupBox3.Controls.Add(this.label51);
            this.groupBox3.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.groupBox3.Location = new System.Drawing.Point(364, 1);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(599, 250);
            this.groupBox3.TabIndex = 17;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Основное";
            // 
            // groupBox11
            // 
            this.groupBox11.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(239)))), ((int)(((byte)(244)))), ((int)(((byte)(250)))));
            this.groupBox11.Controls.Add(this.ResourcesInsertCordsFromGame);
            this.groupBox11.Controls.Add(this.AddResourceRespawnTime);
            this.groupBox11.Controls.Add(this.label54);
            this.groupBox11.Controls.Add(this.AddResourceAmount);
            this.groupBox11.Controls.Add(this.label56);
            this.groupBox11.Controls.Add(this.AddResourceID);
            this.groupBox11.Controls.Add(this.label57);
            this.groupBox11.Controls.Add(this.AddResourcesTrigger);
            this.groupBox11.Controls.Add(this.label75);
            this.groupBox11.Location = new System.Drawing.Point(342, 7);
            this.groupBox11.Name = "groupBox11";
            this.groupBox11.Size = new System.Drawing.Size(254, 165);
            this.groupBox11.TabIndex = 18;
            this.groupBox11.TabStop = false;
            this.groupBox11.Text = "Настройки для добавления новых ресурсов";
            // 
            // ResourcesInsertCordsFromGame
            // 
            this.ResourcesInsertCordsFromGame.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.ResourcesInsertCordsFromGame.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.ResourcesInsertCordsFromGame.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(64)))), ((int)(((byte)(0)))));
            this.ResourcesInsertCordsFromGame.Location = new System.Drawing.Point(2, 111);
            this.ResourcesInsertCordsFromGame.Name = "ResourcesInsertCordsFromGame";
            this.ResourcesInsertCordsFromGame.Size = new System.Drawing.Size(252, 52);
            this.ResourcesInsertCordsFromGame.TabIndex = 67;
            this.ResourcesInsertCordsFromGame.Text = "Вставить координаты с игры";
            this.ResourcesInsertCordsFromGame.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.ResourcesInsertCordsFromGame.UseVisualStyleBackColor = true;
            this.ResourcesInsertCordsFromGame.Click += new System.EventHandler(this.RInsterCordsFromGame_Click);
            // 
            // AddResourceRespawnTime
            // 
            this.AddResourceRespawnTime.BackColor = System.Drawing.SystemColors.Window;
            this.AddResourceRespawnTime.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.AddResourceRespawnTime.Location = new System.Drawing.Point(157, 88);
            this.AddResourceRespawnTime.Maximum = new decimal(new int[] {
            2147483647,
            0,
            0,
            0});
            this.AddResourceRespawnTime.Minimum = new decimal(new int[] {
            -2147483648,
            0,
            0,
            -2147483648});
            this.AddResourceRespawnTime.Name = "AddResourceRespawnTime";
            this.AddResourceRespawnTime.Size = new System.Drawing.Size(76, 20);
            this.AddResourceRespawnTime.TabIndex = 49;
            this.AddResourceRespawnTime.Value = new decimal(new int[] {
            60,
            0,
            0,
            0});
            // 
            // label54
            // 
            this.label54.AutoSize = true;
            this.label54.Location = new System.Drawing.Point(57, 88);
            this.label54.Name = "label54";
            this.label54.Size = new System.Drawing.Size(94, 13);
            this.label54.TabIndex = 48;
            this.label54.Text = "Время респавна:";
            // 
            // AddResourceAmount
            // 
            this.AddResourceAmount.BackColor = System.Drawing.SystemColors.Window;
            this.AddResourceAmount.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.AddResourceAmount.Location = new System.Drawing.Point(157, 65);
            this.AddResourceAmount.Maximum = new decimal(new int[] {
            2147483647,
            0,
            0,
            0});
            this.AddResourceAmount.Minimum = new decimal(new int[] {
            -2147483648,
            0,
            0,
            -2147483648});
            this.AddResourceAmount.Name = "AddResourceAmount";
            this.AddResourceAmount.Size = new System.Drawing.Size(76, 20);
            this.AddResourceAmount.TabIndex = 47;
            this.AddResourceAmount.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // label56
            // 
            this.label56.AutoSize = true;
            this.label56.Location = new System.Drawing.Point(33, 65);
            this.label56.Name = "label56";
            this.label56.Size = new System.Drawing.Size(119, 13);
            this.label56.TabIndex = 46;
            this.label56.Text = "Количество ресурсов:";
            // 
            // AddResourceID
            // 
            this.AddResourceID.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.AddResourceID.Location = new System.Drawing.Point(157, 41);
            this.AddResourceID.Maximum = new decimal(new int[] {
            2147483647,
            0,
            0,
            0});
            this.AddResourceID.Minimum = new decimal(new int[] {
            -2147483648,
            0,
            0,
            -2147483648});
            this.AddResourceID.Name = "AddResourceID";
            this.AddResourceID.Size = new System.Drawing.Size(76, 20);
            this.AddResourceID.TabIndex = 45;
            this.AddResourceID.Value = new decimal(new int[] {
            16,
            0,
            0,
            0});
            // 
            // label57
            // 
            this.label57.AutoSize = true;
            this.label57.Location = new System.Drawing.Point(10, 42);
            this.label57.Name = "label57";
            this.label57.Size = new System.Drawing.Size(143, 13);
            this.label57.TabIndex = 44;
            this.label57.Text = "ID создаваемого ресурсы:\r\n";
            // 
            // AddResourcesTrigger
            // 
            this.AddResourcesTrigger.BackColor = System.Drawing.SystemColors.Window;
            this.AddResourcesTrigger.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.AddResourcesTrigger.Location = new System.Drawing.Point(157, 17);
            this.AddResourcesTrigger.Maximum = new decimal(new int[] {
            2147483647,
            0,
            0,
            0});
            this.AddResourcesTrigger.Minimum = new decimal(new int[] {
            -2147483648,
            0,
            0,
            -2147483648});
            this.AddResourcesTrigger.Name = "AddResourcesTrigger";
            this.AddResourcesTrigger.Size = new System.Drawing.Size(76, 20);
            this.AddResourcesTrigger.TabIndex = 39;
            this.AddResourcesTrigger.Value = new decimal(new int[] {
            3074,
            0,
            0,
            0});
            // 
            // label75
            // 
            this.label75.AutoSize = true;
            this.label75.Location = new System.Drawing.Point(85, 18);
            this.label75.Name = "label75";
            this.label75.Size = new System.Drawing.Size(69, 13);
            this.label75.TabIndex = 0;
            this.label75.Text = "ID триггера:";
            // 
            // RIMaxNuml
            // 
            this.RIMaxNuml.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.RIMaxNuml.Location = new System.Drawing.Point(248, 82);
            this.RIMaxNuml.Name = "RIMaxNuml";
            this.RIMaxNuml.Size = new System.Drawing.Size(91, 20);
            this.RIMaxNuml.TabIndex = 35;
            this.RIMaxNuml.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ResourcesDefault_EnterPress);
            this.RIMaxNuml.Leave += new System.EventHandler(this.ResourcesDefaultLeave);
            // 
            // label36
            // 
            this.label36.AutoSize = true;
            this.label36.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label36.Location = new System.Drawing.Point(182, 84);
            this.label36.Name = "label36";
            this.label36.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.label36.Size = new System.Drawing.Size(67, 15);
            this.label36.TabIndex = 36;
            this.label36.Text = "IMaxNuml:";
            // 
            // RTriggerID
            // 
            this.RTriggerID.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.RTriggerID.ContextMenuStrip = this.contextMenuStrip2;
            this.RTriggerID.Location = new System.Drawing.Point(248, 58);
            this.RTriggerID.Name = "RTriggerID";
            this.RTriggerID.Size = new System.Drawing.Size(91, 20);
            this.RTriggerID.TabIndex = 31;
            this.RTriggerID.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ResourcesDefault_EnterPress);
            this.RTriggerID.Leave += new System.EventHandler(this.ResourcesDefaultLeave);
            // 
            // label37
            // 
            this.label37.AutoSize = true;
            this.label37.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label37.Location = new System.Drawing.Point(192, 60);
            this.label37.Name = "label37";
            this.label37.Size = new System.Drawing.Size(55, 15);
            this.label37.TabIndex = 32;
            this.label37.Text = "Триггер:";
            // 
            // RdwGenID
            // 
            this.RdwGenID.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.RdwGenID.Location = new System.Drawing.Point(248, 36);
            this.RdwGenID.Name = "RdwGenID";
            this.RdwGenID.Size = new System.Drawing.Size(91, 20);
            this.RdwGenID.TabIndex = 29;
            this.RdwGenID.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ResourcesDefault_EnterPress);
            this.RdwGenID.Leave += new System.EventHandler(this.ResourcesDefaultLeave);
            // 
            // label38
            // 
            this.label38.AutoSize = true;
            this.label38.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label38.Location = new System.Drawing.Point(190, 38);
            this.label38.Name = "label38";
            this.label38.Size = new System.Drawing.Size(59, 15);
            this.label38.TabIndex = 30;
            this.label38.Text = "dwGenId:";
            // 
            // RBValidOnce
            // 
            this.RBValidOnce.AutoSize = true;
            this.RBValidOnce.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.RBValidOnce.Location = new System.Drawing.Point(264, 152);
            this.RBValidOnce.Name = "RBValidOnce";
            this.RBValidOnce.Size = new System.Drawing.Size(75, 17);
            this.RBValidOnce.TabIndex = 28;
            this.RBValidOnce.Text = "ValicOnce";
            this.RBValidOnce.TextImageRelation = System.Windows.Forms.TextImageRelation.TextBeforeImage;
            this.RBValidOnce.UseVisualStyleBackColor = true;
            this.RBValidOnce.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ResourcesDefault_EnterPress);
            this.RBValidOnce.Leave += new System.EventHandler(this.ResourcesDefaultLeave);
            // 
            // ResourcesAutoRevive
            // 
            this.ResourcesAutoRevive.AutoSize = true;
            this.ResourcesAutoRevive.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.ResourcesAutoRevive.Location = new System.Drawing.Point(204, 129);
            this.ResourcesAutoRevive.Name = "ResourcesAutoRevive";
            this.ResourcesAutoRevive.Size = new System.Drawing.Size(135, 17);
            this.ResourcesAutoRevive.TabIndex = 27;
            this.ResourcesAutoRevive.Text = "Мгновенный респавн";
            this.ResourcesAutoRevive.TextImageRelation = System.Windows.Forms.TextImageRelation.TextBeforeImage;
            this.ResourcesAutoRevive.UseVisualStyleBackColor = true;
            this.ResourcesAutoRevive.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ResourcesDefault_EnterPress);
            this.ResourcesAutoRevive.Leave += new System.EventHandler(this.ResourcesDefaultLeave);
            // 
            // ResourcesInitGen
            // 
            this.ResourcesInitGen.AutoSize = true;
            this.ResourcesInitGen.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.ResourcesInitGen.Location = new System.Drawing.Point(187, 107);
            this.ResourcesInitGen.Name = "ResourcesInitGen";
            this.ResourcesInitGen.Size = new System.Drawing.Size(152, 17);
            this.ResourcesInitGen.TabIndex = 26;
            this.ResourcesInitGen.Text = "Активировать генератор";
            this.ResourcesInitGen.TextImageRelation = System.Windows.Forms.TextImageRelation.TextBeforeImage;
            this.ResourcesInitGen.UseVisualStyleBackColor = true;
            this.ResourcesInitGen.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ResourcesDefault_EnterPress);
            this.ResourcesInitGen.Leave += new System.EventHandler(this.ResourcesDefaultLeave);
            // 
            // RRotation
            // 
            this.RRotation.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.RRotation.Location = new System.Drawing.Point(77, 172);
            this.RRotation.Name = "RRotation";
            this.RRotation.Size = new System.Drawing.Size(91, 20);
            this.RRotation.TabIndex = 21;
            this.RRotation.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ResourcesDefault_EnterPress);
            this.RRotation.Leave += new System.EventHandler(this.ResourcesDefaultLeave);
            // 
            // label41
            // 
            this.label41.AutoSize = true;
            this.label41.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label41.Location = new System.Drawing.Point(16, 174);
            this.label41.Name = "label41";
            this.label41.Size = new System.Drawing.Size(61, 15);
            this.label41.TabIndex = 20;
            this.label41.Text = "Поворот:";
            // 
            // RZ_Random
            // 
            this.RZ_Random.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.RZ_Random.Location = new System.Drawing.Point(77, 103);
            this.RZ_Random.Name = "RZ_Random";
            this.RZ_Random.Size = new System.Drawing.Size(91, 20);
            this.RZ_Random.TabIndex = 19;
            this.RZ_Random.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ResourcesDefault_EnterPress);
            this.RZ_Random.Leave += new System.EventHandler(this.ResourcesDefaultLeave);
            // 
            // label42
            // 
            this.label42.AutoSize = true;
            this.label42.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label42.Location = new System.Drawing.Point(9, 105);
            this.label42.Name = "label42";
            this.label42.Size = new System.Drawing.Size(68, 15);
            this.label42.TabIndex = 18;
            this.label42.Text = "Разброс Z:";
            // 
            // RX_Random
            // 
            this.RX_Random.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.RX_Random.Location = new System.Drawing.Point(77, 80);
            this.RX_Random.Name = "RX_Random";
            this.RX_Random.Size = new System.Drawing.Size(91, 20);
            this.RX_Random.TabIndex = 17;
            this.RX_Random.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ResourcesDefault_EnterPress);
            this.RX_Random.Leave += new System.EventHandler(this.ResourcesDefaultLeave);
            // 
            // label43
            // 
            this.label43.AutoSize = true;
            this.label43.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label43.Location = new System.Drawing.Point(8, 82);
            this.label43.Name = "label43";
            this.label43.Size = new System.Drawing.Size(69, 15);
            this.label43.TabIndex = 16;
            this.label43.Text = "Разброс X:";
            // 
            // RInCline2
            // 
            this.RInCline2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.RInCline2.Location = new System.Drawing.Point(77, 149);
            this.RInCline2.Name = "RInCline2";
            this.RInCline2.Size = new System.Drawing.Size(91, 20);
            this.RInCline2.TabIndex = 15;
            this.RInCline2.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ResourcesDefault_EnterPress);
            this.RInCline2.Leave += new System.EventHandler(this.ResourcesDefaultLeave);
            // 
            // label44
            // 
            this.label44.AutoSize = true;
            this.label44.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label44.Location = new System.Drawing.Point(14, 151);
            this.label44.Name = "label44";
            this.label44.Size = new System.Drawing.Size(63, 15);
            this.label44.TabIndex = 14;
            this.label44.Text = "Наклон 2:";
            // 
            // RInCline1
            // 
            this.RInCline1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.RInCline1.Location = new System.Drawing.Point(77, 126);
            this.RInCline1.Name = "RInCline1";
            this.RInCline1.Size = new System.Drawing.Size(91, 20);
            this.RInCline1.TabIndex = 13;
            this.RInCline1.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ResourcesDefault_EnterPress);
            this.RInCline1.Leave += new System.EventHandler(this.ResourcesDefaultLeave);
            // 
            // label45
            // 
            this.label45.AutoSize = true;
            this.label45.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label45.Location = new System.Drawing.Point(14, 128);
            this.label45.Name = "label45";
            this.label45.Size = new System.Drawing.Size(63, 15);
            this.label45.TabIndex = 12;
            this.label45.Text = "Наклон 1:";
            // 
            // RZ_position
            // 
            this.RZ_position.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.RZ_position.Location = new System.Drawing.Point(77, 57);
            this.RZ_position.Name = "RZ_position";
            this.RZ_position.Size = new System.Drawing.Size(91, 20);
            this.RZ_position.TabIndex = 10;
            this.RZ_position.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ResourcesDefault_EnterPress);
            this.RZ_position.Leave += new System.EventHandler(this.ResourcesDefaultLeave);
            // 
            // RY_position
            // 
            this.RY_position.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.RY_position.Location = new System.Drawing.Point(77, 34);
            this.RY_position.Name = "RY_position";
            this.RY_position.Size = new System.Drawing.Size(91, 20);
            this.RY_position.TabIndex = 9;
            this.RY_position.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ResourcesDefault_EnterPress);
            this.RY_position.Leave += new System.EventHandler(this.ResourcesDefaultLeave);
            // 
            // RX_position
            // 
            this.RX_position.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.RX_position.Location = new System.Drawing.Point(77, 11);
            this.RX_position.Name = "RX_position";
            this.RX_position.Size = new System.Drawing.Size(91, 20);
            this.RX_position.TabIndex = 7;
            this.RX_position.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ResourcesDefault_EnterPress);
            this.RX_position.Leave += new System.EventHandler(this.ResourcesDefaultLeave);
            // 
            // label47
            // 
            this.label47.AutoSize = true;
            this.label47.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label47.Location = new System.Drawing.Point(60, 58);
            this.label47.Name = "label47";
            this.label47.Size = new System.Drawing.Size(17, 15);
            this.label47.TabIndex = 6;
            this.label47.Text = "Z:";
            // 
            // label48
            // 
            this.label48.AutoSize = true;
            this.label48.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label48.Location = new System.Drawing.Point(60, 36);
            this.label48.Name = "label48";
            this.label48.Size = new System.Drawing.Size(17, 15);
            this.label48.TabIndex = 5;
            this.label48.Text = "Y:";
            // 
            // label49
            // 
            this.label49.AutoSize = true;
            this.label49.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label49.Location = new System.Drawing.Point(60, 13);
            this.label49.Name = "label49";
            this.label49.Size = new System.Drawing.Size(18, 15);
            this.label49.TabIndex = 4;
            this.label49.Text = "X:";
            // 
            // RGroup_amount_textbox
            // 
            this.RGroup_amount_textbox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.RGroup_amount_textbox.Enabled = false;
            this.RGroup_amount_textbox.Location = new System.Drawing.Point(248, 13);
            this.RGroup_amount_textbox.Name = "RGroup_amount_textbox";
            this.RGroup_amount_textbox.Size = new System.Drawing.Size(91, 20);
            this.RGroup_amount_textbox.TabIndex = 2;
            this.RGroup_amount_textbox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label51
            // 
            this.label51.AutoSize = true;
            this.label51.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label51.Location = new System.Drawing.Point(189, 15);
            this.label51.Name = "label51";
            this.label51.Size = new System.Drawing.Size(59, 15);
            this.label51.TabIndex = 3;
            this.label51.Text = "В группе:";
            // 
            // ResourcesGrid
            // 
            this.ResourcesGrid.AllowUserToAddRows = false;
            this.ResourcesGrid.AllowUserToDeleteRows = false;
            this.ResourcesGrid.AllowUserToResizeColumns = false;
            this.ResourcesGrid.AllowUserToResizeRows = false;
            this.ResourcesGrid.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.ResourcesGrid.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(15)))), ((int)(((byte)(15)))));
            this.ResourcesGrid.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None;
            this.ResourcesGrid.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle12.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle12.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle12.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            dataGridViewCellStyle12.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle12.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle12.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle12.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.ResourcesGrid.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle12;
            this.ResourcesGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.ResourcesGrid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewTextBoxColumn6,
            this.dataGridViewTextBoxColumn7,
            this.dataGridViewTextBoxColumn8});
            this.ResourcesGrid.ContextMenuStrip = this.ExistenceContext;
            dataGridViewCellStyle16.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle16.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(15)))), ((int)(((byte)(15)))));
            dataGridViewCellStyle16.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            dataGridViewCellStyle16.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            dataGridViewCellStyle16.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle16.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle16.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.ResourcesGrid.DefaultCellStyle = dataGridViewCellStyle16;
            this.ResourcesGrid.EnableHeadersVisualStyles = false;
            this.ResourcesGrid.Location = new System.Drawing.Point(1, 29);
            this.ResourcesGrid.Name = "ResourcesGrid";
            this.ResourcesGrid.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            this.ResourcesGrid.RowHeadersVisible = false;
            this.ResourcesGrid.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.ResourcesGrid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.ResourcesGrid.ShowCellErrors = false;
            this.ResourcesGrid.ShowCellToolTips = false;
            this.ResourcesGrid.ShowEditingIcon = false;
            this.ResourcesGrid.ShowRowErrors = false;
            this.ResourcesGrid.Size = new System.Drawing.Size(359, 425);
            this.ResourcesGrid.TabIndex = 14;
            this.ResourcesGrid.CellMouseEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.ResourcesGrid_CellMouseEnter);
            this.ResourcesGrid.CurrentCellChanged += new System.EventHandler(this.ResourcesGrid_CurrentCellChanged);
            this.ResourcesGrid.KeyDown += new System.Windows.Forms.KeyEventHandler(this.GridsKeyDown);
            // 
            // dataGridViewTextBoxColumn6
            // 
            dataGridViewCellStyle13.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.dataGridViewTextBoxColumn6.DefaultCellStyle = dataGridViewCellStyle13;
            this.dataGridViewTextBoxColumn6.FillWeight = 6.912442F;
            this.dataGridViewTextBoxColumn6.HeaderText = "#";
            this.dataGridViewTextBoxColumn6.Name = "dataGridViewTextBoxColumn6";
            this.dataGridViewTextBoxColumn6.ReadOnly = true;
            this.dataGridViewTextBoxColumn6.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.dataGridViewTextBoxColumn6.Width = 45;
            // 
            // dataGridViewTextBoxColumn7
            // 
            dataGridViewCellStyle14.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            dataGridViewCellStyle14.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(77)))), ((int)(((byte)(255)))), ((int)(((byte)(143)))));
            this.dataGridViewTextBoxColumn7.DefaultCellStyle = dataGridViewCellStyle14;
            this.dataGridViewTextBoxColumn7.FillWeight = 44.7233F;
            this.dataGridViewTextBoxColumn7.HeaderText = "  ID";
            this.dataGridViewTextBoxColumn7.Name = "dataGridViewTextBoxColumn7";
            this.dataGridViewTextBoxColumn7.ReadOnly = true;
            this.dataGridViewTextBoxColumn7.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.dataGridViewTextBoxColumn7.Width = 95;
            // 
            // dataGridViewTextBoxColumn8
            // 
            dataGridViewCellStyle15.ForeColor = System.Drawing.Color.White;
            this.dataGridViewTextBoxColumn8.DefaultCellStyle = dataGridViewCellStyle15;
            this.dataGridViewTextBoxColumn8.HeaderText = "Name";
            this.dataGridViewTextBoxColumn8.Name = "dataGridViewTextBoxColumn8";
            this.dataGridViewTextBoxColumn8.ReadOnly = true;
            this.dataGridViewTextBoxColumn8.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.dataGridViewTextBoxColumn8.Width = 217;
            // 
            // toolStrip1
            // 
            this.toolStrip1.AutoSize = false;
            this.toolStrip1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(239)))), ((int)(((byte)(244)))), ((int)(((byte)(250)))));
            this.toolStrip1.GripMargin = new System.Windows.Forms.Padding(0);
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ExportResources,
            this.ImportResources,
            this.LineUpResource,
            this.MoveResources});
            this.toolStrip1.Location = new System.Drawing.Point(3, 3);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.toolStrip1.Size = new System.Drawing.Size(960, 25);
            this.toolStrip1.TabIndex = 19;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // ExportResources
            // 
            this.ExportResources.Image = ((System.Drawing.Image)(resources.GetObject("ExportResources.Image")));
            this.ExportResources.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.ExportResources.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.ExportResources.Name = "ExportResources";
            this.ExportResources.Size = new System.Drawing.Size(74, 22);
            this.ExportResources.Text = "Экспорт";
            this.ExportResources.ToolTipText = "Export";
            this.ExportResources.Click += new System.EventHandler(this.ExportExistence_Click);
            // 
            // ImportResources
            // 
            this.ImportResources.Image = ((System.Drawing.Image)(resources.GetObject("ImportResources.Image")));
            this.ImportResources.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.ImportResources.Name = "ImportResources";
            this.ImportResources.Size = new System.Drawing.Size(71, 22);
            this.ImportResources.Text = "Импорт";
            this.ImportResources.ToolTipText = "Import";
            this.ImportResources.Click += new System.EventHandler(this.ImportExistence_Click);
            // 
            // LineUpResource
            // 
            this.LineUpResource.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ResourcesOnX,
            this.ResourcesOnZ});
            this.LineUpResource.Image = ((System.Drawing.Image)(resources.GetObject("LineUpResource.Image")));
            this.LineUpResource.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.LineUpResource.Name = "LineUpResource";
            this.LineUpResource.Size = new System.Drawing.Size(95, 22);
            this.LineUpResource.Text = "Выстроить";
            this.LineUpResource.ToolTipText = "Line up";
            // 
            // ResourcesOnX
            // 
            this.ResourcesOnX.Image = ((System.Drawing.Image)(resources.GetObject("ResourcesOnX.Image")));
            this.ResourcesOnX.Name = "ResourcesOnX";
            this.ResourcesOnX.Size = new System.Drawing.Size(100, 22);
            this.ResourcesOnX.Text = "По X";
            this.ResourcesOnX.Click += new System.EventHandler(this.LineUpX_Click);
            // 
            // ResourcesOnZ
            // 
            this.ResourcesOnZ.Image = ((System.Drawing.Image)(resources.GetObject("ResourcesOnZ.Image")));
            this.ResourcesOnZ.Name = "ResourcesOnZ";
            this.ResourcesOnZ.Size = new System.Drawing.Size(100, 22);
            this.ResourcesOnZ.Text = "По Z";
            this.ResourcesOnZ.Click += new System.EventHandler(this.LineUpZ_Click);
            // 
            // MoveResources
            // 
            this.MoveResources.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ResourceUp,
            this.ResourceDown});
            this.MoveResources.Image = ((System.Drawing.Image)(resources.GetObject("MoveResources.Image")));
            this.MoveResources.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.MoveResources.Name = "MoveResources";
            this.MoveResources.Size = new System.Drawing.Size(108, 22);
            this.MoveResources.Text = "Переместить";
            this.MoveResources.ToolTipText = "Move";
            // 
            // ResourceUp
            // 
            this.ResourceUp.Image = ((System.Drawing.Image)(resources.GetObject("ResourceUp.Image")));
            this.ResourceUp.Name = "ResourceUp";
            this.ResourceUp.Size = new System.Drawing.Size(159, 22);
            this.ResourceUp.Text = "Выше   Shift+W";
            this.ResourceUp.Click += new System.EventHandler(this.UpObjects);
            // 
            // ResourceDown
            // 
            this.ResourceDown.Image = ((System.Drawing.Image)(resources.GetObject("ResourceDown.Image")));
            this.ResourceDown.Name = "ResourceDown";
            this.ResourceDown.Size = new System.Drawing.Size(159, 22);
            this.ResourceDown.Text = "Ниже    Shift+S";
            this.ResourceDown.Click += new System.EventHandler(this.DownObjects);
            // 
            // ResourcesRemoveButton
            // 
            this.ResourcesRemoveButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.ResourcesRemoveButton.ForeColor = System.Drawing.Color.Black;
            this.ResourcesRemoveButton.Image = ((System.Drawing.Image)(resources.GetObject("ResourcesRemoveButton.Image")));
            this.ResourcesRemoveButton.Location = new System.Drawing.Point(180, 454);
            this.ResourcesRemoveButton.Name = "ResourcesRemoveButton";
            this.ResourcesRemoveButton.Size = new System.Drawing.Size(181, 29);
            this.ResourcesRemoveButton.TabIndex = 16;
            this.ResourcesRemoveButton.Text = "Удалить";
            this.ResourcesRemoveButton.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.ResourcesRemoveButton.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.ResourcesRemoveButton.UseVisualStyleBackColor = true;
            this.ResourcesRemoveButton.Click += new System.EventHandler(this.RemoveResourceFull_Click);
            // 
            // ResourcesCloneButton
            // 
            this.ResourcesCloneButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.ResourcesCloneButton.ForeColor = System.Drawing.Color.Black;
            this.ResourcesCloneButton.Image = ((System.Drawing.Image)(resources.GetObject("ResourcesCloneButton.Image")));
            this.ResourcesCloneButton.Location = new System.Drawing.Point(0, 454);
            this.ResourcesCloneButton.Name = "ResourcesCloneButton";
            this.ResourcesCloneButton.Size = new System.Drawing.Size(181, 29);
            this.ResourcesCloneButton.TabIndex = 15;
            this.ResourcesCloneButton.Text = "Клонировать";
            this.ResourcesCloneButton.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.ResourcesCloneButton.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.ResourcesCloneButton.UseVisualStyleBackColor = true;
            this.ResourcesCloneButton.Click += new System.EventHandler(this.CloneResurcesFull_Click);
            // 
            // DynObjectsTab
            // 
            this.DynObjectsTab.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(239)))), ((int)(((byte)(244)))), ((int)(((byte)(250)))));
            this.DynObjectsTab.Controls.Add(this.groupBox9);
            this.DynObjectsTab.Controls.Add(this.groupBox7);
            this.DynObjectsTab.Controls.Add(this.DynamicGrid);
            this.DynObjectsTab.Controls.Add(this.DynObjectsRemoveButton);
            this.DynObjectsTab.Controls.Add(this.DynObjectsCloneButton);
            this.DynObjectsTab.Controls.Add(this.toolStrip2);
            this.DynObjectsTab.Location = new System.Drawing.Point(4, 22);
            this.DynObjectsTab.Name = "DynObjectsTab";
            this.DynObjectsTab.Padding = new System.Windows.Forms.Padding(3);
            this.DynObjectsTab.Size = new System.Drawing.Size(966, 483);
            this.DynObjectsTab.TabIndex = 2;
            this.DynObjectsTab.Text = "Динамические Объекты";
            // 
            // groupBox9
            // 
            this.groupBox9.Controls.Add(this.DynamicPictureBox);
            this.groupBox9.Location = new System.Drawing.Point(366, 131);
            this.groupBox9.Name = "groupBox9";
            this.groupBox9.Size = new System.Drawing.Size(597, 350);
            this.groupBox9.TabIndex = 21;
            this.groupBox9.TabStop = false;
            this.groupBox9.Text = "Изображение";
            // 
            // DynamicPictureBox
            // 
            this.DynamicPictureBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(239)))), ((int)(((byte)(244)))), ((int)(((byte)(250)))));
            this.DynamicPictureBox.Location = new System.Drawing.Point(2, 15);
            this.DynamicPictureBox.Name = "DynamicPictureBox";
            this.DynamicPictureBox.Size = new System.Drawing.Size(594, 335);
            this.DynamicPictureBox.TabIndex = 0;
            this.DynamicPictureBox.TabStop = false;
            // 
            // groupBox7
            // 
            this.groupBox7.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.groupBox7.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(239)))), ((int)(((byte)(244)))), ((int)(((byte)(250)))));
            this.groupBox7.Controls.Add(this.DId_numeric);
            this.groupBox7.Controls.Add(this.Label_DynamicName);
            this.groupBox7.Controls.Add(this.DynObjectsInsertCordsFromGame);
            this.groupBox7.Controls.Add(this.DScale);
            this.groupBox7.Controls.Add(this.groupBox10);
            this.groupBox7.Controls.Add(this.label74);
            this.groupBox7.Controls.Add(this.DTrigger_id);
            this.groupBox7.Controls.Add(this.label73);
            this.groupBox7.Controls.Add(this.DRotation);
            this.groupBox7.Controls.Add(this.label70);
            this.groupBox7.Controls.Add(this.DIncline2);
            this.groupBox7.Controls.Add(this.label71);
            this.groupBox7.Controls.Add(this.DIncline1);
            this.groupBox7.Controls.Add(this.label72);
            this.groupBox7.Controls.Add(this.label69);
            this.groupBox7.Controls.Add(this.DZ_position);
            this.groupBox7.Controls.Add(this.DY_position);
            this.groupBox7.Controls.Add(this.DX_position);
            this.groupBox7.Controls.Add(this.label80);
            this.groupBox7.Controls.Add(this.label81);
            this.groupBox7.Controls.Add(this.label82);
            this.groupBox7.Location = new System.Drawing.Point(364, 1);
            this.groupBox7.Name = "groupBox7";
            this.groupBox7.Size = new System.Drawing.Size(599, 130);
            this.groupBox7.TabIndex = 20;
            this.groupBox7.TabStop = false;
            this.groupBox7.Text = "Основное";
            // 
            // DId_numeric
            // 
            this.DId_numeric.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.DId_numeric.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.DId_numeric.Location = new System.Drawing.Point(67, 13);
            this.DId_numeric.Name = "DId_numeric";
            this.DId_numeric.Size = new System.Drawing.Size(91, 20);
            this.DId_numeric.TabIndex = 69;
            this.DId_numeric.TextChanged += new System.EventHandler(this.IdFind);
            this.DId_numeric.DoubleClick += new System.EventHandler(this.DId_numeric_DoubleClick);
            this.DId_numeric.KeyDown += new System.Windows.Forms.KeyEventHandler(this.DynamicsKeyDown);
            this.DId_numeric.Leave += new System.EventHandler(this.DynamicsLeave);
            // 
            // Label_DynamicName
            // 
            this.Label_DynamicName.AutoSize = true;
            this.Label_DynamicName.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.Label_DynamicName.ForeColor = System.Drawing.Color.Red;
            this.Label_DynamicName.Location = new System.Drawing.Point(157, 15);
            this.Label_DynamicName.Name = "Label_DynamicName";
            this.Label_DynamicName.Size = new System.Drawing.Size(0, 16);
            this.Label_DynamicName.TabIndex = 68;
            // 
            // DynObjectsInsertCordsFromGame
            // 
            this.DynObjectsInsertCordsFromGame.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.DynObjectsInsertCordsFromGame.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.DynObjectsInsertCordsFromGame.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(64)))), ((int)(((byte)(0)))));
            this.DynObjectsInsertCordsFromGame.Location = new System.Drawing.Point(341, 74);
            this.DynObjectsInsertCordsFromGame.Name = "DynObjectsInsertCordsFromGame";
            this.DynObjectsInsertCordsFromGame.Size = new System.Drawing.Size(255, 52);
            this.DynObjectsInsertCordsFromGame.TabIndex = 67;
            this.DynObjectsInsertCordsFromGame.Text = "Вставить координаты с игры";
            this.DynObjectsInsertCordsFromGame.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.DynObjectsInsertCordsFromGame.UseVisualStyleBackColor = true;
            this.DynObjectsInsertCordsFromGame.Click += new System.EventHandler(this.DInsterCordsFromGame_Click);
            // 
            // DScale
            // 
            this.DScale.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.DScale.Location = new System.Drawing.Point(248, 105);
            this.DScale.Name = "DScale";
            this.DScale.Size = new System.Drawing.Size(91, 20);
            this.DScale.TabIndex = 50;
            this.DScale.KeyDown += new System.Windows.Forms.KeyEventHandler(this.DynamicsKeyDown);
            this.DScale.Leave += new System.EventHandler(this.DynamicsLeave);
            // 
            // groupBox10
            // 
            this.groupBox10.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.groupBox10.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(239)))), ((int)(((byte)(244)))), ((int)(((byte)(250)))));
            this.groupBox10.Controls.Add(this.AddDynamicsTrigger);
            this.groupBox10.Controls.Add(this.label40);
            this.groupBox10.Controls.Add(this.AddDynamicsID);
            this.groupBox10.Controls.Add(this.label61);
            this.groupBox10.Location = new System.Drawing.Point(342, 10);
            this.groupBox10.Name = "groupBox10";
            this.groupBox10.Size = new System.Drawing.Size(254, 63);
            this.groupBox10.TabIndex = 18;
            this.groupBox10.TabStop = false;
            this.groupBox10.Text = "Настройки для добавления Дин.Объектов";
            // 
            // AddDynamicsTrigger
            // 
            this.AddDynamicsTrigger.BackColor = System.Drawing.SystemColors.Window;
            this.AddDynamicsTrigger.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.AddDynamicsTrigger.Location = new System.Drawing.Point(157, 40);
            this.AddDynamicsTrigger.Maximum = new decimal(new int[] {
            2147483647,
            0,
            0,
            0});
            this.AddDynamicsTrigger.Minimum = new decimal(new int[] {
            -2147483648,
            0,
            0,
            -2147483648});
            this.AddDynamicsTrigger.Name = "AddDynamicsTrigger";
            this.AddDynamicsTrigger.Size = new System.Drawing.Size(76, 20);
            this.AddDynamicsTrigger.TabIndex = 41;
            // 
            // label40
            // 
            this.label40.AutoSize = true;
            this.label40.Location = new System.Drawing.Point(84, 43);
            this.label40.Name = "label40";
            this.label40.Size = new System.Drawing.Size(69, 13);
            this.label40.TabIndex = 40;
            this.label40.Text = "ID триггера:";
            // 
            // AddDynamicsID
            // 
            this.AddDynamicsID.BackColor = System.Drawing.SystemColors.Window;
            this.AddDynamicsID.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.AddDynamicsID.Location = new System.Drawing.Point(157, 17);
            this.AddDynamicsID.Maximum = new decimal(new int[] {
            2147483647,
            0,
            0,
            0});
            this.AddDynamicsID.Minimum = new decimal(new int[] {
            -2147483648,
            0,
            0,
            -2147483648});
            this.AddDynamicsID.Name = "AddDynamicsID";
            this.AddDynamicsID.Size = new System.Drawing.Size(76, 20);
            this.AddDynamicsID.TabIndex = 39;
            // 
            // label61
            // 
            this.label61.AutoSize = true;
            this.label61.Location = new System.Drawing.Point(61, 18);
            this.label61.Name = "label61";
            this.label61.Size = new System.Drawing.Size(92, 13);
            this.label61.TabIndex = 0;
            this.label61.Text = "ID Дин.Объекта:";
            // 
            // label74
            // 
            this.label74.AutoSize = true;
            this.label74.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label74.Location = new System.Drawing.Point(167, 107);
            this.label74.Name = "label74";
            this.label74.Size = new System.Drawing.Size(80, 15);
            this.label74.TabIndex = 49;
            this.label74.Text = "Увеличение:";
            // 
            // DTrigger_id
            // 
            this.DTrigger_id.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.DTrigger_id.ContextMenuStrip = this.contextMenuStrip2;
            this.DTrigger_id.Location = new System.Drawing.Point(248, 82);
            this.DTrigger_id.Name = "DTrigger_id";
            this.DTrigger_id.Size = new System.Drawing.Size(91, 20);
            this.DTrigger_id.TabIndex = 48;
            this.DTrigger_id.KeyDown += new System.Windows.Forms.KeyEventHandler(this.DynamicsKeyDown);
            this.DTrigger_id.Leave += new System.EventHandler(this.DynamicsLeave);
            // 
            // label73
            // 
            this.label73.AutoSize = true;
            this.label73.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label73.Location = new System.Drawing.Point(192, 83);
            this.label73.Name = "label73";
            this.label73.Size = new System.Drawing.Size(55, 15);
            this.label73.TabIndex = 47;
            this.label73.Text = "Триггер:";
            // 
            // DRotation
            // 
            this.DRotation.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.DRotation.Location = new System.Drawing.Point(248, 59);
            this.DRotation.Name = "DRotation";
            this.DRotation.Size = new System.Drawing.Size(91, 20);
            this.DRotation.TabIndex = 46;
            this.DRotation.KeyDown += new System.Windows.Forms.KeyEventHandler(this.DynamicsKeyDown);
            this.DRotation.Leave += new System.EventHandler(this.DynamicsLeave);
            // 
            // label70
            // 
            this.label70.AutoSize = true;
            this.label70.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label70.Location = new System.Drawing.Point(187, 61);
            this.label70.Name = "label70";
            this.label70.Size = new System.Drawing.Size(61, 15);
            this.label70.TabIndex = 45;
            this.label70.Text = "Поворот:";
            // 
            // DIncline2
            // 
            this.DIncline2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.DIncline2.Location = new System.Drawing.Point(248, 36);
            this.DIncline2.Name = "DIncline2";
            this.DIncline2.Size = new System.Drawing.Size(91, 20);
            this.DIncline2.TabIndex = 44;
            this.DIncline2.KeyDown += new System.Windows.Forms.KeyEventHandler(this.DynamicsKeyDown);
            this.DIncline2.Leave += new System.EventHandler(this.DynamicsLeave);
            // 
            // label71
            // 
            this.label71.AutoSize = true;
            this.label71.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label71.Location = new System.Drawing.Point(184, 38);
            this.label71.Name = "label71";
            this.label71.Size = new System.Drawing.Size(63, 15);
            this.label71.TabIndex = 43;
            this.label71.Text = "Наклон 2:";
            // 
            // DIncline1
            // 
            this.DIncline1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.DIncline1.Location = new System.Drawing.Point(67, 105);
            this.DIncline1.Name = "DIncline1";
            this.DIncline1.Size = new System.Drawing.Size(91, 20);
            this.DIncline1.TabIndex = 42;
            this.DIncline1.KeyDown += new System.Windows.Forms.KeyEventHandler(this.DynamicsKeyDown);
            this.DIncline1.Leave += new System.EventHandler(this.DynamicsLeave);
            // 
            // label72
            // 
            this.label72.AutoSize = true;
            this.label72.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label72.Location = new System.Drawing.Point(4, 107);
            this.label72.Name = "label72";
            this.label72.Size = new System.Drawing.Size(63, 15);
            this.label72.TabIndex = 41;
            this.label72.Text = "Наклон 1:";
            // 
            // label69
            // 
            this.label69.AutoSize = true;
            this.label69.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label69.Location = new System.Drawing.Point(46, 15);
            this.label69.Name = "label69";
            this.label69.Size = new System.Drawing.Size(22, 15);
            this.label69.TabIndex = 39;
            this.label69.Text = "ID:";
            // 
            // DZ_position
            // 
            this.DZ_position.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.DZ_position.Location = new System.Drawing.Point(67, 82);
            this.DZ_position.Name = "DZ_position";
            this.DZ_position.Size = new System.Drawing.Size(91, 20);
            this.DZ_position.TabIndex = 10;
            this.DZ_position.KeyDown += new System.Windows.Forms.KeyEventHandler(this.DynamicsKeyDown);
            this.DZ_position.Leave += new System.EventHandler(this.DynamicsLeave);
            // 
            // DY_position
            // 
            this.DY_position.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.DY_position.Location = new System.Drawing.Point(67, 59);
            this.DY_position.Name = "DY_position";
            this.DY_position.Size = new System.Drawing.Size(91, 20);
            this.DY_position.TabIndex = 9;
            this.DY_position.KeyDown += new System.Windows.Forms.KeyEventHandler(this.DynamicsKeyDown);
            this.DY_position.Leave += new System.EventHandler(this.DynamicsLeave);
            // 
            // DX_position
            // 
            this.DX_position.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.DX_position.Location = new System.Drawing.Point(67, 36);
            this.DX_position.Name = "DX_position";
            this.DX_position.Size = new System.Drawing.Size(91, 20);
            this.DX_position.TabIndex = 7;
            this.DX_position.KeyDown += new System.Windows.Forms.KeyEventHandler(this.DynamicsKeyDown);
            this.DX_position.Leave += new System.EventHandler(this.DynamicsLeave);
            // 
            // label80
            // 
            this.label80.AutoSize = true;
            this.label80.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label80.Location = new System.Drawing.Point(50, 83);
            this.label80.Name = "label80";
            this.label80.Size = new System.Drawing.Size(17, 15);
            this.label80.TabIndex = 6;
            this.label80.Text = "Z:";
            // 
            // label81
            // 
            this.label81.AutoSize = true;
            this.label81.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label81.Location = new System.Drawing.Point(50, 61);
            this.label81.Name = "label81";
            this.label81.Size = new System.Drawing.Size(17, 15);
            this.label81.TabIndex = 5;
            this.label81.Text = "Y:";
            // 
            // label82
            // 
            this.label82.AutoSize = true;
            this.label82.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label82.Location = new System.Drawing.Point(50, 38);
            this.label82.Name = "label82";
            this.label82.Size = new System.Drawing.Size(18, 15);
            this.label82.TabIndex = 4;
            this.label82.Text = "X:";
            // 
            // DynamicGrid
            // 
            this.DynamicGrid.AllowUserToAddRows = false;
            this.DynamicGrid.AllowUserToDeleteRows = false;
            this.DynamicGrid.AllowUserToResizeColumns = false;
            this.DynamicGrid.AllowUserToResizeRows = false;
            this.DynamicGrid.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.DynamicGrid.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(15)))), ((int)(((byte)(15)))));
            this.DynamicGrid.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None;
            this.DynamicGrid.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle17.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle17.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle17.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            dataGridViewCellStyle17.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle17.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle17.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle17.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.DynamicGrid.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle17;
            this.DynamicGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DynamicGrid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewTextBoxColumn12,
            this.dataGridViewTextBoxColumn13,
            this.dataGridViewTextBoxColumn14,
            this.Column1});
            this.DynamicGrid.ContextMenuStrip = this.ExistenceContext;
            dataGridViewCellStyle21.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle21.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(15)))), ((int)(((byte)(15)))));
            dataGridViewCellStyle21.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            dataGridViewCellStyle21.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            dataGridViewCellStyle21.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle21.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle21.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.DynamicGrid.DefaultCellStyle = dataGridViewCellStyle21;
            this.DynamicGrid.EnableHeadersVisualStyles = false;
            this.DynamicGrid.Location = new System.Drawing.Point(1, 29);
            this.DynamicGrid.Name = "DynamicGrid";
            this.DynamicGrid.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            this.DynamicGrid.RowHeadersVisible = false;
            this.DynamicGrid.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.DynamicGrid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.DynamicGrid.ShowCellErrors = false;
            this.DynamicGrid.ShowCellToolTips = false;
            this.DynamicGrid.ShowEditingIcon = false;
            this.DynamicGrid.ShowRowErrors = false;
            this.DynamicGrid.Size = new System.Drawing.Size(359, 425);
            this.DynamicGrid.TabIndex = 17;
            this.DynamicGrid.CurrentCellChanged += new System.EventHandler(this.DynamicGrid_CurrentCellChanged);
            this.DynamicGrid.KeyDown += new System.Windows.Forms.KeyEventHandler(this.GridsKeyDown);
            // 
            // dataGridViewTextBoxColumn12
            // 
            dataGridViewCellStyle18.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.dataGridViewTextBoxColumn12.DefaultCellStyle = dataGridViewCellStyle18;
            this.dataGridViewTextBoxColumn12.FillWeight = 6.912442F;
            this.dataGridViewTextBoxColumn12.HeaderText = "#";
            this.dataGridViewTextBoxColumn12.Name = "dataGridViewTextBoxColumn12";
            this.dataGridViewTextBoxColumn12.ReadOnly = true;
            this.dataGridViewTextBoxColumn12.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.dataGridViewTextBoxColumn12.Width = 45;
            // 
            // dataGridViewTextBoxColumn13
            // 
            dataGridViewCellStyle19.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            dataGridViewCellStyle19.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(77)))), ((int)(((byte)(255)))), ((int)(((byte)(143)))));
            this.dataGridViewTextBoxColumn13.DefaultCellStyle = dataGridViewCellStyle19;
            this.dataGridViewTextBoxColumn13.FillWeight = 44.7233F;
            this.dataGridViewTextBoxColumn13.HeaderText = "  ID";
            this.dataGridViewTextBoxColumn13.Name = "dataGridViewTextBoxColumn13";
            this.dataGridViewTextBoxColumn13.ReadOnly = true;
            this.dataGridViewTextBoxColumn13.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.dataGridViewTextBoxColumn13.Width = 60;
            // 
            // dataGridViewTextBoxColumn14
            // 
            dataGridViewCellStyle20.ForeColor = System.Drawing.Color.White;
            this.dataGridViewTextBoxColumn14.DefaultCellStyle = dataGridViewCellStyle20;
            this.dataGridViewTextBoxColumn14.HeaderText = "Name";
            this.dataGridViewTextBoxColumn14.Name = "dataGridViewTextBoxColumn14";
            this.dataGridViewTextBoxColumn14.ReadOnly = true;
            this.dataGridViewTextBoxColumn14.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.dataGridViewTextBoxColumn14.Width = 196;
            // 
            // Column1
            // 
            this.Column1.HeaderText = "Trigger";
            this.Column1.Name = "Column1";
            this.Column1.ReadOnly = true;
            this.Column1.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.Column1.Width = 59;
            // 
            // DynObjectsRemoveButton
            // 
            this.DynObjectsRemoveButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.DynObjectsRemoveButton.ForeColor = System.Drawing.Color.Black;
            this.DynObjectsRemoveButton.Image = ((System.Drawing.Image)(resources.GetObject("DynObjectsRemoveButton.Image")));
            this.DynObjectsRemoveButton.Location = new System.Drawing.Point(180, 454);
            this.DynObjectsRemoveButton.Name = "DynObjectsRemoveButton";
            this.DynObjectsRemoveButton.Size = new System.Drawing.Size(181, 29);
            this.DynObjectsRemoveButton.TabIndex = 19;
            this.DynObjectsRemoveButton.Text = "Удалить";
            this.DynObjectsRemoveButton.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.DynObjectsRemoveButton.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.DynObjectsRemoveButton.UseVisualStyleBackColor = true;
            this.DynObjectsRemoveButton.Click += new System.EventHandler(this.DDelete_button_Click);
            // 
            // DynObjectsCloneButton
            // 
            this.DynObjectsCloneButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.DynObjectsCloneButton.ForeColor = System.Drawing.Color.Black;
            this.DynObjectsCloneButton.Image = ((System.Drawing.Image)(resources.GetObject("DynObjectsCloneButton.Image")));
            this.DynObjectsCloneButton.Location = new System.Drawing.Point(0, 454);
            this.DynObjectsCloneButton.Name = "DynObjectsCloneButton";
            this.DynObjectsCloneButton.Size = new System.Drawing.Size(181, 29);
            this.DynObjectsCloneButton.TabIndex = 18;
            this.DynObjectsCloneButton.Text = "Клонировать";
            this.DynObjectsCloneButton.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.DynObjectsCloneButton.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.DynObjectsCloneButton.UseVisualStyleBackColor = true;
            this.DynObjectsCloneButton.Click += new System.EventHandler(this.DClone_button_Click);
            // 
            // toolStrip2
            // 
            this.toolStrip2.AutoSize = false;
            this.toolStrip2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(239)))), ((int)(((byte)(244)))), ((int)(((byte)(250)))));
            this.toolStrip2.GripMargin = new System.Windows.Forms.Padding(0);
            this.toolStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButton3,
            this.toolStripButton4,
            this.toolStripDropDownButton3,
            this.toolStripDropDownButton4});
            this.toolStrip2.Location = new System.Drawing.Point(3, 3);
            this.toolStrip2.Name = "toolStrip2";
            this.toolStrip2.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.toolStrip2.Size = new System.Drawing.Size(960, 25);
            this.toolStrip2.TabIndex = 22;
            this.toolStrip2.Text = "toolStrip2";
            // 
            // toolStripButton3
            // 
            this.toolStripButton3.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton3.Image")));
            this.toolStripButton3.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.toolStripButton3.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton3.Name = "toolStripButton3";
            this.toolStripButton3.Size = new System.Drawing.Size(74, 22);
            this.toolStripButton3.Text = "Экспорт";
            this.toolStripButton3.ToolTipText = "Export";
            this.toolStripButton3.Click += new System.EventHandler(this.ExportExistence_Click);
            // 
            // toolStripButton4
            // 
            this.toolStripButton4.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton4.Image")));
            this.toolStripButton4.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton4.Name = "toolStripButton4";
            this.toolStripButton4.Size = new System.Drawing.Size(71, 22);
            this.toolStripButton4.Text = "Импорт";
            this.toolStripButton4.ToolTipText = "Import";
            this.toolStripButton4.Click += new System.EventHandler(this.ImportExistence_Click);
            // 
            // toolStripDropDownButton3
            // 
            this.toolStripDropDownButton3.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem7,
            this.toolStripMenuItem8});
            this.toolStripDropDownButton3.Image = ((System.Drawing.Image)(resources.GetObject("toolStripDropDownButton3.Image")));
            this.toolStripDropDownButton3.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripDropDownButton3.Name = "toolStripDropDownButton3";
            this.toolStripDropDownButton3.Size = new System.Drawing.Size(95, 22);
            this.toolStripDropDownButton3.Text = "Выстроить";
            this.toolStripDropDownButton3.ToolTipText = "Line up";
            // 
            // toolStripMenuItem7
            // 
            this.toolStripMenuItem7.Image = ((System.Drawing.Image)(resources.GetObject("toolStripMenuItem7.Image")));
            this.toolStripMenuItem7.Name = "toolStripMenuItem7";
            this.toolStripMenuItem7.Size = new System.Drawing.Size(100, 22);
            this.toolStripMenuItem7.Text = "По X";
            this.toolStripMenuItem7.Click += new System.EventHandler(this.LineUpX_Click);
            // 
            // toolStripMenuItem8
            // 
            this.toolStripMenuItem8.Image = ((System.Drawing.Image)(resources.GetObject("toolStripMenuItem8.Image")));
            this.toolStripMenuItem8.Name = "toolStripMenuItem8";
            this.toolStripMenuItem8.Size = new System.Drawing.Size(100, 22);
            this.toolStripMenuItem8.Text = "По Z";
            this.toolStripMenuItem8.Click += new System.EventHandler(this.LineUpZ_Click);
            // 
            // toolStripDropDownButton4
            // 
            this.toolStripDropDownButton4.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem9,
            this.toolStripMenuItem10});
            this.toolStripDropDownButton4.Image = ((System.Drawing.Image)(resources.GetObject("toolStripDropDownButton4.Image")));
            this.toolStripDropDownButton4.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripDropDownButton4.Name = "toolStripDropDownButton4";
            this.toolStripDropDownButton4.Size = new System.Drawing.Size(108, 22);
            this.toolStripDropDownButton4.Text = "Переместить";
            this.toolStripDropDownButton4.ToolTipText = "Move";
            // 
            // toolStripMenuItem9
            // 
            this.toolStripMenuItem9.Image = ((System.Drawing.Image)(resources.GetObject("toolStripMenuItem9.Image")));
            this.toolStripMenuItem9.Name = "toolStripMenuItem9";
            this.toolStripMenuItem9.Size = new System.Drawing.Size(159, 22);
            this.toolStripMenuItem9.Text = "Выше   Shift+W";
            this.toolStripMenuItem9.Click += new System.EventHandler(this.UpObjects);
            // 
            // toolStripMenuItem10
            // 
            this.toolStripMenuItem10.Image = ((System.Drawing.Image)(resources.GetObject("toolStripMenuItem10.Image")));
            this.toolStripMenuItem10.Name = "toolStripMenuItem10";
            this.toolStripMenuItem10.Size = new System.Drawing.Size(159, 22);
            this.toolStripMenuItem10.Text = "Ниже    Shift+S";
            this.toolStripMenuItem10.Click += new System.EventHandler(this.DownObjects);
            // 
            // TriggersTab
            // 
            this.TriggersTab.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(239)))), ((int)(((byte)(244)))), ((int)(((byte)(250)))));
            this.TriggersTab.Controls.Add(this.groupBox15);
            this.TriggersTab.Controls.Add(this.groupBox14);
            this.TriggersTab.Controls.Add(this.groupBox13);
            this.TriggersTab.Controls.Add(this.groupBox12);
            this.TriggersTab.Controls.Add(this.TriggersGrid);
            this.TriggersTab.Controls.Add(this.toolStrip3);
            this.TriggersTab.Controls.Add(this.TriggersRemoveButton);
            this.TriggersTab.Controls.Add(this.TriggersCloneButton);
            this.TriggersTab.Location = new System.Drawing.Point(4, 22);
            this.TriggersTab.Name = "TriggersTab";
            this.TriggersTab.Padding = new System.Windows.Forms.Padding(3);
            this.TriggersTab.Size = new System.Drawing.Size(966, 483);
            this.TriggersTab.TabIndex = 3;
            this.TriggersTab.Text = "Тригеры";
            // 
            // groupBox15
            // 
            this.groupBox15.Controls.Add(this.DUTrigger);
            this.groupBox15.Controls.Add(this.GotoDynamicsContacts);
            this.groupBox15.Location = new System.Drawing.Point(767, 214);
            this.groupBox15.Name = "groupBox15";
            this.groupBox15.Size = new System.Drawing.Size(198, 267);
            this.groupBox15.TabIndex = 31;
            this.groupBox15.TabStop = false;
            this.groupBox15.Text = "Используется в ресурсах";
            // 
            // DUTrigger
            // 
            this.DUTrigger.AllowUserToAddRows = false;
            this.DUTrigger.AllowUserToDeleteRows = false;
            this.DUTrigger.AllowUserToResizeColumns = false;
            this.DUTrigger.AllowUserToResizeRows = false;
            this.DUTrigger.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.DUTrigger.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(15)))), ((int)(((byte)(15)))));
            this.DUTrigger.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None;
            this.DUTrigger.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            this.DUTrigger.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DUTrigger.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewTextBoxColumn25,
            this.dataGridViewTextBoxColumn26,
            this.dataGridViewTextBoxColumn27});
            dataGridViewCellStyle25.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle25.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(15)))), ((int)(((byte)(15)))));
            dataGridViewCellStyle25.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            dataGridViewCellStyle25.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            dataGridViewCellStyle25.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle25.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle25.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.DUTrigger.DefaultCellStyle = dataGridViewCellStyle25;
            this.DUTrigger.EnableHeadersVisualStyles = false;
            this.DUTrigger.Location = new System.Drawing.Point(2, 15);
            this.DUTrigger.Name = "DUTrigger";
            this.DUTrigger.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            this.DUTrigger.RowHeadersVisible = false;
            this.DUTrigger.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.DUTrigger.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.DUTrigger.ShowCellErrors = false;
            this.DUTrigger.ShowCellToolTips = false;
            this.DUTrigger.ShowEditingIcon = false;
            this.DUTrigger.ShowRowErrors = false;
            this.DUTrigger.Size = new System.Drawing.Size(194, 197);
            this.DUTrigger.TabIndex = 25;
            this.DUTrigger.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.DUTrigger_CellDoubleClick);
            // 
            // dataGridViewTextBoxColumn25
            // 
            dataGridViewCellStyle22.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.dataGridViewTextBoxColumn25.DefaultCellStyle = dataGridViewCellStyle22;
            this.dataGridViewTextBoxColumn25.FillWeight = 6.912442F;
            this.dataGridViewTextBoxColumn25.HeaderText = "#";
            this.dataGridViewTextBoxColumn25.Name = "dataGridViewTextBoxColumn25";
            this.dataGridViewTextBoxColumn25.ReadOnly = true;
            this.dataGridViewTextBoxColumn25.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.dataGridViewTextBoxColumn25.Width = 35;
            // 
            // dataGridViewTextBoxColumn26
            // 
            dataGridViewCellStyle23.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            dataGridViewCellStyle23.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(77)))), ((int)(((byte)(255)))), ((int)(((byte)(143)))));
            this.dataGridViewTextBoxColumn26.DefaultCellStyle = dataGridViewCellStyle23;
            this.dataGridViewTextBoxColumn26.FillWeight = 44.7233F;
            this.dataGridViewTextBoxColumn26.HeaderText = "  ID";
            this.dataGridViewTextBoxColumn26.Name = "dataGridViewTextBoxColumn26";
            this.dataGridViewTextBoxColumn26.ReadOnly = true;
            this.dataGridViewTextBoxColumn26.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.dataGridViewTextBoxColumn26.Width = 45;
            // 
            // dataGridViewTextBoxColumn27
            // 
            dataGridViewCellStyle24.ForeColor = System.Drawing.Color.LightGray;
            this.dataGridViewTextBoxColumn27.DefaultCellStyle = dataGridViewCellStyle24;
            this.dataGridViewTextBoxColumn27.HeaderText = "Name";
            this.dataGridViewTextBoxColumn27.Name = "dataGridViewTextBoxColumn27";
            this.dataGridViewTextBoxColumn27.ReadOnly = true;
            this.dataGridViewTextBoxColumn27.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.dataGridViewTextBoxColumn27.Width = 120;
            // 
            // GotoDynamicsContacts
            // 
            this.GotoDynamicsContacts.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.GotoDynamicsContacts.ForeColor = System.Drawing.Color.Black;
            this.GotoDynamicsContacts.Image = ((System.Drawing.Image)(resources.GetObject("GotoDynamicsContacts.Image")));
            this.GotoDynamicsContacts.Location = new System.Drawing.Point(1, 211);
            this.GotoDynamicsContacts.Name = "GotoDynamicsContacts";
            this.GotoDynamicsContacts.Size = new System.Drawing.Size(196, 57);
            this.GotoDynamicsContacts.TabIndex = 27;
            this.GotoDynamicsContacts.Text = "Перейти к выбранному";
            this.GotoDynamicsContacts.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.GotoDynamicsContacts.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.GotoDynamicsContacts.UseVisualStyleBackColor = true;
            this.GotoDynamicsContacts.Click += new System.EventHandler(this.GotoDynamicsContacts_Click);
            // 
            // groupBox14
            // 
            this.groupBox14.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(239)))), ((int)(((byte)(244)))), ((int)(((byte)(250)))));
            this.groupBox14.Controls.Add(this.RUTrigger);
            this.groupBox14.Controls.Add(this.GotoResourcesContacts);
            this.groupBox14.Location = new System.Drawing.Point(565, 214);
            this.groupBox14.Name = "groupBox14";
            this.groupBox14.Size = new System.Drawing.Size(198, 267);
            this.groupBox14.TabIndex = 30;
            this.groupBox14.TabStop = false;
            this.groupBox14.Text = "Используется в ресурсах";
            // 
            // RUTrigger
            // 
            this.RUTrigger.AllowUserToAddRows = false;
            this.RUTrigger.AllowUserToDeleteRows = false;
            this.RUTrigger.AllowUserToResizeColumns = false;
            this.RUTrigger.AllowUserToResizeRows = false;
            this.RUTrigger.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.RUTrigger.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(15)))), ((int)(((byte)(15)))));
            this.RUTrigger.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None;
            this.RUTrigger.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            this.RUTrigger.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.RUTrigger.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewTextBoxColumn22,
            this.dataGridViewTextBoxColumn23,
            this.dataGridViewTextBoxColumn24});
            dataGridViewCellStyle29.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle29.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(15)))), ((int)(((byte)(15)))));
            dataGridViewCellStyle29.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            dataGridViewCellStyle29.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            dataGridViewCellStyle29.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle29.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle29.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.RUTrigger.DefaultCellStyle = dataGridViewCellStyle29;
            this.RUTrigger.EnableHeadersVisualStyles = false;
            this.RUTrigger.Location = new System.Drawing.Point(2, 15);
            this.RUTrigger.Name = "RUTrigger";
            this.RUTrigger.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            this.RUTrigger.RowHeadersVisible = false;
            this.RUTrigger.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.RUTrigger.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.RUTrigger.ShowCellErrors = false;
            this.RUTrigger.ShowCellToolTips = false;
            this.RUTrigger.ShowEditingIcon = false;
            this.RUTrigger.ShowRowErrors = false;
            this.RUTrigger.Size = new System.Drawing.Size(194, 197);
            this.RUTrigger.TabIndex = 25;
            this.RUTrigger.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.RUTrigger_CellDoubleClick);
            // 
            // dataGridViewTextBoxColumn22
            // 
            dataGridViewCellStyle26.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.dataGridViewTextBoxColumn22.DefaultCellStyle = dataGridViewCellStyle26;
            this.dataGridViewTextBoxColumn22.FillWeight = 6.912442F;
            this.dataGridViewTextBoxColumn22.HeaderText = "#";
            this.dataGridViewTextBoxColumn22.Name = "dataGridViewTextBoxColumn22";
            this.dataGridViewTextBoxColumn22.ReadOnly = true;
            this.dataGridViewTextBoxColumn22.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.dataGridViewTextBoxColumn22.Width = 35;
            // 
            // dataGridViewTextBoxColumn23
            // 
            dataGridViewCellStyle27.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            dataGridViewCellStyle27.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(77)))), ((int)(((byte)(255)))), ((int)(((byte)(143)))));
            this.dataGridViewTextBoxColumn23.DefaultCellStyle = dataGridViewCellStyle27;
            this.dataGridViewTextBoxColumn23.FillWeight = 44.7233F;
            this.dataGridViewTextBoxColumn23.HeaderText = "  ID";
            this.dataGridViewTextBoxColumn23.Name = "dataGridViewTextBoxColumn23";
            this.dataGridViewTextBoxColumn23.ReadOnly = true;
            this.dataGridViewTextBoxColumn23.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.dataGridViewTextBoxColumn23.Width = 45;
            // 
            // dataGridViewTextBoxColumn24
            // 
            dataGridViewCellStyle28.ForeColor = System.Drawing.Color.LightGray;
            this.dataGridViewTextBoxColumn24.DefaultCellStyle = dataGridViewCellStyle28;
            this.dataGridViewTextBoxColumn24.HeaderText = "Name";
            this.dataGridViewTextBoxColumn24.Name = "dataGridViewTextBoxColumn24";
            this.dataGridViewTextBoxColumn24.ReadOnly = true;
            this.dataGridViewTextBoxColumn24.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.dataGridViewTextBoxColumn24.Width = 120;
            // 
            // GotoResourcesContacts
            // 
            this.GotoResourcesContacts.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.GotoResourcesContacts.ForeColor = System.Drawing.Color.Black;
            this.GotoResourcesContacts.Image = ((System.Drawing.Image)(resources.GetObject("GotoResourcesContacts.Image")));
            this.GotoResourcesContacts.Location = new System.Drawing.Point(1, 211);
            this.GotoResourcesContacts.Name = "GotoResourcesContacts";
            this.GotoResourcesContacts.Size = new System.Drawing.Size(196, 57);
            this.GotoResourcesContacts.TabIndex = 27;
            this.GotoResourcesContacts.Text = "Перейти к выбранному";
            this.GotoResourcesContacts.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.GotoResourcesContacts.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.GotoResourcesContacts.UseVisualStyleBackColor = true;
            this.GotoResourcesContacts.Click += new System.EventHandler(this.GotoResourcesContacts_Click);
            // 
            // groupBox13
            // 
            this.groupBox13.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(239)))), ((int)(((byte)(244)))), ((int)(((byte)(250)))));
            this.groupBox13.Controls.Add(this.MUTrigger);
            this.groupBox13.Controls.Add(this.GotoNpcMobsContacts);
            this.groupBox13.Location = new System.Drawing.Point(364, 214);
            this.groupBox13.Name = "groupBox13";
            this.groupBox13.Size = new System.Drawing.Size(198, 267);
            this.groupBox13.TabIndex = 29;
            this.groupBox13.TabStop = false;
            this.groupBox13.Text = "Используется в существах";
            // 
            // MUTrigger
            // 
            this.MUTrigger.AllowUserToAddRows = false;
            this.MUTrigger.AllowUserToDeleteRows = false;
            this.MUTrigger.AllowUserToResizeColumns = false;
            this.MUTrigger.AllowUserToResizeRows = false;
            this.MUTrigger.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.MUTrigger.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(15)))), ((int)(((byte)(15)))));
            this.MUTrigger.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None;
            this.MUTrigger.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            this.MUTrigger.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.MUTrigger.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewTextBoxColumn28,
            this.dataGridViewTextBoxColumn29,
            this.dataGridViewTextBoxColumn30});
            dataGridViewCellStyle33.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle33.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(15)))), ((int)(((byte)(15)))));
            dataGridViewCellStyle33.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            dataGridViewCellStyle33.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            dataGridViewCellStyle33.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle33.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle33.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.MUTrigger.DefaultCellStyle = dataGridViewCellStyle33;
            this.MUTrigger.EnableHeadersVisualStyles = false;
            this.MUTrigger.Location = new System.Drawing.Point(2, 15);
            this.MUTrigger.Name = "MUTrigger";
            this.MUTrigger.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            this.MUTrigger.RowHeadersVisible = false;
            this.MUTrigger.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.MUTrigger.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.MUTrigger.ShowCellErrors = false;
            this.MUTrigger.ShowCellToolTips = false;
            this.MUTrigger.ShowEditingIcon = false;
            this.MUTrigger.ShowRowErrors = false;
            this.MUTrigger.Size = new System.Drawing.Size(194, 197);
            this.MUTrigger.TabIndex = 25;
            this.MUTrigger.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.MUTrigger_CellDoubleClick);
            this.MUTrigger.CellMouseEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.TriggerUsingInMobsAndNpcsGrid_CellMouseEnter);
            // 
            // dataGridViewTextBoxColumn28
            // 
            dataGridViewCellStyle30.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.dataGridViewTextBoxColumn28.DefaultCellStyle = dataGridViewCellStyle30;
            this.dataGridViewTextBoxColumn28.FillWeight = 6.912442F;
            this.dataGridViewTextBoxColumn28.HeaderText = "#";
            this.dataGridViewTextBoxColumn28.Name = "dataGridViewTextBoxColumn28";
            this.dataGridViewTextBoxColumn28.ReadOnly = true;
            this.dataGridViewTextBoxColumn28.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.dataGridViewTextBoxColumn28.Width = 35;
            // 
            // dataGridViewTextBoxColumn29
            // 
            dataGridViewCellStyle31.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            dataGridViewCellStyle31.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(77)))), ((int)(((byte)(255)))), ((int)(((byte)(143)))));
            this.dataGridViewTextBoxColumn29.DefaultCellStyle = dataGridViewCellStyle31;
            this.dataGridViewTextBoxColumn29.FillWeight = 44.7233F;
            this.dataGridViewTextBoxColumn29.HeaderText = "  ID";
            this.dataGridViewTextBoxColumn29.Name = "dataGridViewTextBoxColumn29";
            this.dataGridViewTextBoxColumn29.ReadOnly = true;
            this.dataGridViewTextBoxColumn29.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.dataGridViewTextBoxColumn29.Width = 45;
            // 
            // dataGridViewTextBoxColumn30
            // 
            dataGridViewCellStyle32.ForeColor = System.Drawing.Color.LightGray;
            this.dataGridViewTextBoxColumn30.DefaultCellStyle = dataGridViewCellStyle32;
            this.dataGridViewTextBoxColumn30.HeaderText = "Name";
            this.dataGridViewTextBoxColumn30.Name = "dataGridViewTextBoxColumn30";
            this.dataGridViewTextBoxColumn30.ReadOnly = true;
            this.dataGridViewTextBoxColumn30.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.dataGridViewTextBoxColumn30.Width = 120;
            // 
            // GotoNpcMobsContacts
            // 
            this.GotoNpcMobsContacts.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.GotoNpcMobsContacts.ForeColor = System.Drawing.Color.Black;
            this.GotoNpcMobsContacts.Image = ((System.Drawing.Image)(resources.GetObject("GotoNpcMobsContacts.Image")));
            this.GotoNpcMobsContacts.Location = new System.Drawing.Point(1, 211);
            this.GotoNpcMobsContacts.Name = "GotoNpcMobsContacts";
            this.GotoNpcMobsContacts.Size = new System.Drawing.Size(196, 57);
            this.GotoNpcMobsContacts.TabIndex = 27;
            this.GotoNpcMobsContacts.Text = "Перейти к выбранному";
            this.GotoNpcMobsContacts.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.GotoNpcMobsContacts.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.GotoNpcMobsContacts.UseVisualStyleBackColor = true;
            this.GotoNpcMobsContacts.Click += new System.EventHandler(this.GotoNpcMobsContacts_Click);
            // 
            // groupBox12
            // 
            this.groupBox12.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.groupBox12.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(239)))), ((int)(((byte)(244)))), ((int)(((byte)(250)))));
            this.groupBox12.Controls.Add(this.groupBox17);
            this.groupBox12.Controls.Add(this.groupBox16);
            this.groupBox12.Controls.Add(this.TStartBySchedule);
            this.groupBox12.Controls.Add(this.TStopBySchedule);
            this.groupBox12.Controls.Add(this.TDuration);
            this.groupBox12.Controls.Add(this.label85);
            this.groupBox12.Controls.Add(this.TWaitStop_textbox);
            this.groupBox12.Controls.Add(this.label84);
            this.groupBox12.Controls.Add(this.TWaitStart_textbox);
            this.groupBox12.Controls.Add(this.TAutoStart);
            this.groupBox12.Controls.Add(this.TId_textbox);
            this.groupBox12.Controls.Add(this.label78);
            this.groupBox12.Controls.Add(this.label89);
            this.groupBox12.Controls.Add(this.TName_textbox);
            this.groupBox12.Controls.Add(this.TGmId_textbox);
            this.groupBox12.Controls.Add(this.label79);
            this.groupBox12.Controls.Add(this.label83);
            this.groupBox12.Controls.Add(this.label99);
            this.groupBox12.Location = new System.Drawing.Point(364, 1);
            this.groupBox12.Name = "groupBox12";
            this.groupBox12.Size = new System.Drawing.Size(599, 209);
            this.groupBox12.TabIndex = 22;
            this.groupBox12.TabStop = false;
            this.groupBox12.Text = "Основное";
            // 
            // groupBox17
            // 
            this.groupBox17.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(239)))), ((int)(((byte)(244)))), ((int)(((byte)(250)))));
            this.groupBox17.Controls.Add(this.TStopMinute);
            this.groupBox17.Controls.Add(this.label93);
            this.groupBox17.Controls.Add(this.TStopHour);
            this.groupBox17.Controls.Add(this.label94);
            this.groupBox17.Controls.Add(this.TStopDay);
            this.groupBox17.Controls.Add(this.label95);
            this.groupBox17.Controls.Add(this.TStopWeekDay);
            this.groupBox17.Controls.Add(this.TStopMonth);
            this.groupBox17.Controls.Add(this.label96);
            this.groupBox17.Controls.Add(this.TStopYear);
            this.groupBox17.Controls.Add(this.label97);
            this.groupBox17.Controls.Add(this.label98);
            this.groupBox17.Location = new System.Drawing.Point(326, 105);
            this.groupBox17.Name = "groupBox17";
            this.groupBox17.Size = new System.Drawing.Size(273, 94);
            this.groupBox17.TabIndex = 94;
            this.groupBox17.TabStop = false;
            this.groupBox17.Text = "Выключение";
            // 
            // TStopMinute
            // 
            this.TStopMinute.Location = new System.Drawing.Point(207, 64);
            this.TStopMinute.Name = "TStopMinute";
            this.TStopMinute.Size = new System.Drawing.Size(63, 20);
            this.TStopMinute.TabIndex = 93;
            this.TStopMinute.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TId_textbox_KeyDown);
            this.TStopMinute.Leave += new System.EventHandler(this.TId_textbox_Leave);
            // 
            // label93
            // 
            this.label93.AutoSize = true;
            this.label93.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label93.Location = new System.Drawing.Point(154, 65);
            this.label93.Name = "label93";
            this.label93.Size = new System.Drawing.Size(54, 15);
            this.label93.TabIndex = 92;
            this.label93.Text = "Минута:";
            // 
            // TStopHour
            // 
            this.TStopHour.Location = new System.Drawing.Point(207, 40);
            this.TStopHour.Name = "TStopHour";
            this.TStopHour.Size = new System.Drawing.Size(63, 20);
            this.TStopHour.TabIndex = 91;
            this.TStopHour.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TId_textbox_KeyDown);
            this.TStopHour.Leave += new System.EventHandler(this.TId_textbox_Leave);
            // 
            // label94
            // 
            this.label94.AutoSize = true;
            this.label94.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label94.Location = new System.Drawing.Point(177, 41);
            this.label94.Name = "label94";
            this.label94.Size = new System.Drawing.Size(31, 15);
            this.label94.TabIndex = 90;
            this.label94.Text = "Час:";
            // 
            // TStopDay
            // 
            this.TStopDay.Location = new System.Drawing.Point(207, 14);
            this.TStopDay.Name = "TStopDay";
            this.TStopDay.Size = new System.Drawing.Size(63, 20);
            this.TStopDay.TabIndex = 89;
            this.TStopDay.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TId_textbox_KeyDown);
            this.TStopDay.Leave += new System.EventHandler(this.TId_textbox_Leave);
            // 
            // label95
            // 
            this.label95.AutoSize = true;
            this.label95.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label95.Location = new System.Drawing.Point(168, 15);
            this.label95.Name = "label95";
            this.label95.Size = new System.Drawing.Size(40, 15);
            this.label95.TabIndex = 88;
            this.label95.Text = "День:";
            // 
            // TStopWeekDay
            // 
            this.TStopWeekDay.BackColor = System.Drawing.Color.Silver;
            this.TStopWeekDay.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.TStopWeekDay.FormattingEnabled = true;
            this.TStopWeekDay.Items.AddRange(new object[] {
            "Все",
            "Воскресенье",
            "Понедельник",
            "Вторник",
            "Среда",
            "Четверг",
            "Пятница",
            "Суббота"});
            this.TStopWeekDay.Location = new System.Drawing.Point(84, 64);
            this.TStopWeekDay.Name = "TStopWeekDay";
            this.TStopWeekDay.Size = new System.Drawing.Size(63, 21);
            this.TStopWeekDay.TabIndex = 87;
            this.TStopWeekDay.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TId_textbox_KeyDown);
            this.TStopWeekDay.Leave += new System.EventHandler(this.TId_textbox_Leave);
            // 
            // TStopMonth
            // 
            this.TStopMonth.Location = new System.Drawing.Point(84, 40);
            this.TStopMonth.Name = "TStopMonth";
            this.TStopMonth.Size = new System.Drawing.Size(63, 20);
            this.TStopMonth.TabIndex = 86;
            this.TStopMonth.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TId_textbox_KeyDown);
            this.TStopMonth.Leave += new System.EventHandler(this.TId_textbox_Leave);
            // 
            // label96
            // 
            this.label96.AutoSize = true;
            this.label96.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label96.Location = new System.Drawing.Point(38, 40);
            this.label96.Name = "label96";
            this.label96.Size = new System.Drawing.Size(48, 15);
            this.label96.TabIndex = 84;
            this.label96.Text = "Месяц:";
            // 
            // TStopYear
            // 
            this.TStopYear.Location = new System.Drawing.Point(84, 15);
            this.TStopYear.Name = "TStopYear";
            this.TStopYear.Size = new System.Drawing.Size(63, 20);
            this.TStopYear.TabIndex = 83;
            this.TStopYear.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TId_textbox_KeyDown);
            this.TStopYear.Leave += new System.EventHandler(this.TId_textbox_Leave);
            // 
            // label97
            // 
            this.label97.AutoSize = true;
            this.label97.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label97.Location = new System.Drawing.Point(55, 16);
            this.label97.Name = "label97";
            this.label97.Size = new System.Drawing.Size(31, 15);
            this.label97.TabIndex = 82;
            this.label97.Text = "Год:";
            // 
            // label98
            // 
            this.label98.AutoSize = true;
            this.label98.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label98.Location = new System.Drawing.Point(1, 65);
            this.label98.Name = "label98";
            this.label98.Size = new System.Drawing.Size(85, 15);
            this.label98.TabIndex = 85;
            this.label98.Text = "День недели:";
            // 
            // groupBox16
            // 
            this.groupBox16.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(239)))), ((int)(((byte)(244)))), ((int)(((byte)(250)))));
            this.groupBox16.Controls.Add(this.TStartMinute);
            this.groupBox16.Controls.Add(this.label92);
            this.groupBox16.Controls.Add(this.TStartHour);
            this.groupBox16.Controls.Add(this.label91);
            this.groupBox16.Controls.Add(this.TStartDay);
            this.groupBox16.Controls.Add(this.label90);
            this.groupBox16.Controls.Add(this.TStartWeekDay);
            this.groupBox16.Controls.Add(this.TStartMonth);
            this.groupBox16.Controls.Add(this.label87);
            this.groupBox16.Controls.Add(this.TStartYear);
            this.groupBox16.Controls.Add(this.label86);
            this.groupBox16.Controls.Add(this.label88);
            this.groupBox16.Location = new System.Drawing.Point(323, 5);
            this.groupBox16.Name = "groupBox16";
            this.groupBox16.Size = new System.Drawing.Size(273, 94);
            this.groupBox16.TabIndex = 80;
            this.groupBox16.TabStop = false;
            this.groupBox16.Text = "Запуск";
            // 
            // TStartMinute
            // 
            this.TStartMinute.Location = new System.Drawing.Point(207, 64);
            this.TStartMinute.Name = "TStartMinute";
            this.TStartMinute.Size = new System.Drawing.Size(63, 20);
            this.TStartMinute.TabIndex = 93;
            this.TStartMinute.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TId_textbox_KeyDown);
            this.TStartMinute.Leave += new System.EventHandler(this.TId_textbox_Leave);
            // 
            // label92
            // 
            this.label92.AutoSize = true;
            this.label92.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label92.Location = new System.Drawing.Point(154, 65);
            this.label92.Name = "label92";
            this.label92.Size = new System.Drawing.Size(54, 15);
            this.label92.TabIndex = 92;
            this.label92.Text = "Минута:";
            // 
            // TStartHour
            // 
            this.TStartHour.Location = new System.Drawing.Point(207, 40);
            this.TStartHour.Name = "TStartHour";
            this.TStartHour.Size = new System.Drawing.Size(63, 20);
            this.TStartHour.TabIndex = 91;
            this.TStartHour.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TId_textbox_KeyDown);
            this.TStartHour.Leave += new System.EventHandler(this.TId_textbox_Leave);
            // 
            // label91
            // 
            this.label91.AutoSize = true;
            this.label91.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label91.Location = new System.Drawing.Point(177, 41);
            this.label91.Name = "label91";
            this.label91.Size = new System.Drawing.Size(31, 15);
            this.label91.TabIndex = 90;
            this.label91.Text = "Час:";
            // 
            // TStartDay
            // 
            this.TStartDay.Location = new System.Drawing.Point(207, 14);
            this.TStartDay.Name = "TStartDay";
            this.TStartDay.Size = new System.Drawing.Size(63, 20);
            this.TStartDay.TabIndex = 89;
            this.TStartDay.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TId_textbox_KeyDown);
            this.TStartDay.Leave += new System.EventHandler(this.TId_textbox_Leave);
            // 
            // label90
            // 
            this.label90.AutoSize = true;
            this.label90.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label90.Location = new System.Drawing.Point(168, 15);
            this.label90.Name = "label90";
            this.label90.Size = new System.Drawing.Size(40, 15);
            this.label90.TabIndex = 88;
            this.label90.Text = "День:";
            // 
            // TStartWeekDay
            // 
            this.TStartWeekDay.BackColor = System.Drawing.Color.Silver;
            this.TStartWeekDay.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.TStartWeekDay.FormattingEnabled = true;
            this.TStartWeekDay.Items.AddRange(new object[] {
            "Все",
            "Воскресенье",
            "Понедельник",
            "Вторник",
            "Среда",
            "Четверг",
            "Пятница",
            "Суббота"});
            this.TStartWeekDay.Location = new System.Drawing.Point(84, 64);
            this.TStartWeekDay.Name = "TStartWeekDay";
            this.TStartWeekDay.Size = new System.Drawing.Size(63, 21);
            this.TStartWeekDay.TabIndex = 87;
            this.TStartWeekDay.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TId_textbox_KeyDown);
            this.TStartWeekDay.Leave += new System.EventHandler(this.TId_textbox_Leave);
            // 
            // TStartMonth
            // 
            this.TStartMonth.Location = new System.Drawing.Point(84, 40);
            this.TStartMonth.Name = "TStartMonth";
            this.TStartMonth.Size = new System.Drawing.Size(63, 20);
            this.TStartMonth.TabIndex = 86;
            this.TStartMonth.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TId_textbox_KeyDown);
            this.TStartMonth.Leave += new System.EventHandler(this.TId_textbox_Leave);
            // 
            // label87
            // 
            this.label87.AutoSize = true;
            this.label87.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label87.Location = new System.Drawing.Point(38, 40);
            this.label87.Name = "label87";
            this.label87.Size = new System.Drawing.Size(48, 15);
            this.label87.TabIndex = 84;
            this.label87.Text = "Месяц:";
            // 
            // TStartYear
            // 
            this.TStartYear.Location = new System.Drawing.Point(84, 15);
            this.TStartYear.Name = "TStartYear";
            this.TStartYear.Size = new System.Drawing.Size(63, 20);
            this.TStartYear.TabIndex = 83;
            this.TStartYear.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TId_textbox_KeyDown);
            this.TStartYear.Leave += new System.EventHandler(this.TId_textbox_Leave);
            // 
            // label86
            // 
            this.label86.AutoSize = true;
            this.label86.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label86.Location = new System.Drawing.Point(55, 16);
            this.label86.Name = "label86";
            this.label86.Size = new System.Drawing.Size(31, 15);
            this.label86.TabIndex = 82;
            this.label86.Text = "Год:";
            // 
            // label88
            // 
            this.label88.AutoSize = true;
            this.label88.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label88.Location = new System.Drawing.Point(1, 65);
            this.label88.Name = "label88";
            this.label88.Size = new System.Drawing.Size(85, 15);
            this.label88.TabIndex = 85;
            this.label88.Text = "День недели:";
            // 
            // TStartBySchedule
            // 
            this.TStartBySchedule.AutoSize = true;
            this.TStartBySchedule.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.TStartBySchedule.Location = new System.Drawing.Point(178, 170);
            this.TStartBySchedule.Name = "TStartBySchedule";
            this.TStartBySchedule.Size = new System.Drawing.Size(139, 17);
            this.TStartBySchedule.TabIndex = 79;
            this.TStartBySchedule.Text = "Запускать по графику";
            this.TStartBySchedule.TextImageRelation = System.Windows.Forms.TextImageRelation.TextBeforeImage;
            this.TStartBySchedule.UseVisualStyleBackColor = true;
            this.TStartBySchedule.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TId_textbox_KeyDown);
            this.TStartBySchedule.Leave += new System.EventHandler(this.TId_textbox_Leave);
            // 
            // TStopBySchedule
            // 
            this.TStopBySchedule.AutoSize = true;
            this.TStopBySchedule.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.TStopBySchedule.Location = new System.Drawing.Point(174, 190);
            this.TStopBySchedule.Name = "TStopBySchedule";
            this.TStopBySchedule.Size = new System.Drawing.Size(143, 17);
            this.TStopBySchedule.TabIndex = 78;
            this.TStopBySchedule.Text = "Выключать по графику";
            this.TStopBySchedule.TextImageRelation = System.Windows.Forms.TextImageRelation.TextBeforeImage;
            this.TStopBySchedule.UseVisualStyleBackColor = true;
            this.TStopBySchedule.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TId_textbox_KeyDown);
            this.TStopBySchedule.Leave += new System.EventHandler(this.TId_textbox_Leave);
            // 
            // TDuration
            // 
            this.TDuration.Location = new System.Drawing.Point(147, 147);
            this.TDuration.Name = "TDuration";
            this.TDuration.Size = new System.Drawing.Size(170, 20);
            this.TDuration.TabIndex = 76;
            this.TDuration.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TId_textbox_KeyDown);
            this.TDuration.Leave += new System.EventHandler(this.TId_textbox_Leave);
            // 
            // label85
            // 
            this.label85.AutoSize = true;
            this.label85.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label85.Location = new System.Drawing.Point(7, 147);
            this.label85.Name = "label85";
            this.label85.Size = new System.Drawing.Size(141, 15);
            this.label85.TabIndex = 77;
            this.label85.Text = "   Продолжительность:";
            // 
            // TWaitStop_textbox
            // 
            this.TWaitStop_textbox.Location = new System.Drawing.Point(147, 105);
            this.TWaitStop_textbox.Name = "TWaitStop_textbox";
            this.TWaitStop_textbox.Size = new System.Drawing.Size(170, 20);
            this.TWaitStop_textbox.TabIndex = 74;
            this.TWaitStop_textbox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TId_textbox_KeyDown);
            this.TWaitStop_textbox.Leave += new System.EventHandler(this.TId_textbox_Leave);
            // 
            // label84
            // 
            this.label84.AutoSize = true;
            this.label84.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label84.Location = new System.Drawing.Point(5, 106);
            this.label84.Name = "label84";
            this.label84.Size = new System.Drawing.Size(143, 15);
            this.label84.TabIndex = 75;
            this.label84.Text = "Задержка выключения:";
            // 
            // TWaitStart_textbox
            // 
            this.TWaitStart_textbox.Location = new System.Drawing.Point(147, 83);
            this.TWaitStart_textbox.Name = "TWaitStart_textbox";
            this.TWaitStart_textbox.Size = new System.Drawing.Size(170, 20);
            this.TWaitStart_textbox.TabIndex = 72;
            this.TWaitStart_textbox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TId_textbox_KeyDown);
            this.TWaitStart_textbox.Leave += new System.EventHandler(this.TId_textbox_Leave);
            // 
            // TAutoStart
            // 
            this.TAutoStart.AutoSize = true;
            this.TAutoStart.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.TAutoStart.Location = new System.Drawing.Point(158, 128);
            this.TAutoStart.Name = "TAutoStart";
            this.TAutoStart.Size = new System.Drawing.Size(159, 17);
            this.TAutoStart.TabIndex = 71;
            this.TAutoStart.Text = "Запускать автоматически";
            this.TAutoStart.TextImageRelation = System.Windows.Forms.TextImageRelation.TextBeforeImage;
            this.TAutoStart.UseVisualStyleBackColor = true;
            this.TAutoStart.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TId_textbox_KeyDown);
            this.TAutoStart.Leave += new System.EventHandler(this.TId_textbox_Leave);
            // 
            // TId_textbox
            // 
            this.TId_textbox.Location = new System.Drawing.Point(147, 14);
            this.TId_textbox.Name = "TId_textbox";
            this.TId_textbox.Size = new System.Drawing.Size(170, 20);
            this.TId_textbox.TabIndex = 69;
            this.TId_textbox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TId_textbox_KeyDown);
            this.TId_textbox.Leave += new System.EventHandler(this.TId_textbox_Leave);
            // 
            // label78
            // 
            this.label78.AutoSize = true;
            this.label78.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label78.ForeColor = System.Drawing.Color.Red;
            this.label78.Location = new System.Drawing.Point(157, 15);
            this.label78.Name = "label78";
            this.label78.Size = new System.Drawing.Size(0, 16);
            this.label78.TabIndex = 68;
            // 
            // label89
            // 
            this.label89.AutoSize = true;
            this.label89.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label89.Location = new System.Drawing.Point(71, 16);
            this.label89.Name = "label89";
            this.label89.Size = new System.Drawing.Size(77, 15);
            this.label89.TabIndex = 39;
            this.label89.Text = "ID Триггера:";
            // 
            // TName_textbox
            // 
            this.TName_textbox.Location = new System.Drawing.Point(147, 60);
            this.TName_textbox.Name = "TName_textbox";
            this.TName_textbox.Size = new System.Drawing.Size(170, 20);
            this.TName_textbox.TabIndex = 9;
            this.TName_textbox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TId_textbox_KeyDown);
            this.TName_textbox.Leave += new System.EventHandler(this.TId_textbox_Leave);
            // 
            // TGmId_textbox
            // 
            this.TGmId_textbox.Location = new System.Drawing.Point(147, 37);
            this.TGmId_textbox.Name = "TGmId_textbox";
            this.TGmId_textbox.Size = new System.Drawing.Size(170, 20);
            this.TGmId_textbox.TabIndex = 7;
            this.TGmId_textbox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TId_textbox_KeyDown);
            this.TGmId_textbox.Leave += new System.EventHandler(this.TId_textbox_Leave);
            // 
            // label79
            // 
            this.label79.AutoSize = true;
            this.label79.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label79.Location = new System.Drawing.Point(50, 39);
            this.label79.Name = "label79";
            this.label79.Size = new System.Drawing.Size(98, 15);
            this.label79.TabIndex = 70;
            this.label79.Text = "ID в панели ГМ:";
            // 
            // label83
            // 
            this.label83.AutoSize = true;
            this.label83.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label83.Location = new System.Drawing.Point(14, 84);
            this.label83.Name = "label83";
            this.label83.Size = new System.Drawing.Size(134, 15);
            this.label83.TabIndex = 73;
            this.label83.Text = "Задержка включения:";
            // 
            // label99
            // 
            this.label99.AutoSize = true;
            this.label99.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label99.Location = new System.Drawing.Point(81, 61);
            this.label99.Name = "label99";
            this.label99.Size = new System.Drawing.Size(67, 15);
            this.label99.TabIndex = 95;
            this.label99.Text = "Название:";
            // 
            // TriggersGrid
            // 
            this.TriggersGrid.AllowUserToAddRows = false;
            this.TriggersGrid.AllowUserToDeleteRows = false;
            this.TriggersGrid.AllowUserToResizeColumns = false;
            this.TriggersGrid.AllowUserToResizeRows = false;
            this.TriggersGrid.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.TriggersGrid.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(15)))), ((int)(((byte)(15)))));
            this.TriggersGrid.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None;
            this.TriggersGrid.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle34.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle34.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle34.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            dataGridViewCellStyle34.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle34.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle34.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle34.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.TriggersGrid.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle34;
            this.TriggersGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.TriggersGrid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewTextBoxColumn15,
            this.dataGridViewTextBoxColumn16,
            this.dataGridViewTextBoxColumn21,
            this.dataGridViewTextBoxColumn17});
            this.TriggersGrid.ContextMenuStrip = this.TriggersContext;
            dataGridViewCellStyle38.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle38.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(15)))), ((int)(((byte)(15)))));
            dataGridViewCellStyle38.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            dataGridViewCellStyle38.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            dataGridViewCellStyle38.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle38.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle38.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.TriggersGrid.DefaultCellStyle = dataGridViewCellStyle38;
            this.TriggersGrid.EnableHeadersVisualStyles = false;
            this.TriggersGrid.Location = new System.Drawing.Point(1, 29);
            this.TriggersGrid.Name = "TriggersGrid";
            this.TriggersGrid.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            this.TriggersGrid.RowHeadersVisible = false;
            this.TriggersGrid.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.TriggersGrid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.TriggersGrid.ShowCellErrors = false;
            this.TriggersGrid.ShowCellToolTips = false;
            this.TriggersGrid.ShowEditingIcon = false;
            this.TriggersGrid.ShowRowErrors = false;
            this.TriggersGrid.Size = new System.Drawing.Size(359, 425);
            this.TriggersGrid.TabIndex = 18;
            this.TriggersGrid.CurrentCellChanged += new System.EventHandler(this.TriggersGrid_CurrentCellChanged);
            this.TriggersGrid.KeyDown += new System.Windows.Forms.KeyEventHandler(this.GridsKeyDown);
            // 
            // dataGridViewTextBoxColumn15
            // 
            dataGridViewCellStyle35.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.dataGridViewTextBoxColumn15.DefaultCellStyle = dataGridViewCellStyle35;
            this.dataGridViewTextBoxColumn15.FillWeight = 6.912442F;
            this.dataGridViewTextBoxColumn15.HeaderText = "#";
            this.dataGridViewTextBoxColumn15.Name = "dataGridViewTextBoxColumn15";
            this.dataGridViewTextBoxColumn15.ReadOnly = true;
            this.dataGridViewTextBoxColumn15.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.dataGridViewTextBoxColumn15.Width = 45;
            // 
            // dataGridViewTextBoxColumn16
            // 
            dataGridViewCellStyle36.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            dataGridViewCellStyle36.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(77)))), ((int)(((byte)(255)))), ((int)(((byte)(143)))));
            this.dataGridViewTextBoxColumn16.DefaultCellStyle = dataGridViewCellStyle36;
            this.dataGridViewTextBoxColumn16.FillWeight = 44.7233F;
            this.dataGridViewTextBoxColumn16.HeaderText = "  ID";
            this.dataGridViewTextBoxColumn16.Name = "dataGridViewTextBoxColumn16";
            this.dataGridViewTextBoxColumn16.ReadOnly = true;
            this.dataGridViewTextBoxColumn16.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.dataGridViewTextBoxColumn16.Width = 55;
            // 
            // dataGridViewTextBoxColumn21
            // 
            this.dataGridViewTextBoxColumn21.HeaderText = "GmId";
            this.dataGridViewTextBoxColumn21.Name = "dataGridViewTextBoxColumn21";
            this.dataGridViewTextBoxColumn21.Width = 55;
            // 
            // dataGridViewTextBoxColumn17
            // 
            dataGridViewCellStyle37.ForeColor = System.Drawing.Color.LightPink;
            this.dataGridViewTextBoxColumn17.DefaultCellStyle = dataGridViewCellStyle37;
            this.dataGridViewTextBoxColumn17.HeaderText = "Name";
            this.dataGridViewTextBoxColumn17.Name = "dataGridViewTextBoxColumn17";
            this.dataGridViewTextBoxColumn17.ReadOnly = true;
            this.dataGridViewTextBoxColumn17.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.dataGridViewTextBoxColumn17.Width = 220;
            // 
            // TriggersContext
            // 
            this.TriggersContext.AllowMerge = false;
            this.TriggersContext.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.DeleteEmptyTrigger,
            this.toolStripMenuItem4,
            this.toolStripSeparator3,
            this.toolStripMenuItem12,
            this.toolStripMenuItem11});
            this.TriggersContext.Name = "TriggersContext";
            this.TriggersContext.Size = new System.Drawing.Size(215, 98);
            this.TriggersContext.Text = "Дополнительно";
            // 
            // DeleteEmptyTrigger
            // 
            this.DeleteEmptyTrigger.Image = ((System.Drawing.Image)(resources.GetObject("DeleteEmptyTrigger.Image")));
            this.DeleteEmptyTrigger.Name = "DeleteEmptyTrigger";
            this.DeleteEmptyTrigger.Size = new System.Drawing.Size(214, 22);
            this.DeleteEmptyTrigger.Text = "Удалить пустые триггеры";
            this.DeleteEmptyTrigger.Click += new System.EventHandler(this.DeleteEmptyTrigger_Click);
            // 
            // toolStripMenuItem4
            // 
            this.toolStripMenuItem4.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.UpTrigger,
            this.DownTrigger});
            this.toolStripMenuItem4.Image = ((System.Drawing.Image)(resources.GetObject("toolStripMenuItem4.Image")));
            this.toolStripMenuItem4.Name = "toolStripMenuItem4";
            this.toolStripMenuItem4.Size = new System.Drawing.Size(214, 22);
            this.toolStripMenuItem4.Text = "Переместить";
            // 
            // UpTrigger
            // 
            this.UpTrigger.Image = ((System.Drawing.Image)(resources.GetObject("UpTrigger.Image")));
            this.UpTrigger.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.UpTrigger.Name = "UpTrigger";
            this.UpTrigger.Size = new System.Drawing.Size(180, 22);
            this.UpTrigger.Text = "Выше";
            this.UpTrigger.Click += new System.EventHandler(this.UpObjects);
            // 
            // DownTrigger
            // 
            this.DownTrigger.Image = ((System.Drawing.Image)(resources.GetObject("DownTrigger.Image")));
            this.DownTrigger.Name = "DownTrigger";
            this.DownTrigger.Size = new System.Drawing.Size(180, 22);
            this.DownTrigger.Text = "Ниже";
            this.DownTrigger.Click += new System.EventHandler(this.DownObjects);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(211, 6);
            // 
            // toolStripMenuItem12
            // 
            this.toolStripMenuItem12.Image = ((System.Drawing.Image)(resources.GetObject("toolStripMenuItem12.Image")));
            this.toolStripMenuItem12.Name = "toolStripMenuItem12";
            this.toolStripMenuItem12.Size = new System.Drawing.Size(214, 22);
            this.toolStripMenuItem12.Text = "Экспорт";
            this.toolStripMenuItem12.Click += new System.EventHandler(this.ExportExistence_Click);
            // 
            // toolStripMenuItem11
            // 
            this.toolStripMenuItem11.Image = ((System.Drawing.Image)(resources.GetObject("toolStripMenuItem11.Image")));
            this.toolStripMenuItem11.Name = "toolStripMenuItem11";
            this.toolStripMenuItem11.Size = new System.Drawing.Size(214, 22);
            this.toolStripMenuItem11.Text = "Импорт";
            this.toolStripMenuItem11.Click += new System.EventHandler(this.ImportExistence_Click);
            // 
            // toolStrip3
            // 
            this.toolStrip3.AutoSize = false;
            this.toolStrip3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(239)))), ((int)(((byte)(244)))), ((int)(((byte)(250)))));
            this.toolStrip3.GripMargin = new System.Windows.Forms.Padding(0);
            this.toolStrip3.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButton5,
            this.toolStripButton6,
            this.toolStripDropDownButton6,
            this.toolStripButton7});
            this.toolStrip3.Location = new System.Drawing.Point(3, 3);
            this.toolStrip3.Name = "toolStrip3";
            this.toolStrip3.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.toolStrip3.Size = new System.Drawing.Size(960, 25);
            this.toolStrip3.TabIndex = 32;
            this.toolStrip3.Text = "toolStrip3";
            // 
            // toolStripButton5
            // 
            this.toolStripButton5.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton5.Image")));
            this.toolStripButton5.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.toolStripButton5.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton5.Name = "toolStripButton5";
            this.toolStripButton5.Size = new System.Drawing.Size(74, 22);
            this.toolStripButton5.Text = "Экспорт";
            this.toolStripButton5.ToolTipText = "Export";
            this.toolStripButton5.Click += new System.EventHandler(this.ExportExistence_Click);
            // 
            // toolStripButton6
            // 
            this.toolStripButton6.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton6.Image")));
            this.toolStripButton6.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton6.Name = "toolStripButton6";
            this.toolStripButton6.Size = new System.Drawing.Size(71, 22);
            this.toolStripButton6.Text = "Импорт";
            this.toolStripButton6.ToolTipText = "Import";
            this.toolStripButton6.Click += new System.EventHandler(this.ImportExistence_Click);
            // 
            // toolStripDropDownButton6
            // 
            this.toolStripDropDownButton6.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem13,
            this.toolStripMenuItem14});
            this.toolStripDropDownButton6.Image = ((System.Drawing.Image)(resources.GetObject("toolStripDropDownButton6.Image")));
            this.toolStripDropDownButton6.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripDropDownButton6.Name = "toolStripDropDownButton6";
            this.toolStripDropDownButton6.Size = new System.Drawing.Size(108, 22);
            this.toolStripDropDownButton6.Text = "Переместить";
            this.toolStripDropDownButton6.ToolTipText = "Move";
            // 
            // toolStripMenuItem13
            // 
            this.toolStripMenuItem13.Image = ((System.Drawing.Image)(resources.GetObject("toolStripMenuItem13.Image")));
            this.toolStripMenuItem13.Name = "toolStripMenuItem13";
            this.toolStripMenuItem13.Size = new System.Drawing.Size(159, 22);
            this.toolStripMenuItem13.Text = "Выше   Shift+W";
            this.toolStripMenuItem13.Click += new System.EventHandler(this.UpObjects);
            // 
            // toolStripMenuItem14
            // 
            this.toolStripMenuItem14.Image = ((System.Drawing.Image)(resources.GetObject("toolStripMenuItem14.Image")));
            this.toolStripMenuItem14.Name = "toolStripMenuItem14";
            this.toolStripMenuItem14.Size = new System.Drawing.Size(159, 22);
            this.toolStripMenuItem14.Text = "Ниже    Shift+S";
            this.toolStripMenuItem14.Click += new System.EventHandler(this.DownObjects);
            // 
            // toolStripButton7
            // 
            this.toolStripButton7.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton7.Image")));
            this.toolStripButton7.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton7.Name = "toolStripButton7";
            this.toolStripButton7.Size = new System.Drawing.Size(79, 22);
            this.toolStripButton7.Text = "Очистить";
            this.toolStripButton7.ToolTipText = "Russian-: Удалить пустые триггеры\r\nEnglish-: Remove empty triggers";
            this.toolStripButton7.Click += new System.EventHandler(this.DeleteEmptyTrigger_Click);
            // 
            // TriggersRemoveButton
            // 
            this.TriggersRemoveButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.TriggersRemoveButton.ForeColor = System.Drawing.Color.Black;
            this.TriggersRemoveButton.Image = ((System.Drawing.Image)(resources.GetObject("TriggersRemoveButton.Image")));
            this.TriggersRemoveButton.Location = new System.Drawing.Point(180, 454);
            this.TriggersRemoveButton.Name = "TriggersRemoveButton";
            this.TriggersRemoveButton.Size = new System.Drawing.Size(181, 29);
            this.TriggersRemoveButton.TabIndex = 21;
            this.TriggersRemoveButton.Text = "Удалить";
            this.TriggersRemoveButton.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.TriggersRemoveButton.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.TriggersRemoveButton.UseVisualStyleBackColor = true;
            this.TriggersRemoveButton.Click += new System.EventHandler(this.DeleteTrigger_Click);
            // 
            // TriggersCloneButton
            // 
            this.TriggersCloneButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.TriggersCloneButton.ForeColor = System.Drawing.Color.Black;
            this.TriggersCloneButton.Image = ((System.Drawing.Image)(resources.GetObject("TriggersCloneButton.Image")));
            this.TriggersCloneButton.Location = new System.Drawing.Point(0, 454);
            this.TriggersCloneButton.Name = "TriggersCloneButton";
            this.TriggersCloneButton.Size = new System.Drawing.Size(181, 29);
            this.TriggersCloneButton.TabIndex = 20;
            this.TriggersCloneButton.Text = "Клонировать";
            this.TriggersCloneButton.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.TriggersCloneButton.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.TriggersCloneButton.UseVisualStyleBackColor = true;
            this.TriggersCloneButton.Click += new System.EventHandler(this.CloneTrigger_Click);
            // 
            // SearchTab
            // 
            this.SearchTab.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(239)))), ((int)(((byte)(244)))), ((int)(((byte)(250)))));
            this.SearchTab.Controls.Add(this.SearchGrid);
            this.SearchTab.Controls.Add(this.groupBox21);
            this.SearchTab.Controls.Add(this.groupBox20);
            this.SearchTab.Controls.Add(this.groupBox19);
            this.SearchTab.Controls.Add(this.groupBox18);
            this.SearchTab.Location = new System.Drawing.Point(4, 22);
            this.SearchTab.Name = "SearchTab";
            this.SearchTab.Padding = new System.Windows.Forms.Padding(3);
            this.SearchTab.Size = new System.Drawing.Size(966, 483);
            this.SearchTab.TabIndex = 5;
            this.SearchTab.Text = "Поиск";
            // 
            // SearchGrid
            // 
            this.SearchGrid.AllowUserToAddRows = false;
            this.SearchGrid.AllowUserToDeleteRows = false;
            this.SearchGrid.AllowUserToResizeColumns = false;
            this.SearchGrid.AllowUserToResizeRows = false;
            this.SearchGrid.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.SearchGrid.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(15)))), ((int)(((byte)(15)))));
            this.SearchGrid.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None;
            this.SearchGrid.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            this.SearchGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.SearchGrid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewTextBoxColumn18,
            this.dataGridViewTextBoxColumn19,
            this.dataGridViewTextBoxColumn20,
            this.Column2});
            this.SearchGrid.ContextMenuStrip = this.contextMenuStrip1;
            dataGridViewCellStyle42.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle42.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(15)))), ((int)(((byte)(15)))));
            dataGridViewCellStyle42.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            dataGridViewCellStyle42.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            dataGridViewCellStyle42.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle42.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle42.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.SearchGrid.DefaultCellStyle = dataGridViewCellStyle42;
            this.SearchGrid.EnableHeadersVisualStyles = false;
            this.SearchGrid.Location = new System.Drawing.Point(3, 157);
            this.SearchGrid.Name = "SearchGrid";
            this.SearchGrid.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            this.SearchGrid.RowHeadersVisible = false;
            this.SearchGrid.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.SearchGrid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.SearchGrid.ShowCellErrors = false;
            this.SearchGrid.ShowCellToolTips = false;
            this.SearchGrid.ShowEditingIcon = false;
            this.SearchGrid.ShowRowErrors = false;
            this.SearchGrid.Size = new System.Drawing.Size(960, 323);
            this.SearchGrid.TabIndex = 16;
            this.SearchGrid.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.SearchGrid_CellDoubleClick);
            // 
            // dataGridViewTextBoxColumn18
            // 
            dataGridViewCellStyle39.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.dataGridViewTextBoxColumn18.DefaultCellStyle = dataGridViewCellStyle39;
            this.dataGridViewTextBoxColumn18.FillWeight = 6.912442F;
            this.dataGridViewTextBoxColumn18.HeaderText = "#";
            this.dataGridViewTextBoxColumn18.Name = "dataGridViewTextBoxColumn18";
            this.dataGridViewTextBoxColumn18.ReadOnly = true;
            this.dataGridViewTextBoxColumn18.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridViewTextBoxColumn18.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.dataGridViewTextBoxColumn18.Width = 45;
            // 
            // dataGridViewTextBoxColumn19
            // 
            dataGridViewCellStyle40.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            dataGridViewCellStyle40.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(77)))), ((int)(((byte)(255)))), ((int)(((byte)(143)))));
            this.dataGridViewTextBoxColumn19.DefaultCellStyle = dataGridViewCellStyle40;
            this.dataGridViewTextBoxColumn19.FillWeight = 44.7233F;
            this.dataGridViewTextBoxColumn19.HeaderText = "  ID";
            this.dataGridViewTextBoxColumn19.Name = "dataGridViewTextBoxColumn19";
            this.dataGridViewTextBoxColumn19.ReadOnly = true;
            this.dataGridViewTextBoxColumn19.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridViewTextBoxColumn19.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.dataGridViewTextBoxColumn19.Width = 315;
            // 
            // dataGridViewTextBoxColumn20
            // 
            dataGridViewCellStyle41.ForeColor = System.Drawing.Color.White;
            this.dataGridViewTextBoxColumn20.DefaultCellStyle = dataGridViewCellStyle41;
            this.dataGridViewTextBoxColumn20.HeaderText = "Name";
            this.dataGridViewTextBoxColumn20.Name = "dataGridViewTextBoxColumn20";
            this.dataGridViewTextBoxColumn20.ReadOnly = true;
            this.dataGridViewTextBoxColumn20.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridViewTextBoxColumn20.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.dataGridViewTextBoxColumn20.Width = 450;
            // 
            // Column2
            // 
            this.Column2.HeaderText = "Section";
            this.Column2.Name = "Column2";
            this.Column2.ReadOnly = true;
            this.Column2.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.Column2.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.Column2.Width = 150;
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MoveToSelected});
            this.contextMenuStrip1.Name = "TriggersContext";
            this.contextMenuStrip1.Size = new System.Drawing.Size(205, 26);
            this.contextMenuStrip1.Text = "Дополнительно";
            // 
            // MoveToSelected
            // 
            this.MoveToSelected.Name = "MoveToSelected";
            this.MoveToSelected.Size = new System.Drawing.Size(204, 22);
            this.MoveToSelected.Text = "Перейти к выбранному";
            this.MoveToSelected.Click += new System.EventHandler(this.MoveToSelected_Click);
            // 
            // groupBox21
            // 
            this.groupBox21.Controls.Add(this.TriggerSearchButton);
            this.groupBox21.Controls.Add(this.TriggerSearchName);
            this.groupBox21.Controls.Add(this.TriggerSearchName_Radio);
            this.groupBox21.Controls.Add(this.TriggerSearchGmID);
            this.groupBox21.Controls.Add(this.label107);
            this.groupBox21.Controls.Add(this.TriggerSearchId_Radio);
            this.groupBox21.Controls.Add(this.TriggerSearchID);
            this.groupBox21.Controls.Add(this.label108);
            this.groupBox21.Controls.Add(this.TriggerSearchGmId_Radio);
            this.groupBox21.Location = new System.Drawing.Point(726, 1);
            this.groupBox21.Name = "groupBox21";
            this.groupBox21.Size = new System.Drawing.Size(235, 154);
            this.groupBox21.TabIndex = 12;
            this.groupBox21.TabStop = false;
            this.groupBox21.Text = "Поиск в Триггерах";
            // 
            // TriggerSearchButton
            // 
            this.TriggerSearchButton.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.TriggerSearchButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.TriggerSearchButton.ForeColor = System.Drawing.Color.Black;
            this.TriggerSearchButton.Image = ((System.Drawing.Image)(resources.GetObject("TriggerSearchButton.Image")));
            this.TriggerSearchButton.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.TriggerSearchButton.Location = new System.Drawing.Point(1, 107);
            this.TriggerSearchButton.Name = "TriggerSearchButton";
            this.TriggerSearchButton.Size = new System.Drawing.Size(234, 45);
            this.TriggerSearchButton.TabIndex = 31;
            this.TriggerSearchButton.Text = "Найти";
            this.TriggerSearchButton.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.TriggerSearchButton.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.TriggerSearchButton.UseVisualStyleBackColor = true;
            this.TriggerSearchButton.Click += new System.EventHandler(this.TriggerSearchButton_Click);
            // 
            // TriggerSearchName
            // 
            this.TriggerSearchName.Location = new System.Drawing.Point(80, 64);
            this.TriggerSearchName.Name = "TriggerSearchName";
            this.TriggerSearchName.Size = new System.Drawing.Size(151, 20);
            this.TriggerSearchName.TabIndex = 6;
            this.TriggerSearchName.Click += new System.EventHandler(this.MainTabControl_Click);
            // 
            // TriggerSearchName_Radio
            // 
            this.TriggerSearchName_Radio.AutoSize = true;
            this.TriggerSearchName_Radio.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.TriggerSearchName_Radio.FlatAppearance.CheckedBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.TriggerSearchName_Radio.Location = new System.Drawing.Point(1, 64);
            this.TriggerSearchName_Radio.Name = "TriggerSearchName_Radio";
            this.TriggerSearchName_Radio.Size = new System.Drawing.Size(75, 17);
            this.TriggerSearchName_Radio.TabIndex = 7;
            this.TriggerSearchName_Radio.Text = "Название";
            this.TriggerSearchName_Radio.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.TriggerSearchName_Radio.UseVisualStyleBackColor = true;
            // 
            // TriggerSearchGmID
            // 
            this.TriggerSearchGmID.Location = new System.Drawing.Point(80, 42);
            this.TriggerSearchGmID.Name = "TriggerSearchGmID";
            this.TriggerSearchGmID.Size = new System.Drawing.Size(151, 20);
            this.TriggerSearchGmID.TabIndex = 4;
            this.TriggerSearchGmID.Click += new System.EventHandler(this.MainTabControl_Click);
            this.TriggerSearchGmID.TextChanged += new System.EventHandler(this.TriggerSearchGmID_TextChanged);
            // 
            // label107
            // 
            this.label107.AutoSize = true;
            this.label107.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label107.Location = new System.Drawing.Point(153, 23);
            this.label107.Name = "label107";
            this.label107.Size = new System.Drawing.Size(0, 13);
            this.label107.TabIndex = 3;
            // 
            // TriggerSearchId_Radio
            // 
            this.TriggerSearchId_Radio.AutoSize = true;
            this.TriggerSearchId_Radio.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.TriggerSearchId_Radio.Checked = true;
            this.TriggerSearchId_Radio.FlatAppearance.CheckedBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.TriggerSearchId_Radio.Location = new System.Drawing.Point(40, 20);
            this.TriggerSearchId_Radio.Name = "TriggerSearchId_Radio";
            this.TriggerSearchId_Radio.Size = new System.Drawing.Size(36, 17);
            this.TriggerSearchId_Radio.TabIndex = 1;
            this.TriggerSearchId_Radio.TabStop = true;
            this.TriggerSearchId_Radio.Text = "ID";
            this.TriggerSearchId_Radio.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.TriggerSearchId_Radio.UseVisualStyleBackColor = true;
            // 
            // TriggerSearchID
            // 
            this.TriggerSearchID.BackColor = System.Drawing.Color.WhiteSmoke;
            this.TriggerSearchID.Location = new System.Drawing.Point(80, 20);
            this.TriggerSearchID.Name = "TriggerSearchID";
            this.TriggerSearchID.Size = new System.Drawing.Size(67, 20);
            this.TriggerSearchID.TabIndex = 0;
            this.TriggerSearchID.Text = "0";
            this.toolTip1.SetToolTip(this.TriggerSearchID, "Double click to show elements window");
            this.TriggerSearchID.Click += new System.EventHandler(this.MainTabControl_Click);
            this.TriggerSearchID.TextChanged += new System.EventHandler(this.TriggerSearchID_TextChanged);
            // 
            // label108
            // 
            this.label108.AutoSize = true;
            this.label108.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label108.Location = new System.Drawing.Point(144, 18);
            this.label108.Name = "label108";
            this.label108.Size = new System.Drawing.Size(14, 20);
            this.label108.TabIndex = 2;
            this.label108.Text = "|";
            // 
            // TriggerSearchGmId_Radio
            // 
            this.TriggerSearchGmId_Radio.AutoSize = true;
            this.TriggerSearchGmId_Radio.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.TriggerSearchGmId_Radio.FlatAppearance.CheckedBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.TriggerSearchGmId_Radio.Location = new System.Drawing.Point(24, 43);
            this.TriggerSearchGmId_Radio.Name = "TriggerSearchGmId_Radio";
            this.TriggerSearchGmId_Radio.Size = new System.Drawing.Size(52, 17);
            this.TriggerSearchGmId_Radio.TabIndex = 5;
            this.TriggerSearchGmId_Radio.Text = "GmID";
            this.TriggerSearchGmId_Radio.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.TriggerSearchGmId_Radio.UseVisualStyleBackColor = true;
            // 
            // groupBox20
            // 
            this.groupBox20.Controls.Add(this.DynamicSearchButton);
            this.groupBox20.Controls.Add(this.DynamicSearchTrigger);
            this.groupBox20.Controls.Add(this.DynamicSearchTrigger_Radio);
            this.groupBox20.Controls.Add(this.DynamicSearchName);
            this.groupBox20.Controls.Add(this.label105);
            this.groupBox20.Controls.Add(this.DynamicSearchId_Radio);
            this.groupBox20.Controls.Add(this.DynamicSearchId);
            this.groupBox20.Controls.Add(this.label106);
            this.groupBox20.Controls.Add(this.DynamicSearchName_Radio);
            this.groupBox20.Location = new System.Drawing.Point(485, 1);
            this.groupBox20.Name = "groupBox20";
            this.groupBox20.Size = new System.Drawing.Size(235, 154);
            this.groupBox20.TabIndex = 11;
            this.groupBox20.TabStop = false;
            this.groupBox20.Text = "Поиск в Дин.Объектах";
            // 
            // DynamicSearchButton
            // 
            this.DynamicSearchButton.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.DynamicSearchButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.DynamicSearchButton.ForeColor = System.Drawing.Color.Black;
            this.DynamicSearchButton.Image = ((System.Drawing.Image)(resources.GetObject("DynamicSearchButton.Image")));
            this.DynamicSearchButton.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.DynamicSearchButton.Location = new System.Drawing.Point(0, 107);
            this.DynamicSearchButton.Name = "DynamicSearchButton";
            this.DynamicSearchButton.Size = new System.Drawing.Size(234, 45);
            this.DynamicSearchButton.TabIndex = 30;
            this.DynamicSearchButton.Text = "Найти";
            this.DynamicSearchButton.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.DynamicSearchButton.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.DynamicSearchButton.UseVisualStyleBackColor = true;
            this.DynamicSearchButton.Click += new System.EventHandler(this.DynamicSearchButton_Click);
            // 
            // DynamicSearchTrigger
            // 
            this.DynamicSearchTrigger.Location = new System.Drawing.Point(80, 64);
            this.DynamicSearchTrigger.Name = "DynamicSearchTrigger";
            this.DynamicSearchTrigger.Size = new System.Drawing.Size(151, 20);
            this.DynamicSearchTrigger.TabIndex = 6;
            this.DynamicSearchTrigger.Click += new System.EventHandler(this.MainTabControl_Click);
            // 
            // DynamicSearchTrigger_Radio
            // 
            this.DynamicSearchTrigger_Radio.AutoSize = true;
            this.DynamicSearchTrigger_Radio.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.DynamicSearchTrigger_Radio.FlatAppearance.CheckedBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.DynamicSearchTrigger_Radio.Location = new System.Drawing.Point(10, 64);
            this.DynamicSearchTrigger_Radio.Name = "DynamicSearchTrigger_Radio";
            this.DynamicSearchTrigger_Radio.Size = new System.Drawing.Size(66, 17);
            this.DynamicSearchTrigger_Radio.TabIndex = 7;
            this.DynamicSearchTrigger_Radio.Text = "Триггер";
            this.DynamicSearchTrigger_Radio.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.DynamicSearchTrigger_Radio.UseVisualStyleBackColor = true;
            // 
            // DynamicSearchName
            // 
            this.DynamicSearchName.Location = new System.Drawing.Point(80, 42);
            this.DynamicSearchName.Name = "DynamicSearchName";
            this.DynamicSearchName.Size = new System.Drawing.Size(151, 20);
            this.DynamicSearchName.TabIndex = 4;
            this.DynamicSearchName.Click += new System.EventHandler(this.MainTabControl_Click);
            // 
            // label105
            // 
            this.label105.AutoSize = true;
            this.label105.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label105.Location = new System.Drawing.Point(153, 23);
            this.label105.Name = "label105";
            this.label105.Size = new System.Drawing.Size(0, 13);
            this.label105.TabIndex = 3;
            // 
            // DynamicSearchId_Radio
            // 
            this.DynamicSearchId_Radio.AutoSize = true;
            this.DynamicSearchId_Radio.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.DynamicSearchId_Radio.Checked = true;
            this.DynamicSearchId_Radio.FlatAppearance.CheckedBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.DynamicSearchId_Radio.Location = new System.Drawing.Point(40, 20);
            this.DynamicSearchId_Radio.Name = "DynamicSearchId_Radio";
            this.DynamicSearchId_Radio.Size = new System.Drawing.Size(36, 17);
            this.DynamicSearchId_Radio.TabIndex = 1;
            this.DynamicSearchId_Radio.TabStop = true;
            this.DynamicSearchId_Radio.Text = "ID";
            this.DynamicSearchId_Radio.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.DynamicSearchId_Radio.UseVisualStyleBackColor = true;
            // 
            // DynamicSearchId
            // 
            this.DynamicSearchId.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.DynamicSearchId.Location = new System.Drawing.Point(80, 20);
            this.DynamicSearchId.Name = "DynamicSearchId";
            this.DynamicSearchId.Size = new System.Drawing.Size(67, 20);
            this.DynamicSearchId.TabIndex = 0;
            this.DynamicSearchId.Text = "0";
            this.toolTip1.SetToolTip(this.DynamicSearchId, "Double click to show dynamic objects window");
            this.DynamicSearchId.Click += new System.EventHandler(this.MainTabControl_Click);
            this.DynamicSearchId.TextChanged += new System.EventHandler(this.DynamicSearchId_TextChanged);
            this.DynamicSearchId.DoubleClick += new System.EventHandler(this.DynamicSearchId_DoubleClick);
            // 
            // label106
            // 
            this.label106.AutoSize = true;
            this.label106.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label106.Location = new System.Drawing.Point(144, 18);
            this.label106.Name = "label106";
            this.label106.Size = new System.Drawing.Size(14, 20);
            this.label106.TabIndex = 2;
            this.label106.Text = "|";
            // 
            // DynamicSearchName_Radio
            // 
            this.DynamicSearchName_Radio.AutoSize = true;
            this.DynamicSearchName_Radio.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.DynamicSearchName_Radio.FlatAppearance.CheckedBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.DynamicSearchName_Radio.Location = new System.Drawing.Point(1, 43);
            this.DynamicSearchName_Radio.Name = "DynamicSearchName_Radio";
            this.DynamicSearchName_Radio.Size = new System.Drawing.Size(75, 17);
            this.DynamicSearchName_Radio.TabIndex = 5;
            this.DynamicSearchName_Radio.Text = "Название";
            this.DynamicSearchName_Radio.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.DynamicSearchName_Radio.UseVisualStyleBackColor = true;
            // 
            // groupBox19
            // 
            this.groupBox19.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(239)))), ((int)(((byte)(244)))), ((int)(((byte)(250)))));
            this.groupBox19.Controls.Add(this.ResourceSearchButton);
            this.groupBox19.Controls.Add(this.ResourceSearchTrigger);
            this.groupBox19.Controls.Add(this.ResourceSearchTrigger_Radio);
            this.groupBox19.Controls.Add(this.ResourceSearchName);
            this.groupBox19.Controls.Add(this.label103);
            this.groupBox19.Controls.Add(this.ResourceSearchId_Radio);
            this.groupBox19.Controls.Add(this.ResourceSearchId);
            this.groupBox19.Controls.Add(this.label104);
            this.groupBox19.Controls.Add(this.ResourceSearchName_Radio);
            this.groupBox19.Location = new System.Drawing.Point(244, 1);
            this.groupBox19.Name = "groupBox19";
            this.groupBox19.Size = new System.Drawing.Size(235, 154);
            this.groupBox19.TabIndex = 10;
            this.groupBox19.TabStop = false;
            this.groupBox19.Text = "Поиск в ресурсах";
            // 
            // ResourceSearchButton
            // 
            this.ResourceSearchButton.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.ResourceSearchButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.ResourceSearchButton.ForeColor = System.Drawing.Color.Black;
            this.ResourceSearchButton.Image = ((System.Drawing.Image)(resources.GetObject("ResourceSearchButton.Image")));
            this.ResourceSearchButton.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.ResourceSearchButton.Location = new System.Drawing.Point(1, 107);
            this.ResourceSearchButton.Name = "ResourceSearchButton";
            this.ResourceSearchButton.Size = new System.Drawing.Size(234, 45);
            this.ResourceSearchButton.TabIndex = 29;
            this.ResourceSearchButton.Text = "Найти";
            this.ResourceSearchButton.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.ResourceSearchButton.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.ResourceSearchButton.UseVisualStyleBackColor = true;
            this.ResourceSearchButton.Click += new System.EventHandler(this.ResourceSearchButton_Click);
            // 
            // ResourceSearchTrigger
            // 
            this.ResourceSearchTrigger.Location = new System.Drawing.Point(80, 64);
            this.ResourceSearchTrigger.Name = "ResourceSearchTrigger";
            this.ResourceSearchTrigger.Size = new System.Drawing.Size(151, 20);
            this.ResourceSearchTrigger.TabIndex = 6;
            this.ResourceSearchTrigger.Click += new System.EventHandler(this.MainTabControl_Click);
            // 
            // ResourceSearchTrigger_Radio
            // 
            this.ResourceSearchTrigger_Radio.AutoSize = true;
            this.ResourceSearchTrigger_Radio.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.ResourceSearchTrigger_Radio.FlatAppearance.CheckedBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.ResourceSearchTrigger_Radio.Location = new System.Drawing.Point(10, 64);
            this.ResourceSearchTrigger_Radio.Name = "ResourceSearchTrigger_Radio";
            this.ResourceSearchTrigger_Radio.Size = new System.Drawing.Size(66, 17);
            this.ResourceSearchTrigger_Radio.TabIndex = 7;
            this.ResourceSearchTrigger_Radio.Text = "Триггер";
            this.ResourceSearchTrigger_Radio.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.ResourceSearchTrigger_Radio.UseVisualStyleBackColor = true;
            // 
            // ResourceSearchName
            // 
            this.ResourceSearchName.Location = new System.Drawing.Point(80, 42);
            this.ResourceSearchName.Name = "ResourceSearchName";
            this.ResourceSearchName.Size = new System.Drawing.Size(151, 20);
            this.ResourceSearchName.TabIndex = 4;
            this.ResourceSearchName.Click += new System.EventHandler(this.MainTabControl_Click);
            // 
            // label103
            // 
            this.label103.AutoSize = true;
            this.label103.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label103.Location = new System.Drawing.Point(153, 23);
            this.label103.Name = "label103";
            this.label103.Size = new System.Drawing.Size(0, 13);
            this.label103.TabIndex = 3;
            // 
            // ResourceSearchId_Radio
            // 
            this.ResourceSearchId_Radio.AutoSize = true;
            this.ResourceSearchId_Radio.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.ResourceSearchId_Radio.Checked = true;
            this.ResourceSearchId_Radio.FlatAppearance.CheckedBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.ResourceSearchId_Radio.Location = new System.Drawing.Point(40, 20);
            this.ResourceSearchId_Radio.Name = "ResourceSearchId_Radio";
            this.ResourceSearchId_Radio.Size = new System.Drawing.Size(36, 17);
            this.ResourceSearchId_Radio.TabIndex = 1;
            this.ResourceSearchId_Radio.TabStop = true;
            this.ResourceSearchId_Radio.Text = "ID";
            this.ResourceSearchId_Radio.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.ResourceSearchId_Radio.UseVisualStyleBackColor = true;
            // 
            // ResourceSearchId
            // 
            this.ResourceSearchId.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.ResourceSearchId.Location = new System.Drawing.Point(80, 20);
            this.ResourceSearchId.Name = "ResourceSearchId";
            this.ResourceSearchId.Size = new System.Drawing.Size(67, 20);
            this.ResourceSearchId.TabIndex = 0;
            this.ResourceSearchId.Text = "0";
            this.toolTip1.SetToolTip(this.ResourceSearchId, "Double click to show elements window");
            this.ResourceSearchId.Click += new System.EventHandler(this.MainTabControl_Click);
            this.ResourceSearchId.TextChanged += new System.EventHandler(this.ResourceSearchId_TextChanged);
            this.ResourceSearchId.DoubleClick += new System.EventHandler(this.ResourceSearchId_DoubleClick);
            // 
            // label104
            // 
            this.label104.AutoSize = true;
            this.label104.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label104.Location = new System.Drawing.Point(144, 18);
            this.label104.Name = "label104";
            this.label104.Size = new System.Drawing.Size(14, 20);
            this.label104.TabIndex = 2;
            this.label104.Text = "|";
            // 
            // ResourceSearchName_Radio
            // 
            this.ResourceSearchName_Radio.AutoSize = true;
            this.ResourceSearchName_Radio.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.ResourceSearchName_Radio.FlatAppearance.CheckedBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.ResourceSearchName_Radio.Location = new System.Drawing.Point(1, 43);
            this.ResourceSearchName_Radio.Name = "ResourceSearchName_Radio";
            this.ResourceSearchName_Radio.Size = new System.Drawing.Size(75, 17);
            this.ResourceSearchName_Radio.TabIndex = 5;
            this.ResourceSearchName_Radio.Text = "Название";
            this.ResourceSearchName_Radio.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.ResourceSearchName_Radio.UseVisualStyleBackColor = true;
            // 
            // groupBox18
            // 
            this.groupBox18.Controls.Add(this.ExistenceSearchButton);
            this.groupBox18.Controls.Add(this.ExistenceSearchPath);
            this.groupBox18.Controls.Add(this.ExistenceSearchPath_Radio);
            this.groupBox18.Controls.Add(this.ExistenceSearchTrigger);
            this.groupBox18.Controls.Add(this.ExistenceSearchTrigger_Radio);
            this.groupBox18.Controls.Add(this.ExistenceSearchName);
            this.groupBox18.Controls.Add(this.label102);
            this.groupBox18.Controls.Add(this.ExistenceSearchId_Radio);
            this.groupBox18.Controls.Add(this.ExistenceSearchId);
            this.groupBox18.Controls.Add(this.label76);
            this.groupBox18.Controls.Add(this.ExistenceSearchName_Radio);
            this.groupBox18.Location = new System.Drawing.Point(3, 1);
            this.groupBox18.Name = "groupBox18";
            this.groupBox18.Size = new System.Drawing.Size(235, 154);
            this.groupBox18.TabIndex = 0;
            this.groupBox18.TabStop = false;
            this.groupBox18.Text = "Поиск в существах";
            // 
            // ExistenceSearchButton
            // 
            this.ExistenceSearchButton.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.ExistenceSearchButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.ExistenceSearchButton.ForeColor = System.Drawing.Color.Black;
            this.ExistenceSearchButton.Image = ((System.Drawing.Image)(resources.GetObject("ExistenceSearchButton.Image")));
            this.ExistenceSearchButton.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.ExistenceSearchButton.Location = new System.Drawing.Point(0, 107);
            this.ExistenceSearchButton.Name = "ExistenceSearchButton";
            this.ExistenceSearchButton.Size = new System.Drawing.Size(234, 45);
            this.ExistenceSearchButton.TabIndex = 28;
            this.ExistenceSearchButton.Text = "Найти";
            this.ExistenceSearchButton.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.ExistenceSearchButton.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.ExistenceSearchButton.UseVisualStyleBackColor = true;
            this.ExistenceSearchButton.Click += new System.EventHandler(this.ExistenceSearchButton_Click);
            // 
            // ExistenceSearchPath
            // 
            this.ExistenceSearchPath.Location = new System.Drawing.Point(80, 86);
            this.ExistenceSearchPath.Name = "ExistenceSearchPath";
            this.ExistenceSearchPath.Size = new System.Drawing.Size(151, 20);
            this.ExistenceSearchPath.TabIndex = 8;
            this.ExistenceSearchPath.Click += new System.EventHandler(this.MainTabControl_Click);
            // 
            // ExistenceSearchPath_Radio
            // 
            this.ExistenceSearchPath_Radio.AutoSize = true;
            this.ExistenceSearchPath_Radio.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.ExistenceSearchPath_Radio.FlatAppearance.CheckedBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.ExistenceSearchPath_Radio.Location = new System.Drawing.Point(27, 86);
            this.ExistenceSearchPath_Radio.Name = "ExistenceSearchPath_Radio";
            this.ExistenceSearchPath_Radio.Size = new System.Drawing.Size(49, 17);
            this.ExistenceSearchPath_Radio.TabIndex = 9;
            this.ExistenceSearchPath_Radio.Text = "Путь";
            this.ExistenceSearchPath_Radio.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.ExistenceSearchPath_Radio.UseVisualStyleBackColor = true;
            // 
            // ExistenceSearchTrigger
            // 
            this.ExistenceSearchTrigger.Location = new System.Drawing.Point(80, 64);
            this.ExistenceSearchTrigger.Name = "ExistenceSearchTrigger";
            this.ExistenceSearchTrigger.Size = new System.Drawing.Size(151, 20);
            this.ExistenceSearchTrigger.TabIndex = 6;
            this.ExistenceSearchTrigger.Click += new System.EventHandler(this.MainTabControl_Click);
            // 
            // ExistenceSearchTrigger_Radio
            // 
            this.ExistenceSearchTrigger_Radio.AutoSize = true;
            this.ExistenceSearchTrigger_Radio.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.ExistenceSearchTrigger_Radio.FlatAppearance.CheckedBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.ExistenceSearchTrigger_Radio.Location = new System.Drawing.Point(10, 64);
            this.ExistenceSearchTrigger_Radio.Name = "ExistenceSearchTrigger_Radio";
            this.ExistenceSearchTrigger_Radio.Size = new System.Drawing.Size(66, 17);
            this.ExistenceSearchTrigger_Radio.TabIndex = 7;
            this.ExistenceSearchTrigger_Radio.Text = "Триггер";
            this.ExistenceSearchTrigger_Radio.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.ExistenceSearchTrigger_Radio.UseVisualStyleBackColor = true;
            // 
            // ExistenceSearchName
            // 
            this.ExistenceSearchName.Location = new System.Drawing.Point(80, 42);
            this.ExistenceSearchName.Name = "ExistenceSearchName";
            this.ExistenceSearchName.Size = new System.Drawing.Size(151, 20);
            this.ExistenceSearchName.TabIndex = 4;
            this.ExistenceSearchName.Click += new System.EventHandler(this.MainTabControl_Click);
            // 
            // label102
            // 
            this.label102.AutoSize = true;
            this.label102.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label102.Location = new System.Drawing.Point(153, 23);
            this.label102.Name = "label102";
            this.label102.Size = new System.Drawing.Size(0, 13);
            this.label102.TabIndex = 3;
            // 
            // ExistenceSearchId_Radio
            // 
            this.ExistenceSearchId_Radio.AutoSize = true;
            this.ExistenceSearchId_Radio.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.ExistenceSearchId_Radio.Checked = true;
            this.ExistenceSearchId_Radio.FlatAppearance.CheckedBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.ExistenceSearchId_Radio.Location = new System.Drawing.Point(40, 20);
            this.ExistenceSearchId_Radio.Name = "ExistenceSearchId_Radio";
            this.ExistenceSearchId_Radio.Size = new System.Drawing.Size(36, 17);
            this.ExistenceSearchId_Radio.TabIndex = 1;
            this.ExistenceSearchId_Radio.TabStop = true;
            this.ExistenceSearchId_Radio.Text = "ID";
            this.ExistenceSearchId_Radio.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.ExistenceSearchId_Radio.UseVisualStyleBackColor = true;
            // 
            // ExistenceSearchId
            // 
            this.ExistenceSearchId.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.ExistenceSearchId.Location = new System.Drawing.Point(80, 20);
            this.ExistenceSearchId.Name = "ExistenceSearchId";
            this.ExistenceSearchId.Size = new System.Drawing.Size(67, 20);
            this.ExistenceSearchId.TabIndex = 0;
            this.ExistenceSearchId.Text = "0";
            this.toolTip1.SetToolTip(this.ExistenceSearchId, "Double click to show elements window");
            this.ExistenceSearchId.Click += new System.EventHandler(this.MainTabControl_Click);
            this.ExistenceSearchId.TextChanged += new System.EventHandler(this.ExistenceSearchId_TextChanged);
            this.ExistenceSearchId.DoubleClick += new System.EventHandler(this.ExistenceSearchId_DoubleClick);
            // 
            // label76
            // 
            this.label76.AutoSize = true;
            this.label76.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label76.Location = new System.Drawing.Point(144, 18);
            this.label76.Name = "label76";
            this.label76.Size = new System.Drawing.Size(14, 20);
            this.label76.TabIndex = 2;
            this.label76.Text = "|";
            // 
            // ExistenceSearchName_Radio
            // 
            this.ExistenceSearchName_Radio.AutoSize = true;
            this.ExistenceSearchName_Radio.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.ExistenceSearchName_Radio.FlatAppearance.CheckedBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.ExistenceSearchName_Radio.Location = new System.Drawing.Point(1, 43);
            this.ExistenceSearchName_Radio.Name = "ExistenceSearchName_Radio";
            this.ExistenceSearchName_Radio.Size = new System.Drawing.Size(75, 17);
            this.ExistenceSearchName_Radio.TabIndex = 5;
            this.ExistenceSearchName_Radio.Text = "Название";
            this.ExistenceSearchName_Radio.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.ExistenceSearchName_Radio.UseVisualStyleBackColor = true;
            // 
            // ErrorsTab
            // 
            this.ErrorsTab.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(239)))), ((int)(((byte)(244)))), ((int)(((byte)(250)))));
            this.ErrorsTab.Controls.Add(this.RemoveAllErrors);
            this.ErrorsTab.Controls.Add(this.SearchErrorsButton);
            this.ErrorsTab.Controls.Add(this.ErrorsGrid);
            this.ErrorsTab.Location = new System.Drawing.Point(4, 22);
            this.ErrorsTab.Name = "ErrorsTab";
            this.ErrorsTab.Padding = new System.Windows.Forms.Padding(3);
            this.ErrorsTab.Size = new System.Drawing.Size(966, 483);
            this.ErrorsTab.TabIndex = 6;
            this.ErrorsTab.Text = "Ошибки";
            // 
            // RemoveAllErrors
            // 
            this.RemoveAllErrors.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.75F);
            this.RemoveAllErrors.ForeColor = System.Drawing.Color.Black;
            this.RemoveAllErrors.Image = ((System.Drawing.Image)(resources.GetObject("RemoveAllErrors.Image")));
            this.RemoveAllErrors.Location = new System.Drawing.Point(187, 428);
            this.RemoveAllErrors.Name = "RemoveAllErrors";
            this.RemoveAllErrors.Size = new System.Drawing.Size(249, 54);
            this.RemoveAllErrors.TabIndex = 18;
            this.RemoveAllErrors.Text = "Удалить все объекты";
            this.RemoveAllErrors.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.RemoveAllErrors.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.RemoveAllErrors.UseVisualStyleBackColor = true;
            this.RemoveAllErrors.Click += new System.EventHandler(this.RemoveAllErrors_Click);
            // 
            // SearchErrorsButton
            // 
            this.SearchErrorsButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.75F);
            this.SearchErrorsButton.ForeColor = System.Drawing.Color.Black;
            this.SearchErrorsButton.Image = ((System.Drawing.Image)(resources.GetObject("SearchErrorsButton.Image")));
            this.SearchErrorsButton.Location = new System.Drawing.Point(0, 428);
            this.SearchErrorsButton.Name = "SearchErrorsButton";
            this.SearchErrorsButton.Size = new System.Drawing.Size(186, 54);
            this.SearchErrorsButton.TabIndex = 17;
            this.SearchErrorsButton.Text = "Найти ошибки";
            this.SearchErrorsButton.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.SearchErrorsButton.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.SearchErrorsButton.UseVisualStyleBackColor = true;
            this.SearchErrorsButton.Click += new System.EventHandler(this.SearchErrorsButton_Click);
            // 
            // ErrorsGrid
            // 
            this.ErrorsGrid.AllowUserToAddRows = false;
            this.ErrorsGrid.AllowUserToDeleteRows = false;
            this.ErrorsGrid.AllowUserToResizeColumns = false;
            this.ErrorsGrid.AllowUserToResizeRows = false;
            this.ErrorsGrid.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.ErrorsGrid.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(15)))), ((int)(((byte)(15)))));
            this.ErrorsGrid.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None;
            this.ErrorsGrid.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle43.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle43.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle43.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            dataGridViewCellStyle43.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle43.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle43.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle43.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.ErrorsGrid.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle43;
            this.ErrorsGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.ErrorsGrid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column3,
            this.dataGridViewTextBoxColumn31,
            this.dataGridViewTextBoxColumn32,
            this.dataGridViewTextBoxColumn33});
            dataGridViewCellStyle46.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle46.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(15)))), ((int)(((byte)(15)))));
            dataGridViewCellStyle46.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            dataGridViewCellStyle46.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle46.SelectionBackColor = System.Drawing.Color.BlueViolet;
            dataGridViewCellStyle46.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle46.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.ErrorsGrid.DefaultCellStyle = dataGridViewCellStyle46;
            this.ErrorsGrid.EnableHeadersVisualStyles = false;
            this.ErrorsGrid.Location = new System.Drawing.Point(0, 0);
            this.ErrorsGrid.Name = "ErrorsGrid";
            this.ErrorsGrid.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            this.ErrorsGrid.RowHeadersVisible = false;
            this.ErrorsGrid.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.ErrorsGrid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.ErrorsGrid.ShowCellErrors = false;
            this.ErrorsGrid.ShowCellToolTips = false;
            this.ErrorsGrid.ShowEditingIcon = false;
            this.ErrorsGrid.ShowRowErrors = false;
            this.ErrorsGrid.Size = new System.Drawing.Size(966, 427);
            this.ErrorsGrid.TabIndex = 16;
            this.ErrorsGrid.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.ErrorsGrid_CellDoubleClick);
            // 
            // Column3
            // 
            this.Column3.HeaderText = "#";
            this.Column3.Name = "Column3";
            this.Column3.Width = 45;
            // 
            // dataGridViewTextBoxColumn31
            // 
            dataGridViewCellStyle44.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(77)))), ((int)(((byte)(255)))), ((int)(((byte)(143)))));
            this.dataGridViewTextBoxColumn31.DefaultCellStyle = dataGridViewCellStyle44;
            this.dataGridViewTextBoxColumn31.FillWeight = 6.912442F;
            this.dataGridViewTextBoxColumn31.HeaderText = "№";
            this.dataGridViewTextBoxColumn31.Name = "dataGridViewTextBoxColumn31";
            this.dataGridViewTextBoxColumn31.ReadOnly = true;
            this.dataGridViewTextBoxColumn31.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridViewTextBoxColumn31.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.dataGridViewTextBoxColumn31.Width = 50;
            // 
            // dataGridViewTextBoxColumn32
            // 
            dataGridViewCellStyle45.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(77)))), ((int)(((byte)(255)))), ((int)(((byte)(143)))));
            this.dataGridViewTextBoxColumn32.DefaultCellStyle = dataGridViewCellStyle45;
            this.dataGridViewTextBoxColumn32.FillWeight = 44.7233F;
            this.dataGridViewTextBoxColumn32.HeaderText = "Section";
            this.dataGridViewTextBoxColumn32.Name = "dataGridViewTextBoxColumn32";
            this.dataGridViewTextBoxColumn32.ReadOnly = true;
            this.dataGridViewTextBoxColumn32.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridViewTextBoxColumn32.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.dataGridViewTextBoxColumn32.Width = 200;
            // 
            // dataGridViewTextBoxColumn33
            // 
            this.dataGridViewTextBoxColumn33.HeaderText = "Error";
            this.dataGridViewTextBoxColumn33.Name = "dataGridViewTextBoxColumn33";
            this.dataGridViewTextBoxColumn33.ReadOnly = true;
            this.dataGridViewTextBoxColumn33.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridViewTextBoxColumn33.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.dataGridViewTextBoxColumn33.Width = 669;
            // 
            // OptionsTab
            // 
            this.OptionsTab.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(239)))), ((int)(((byte)(244)))), ((int)(((byte)(250)))));
            this.OptionsTab.Controls.Add(this.groupBox23);
            this.OptionsTab.Controls.Add(this.groupBox22);
            this.OptionsTab.Controls.Add(this.ConvertComboboxVersion);
            this.OptionsTab.Controls.Add(this.label52);
            this.OptionsTab.Controls.Add(this.groupBox6);
            this.OptionsTab.Controls.Add(this.Version_combobox);
            this.OptionsTab.Controls.Add(this.ConvertAndSaveButton);
            this.OptionsTab.Controls.Add(this.SaveFile);
            this.OptionsTab.Location = new System.Drawing.Point(4, 22);
            this.OptionsTab.Name = "OptionsTab";
            this.OptionsTab.Padding = new System.Windows.Forms.Padding(3);
            this.OptionsTab.Size = new System.Drawing.Size(966, 483);
            this.OptionsTab.TabIndex = 4;
            this.OptionsTab.Text = "Настройки и сохранение";
            // 
            // groupBox23
            // 
            this.groupBox23.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.groupBox23.Controls.Add(this.Clear);
            this.groupBox23.Controls.Add(this.Dark);
            this.groupBox23.Location = new System.Drawing.Point(774, 4);
            this.groupBox23.Name = "groupBox23";
            this.groupBox23.Size = new System.Drawing.Size(96, 98);
            this.groupBox23.TabIndex = 63;
            this.groupBox23.TabStop = false;
            this.groupBox23.Text = "Interface";
            // 
            // Clear
            // 
            this.Clear.BackColor = System.Drawing.Color.Transparent;
            this.Clear.Checked = true;
            this.Clear.ForeColor = System.Drawing.Color.Black;
            this.Clear.Image = ((System.Drawing.Image)(resources.GetObject("Clear.Image")));
            this.Clear.Location = new System.Drawing.Point(10, 19);
            this.Clear.Name = "Clear";
            this.Clear.Size = new System.Drawing.Size(85, 24);
            this.Clear.TabIndex = 59;
            this.Clear.TabStop = true;
            this.Clear.Text = "Clear";
            this.Clear.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.Clear.UseVisualStyleBackColor = false;
            this.Clear.CheckedChanged += new System.EventHandler(this.InterfaceColorChanged);
            // 
            // Dark
            // 
            this.Dark.BackColor = System.Drawing.Color.Transparent;
            this.Dark.ForeColor = System.Drawing.Color.Black;
            this.Dark.Image = ((System.Drawing.Image)(resources.GetObject("Dark.Image")));
            this.Dark.Location = new System.Drawing.Point(10, 42);
            this.Dark.Name = "Dark";
            this.Dark.Size = new System.Drawing.Size(79, 24);
            this.Dark.TabIndex = 60;
            this.Dark.Text = "Dark";
            this.Dark.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.Dark.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.Dark.UseVisualStyleBackColor = false;
            this.Dark.CheckedChanged += new System.EventHandler(this.InterfaceColorChanged);
            // 
            // groupBox22
            // 
            this.groupBox22.Controls.Add(this.Russian);
            this.groupBox22.Controls.Add(this.English);
            this.groupBox22.Location = new System.Drawing.Point(872, 2);
            this.groupBox22.Name = "groupBox22";
            this.groupBox22.Size = new System.Drawing.Size(92, 100);
            this.groupBox22.TabIndex = 62;
            this.groupBox22.TabStop = false;
            this.groupBox22.Text = "Language";
            // 
            // Russian
            // 
            this.Russian.BackColor = System.Drawing.SystemColors.Control;
            this.Russian.Checked = true;
            this.Russian.ForeColor = System.Drawing.Color.Black;
            this.Russian.Image = ((System.Drawing.Image)(resources.GetObject("Russian.Image")));
            this.Russian.Location = new System.Drawing.Point(10, 19);
            this.Russian.Name = "Russian";
            this.Russian.Size = new System.Drawing.Size(85, 24);
            this.Russian.TabIndex = 59;
            this.Russian.TabStop = true;
            this.Russian.Text = "Russian";
            this.Russian.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.Russian.UseVisualStyleBackColor = false;
            this.Russian.CheckedChanged += new System.EventHandler(this.ChangeLanguage);
            // 
            // English
            // 
            this.English.BackColor = System.Drawing.SystemColors.Control;
            this.English.ForeColor = System.Drawing.Color.Black;
            this.English.Image = ((System.Drawing.Image)(resources.GetObject("English.Image")));
            this.English.Location = new System.Drawing.Point(10, 42);
            this.English.Name = "English";
            this.English.Size = new System.Drawing.Size(79, 24);
            this.English.TabIndex = 60;
            this.English.Text = "English";
            this.English.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.English.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.English.UseVisualStyleBackColor = false;
            this.English.CheckedChanged += new System.EventHandler(this.ChangeLanguage);
            // 
            // ConvertComboboxVersion
            // 
            this.ConvertComboboxVersion.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.ConvertComboboxVersion.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ConvertComboboxVersion.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.ConvertComboboxVersion.FormattingEnabled = true;
            this.ConvertComboboxVersion.Items.AddRange(new object[] {
            "6",
            "7",
            "10",
            "11"});
            this.ConvertComboboxVersion.Location = new System.Drawing.Point(335, 435);
            this.ConvertComboboxVersion.Name = "ConvertComboboxVersion";
            this.ConvertComboboxVersion.Size = new System.Drawing.Size(43, 21);
            this.ConvertComboboxVersion.TabIndex = 61;
            // 
            // label52
            // 
            this.label52.AutoSize = true;
            this.label52.BackColor = System.Drawing.Color.Transparent;
            this.label52.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label52.ForeColor = System.Drawing.Color.Black;
            this.label52.Location = new System.Drawing.Point(2, 4);
            this.label52.Name = "label52";
            this.label52.Size = new System.Drawing.Size(289, 15);
            this.label52.TabIndex = 5;
            this.label52.Text = "Версия клиента для захвата координат из игры:";
            // 
            // groupBox6
            // 
            this.groupBox6.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(239)))), ((int)(((byte)(244)))), ((int)(((byte)(250)))));
            this.groupBox6.Controls.Add(this.ExtraDynamicsButton_combobox);
            this.groupBox6.Controls.Add(this.label100);
            this.groupBox6.Controls.Add(this.DefaultDynamicsButton_combobox);
            this.groupBox6.Controls.Add(this.label101);
            this.groupBox6.Controls.Add(this.ExtraResourceButton_combobox);
            this.groupBox6.Controls.Add(this.label62);
            this.groupBox6.Controls.Add(this.DefaultResourceButton_combobox);
            this.groupBox6.Controls.Add(this.label77);
            this.groupBox6.Controls.Add(this.ExtraMobButton_combobox);
            this.groupBox6.Controls.Add(this.label46);
            this.groupBox6.Controls.Add(this.DefaultMobButton_combobox);
            this.groupBox6.Controls.Add(this.label50);
            this.groupBox6.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.groupBox6.Location = new System.Drawing.Point(1, 25);
            this.groupBox6.Name = "groupBox6";
            this.groupBox6.Size = new System.Drawing.Size(398, 148);
            this.groupBox6.TabIndex = 0;
            this.groupBox6.TabStop = false;
            this.groupBox6.Text = "Горячие клавиши";
            // 
            // ExtraDynamicsButton_combobox
            // 
            this.ExtraDynamicsButton_combobox.DropDownHeight = 100;
            this.ExtraDynamicsButton_combobox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ExtraDynamicsButton_combobox.FormattingEnabled = true;
            this.ExtraDynamicsButton_combobox.IntegralHeight = false;
            this.ExtraDynamicsButton_combobox.ItemHeight = 13;
            this.ExtraDynamicsButton_combobox.Items.AddRange(new object[] {
            "A",
            "B",
            "C",
            "D",
            "E",
            "F",
            "G",
            "H",
            "I",
            "J",
            "K",
            "L",
            "M",
            "N",
            "O",
            "P",
            "Q",
            "R",
            "S",
            "T",
            "U",
            "V",
            "W",
            "Q",
            "Y",
            "Z"});
            this.ExtraDynamicsButton_combobox.Location = new System.Drawing.Point(189, 73);
            this.ExtraDynamicsButton_combobox.Name = "ExtraDynamicsButton_combobox";
            this.ExtraDynamicsButton_combobox.Size = new System.Drawing.Size(42, 21);
            this.ExtraDynamicsButton_combobox.TabIndex = 16;
            this.ExtraDynamicsButton_combobox.SelectedIndexChanged += new System.EventHandler(this.DefaultMobButton_combobox_SelectedIndexChanged);
            // 
            // label100
            // 
            this.label100.AutoSize = true;
            this.label100.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label100.Location = new System.Drawing.Point(174, 73);
            this.label100.Name = "label100";
            this.label100.Size = new System.Drawing.Size(17, 18);
            this.label100.TabIndex = 15;
            this.label100.Text = "+";
            // 
            // DefaultDynamicsButton_combobox
            // 
            this.DefaultDynamicsButton_combobox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.DefaultDynamicsButton_combobox.FormattingEnabled = true;
            this.DefaultDynamicsButton_combobox.Items.AddRange(new object[] {
            "Alt",
            "Control"});
            this.DefaultDynamicsButton_combobox.Location = new System.Drawing.Point(87, 73);
            this.DefaultDynamicsButton_combobox.Name = "DefaultDynamicsButton_combobox";
            this.DefaultDynamicsButton_combobox.Size = new System.Drawing.Size(87, 21);
            this.DefaultDynamicsButton_combobox.TabIndex = 14;
            this.DefaultDynamicsButton_combobox.SelectedIndexChanged += new System.EventHandler(this.DefaultMobButton_combobox_SelectedIndexChanged);
            // 
            // label101
            // 
            this.label101.AutoSize = true;
            this.label101.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label101.ForeColor = System.Drawing.Color.Black;
            this.label101.Location = new System.Drawing.Point(7, 73);
            this.label101.Name = "label101";
            this.label101.Size = new System.Drawing.Size(79, 15);
            this.label101.TabIndex = 17;
            this.label101.Text = "Дин.Объект:";
            // 
            // ExtraResourceButton_combobox
            // 
            this.ExtraResourceButton_combobox.DropDownHeight = 100;
            this.ExtraResourceButton_combobox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ExtraResourceButton_combobox.FormattingEnabled = true;
            this.ExtraResourceButton_combobox.IntegralHeight = false;
            this.ExtraResourceButton_combobox.ItemHeight = 13;
            this.ExtraResourceButton_combobox.Items.AddRange(new object[] {
            "A",
            "B",
            "C",
            "D",
            "E",
            "F",
            "G",
            "H",
            "I",
            "J",
            "K",
            "L",
            "M",
            "N",
            "O",
            "P",
            "Q",
            "R",
            "S",
            "T",
            "U",
            "V",
            "W",
            "Q",
            "Y",
            "Z"});
            this.ExtraResourceButton_combobox.Location = new System.Drawing.Point(189, 46);
            this.ExtraResourceButton_combobox.Name = "ExtraResourceButton_combobox";
            this.ExtraResourceButton_combobox.Size = new System.Drawing.Size(42, 21);
            this.ExtraResourceButton_combobox.TabIndex = 12;
            this.ExtraResourceButton_combobox.SelectedIndexChanged += new System.EventHandler(this.DefaultMobButton_combobox_SelectedIndexChanged);
            // 
            // label62
            // 
            this.label62.AutoSize = true;
            this.label62.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label62.Location = new System.Drawing.Point(174, 46);
            this.label62.Name = "label62";
            this.label62.Size = new System.Drawing.Size(17, 18);
            this.label62.TabIndex = 11;
            this.label62.Text = "+";
            // 
            // DefaultResourceButton_combobox
            // 
            this.DefaultResourceButton_combobox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.DefaultResourceButton_combobox.FormattingEnabled = true;
            this.DefaultResourceButton_combobox.Items.AddRange(new object[] {
            "Alt",
            "Control"});
            this.DefaultResourceButton_combobox.Location = new System.Drawing.Point(87, 46);
            this.DefaultResourceButton_combobox.Name = "DefaultResourceButton_combobox";
            this.DefaultResourceButton_combobox.Size = new System.Drawing.Size(87, 21);
            this.DefaultResourceButton_combobox.TabIndex = 10;
            this.DefaultResourceButton_combobox.SelectedIndexChanged += new System.EventHandler(this.DefaultMobButton_combobox_SelectedIndexChanged);
            // 
            // label77
            // 
            this.label77.AutoSize = true;
            this.label77.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label77.ForeColor = System.Drawing.Color.Black;
            this.label77.Location = new System.Drawing.Point(37, 46);
            this.label77.Name = "label77";
            this.label77.Size = new System.Drawing.Size(49, 15);
            this.label77.TabIndex = 13;
            this.label77.Text = "Ресурс:";
            // 
            // ExtraMobButton_combobox
            // 
            this.ExtraMobButton_combobox.DropDownHeight = 100;
            this.ExtraMobButton_combobox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ExtraMobButton_combobox.FormattingEnabled = true;
            this.ExtraMobButton_combobox.IntegralHeight = false;
            this.ExtraMobButton_combobox.ItemHeight = 13;
            this.ExtraMobButton_combobox.Items.AddRange(new object[] {
            "A",
            "B",
            "C",
            "D",
            "E",
            "F",
            "G",
            "H",
            "I",
            "J",
            "K",
            "L",
            "M",
            "N",
            "O",
            "P",
            "Q",
            "R",
            "S",
            "T",
            "U",
            "V",
            "W",
            "Q",
            "Y",
            "Z"});
            this.ExtraMobButton_combobox.Location = new System.Drawing.Point(189, 19);
            this.ExtraMobButton_combobox.Name = "ExtraMobButton_combobox";
            this.ExtraMobButton_combobox.Size = new System.Drawing.Size(42, 21);
            this.ExtraMobButton_combobox.TabIndex = 8;
            this.ExtraMobButton_combobox.SelectedIndexChanged += new System.EventHandler(this.DefaultMobButton_combobox_SelectedIndexChanged);
            // 
            // label46
            // 
            this.label46.AutoSize = true;
            this.label46.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label46.Location = new System.Drawing.Point(174, 19);
            this.label46.Name = "label46";
            this.label46.Size = new System.Drawing.Size(17, 18);
            this.label46.TabIndex = 7;
            this.label46.Text = "+";
            // 
            // DefaultMobButton_combobox
            // 
            this.DefaultMobButton_combobox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.DefaultMobButton_combobox.FormattingEnabled = true;
            this.DefaultMobButton_combobox.Items.AddRange(new object[] {
            "Alt",
            "Control"});
            this.DefaultMobButton_combobox.Location = new System.Drawing.Point(87, 19);
            this.DefaultMobButton_combobox.Name = "DefaultMobButton_combobox";
            this.DefaultMobButton_combobox.Size = new System.Drawing.Size(87, 21);
            this.DefaultMobButton_combobox.TabIndex = 6;
            this.DefaultMobButton_combobox.SelectedIndexChanged += new System.EventHandler(this.DefaultMobButton_combobox_SelectedIndexChanged);
            // 
            // label50
            // 
            this.label50.AutoSize = true;
            this.label50.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label50.ForeColor = System.Drawing.Color.Black;
            this.label50.Location = new System.Drawing.Point(20, 19);
            this.label50.Name = "label50";
            this.label50.Size = new System.Drawing.Size(66, 15);
            this.label50.TabIndex = 9;
            this.label50.Text = "Существо:";
            // 
            // Version_combobox
            // 
            this.Version_combobox.DropDownHeight = 150;
            this.Version_combobox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.Version_combobox.FormattingEnabled = true;
            this.Version_combobox.IntegralHeight = false;
            this.Version_combobox.Location = new System.Drawing.Point(292, 2);
            this.Version_combobox.Name = "Version_combobox";
            this.Version_combobox.Size = new System.Drawing.Size(107, 21);
            this.Version_combobox.TabIndex = 16;
            // 
            // ConvertAndSaveButton
            // 
            this.ConvertAndSaveButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.ConvertAndSaveButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.5F);
            this.ConvertAndSaveButton.ForeColor = System.Drawing.Color.Black;
            this.ConvertAndSaveButton.Image = ((System.Drawing.Image)(resources.GetObject("ConvertAndSaveButton.Image")));
            this.ConvertAndSaveButton.Location = new System.Drawing.Point(175, 423);
            this.ConvertAndSaveButton.Name = "ConvertAndSaveButton";
            this.ConvertAndSaveButton.Size = new System.Drawing.Size(207, 60);
            this.ConvertAndSaveButton.TabIndex = 17;
            this.ConvertAndSaveButton.Text = "Конвертировать в     версию и сохранить";
            this.ConvertAndSaveButton.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.ConvertAndSaveButton.UseVisualStyleBackColor = true;
            this.ConvertAndSaveButton.Click += new System.EventHandler(this.ConvertAndSaveButton_Click);
            // 
            // SaveFile
            // 
            this.SaveFile.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.SaveFile.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.5F);
            this.SaveFile.ForeColor = System.Drawing.Color.Black;
            this.SaveFile.Image = ((System.Drawing.Image)(resources.GetObject("SaveFile.Image")));
            this.SaveFile.Location = new System.Drawing.Point(3, 423);
            this.SaveFile.Name = "SaveFile";
            this.SaveFile.Size = new System.Drawing.Size(169, 60);
            this.SaveFile.TabIndex = 7;
            this.SaveFile.Text = "Сохранить Npcgen.data";
            this.SaveFile.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.SaveFile.UseVisualStyleBackColor = true;
            this.SaveFile.Click += new System.EventHandler(this.SaveFile_Click);
            // 
            // Maps_combobox
            // 
            this.Maps_combobox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.Maps_combobox.DropDownHeight = 150;
            this.Maps_combobox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.Maps_combobox.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.Maps_combobox.FormattingEnabled = true;
            this.Maps_combobox.IntegralHeight = false;
            this.Maps_combobox.Location = new System.Drawing.Point(882, 5);
            this.Maps_combobox.Name = "Maps_combobox";
            this.Maps_combobox.Size = new System.Drawing.Size(94, 21);
            this.Maps_combobox.TabIndex = 220;
            // 
            // Element_dialog
            // 
            this.Element_dialog.FileName = "Elements.data";
            this.Element_dialog.Filter = "Elements|Elements.data|All Files|*.*";
            // 
            // Npcgen_dialog
            // 
            this.Npcgen_dialog.FileName = "Npcgen.data";
            this.Npcgen_dialog.Filter = "Npcgen|Npcgen.data|All Files|*.*";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(5, 57);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(73, 13);
            this.label4.TabIndex = 12;
            this.label4.Text = "Surfaces.pck:";
            // 
            // Surfaces_path
            // 
            this.Surfaces_path.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Surfaces_path.Location = new System.Drawing.Point(75, 54);
            this.Surfaces_path.Name = "Surfaces_path";
            this.Surfaces_path.Size = new System.Drawing.Size(687, 20);
            this.Surfaces_path.TabIndex = 13;
            // 
            // Surfaces_search
            // 
            this.Surfaces_search.FileName = "Surfaces.pck";
            this.Surfaces_search.Filter = "Anjelica engine package|Surfaces.pck|All Files|*.*";
            // 
            // Npcgen_save_dialog
            // 
            this.Npcgen_save_dialog.FileName = "Npcgen.data";
            this.Npcgen_save_dialog.Filter = "Npcgen|Npcgen.data|All Files|*.*";
            // 
            // ButtonShowMap
            // 
            this.ButtonShowMap.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.ButtonShowMap.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ButtonShowMap.Location = new System.Drawing.Point(882, 28);
            this.ButtonShowMap.Name = "ButtonShowMap";
            this.ButtonShowMap.Size = new System.Drawing.Size(96, 24);
            this.ButtonShowMap.TabIndex = 1;
            this.ButtonShowMap.Text = "Показать карту";
            this.ButtonShowMap.UseVisualStyleBackColor = false;
            this.ButtonShowMap.Click += new System.EventHandler(this.ShowOnMapsButton_Click);
            // 
            // MapProgress
            // 
            this.MapProgress.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.MapProgress.Location = new System.Drawing.Point(882, 53);
            this.MapProgress.Name = "MapProgress";
            this.MapProgress.Size = new System.Drawing.Size(95, 22);
            this.MapProgress.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
            this.MapProgress.TabIndex = 0;
            // 
            // Dynamics_dialog
            // 
            this.Dynamics_dialog.FileName = "DynamicObjects.data";
            this.Dynamics_dialog.Filter = "DynamicObjects|DynamicObjects.data|All Files|*.*";
            // 
            // InformationButton
            // 
            this.InformationButton.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.InformationButton.Image = ((System.Drawing.Image)(resources.GetObject("InformationButton.Image")));
            this.InformationButton.Location = new System.Drawing.Point(950, 75);
            this.InformationButton.Name = "InformationButton";
            this.InformationButton.Size = new System.Drawing.Size(28, 27);
            this.InformationButton.TabIndex = 16;
            this.InformationButton.UseVisualStyleBackColor = true;
            this.InformationButton.Click += new System.EventHandler(this.InformationButton_Click);
            // 
            // Open_surfaces
            // 
            this.Open_surfaces.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Open_surfaces.ForeColor = System.Drawing.Color.Black;
            this.Open_surfaces.Image = ((System.Drawing.Image)(resources.GetObject("Open_surfaces.Image")));
            this.Open_surfaces.Location = new System.Drawing.Point(790, 51);
            this.Open_surfaces.Name = "Open_surfaces";
            this.Open_surfaces.Size = new System.Drawing.Size(90, 24);
            this.Open_surfaces.TabIndex = 15;
            this.Open_surfaces.Text = "Открыть";
            this.Open_surfaces.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.Open_surfaces.UseVisualStyleBackColor = true;
            this.Open_surfaces.Click += new System.EventHandler(this.Open_surfaces_Click);
            // 
            // Search_surfaces
            // 
            this.Search_surfaces.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Search_surfaces.BackColor = System.Drawing.SystemColors.Control;
            this.Search_surfaces.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Search_surfaces.Image = ((System.Drawing.Image)(resources.GetObject("Search_surfaces.Image")));
            this.Search_surfaces.Location = new System.Drawing.Point(762, 54);
            this.Search_surfaces.Name = "Search_surfaces";
            this.Search_surfaces.Size = new System.Drawing.Size(26, 20);
            this.Search_surfaces.TabIndex = 14;
            this.Search_surfaces.UseVisualStyleBackColor = false;
            this.Search_surfaces.Click += new System.EventHandler(this.SearchSurfacesButton);
            // 
            // OpenFiles
            // 
            this.OpenFiles.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.OpenFiles.ForeColor = System.Drawing.Color.Black;
            this.OpenFiles.Image = ((System.Drawing.Image)(resources.GetObject("OpenFiles.Image")));
            this.OpenFiles.Location = new System.Drawing.Point(790, 5);
            this.OpenFiles.Name = "OpenFiles";
            this.OpenFiles.Size = new System.Drawing.Size(90, 45);
            this.OpenFiles.TabIndex = 6;
            this.OpenFiles.Text = "Открыть";
            this.OpenFiles.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.OpenFiles.UseVisualStyleBackColor = true;
            this.OpenFiles.Click += new System.EventHandler(this.OpenElementAndNpcgen);
            // 
            // Search_Npcgen
            // 
            this.Search_Npcgen.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Search_Npcgen.BackColor = System.Drawing.SystemColors.Control;
            this.Search_Npcgen.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Search_Npcgen.Image = ((System.Drawing.Image)(resources.GetObject("Search_Npcgen.Image")));
            this.Search_Npcgen.Location = new System.Drawing.Point(762, 30);
            this.Search_Npcgen.Name = "Search_Npcgen";
            this.Search_Npcgen.Size = new System.Drawing.Size(26, 20);
            this.Search_Npcgen.TabIndex = 5;
            this.Search_Npcgen.UseVisualStyleBackColor = false;
            this.Search_Npcgen.Click += new System.EventHandler(this.SearchNpcgenButton);
            // 
            // Search_element
            // 
            this.Search_element.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Search_element.BackColor = System.Drawing.SystemColors.Control;
            this.Search_element.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Search_element.Image = ((System.Drawing.Image)(resources.GetObject("Search_element.Image")));
            this.Search_element.Location = new System.Drawing.Point(762, 6);
            this.Search_element.Name = "Search_element";
            this.Search_element.Size = new System.Drawing.Size(26, 20);
            this.Search_element.TabIndex = 4;
            this.Search_element.TabStop = false;
            this.Search_element.UseVisualStyleBackColor = false;
            this.Search_element.Click += new System.EventHandler(this.SearchElementButton);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoValidate = System.Windows.Forms.AutoValidate.Disable;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(239)))), ((int)(((byte)(244)))), ((int)(((byte)(250)))));
            this.ClientSize = new System.Drawing.Size(981, 614);
            this.Controls.Add(this.InformationButton);
            this.Controls.Add(this.MapProgress);
            this.Controls.Add(this.Maps_combobox);
            this.Controls.Add(this.ButtonShowMap);
            this.Controls.Add(this.Open_surfaces);
            this.Controls.Add(this.Search_surfaces);
            this.Controls.Add(this.Surfaces_path);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.MainTabControl);
            this.Controls.Add(this.MainProgressBar);
            this.Controls.Add(this.OpenFiles);
            this.Controls.Add(this.Search_Npcgen);
            this.Controls.Add(this.Search_element);
            this.Controls.Add(this.Npcgen_textbox);
            this.Controls.Add(this.Elements_textbox);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimumSize = new System.Drawing.Size(997, 650);
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Npcgen Editor v1.7.4";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Form1_FormClosed);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.MainTabControl.ResumeLayout(false);
            this.ExistenceTab.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Group_numeric)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.RefreshLower_numeric)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Deadtime_numeric)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.NeedHelp_numeric)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.AskHelp_numeric)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.NpcsGroupGrid)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Turn_numeric)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Water_numeric)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Path_numeric)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Path_speed)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.DeathAmount_numeric)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Respawn_numeric)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Amount_numeric)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Id_numeric)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.NpcMobsGrid)).EndInit();
            this.ExistenceContext.ResumeLayout(false);
            this.MainGroupBox.ResumeLayout(false);
            this.MainGroupBox.PerformLayout();
            this.groupBox8.ResumeLayout(false);
            this.groupBox8.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.AddMonsterRespawnTime)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.AddMonsterAmount)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.AddMonsterId)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.AddMonsterTrigger)).EndInit();
            this.contextMenuStrip2.ResumeLayout(false);
            this.ExistenceToolStrip.ResumeLayout(false);
            this.ExistenceToolStrip.PerformLayout();
            this.ResourcesTab.ResumeLayout(false);
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.RType_numeric)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.RfHeiOff_numeric)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.RRespawn_numeric)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.RAmount_numeric)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.RId_numeric)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ResourcesGroupGrid)).EndInit();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox11.ResumeLayout(false);
            this.groupBox11.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.AddResourceRespawnTime)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.AddResourceAmount)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.AddResourceID)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.AddResourcesTrigger)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ResourcesGrid)).EndInit();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.DynObjectsTab.ResumeLayout(false);
            this.groupBox9.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.DynamicPictureBox)).EndInit();
            this.groupBox7.ResumeLayout(false);
            this.groupBox7.PerformLayout();
            this.groupBox10.ResumeLayout(false);
            this.groupBox10.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.AddDynamicsTrigger)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.AddDynamicsID)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.DynamicGrid)).EndInit();
            this.toolStrip2.ResumeLayout(false);
            this.toolStrip2.PerformLayout();
            this.TriggersTab.ResumeLayout(false);
            this.groupBox15.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.DUTrigger)).EndInit();
            this.groupBox14.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.RUTrigger)).EndInit();
            this.groupBox13.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.MUTrigger)).EndInit();
            this.groupBox12.ResumeLayout(false);
            this.groupBox12.PerformLayout();
            this.groupBox17.ResumeLayout(false);
            this.groupBox17.PerformLayout();
            this.groupBox16.ResumeLayout(false);
            this.groupBox16.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.TriggersGrid)).EndInit();
            this.TriggersContext.ResumeLayout(false);
            this.toolStrip3.ResumeLayout(false);
            this.toolStrip3.PerformLayout();
            this.SearchTab.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.SearchGrid)).EndInit();
            this.contextMenuStrip1.ResumeLayout(false);
            this.groupBox21.ResumeLayout(false);
            this.groupBox21.PerformLayout();
            this.groupBox20.ResumeLayout(false);
            this.groupBox20.PerformLayout();
            this.groupBox19.ResumeLayout(false);
            this.groupBox19.PerformLayout();
            this.groupBox18.ResumeLayout(false);
            this.groupBox18.PerformLayout();
            this.ErrorsTab.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.ErrorsGrid)).EndInit();
            this.OptionsTab.ResumeLayout(false);
            this.OptionsTab.PerformLayout();
            this.groupBox23.ResumeLayout(false);
            this.groupBox22.ResumeLayout(false);
            this.groupBox6.ResumeLayout(false);
            this.groupBox6.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

	}
}
