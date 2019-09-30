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
    public class MonedasController : ApiController
    {
        private sContableEntities db = new sContableEntities();

        // GET: api/Monedas
        public IQueryable<Moneda> GetMonedas()
        {
            return db.Monedas;
        }

        // GET: api/Monedas/5
        [ResponseType(typeof(Moneda))]
        public async Task<IHttpActionResult> GetMoneda(int id)
        {
            Moneda moneda = await db.Monedas.FindAsync(id);
            if (moneda == null)
            {
                return NotFound();
            }

            return Ok(moneda);
        }

        // PUT: api/Monedas/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutMoneda(int id, Moneda moneda)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != moneda.id)
            {
                return BadRequest();
            }

            db.Entry(moneda).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MonedaExists(id))
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

        // POST: api/Monedas
        [ResponseType(typeof(Moneda))]
        public async Task<IHttpActionResult> PostMoneda(Moneda moneda)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Monedas.Add(moneda);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = moneda.id }, moneda);
        }

        // DELETE: api/Monedas/5
        [ResponseType(typeof(Moneda))]
        public async Task<IHttpActionResult> DeleteMoneda(int id)
        {
            Moneda moneda = await db.Monedas.FindAsync(id);
            if (moneda == null)
            {
                return NotFound();
            }

            db.Monedas.Remove(moneda);
            await db.SaveChangesAsync();

            return Ok(moneda);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool MonedaExists(int id)
        {
            return db.Monedas.Count(e => e.id == id) > 0;
        }
    }
}