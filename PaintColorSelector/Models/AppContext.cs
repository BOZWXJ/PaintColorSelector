using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Livet;

namespace PaintColorSelector.Models
{
	public class AppContext : NotificationObject
	{
		//* NotificationObjectはプロパティ変更通知の仕組みを実装したオブジェクトです。

		public static AppContext Instance { get; } = new AppContext();
		private AppContext()
		{
			foreach (var paint in PaintListFile.Read(PaintListPath)) {
				Paints.Add(paint);
				System.Diagnostics.Debug.WriteLine(paint);
			}

		}

		/// <summary>
		/// 
		/// </summary>
		readonly string PaintListPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"Data\PaintList.txt");

		/// <summary>
		/// 
		/// </summary>
		public ObservableSynchronizedCollection<Paint> Paints { get; private set; } = new ObservableSynchronizedCollection<Paint>();


	}
}
