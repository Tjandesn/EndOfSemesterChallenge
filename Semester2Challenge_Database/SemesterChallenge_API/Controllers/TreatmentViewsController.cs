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
    public class TreatmentViewsController : ApiController
    {
        private Semester2ChallengeEntities db = new Semester2ChallengeEntities();

        // GET: api/TreatmentViews
        public IQueryable<TreatmentView> GetTreatmentViews()
        {
            return db.TreatmentViews;
        }

        // GET: api/TreatmentViews/5
        [ResponseType(typeof(TreatmentView))]
        public async Task<IHttpActionResult>GetTreatmentView(int id, string PetName, int ProcedureID, DateTime Date)
        {
            TreatmentView treatmentView = await db.TreatmentViews.FirstOrDefaultAsync(t => t.OwnerID == id && t.PetName == PetName && t.ProcedureID == ProcedureID && t.Date == Date);
            if (treatmentView == null)
            {
                return NotFound();
            }

            return Ok(treatmentView);
        }

        // PUT: api/TreatmentViews/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutTreatmentView(string id, TreatmentView treatmentView)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != treatmentView.PetName)
            {
                return BadRequest();
            }

            db.Entry(treatmentView).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TreatmentViewExists(id))
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

        // POST: api/TreatmentViews
        [ResponseType(typeof(TreatmentView))]
        public IHttpActionResult PostTreatmentView(TreatmentView treatmentView)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.TreatmentViews.Add(treatmentView);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (TreatmentViewExists(treatmentView.PetName))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = treatmentView.PetName }, treatmentView);
        }

        // DELETE: api/TreatmentViews/5
        [ResponseType(typeof(TreatmentView))]
        public IHttpActionResult DeleteTreatmentView(string id)
        {
            TreatmentView treatmentView = db.TreatmentViews.Find(id);
            if (treatmentView == null)
            {
                return NotFound();
            }

            db.TreatmentViews.Remove(treatmentView);
            db.SaveChanges();

            return Ok(treatmentView);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool TreatmentViewExists(string id)
        {
            return db.TreatmentViews.Count(e => e.PetName == id) > 0;
        }
    }
}