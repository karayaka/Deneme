using Deneme.DAL.Class.BaseClasses;
using Deneme.DAL.Class.Definations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Deneme.DAL.Class.BlogClasses
{
    public class BlogClass:BaseClass
    {
        [Display(Name = "Blog Başlığı")]
        public string BlogTitle { get; set; }

        [Display(Name = "Blog İçeriği")]
        public string BlogContext { get; set; }

        [Display(Name = "Yayın Tarihi")]
        public DateTime PublishDate { get; set; }

        public int CategoriID { get; set; }
        public virtual BlogCategoriDefination Categori { get; set; }
    }
}
