using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.ServiceModel.Activation;
using System.ServiceModel.Web;

namespace Jiuyong
{
	// 注意: 使用“重构”菜单上的“重命名”命令，可以同时更改代码和配置文件中的类名“DemoService”。
	[AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
	[ServiceBehavior(InstanceContextMode = InstanceContextMode.PerCall)]
	[ServiceContract]
	public class DemoService: IDebugService,ICoreService
	{

		#region ICoreService 成员

		[WebGet(UriTemplate=Commands.Login)]
		public string GetUserToken()
		{
			return RestClientDefault.ClientToken;
		}

		#endregion

		#region IDebugService 成员

		DateTime IDebugService.DoWork()
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
	}
}
