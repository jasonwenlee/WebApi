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
using WebApi.Models;

namespace WebApi.Controllers
{
    public class StepsController : ApiController
    {
        private myproceduresEntities db = new myproceduresEntities();

        // GET: api/Steps
        public IQueryable<step> Getsteps()
        {
            return db.steps;
        }

        // GET: api/Steps/5
        [ResponseType(typeof(step))]
        public async Task<IHttpActionResult> Getstep(int id)
        {
            step step = await db.steps.FindAsync(id);
            if (step == null)
            {
                return NotFound();
            }

            return Ok(step);
        }

        // PUT: api/Steps/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> Putstep(int id, step step)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != step.StepID)
            {
                return BadRequest();
            }

            db.Entry(step).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!stepExists(id))
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

        // POST: api/Steps
        [ResponseType(typeof(step))]
        public async Task<IHttpActionResult> Poststep(step step)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.steps.Add(step);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = step.StepID }, step);
        }

        // DELETE: api/Steps/5
        [ResponseType(typeof(step))]
        public async Task<IHttpActionResult> Deletestep(int id)
        {
            step step = await db.steps.FindAsync(id);
            if (step == null)
            {
                return NotFound();
            }

            db.steps.Remove(step);
            await db.SaveChangesAsync();

            return Ok(step);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool stepExists(int id)
        {
            return db.steps.Count(e => e.StepID == id) > 0;
        }
    }
}