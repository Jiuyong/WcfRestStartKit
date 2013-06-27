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
using System.IO;
using System.Security.Cryptography;

namespace Jiuyong
{
	/// <summary>
	/// 使用 UploadFiles 方法调用，返回是否有数据发送到服务器（不一定都发送完全）。
	/// </summary>
    public partial class UploadWindow : Page
    {

        public UploadWindow()
        {
            InitializeComponent();
			this.Loaded += new RoutedEventHandler(UploadWindow_Loaded);
        }

		void UploadWindow_Loaded(object sender,RoutedEventArgs e)
		{
		}

		public UploadWindowModel Model
		{
			get
			{
				//return _model;
				return this.DataContext as UploadWindowModel;
			}

			//set
			//{
			//	_model = value;
			////	OnPropertyChanged("Model");
			//}
		}

		private void OnReady()
		{
			if (Model.Body.Count()>0)
			{
				VisualStateManager.GoToState(this,UploadReady.Name,true);
			}
		}

		private void OnComplete()
		{
			VisualStateManager.GoToState(this, UploadComplete.Name, true);
		}

		private void BeginButton_Click(object sender, RoutedEventArgs e)
		{
			BeginButton.IsEnabled = false;
			foreach (var item in Model.Body)
			{
				StartUploadFile(item, r =>
				{
					Model.CurrentValue = Model.Body.Where(x =>System.Math.Abs( x.CurrentValue - x.Body.Length)<1).Count();
					if (System.Math.Abs(Model.CurrentValue - Model.Total)<1)
					{
						Model.Completed = true;
						OnComplete();
					}
				});
			}
		}

		internal void StartUploadFile(ProgressViewModel<System.IO.FileInfo> item,Action<long> uploadCompletedCallback)
		{
			var us = new UploadFile("",item.Body);
			var sm = us.Stream;
			var lt = us.File.Length;
			//this.SendData(us.Stream,0,us.File.Length,r=>0,us.File.Length,
			this.SendFilePartial<long>("",us.Stream,r=>
			{
			    if(r.HasResult)
			    {
					item.CurrentValue = r.Result;
			        if (r.Result < item.Body.Length)//不能使用 us.Stream.Length;因为当读取时有可能会返回异常。
			        {//传输中。
			            us.Read(r.Result);
			            if (us.Cancelled || item.Cancelled)
			            {//被用户取消。
			                us.Close();
			            }
			            else
			            {
							//继续传输。
							//svr.UploadAsync(us.Storage,e.Result,us.GetBuffer());//使用流拷贝后不再使用。
			            }
			        }

			        if (r.Result == item.Body.Length)
			        {//传输完毕。
			            us.Close();
			            uploadCompletedCallback(r.Result);
			            item.Completed = true;
			        }

					//Jiuyong:完成文件不对时的处理（比如，因意外而导致的不是上传到同一个文件上的）
			    }
				else
				{
					//失去响应。
					us.Close();
					item.Cancelled = true;
				}
			},
			totalLength:us.File.Length,
			fileName:item.Body.Name,
			hash:new SHA1Managed().ComputeHash(us.Stream),
			hashType:HashType.Sha1);

			//svr.CreateFileCompleted += (s,e) =>
			//{
			//    svr.UploadFileAsync(e.Result,0,us.GetBuffer(),false,false);
			//};
			//svr.CreateFileAsync(us.File.Length);
		}

		private void SelectFilesButton_Click(object sender,RoutedEventArgs e)
		{

			var dlg = new OpenFileDialog();
			dlg.Multiselect = true;
			if (dlg.ShowDialog() == true && dlg.Files.Count() > 0)
			{
				var uw = this;
				uw.Model.Body = dlg.Files.Select(fi => new ProgressViewModel<FileInfo>()
				{
					Body = fi,
					Total = fi.Length
				}).ToArray();
				uw.Model.Total = uw.Model.Body.Length;
				OnReady();
			}
			else
			{
				//dialog.Hide();
			}
		}

		private void CompleteButton_Click(object sender,RoutedEventArgs e)
		{
			Model.Body = new ProgressViewModel<FileInfo>[0];
		}

		private void CancelButton_Click(object sender,RoutedEventArgs e)
		{
			foreach (var item in Model.Body)
			{
				item.Cancelled = true;
			}
		}
    }

	public partial class UploadWindowModel : ProgressViewModel<ProgressViewModel<System.IO.FileInfo>[]>
	{
		//public UploadWindowModel()
		//{
		//    Body = new ProgressViewModel<FileInfo>[];
		//}
	}
}

