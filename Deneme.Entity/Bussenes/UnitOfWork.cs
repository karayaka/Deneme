using Deneme.DAL.DataContexts;
using Deneme.Entity.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Deneme.Entity.Bussenes
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DataContext context;

        public UnitOfWork(DataContext _context)
        {
            context = _context?? throw new ArgumentNullException("context can not be null");
        }
        private IAppBaseRepository _BaseRepository;
        private IBlogRepository _BlogRepository;

       


        public IAppBaseRepository BaseRepository
        {
            get
            {
                return _BaseRepository ?? (_BaseRepository = new AppBaseRepository(context));
            }
        }

        public IBlogRepository BlogRepository 
        {
            get
            {
                return _BlogRepository ?? (_BlogRepository = new BlogRepository(context));
            }
            
        }


        public void Dispose()
        {
            context.Dispose();
        }

        public int SaveChange()
        {
            try
            {
                return context.SaveChanges();
            }
            catch (Exception)
            {

                return -1;
            }
        }
    }
}
