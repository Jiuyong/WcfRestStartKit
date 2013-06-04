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

namespace Demo
{
	// 注意: 使用“重构”菜单上的“重命名”命令，可以同时更改代码和配置文件中的类名“DemoService”。
	[AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
	[ServiceBehavior(InstanceContextMode = InstanceContextMode.PerCall)]
	[ServiceContract]
	public class DemoService: IDebugService,ICoreService
	{
		[WebGet(UriTemplate=Commands.Test)]
		public Category GetCategory()
		{
			return new Category()
			{
				categoryId = "001"
				,
				categoryName= ".net"
			};
		}

		#region ICoreService 成员

		[WebGet(UriTemplate=Commands.Login)]
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

		[WebInvoke(UriTemplate = Commands.Test,Method=HttpMethod.Put)]
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
	}
}
