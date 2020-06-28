using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Deneme.DAL.Enums
{
    public enum Status
    {
        [Display(Name ="Aktif")]
        Active=1,

        [Display(Name = "Pasif")]
        Pasive = 0,


    }
}
