using Deneme.DAL.Class.BlogClasses;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Deneme.Entity.Interfaces
{
    public interface IBlogRepository:IAppBaseRepository
    {
        public IQueryable<BlogClass> GetFiveBlog();
    }
}
