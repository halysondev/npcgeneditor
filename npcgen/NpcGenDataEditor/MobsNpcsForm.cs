using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using NpcGenDataEditor.Properties;

namespace NpcGenDataEditor;

public class MobsNpcsForm : Form
{
	private Form1 Main_form;

	private List<NpcMonster> MobsAndNpcs;

	private int Monsters;

	private int Npcs;

	private List<NpcMonster> resources;

	private int MainAction;

	private int Window;

	private List<int> MobsSearchIndexes = new List<int>();

	private int MobsSearchPosition;

	private List<int> NpcsSearchIndexes = new List<int>();

	private int NpcsSearchPosition;

	private List<int> ResourcesSearchIndexes = new List<int>();

	private int ResourcesSearchPosition;

	private IContainer components = null;

	private TabControl tabControl1;

	private TabPage tabPage1;

	private TabPage tabPage2;

	private DataGridView NpcsGrid;

	private new Button AcceptButton;

	private new Button CancelButton;

	private DataGridView MobsGrid;

	private DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;

	private DataGridViewTextBoxColumn dataGridViewTextBoxColumn4;

	private DataGridViewTextBoxColumn dataGridViewTextBoxColumn5;

	private TabPage tabPage3;

	private DataGridView ResourcesGrid;

	private DataGridViewTextBoxColumn dataGridViewTextBoxColumn6;

	private DataGridViewTextBoxColumn dataGridViewTextBoxColumn7;

	private DataGridViewTextBoxColumn dataGridViewTextBoxColumn8;

	private DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;

	private DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;

	private DataGridViewTextBoxColumn Column4;

	private Button ContinueMobsSearch;

	private TextBox MobsSearchTextbox;

	private Label label1;

	private Label label2;

	private TextBox NpcsSearchTextbox;

	private Button ContinueNpcsSearch;

	private Label label3;

	private TextBox ResourcesSearchTextbox;

	private Button ContinueResourcesSearch;

	public int SetAction
	{
		set
		{
			MainAction = value;
		}
	}

	public int SetWindow
	{
		set
		{
			Window = value;
		}
	}

	public MobsNpcsForm(Form1 fm, List<NpcMonster> ls, List<NpcMonster> rs, int Monsters, int Npcs)
	{
		resources = rs;
		InitializeComponent();
		Main_form = fm;
		MobsAndNpcs = ls;
		this.Monsters = Monsters;
		this.Npcs = Npcs;
		RefreshGrids();
	}

	public void RefreshGrids()
	{
		NpcsGrid.ScrollBars = ScrollBars.None;
		MobsGrid.ScrollBars = ScrollBars.None;
		ResourcesGrid.ScrollBars = ScrollBars.None;
		for (int i = 0; i < Monsters; i++)
		{
			MobsGrid.Rows.Add(i + 1, MobsAndNpcs[i].Id, MobsAndNpcs[i].Name);
		}
		for (int j = Monsters; j < Npcs + Monsters; j++)
		{
			NpcsGrid.Rows.Add(j + 1 - Monsters, MobsAndNpcs[j].Id, MobsAndNpcs[j].Name);
		}
		for (int k = 0; k < resources.Count; k++)
		{
			ResourcesGrid.Rows.Add(k + 1, resources[k].Id, resources[k].Name);
		}
		MobsGrid.ScrollBars = ScrollBars.Vertical;
		NpcsGrid.ScrollBars = ScrollBars.Vertical;
		ResourcesGrid.ScrollBars = ScrollBars.Vertical;
	}

