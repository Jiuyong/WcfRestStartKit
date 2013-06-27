using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Demo.Models
{
	public class Post
	{
		public int PostId;
		public string Title;
		public string Content;
		public DateTime CreateTime;
		public int BlogId;
	}

	public class Category
	{
		public string categoryId;
		public string categoryName;
	}

	public class Student
	{
		public int ID;
		public string Name;
		public int Score;
		public DateTime EvaluationTime;
	}
}
