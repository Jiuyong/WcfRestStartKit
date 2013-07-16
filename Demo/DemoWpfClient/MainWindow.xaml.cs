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
using Microsoft.Win32;
using System.Security.Cryptography;

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
			//Loaded += Window_Loaded;
			UploadFileButton.Click += new RoutedEventHandler(UploadFileButton_Click);
		}

		void UploadFileButton_Click(object sender,RoutedEventArgs e)
		{
			var dlg = new OpenFileDialog();
			if (true == dlg.ShowDialog() && dlg.CheckFileExists)
			{
				var fs = dlg.OpenFile();
				var hash = new SHA256Managed().ComputeHash(fs);
				fs.Position = 0;
				UploadFileButton.SendFilePartial<long>("tsfp",fs,r =>//tsfp —— 可以替换为实际请求的相对基址路径。
				{
					MessageBox.Show(String.Format("发送文件 {0} ，共 {1} 字节完成。",dlg.FileName,fs.Length));
				},offset:0,length:fs.Length,totalLength:fs.Length,hash:hash,fileName:dlg.SafeFileName);
			}
		}

		void Window_Loaded(object sender, RoutedEventArgs e)
		{
			//var d = new Models.Post
			//{
			//    BlogId = 1,
			//    Content = "一大段文本内容。",
			//    CreateTime = DateTime.Now,
			//    Title = "WPF 评论"
			//};


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
