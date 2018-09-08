using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Web.Http.Description;
using TMSAPI.Models;

namespace TMSAPI.Controllers
{
    [EnableCors(origins: "http://localhost:49311/", headers: "*", methods: "*")]
    
    
    public class UsersController : ApiController
    {
        private TMS_DbEntities db = new TMS_DbEntities();


        // POST: api/Users
        [ResponseType(typeof(User))]
        public string PostUser(User user)
        {
            if (ModelState.IsValid)
            {
                db.Users.Add(user);
                db.SaveChanges();
                return "success";
            }
            else
                return "Fail";
        }

        [ResponseType(typeof(User))]
        public string CheckUser(User user)
        {
            var Valid = from item in db.Users
                        where user.UserName == item.UserName &&
                        user.UserPassword == item.UserPassword
                        select item.UserID;
            if (Valid.Count() >= 1)
                return "Valid";
            else
                return "Invalid";
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool UserExists(int id)
        {
            return db.Users.Count(e => e.UserID == id) > 0;
        }
    }
}