	private void AcceptButton_Click(object sender, EventArgs e)
	{
		if (tabControl1.SelectedIndex == 0 && Window == 1)
		{
			Main_form.SetId(Convert.ToInt32(MobsGrid.CurrentRow.Cells[1].Value), MainAction, Window);
		}
		else if (tabControl1.SelectedIndex == 0 && Window == 2)
		{
			Main_form.SetId(Convert.ToInt32(MobsGrid.CurrentRow.Cells[1].Value), MainAction, Window);
		}
		else if (tabControl1.SelectedIndex == 1 && Window == 1)
		{
			Main_form.SetId(Convert.ToInt32(NpcsGrid.CurrentRow.Cells[1].Value), MainAction, Window);
		}
		else if (tabControl1.SelectedIndex == 1 && Window == 2)
		{
			Main_form.SetId(Convert.ToInt32(NpcsGrid.CurrentRow.Cells[1].Value), MainAction, Window);
		}
		else if (tabControl1.SelectedIndex == 2 && Window == 1)
		{
			Main_form.SetId(Convert.ToInt32(ResourcesGrid.CurrentRow.Cells[1].Value), MainAction, Window);
		}
		else if (tabControl1.SelectedIndex == 2 && Window == 2)
		{
			Main_form.SetId(Convert.ToInt32(ResourcesGrid.CurrentRow.Cells[1].Value), MainAction, Window);
		}
		Hide();
	}

	private void ResourcesGrid_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
	{
		Main_form.SetId(Convert.ToInt32(ResourcesGrid.CurrentRow.Cells[1].Value), MainAction, Window);
		Hide();
	}

	private void NpcsGrid_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
	{
		Main_form.SetId(Convert.ToInt32(NpcsGrid.CurrentRow.Cells[1].Value), MainAction, Window);
		Hide();
	}

	private void MobsGrid_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
	{
		Main_form.SetId(Convert.ToInt32(MobsGrid.CurrentRow.Cells[1].Value), MainAction, Window);
		Hide();
	}

	public void FindRow(int Index, string act)
	{
		switch (act)
		{
		case "Mob":
			tabControl1.SelectedIndex = 0;
			MobsGrid.CurrentCell = MobsGrid.Rows[Index].Cells[1];
			break;
		case "Npc":
			tabControl1.SelectedIndex = 1;
			NpcsGrid.CurrentCell = NpcsGrid.Rows[Index].Cells[1];
			break;
		case "Resource":
			tabControl1.SelectedIndex = 2;
			ResourcesGrid.CurrentCell = ResourcesGrid.Rows[Index].Cells[1];
			break;
		}
	}

	private void CancelButton_Click(object sender, EventArgs e)
	{
		Hide();
	}

	public void RefreshLanguage(int Language)
	{
		switch (Language)
		{
		case 1:
			tabPage1.Text = "Мобы";
			tabPage2.Text = "Нипа";
			tabPage3.Text = "Ресурсы";
			AcceptButton.Text = "Выбрать";
			CancelButton.Text = "Отменить";
			label1.Text = "Поиск:";
			label2.Text = "Поиск:";
			label3.Text = "Поиск:";
			break;
		case 2:
			tabPage1.Text = "Monsters";
			tabPage2.Text = "Npcs";
			tabPage3.Text = "Resources";
			AcceptButton.Text = "Select";
			CancelButton.Text = "Cancel";
			label1.Text = "Search:";
			label2.Text = "Search:";
			label3.Text = "Search:";
			break;
		}
	}

	private void MobsSearchTextbox_TextChanged(object sender, EventArgs e)
	{
		if (!string.IsNullOrWhiteSpace(MobsSearchTextbox.Text))
		{
			MobsSearchPosition = 0;
			MobsSearchIndexes = (from DataGridViewRow i in MobsGrid.Rows
				where i.Cells[1].Value.ToString().Contains(MobsSearchTextbox.Text) || i.Cells[2].Value.ToString().ToLower().Contains(MobsSearchTextbox.Text.ToLower())
				select i into v
				select v.Index).ToList();
			if (MobsSearchIndexes.Count != 0)
			{
				MobsGrid.CurrentCell = MobsGrid.Rows[MobsSearchIndexes[0]].Cells[1];
				MobsGrid.FirstDisplayedScrollingRowIndex = MobsGrid.Rows[MobsSearchIndexes[0]].Index;
				MobsSearchPosition++;
			}
		}
	}

