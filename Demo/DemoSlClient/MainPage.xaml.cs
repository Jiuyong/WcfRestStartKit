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
using Jiuyong;

namespace Demo
{
	public partial class MainPage : UserControl
	{
		public MainPage()
		{
			InitializeComponent();
			Loaded += Page_Loaded;
		}

		void Page_Loaded(object sender, RoutedEventArgs e)
		{
			this.SendCommand<int?>(Commands.TestDataBase, null, r =>
			{
				MessageBox.Show(r.Result.ToString());
			});
			//MessageBox.Show("你好，世界！");
		}
	}
}
