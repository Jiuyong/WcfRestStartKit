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

			//this.SendCommand<Root<Category>>(new Uri("http://202.119.11.100:8080/BookRestService/rest/categoryservice/category/001"),null,r =>

			////this.SendCommand<Demo.Models.Category>(Commands.Test,null,r =>
			//{
			//    MessageBox.Show(r.Result.ToString());
			//}
			////,
			////new DotNetXmlSerializer()
			//);

			var rq = new Root<Category>()
			{
				Category = new Category
				{
					categoryId = "002"
					,
					categoryName = "xin"
				}
			};

			this.SendCommand<string>(new Uri("http://202.119.11.100:8080/BookRestService/rest/categoryservice/category"),rq,r =>

			//this.SendCommand<Demo.Models.Category>(Commands.Test,null,r =>
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

	public class Root<T>
	{
		public T Category;
	}
}
