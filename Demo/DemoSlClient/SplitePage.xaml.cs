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

namespace Demo
{
    public partial class SplitePage : Page
    {
        public SplitePage()
        {
            InitializeComponent();
        }

        // 当用户导航到此网页时执行。
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
        }

		private void SplitedSelecter_Click(object sender, RoutedEventArgs e)
		{
			switch (SplitedSelecter.IsChecked)
			{
				case true:
					TopRowDefinition.MaxHeight = 0;
					BottomRowDefinition.MaxHeight = Double.PositiveInfinity;
					break;
				case false:
					TopRowDefinition.MaxHeight = Double.PositiveInfinity;
					BottomRowDefinition.MaxHeight = 0;
					break;
				default:
					TopRowDefinition.MaxHeight = BottomRowDefinition.MaxHeight = Double.PositiveInfinity;
					break;
			}
		}
    }
}
