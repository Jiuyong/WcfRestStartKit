using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations;

namespace Demo
{
	class DemoDatabase
	{
	}

	namespace Entity
	{
		public class Blog
		{
			[Key]
			public int BlogId
			{
				get;
				set;
			}
			public string Name
			{
				get;
				set;
			}

			public virtual List<Post> Posts
			{
				get;
				set;
			}
		}

		public class Post
		{
			[Key]
			public int PostId
			{
				get;
				set;
			}
			public string Title
			{
				get;
				set;
			}
			public string Content
			{
				get;
				set;
			}

			public int BlogId
			{
				get;
				set;
			}
			public virtual Blog Blog
			{
				get;
				set;
			}
		}

		public class DatabaseContext : DbContext
		{		
			static DatabaseContext()
		{
			System.Data.Entity.Database.SetInitializer(new DatabaseContext.ModelChanges());
		}

		class ModelChanges:DropCreateDatabaseIfModelChanges<DatabaseContext>
		{
			protected override void Seed(DatabaseContext dbc)
			{
				base.Seed(dbc);

				//TODO:初始化数据库数据。
				dbc.Blogs.Add(new Blog
				{
					BlogId = 0
					,
					Name= "真没有了。。。"
				});

				dbc.SaveChanges();
			}
		}
			public DbSet<Blog> Blogs
			{
				get;
				set;
			}
			public DbSet<Post> Posts
			{
				get;
				set;
			}
		}

	}

}
