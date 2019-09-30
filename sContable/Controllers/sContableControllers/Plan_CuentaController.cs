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
    public class Plan_CuentaController : ApiController
    {
        private sContableEntities db = new sContableEntities();

        // GET: api/Plan_Cuenta
        public IQueryable<Plan_Cuenta> GetPlan_Cuenta()
        {
            return db.Plan_Cuenta;
        }

        // GET: api/Plan_Cuenta/5
        [ResponseType(typeof(Plan_Cuenta))]
        public async Task<IHttpActionResult> GetPlan_Cuenta(int id)
        {
            Plan_Cuenta plan_Cuenta = await db.Plan_Cuenta.FindAsync(id);
            if (plan_Cuenta == null)
            {
                return NotFound();
            }

            return Ok(plan_Cuenta);
        }

        // PUT: api/Plan_Cuenta/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutPlan_Cuenta(int id, Plan_Cuenta plan_Cuenta)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != plan_Cuenta.Cod)
            {
                return BadRequest();
            }

            db.Entry(plan_Cuenta).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!Plan_CuentaExists(id))
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

        // POST: api/Plan_Cuenta
        [ResponseType(typeof(Plan_Cuenta))]
        public async Task<IHttpActionResult> PostPlan_Cuenta(Plan_Cuenta plan_Cuenta)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Plan_Cuenta.Add(plan_Cuenta);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = plan_Cuenta.Cod }, plan_Cuenta);
        }

        // DELETE: api/Plan_Cuenta/5
        [ResponseType(typeof(Plan_Cuenta))]
        public async Task<IHttpActionResult> DeletePlan_Cuenta(int id)
        {
            Plan_Cuenta plan_Cuenta = await db.Plan_Cuenta.FindAsync(id);
            if (plan_Cuenta == null)
            {
                return NotFound();
            }

            db.Plan_Cuenta.Remove(plan_Cuenta);
            await db.SaveChangesAsync();

            return Ok(plan_Cuenta);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool Plan_CuentaExists(int id)
        {
            return db.Plan_Cuenta.Count(e => e.Cod == id) > 0;
        }
    }
}