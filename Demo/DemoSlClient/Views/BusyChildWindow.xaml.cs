using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

namespace Jiuyong
{
    public partial class BusyChildWindow : ChildWindow
    {
        public BusyChildWindow()
        {
            InitializeComponent();
			this.Loaded += BusyChildWindow_Loaded;
        }

		void BusyChildWindow_Loaded(object sender, RoutedEventArgs e)
		{
			var b = new System.Windows.Data.Binding();
			b.Source = _messages;
			messageBlock.SetBinding(ListBox.ItemsSourceProperty, b);
		}

        private void OKButton_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
        }

		private System.Collections.ObjectModel.ObservableCollection<string> _messages= new System.Collections.ObjectModel.ObservableCollection<string>();
		//public System.Collections.ObjectModel.ObservableCollection<string> Messages
		//{
		//    get
		//    {
		//        return _messages;
		//    }

		//    //set
		//    //{
		//    //	_messages = value;
		//    ////	OnPropertyChanged("Messages");
		//    //}
		//}
		public void AddMessage(string msg)
		{
			_messages.Add(msg);
			//if (_messages.Count >= _testTotal)
			//{
			//    this.DialogResult = true;
			//}
		}

		//private int _testTotal= 1;
		//public int TestTotal
		//{
		//    get
		//    {
		//        return _testTotal;
		//    }

		//    set
		//    {
		//        _testTotal = value;
		//        //	OnPropertyChanged("TestTotal");
		//    }
		//}




		//private bool _oKEnabled= false;
		public bool OKEnabled
		{
			get
			{
				//return _oKEnabled;
				return OKButton.IsEnabled;
			}

			set
			{
				//_oKEnabled = value;
				OKButton.IsEnabled = value;
				busyBox.IsBusy = false;
				//	OnPropertyChanged("OKEnabled");
			}
		}


	}
}

