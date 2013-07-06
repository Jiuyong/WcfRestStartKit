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
	}
}