	private void NpcsSearchTextbox_TextChanged(object sender, EventArgs e)
	{
		if (!string.IsNullOrWhiteSpace(NpcsSearchTextbox.Text))
		{
			NpcsSearchPosition = 0;
			NpcsSearchIndexes = (from DataGridViewRow i in NpcsGrid.Rows
				where i.Cells[1].Value.ToString().Contains(NpcsSearchTextbox.Text) || i.Cells[2].Value.ToString().ToLower().Contains(NpcsSearchTextbox.Text.ToLower())
				select i into v
				select v.Index).ToList();
			if (NpcsSearchIndexes.Count != 0)
			{
				NpcsGrid.CurrentCell = NpcsGrid.Rows[NpcsSearchIndexes[0]].Cells[1];
				NpcsGrid.FirstDisplayedScrollingRowIndex = NpcsGrid.Rows[NpcsSearchIndexes[0]].Index;
				NpcsSearchPosition++;
			}
		}
	}

	private void ResourcesSearchTextbox_TextChanged(object sender, EventArgs e)
	{
		if (!string.IsNullOrWhiteSpace(ResourcesSearchTextbox.Text))
		{
			ResourcesSearchPosition = 0;
			ResourcesSearchIndexes = (from DataGridViewRow i in ResourcesGrid.Rows
				where i.Cells[1].Value.ToString().Contains(ResourcesSearchTextbox.Text) || i.Cells[2].Value.ToString().ToLower().Contains(ResourcesSearchTextbox.Text.ToLower())
				select i into v
				select v.Index).ToList();
			if (ResourcesSearchIndexes.Count != 0)
			{
				ResourcesGrid.CurrentCell = ResourcesGrid.Rows[ResourcesSearchIndexes[0]].Cells[1];
				ResourcesGrid.FirstDisplayedScrollingRowIndex = ResourcesGrid.Rows[ResourcesSearchIndexes[0]].Index;
				ResourcesSearchPosition++;
			}
		}
	}

	private void ContinueMobsSearch_Click(object sender, EventArgs e)
	{
		if (MobsSearchIndexes != null && MobsSearchPosition != MobsSearchIndexes.Count)
		{
			MobsGrid.CurrentCell = MobsGrid.Rows[MobsSearchIndexes[MobsSearchPosition]].Cells[1];
			MobsGrid.FirstDisplayedScrollingRowIndex = MobsGrid.Rows[MobsSearchIndexes[MobsSearchPosition]].Index;
			MobsSearchPosition++;
		}
	}

	private void ContinueNpcsSearch_Click(object sender, EventArgs e)
	{
		if (NpcsSearchIndexes != null && NpcsSearchPosition != NpcsSearchIndexes.Count)
		{
			NpcsGrid.CurrentCell = NpcsGrid.Rows[NpcsSearchIndexes[NpcsSearchPosition]].Cells[1];
			NpcsGrid.FirstDisplayedScrollingRowIndex = NpcsGrid.Rows[NpcsSearchIndexes[NpcsSearchPosition]].Index;
			NpcsSearchPosition++;
		}
	}

	private void ContinueResourcesSearch_Click(object sender, EventArgs e)
	{
		if (ResourcesSearchIndexes != null && ResourcesSearchPosition != ResourcesSearchIndexes.Count)
		{
			ResourcesGrid.CurrentCell = ResourcesGrid.Rows[ResourcesSearchIndexes[ResourcesSearchPosition]].Cells[1];
			ResourcesGrid.FirstDisplayedScrollingRowIndex = ResourcesGrid.Rows[ResourcesSearchIndexes[ResourcesSearchPosition]].Index;
			ResourcesSearchPosition++;
		}
	}

	private void MobsSearchTextbox_DoubleClick(object sender, EventArgs e)
	{
		MobsSearchTextbox.SelectAll();
	}

