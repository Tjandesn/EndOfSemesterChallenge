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
using SemesterChallenge_API;

namespace SemesterChallenge_API.Controllers
{
    public class OwnerViewsController : ApiController
    {
        private Semester2ChallengeEntities db = new Semester2ChallengeEntities();

        // GET: api/OwnerViews
        public IQueryable<OwnerView> GetOwnerViews()
        {
            return db.OwnerViews;
        }

        // GET: api/OwnerViews/5
        [ResponseType(typeof(OwnerView))]
        public async Task<IHttpActionResult> GetOwnerView(int id)
        {
            OwnerView ownerView = await db.OwnerViews.FirstOrDefaultAsync(o => o.OwnerId == id);
            if (ownerView == null)
            {
                return NotFound();
            }

            return Ok(ownerView);
        }

        // PUT: api/OwnerViews/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutOwnerView(int id, OwnerView ownerView)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != ownerView.OwnerId)
            {
                return BadRequest();
            }

            db.Entry(ownerView).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OwnerViewExists(id))
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

        // POST: api/OwnerViews
        [ResponseType(typeof(OwnerView))]
        public IHttpActionResult PostOwnerView(OwnerView ownerView)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.OwnerViews.Add(ownerView);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (OwnerViewExists(ownerView.OwnerId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = ownerView.OwnerId }, ownerView);
        }

        // DELETE: api/OwnerViews/5
        [ResponseType(typeof(OwnerView))]
        public IHttpActionResult DeleteOwnerView(int id)
        {
            OwnerView ownerView = db.OwnerViews.Find(id);
            if (ownerView == null)
            {
                return NotFound();
            }

            db.OwnerViews.Remove(ownerView);
            db.SaveChanges();

            return Ok(ownerView);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool OwnerViewExists(int id)
        {
            return db.OwnerViews.Count(e => e.OwnerId == id) > 0;
        }
    }
}