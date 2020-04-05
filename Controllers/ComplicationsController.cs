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
    public class ComplicationsController : ApiController
    {
        private myproceduresEntities db = new myproceduresEntities();

        // GET: api/Complications
        public IQueryable<complication> Getcomplications()
        {
            return db.complications;
        }

        // GET: api/Complications/5
        [ResponseType(typeof(complication))]
        public async Task<IHttpActionResult> Getcomplication(int id)
        {
            complication complication = await db.complications.FindAsync(id);
            if (complication == null)
            {
                return NotFound();
            }

            return Ok(complication);
        }

        // PUT: api/Complications/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> Putcomplication(int id, complication complication)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != complication.ComplicationID)
            {
                return BadRequest();
            }

            db.Entry(complication).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!complicationExists(id))
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

        // POST: api/Complications
        [ResponseType(typeof(complication))]
        public async Task<IHttpActionResult> Postcomplication(complication complication)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.complications.Add(complication);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = complication.ComplicationID }, complication);
        }

        // DELETE: api/Complications/5
        [ResponseType(typeof(complication))]
        public async Task<IHttpActionResult> Deletecomplication(int id)
        {
            complication complication = await db.complications.FindAsync(id);
            if (complication == null)
            {
                return NotFound();
            }

            db.complications.Remove(complication);
            await db.SaveChangesAsync();

            return Ok(complication);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool complicationExists(int id)
        {
            return db.complications.Count(e => e.ComplicationID == id) > 0;
        }
    }
}