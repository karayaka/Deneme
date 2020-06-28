using Deneme.DAL.Class.BaseClasses;
using Deneme.DAL.DataContexts;
using Deneme.DAL.Enums;
using Deneme.Entity.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;

namespace Deneme.Entity.Bussenes
{
    public class AppBaseRepository : IAppBaseRepository
    {
        private readonly DataContext db;
        //private readonly HttpContent httpContent;
        public AppBaseRepository(DataContext _db)
        {  
            db = _db;
        }
        public void Add<T>(T Entitiy) where T : BaseClass
        {
            Entitiy.CreatedDate = DateTime.Now;
            Entitiy.LastUpdateDate = DateTime.Now;
            Entitiy.ObjectStatus = ObjectStatus.NonDeleted;
            Entitiy.Status = Status.Active;
            db.Add(Entitiy);
            db.SaveChanges();
        }

        public void Delete<T>(int ID) where T : BaseClass
        {
            var model = db.Set<T>().FirstOrDefault(t => t.ID==ID);
            model.LastUpdateDate = DateTime.Now;
            model.ObjectStatus = ObjectStatus.Deleted;
            model.Status = Status.Pasive;
            db.Update(model);
            db.SaveChanges();
        }

        public IQueryable<T> Get<T>(Expression<Func<T, bool>> expression) where T : BaseClass
        {
            return db.Set<T>().Where(expression);
        }

        public DbSet<T> GetAllObject<T>() where T : BaseClass
        {
            return db.Set<T>();
        }

        public T GetByID<T>(int ID) where T : BaseClass
        {
            return db.Set<T>().FirstOrDefault(t => t.ID == ID);
        }

        public IQueryable<T> GetNonDeleted<T>(Expression<Func<T, bool>> expression) where T : BaseClass
        {
            IQueryable<T> models;
            if (expression != null)
            {
                models = db.Set<T>().Where(expression);
            }
            else
            {
                models = db.Set<T>();
            }
            return models.Where(t => t.ObjectStatus == ObjectStatus.NonDeleted);
        }

        public IQueryable<T> GetNonDeletedAndActive<T>(Expression<Func<T, bool>> expression) where T : BaseClass
        {
            IQueryable<T> models;
            if (expression != null)
            {
                models = db.Set<T>().Where(expression);
            }
            else
            {
                models = db.Set<T>();
            }
            return models.Where(t => t.ObjectStatus == ObjectStatus.NonDeleted && t.Status == Status.Active);
        }

        public void Update<T>(T Entitiy) where T : BaseClass
        {
            Entitiy.LastUpdateDate = DateTime.Now;
            Entitiy.ObjectStatus = ObjectStatus.NonDeleted;
            Entitiy.Status = Status.Active;
            db.Update(Entitiy);
            db.SaveChanges();
        }
    }
}
