using System.Collections.Generic;
using System.Windows.Controls;

namespace LBLIBRARY.Components;

public class DatagridA : DataGrid
{
	public List<int> SelectedRowsIndexes
	{
		get
		{
			List<int> list = new List<int>();
			foreach (object selectedItem in base.SelectedItems)
			{
				list.Add(base.Items.IndexOf(selectedItem));
			}
			return list;
		}
	}
}
