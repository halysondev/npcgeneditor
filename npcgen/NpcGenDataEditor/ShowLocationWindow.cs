using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using Cyotek.Windows.Forms;
using NpcGenDataEditor.Properties;

namespace NpcGenDataEditor;

public class ShowLocationWindow : Form
{
	private Form1 Main_form;

	private Bitmap MapImage;

	private IContainer components = null;

	private ImageBox pictureBox1;

	private ContextMenuStrip contextMenuStrip1;

	private ToolStripMenuItem toolStripMenuItem1;

	public ShowLocationWindow(Form1 fm, Bitmap fff)
	{
		Main_form = fm;
		InitializeComponent();
		MapImage = fff;
		pictureBox1.Image = fff;
	}

	public void GetCoordinates(List<PointF> ls)
	{
		pictureBox1.Zoom = 100;
		Graphics graphics = Graphics.FromImage(pictureBox1.Image);
		int num = 1;
		if (pictureBox1.Image.Size != new Size(8192, 11264))
		{
			num = 2;
		}
		foreach (PointF l in ls)
		{
			float num2 = l.X / (float)num + (float)(pictureBox1.Image.Width / 2) - 10f;
			float num3 = Math.Abs(l.Y / (float)num - (float)(pictureBox1.Image.Height / 2)) - 8f;
			if (num2 <= (float)pictureBox1.Image.Width && num3 <= (float)pictureBox1.Image.Height)
			{
				graphics.DrawImage(Resources.arrow, num2, num3);
			}
		}
		pictureBox1.ScrollTo(Convert.ToInt32(ls[ls.Count - 1].X + (float)(pictureBox1.Image.Width / 2)) - pictureBox1.DisplayRectangle.Width / 2, Convert.ToInt32(Math.Abs(ls[ls.Count - 1].Y - (float)(pictureBox1.Image.Height / 2))) - pictureBox1.DisplayRectangle.Height / 2);
		pictureBox1.Refresh();
	}

	public void SetPicture(Bitmap bmz)
	{
		pictureBox1.Image = bmz;
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ShowLocationWindow));
            this.pictureBox1 = new Cyotek.Windows.Forms.ImageBox();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // pictureBox1
            // 
            this.pictureBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureBox1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(25)))), ((int)(((byte)(25)))));
            this.pictureBox1.GridDisplayMode = Cyotek.Windows.Forms.ImageBoxGridDisplayMode.None;
            this.pictureBox1.Location = new System.Drawing.Point(-1, 1);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(535, 611);
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem1});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(155, 26);
            this.contextMenuStrip1.Text = "Additionally";
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Checked = true;
            this.toolStripMenuItem1.CheckState = System.Windows.Forms.CheckState.Checked;
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(154, 22);
            this.toolStripMenuItem1.Text = "Edit";
            // 
            // ShowLocationWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.ClientSize = new System.Drawing.Size(534, 611);
            this.Controls.Add(this.pictureBox1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "ShowLocationWindow";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Map";
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);

	}
}
