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
    public class ProceduresController : ApiController
    {
        private myproceduresEntities db = new myproceduresEntities();

        // GET: api/Procedures
        public IQueryable<procedure> Getprocedures()
        {
            return db.procedures.Include(i => i.steps)
                                .Include(i => i.variations)
                                .Include(i => i.complications)
                                .Include(i => i.keypoints)
                                .Include(i => i.histories)
                                .Include(i => i.references);
        }

        // GET: api/Procedures/5
        [ResponseType(typeof(procedure))]
        public async Task<IHttpActionResult> Getprocedure(int id)
        {
            procedure procedure = await db.procedures.Include(i => i.steps)
                                                     .Include(i => i.variations)
                                                     .Include(i => i.complications)
                                                     .Include(i => i.keypoints)
                                                     .Include(i => i.histories)
                                                     .Include(i => i.references)
                                                     .FirstOrDefaultAsync(i => i.ProcedureID == id);
            if (procedure == null)
            {
                return NotFound();
            }

            return Ok(procedure);
        }

        // PUT: api/Procedures/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> Putprocedure(int id, procedure procedure)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != procedure.ProcedureID)
            {
                return BadRequest();
            }

            db.Entry(procedure).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!procedureExists(id))
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

        // POST: api/Procedures
        [ResponseType(typeof(procedure))]
        public async Task<IHttpActionResult> Postprocedure(procedure procedure)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.procedures.Add(procedure);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = procedure.ProcedureID }, procedure);
        }

        // DELETE: api/Procedures/5
        [ResponseType(typeof(procedure))]
        public async Task<IHttpActionResult> Deleteprocedure(int id)
        {
            procedure procedure = await db.procedures.FindAsync(id);
            if (procedure == null)
            {
                return NotFound();
            }
            else
            {
                try
                {
                    db.steps.RemoveRange(db.steps.Where(x => x.procedure.ProcedureID == id));
                    db.keypoints.RemoveRange(db.keypoints.Where(x => x.procedure.ProcedureID == id));
                    db.variations.RemoveRange(db.variations.Where(x => x.procedure.ProcedureID == id));
                    db.complications.RemoveRange(db.complications.Where(x => x.procedure.ProcedureID == id));
                    db.histories.RemoveRange(db.histories.Where(x => x.procedure.ProcedureID == id));
                    db.references.RemoveRange(db.references.Where(x => x.procedure.ProcedureID == id));
                }
                catch (Exception)
                {
                    throw;
                }
            }
            db.procedures.Remove(procedure);
            await db.SaveChangesAsync();

            return Ok(procedure);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool procedureExists(int id)
        {
            return db.procedures.Count(e => e.ProcedureID == id) > 0;
        }
    }
}