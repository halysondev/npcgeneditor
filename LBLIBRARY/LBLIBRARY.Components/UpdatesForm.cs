using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace LBLIBRARY.Components;

public class UpdatesForm : Form
{
	private bool isTopPanelDragged;

	private bool isWindowMaximized;

	private Point offset;

	private Size _normalWindowSize;

	private Point _normalWindowLocation = Point.Empty;

	private int Language = 1;

	private IContainer components;

	private Panel TopPanel;

	private ButtonB ClosseWindow;

	private Label WindowTextLabel;

	private ToolTip toolTip1;

	private OpenFileDialog DialogOpenTask;

	private RichTextBox UpdatesBox;

	private ComboBoxA comboBoxEx1;

	public int SetLanguage
	{
		get
		{
			return Language;
		}
		set
		{
			Language = value;
			if (Language == 1)
			{
				comboBoxEx1.SelectedIndex = 0;
			}
			else
			{
				comboBoxEx1.SelectedIndex = 1;
			}
		}
	}

	public UpdatesForm()
	{
		InitializeComponent();
	}

	private void TopPanel_MouseDown(object sender, MouseEventArgs e)
	{
		if (e.Button == MouseButtons.Left)
		{
			isTopPanelDragged = true;
			Point point = PointToScreen(new Point(e.X, e.Y));
			offset = new Point
			{
				X = base.Location.X - point.X,
				Y = base.Location.Y - point.Y
			};
		}
		else
		{
			isTopPanelDragged = false;
		}
		if (e.Clicks == 2)
		{
			isTopPanelDragged = false;
		}
	}

	private void TopPanel_MouseMove(object sender, MouseEventArgs e)
	{
		if (isTopPanelDragged)
		{
			Point location = TopPanel.PointToScreen(new Point(e.X, e.Y));
			location.Offset(offset);
			base.Location = location;
			if ((base.Location.X > 2 || base.Location.Y > 2) && base.WindowState == FormWindowState.Maximized)
			{
				base.Location = _normalWindowLocation;
				base.Size = _normalWindowSize;
				isWindowMaximized = false;
			}
		}
	}

	private void TopPanel_MouseUp(object sender, MouseEventArgs e)
	{
		isTopPanelDragged = false;
		if (base.Location.Y <= 5 && !isWindowMaximized)
		{
			_normalWindowSize = base.Size;
			_normalWindowLocation = base.Location;
			Rectangle workingArea = Screen.PrimaryScreen.WorkingArea;
			base.Location = new Point(0, 0);
			base.Size = new Size(workingArea.Width, workingArea.Height);
			isWindowMaximized = true;
		}
	}

	private void _CloseButton_Click(object sender, EventArgs e)
	{
		Close();
	}

	private void WindowTextLabel_MouseDown(object sender, MouseEventArgs e)
	{
		TopPanel_MouseDown(sender, e);
	}

	private void WindowTextLabel_MouseMove(object sender, MouseEventArgs e)
	{
		TopPanel_MouseMove(sender, e);
	}

	private void WindowTextLabel_MouseUp(object sender, MouseEventArgs e)
	{
		TopPanel_MouseUp(sender, e);
	}

