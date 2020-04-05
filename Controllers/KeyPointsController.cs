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
    public class KeyPointsController : ApiController
    {
        private myproceduresEntities db = new myproceduresEntities();

        // GET: api/KeyPoints
        public IQueryable<keypoint> Getkeypoints()
        {
            return db.keypoints;
        }

        // GET: api/KeyPoints/5
        [ResponseType(typeof(keypoint))]
        public async Task<IHttpActionResult> Getkeypoint(int id)
        {
            keypoint keypoint = await db.keypoints.FindAsync(id);
            if (keypoint == null)
            {
                return NotFound();
            }

            return Ok(keypoint);
        }

        // PUT: api/KeyPoints/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> Putkeypoint(int id, keypoint keypoint)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != keypoint.KeyPointID)
            {
                return BadRequest();
            }

            db.Entry(keypoint).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!keypointExists(id))
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

        // POST: api/KeyPoints
        [ResponseType(typeof(keypoint))]
        public async Task<IHttpActionResult> Postkeypoint(keypoint keypoint)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.keypoints.Add(keypoint);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = keypoint.KeyPointID }, keypoint);
        }

        // DELETE: api/KeyPoints/5
        [ResponseType(typeof(keypoint))]
        public async Task<IHttpActionResult> Deletekeypoint(int id)
        {
            keypoint keypoint = await db.keypoints.FindAsync(id);
            if (keypoint == null)
            {
                return NotFound();
            }

            db.keypoints.Remove(keypoint);
            await db.SaveChangesAsync();

            return Ok(keypoint);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool keypointExists(int id)
        {
            return db.keypoints.Count(e => e.KeyPointID == id) > 0;
        }
    }
}