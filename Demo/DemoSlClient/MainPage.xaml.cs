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
using Demo.Models;

namespace Demo
{
	public partial class MainPage : UserControl
	{
		public MainPage()
		{
			InitializeComponent();
			//Loaded += Page_Loaded;
		}

		//void Page_Loaded(object sender, RoutedEventArgs e)
		//{

		//}


		private void ButtonCancel_Click(object sender, RoutedEventArgs e)
		{
			//Application.Current.Shutdown();
		}

		private void ButtonOK_Click(object sender, RoutedEventArgs e)
		{

		}

		private void HyperlinkButton_Click(object sender,RoutedEventArgs e)
		{
			App.GetPage((((HyperlinkButton)sender).Content as string),pg =>
			{
				pg.Show();
			});
		}
	}
}
