using Microsoft.Ajax.Utilities;
using sContable.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace sContable.Controllers.sContableControllers
{
    public class UsersController : ApiController
    {
        ApplicationDbContext db = new ApplicationDbContext(); 
        // GET: api/Users
        public IEnumerable<ApplicationUser> Get()
        {
            return db.Users.ToList();
        }

        // GET: api/Users/5
        public IHttpActionResult Get(string id)
        {
            ApplicationUser applicationUser = db.Users.ToList().Find((s) => { return s.Id == id; });
            if (applicationUser != null)
            {
                return Content(HttpStatusCode.OK, applicationUser);
            }
            else
            {
                return NotFound();
            }
        }

        // POST: api/Users
        public IHttpActionResult Post(ApplicationUser user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Users.Add(user);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = user.Id }, user);
        }

        // PUT: api/Users/5
        public async Task<IHttpActionResult> Put(string id, ApplicationUser user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != user.Id)
            {
                return BadRequest();
            }

            db.Entry(user).State = System.Data.Entity.EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // DELETE: api/Users/5
        public IHttpActionResult Delete(string id)
        {
            ApplicationUser user = db.Users.ToList().Find(rol => { return rol.Id == id; });
            if (user == null)
            {
                return NotFound();
            }

            db.Users.Remove(user);
            db.SaveChanges();

            return Ok(user);
        }
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
        private bool UserExists(string id)
        {
            return db.Users.Count(e => e.Id == id) > 0;
        }
    }
}
