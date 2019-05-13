using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Media;
using ColorMine.ColorSpaces;
using Livet;

namespace PaintColorSelector.Models
{
	public class Paint : NotificationObject
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
		/// CIE L*a*b*
		/// </summary>
		public Lab Lab
		{
			get => _Lab;
			set => RaisePropertyChangedIfSet(ref _Lab, value);
		}
		private Lab _Lab;

		/// <summary>
		/// ΔE
		/// </summary>
		public double DeltaE
		{
			get => _DeltaE;
			set => RaisePropertyChangedIfSet(ref _DeltaE, value);
		}
		private double _DeltaE;

		public override string ToString()
		{
			return $"{Maker}\t{Series}\t{ColorCode}\t{ColorName}\t{Note}";
		}

		public static Paint FromString(string s)
		{
			string[] vs = s.Split('\t');
			return new Paint() { Maker = vs[0], Series = vs[1], ColorCode = vs[2], ColorName = vs[3], Note = vs.Length >= 5 ? vs[4] : "" };
		}

		private void SetColorCode(string colorCode)
		{
			Color = (Color)ColorConverter.ConvertFromString(colorCode);
			var rgb = new Rgb {
				R = Color.R,
				G = Color.G,
				B = Color.B
			};
			Lab = rgb.To<Lab>();
			DeltaE = 0;
		}

	}
}
