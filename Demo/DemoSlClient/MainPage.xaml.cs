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
	public partial class MainPage : UserControl
	{
		public MainPage()
		{
			InitializeComponent();
			Loaded += Page_Loaded;
		}

		void Page_Loaded(object sender, RoutedEventArgs e)
		{
			var d = new Models.Post{BlogId=1,Content="一大段文本内容。",CreateTime=DateTime.Now,Title="Silverlight 评论"};

			this.SendCommand<int?>(Commands.Test,d , r =>
			{
				MessageBox.Show(r.Result.ToString());
			}
			,
			new DataContractXmlSerializer()
			,
			httpMethod: HttpMethod.Put
			);
		}
	}
}
