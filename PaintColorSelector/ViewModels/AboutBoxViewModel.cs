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
using System.Reflection;
using System.IO;

namespace PaintColorSelector.ViewModels
{
	public class AboutBoxViewModel : ViewModel
	{
		public void Initialize()
		{
		}

		#region アセンブリ属性アクセサー

		public string AssemblyTitle
		{
			get
			{
				string title;
				var titleAttribute = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyTitleAttribute), false).FirstOrDefault() as AssemblyTitleAttribute;
				title = titleAttribute?.Title;
				if (string.IsNullOrEmpty(title)) {
					title = Path.GetFileNameWithoutExtension(Assembly.GetExecutingAssembly().CodeBase);
				}
				return $"{title} のバージョン情報";
			}
		}

		public string AssemblyVersion
		{
			get
			{
				return $"バージョン {Assembly.GetExecutingAssembly().GetName().Version}";
			}
		}

		public string AssemblyDescription
		{
			get
			{
				var attribute = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyDescriptionAttribute), false).FirstOrDefault() as AssemblyDescriptionAttribute;
				return attribute?.Description;
			}
		}

		public string AssemblyProduct
		{
			get
			{
				var attribute = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyProductAttribute), false).FirstOrDefault() as AssemblyProductAttribute;
				return attribute?.Product;
			}
		}

		public string AssemblyCopyright
		{
			get
			{
				var attribute = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyCopyrightAttribute), false).FirstOrDefault() as AssemblyCopyrightAttribute;
				return attribute?.Copyright;
			}
		}

		public string AssemblyCompany
		{
			get
			{
				var attribute = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyCompanyAttribute), false).FirstOrDefault() as AssemblyCompanyAttribute;
				return attribute?.Company;
			}
		}
		#endregion


	}
}
