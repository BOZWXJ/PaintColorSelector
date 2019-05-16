using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

using Livet;

namespace PaintColorSelector.Models
{
	public class FilterList : NotificationObject
	{
		/// <summary>
		/// フィルター項目リスト
		/// </summary>
		public ObservableCollection<Filter> Filters { get; private set; }

		public FilterList(Filter[] list)
		{
			Filters = new ObservableCollection<Filter>(list);

			FilterInitialize();
		}

		/// <summary>
		/// フィルター用メーカー名リスト
		/// </summary>
		public ObservableCollection<string> MakerList { get; private set; } = new ObservableCollection<string>();
		public ObservableCollection<string> SelectedMakerList { get; private set; } = new ObservableCollection<string>();

		/// <summary>
		/// フィルター用シリーズ名リスト
		/// </summary>
		public ObservableCollection<string> SeriesList { get; private set; } = new ObservableCollection<string>();
		public ObservableCollection<string> SelectedSeriesList { get; private set; } = new ObservableCollection<string>();

		private void FilterInitialize()
		{
			// メーカー名フィルター初期化
			MakerList.Clear();
			foreach (var s in Filters.Select(p => p.Maker).Distinct()) {
				MakerList.Add(s);
			}
			// シリーズ名フィルター初期化
			SeriesList.Clear();
			var list = Filters.Select(p => p.Series).Distinct();
			foreach (var s in list) {
				SeriesList.Add(s);
			}
		}




	}
}
