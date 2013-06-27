﻿using System;
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
				);
			MessageBox.Show(RestClientDefault.BaseUri.ToString());
#else
			RestClientDefault.BaseUri = new Uri("http://localhost:39012/Demo/Service/");
#endif
			//this.RootVisual = new MainPage();
			//this.RootVisual = new SplitePage();
			this.RootVisual = new Views.ServerPagedCollectionViewPage();
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
	}
}
