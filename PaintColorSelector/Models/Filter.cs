using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Livet;

namespace PaintColorSelector.Models
{
	public class Filter : NotificationObject
	{
		/// <summary>
		/// メーカー
		/// </summary>
		public string Maker
		{
			get => _Maker;
			set => RaisePropertyChangedIfSet(ref _Maker, value);
		}
		private string _Maker;

		/// <summary>
		/// シリーズ
		/// </summary>
		public string Series
		{
			get => _Series;
			set => RaisePropertyChangedIfSet(ref _Series, value);
		}
		private string _Series;
	}
}
