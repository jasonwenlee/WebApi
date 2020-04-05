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
    public class VariationsController : ApiController
    {
        private myproceduresEntities db = new myproceduresEntities();

        // GET: api/Variations
        public IQueryable<variation> Getvariations()
        {
            return db.variations;
        }

        // GET: api/Variations/5
        [ResponseType(typeof(variation))]
        public async Task<IHttpActionResult> Getvariation(int id)
        {
            variation variation = await db.variations.FindAsync(id);
            if (variation == null)
            {
                return NotFound();
            }

            return Ok(variation);
        }

        // PUT: api/Variations/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> Putvariation(int id, variation variation)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != variation.VariationID)
            {
                return BadRequest();
            }

            db.Entry(variation).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!variationExists(id))
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

        // POST: api/Variations
        [ResponseType(typeof(variation))]
        public async Task<IHttpActionResult> Postvariation(variation variation)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.variations.Add(variation);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = variation.VariationID }, variation);
        }

        // DELETE: api/Variations/5
        [ResponseType(typeof(variation))]
        public async Task<IHttpActionResult> Deletevariation(int id)
        {
            variation variation = await db.variations.FindAsync(id);
            if (variation == null)
            {
                return NotFound();
            }

            db.variations.Remove(variation);
            await db.SaveChangesAsync();

            return Ok(variation);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool variationExists(int id)
        {
            return db.variations.Count(e => e.VariationID == id) > 0;
        }
    }
}