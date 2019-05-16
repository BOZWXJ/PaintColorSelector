using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Livet;
using System.Collections.ObjectModel;

namespace PaintColorSelector.Models
{
	public class AppContext : NotificationObject
	{
		/*
		 * NotificationObjectはプロパティ変更通知の仕組みを実装したオブジェクトです。
		 */

		public static AppContext Instance { get; } = new AppContext();
		private AppContext()
		{
			PaintList = new PaintList();
		}

		public PaintList PaintList { get; private set; }

	}
}
