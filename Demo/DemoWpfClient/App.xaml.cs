using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Windows;
using Jiuyong;
namespace Demo
{
	/// <summary>
	/// App.xaml 的交互逻辑
	/// </summary>
	public partial class App : Application
	{
		protected override void OnStartup(StartupEventArgs e)
		{
			base.OnStartup(e);
			RestClientDefault.BaseUri = new Uri(
//@"http://localhost:39012/Demo/Service/"
@"http://202.119.11.100:8080/BookRestService/rest/categoryservice/"
);
		}
	}
}
