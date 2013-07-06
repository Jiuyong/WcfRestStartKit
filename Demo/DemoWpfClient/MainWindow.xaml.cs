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
using Demo.Models;

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

			this.SendCommand<DateTime>(new Uri(new Uri("http://localhost:80/product/smartinfobase/"), Jiuyong.Commands.TestRoute, false), null, r =>
			{
				MessageBox.Show(r.Result.ToString());
			}
			);


		}

		private void ButtonGetData_Click(object sender, RoutedEventArgs e)
		{
			try
			{
				ButtonGetData.SendCommand<Root<Demo.Models.Category>>(Commands.Category + String.Format("/{0:000}",BoxID.Text),null,r =>
				{
					BoxID.Text = r.Result.Category.categoryId;
					BoxName.Text = r.Result.Category.categoryName;
				}
					//,
					//httpMethod: HttpMethod.Get
				);

			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.StackTrace);
			}
		}

		private void ButtonPutData_Click(object sender, RoutedEventArgs e)
		{

			var rq = new Root<Category>()
			{
				Category = new Category
				{
					categoryId = BoxID.Text
					,
					categoryName = BoxName.Text
				}
			};

			this.SendCommand<Root<Category>>(Demo.Commands.Category,rq,r =>
			{
				MessageBox.Show(r.Result.ToString());
			}
			//,
			//httpMethod: HttpMethod.Put
			);

		}

		private void ButtonCancel_Click(object sender, RoutedEventArgs e)
		{
			Application.Current.Shutdown();
		}

		private void ButtonOK_Click(object sender, RoutedEventArgs e)
		{

		}
	}
}
