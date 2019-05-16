using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using ColorMine.ColorSpaces;
using ColorMine.ColorSpaces.Comparisons;
using Livet;

namespace PaintColorSelector.Models
{
	public class PaintList : NotificationObject
	{
		/// <summary>
		/// マスターリスト
		/// </summary>
		public ObservableCollection<Paint> Paints { get; private set; } = new ObservableCollection<Paint>();
		// マスターリスト ファイルパス
		private readonly string PaintListPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"Data\PaintList.txt");

		/// <summary>
		/// フィルター項目
		/// </summary>
		public FilterList PaintFilter { get; private set; }

		public PaintList()
		{
			// マスターリスト読み込み
			if (!(bool)(System.ComponentModel.DesignerProperties.IsInDesignModeProperty.GetMetadata(typeof(System.Windows.DependencyObject)).DefaultValue)) {
				LoadPaintList();
			}
			// フィルター項目 設定
			PaintFilter = new FilterList(Paints.Select(p => new Filter() { Maker = p.Maker, Series = p.Series }).Distinct().ToArray());
		}

		private void LoadPaintList()
		{
			Paints.Clear();
			foreach (Paint paint in PaintListFile.Read(PaintListPath)) {
				Paints.Add(paint);
			}
		}

		/// <summary>
		/// 基準カラー変更
		/// </summary>
		public void ReferenceColorChange(Lab lab)
		{
			// DeltaE 更新
			foreach (var paint in Paints) {
				paint.DeltaE = new Cie1976Comparison().Compare(lab, paint.Lab);
			}
		}

	}
}
