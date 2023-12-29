using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace LBLIBRARY.Components;

public class BlackForm : Form
{
	private bool isTopPanelDragged;

	private bool isLeftPanelDragged;

	private bool isRightPanelDragged;

	private bool isBottomPanelDragged;

	private bool isTopBorderPanelDragged;

	private bool isRightBottomPanelDragged;

	private bool isLeftBottomPanelDragged;

	private bool isRightTopPanelDragged;

	private bool isLeftTopPanelDragged;

	private bool isWindowMaximized;

	private Point offset;

	private Size _normalWindowSize;

	private Point _normalWindowLocation = Point.Empty;

	private bool ResizeFoldingVisible = true;

	private IContainer components;

	protected Panel TopBorderPanel;

	protected Panel RightPanel;

	protected Panel LeftPanel;

	protected Panel BottomPanel;

	protected ButtonB _CloseButton;

	protected Panel RightBottomPanel_1;

	protected Label WindowTextLabel;

	protected ButtonD _MaxButton;

	protected ButtonB _MinButton;

	protected ToolTip toolTip1;

	protected Panel RightBottomPanel_2;

	protected Panel LeftBottomPanel_1;

	protected Panel LeftBottomPanel_2;

	protected Panel RightTopPanel_1;

	protected Panel RightTopPanel_2;

	protected Panel LeftTopPanel_1;

	protected Panel LeftTopPanel_2;

	protected Panel TopPanel;

	[Category("Own")]
	public bool EnableResizeAndFold
	{
		get
		{
			return ResizeFoldingVisible;
		}
		set
		{
			ResizeFoldingVisible = value;
			if (ResizeFoldingVisible)
			{
				_MinButton.Visible = true;
				_MaxButton.Visible = true;
			}
			else
			{
				_MinButton.Visible = false;
				_MaxButton.Visible = false;
			}
		}
	}

	public BlackForm()
	{
		InitializeComponent();
	}

	private void TopBorderPanel_MouseDown(object sender, MouseEventArgs e)
	{
		if (e.Button == MouseButtons.Left)
		{
			isTopBorderPanelDragged = true;
		}
		else
		{
			isTopBorderPanelDragged = false;
		}
	}

	private void TopBorderPanel_MouseMove(object sender, MouseEventArgs e)
	{
		if (e.Y < base.Location.Y && isTopBorderPanelDragged)
		{
			if (base.Height < 50)
			{
				base.Height = 50;
				isTopBorderPanelDragged = false;
			}
			else
			{
				base.Location = new Point(base.Location.X, base.Location.Y + e.Y);
				base.Height -= e.Y;
			}
		}
	}

	private void TopBorderPanel_MouseUp(object sender, MouseEventArgs e)
	{
		isTopBorderPanelDragged = false;
	}

	private void TopPanel_MouseDown(object sender, MouseEventArgs e)
	{
		if (e.Button == MouseButtons.Left)
		{
			isTopPanelDragged = true;
			Point point = PointToScreen(new Point(e.X, e.Y));
			offset = default(Point);
			offset.X = base.Location.X - point.X;
			offset.Y = base.Location.Y - point.Y;
		}
		else
		{
			isTopPanelDragged = false;
		}
		if (e.Clicks == 2)
		{
			isTopPanelDragged = false;
			_MaxButton_Click(sender, e);
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
				toolTip1.SetToolTip(_MaxButton, "Maximize");
				_MaxButton.CFormState = ButtonD.CustomFormState.Normal;
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
			toolTip1.SetToolTip(_MaxButton, "Restore Down");
			_MaxButton.CFormState = ButtonD.CustomFormState.Maximize;
			isWindowMaximized = true;
		}
	}

	private void LeftPanel_MouseDown(object sender, MouseEventArgs e)
	{
		if (base.Location.X <= 0 || e.X < 0)
		{
			isLeftPanelDragged = false;
			base.Location = new Point(10, base.Location.Y);
		}
		else if (e.Button == MouseButtons.Left)
		{
			isLeftPanelDragged = true;
		}
		else
		{
			isLeftPanelDragged = false;
		}
	}

