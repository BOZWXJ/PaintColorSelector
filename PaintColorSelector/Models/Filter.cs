using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Livet;

namespace PaintColorSelector.Models
{
	public class Filter : NotificationObject, IEquatable<Filter>
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

		#region IEquatable<Filter>

		public override bool Equals(object obj)
		{
			return Equals(obj as Filter);
		}

		public bool Equals(Filter other)
		{
			return other != null && Maker == other.Maker && Series == other.Series;
		}

		public override int GetHashCode()
		{
			var hashCode = -724254097;
			hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Maker);
			hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Series);
			return hashCode;
		}

		#endregion

	}
}
