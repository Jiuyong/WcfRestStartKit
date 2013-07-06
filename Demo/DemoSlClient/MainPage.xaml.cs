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
			Loaded += Page_Loaded;
		}

		void Page_Loaded(object sender, RoutedEventArgs e)
		{
			var d = new Models.Post
			{
				BlogId = Core.GetNextSerialInt32(),
				Content = "一大段文本内容。",
				CreateTime = DateTime.Now,
				Title = "WPF 评论"
			};

			this.SendCommand<int?>(Jiuyong.Commands.TestDataBase, null, r =>
			{
				MessageBox.Show(r.Result.ToString());
			});
		}


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
