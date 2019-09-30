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
    public class Tipo_CambioController : ApiController
    {
        private sContableEntities db = new sContableEntities();

        // GET: api/Tipo_Cambio
        public IQueryable<Tipo_Cambio> GetTipo_Cambio()
        {
            return db.Tipo_Cambio;
        }

        // GET: api/Tipo_Cambio/5
        [ResponseType(typeof(Tipo_Cambio))]
        public async Task<IHttpActionResult> GetTipo_Cambio(int id)
        {
            Tipo_Cambio tipo_Cambio = await db.Tipo_Cambio.FindAsync(id);
            if (tipo_Cambio == null)
            {
                return NotFound();
            }

            return Ok(tipo_Cambio);
        }

        // PUT: api/Tipo_Cambio/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutTipo_Cambio(int id, Tipo_Cambio tipo_Cambio)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != tipo_Cambio.id)
            {
                return BadRequest();
            }

            db.Entry(tipo_Cambio).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!Tipo_CambioExists(id))
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

        // POST: api/Tipo_Cambio
        [ResponseType(typeof(Tipo_Cambio))]
        public async Task<IHttpActionResult> PostTipo_Cambio(Tipo_Cambio tipo_Cambio)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Tipo_Cambio.Add(tipo_Cambio);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = tipo_Cambio.id }, tipo_Cambio);
        }

        // DELETE: api/Tipo_Cambio/5
        [ResponseType(typeof(Tipo_Cambio))]
        public async Task<IHttpActionResult> DeleteTipo_Cambio(int id)
        {
            Tipo_Cambio tipo_Cambio = await db.Tipo_Cambio.FindAsync(id);
            if (tipo_Cambio == null)
            {
                return NotFound();
            }

            db.Tipo_Cambio.Remove(tipo_Cambio);
            await db.SaveChangesAsync();

            return Ok(tipo_Cambio);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool Tipo_CambioExists(int id)
        {
            return db.Tipo_Cambio.Count(e => e.id == id) > 0;
        }
    }
}