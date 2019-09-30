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
    public class ChequesController : ApiController
    {
        private sContableEntities db = new sContableEntities();

        // GET: api/Cheques
        public IQueryable<Cheque> GetCheques()
        {
            return db.Cheques;
        }

        // GET: api/Cheques/5
        [ResponseType(typeof(Cheque))]
        public async Task<IHttpActionResult> GetCheque(int id)
        {
            Cheque cheque = await db.Cheques.FindAsync(id);
            if (cheque == null)
            {
                return NotFound();
            }

            return Ok(cheque);
        }

        // PUT: api/Cheques/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutCheque(int id, Cheque cheque)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != cheque.nro)
            {
                return BadRequest();
            }

            db.Entry(cheque).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ChequeExists(id))
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

        // POST: api/Cheques
        [ResponseType(typeof(Cheque))]
        public async Task<IHttpActionResult> PostCheque(Cheque cheque)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Cheques.Add(cheque);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = cheque.nro }, cheque);
        }

        // DELETE: api/Cheques/5
        [ResponseType(typeof(Cheque))]
        public async Task<IHttpActionResult> DeleteCheque(int id)
        {
            Cheque cheque = await db.Cheques.FindAsync(id);
            if (cheque == null)
            {
                return NotFound();
            }

            db.Cheques.Remove(cheque);
            await db.SaveChangesAsync();

            return Ok(cheque);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ChequeExists(int id)
        {
            return db.Cheques.Count(e => e.nro == id) > 0;
        }
    }
}