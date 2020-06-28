using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Deneme.WepApi.Models.CategoriModels
{
    public class CategoriCreateModel
    {
        public string CategoriName { get; set; }

        public string CategoriDesc { get; set; }

        public int CategoriType { get; set; }
    }
}
