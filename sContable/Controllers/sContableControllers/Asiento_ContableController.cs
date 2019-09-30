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
    public class Asiento_ContableController : ApiController
    {
        private sContableEntities db = new sContableEntities();

        // GET: api/Asiento_Contable
        public IQueryable<Asiento_Contable> GetAsiento_Contable()
        {
            return db.Asiento_Contable;
        }

        // GET: api/Asiento_Contable/5
        [ResponseType(typeof(Asiento_Contable))]
        public async Task<IHttpActionResult> GetAsiento_Contable(int id)
        {
            Asiento_Contable asiento_Contable = await db.Asiento_Contable.FindAsync(id);
            if (asiento_Contable == null)
            {
                return NotFound();
            }

            return Ok(asiento_Contable);
        }

        // PUT: api/Asiento_Contable/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutAsiento_Contable(int id, Asiento_Contable asiento_Contable)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != asiento_Contable.Cod)
            {
                return BadRequest();
            }

            db.Entry(asiento_Contable).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!Asiento_ContableExists(id))
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

        // POST: api/Asiento_Contable
        [ResponseType(typeof(Asiento_Contable))]
        public async Task<IHttpActionResult> PostAsiento_Contable(Asiento_Contable asiento_Contable)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Asiento_Contable.Add(asiento_Contable);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = asiento_Contable.Cod }, asiento_Contable);
        }

        // DELETE: api/Asiento_Contable/5
        [ResponseType(typeof(Asiento_Contable))]
        public async Task<IHttpActionResult> DeleteAsiento_Contable(int id)
        {
            Asiento_Contable asiento_Contable = await db.Asiento_Contable.FindAsync(id);
            if (asiento_Contable == null)
            {
                return NotFound();
            }

            db.Asiento_Contable.Remove(asiento_Contable);
            await db.SaveChangesAsync();

            return Ok(asiento_Contable);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool Asiento_ContableExists(int id)
        {
            return db.Asiento_Contable.Count(e => e.Cod == id) > 0;
        }
    }
}