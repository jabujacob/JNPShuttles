using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebAPI.Models
{
    public class MenuPermission
    {
        public int Id { get; set; }
        public int MenuId { get; set; }
        public bool IsCreate { get; set; }
        public bool IsRead { get; set; }
        public bool IsUpdate { get; set; }
        public bool IsDelete { get; set; }
        public string MenuText { get; set; }
        public string MenuURL { get; set; }
        public int ParentID { get; set; }
    }
}