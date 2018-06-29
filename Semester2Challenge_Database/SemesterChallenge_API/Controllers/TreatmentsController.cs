using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using SemesterChallenge_API;

namespace SemesterChallenge_API.Controllers
{
    public class TreatmentsController : ApiController
    {
        private Semester2ChallengeEntities db = new Semester2ChallengeEntities();

        // GET: api/Treatments
        public IQueryable<Treatment> GetTreatments()
        {
            return db.Treatments;
        }

        // GET: api/Treatments/5
        [ResponseType(typeof(Treatment))]
        public IHttpActionResult GetTreatment(string id)
        {
            Treatment treatment = db.Treatments.Find(id);
            if (treatment == null)
            {
                return NotFound();
            }

            return Ok(treatment);
        }

        // PUT: api/Treatments/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutTreatment(string id, Treatment treatment)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != treatment.PetName)
            {
                return BadRequest();
            }

            db.Entry(treatment).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TreatmentExists(id))
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

        // POST: api/Treatments
        [ResponseType(typeof(Treatment))]
        public IHttpActionResult PostTreatment(Treatment treatment)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Treatments.Add(treatment);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (TreatmentExists(treatment.PetName))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = treatment.PetName }, treatment);
        }

        // DELETE: api/Treatments/5
        [ResponseType(typeof(Treatment))]
        public IHttpActionResult DeleteTreatment(string id)
        {
            Treatment treatment = db.Treatments.Find(id);
            if (treatment == null)
            {
                return NotFound();
            }

            db.Treatments.Remove(treatment);
            db.SaveChanges();

            return Ok(treatment);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool TreatmentExists(string id)
        {
            return db.Treatments.Count(e => e.PetName == id) > 0;
        }
    }
}