	private void NpcsSearchTextbox_DoubleClick(object sender, EventArgs e)
	{
		NpcsSearchTextbox.SelectAll();
	}

	private void ResourcesSearchTextbox_DoubleClick(object sender, EventArgs e)
	{
		ResourcesSearchTextbox.SelectAll();
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle12 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle9 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle10 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle11 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MobsNpcsForm));
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.MobsSearchTextbox = new System.Windows.Forms.TextBox();
            this.ContinueMobsSearch = new System.Windows.Forms.Button();
            this.MobsGrid = new System.Windows.Forms.DataGridView();
            this.dataGridViewTextBoxColumn3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.label1 = new System.Windows.Forms.Label();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.NpcsSearchTextbox = new System.Windows.Forms.TextBox();
            this.ContinueNpcsSearch = new System.Windows.Forms.Button();
            this.NpcsGrid = new System.Windows.Forms.DataGridView();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.label2 = new System.Windows.Forms.Label();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.ResourcesSearchTextbox = new System.Windows.Forms.TextBox();
            this.ContinueResourcesSearch = new System.Windows.Forms.Button();
            this.ResourcesGrid = new System.Windows.Forms.DataGridView();
            this.dataGridViewTextBoxColumn6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn7 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn8 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.label3 = new System.Windows.Forms.Label();
            this.CancelButton = new System.Windows.Forms.Button();
            this.AcceptButton = new System.Windows.Forms.Button();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.MobsGrid)).BeginInit();
            this.tabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.NpcsGrid)).BeginInit();
            this.tabPage3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ResourcesGrid)).BeginInit();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Location = new System.Drawing.Point(1, 1);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(364, 493);
            this.tabControl1.TabIndex = 0;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.MobsSearchTextbox);
            this.tabPage1.Controls.Add(this.ContinueMobsSearch);
            this.tabPage1.Controls.Add(this.MobsGrid);
            this.tabPage1.Controls.Add(this.label1);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(356, 467);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Mobs";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // MobsSearchTextbox
            // 
            this.MobsSearchTextbox.Location = new System.Drawing.Point(48, 445);
            this.MobsSearchTextbox.Name = "MobsSearchTextbox";
            this.MobsSearchTextbox.Size = new System.Drawing.Size(278, 20);
            this.MobsSearchTextbox.TabIndex = 18;
            this.MobsSearchTextbox.Text = "Search:Enter name or ID";
            this.MobsSearchTextbox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.MobsSearchTextbox.TextChanged += new System.EventHandler(this.MobsSearchTextbox_TextChanged);
            this.MobsSearchTextbox.DoubleClick += new System.EventHandler(this.MobsSearchTextbox_DoubleClick);
            // 
            // ContinueMobsSearch
            // 
            this.ContinueMobsSearch.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ContinueMobsSearch.Location = new System.Drawing.Point(328, 445);
            this.ContinueMobsSearch.Name = "ContinueMobsSearch";
            this.ContinueMobsSearch.Size = new System.Drawing.Size(26, 20);
            this.ContinueMobsSearch.TabIndex = 17;
            this.ContinueMobsSearch.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.ContinueMobsSearch.UseVisualStyleBackColor = true;
            this.ContinueMobsSearch.Click += new System.EventHandler(this.ContinueMobsSearch_Click);
            // 
            // MobsGrid
            // 
            this.MobsGrid.AllowUserToAddRows = false;
            this.MobsGrid.AllowUserToDeleteRows = false;
            this.MobsGrid.AllowUserToResizeColumns = false;
            this.MobsGrid.AllowUserToResizeRows = false;
            this.MobsGrid.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.MobsGrid.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.MobsGrid.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None;
            this.MobsGrid.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            this.MobsGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.MobsGrid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewTextBoxColumn3,
            this.dataGridViewTextBoxColumn4,
            this.dataGridViewTextBoxColumn5});
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.MobsGrid.DefaultCellStyle = dataGridViewCellStyle4;
            this.MobsGrid.EnableHeadersVisualStyles = false;
            this.MobsGrid.Location = new System.Drawing.Point(0, 0);
            this.MobsGrid.MultiSelect = false;
            this.MobsGrid.Name = "MobsGrid";
            this.MobsGrid.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            this.MobsGrid.RowHeadersVisible = false;
            this.MobsGrid.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.MobsGrid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.MobsGrid.ShowCellErrors = false;
            this.MobsGrid.ShowCellToolTips = false;
            this.MobsGrid.ShowEditingIcon = false;
            this.MobsGrid.ShowRowErrors = false;
            this.MobsGrid.Size = new System.Drawing.Size(356, 443);
            this.MobsGrid.TabIndex = 15;
            this.MobsGrid.CellMouseDoubleClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.MobsGrid_CellMouseDoubleClick);
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
            this.dataGridViewTextBoxColumn3.Width = 45;
            // 
            // dataGridViewTextBoxColumn4
            // 
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(77)))), ((int)(((byte)(255)))), ((int)(((byte)(143)))));
            this.dataGridViewTextBoxColumn4.DefaultCellStyle = dataGridViewCellStyle2;
            this.dataGridViewTextBoxColumn4.FillWeight = 44.7233F;
            this.dataGridViewTextBoxColumn4.HeaderText = "  ID";
            this.dataGridViewTextBoxColumn4.Name = "dataGridViewTextBoxColumn4";
            this.dataGridViewTextBoxColumn4.ReadOnly = true;
            this.dataGridViewTextBoxColumn4.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.dataGridViewTextBoxColumn4.Width = 60;
            // 
            // dataGridViewTextBoxColumn5
            // 
            dataGridViewCellStyle3.ForeColor = System.Drawing.Color.White;
            this.dataGridViewTextBoxColumn5.DefaultCellStyle = dataGridViewCellStyle3;
            this.dataGridViewTextBoxColumn5.HeaderText = "Name";
            this.dataGridViewTextBoxColumn5.Name = "dataGridViewTextBoxColumn5";
            this.dataGridViewTextBoxColumn5.ReadOnly = true;
            this.dataGridViewTextBoxColumn5.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.dataGridViewTextBoxColumn5.Width = 250;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label1.Location = new System.Drawing.Point(1, 447);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(49, 15);
            this.label1.TabIndex = 19;
            this.label1.Text = "Search:";
            // 
            // tabPage2
            // 
            this.tabPage2.BackColor = System.Drawing.Color.Transparent;
            this.tabPage2.Controls.Add(this.NpcsSearchTextbox);
            this.tabPage2.Controls.Add(this.ContinueNpcsSearch);
            this.tabPage2.Controls.Add(this.NpcsGrid);
            this.tabPage2.Controls.Add(this.label2);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(356, 467);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "NPCs";
            // 
            // NpcsSearchTextbox
            // 
            this.NpcsSearchTextbox.Location = new System.Drawing.Point(48, 445);
            this.NpcsSearchTextbox.Name = "NpcsSearchTextbox";
            this.NpcsSearchTextbox.Size = new System.Drawing.Size(278, 20);
            this.NpcsSearchTextbox.TabIndex = 21;
            this.NpcsSearchTextbox.Text = "Search:Enter name or ID";
            this.NpcsSearchTextbox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.NpcsSearchTextbox.TextChanged += new System.EventHandler(this.NpcsSearchTextbox_TextChanged);
            this.NpcsSearchTextbox.DoubleClick += new System.EventHandler(this.NpcsSearchTextbox_DoubleClick);
            // 
            // ContinueNpcsSearch
            // 
            this.ContinueNpcsSearch.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ContinueNpcsSearch.Location = new System.Drawing.Point(328, 445);
            this.ContinueNpcsSearch.Name = "ContinueNpcsSearch";
            this.ContinueNpcsSearch.Size = new System.Drawing.Size(26, 20);
            this.ContinueNpcsSearch.TabIndex = 20;
            this.ContinueNpcsSearch.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.ContinueNpcsSearch.UseVisualStyleBackColor = true;
            this.ContinueNpcsSearch.Click += new System.EventHandler(this.ContinueNpcsSearch_Click);
            // 
            // NpcsGrid
            // 
            this.NpcsGrid.AllowUserToAddRows = false;
            this.NpcsGrid.AllowUserToDeleteRows = false;
            this.NpcsGrid.AllowUserToResizeColumns = false;
            this.NpcsGrid.AllowUserToResizeRows = false;
            this.NpcsGrid.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.NpcsGrid.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.NpcsGrid.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None;
            this.NpcsGrid.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            this.NpcsGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.NpcsGrid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewTextBoxColumn1,
            this.dataGridViewTextBoxColumn2,
            this.Column4});
            dataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle8.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            dataGridViewCellStyle8.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            dataGridViewCellStyle8.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            dataGridViewCellStyle8.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle8.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle8.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.NpcsGrid.DefaultCellStyle = dataGridViewCellStyle8;
            this.NpcsGrid.EnableHeadersVisualStyles = false;
            this.NpcsGrid.Location = new System.Drawing.Point(0, 0);
            this.NpcsGrid.MultiSelect = false;
            this.NpcsGrid.Name = "NpcsGrid";
            this.NpcsGrid.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            this.NpcsGrid.RowHeadersVisible = false;
            this.NpcsGrid.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.NpcsGrid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.NpcsGrid.ShowCellErrors = false;
            this.NpcsGrid.ShowCellToolTips = false;
            this.NpcsGrid.ShowEditingIcon = false;
            this.NpcsGrid.ShowRowErrors = false;
            this.NpcsGrid.Size = new System.Drawing.Size(356, 443);
            this.NpcsGrid.TabIndex = 14;
            this.NpcsGrid.CellMouseDoubleClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.NpcsGrid_CellMouseDoubleClick);
            // 
            // dataGridViewTextBoxColumn1
            // 
            dataGridViewCellStyle5.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.dataGridViewTextBoxColumn1.DefaultCellStyle = dataGridViewCellStyle5;
            this.dataGridViewTextBoxColumn1.FillWeight = 6.912442F;
            this.dataGridViewTextBoxColumn1.HeaderText = "#";
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            this.dataGridViewTextBoxColumn1.ReadOnly = true;
            this.dataGridViewTextBoxColumn1.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.dataGridViewTextBoxColumn1.Width = 45;
            // 
            // dataGridViewTextBoxColumn2
            // 
            dataGridViewCellStyle6.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            dataGridViewCellStyle6.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(251)))), ((int)(((byte)(251)))), ((int)(((byte)(107)))));
            this.dataGridViewTextBoxColumn2.DefaultCellStyle = dataGridViewCellStyle6;
            this.dataGridViewTextBoxColumn2.FillWeight = 44.7233F;
            this.dataGridViewTextBoxColumn2.HeaderText = "  ID";
            this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            this.dataGridViewTextBoxColumn2.ReadOnly = true;
            this.dataGridViewTextBoxColumn2.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.dataGridViewTextBoxColumn2.Width = 60;
            // 
            // Column4
            // 
            dataGridViewCellStyle7.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(251)))), ((int)(((byte)(251)))), ((int)(((byte)(107)))));
            this.Column4.DefaultCellStyle = dataGridViewCellStyle7;
            this.Column4.HeaderText = "Name";
            this.Column4.Name = "Column4";
            this.Column4.ReadOnly = true;
            this.Column4.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.Column4.Width = 250;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label2.Location = new System.Drawing.Point(1, 447);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(45, 15);
            this.label2.TabIndex = 22;
            this.label2.Text = "Search:";
            // 
            // tabPage3
            // 
            this.tabPage3.BackColor = System.Drawing.Color.Transparent;
            this.tabPage3.Controls.Add(this.ResourcesSearchTextbox);
            this.tabPage3.Controls.Add(this.ContinueResourcesSearch);
            this.tabPage3.Controls.Add(this.ResourcesGrid);
            this.tabPage3.Controls.Add(this.label3);
            this.tabPage3.Location = new System.Drawing.Point(4, 22);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage3.Size = new System.Drawing.Size(356, 467);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "Resources";
            // 
            // ResourcesSearchTextbox
            // 
            this.ResourcesSearchTextbox.Location = new System.Drawing.Point(48, 445);
            this.ResourcesSearchTextbox.Name = "ResourcesSearchTextbox";
            this.ResourcesSearchTextbox.Size = new System.Drawing.Size(278, 20);
            this.ResourcesSearchTextbox.TabIndex = 21;
            this.ResourcesSearchTextbox.Text = "Search:Enter name or ID";
            this.ResourcesSearchTextbox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.ResourcesSearchTextbox.TextChanged += new System.EventHandler(this.ResourcesSearchTextbox_TextChanged);
            this.ResourcesSearchTextbox.DoubleClick += new System.EventHandler(this.ResourcesSearchTextbox_DoubleClick);
            // 
            // ContinueResourcesSearch
            // 
            this.ContinueResourcesSearch.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ContinueResourcesSearch.Location = new System.Drawing.Point(328, 445);
            this.ContinueResourcesSearch.Name = "ContinueResourcesSearch";
            this.ContinueResourcesSearch.Size = new System.Drawing.Size(26, 20);
            this.ContinueResourcesSearch.TabIndex = 20;
            this.ContinueResourcesSearch.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.ContinueResourcesSearch.UseVisualStyleBackColor = true;
            this.ContinueResourcesSearch.Click += new System.EventHandler(this.ContinueResourcesSearch_Click);
            // 
            // ResourcesGrid
            // 
            this.ResourcesGrid.AllowUserToAddRows = false;
            this.ResourcesGrid.AllowUserToDeleteRows = false;
            this.ResourcesGrid.AllowUserToResizeColumns = false;
            this.ResourcesGrid.AllowUserToResizeRows = false;
            this.ResourcesGrid.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ResourcesGrid.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.ResourcesGrid.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None;
            this.ResourcesGrid.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            this.ResourcesGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.ResourcesGrid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewTextBoxColumn6,
            this.dataGridViewTextBoxColumn7,
            this.dataGridViewTextBoxColumn8});
            dataGridViewCellStyle12.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle12.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            dataGridViewCellStyle12.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            dataGridViewCellStyle12.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            dataGridViewCellStyle12.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle12.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle12.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.ResourcesGrid.DefaultCellStyle = dataGridViewCellStyle12;
            this.ResourcesGrid.EnableHeadersVisualStyles = false;
            this.ResourcesGrid.Location = new System.Drawing.Point(0, 0);
            this.ResourcesGrid.MultiSelect = false;
            this.ResourcesGrid.Name = "ResourcesGrid";
            this.ResourcesGrid.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            this.ResourcesGrid.RowHeadersVisible = false;
            this.ResourcesGrid.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.ResourcesGrid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.ResourcesGrid.ShowCellErrors = false;
            this.ResourcesGrid.ShowCellToolTips = false;
            this.ResourcesGrid.ShowEditingIcon = false;
            this.ResourcesGrid.ShowRowErrors = false;
            this.ResourcesGrid.Size = new System.Drawing.Size(356, 443);
            this.ResourcesGrid.TabIndex = 15;
            this.ResourcesGrid.CellMouseDoubleClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.ResourcesGrid_CellMouseDoubleClick);
            // 
            // dataGridViewTextBoxColumn6
            // 
            dataGridViewCellStyle9.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.dataGridViewTextBoxColumn6.DefaultCellStyle = dataGridViewCellStyle9;
            this.dataGridViewTextBoxColumn6.FillWeight = 6.912442F;
            this.dataGridViewTextBoxColumn6.HeaderText = "#";
            this.dataGridViewTextBoxColumn6.Name = "dataGridViewTextBoxColumn6";
            this.dataGridViewTextBoxColumn6.ReadOnly = true;
            this.dataGridViewTextBoxColumn6.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.dataGridViewTextBoxColumn6.Width = 45;
            // 
            // dataGridViewTextBoxColumn7
            // 
            dataGridViewCellStyle10.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            dataGridViewCellStyle10.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(77)))), ((int)(((byte)(255)))), ((int)(((byte)(143)))));
            this.dataGridViewTextBoxColumn7.DefaultCellStyle = dataGridViewCellStyle10;
            this.dataGridViewTextBoxColumn7.FillWeight = 44.7233F;
            this.dataGridViewTextBoxColumn7.HeaderText = "  ID";
            this.dataGridViewTextBoxColumn7.Name = "dataGridViewTextBoxColumn7";
            this.dataGridViewTextBoxColumn7.ReadOnly = true;
            this.dataGridViewTextBoxColumn7.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.dataGridViewTextBoxColumn7.Width = 60;
            // 
            // dataGridViewTextBoxColumn8
            // 
            dataGridViewCellStyle11.ForeColor = System.Drawing.Color.White;
            this.dataGridViewTextBoxColumn8.DefaultCellStyle = dataGridViewCellStyle11;
            this.dataGridViewTextBoxColumn8.HeaderText = "Name";
            this.dataGridViewTextBoxColumn8.Name = "dataGridViewTextBoxColumn8";
            this.dataGridViewTextBoxColumn8.ReadOnly = true;
            this.dataGridViewTextBoxColumn8.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.dataGridViewTextBoxColumn8.Width = 250;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label3.Location = new System.Drawing.Point(1, 447);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(45, 15);
            this.label3.TabIndex = 22;
            this.label3.Text = "Search:";
            // 
            // CancelButton
            // 
            this.CancelButton.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.CancelButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.CancelButton.Image = ((System.Drawing.Image)(resources.GetObject("CancelButton.Image")));
            this.CancelButton.Location = new System.Drawing.Point(182, 493);
            this.CancelButton.Name = "CancelButton";
            this.CancelButton.Size = new System.Drawing.Size(181, 37);
            this.CancelButton.TabIndex = 2;
            this.CancelButton.Text = "Cancel";
            this.CancelButton.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.CancelButton.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.CancelButton.UseVisualStyleBackColor = true;
            this.CancelButton.Click += new System.EventHandler(this.CancelButton_Click);
            // 
            // AcceptButton
            // 
            this.AcceptButton.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.AcceptButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.AcceptButton.Image = ((System.Drawing.Image)(resources.GetObject("AcceptButton.Image")));
            this.AcceptButton.Location = new System.Drawing.Point(2, 493);
            this.AcceptButton.Name = "AcceptButton";
            this.AcceptButton.Size = new System.Drawing.Size(181, 37);
            this.AcceptButton.TabIndex = 1;
            this.AcceptButton.Text = "Choose";
            this.AcceptButton.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.AcceptButton.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.AcceptButton.UseVisualStyleBackColor = true;
            this.AcceptButton.Click += new System.EventHandler(this.AcceptButton_Click);
            // 
            // MobsNpcsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(239)))), ((int)(((byte)(244)))), ((int)(((byte)(250)))));
            this.ClientSize = new System.Drawing.Size(366, 531);
            this.Controls.Add(this.CancelButton);
            this.Controls.Add(this.AcceptButton);
            this.Controls.Add(this.tabControl1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "MobsNpcsForm";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Choose Mobs or Npcs";
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.MobsGrid)).EndInit();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.NpcsGrid)).EndInit();
            this.tabPage3.ResumeLayout(false);
            this.tabPage3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ResourcesGrid)).EndInit();
            this.ResumeLayout(false);

	}
}
