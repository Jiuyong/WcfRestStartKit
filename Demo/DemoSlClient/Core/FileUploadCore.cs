using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using System.Linq;

namespace Jiuyong
{
	/// <summary>
	/// 为上传文件提供实时操作状态存取与方法。
	/// </summary>
	public class UploadFile
	{
		public readonly System.IO.Stream Stream;
		public readonly System.IO.FileInfo File;
		public readonly string Storage;
		public bool Cancelled = false;

		readonly byte[] _buffer;
		private int _bufferCount;

		public UploadFile(string path,System.IO.FileInfo storage)
		{
			_buffer = new byte[Int16.MaxValue >> 1];//BMK 只需在此调整缓冲的大小即可。32K 太大改为16K ！
			File = storage;
			Stream = File.OpenRead();
			Storage = path + File.Name;
		}

		internal void Close()
		{
			Stream.Close();
			Stream.Dispose();
		}

		internal void Read(long start)
		{
			Stream.Position = start;
			_bufferCount = Stream.Read(_buffer,0,_buffer.Length);
		}

		internal byte[] GetBuffer()
		{
			var r = _buffer;
			if (_bufferCount < _buffer.Length)
			{
				r = new byte[_bufferCount];
				System.Buffer.BlockCopy(_buffer,0,r,0,_bufferCount);
			}
			return r;
		}
	}

	public partial class ProgressViewModel<TBody> : NotifyPropertyChanged
		//where TBody:new()
	{
		//private static ProgressViewModel<TBody> _singlone = new ProgressViewModel<TBody>();

		private double _maximum;
		public double Total
		{
			get
			{
				return _maximum;
			}

			set
			{
				_maximum = value;
				OnPropertyChanged("Total");
			}
		}

		private double _value;
		public double CurrentValue
		{
			get
			{
				return _value;
			}

			set
			{
				_value = value;
				OnPropertyChanged("CurrentValue");
				OnPropertyChanged("Percent");
			}
		}

		//private double _currentPercent;
		public double Percent
		{
			get
			{
				//return _currentPercent;
				return Total == 0 ? 0 : (double)CurrentValue / Total;
			}

			//set
			//{
			//    _currentPercent = value;
			//    OnPropertyChanged("Percent");
			//}
		}


		private TBody _body;
		public TBody Body
		{
			get
			{
				return _body;
			}

			set
			{
				_body = value;
				OnPropertyChanged("Body");
			}
		}

		private bool _cancelled;
		public bool Cancelled
		{
			get
			{
				return _cancelled;
			}

			set
			{
				_cancelled = value;
				OnPropertyChanged("Cancelled");
			}
		}

		private bool _completed;
		public bool Completed
		{
			get
			{
				return _completed;
			}

			set
			{
				_completed = value;
				OnPropertyChanged("Completed");
			}
		}
	}

	public class StorageFileInfo
	{
		//索引键。
		private byte[] _sha1;
		public byte[] Sha1
		{
			get
			{
				return _sha1;
			}

			set
			{
				_sha1 = value;
				//	OnPropertyChanged("Sha1");
			}
		}

		private byte[] _sha256;
		public byte[] Sha256
		{
			get
			{
				return _sha256;
			}

			set
			{
				_sha256 = value;
				//	OnPropertyChanged("Sha256");
			}
		}

		private Guid _guid;
		public Guid Guid
		{
			get
			{
				return _guid;
			}

			set
			{
				_guid = value;
				//	OnPropertyChanged("Guid");
			}
		}

		private long _id;
		public long ID
		{
			get
			{
				return _id;
			}

			set
			{
				_id = value;
				//	OnPropertyChanged("ID");
			}
		}


		private DateTime _time;
		public DateTime Time
		{
			get
			{
				return _time;
			}

			set
			{
				_time = value;
				//	OnPropertyChanged("Time");
			}
		}

		//当前状态。
		private FileState _state;
		public FileState State
		{
			get
			{
				return _state;
			}

			set
			{
				_state = value;
				//	OnPropertyChanged("State");
			}
		}

		/// <summary>
		/// 文件当前长度。
		/// </summary>
		public long Length
		{
			get;
			set;
		}
	}

	[Flags]
	public enum FileState
	{
		Normal,

		Deleted,

		Uploading,

		Unknow
	}
}
