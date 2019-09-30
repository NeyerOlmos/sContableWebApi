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
    public class CuentasController : ApiController
    {
        private sContableEntities db = new sContableEntities();

        // GET: api/Cuentas
        public IQueryable<Cuenta> GetCuentas()
        {
            return db.Cuentas;
        }

        // GET: api/Cuentas/5
        [ResponseType(typeof(Cuenta))]
        public async Task<IHttpActionResult> GetCuenta(int id)
        {
            Cuenta cuenta = await db.Cuentas.FindAsync(id);
            if (cuenta == null)
            {
                return NotFound();
            }

            return Ok(cuenta);
        }

        // PUT: api/Cuentas/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutCuenta(int id, Cuenta cuenta)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != cuenta.id)
            {
                return BadRequest();
            }

            db.Entry(cuenta).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CuentaExists(id))
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

        // POST: api/Cuentas
        [ResponseType(typeof(Cuenta))]
        public async Task<IHttpActionResult> PostCuenta(Cuenta cuenta)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Cuentas.Add(cuenta);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = cuenta.id }, cuenta);
        }

        // DELETE: api/Cuentas/5
        [ResponseType(typeof(Cuenta))]
        public async Task<IHttpActionResult> DeleteCuenta(int id)
        {
            Cuenta cuenta = await db.Cuentas.FindAsync(id);
            if (cuenta == null)
            {
                return NotFound();
            }

            db.Cuentas.Remove(cuenta);
            await db.SaveChangesAsync();

            return Ok(cuenta);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool CuentaExists(int id)
        {
            return db.Cuentas.Count(e => e.id == id) > 0;
        }
    }
}