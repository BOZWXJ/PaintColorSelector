using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaintColorSelector.Models
{
	public static class PaintListFile
	{
		public static Paint[] Read(string path)
		{
			List<Paint> list = new List<Paint>();
			foreach (var s in File.ReadLines(path)) {
				if (!string.IsNullOrWhiteSpace(s)) {
					list.Add(Paint.FromString(s));
				}
			}
			return list.ToArray();
		}

		public static void Write(string path, Paint[] paints)
		{
			StringBuilder sb = new StringBuilder();
			foreach (var paint in paints) {
				sb.AppendLine(paint.ToString());
			}
			File.WriteAllText(path, sb.ToString());
		}
	}
}