	public void SetText(List<string> Text)
	{
		string[] lines;
		if (Language == 1)
		{
			foreach (string item in Text)
			{
				if (!item.StartsWith("В"))
				{
					RichTextBox updatesBox = UpdatesBox;
					updatesBox.Text = updatesBox.Text + item + "\n";
				}
				else
				{
					UpdatesBox.Text += $"\t\t\t\t\t\t     {item}\n";
				}
			}
			lines = UpdatesBox.Lines;
			foreach (string text in lines)
			{
				if (text.StartsWith("\t\t\t\t\t\t     В"))
				{
					UpdatesBox.Select(UpdatesBox.Text.IndexOf(text), text.Length);
					UpdatesBox.SelectionColor = Color.Green;
					UpdatesBox.SelectionFont = new Font("Segui UI", 12f);
				}
			}
			UpdatesBox.AutoScrollOffset = new Point(0, 1000);
			UpdatesBox.Select(0, 0);
			return;
		}
		foreach (string item2 in Text)
		{
			if (!item2.StartsWith("V"))
			{
				RichTextBox updatesBox2 = UpdatesBox;
				updatesBox2.Text = updatesBox2.Text + item2 + "\n";
			}
			else
			{
				UpdatesBox.Text += $"\t\t\t\t\t\t     {item2}\n";
			}
		}
		lines = UpdatesBox.Lines;
		foreach (string text2 in lines)
		{
			if (text2.StartsWith("\t\t\t\t\t\t     V"))
			{
				UpdatesBox.Select(UpdatesBox.Text.IndexOf(text2), text2.Length);
				UpdatesBox.SelectionColor = Color.Green;
				UpdatesBox.SelectionFont = new Font("Segui UI", 12f);
			}
		}
		UpdatesBox.AutoScrollOffset = new Point(0, 1000);
		UpdatesBox.Select(0, 0);
	}

