using Deneme.DAL.Class.BaseClasses;
using System;
using System.Collections.Generic;
using System.Text;

namespace Deneme.DAL.Class.UserClasses
{
    public class UserClass:BaseClass
    {
        public string UserName { get; set; }

        public string Email { get; set; }

        public byte[] PasswordHash { get; set; }

        public byte[] PasswordSalt { get; set; }

        public string userPassword { get; set; }
    }
}
