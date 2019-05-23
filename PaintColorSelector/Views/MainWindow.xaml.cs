using PaintColorSelector.Models;
using PaintColorSelector.ViewModels;
using System;
using System.Collections.Generic;
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
			viewSource.View.Filter = ViewModel.Contains;
		}

		CollectionViewSource viewSource;

		private void Close_Click(object sender, RoutedEventArgs e)
		{
			Close();
		}

		private void About_Click(object sender, RoutedEventArgs e)
		{
			new AboutBox() { Owner = this }.ShowDialog();
		}

		private void SelectFindPaint(object sender, RoutedEventArgs e)
		{
			// VM にイベント送信 検索カラー設定 ΔE 計算
			ViewModel.FindPaintChange();

			// DataGrid ΔE 列でソート
			viewSource.SortDescriptions.Clear();
			viewSource.SortDescriptions.Add(new System.ComponentModel.SortDescription() { PropertyName = "DeltaE", Direction = System.ComponentModel.ListSortDirection.Ascending });
			viewSource.View.Filter = ViewModel.Contains;

			// DataGrid 先頭行にスクロール
			PaintListDataGrid.ScrollIntoView(viewSource.View.CurrentItem);
		}

		private void SeriesListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			// VM にイベント送信 AllCheckBox 状態更新
			ViewModel.CheckBox_Click();

			// DataGrid 表示更新
			viewSource.View.Refresh();
		}

	}
}
