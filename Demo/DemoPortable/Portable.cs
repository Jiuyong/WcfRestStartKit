using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Demo
{
	public static class Commands
	{
		public const string Category = "category";
		public const string SendFilePartial = "SendFilePartial";
	}
	
	public class Root<T>
	{
		public T Category;
	}
}
