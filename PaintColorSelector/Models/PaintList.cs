using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using ColorMine.ColorSpaces;
using Livet;

namespace PaintColorSelector.Models
{
	public class PaintList : NotificationObject
	{
		// マスターリスト
		private List<Paint> Paints = new List<Paint>();
		// マスターリスト ファイルパス
		private readonly string PaintListPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"Data\PaintList.txt");

		/// <summary>
		/// フィルター用メーカー名リスト
		/// </summary>
		public ObservableCollection<string> MakerList { get; private set; } = new ObservableCollection<string>();
		/// <summary>
		/// フィルター用メーカー名
		/// </summary>
		public string MakerFilterStr
		{
			get => _MakerFilterStr;
			set => RaisePropertyChangedIfSet(ref _MakerFilterStr, value);
		}
		private string _MakerFilterStr;
		private const string AllMakerFilterStr = "全て";

		/// <summary>
		/// フィルター用シリーズ名リスト
		/// </summary>
		public ObservableCollection<string> SeriesList { get; private set; } = new ObservableCollection<string>();
		/// <summary>
		/// フィルター用シリーズ名
		/// </summary>
		public string SeriesFilterStr
		{
			get => _SeriesFilterStr;
			set => RaisePropertyChangedIfSet(ref _SeriesFilterStr, value);
		}
		private string _SeriesFilterStr;
		private const string AllSeriesFilterStr = "全て";

		/// <summary>
		/// フィルター済リスト
		/// </summary>
		public ObservableCollection<Paint> FilterdPaints { get; private set; } = new ObservableCollection<Paint>();

		public PaintList()
		{
			// マスターリスト読み込み
			if (!(bool)(System.ComponentModel.DesignerProperties.IsInDesignModeProperty.GetMetadata(typeof(System.Windows.DependencyObject)).DefaultValue)) {
				LoadPaintList();
			}
			// フィルター設定
			FilterInitialize();
		}

		private void LoadPaintList()
		{
			Paints.Clear();
			foreach (Paint paint in PaintListFile.Read(PaintListPath)) {
				Paints.Add(paint);
			}
		}

		private void FilterInitialize()
		{
			// メーカー名フィルター初期化
			MakerList.Clear();
			MakerList.Add(AllMakerFilterStr);
			foreach (var s in Paints.Select(p => p.Maker).Distinct()) {
				MakerList.Add(s);
			}
			MakerFilterStr = MakerList[0];

			// シリーズ名フィルター初期化
			SeriesList.Clear();
			SeriesList.Add(AllSeriesFilterStr);
			foreach (var s in Paints.Where(p => MakerFilterStr == AllMakerFilterStr || MakerFilterStr == p.Maker).Select(p => p.Series).Distinct()) {
				SeriesList.Add(s);
			}
			SeriesFilterStr = SeriesList[0];

			// フィルター済リスト設定
			FilterdPaints.Clear();
			foreach (var paint in Paints.Where(p => (MakerFilterStr == AllMakerFilterStr || MakerFilterStr == p.Maker) && (SeriesFilterStr == AllSeriesFilterStr || SeriesFilterStr == p.Series)).Select(p => p)) {
				FilterdPaints.Add(paint);
			}
		}

		/// <summary>
		/// フィルター変更
		/// </summary>
		public void FilterChanged()
		{
			System.Diagnostics.Debug.WriteLine($"PaintList.FilterChanged() {MakerFilterStr} {SeriesFilterStr}");

			// todo: フィルター変更処理

			// 

		}

		/// <summary>
		/// 基準カラー変更
		/// </summary>
		public void ReferenceColorChanged(Lab lab)
		{
			System.Diagnostics.Debug.WriteLine($"PaintList.ReferenceColorChanged() {lab}");

			// todo: 基準カラー変更処理

			// DeltaE 更新

		}


	}
}
