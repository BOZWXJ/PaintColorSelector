using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Media;
using Livet;

namespace PaintColorSelector.Models
{
	public class Paint : NotificationObject
	{
		//* NotificationObjectはプロパティ変更通知の仕組みを実装したオブジェクトです。

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

		/// <summary>
		/// カラーコード
		/// </summary>
		public string ColorCode
		{
			get { return _ColorCode; }
			set
			{
				SetColorCode(value);
				RaisePropertyChangedIfSet(ref _ColorCode, value);
			}
		}
		private string _ColorCode;

		/// <summary>
		/// 色名
		/// </summary>
		public string ColorName
		{
			get => _ColorName;
			set => RaisePropertyChangedIfSet(ref _ColorName, value);
		}
		private string _ColorName;

		/// <summary>
		/// 備考
		/// </summary>
		public string Note
		{
			get => _Note;
			set => RaisePropertyChangedIfSet(ref _Note, value);
		}
		private string _Note;

		private void SetColorCode(string colorCode)
		{
			Color = (Color)ColorConverter.ConvertFromString(colorCode);
			(Hue, Saturation, Lightness) = Utility.ConvertHSL(Color.R, Color.G, Color.B);
		}

		/// <summary>
		/// サンプルカラー
		/// </summary>
		public Color Color
		{
			get => _Color;
			set => RaisePropertyChangedIfSet(ref _Color, value);
		}
		private Color _Color;

		/// <summary>
		/// 色相
		/// </summary>
		public int Hue { get; private set; }
		/// <summary>
		/// 彩度
		/// </summary>
		public int Saturation { get; private set; }
		/// <summary>
		/// 輝度
		/// </summary>
		public int Lightness { get; private set; }

		public override string ToString()
		{
			return $"{Maker}\t{Series}\t{ColorCode}\t{ColorName}\t{Note}";
		}

		public static Paint FromString(string s)
		{
			string[] vs = s.Split('\t');
			return new Paint() { Maker = vs[0], Series = vs[1], ColorCode = vs[2], ColorName = vs[3], Note = vs.Length >= 5 ? vs[4] : "" };
		}

	}
}
