using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Web;
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
			Loaded += MainWindow_Loaded;
		}

		void MainWindow_Loaded(object sender, RoutedEventArgs e)
		{
			WebServiceHost host;

			host = new WebServiceHost(typeof(Demo.DemoService), new Uri("http://localhost:80/product/smartinfobase/"));

			try
			{
				host.Open();

				Console.WriteLine("The Silverlight service is ready.");
				Console.WriteLine("Press <ENTER> to terminate service.");
				Console.WriteLine();
				//Console.ReadLine();
			}
			catch (CommunicationException ce)
			{
				Console.WriteLine("An exception occured: {0}", ce.Message);
				host.Abort();
			}
		}

		private void Hyperlink_Click(object sender, RoutedEventArgs e)
		{
			if (sender is Hyperlink)
			{
				var sd = sender as Hyperlink;
				var uri = sd.NavigateUri.ToString();
				try
				{
					System.Diagnostics.Process.Start(uri);
				}
				catch (System.ComponentModel.Win32Exception we)//可能文件夹并不存在。
				{
					//vs.ShowError("打开地址“{1}”出现错误“{0}”。", we.Message, uri);
					MessageBox.Show(String.Format("打开地址“{1}”出现错误“{0}”。", we.Message, uri));
				}
			}
		}
	}
}
