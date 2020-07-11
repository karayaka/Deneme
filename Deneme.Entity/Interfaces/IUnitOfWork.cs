using System;
using System.Collections.Generic;
using System.Text;

namespace Deneme.Entity.Interfaces
{
    public interface IUnitOfWork:IDisposable
    {
        IAppBaseRepository BaseRepository { get; }
        IBlogRepository BlogRepository { get; }

        int SaveChange();
    }
}
