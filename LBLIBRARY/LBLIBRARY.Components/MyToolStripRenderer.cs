using System.Drawing;
using System.Windows.Forms;

namespace LBLIBRARY.Components;

public class MyToolStripRenderer : ToolStripRenderer
{
	private Color OwnBackColor;

	private Color OwnForeColor;

	private Color OwnSeparatorColor;

	private Color OwnLeftSideColor;

	private Color OwnMouseBackColor;

	private Color OwnMouseBorderColor;

	private Color ItemChecked;

	private Color ArrowColor;

	public MyToolStripRenderer(Color co, Color fc, Color sc, Color ls, Color mbc, Color bc, Color ic, Color ac)
	{
		OwnBackColor = co;
		OwnForeColor = fc;
		OwnSeparatorColor = sc;
		OwnLeftSideColor = ls;
		OwnMouseBackColor = mbc;
		OwnMouseBorderColor = bc;
		ItemChecked = ic;
		ArrowColor = ac;
	}

	protected override void OnRenderMenuItemBackground(ToolStripItemRenderEventArgs e)
	{
		base.OnRenderMenuItemBackground(e);
		if (e.Item.Enabled)
		{
			if (!e.Item.IsOnDropDown && e.Item.Selected)
			{
				Rectangle rect = new Rectangle(0, 0, e.Item.Width - 1, e.Item.Height - 1);
				e.Graphics.FillRectangle(new SolidBrush(OwnMouseBackColor), rect);
				e.Graphics.DrawRectangle(new Pen(new SolidBrush(Color.Blue)), rect);
				e.Item.ForeColor = OwnForeColor;
			}
			else
			{
				e.Item.ForeColor = OwnForeColor;
			}
			if (e.Item.IsOnDropDown && e.Item.Selected)
			{
				Rectangle rect2 = new Rectangle(0, 0, e.Item.Width - 1, e.Item.Height - 1);
				e.Graphics.FillRectangle(new SolidBrush(OwnMouseBackColor), rect2);
				e.Graphics.DrawRectangle(new Pen(new SolidBrush(Color.LightBlue)), rect2);
				e.Item.ForeColor = OwnForeColor;
			}
			if ((e.Item as ToolStripMenuItem).DropDown.Visible && !e.Item.IsOnDropDown)
			{
				Rectangle rect3 = new Rectangle(0, 0, e.Item.Width - 1, e.Item.Height - 1);
				Rectangle rect4 = new Rectangle(0, 0, e.Item.Width - 1, e.Item.Height - 1);
				e.Graphics.FillRectangle(new SolidBrush(OwnBackColor), rect3);
				e.Graphics.DrawRectangle(new Pen(new SolidBrush(Color.Black)), rect4);
				e.Item.ForeColor = OwnForeColor;
			}
		}
	}

	protected override void OnRenderSeparator(ToolStripSeparatorRenderEventArgs e)
	{
		base.OnRenderSeparator(e);
		SolidBrush brush = new SolidBrush(OwnSeparatorColor);
		Rectangle rect = new Rectangle(30, 3, e.Item.Width - 30, 1);
		e.Graphics.FillRectangle(brush, rect);
	}

	protected override void OnRenderItemCheck(ToolStripItemImageRenderEventArgs e)
	{
		base.OnRenderItemCheck(e);
		if (e.Item.Selected)
		{
			Rectangle rect = new Rectangle(2, 0, 23, 23);
			Rectangle rect2 = new Rectangle(2, 0, 23, 23);
			SolidBrush brush = new SolidBrush(Color.Black);
			SolidBrush brush2 = new SolidBrush(ItemChecked);
			e.Graphics.FillRectangle(brush, rect);
			e.Graphics.FillRectangle(brush2, rect2);
			e.Graphics.DrawImage(e.Image, new Point(5, 3));
		}
		else
		{
			Rectangle rect3 = new Rectangle(2, 0, 23, 23);
			Rectangle rect4 = new Rectangle(2, 0, 23, 23);
			SolidBrush brush3 = new SolidBrush(Color.White);
			SolidBrush brush4 = new SolidBrush(ItemChecked);
			e.Graphics.FillRectangle(brush3, rect3);
			e.Graphics.FillRectangle(brush4, rect4);
			e.Graphics.DrawImage(e.Image, new Point(5, 3));
		}
	}

	protected override void OnRenderImageMargin(ToolStripRenderEventArgs e)
	{
		base.OnRenderImageMargin(e);
		Rectangle rect = new Rectangle(0, 0, e.ToolStrip.Width, e.ToolStrip.Height);
		e.Graphics.FillRectangle(new SolidBrush(OwnBackColor), rect);
		SolidBrush brush = new SolidBrush(OwnLeftSideColor);
		Rectangle rect2 = new Rectangle(0, 0, 26, e.AffectedBounds.Height);
		e.Graphics.FillRectangle(brush, rect2);
		Rectangle rect3 = new Rectangle(0, 0, e.ToolStrip.Width - 1, e.ToolStrip.Height - 1);
		e.Graphics.DrawRectangle(new Pen(new SolidBrush(Color.Black)), rect3);
	}

	protected override void OnRenderArrow(ToolStripArrowRenderEventArgs e)
	{
		e.ArrowColor = ArrowColor;
		base.OnRenderArrow(e);
	}
}