	private void LeftPanel_MouseMove(object sender, MouseEventArgs e)
	{
		if (e.X < base.Location.X && isLeftPanelDragged)
		{
			if (base.Width < 100)
			{
				base.Width = 100;
				isLeftPanelDragged = false;
			}
			else
			{
				base.Location = new Point(base.Location.X + e.X, base.Location.Y);
				base.Width -= e.X;
			}
		}
	}

	private void LeftPanel_MouseUp(object sender, MouseEventArgs e)
	{
		isLeftPanelDragged = false;
	}

	private void RightPanel_MouseDown(object sender, MouseEventArgs e)
	{
		if (e.Button == MouseButtons.Left)
		{
			isRightPanelDragged = true;
		}
		else
		{
			isRightPanelDragged = false;
		}
	}

	private void RightPanel_MouseMove(object sender, MouseEventArgs e)
	{
		if (isRightPanelDragged)
		{
			if (base.Width < 100)
			{
				base.Width = 100;
				isRightPanelDragged = false;
			}
			else
			{
				base.Width += e.X;
			}
		}
	}

	private void RightPanel_MouseUp(object sender, MouseEventArgs e)
	{
		isRightPanelDragged = false;
	}

	private void BottomPanel_MouseDown(object sender, MouseEventArgs e)
	{
		if (e.Button == MouseButtons.Left)
		{
			isBottomPanelDragged = true;
		}
		else
		{
			isBottomPanelDragged = false;
		}
	}

	private void BottomPanel_MouseMove(object sender, MouseEventArgs e)
	{
		if (isBottomPanelDragged)
		{
			if (base.Height < 50)
			{
				base.Height = 50;
				isBottomPanelDragged = false;
			}
			else
			{
				base.Height += e.Y;
			}
		}
	}

	private void BottomPanel_MouseUp(object sender, MouseEventArgs e)
	{
		isBottomPanelDragged = false;
	}

	private void _MinButton_Click(object sender, EventArgs e)
	{
		base.WindowState = FormWindowState.Minimized;
	}

	private void _MaxButton_Click(object sender, EventArgs e)
	{
		if (isWindowMaximized)
		{
			base.Location = _normalWindowLocation;
			base.Size = _normalWindowSize;
			toolTip1.SetToolTip(_MaxButton, "Maximize");
			_MaxButton.CFormState = ButtonD.CustomFormState.Normal;
			isWindowMaximized = false;
		}
		else
		{
			_normalWindowSize = base.Size;
			_normalWindowLocation = base.Location;
			Rectangle workingArea = Screen.PrimaryScreen.WorkingArea;
			base.Location = new Point(0, 0);
			base.Size = new Size(workingArea.Width, workingArea.Height);
			toolTip1.SetToolTip(_MaxButton, "Restore Down");
			_MaxButton.CFormState = ButtonD.CustomFormState.Maximize;
			isWindowMaximized = true;
		}
	}

	private void _CloseButton_Click(object sender, EventArgs e)
	{
		Close();
	}

	private void RightBottomPanel_1_MouseDown(object sender, MouseEventArgs e)
	{
		isRightBottomPanelDragged = true;
	}

	private void RightBottomPanel_1_MouseMove(object sender, MouseEventArgs e)
	{
		if (isRightBottomPanelDragged)
		{
			if (base.Width < 100 || base.Height < 50)
			{
				base.Width = 100;
				base.Height = 50;
				isRightBottomPanelDragged = false;
			}
			else
			{
				base.Width += e.X;
				base.Height += e.Y;
			}
		}
	}

	private void RightBottomPanel_1_MouseUp(object sender, MouseEventArgs e)
	{
		isRightBottomPanelDragged = false;
	}

	private void RightBottomPanel_2_MouseDown(object sender, MouseEventArgs e)
	{
		RightBottomPanel_1_MouseDown(sender, e);
	}

