using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using WebAPI.Models;

namespace WebAPI.Controllers
{
    public class MenuController : ApiController
    {
        readonly DataAccess db = new DataAccess();

        // GET: api/menu/
        [ResponseType(typeof(IEnumerable<MenuPermission>))]
        public IHttpActionResult Get(int id)
        {
            IEnumerable<MenuPermission> menuPermissions = db.GetMenuPermissions(id);
            return Ok(menuPermissions);
        }
    }
}
