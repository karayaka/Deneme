using Deneme.DAL.Class.BaseClasses;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace Deneme.Entity.Interfaces
{
    public interface IAppBaseRepository
    {
        void Add<T>(T Entitiy) where T : BaseClass;

        void Update<T>(T Entitiy) where T : BaseClass;

        void Delete<T>(int ID) where T : BaseClass;

        IQueryable<T> Get<T>(Expression<Func<T,bool>> expression) where T : BaseClass;

        IQueryable<T> GetNonDeleted<T>(Expression<Func<T, bool>> expression) where T : BaseClass;

        IQueryable<T> GetNonDeletedAndActive<T>(Expression<Func<T, bool>> expression) where T : BaseClass;

        T GetByID<T>(int ID) where T : BaseClass;

        DbSet<T> GetAllObject<T>() where T : BaseClass;
    }
}
