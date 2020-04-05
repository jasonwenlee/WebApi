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
    public class ReferencesController : ApiController
    {
        private myproceduresEntities db = new myproceduresEntities();

        // GET: api/References
        public IQueryable<reference> Getreferences()
        {
            return db.references;
        }

        // GET: api/References/5
        [ResponseType(typeof(reference))]
        public async Task<IHttpActionResult> Getreference(int id)
        {
            reference reference = await db.references.FindAsync(id);
            if (reference == null)
            {
                return NotFound();
            }

            return Ok(reference);
        }

        // PUT: api/References/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> Putreference(int id, reference reference)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != reference.ReferenceID)
            {
                return BadRequest();
            }

            db.Entry(reference).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!referenceExists(id))
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

        // POST: api/References
        [ResponseType(typeof(reference))]
        public async Task<IHttpActionResult> Postreference(reference reference)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.references.Add(reference);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = reference.ReferenceID }, reference);
        }

        // DELETE: api/References/5
        [ResponseType(typeof(reference))]
        public async Task<IHttpActionResult> Deletereference(int id)
        {
            reference reference = await db.references.FindAsync(id);
            if (reference == null)
            {
                return NotFound();
            }

            db.references.Remove(reference);
            await db.SaveChangesAsync();

            return Ok(reference);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool referenceExists(int id)
        {
            return db.references.Count(e => e.ReferenceID == id) > 0;
        }
    }
}