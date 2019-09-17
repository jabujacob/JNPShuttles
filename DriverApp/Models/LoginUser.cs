using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace JNPShuttle.Models
{
    public class LoginUser
    {
        public string Username { get; set; }
        [DataType(DataType.Password)]
        public string Password { get; set;
        }
        
    }
}
