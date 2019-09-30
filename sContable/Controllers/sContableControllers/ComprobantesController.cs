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
    public class ComprobantesController : ApiController
    {
        private sContableEntities db = new sContableEntities();

        // GET: api/Comprobantes
        public IQueryable<Comprobante> GetComprobantes()
        {
            return db.Comprobantes;
        }

        // GET: api/Comprobantes/5
        [ResponseType(typeof(Comprobante))]
        public async Task<IHttpActionResult> GetComprobante(int id)
        {
            Comprobante comprobante = await db.Comprobantes.FindAsync(id);
            if (comprobante == null)
            {
                return NotFound();
            }

            return Ok(comprobante);
        }

        // PUT: api/Comprobantes/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutComprobante(int id, Comprobante comprobante)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != comprobante.Nro)
            {
                return BadRequest();
            }

            db.Entry(comprobante).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ComprobanteExists(id))
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

        // POST: api/Comprobantes
        [ResponseType(typeof(Comprobante))]
        public async Task<IHttpActionResult> PostComprobante(Comprobante comprobante)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Comprobantes.Add(comprobante);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = comprobante.Nro }, comprobante);
        }

        // DELETE: api/Comprobantes/5
        [ResponseType(typeof(Comprobante))]
        public async Task<IHttpActionResult> DeleteComprobante(int id)
        {
            Comprobante comprobante = await db.Comprobantes.FindAsync(id);
            if (comprobante == null)
            {
                return NotFound();
            }

            db.Comprobantes.Remove(comprobante);
            await db.SaveChangesAsync();

            return Ok(comprobante);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ComprobanteExists(int id)
        {
            return db.Comprobantes.Count(e => e.Nro == id) > 0;
        }
    }
}