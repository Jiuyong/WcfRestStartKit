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
	public partial class StudentsPage : Page
	{
		public StudentsPage()
		{
			InitializeComponent();
		}

		public ViewModels.StudentsPageViewModel ViewModel
		{
			get
			{
				return DataContext as ViewModels.StudentsPageViewModel;
			}
		}

		// 当用户导航到此页面时执行。
		protected override void OnNavigatedTo(NavigationEventArgs e)
		{
		}

		private void GetStudentsButton_Click(object sender,RoutedEventArgs e)
		{
			//var p = new Root<List<Models.Student>>()
			//{
			//};

			var ss = new Models.Student[10];

			for (int i = 0; i < ss.Length; i++)
			{
				ss[i] = new Models.Student()
				{
					ID = i
				};
			}

			GetStudentsButton.SendCommand<ViewModels.StudentViewModel[]>("StudentService/EvaluateStudents",ss,r =>
			{
				//StudentsDataGrid.ItemsSource = r.Result;
				ViewModel.Students.UnionWith(r.Result);
			});
		}

	}
}

namespace Demo
{
	namespace ViewModels
	{
		public class StudentsPageViewModel : ViewModelBase
		{
			private System.Collections.ObjectModel.ObservableCollection<StudentViewModel> _students = new System.Collections.ObjectModel.ObservableCollection<StudentViewModel>();
			public System.Collections.ObjectModel.ObservableCollection<StudentViewModel> Students
			{
				get
				{
					return _students;
				}

				set
				{
					_students = value;
					OnPropertyChanged("Students");
				}
			}
		}

		public class StudentViewModel:ViewModelBase
		{
			private int _iD;
			public int ID
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

			private string _name;
			public string Name
			{
				get
				{
					return _name;
				}

				set
				{
					_name = value;
					OnPropertyChanged("Name");
				}
			}

			private int _score;
			public int Score
			{
				get
				{
					return _score;
				}

				set
				{
					_score = value;
					OnPropertyChanged("Score");
				}
			}

			private DateTime _evaluationTime;
			public DateTime EvaluationTime
			{
				get
				{
					return _evaluationTime;
				}

				set
				{
					_evaluationTime = value;
					OnPropertyChanged("EvaluationTime");
				}
			}


		}
	}
}
