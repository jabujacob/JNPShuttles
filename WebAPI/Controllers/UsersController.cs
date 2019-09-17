using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Http.Description;
using WebAPI.Models;


namespace WebAPI.Controllers
{
    public class UsersController : ApiController
    {
        readonly DataAccess db = new DataAccess();

        // GET: api/Users
        [ResponseType(typeof(IEnumerable<User>))]
        public IHttpActionResult Get()
        {
            return Ok(db.GetAllUsers());
        }
          // GET: api/Users/5
        [ResponseType(typeof(User))]
        public IHttpActionResult Get(int id)
        {
            User user = db.GetUser(id);

            if (user == null)
            {
                return BadRequest();
            }
            return Ok(user);
        }

        // GET: api/Users
        [ResponseType(typeof(User))]
        public IHttpActionResult Get(string username,string password)
        {
            User user = db.GetUser(username, password);
            
            if (user == null)
            {
                return BadRequest();
            }

            return Ok(user);
        }

        // POST: api/Users
        [ResponseType(typeof(User))]
        public void Post(User user)
        {

            db.InsertUser(user);

        }

        // PUT: api/users/        
        public void Put(User user)
        {
            db.UpdateUser(user);
        }      
    }
}