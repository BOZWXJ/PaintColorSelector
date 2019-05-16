using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

using Livet;
using Livet.Commands;
using Livet.Messaging;
using Livet.Messaging.IO;
using Livet.EventListeners;
using Livet.Messaging.Windows;

using PaintColorSelector.Models;
using System.Threading.Tasks;
using System.Threading;
using System.Collections.ObjectModel;
using Reactive.Bindings;
using Reactive.Bindings.Extensions;

namespace PaintColorSelector.ViewModels
{
	public class MainWindowViewModel : ViewModel
	{
		/* コマンド、プロパティの定義にはそれぞれ 
         * 
         *  lvcom    : ViewModelCommand
         *  lvcomn   : ViewModelCommand(CanExecute無)
         *  llcom    : ListenerCommand(パラメータ有のコマンド)
         *  llcomn   : ListenerCommand(パラメータ有のコマンド・CanExecute無)
         *  lprop    : 変更通知プロパティ
         *  lsprop   : 変更通知プロパティ(ショートバージョン)
         *  
         * を使用してください。
         * 
         * Modelが十分にリッチであるならコマンドにこだわる必要はありません。
         * View側のコードビハインドを使用しないMVVMパターンの実装を行う場合でも、ViewModelにメソッドを定義し、
         * LivetCallMethodActionなどから直接メソッドを呼び出してください。
         * 
         * ViewModelのコマンドを呼び出せるLivetのすべてのビヘイビア・トリガー・アクションは
         * 同様に直接ViewModelのメソッドを呼び出し可能です。
         */

		/* ViewModelからViewを操作したい場合は、View側のコードビハインド無で処理を行いたい場合は
         * Messengerプロパティからメッセージ(各種InteractionMessage)を発信する事を検討してください。
         */

		/* Modelからの変更通知などの各種イベントを受け取る場合は、PropertyChangedEventListenerや
         * CollectionChangedEventListenerを使うと便利です。各種ListenerはViewModelに定義されている
         * CompositeDisposableプロパティ(LivetCompositeDisposable型)に格納しておく事でイベント解放を容易に行えます。
         * 
         * ReactiveExtensionsなどを併用する場合は、ReactiveExtensionsのCompositeDisposableを
         * ViewModelのCompositeDisposableプロパティに格納しておくのを推奨します。
         * 
         * LivetのWindowテンプレートではViewのウィンドウが閉じる際にDataContextDisposeActionが動作するようになっており、
         * ViewModelのDisposeが呼ばれCompositeDisposableプロパティに格納されたすべてのIDisposable型のインスタンスが解放されます。
         * 
         * ViewModelを使いまわしたい時などは、ViewからDataContextDisposeActionを取り除くか、発動のタイミングをずらす事で対応可能です。
         */

		/* UIDispatcherを操作する場合は、DispatcherHelperのメソッドを操作してください。
         * UIDispatcher自体はApp.xaml.csでインスタンスを確保してあります。
         * 
         * LivetのViewModelではプロパティ変更通知(RaisePropertyChanged)やDispatcherCollectionを使ったコレクション変更通知は
         * 自動的にUIDispatcher上での通知に変換されます。変更通知に際してUIDispatcherを操作する必要はありません。
         */

		private Models.AppContext model;

		public MainWindowViewModel()
		{
			model = Models.AppContext.Instance;

			PaintList = model.PaintList.Paints
				.ToReadOnlyReactiveCollection()
				.AddTo(CompositeDisposable);

			MakerList = model.PaintList.PaintFilter.MakerList
				.ToReadOnlyReactiveCollection()
				.AddTo(CompositeDisposable);
			SelectedMakerList = model.PaintList.PaintFilter.SelectedMakerList
				.ToReadOnlyReactiveCollection()
				.AddTo(CompositeDisposable);
			SeriesList = model.PaintList.PaintFilter.SeriesList
				.ToReadOnlyReactiveCollection()
				.AddTo(CompositeDisposable);
			SelectedSeriesList = model.PaintList.PaintFilter.SelectedSeriesList
				.ToReadOnlyReactiveCollection()
				.AddTo(CompositeDisposable);

			SelectedPaint = new ReactiveProperty<Paint>()
				.AddTo(CompositeDisposable);
			FindPaint = new ReactiveProperty<Paint>()
				.AddTo(CompositeDisposable);
		}

		public void Initialize()
		{
			// 
			Task.Run(() => {
				while (true) {
					Clock = DateTime.Now.ToString();
					Thread.Sleep(1000 - DateTime.Now.Millisecond);
				}
			});
		}

		/// <summary>
		/// カラーリスト
		/// </summary>
		public ReadOnlyReactiveCollection<Paint> PaintList { get; private set; }

		#region 基準カラー処理

		/// <summary>
		/// 選択カラー
		/// </summary>
		public ReactiveProperty<Paint> SelectedPaint { get; private set; }

		/// <summary>
		/// 検索カラー
		/// </summary>
		public ReactiveProperty<Paint> FindPaint { get; private set; }

		/// <summary>
		/// 検索カラー設定
		/// </summary>
		public void FindPaintChange()
		{
			FindPaint.Value = SelectedPaint.Value;
			model.PaintList.ReferenceColorChange(FindPaint.Value.Lab);
		}

		#endregion

		#region フィルター処理

		/// <summary>
		/// 大分類 フィルターリスト
		/// </summary>
		public ReadOnlyReactiveCollection<string> MakerList { get; private set; }
		public ReadOnlyReactiveCollection<string> SelectedMakerList { get; private set; }
		/// <summary>
		/// 小分類 フィルターリスト
		/// </summary>
		public ReadOnlyReactiveCollection<string> SeriesList { get; private set; }
		public ReadOnlyReactiveCollection<string> SelectedSeriesList { get; private set; }


		/// <summary>
		/// フィルター変更
		/// </summary>
		public void MakerFilterChange()
		{


		}

		#endregion

		// debug: Clock
		public string Clock
		{
			get => _Clock;
			private set => RaisePropertyChangedIfSet(ref _Clock, value);
		}
		private string _Clock;

	}
}
