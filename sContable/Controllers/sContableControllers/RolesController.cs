using Microsoft.AspNet.Identity.EntityFramework;
using sContable.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace sContable.Controllers.sContableControllers
{
    public class RolesController : ApiController
    {
        ApplicationDbContext db = new ApplicationDbContext();
        // GET: api/Roles
        public IEnumerable<IdentityRole> Get()
        {
            return db.Roles.ToList();
        }

        // GET: api/Roles/5
        public async Task<IHttpActionResult> Get(string id)
        {
            IdentityRole role = db.Roles.ToList().Find((rol) => { return rol.Id == id; });
            if (role != null)
            {
                return  Content(HttpStatusCode.OK,role);
            }
            else
            {
                return NotFound();
            }

        }

        // POST: api/Roles
        public async Task<IHttpActionResult> Post(IdentityRole Rol)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Roles.Add(Rol);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = Rol.Id }, Rol);
        }

        // PUT: api/Roles/5
        public IHttpActionResult Put(string id, IdentityRole rol)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != rol.Id)
            {
                return BadRequest();
            }

            db.Entry(rol).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RolExists(id))
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

        // DELETE: api/Roles/5
        public IHttpActionResult Delete(string id)
        {
            IdentityRole Rol = db.Roles.ToList().Find(rol => { return rol.Id == id; });
            if (Rol == null)
            {
                return NotFound();
            }

            db.Roles.Remove(Rol);
            db.SaveChanges();

            return Ok(Rol);
        }
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool RolExists(string id)
        {
            return db.Roles.ToList().Count(e => e.Id == id) > 0;
        }
    }
}
