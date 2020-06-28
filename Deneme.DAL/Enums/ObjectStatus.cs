using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Deneme.DAL.Enums
{
    public enum ObjectStatus:byte
    {
        [Display(Name = "Silindi")]
        Deleted =0,
        [Display(Name = "Silinmedi")]
        NonDeleted =1

    }
}
