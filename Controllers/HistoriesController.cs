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
    public class HistoriesController : ApiController
    {
        private myproceduresEntities db = new myproceduresEntities();

        // GET: api/Histories
        public IQueryable<history> Gethistories()
        {
            return db.histories;
        }

        // GET: api/Histories/5
        [ResponseType(typeof(history))]
        public async Task<IHttpActionResult> Gethistory(int id)
        {
            history history = await db.histories.FindAsync(id);
            if (history == null)
            {
                return NotFound();
            }

            return Ok(history);
        }

        // PUT: api/Histories/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> Puthistory(int id, history history)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != history.HistoryID)
            {
                return BadRequest();
            }

            db.Entry(history).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!historyExists(id))
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

        // POST: api/Histories
        [ResponseType(typeof(history))]
        public async Task<IHttpActionResult> Posthistory(history history)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.histories.Add(history);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = history.HistoryID }, history);
        }

        // DELETE: api/Histories/5
        [ResponseType(typeof(history))]
        public async Task<IHttpActionResult> Deletehistory(int id)
        {
            history history = await db.histories.FindAsync(id);
            if (history == null)
            {
                return NotFound();
            }

            db.histories.Remove(history);
            await db.SaveChangesAsync();

            return Ok(history);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool historyExists(int id)
        {
            return db.histories.Count(e => e.HistoryID == id) > 0;
        }
    }
}