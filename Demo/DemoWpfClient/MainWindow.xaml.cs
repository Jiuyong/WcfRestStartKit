using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Jiuyong;

namespace Demo
{
	/// <summary>
	/// MainWindow.xaml 的交互逻辑
	/// </summary>
	public partial class MainWindow : Window
	{
		public MainWindow()
		{
			InitializeComponent();
			Loaded += Window_Loaded;
		}

		void Window_Loaded(object sender, RoutedEventArgs e)
		{
			var d = new Models.Post
			{
				BlogId = 1,
				Content = "一大段文本内容。",
				CreateTime = DateTime.Now,
				Title = "WPF 评论"
			};

			this.SendCommand<int?>(Commands.Test, d, r =>
			{
				MessageBox.Show(r.Result.ToString());
			}
			,
			new DataContractXmlSerializer()
			,
			httpMethod : HttpMethod.Put
			);

		}
	}
}
