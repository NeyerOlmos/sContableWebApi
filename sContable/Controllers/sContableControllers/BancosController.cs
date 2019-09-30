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
    public class BancosController : ApiController
    {
        private sContableEntities db = new sContableEntities();

        // GET: api/Bancos
        public IQueryable<Banco> GetBancoes()
        {
            return db.Bancoes;
        }

        // GET: api/Bancos/5
        [ResponseType(typeof(Banco))]
        public async Task<IHttpActionResult> GetBanco(int id)
        {
            Banco banco = await db.Bancoes.FindAsync(id);
            if (banco == null)
            {
                return NotFound();
            }

            return Ok(banco);
        }

        // PUT: api/Bancos/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutBanco(int id, Banco banco)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != banco.Id)
            {
                return BadRequest();
            }

            db.Entry(banco).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BancoExists(id))
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

        // POST: api/Bancos
        [ResponseType(typeof(Banco))]
        public async Task<IHttpActionResult> PostBanco(Banco banco)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Bancoes.Add(banco);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = banco.Id }, banco);
        }

        // DELETE: api/Bancos/5
        [ResponseType(typeof(Banco))]
        public async Task<IHttpActionResult> DeleteBanco(int id)
        {
            Banco banco = await db.Bancoes.FindAsync(id);
            if (banco == null)
            {
                return NotFound();
            }

            db.Bancoes.Remove(banco);
            await db.SaveChangesAsync();

            return Ok(banco);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool BancoExists(int id)
        {
            return db.Bancoes.Count(e => e.Id == id) > 0;
        }
    }
}