using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaintColorSelector.Models
{
	public static class Utility
	{
		public static (int, int, int) ConvertHSL(int R, int G, int B)
		{
			int hue, saturation, lightness;
			int max = Math.Max(R, Math.Max(G, B));
			int min = Math.Min(R, Math.Min(G, B));
			// 輝度
			lightness = (max + min) / 2;
			if (R == G && R == B && G == B) {
				// 色相
				hue = 0;
				// 彩度
				saturation = 0;
			} else {
				// 色相
				if (R >= G && R >= B) {
					hue = 60 * (G - B) / (max - min);
				} else if (G >= R && G >= B) {
					hue = 60 * (B - R) / (max - min) + 120;
				} else {
					hue = 60 * (R - G) / (max - min) + 240;
				}
				if (hue < 0) {
					hue += 360;
				}
				// 彩度
				if (lightness < 128) {
					saturation = 255 * (max - min) / (max + min);
				} else {
					saturation = 255 * (max - min) / (510 - max - min);
				}
			}

			return (hue, saturation, lightness);
		}
	}
}
