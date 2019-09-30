using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using sContable.DAL;

namespace sContable.Controllers.sContableControllers
{
    public class Libro_DiarioController : ApiController
    {
        private sContableEntities db = new sContableEntities();

        // GET: api/Libro_Diario
        public IQueryable<Libro_Diario> GetLibro_Diario()
        {
            return db.Libro_Diario;
        }

        // GET: api/Libro_Diario/5
        [ResponseType(typeof(Libro_Diario))]
        public async Task<IHttpActionResult> GetLibro_Diario(int id)
        {
            Libro_Diario libro_Diario = await db.Libro_Diario.FindAsync(id);
            if (libro_Diario == null)
            {
                return NotFound();
            }

            return Ok(libro_Diario);
        }

        // PUT: api/Libro_Diario/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutLibro_Diario(int id, Libro_Diario libro_Diario)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != libro_Diario.cod)
            {
                return BadRequest();
            }

            db.Entry(libro_Diario).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!Libro_DiarioExists(id))
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

        // POST: api/Libro_Diario
        [ResponseType(typeof(Libro_Diario))]
        public async Task<IHttpActionResult> PostLibro_Diario(Libro_Diario libro_Diario)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Libro_Diario.Add(libro_Diario);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = libro_Diario.cod }, libro_Diario);
        }

        // DELETE: api/Libro_Diario/5
        [ResponseType(typeof(Libro_Diario))]
        public async Task<IHttpActionResult> DeleteLibro_Diario(int id)
        {
            Libro_Diario libro_Diario = await db.Libro_Diario.FindAsync(id);
            if (libro_Diario == null)
            {
                return NotFound();
            }

            db.Libro_Diario.Remove(libro_Diario);
            await db.SaveChangesAsync();

            return Ok(libro_Diario);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool Libro_DiarioExists(int id)
        {
            return db.Libro_Diario.Count(e => e.cod == id) > 0;
        }
    }
}