	private void RightBottomPanel_2_MouseMove(object sender, MouseEventArgs e)
	{
		RightBottomPanel_1_MouseMove(sender, e);
	}

	private void RightBottomPanel_2_MouseUp(object sender, MouseEventArgs e)
	{
		RightBottomPanel_1_MouseUp(sender, e);
	}

	private void LeftBottomPanel_1_MouseDown(object sender, MouseEventArgs e)
	{
		isLeftBottomPanelDragged = true;
	}

	private void LeftBottomPanel_1_MouseMove(object sender, MouseEventArgs e)
	{
		if (e.X < base.Location.X && (isLeftBottomPanelDragged || base.Height < 50))
		{
			if (base.Width < 100)
			{
				base.Width = 100;
				base.Height = 50;
				isLeftBottomPanelDragged = false;
			}
			else
			{
				base.Location = new Point(base.Location.X + e.X, base.Location.Y);
				base.Width -= e.X;
				base.Height += e.Y;
			}
		}
	}

	private void LeftBottomPanel_1_MouseUp(object sender, MouseEventArgs e)
	{
		isLeftBottomPanelDragged = false;
	}

	private void LeftBottomPanel_2_MouseDown(object sender, MouseEventArgs e)
	{
		LeftBottomPanel_1_MouseDown(sender, e);
	}

	private void LeftBottomPanel_2_MouseMove(object sender, MouseEventArgs e)
	{
		LeftBottomPanel_1_MouseMove(sender, e);
	}

	private void LeftBottomPanel_2_MouseUp(object sender, MouseEventArgs e)
	{
		LeftBottomPanel_1_MouseUp(sender, e);
	}

	private void RightTopPanel_1_MouseDown(object sender, MouseEventArgs e)
	{
		isRightTopPanelDragged = true;
	}

	private void RightTopPanel_1_MouseMove(object sender, MouseEventArgs e)
	{
		if ((e.Y < base.Location.Y || e.X < base.Location.X) && isRightTopPanelDragged)
		{
			if (base.Height < 50 || base.Width < 100)
			{
				base.Height = 50;
				base.Width = 100;
				isRightTopPanelDragged = false;
			}
			else
			{
				base.Location = new Point(base.Location.X, base.Location.Y + e.Y);
				base.Height -= e.Y;
				base.Width += e.X;
			}
		}
	}

	private void RightTopPanel_1_MouseUp(object sender, MouseEventArgs e)
	{
		isRightTopPanelDragged = false;
	}

	private void RightTopPanel_2_MouseDown(object sender, MouseEventArgs e)
	{
		RightTopPanel_1_MouseDown(sender, e);
	}

	private void RightTopPanel_2_MouseMove(object sender, MouseEventArgs e)
	{
		RightTopPanel_1_MouseMove(sender, e);
	}

	private void RightTopPanel_2_MouseUp(object sender, MouseEventArgs e)
	{
		RightTopPanel_1_MouseUp(sender, e);
	}

	private void LeftTopPanel_1_MouseDown(object sender, MouseEventArgs e)
	{
		isLeftTopPanelDragged = true;
	}

	private void LeftTopPanel_1_MouseMove(object sender, MouseEventArgs e)
	{
		if ((e.X < base.Location.X || e.Y < base.Location.Y) && isLeftTopPanelDragged)
		{
			if (base.Width < 100 || base.Height < 50)
			{
				base.Width = 100;
				base.Height = 100;
				isLeftTopPanelDragged = false;
			}
			else
			{
				base.Location = new Point(base.Location.X + e.X, base.Location.Y);
				base.Width -= e.X;
				base.Location = new Point(base.Location.X, base.Location.Y + e.Y);
				base.Height -= e.Y;
			}
		}
	}

	private void LeftTopPanel_1_MouseUp(object sender, MouseEventArgs e)
	{
		isLeftTopPanelDragged = false;
	}

	private void LeftTopPanel_2_MouseDown(object sender, MouseEventArgs e)
	{
		LeftTopPanel_1_MouseDown(sender, e);
	}