	private void ChangeIndex(object sender, EventArgs e)
	{
		UpdatesBox.Text = "";
		if (comboBoxEx1.SelectedIndex == 0)
		{
			Language = 1;
			WindowTextLabel.Text = "История обновлений";
			SetText(File.ReadAllLines(Application.StartupPath + "\\Changelog_Ru.txt", Encoding.GetEncoding(1251)).ToList());
		}
		else
		{
			Language = 2;
			WindowTextLabel.Text = "Updates history";
			SetText(File.ReadAllLines(Application.StartupPath + "\\Changelog_En.txt", Encoding.GetEncoding(1251)).ToList());
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
		this.TopPanel = new System.Windows.Forms.Panel();
		this.comboBoxEx1 = new LBLIBRARY.Components.ComboBoxA();
		this.WindowTextLabel = new System.Windows.Forms.Label();
		this.ClosseWindow = new LBLIBRARY.Components.ButtonB();
		this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
		this.DialogOpenTask = new System.Windows.Forms.OpenFileDialog();
		this.UpdatesBox = new System.Windows.Forms.RichTextBox();
		this.TopPanel.SuspendLayout();
		base.SuspendLayout();
		this.TopPanel.BackColor = System.Drawing.Color.FromArgb(220, 120, 80);
		this.TopPanel.Controls.Add(this.comboBoxEx1);
		this.TopPanel.Controls.Add(this.WindowTextLabel);
		this.TopPanel.Controls.Add(this.ClosseWindow);
		this.TopPanel.Dock = System.Windows.Forms.DockStyle.Top;
		this.TopPanel.Location = new System.Drawing.Point(0, 0);
		this.TopPanel.Name = "TopPanel";
		this.TopPanel.Size = new System.Drawing.Size(730, 33);
		this.TopPanel.TabIndex = 4;
		this.TopPanel.MouseDown += new System.Windows.Forms.MouseEventHandler(TopPanel_MouseDown);
		this.TopPanel.MouseMove += new System.Windows.Forms.MouseEventHandler(TopPanel_MouseMove);
		this.TopPanel.MouseUp += new System.Windows.Forms.MouseEventHandler(TopPanel_MouseUp);
		this.comboBoxEx1.ArrowColor = System.Drawing.Color.Black;
		this.comboBoxEx1.BackColor = System.Drawing.Color.FromArgb(255, 224, 192);
		this.comboBoxEx1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
		this.comboBoxEx1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
		this.comboBoxEx1.FormattingEnabled = true;
		this.comboBoxEx1.Items.AddRange(new object[2] { "Russian", "English" });
		this.comboBoxEx1.Location = new System.Drawing.Point(0, 0);
		this.comboBoxEx1.Name = "comboBoxEx1";
		this.comboBoxEx1.SelectionColor = System.Drawing.Color.FromArgb(192, 255, 255);
		this.comboBoxEx1.SetColorBlack = LBLIBRARY.Components.ComboBoxA.MyEnum.None;
		this.comboBoxEx1.SetFont = new System.Drawing.Font("Microsoft Sans Serif", 9f);
		this.comboBoxEx1.Size = new System.Drawing.Size(128, 21);
		this.comboBoxEx1.TabIndex = 2;
		this.comboBoxEx1.SelectedIndexChanged += new System.EventHandler(ChangeIndex);
		this.WindowTextLabel.AutoSize = true;
		this.WindowTextLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 204);
		this.WindowTextLabel.ForeColor = System.Drawing.Color.White;
		this.WindowTextLabel.Location = new System.Drawing.Point(270, 5);
		this.WindowTextLabel.Name = "WindowTextLabel";
		this.WindowTextLabel.Size = new System.Drawing.Size(200, 24);
		this.WindowTextLabel.TabIndex = 1;
		this.WindowTextLabel.Text = "История обновлений";
		this.WindowTextLabel.MouseDown += new System.Windows.Forms.MouseEventHandler(WindowTextLabel_MouseDown);
		this.WindowTextLabel.MouseMove += new System.Windows.Forms.MouseEventHandler(WindowTextLabel_MouseMove);
		this.WindowTextLabel.MouseUp += new System.Windows.Forms.MouseEventHandler(WindowTextLabel_MouseUp);
		this.ClosseWindow.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.ClosseWindow.BZBackColor = System.Drawing.Color.FromArgb(220, 120, 80);
		this.ClosseWindow.DisplayText = "X";
		this.ClosseWindow.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
		this.ClosseWindow.Font = new System.Drawing.Font("Microsoft YaHei UI", 12f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.ClosseWindow.ForeColor = System.Drawing.Color.White;
		this.ClosseWindow.Location = new System.Drawing.Point(699, 2);
		this.ClosseWindow.MouseClickColor1 = System.Drawing.Color.FromArgb(60, 60, 160);
		this.ClosseWindow.MouseHoverColor = System.Drawing.Color.FromArgb(50, 50, 50);
		this.ClosseWindow.Name = "ClosseWindow";
		this.ClosseWindow.Size = new System.Drawing.Size(31, 30);
		this.ClosseWindow.TabIndex = 0;
		this.ClosseWindow.Text = "X";
		this.ClosseWindow.TextLocation_X = 6;
		this.ClosseWindow.TextLocation_Y = 5;
		this.toolTip1.SetToolTip(this.ClosseWindow, "Close");
		this.ClosseWindow.UseVisualStyleBackColor = true;
		this.ClosseWindow.Click += new System.EventHandler(_CloseButton_Click);
		this.DialogOpenTask.FileName = "Tasks";
		this.DialogOpenTask.Filter = "Tasks.data|*.data|All Files|*.*";
		this.UpdatesBox.BackColor = System.Drawing.Color.WhiteSmoke;
		this.UpdatesBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
		this.UpdatesBox.Cursor = System.Windows.Forms.Cursors.SizeNS;
		this.UpdatesBox.Location = new System.Drawing.Point(3, 33);
		this.UpdatesBox.Name = "UpdatesBox";
		this.UpdatesBox.ReadOnly = true;
		this.UpdatesBox.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.Vertical;
		this.UpdatesBox.Size = new System.Drawing.Size(727, 438);
		this.UpdatesBox.TabIndex = 5;
		this.UpdatesBox.Text = "";
		base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		this.BackColor = System.Drawing.Color.WhiteSmoke;
		base.ClientSize = new System.Drawing.Size(730, 473);
		base.Controls.Add(this.UpdatesBox);
		base.Controls.Add(this.TopPanel);
		base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
		base.Name = "UpdatesForm";
		base.ShowInTaskbar = false;
		base.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
		this.Text = "My App";
		this.TopPanel.ResumeLayout(false);
		this.TopPanel.PerformLayout();
		base.ResumeLayout(false);
	}
}
