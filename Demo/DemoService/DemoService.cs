using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.ServiceModel.Activation;
using System.ServiceModel.Web;
using Jiuyong;
using Demo.Models;
using System.ComponentModel;

namespace Demo
{
	// 注意: 使用“重构”菜单上的“重命名”命令，可以同时更改代码和配置文件中的类名“DemoService”。
	[AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
	[ServiceBehavior(InstanceContextMode = InstanceContextMode.PerCall)]
	[ServiceContract]
	public class DemoService: IDebugService,ICoreService,IFileService,IClientAccessPolicy
	{
		[WebInvoke(UriTemplate=Commands.Category+@"/{id}",Method=HttpMethod.Get)]
		public Root<Category> GetCategory(string id)
		{
			return new Root<Category>()
			{
				Category = new Category()
				{
					categoryId = id
					,
					categoryName = ".net"
				}
			};
		}

		[WebInvoke(UriTemplate = Commands.Category, Method = HttpMethod.Post)]
		public bool PutGetCategory(Root<Category> root)
		{
			return null != root;
		}


		#region ICoreService 成员

		[WebGet(UriTemplate=Jiuyong.Commands.Login)]
		public string GetUserToken()
		{
			return Guid.NewGuid().ToString();
		}

		#endregion

		#region IDebugService 成员

		public DateTime DoWork()
		{
			throw new NotImplementedException();
		}

		public int? GetVersion()
		{
			throw new NotImplementedException();
		}

		public DateTime GetTest()
		{
			throw new NotImplementedException();
		}

		public DateTime PostTest()
		{
			throw new NotImplementedException();
		}

		public string GetQueryString(string name)
		{
			throw new NotImplementedException();
		}

		#endregion

		[WebInvoke(UriTemplate = Jiuyong.Commands.TestDataBase,Method=HttpMethod.Put)]
		public int PostTest(Models.Post post)
		{
			var db = new Entity.DatabaseContext();
			var p = new Entity.Post
			{
				BlogId=post.BlogId
				,
				Content=post.Content
				,
				Title=post.Title
			};
			db.Posts.Add(p);
			db.SaveChanges();
			return db.Posts.Count();
		}

		[WebGet(UriTemplate=Jiuyong.Commands.TestRoute)]
		public DateTime GetTestRoute()
		{
			return DateTime.Now;
		}

		[OperationContract]
		[WebInvoke(Method = "POST",UriTemplate = "StudentService/EvaluateStudents")]
		public List<Student> EvaluateStudents(List<Student> students)
		{
			var rand = new Random();
			foreach (var student in students)
			{
				student.Score = 60 + (int)(rand.NextDouble() * 40);
				student.EvaluationTime = DateTime.Now;
			}

			System.Threading.Thread.Sleep(3 * 1000);
			return students;
		}

		#region IFileService 成员

		[Description("分段向服务器上传文件。")]
		[WebInvoke(UriTemplate = Jiuyong.Commands.TestSendFilePartial + "?" + RequestKeys.SendFilePartialParamsString)]
		public long SendFile(System.IO.Stream data, long offset = 0, long length = -1, byte[] hash = null, string contentTpye = "application/octet-stream", HashType hashType = HashType.Sha256, string fileName = "", long totalLength = -1)
		{
			//System.Threading.Thread.Sleep(33);
			var ms = new System.IO.MemoryStream();
			data.CopyTo(ms);
			return offset + ms.Length;
		}

		public System.IO.Stream DownloadFile(byte[] sha256hash)
		{
			throw new NotImplementedException();
		}

		public Jiuyong.Models.FileInfoModel GetFileInfo(byte[] sha256hash)
		{
			throw new NotImplementedException();
		}

		public long TestSendData(System.IO.Stream data)
		{
			throw new NotImplementedException();
		}

		public long TestSendFilePartial(System.IO.Stream data, long offset = 0, long length = -1, byte[] hash = null, string contentTpye = "application/octet-stream", HashType hashType = HashType.Sha256, string fileName = "", long totalLength = -1)
		{
			throw new NotImplementedException();
		}

		#endregion

		[WebGet(UriTemplate = "/clientaccesspolicy.xml")]
		public System.IO.Stream GetPolicy()
		{
			return ClientAccessPolicy.GetPolicy();
		}

		[WebGet(UriTemplate = "/")]
		public System.IO.Stream GetDefaultPage()
		{
			return WebHost.GetFile("Web/DemoSlClientTestPage.html");
		}

		[WebGet(UriTemplate = "/client.xap")]
		public System.IO.Stream GetClientXap()
		{
			return WebHost.GetFile("Web/client.xap");
		}
	}
}
