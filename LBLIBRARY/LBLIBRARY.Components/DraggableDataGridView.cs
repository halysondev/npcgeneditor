using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace LBLIBRARY.Components;

public class DraggableDataGridView : DataGridView
{
	private bool DelayedMouseDown;

	private Rectangle DragRectangle = Rectangle.Empty;

	public DraggableDataGridView()
	{
		AllowDrop = true;
		base.Tag = new Stopwatch();
	}

	protected override void OnCellMouseDown(DataGridViewCellMouseEventArgs e)
	{
		base.OnCellMouseDown(e);
		if (e.RowIndex < 0 || e.Button != MouseButtons.Right)
		{
			return;
		}
		int index = base.CurrentRow.Index;
		List<DataGridViewRow> list = base.SelectedRows.OfType<DataGridViewRow>().ToList();
		bool selected = base.Rows[e.RowIndex].Selected;
		base.CurrentCell = base.Rows[e.RowIndex].Cells[e.ColumnIndex];
		if ((Control.ModifierKeys & Keys.Control) != 0 || selected)
		{
			list.ForEach(delegate(DataGridViewRow row)
			{
				row.Selected = true;
			});
		}
		if ((Control.ModifierKeys & Keys.Shift) != 0)
		{
			for (int i = index; i != e.RowIndex; i += Math.Sign(e.RowIndex - index))
			{
				base.Rows[i].Selected = true;
			}
		}
	}

	protected override void OnMouseDown(MouseEventArgs e)
	{
		(base.Tag as Stopwatch).Start();
		int rowIndex = HitTest(e.X, e.Y).RowIndex;
		DelayedMouseDown = rowIndex >= 0 && (base.SelectedRows.Contains(base.Rows[rowIndex]) || (Control.ModifierKeys & Keys.Control) > Keys.None);
		if (!DelayedMouseDown)
		{
			base.OnMouseDown(e);
			if (rowIndex >= 0)
			{
				Size dragSize = SystemInformation.DragSize;
				DragRectangle = new Rectangle(new Point(e.X - dragSize.Width / 2, e.Y - dragSize.Height / 2), dragSize);
			}
			else
			{
				DragRectangle = Rectangle.Empty;
			}
		}
	}

	protected override void OnMouseUp(MouseEventArgs e)
	{
		Stopwatch obj = base.Tag as Stopwatch;
		obj.Stop();
		obj.Reset();
		if (DelayedMouseDown)
		{
			DelayedMouseDown = false;
			base.OnMouseDown(e);
		}
		base.OnMouseUp(e);
	}

	protected override void OnMouseMove(MouseEventArgs e)
	{
		base.OnMouseMove(e);
		Stopwatch obj = base.Tag as Stopwatch;
		obj.Stop();
		if (obj.ElapsedMilliseconds > 75)
		{
			DoDragDrop(base.SelectedRows, DragDropEffects.Move);
		}
		obj.Reset();
	}

	protected override void OnDragOver(DragEventArgs drgevent)
	{
		base.OnDragOver(drgevent);
		drgevent.Effect = DragDropEffects.All;
	}
}
