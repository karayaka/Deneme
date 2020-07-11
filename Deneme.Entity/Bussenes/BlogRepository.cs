using Deneme.DAL.Class.BlogClasses;
using Deneme.DAL.DataContexts;
using Deneme.Entity.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace Deneme.Entity.Bussenes
{
    public class BlogRepository : AppBaseRepository, IBlogRepository
    {
        private readonly DataContext db;

        public BlogRepository(DataContext _db) : base(_db)
        {
            db = _db;
        }   

        public IQueryable<BlogClass> GetFiveBlog()
        {
            return db.Blogs.Where(t => t.ObjectStatus == DAL.Enums.ObjectStatus.NonDeleted).Take(5);
        }
    }
}
