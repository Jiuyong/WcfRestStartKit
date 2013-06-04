using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Demo
{
	public static class Commands
	{
		public const string Category = "category";
	}
	
	public class Root<T>
	{
		public T Category;
	}
}
