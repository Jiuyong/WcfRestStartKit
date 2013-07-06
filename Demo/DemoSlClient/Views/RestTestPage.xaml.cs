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
using System.Windows.Navigation;
using Jiuyong;

namespace Demo.Views
{
	public partial class RestTestPage : Page
	{
		public RestTestPage()
		{
			InitializeComponent();
			GetTestDatabaseButton.Click += GetTestDatabaseButton_Click;
		}

		void GetTestDatabaseButton_Click(object sender, RoutedEventArgs e)
		{
			var dlg = new Jiuyong.BusyChildWindow();
				dlg.Show();

				this.SendCommand<int?>(Jiuyong.Commands.TestDataBase, null, r =>
				{
					dlg.AddMessage(r.Result.ToString());
					dlg.OKEnabled = r.HasResult;
				});
		}

		// 当用户导航到此页面时执行。
		protected override void OnNavigatedTo(NavigationEventArgs e)
		{
		}

	}
}
