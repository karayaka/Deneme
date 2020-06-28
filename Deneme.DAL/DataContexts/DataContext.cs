using Deneme.DAL.Class.BlogClasses;
using Deneme.DAL.Class.Definations;
using Deneme.DAL.Class.UserClasses;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Deneme.DAL.DataContexts
{
    public class DataContext:DbContext
    {
        public DataContext(DbContextOptions<DataContext> options): base(options)
        {

        }

        public DbSet<BlogClass> Blogs { get; set; }

        public DbSet<UserClass> Users { get; set; }

        public DbSet<BlogCategoriDefination> BlogCategoris { get; set; }
    }
}
