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
    public class Centro_CostosController : ApiController
    {
        private sContableEntities db = new sContableEntities();

        // GET: api/Centro_Costos
        public IQueryable<Centro_Costos> GetCentro_Costos()
        {
            return db.Centro_Costos;
        }

        // GET: api/Centro_Costos/5
        [ResponseType(typeof(Centro_Costos))]
        public async Task<IHttpActionResult> GetCentro_Costos(int id)
        {
            Centro_Costos centro_Costos = await db.Centro_Costos.FindAsync(id);
            if (centro_Costos == null)
            {
                return NotFound();
            }

            return Ok(centro_Costos);
        }

        // PUT: api/Centro_Costos/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutCentro_Costos(int id, Centro_Costos centro_Costos)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != centro_Costos.Cod)
            {
                return BadRequest();
            }

            db.Entry(centro_Costos).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!Centro_CostosExists(id))
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

        // POST: api/Centro_Costos
        [ResponseType(typeof(Centro_Costos))]
        public async Task<IHttpActionResult> PostCentro_Costos(Centro_Costos centro_Costos)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Centro_Costos.Add(centro_Costos);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = centro_Costos.Cod }, centro_Costos);
        }

        // DELETE: api/Centro_Costos/5
        [ResponseType(typeof(Centro_Costos))]
        public async Task<IHttpActionResult> DeleteCentro_Costos(int id)
        {
            Centro_Costos centro_Costos = await db.Centro_Costos.FindAsync(id);
            if (centro_Costos == null)
            {
                return NotFound();
            }

            db.Centro_Costos.Remove(centro_Costos);
            await db.SaveChangesAsync();

            return Ok(centro_Costos);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool Centro_CostosExists(int id)
        {
            return db.Centro_Costos.Count(e => e.Cod == id) > 0;
        }
    }
}