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
	public partial class App : Application
	{

		public App()
		{
			this.Startup += this.Application_Startup;
			this.Exit += this.Application_Exit;
			this.UnhandledException += this.Application_UnhandledException;

			InitializeComponent();
		}

		private void Application_Startup(object sender, StartupEventArgs e)
		{
			//RestClientDefault.ClientToken = null;
#if DEBUG
			RestClientDefault.BaseUri = new Uri(
				App.Current.Host.Source.AbsoluteUri.Replace("/ClientBin/client.xap", "/Service/")
				//@"http://202.119.11.100:8080/BookRestService/rest/categoryservice/"
				//App.Current.Host.Source.AbsoluteUri.Replace("/ClientBin/client.xap", "/rest/categoryservice/")
				//@"http://202.119.11.100:62152/StudentService/"
				);
			//MessageBox.Show(RestClientDefault.BaseUri.ToString());
#else
			RestClientDefault.BaseUri = new Uri("http://localhost:39012/Demo/Service/");
#endif

			#region 加载将所有要用到的页面的类型
			//JiuyongOK:在此之下添加业务页面，形如：
			//Register<XXXPageView>("某页");
			Register<Views.ServerPagedCollectionViewPage>("服务器端分页功能验证");
			Register<RestPage>("Rest调用验证");
			Register<Views.StudentsPage>("Rest获取数据");
			Register<UploadWindow>("上传文件");
			#endregion

			this.RootVisual = new MainPage();
			//this.RootVisual = new SplitePage();
			//this.RootVisual = new Views.ServerPagedCollectionViewPage();
		}

		private void Application_Exit(object sender, EventArgs e)
		{

		}

		private void Application_UnhandledException(object sender, ApplicationUnhandledExceptionEventArgs e)
		{
			// 如果应用程序是在调试器外运行的，则使用浏览器的
			// 异常机制报告该异常。在 IE 上，将在状态栏中用一个 
			// 黄色警报图标来显示该异常，而 Firefox 则会显示一个脚本错误。
			if (!System.Diagnostics.Debugger.IsAttached)
			{

				// 注意: 这使应用程序可以在已引发异常但尚未处理该异常的情况下
				// 继续运行。 
				// 对于生产应用程序，此错误处理应替换为向网站报告错误
				// 并停止应用程序。
				e.Handled = true;
				Deployment.Current.Dispatcher.BeginInvoke(delegate
				{
					ReportErrorToDOM(e);
				});
			}
		}

		private void ReportErrorToDOM(ApplicationUnhandledExceptionEventArgs e)
		{
			try
			{
				string errorMsg = e.ExceptionObject.Message + e.ExceptionObject.StackTrace;
				errorMsg = errorMsg.Replace('"', '\'').Replace("\r\n", @"\n");

				System.Windows.Browser.HtmlPage.Window.Eval("throw new Error(\"Unhandled Error in Silverlight Application " + errorMsg + "\");");
			}
			catch (Exception)
			{
			}
		}

		#region 类型缓存机制
		static readonly PageTypeCache pageCache = new PageTypeCache();

		/// <summary>
		/// 将TabPage注册到程序中，以便服务器调用。
		/// </summary>
		/// <typeparam name="TPage">TabPage的类型</typeparam>
		/// <param name="pageCapition">页实例的Tab主标题</param>
		private void Register<TPage>(string pageCapition)
						where TPage : Page,new()
		{
			App.pageCache.Register<TPage>(pageCapition);
		}

		/// <summary>
		/// 经过缓冲来获得数据。
		/// </summary>
		/// <param name="key">数据的键或名字。</param>
		/// <param name="callback">调用方获得数据后的回调。</param>
		public static void GetPage(string pageCapition,Action<Page> callback)
		{
			App.pageCache.Get(pageCapition,pg =>
			{
				callback(pg.Value);
			});
		}

		///// <summary>
		///// 将标签页航到对应页。
		///// </summary>
		///// <param name="pageCapition"></param>
		//public static void NavigatedTo(string pageCapition)
		//{
		//    (App.Current.RootVisual as MainPage).NavigatedTo(pageCapition);
		//}

		public static System.Collections.ObjectModel.ReadOnlyObservableCollection<PageTypeCache.Entry<Lazy<Page>>> GetRegistedPages()
		{
			return App.pageCache.Registeds;
		}
		#endregion

	}
}
