using Livet;
using Livet.Commands;
using PaintColorSelector.Models;
using PaintColorSelector.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace PaintColorSelector.Views
{
	/* 
	 * ViewModelからの変更通知などの各種イベントを受け取る場合は、PropertyChangedWeakEventListenerや
     * CollectionChangedWeakEventListenerを使うと便利です。独自イベントの場合はLivetWeakEventListenerが使用できます。
     * クローズ時などに、LivetCompositeDisposableに格納した各種イベントリスナをDisposeする事でイベントハンドラの開放が容易に行えます。
     *
     * WeakEventListenerなので明示的に開放せずともメモリリークは起こしませんが、できる限り明示的に開放するようにしましょう。
     */

	/// <summary>
	/// MainWindow.xaml の相互作用ロジック
	/// </summary>
	public partial class MainWindow : Window
	{
		public MainWindow()
		{
			InitializeComponent();

			viewSource = (CollectionViewSource)Resources["PaintListViewSource"];
			viewSource.View.Filter = ViewModel.SeriesListIsSelected;
		}

		private void Close_Executed(object sender, ExecutedRoutedEventArgs e)
		{
			Close();
		}

		private void About_Click(object sender, RoutedEventArgs e)
		{
			new AboutBox() { Owner = this }.ShowDialog();
		}

		private readonly CollectionViewSource viewSource;

		// 基準カラー設定
		private void SelectFindPaint(object sender, RoutedEventArgs e)
		{
			// VM にイベント送信 基準カラー設定 ΔE 計算
			ViewModel.FindPaintChange();

			// DataGrid ΔE 列でソート
			viewSource.SortDescriptions.Clear();
			viewSource.SortDescriptions.Add(new SortDescription() { PropertyName = "DeltaE", Direction = ListSortDirection.Ascending });
			viewSource.View.Filter = ViewModel.SeriesListIsSelected;

			// 
			ColorCode_DataGridTextColumn.SortDirection = null;
			ColorName_DataGridTextColumn.SortDirection = null;
			Note_DataGridTextColumn.SortDirection = null;
			DeltaE_DataGridTextColumn.SortDirection = ListSortDirection.Ascending;

			// DataGrid 先頭行にスクロール
			PaintListDataGrid.ScrollIntoView(viewSource.View.CurrentItem);
		}

		// フィルター処理
		private void SeriesListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			// VM にイベント送信 AllCheckBox 状態更新
			ViewModel.CheckBox_Click();

			// DataGrid 表示更新
			viewSource.View.Refresh();
		}

		// 文字列検索
		private void FindTextBox_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.Key == Key.Enter) {
				SearchPaintListDataGrid(FindTextBox.Text);
			}
		}

		private void FindButton_Click(object sender, RoutedEventArgs e)
		{
			SearchPaintListDataGrid(FindTextBox.Text);
		}

		private void SearchPaintListDataGrid(string str)
		{
			bool f = false;
			object findObj = null;
			foreach (var obj in viewSource.View) {
				// 選択行まで進める
				if (obj == viewSource.View.CurrentItem) {
					f = true;
					continue;
				}
				// 検索する
				if (f && ViewModel.FindStringIsContains(obj, str)) {
					findObj = obj;
					break;
				}
			}
			// 最後まで該当無ければ先頭から検索する
			if (findObj == null) {
				foreach (var obj in viewSource.View) {
					// 検索する
					if (ViewModel.FindStringIsContains(obj, str)) {
						findObj = obj;
						break;
					}
					// 選択行まで進んだら終了
					if (obj == viewSource.View.CurrentItem) {
						break;
					}
				}
			}
			if (findObj != null) {
				viewSource.View.MoveCurrentTo(findObj);
			} else {
				// todo: ステータスバーに表示
				System.Diagnostics.Debug.WriteLine("検索結果無し");
			}
			// スクロール
			PaintListDataGrid.ScrollIntoView(viewSource.View.CurrentItem);
		}

	}
}
