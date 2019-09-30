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
    public class Tipo_AsientosController : ApiController
    {
        private sContableEntities db = new sContableEntities();

        // GET: api/Tipo_Asientos
        public IQueryable<Tipo_Asientos> GetTipo_Asientos()
        {
            return db.Tipo_Asientos;
        }

        // GET: api/Tipo_Asientos/5
        [ResponseType(typeof(Tipo_Asientos))]
        public async Task<IHttpActionResult> GetTipo_Asientos(int id)
        {
            Tipo_Asientos tipo_Asientos = await db.Tipo_Asientos.FindAsync(id);
            if (tipo_Asientos == null)
            {
                return NotFound();
            }

            return Ok(tipo_Asientos);
        }

        // PUT: api/Tipo_Asientos/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutTipo_Asientos(int id, Tipo_Asientos tipo_Asientos)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != tipo_Asientos.cod)
            {
                return BadRequest();
            }

            db.Entry(tipo_Asientos).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!Tipo_AsientosExists(id))
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

        // POST: api/Tipo_Asientos
        [ResponseType(typeof(Tipo_Asientos))]
        public async Task<IHttpActionResult> PostTipo_Asientos(Tipo_Asientos tipo_Asientos)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Tipo_Asientos.Add(tipo_Asientos);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = tipo_Asientos.cod }, tipo_Asientos);
        }

        // DELETE: api/Tipo_Asientos/5
        [ResponseType(typeof(Tipo_Asientos))]
        public async Task<IHttpActionResult> DeleteTipo_Asientos(int id)
        {
            Tipo_Asientos tipo_Asientos = await db.Tipo_Asientos.FindAsync(id);
            if (tipo_Asientos == null)
            {
                return NotFound();
            }

            db.Tipo_Asientos.Remove(tipo_Asientos);
            await db.SaveChangesAsync();

            return Ok(tipo_Asientos);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool Tipo_AsientosExists(int id)
        {
            return db.Tipo_Asientos.Count(e => e.cod == id) > 0;
        }
    }
}