	private void LeftTopPanel_2_MouseMove(object sender, MouseEventArgs e)
	{
		LeftTopPanel_1_MouseMove(sender, e);
	}

	private void LeftTopPanel_2_MouseUp(object sender, MouseEventArgs e)
	{
		LeftTopPanel_1_MouseUp(sender, e);
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

	private void BlackForm_Resize(object sender, EventArgs e)
	{
		WindowTextLabel.Location = new Point(base.Width / 2 - TextRenderer.MeasureText(WindowTextLabel.Text, WindowTextLabel.Font).Width / 2, WindowTextLabel.Location.Y);
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
		this.TopBorderPanel = new System.Windows.Forms.Panel();
		this.RightPanel = new System.Windows.Forms.Panel();
		this.LeftPanel = new System.Windows.Forms.Panel();
		this.BottomPanel = new System.Windows.Forms.Panel();
		this.TopPanel = new System.Windows.Forms.Panel();
		this.WindowTextLabel = new System.Windows.Forms.Label();
		this.RightBottomPanel_1 = new System.Windows.Forms.Panel();
		this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
		this.RightBottomPanel_2 = new System.Windows.Forms.Panel();
		this.LeftBottomPanel_1 = new System.Windows.Forms.Panel();
		this.LeftBottomPanel_2 = new System.Windows.Forms.Panel();
		this.RightTopPanel_1 = new System.Windows.Forms.Panel();
		this.RightTopPanel_2 = new System.Windows.Forms.Panel();
		this.LeftTopPanel_1 = new System.Windows.Forms.Panel();
		this.LeftTopPanel_2 = new System.Windows.Forms.Panel();
		this._MinButton = new LBLIBRARY.Components.ButtonB();
		this._MaxButton = new LBLIBRARY.Components.ButtonD();
		this._CloseButton = new LBLIBRARY.Components.ButtonB();
		this.TopPanel.SuspendLayout();
		base.SuspendLayout();
		this.TopBorderPanel.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.TopBorderPanel.BackColor = System.Drawing.Color.Black;
		this.TopBorderPanel.Cursor = System.Windows.Forms.Cursors.SizeNS;
		this.TopBorderPanel.Location = new System.Drawing.Point(20, 0);
		this.TopBorderPanel.Name = "TopBorderPanel";
		this.TopBorderPanel.Size = new System.Drawing.Size(690, 2);
		this.TopBorderPanel.TabIndex = 0;
		this.TopBorderPanel.MouseDown += new System.Windows.Forms.MouseEventHandler(TopBorderPanel_MouseDown);
		this.TopBorderPanel.MouseMove += new System.Windows.Forms.MouseEventHandler(TopBorderPanel_MouseMove);
		this.TopBorderPanel.MouseUp += new System.Windows.Forms.MouseEventHandler(TopBorderPanel_MouseUp);
		this.RightPanel.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		this.RightPanel.BackColor = System.Drawing.Color.Black;
		this.RightPanel.Cursor = System.Windows.Forms.Cursors.SizeWE;
		this.RightPanel.Location = new System.Drawing.Point(728, 22);
		this.RightPanel.Name = "RightPanel";
		this.RightPanel.Size = new System.Drawing.Size(2, 430);
		this.RightPanel.TabIndex = 1;
		this.RightPanel.MouseDown += new System.Windows.Forms.MouseEventHandler(RightPanel_MouseDown);
		this.RightPanel.MouseMove += new System.Windows.Forms.MouseEventHandler(RightPanel_MouseMove);
		this.RightPanel.MouseUp += new System.Windows.Forms.MouseEventHandler(RightPanel_MouseUp);
		this.LeftPanel.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
		this.LeftPanel.BackColor = System.Drawing.Color.Black;
		this.LeftPanel.Cursor = System.Windows.Forms.Cursors.SizeWE;
		this.LeftPanel.Location = new System.Drawing.Point(0, 20);
		this.LeftPanel.Name = "LeftPanel";
		this.LeftPanel.Size = new System.Drawing.Size(2, 430);
		this.LeftPanel.TabIndex = 2;
		this.LeftPanel.MouseDown += new System.Windows.Forms.MouseEventHandler(LeftPanel_MouseDown);
		this.LeftPanel.MouseMove += new System.Windows.Forms.MouseEventHandler(LeftPanel_MouseMove);
		this.LeftPanel.MouseUp += new System.Windows.Forms.MouseEventHandler(LeftPanel_MouseUp);
		this.BottomPanel.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.BottomPanel.BackColor = System.Drawing.Color.Black;
		this.BottomPanel.Cursor = System.Windows.Forms.Cursors.SizeNS;
		this.BottomPanel.Location = new System.Drawing.Point(15, 471);
		this.BottomPanel.Name = "BottomPanel";
		this.BottomPanel.Size = new System.Drawing.Size(692, 2);
		this.BottomPanel.TabIndex = 3;
		this.BottomPanel.MouseDown += new System.Windows.Forms.MouseEventHandler(BottomPanel_MouseDown);
		this.BottomPanel.MouseMove += new System.Windows.Forms.MouseEventHandler(BottomPanel_MouseMove);
		this.BottomPanel.MouseUp += new System.Windows.Forms.MouseEventHandler(BottomPanel_MouseUp);
		this.TopPanel.BackColor = System.Drawing.Color.FromArgb(30, 30, 30);
		this.TopPanel.Controls.Add(this._MinButton);
		this.TopPanel.Controls.Add(this._MaxButton);
		this.TopPanel.Controls.Add(this.WindowTextLabel);
		this.TopPanel.Controls.Add(this._CloseButton);
		this.TopPanel.Dock = System.Windows.Forms.DockStyle.Top;
		this.TopPanel.Location = new System.Drawing.Point(0, 0);
		this.TopPanel.Name = "TopPanel";
		this.TopPanel.Size = new System.Drawing.Size(730, 76);
		this.TopPanel.TabIndex = 4;
		this.TopPanel.MouseDown += new System.Windows.Forms.MouseEventHandler(TopPanel_MouseDown);
		this.TopPanel.MouseMove += new System.Windows.Forms.MouseEventHandler(TopPanel_MouseMove);
		this.TopPanel.MouseUp += new System.Windows.Forms.MouseEventHandler(TopPanel_MouseUp);
		this.WindowTextLabel.AutoSize = true;
		this.WindowTextLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 26.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.WindowTextLabel.ForeColor = System.Drawing.Color.White;
		this.WindowTextLabel.Location = new System.Drawing.Point(3, 0);
		this.WindowTextLabel.Name = "WindowTextLabel";
		this.WindowTextLabel.Size = new System.Drawing.Size(134, 39);
		this.WindowTextLabel.TabIndex = 1;
		this.WindowTextLabel.Text = "My App";
		this.WindowTextLabel.MouseDown += new System.Windows.Forms.MouseEventHandler(WindowTextLabel_MouseDown);
		this.WindowTextLabel.MouseMove += new System.Windows.Forms.MouseEventHandler(WindowTextLabel_MouseMove);
		this.WindowTextLabel.MouseUp += new System.Windows.Forms.MouseEventHandler(WindowTextLabel_MouseUp);
		this.RightBottomPanel_1.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		this.RightBottomPanel_1.BackColor = System.Drawing.Color.Black;
		this.RightBottomPanel_1.Cursor = System.Windows.Forms.Cursors.SizeNWSE;
		this.RightBottomPanel_1.Location = new System.Drawing.Point(710, 471);
		this.RightBottomPanel_1.Name = "RightBottomPanel_1";
		this.RightBottomPanel_1.Size = new System.Drawing.Size(19, 2);
		this.RightBottomPanel_1.TabIndex = 5;
		this.RightBottomPanel_1.MouseDown += new System.Windows.Forms.MouseEventHandler(RightBottomPanel_1_MouseDown);
		this.RightBottomPanel_1.MouseMove += new System.Windows.Forms.MouseEventHandler(RightBottomPanel_1_MouseMove);
		this.RightBottomPanel_1.MouseUp += new System.Windows.Forms.MouseEventHandler(RightBottomPanel_1_MouseUp);
		this.RightBottomPanel_2.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		this.RightBottomPanel_2.BackColor = System.Drawing.Color.Black;
		this.RightBottomPanel_2.Cursor = System.Windows.Forms.Cursors.SizeNWSE;
		this.RightBottomPanel_2.Location = new System.Drawing.Point(728, 452);
		this.RightBottomPanel_2.Name = "RightBottomPanel_2";
		this.RightBottomPanel_2.Size = new System.Drawing.Size(2, 19);
		this.RightBottomPanel_2.TabIndex = 9;
		this.RightBottomPanel_2.MouseDown += new System.Windows.Forms.MouseEventHandler(RightBottomPanel_2_MouseDown);
		this.RightBottomPanel_2.MouseMove += new System.Windows.Forms.MouseEventHandler(RightBottomPanel_2_MouseMove);
		this.RightBottomPanel_2.MouseUp += new System.Windows.Forms.MouseEventHandler(RightBottomPanel_2_MouseUp);
		this.LeftBottomPanel_1.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
		this.LeftBottomPanel_1.BackColor = System.Drawing.Color.Black;
		this.LeftBottomPanel_1.Cursor = System.Windows.Forms.Cursors.SizeNESW;
		this.LeftBottomPanel_1.Location = new System.Drawing.Point(0, 471);
		this.LeftBottomPanel_1.Name = "LeftBottomPanel_1";
		this.LeftBottomPanel_1.Size = new System.Drawing.Size(15, 2);
		this.LeftBottomPanel_1.TabIndex = 10;
		this.LeftBottomPanel_1.MouseDown += new System.Windows.Forms.MouseEventHandler(LeftBottomPanel_1_MouseDown);
		this.LeftBottomPanel_1.MouseMove += new System.Windows.Forms.MouseEventHandler(LeftBottomPanel_1_MouseMove);
		this.LeftBottomPanel_1.MouseUp += new System.Windows.Forms.MouseEventHandler(LeftBottomPanel_1_MouseUp);
		this.LeftBottomPanel_2.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
		this.LeftBottomPanel_2.BackColor = System.Drawing.Color.Black;
		this.LeftBottomPanel_2.Cursor = System.Windows.Forms.Cursors.SizeNESW;
		this.LeftBottomPanel_2.Location = new System.Drawing.Point(0, 453);
		this.LeftBottomPanel_2.Name = "LeftBottomPanel_2";
		this.LeftBottomPanel_2.Size = new System.Drawing.Size(2, 19);
		this.LeftBottomPanel_2.TabIndex = 11;
		this.LeftBottomPanel_2.MouseDown += new System.Windows.Forms.MouseEventHandler(LeftBottomPanel_2_MouseDown);
		this.LeftBottomPanel_2.MouseMove += new System.Windows.Forms.MouseEventHandler(LeftBottomPanel_2_MouseMove);
		this.LeftBottomPanel_2.MouseUp += new System.Windows.Forms.MouseEventHandler(LeftBottomPanel_2_MouseUp);
		this.RightTopPanel_1.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.RightTopPanel_1.BackColor = System.Drawing.Color.Black;
		this.RightTopPanel_1.Cursor = System.Windows.Forms.Cursors.SizeNESW;
		this.RightTopPanel_1.Location = new System.Drawing.Point(728, 2);
		this.RightTopPanel_1.Name = "RightTopPanel_1";
		this.RightTopPanel_1.Size = new System.Drawing.Size(2, 20);
		this.RightTopPanel_1.TabIndex = 12;
		this.RightTopPanel_1.MouseDown += new System.Windows.Forms.MouseEventHandler(RightTopPanel_1_MouseDown);
		this.RightTopPanel_1.MouseMove += new System.Windows.Forms.MouseEventHandler(RightTopPanel_1_MouseMove);
		this.RightTopPanel_1.MouseUp += new System.Windows.Forms.MouseEventHandler(RightTopPanel_1_MouseUp);
		this.RightTopPanel_2.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.RightTopPanel_2.BackColor = System.Drawing.Color.Black;
		this.RightTopPanel_2.Cursor = System.Windows.Forms.Cursors.SizeNESW;
		this.RightTopPanel_2.Location = new System.Drawing.Point(710, 0);
		this.RightTopPanel_2.Name = "RightTopPanel_2";
		this.RightTopPanel_2.Size = new System.Drawing.Size(20, 2);
		this.RightTopPanel_2.TabIndex = 13;
		this.RightTopPanel_2.MouseDown += new System.Windows.Forms.MouseEventHandler(RightTopPanel_2_MouseDown);
		this.RightTopPanel_2.MouseMove += new System.Windows.Forms.MouseEventHandler(RightTopPanel_2_MouseMove);
		this.RightTopPanel_2.MouseUp += new System.Windows.Forms.MouseEventHandler(RightTopPanel_2_MouseUp);
		this.LeftTopPanel_1.BackColor = System.Drawing.Color.Black;
		this.LeftTopPanel_1.Cursor = System.Windows.Forms.Cursors.SizeNWSE;
		this.LeftTopPanel_1.Location = new System.Drawing.Point(0, 0);
		this.LeftTopPanel_1.Name = "LeftTopPanel_1";
		this.LeftTopPanel_1.Size = new System.Drawing.Size(20, 2);
		this.LeftTopPanel_1.TabIndex = 14;
		this.LeftTopPanel_1.MouseDown += new System.Windows.Forms.MouseEventHandler(LeftTopPanel_1_MouseDown);
		this.LeftTopPanel_1.MouseMove += new System.Windows.Forms.MouseEventHandler(LeftTopPanel_1_MouseMove);
		this.LeftTopPanel_1.MouseUp += new System.Windows.Forms.MouseEventHandler(LeftTopPanel_1_MouseUp);
		this.LeftTopPanel_2.BackColor = System.Drawing.Color.Black;
		this.LeftTopPanel_2.Cursor = System.Windows.Forms.Cursors.SizeNWSE;
		this.LeftTopPanel_2.Location = new System.Drawing.Point(0, 0);
		this.LeftTopPanel_2.Name = "LeftTopPanel_2";
		this.LeftTopPanel_2.Size = new System.Drawing.Size(2, 20);
		this.LeftTopPanel_2.TabIndex = 15;
		this.LeftTopPanel_2.MouseDown += new System.Windows.Forms.MouseEventHandler(LeftTopPanel_2_MouseDown);
		this.LeftTopPanel_2.MouseMove += new System.Windows.Forms.MouseEventHandler(LeftTopPanel_2_MouseMove);
		this.LeftTopPanel_2.MouseUp += new System.Windows.Forms.MouseEventHandler(LeftTopPanel_2_MouseUp);
		this._MinButton.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this._MinButton.BZBackColor = System.Drawing.Color.FromArgb(30, 30, 30);
		this._MinButton.DisplayText = "_";
		this._MinButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
		this._MinButton.Font = new System.Drawing.Font("Microsoft YaHei UI", 20.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this._MinButton.ForeColor = System.Drawing.Color.White;
		this._MinButton.Image = null;
		this._MinButton.Image_Location = new System.Drawing.Point(0, 0);
		this._MinButton.ImageToHeight = false;
		this._MinButton.Location = new System.Drawing.Point(632, 6);
		this._MinButton.MouseClickColor1 = System.Drawing.Color.FromArgb(60, 60, 160);
		this._MinButton.MouseHoverColor = System.Drawing.Color.FromArgb(50, 50, 50);
		this._MinButton.Name = "_MinButton";
		this._MinButton.Size = new System.Drawing.Size(31, 24);
		this._MinButton.TabIndex = 4;
		this._MinButton.Text = "_";
		this._MinButton.TextLocation_X = 6;
		this._MinButton.TextLocation_Y = -20;
		this.toolTip1.SetToolTip(this._MinButton, "Minimize");
		this._MinButton.UseVisualStyleBackColor = true;
		this._MinButton.Click += new System.EventHandler(_MinButton_Click);
		this._MaxButton.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this._MaxButton.BZBackColor = System.Drawing.Color.FromArgb(30, 30, 30);
		this._MaxButton.CFormState = LBLIBRARY.Components.ButtonD.CustomFormState.Normal;
		this._MaxButton.DisplayText = "_";
		this._MaxButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
		this._MaxButton.ForeColor = System.Drawing.Color.White;
		this._MaxButton.Location = new System.Drawing.Point(663, 6);
		this._MaxButton.MouseClickColor1 = System.Drawing.Color.FromArgb(60, 60, 160);
		this._MaxButton.MouseHoverColor = System.Drawing.Color.FromArgb(50, 50, 50);
		this._MaxButton.Name = "_MaxButton";
		this._MaxButton.Size = new System.Drawing.Size(31, 24);
		this._MaxButton.TabIndex = 2;
		this._MaxButton.Text = "minMaxButton1";
		this._MaxButton.TextLocation_X = 8;
		this._MaxButton.TextLocation_Y = 6;
		this.toolTip1.SetToolTip(this._MaxButton, "Maximize");
		this._MaxButton.UseVisualStyleBackColor = true;
		this._MaxButton.Click += new System.EventHandler(_MaxButton_Click);
		this._CloseButton.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this._CloseButton.BZBackColor = System.Drawing.Color.FromArgb(30, 30, 30);
		this._CloseButton.DisplayText = "X";
		this._CloseButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
		this._CloseButton.Font = new System.Drawing.Font("Microsoft YaHei UI", 12f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this._CloseButton.ForeColor = System.Drawing.Color.White;
		this._CloseButton.Image = null;
		this._CloseButton.Image_Location = new System.Drawing.Point(0, 0);
		this._CloseButton.ImageToHeight = false;
		this._CloseButton.Location = new System.Drawing.Point(694, 6);
		this._CloseButton.MouseClickColor1 = System.Drawing.Color.FromArgb(60, 60, 160);
		this._CloseButton.MouseHoverColor = System.Drawing.Color.FromArgb(50, 50, 50);
		this._CloseButton.Name = "_CloseButton";
		this._CloseButton.Size = new System.Drawing.Size(31, 24);
		this._CloseButton.TabIndex = 0;
		this._CloseButton.Text = "X";
		this._CloseButton.TextLocation_X = 6;
		this._CloseButton.TextLocation_Y = 1;
		this.toolTip1.SetToolTip(this._CloseButton, "Close");
		this._CloseButton.UseVisualStyleBackColor = true;
		this._CloseButton.Click += new System.EventHandler(_CloseButton_Click);
		base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		this.BackColor = System.Drawing.Color.FromArgb(60, 60, 60);
		base.ClientSize = new System.Drawing.Size(730, 473);
		base.Controls.Add(this.LeftTopPanel_2);
		base.Controls.Add(this.LeftTopPanel_1);
		base.Controls.Add(this.RightTopPanel_2);
		base.Controls.Add(this.RightTopPanel_1);
		base.Controls.Add(this.LeftBottomPanel_2);
		base.Controls.Add(this.LeftBottomPanel_1);
		base.Controls.Add(this.RightBottomPanel_2);
		base.Controls.Add(this.RightBottomPanel_1);
		base.Controls.Add(this.BottomPanel);
		base.Controls.Add(this.LeftPanel);
		base.Controls.Add(this.RightPanel);
		base.Controls.Add(this.TopBorderPanel);
		base.Controls.Add(this.TopPanel);
		base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
		base.Name = "BlackForm";
		this.Text = "My App";
		base.Resize += new System.EventHandler(BlackForm_Resize);
		this.TopPanel.ResumeLayout(false);
		this.TopPanel.PerformLayout();
		base.ResumeLayout(false);
	}
}
