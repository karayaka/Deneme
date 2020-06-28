using Deneme.DAL.Class.BaseClasses;
using Deneme.DAL.Class.BlogClasses;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Deneme.DAL.Class.Definations
{
    public class BlogCategoriDefination:BaseClass
    {
        [Display(Name ="Categori Adı")]
        public string CategoriName { get; set; }

        [Display(Name = "Categori Türü")]
        public int CategoriType { get; set; }

        [Display(Name = "Categori Açıklama")]
        public string CategoriDesc { get; set; }

        public ICollection<BlogClass> Blogs { get; set; }
    }
}

