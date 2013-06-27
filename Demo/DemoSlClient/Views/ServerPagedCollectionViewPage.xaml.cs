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
using System.Collections.ObjectModel;
using Demo.ViewModel;
using System.ComponentModel;
using Demo.Contriver;
using Jiuyong;

namespace Demo.Views
{
	public partial class ServerPagedCollectionViewPage : Page
	{
		public ServerPagedCollectionViewPage()
		{
			InitializeComponent();
			PrePageButton.Click += new RoutedEventHandler(PrePageButton_Click);
			NextPageButton.Click += new RoutedEventHandler(NextPageButton_Click);
		}

		void NextPageButton_Click(object sender,RoutedEventArgs e)
		{
			Model.CurrentIndex += Model.PageSize;
			Model.Refresh();
		}

		void PrePageButton_Click(object sender,RoutedEventArgs e)
		{
			var idx = Model.CurrentIndex - Model.PageSize;
			Model.CurrentIndex = idx < 0 ? 0 : idx;
			Model.Refresh();
		}

		public ServerPagedViewModel Model{get{return DataContext as ServerPagedViewModel;}}

		// 当用户导航到此页面时执行。
		protected override void OnNavigatedTo(NavigationEventArgs e)
		{
			Model.PageSize = (int)((grid.ActualHeight-4) / 25);
			Model.Refresh();
		}

		private void Grid_SizeChanged(object sender,SizeChangedEventArgs e)
		{
			Model.PageSize = (int)((e.NewSize.Height-4) / 25);
			Model.Refresh();
		}

		private void Pager_PageIndexChanged(object sender,EventArgs e)
		{

		}

	}


}

namespace Demo.ViewModel
{
	public partial class ServerPagedViewModel : ViewModelBase
	{
		public ServerPagedViewModel()
		{
			Refresh();
		}

		public void Refresh()
		{
			Data.Clear();
			for (int i = 0; i < PageSize; i++)
			{
				//Data.Add(new ServerItem(){ No = CurrentIndex + i} );
				cache.Get((CurrentIndex + i).ToString(),si =>
				{
					Data.Add(si);
				},sia =>
				{
					var si = new ServerItem()
					{
						ID=Guid.NewGuid()
						,
						No=i+CurrentIndex
					};
					sia(si);
				},false);
			}
		}

		Cache<ServerItem> cache = new Cache<ServerItem>();

		private ObservableCollection<ServerItem> _data = new ObservableCollection<ServerItem>();
		public ObservableCollection<ServerItem> Data
		{
			get
			{
				return _data;
			}

			set
			{
				_data = value;
				OnPropertyChanged("Data");
			}
		}


		private int _currentIndex;
		public int CurrentIndex
		{
			get
			{
				return _currentIndex;
			}

			set
			{
				_currentIndex = value;
				OnPropertyChanged("CurrentIndex");
			}
		}

		private int _pageSize = 20;
		public int PageSize
		{
			get
			{
				return _pageSize;
			}

			set
			{
				_pageSize = value;
				OnPropertyChanged("PageSize");
			}
		}

	}

}

namespace Demo.Contriver
{
	public partial class ServerItem:ViewModelBase
	{
		private int _no;
		public int No
		{
			get
			{
				return _no;
			}

			set
			{
				_no = value;
				OnPropertyChanged("No");
			}
		}

		private Guid _iD = Guid.NewGuid();
		public Guid ID
		{
			get
			{
				return _iD;
			}

			set
			{
				_iD = value;
				OnPropertyChanged("ID");
			}
		}

	}
}

namespace Demo
{
	public partial class ServerPagedCollectionView :NotifyPropertyChanged,IPagedCollectionView
	{
		#region IPagedCollectionView 成员

		public bool CanChangePage
		{
			get
			{
				return true;
			}
		}

		private bool _isPageChanging;
		public bool IsPageChanging
		{
			get
			{
				return _isPageChanging;
			}

			set
			{
				_isPageChanging = value;
				OnPropertyChanged("IsPageChanging");
			}
		}


		private int _itemCount;
		public int ItemCount
		{
			get
			{
				return _itemCount;
			}

			set
			{
				_itemCount = value;
				OnPropertyChanged("ItemCount");
			}
		}


		private int _pageIndex;
		public int PageIndex
		{
			get
			{
				return _pageIndex;
			}

			set
			{
				_pageIndex = value;
				OnPropertyChanged("PageIndex");
			}
		}


		private int _pageSize;
		public int PageSize
		{
			get
			{
				return _pageSize;
			}

			set
			{
				_pageSize = value;
				OnPropertyChanged("PageSize");
			}
		}


		private int _totalItemCount;
		public int TotalItemCount
		{
			get
			{
				return _totalItemCount;
			}

			set
			{
				_totalItemCount = value;
				OnPropertyChanged("TotalItemCount");
			}
		}



		public bool MoveToFirstPage()
		{
			throw new NotImplementedException();
		}

		public bool MoveToLastPage()
		{
			throw new NotImplementedException();
		}

		public bool MoveToNextPage()
		{
			throw new NotImplementedException();
		}

		public bool MoveToPage(int pageIndex)
		{
			throw new NotImplementedException();
		}

		public bool MoveToPreviousPage()
		{
			throw new NotImplementedException();
		}

		public event EventHandler<EventArgs> PageChanged;
		public void OnPageChanged()
		{
			if (null==PageChanged)
			{

			}
			else
			{
				PageChanged(this,new EventArgs());
			}
		}

		public event EventHandler<PageChangingEventArgs> PageChanging;
		public void OnPageChanging(int newPageIndex)
		{
			if (null==PageChanging)
			{

			}
			else
			{
				PageChanging(this,new PageChangingEventArgs(newPageIndex));
			}
		}
	
		#endregion
	}
}
