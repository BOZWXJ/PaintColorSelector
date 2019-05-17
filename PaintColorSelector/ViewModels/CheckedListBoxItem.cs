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

namespace PaintColorSelector.ViewModels
{
	public class CheckedListBoxItem : ViewModel
	{
		public void Initialize() { }

		public string Text
		{
			get => _Text;
			set => RaisePropertyChangedIfSet(ref _Text, value);
		}
		private string _Text;

		public bool IsSelected
		{
			get => _IsSelected;
			set => RaisePropertyChangedIfSet(ref _IsSelected, value);
		}
		private bool _IsSelected;

		public CheckedListBoxItem() : this(string.Empty) { }
		public CheckedListBoxItem(string text)
		{
			Text = text;
			IsSelected = true;
		}
	}
}
