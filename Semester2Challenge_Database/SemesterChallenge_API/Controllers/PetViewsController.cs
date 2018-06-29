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
    public class PetViewsController : ApiController
    {
        private Semester2ChallengeEntities db = new Semester2ChallengeEntities();

        // GET: api/PetViews
        public IQueryable<PetView> GetPetViews()
        {
            return db.PetViews;
        }

        // GET: api/PetViews/5
        [ResponseType(typeof(PetView))]
        public async Task<IHttpActionResult> GetPetView(string PetName, int id)
        {
            PetView petView = await db.PetViews.FirstOrDefaultAsync(p => p.PetName == PetName && p.OwnerID == id);
            if (petView == null)
            {
                return NotFound();
            }

            return Ok(petView);
        }

        // PUT: api/PetViews/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutPetView(string id, PetView petView)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != petView.PetName)
            {
                return BadRequest();
            }

            db.Entry(petView).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PetViewExists(id))
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

        // POST: api/PetViews
        [ResponseType(typeof(PetView))]
        public IHttpActionResult PostPetView(PetView petView)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.PetViews.Add(petView);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (PetViewExists(petView.PetName))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = petView.PetName }, petView);
        }

        // DELETE: api/PetViews/5
        [ResponseType(typeof(PetView))]
        public IHttpActionResult DeletePetView(string id)
        {
            PetView petView = db.PetViews.Find(id);
            if (petView == null)
            {
                return NotFound();
            }

            db.PetViews.Remove(petView);
            db.SaveChanges();

            return Ok(petView);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool PetViewExists(string id)
        {
            return db.PetViews.Count(e => e.PetName == id) > 0;
        }
    }
}