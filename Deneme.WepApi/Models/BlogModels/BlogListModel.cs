using Deneme.DAL.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Deneme.WepApi.Models.BlogModels
{
    public class BlogListModel
    {
        public int ID { get; set; }
        public string BlogName { get; set; }
        public string BlogContext { get; set; }
        public string BlogCategori { get; set; }
        public Status Status { get; set; }
       
    }
}
