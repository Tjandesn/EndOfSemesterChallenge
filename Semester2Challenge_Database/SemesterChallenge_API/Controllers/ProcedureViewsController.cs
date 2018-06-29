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
    public class ProcedureViewsController : ApiController
    {
        private Semester2ChallengeEntities db = new Semester2ChallengeEntities();

        // GET: api/ProcedureViews
        public IQueryable<ProcedureView> GetProcedureViews()
        {
            return db.ProcedureViews;
        }

        // GET: api/ProcedureViews/5
        [ResponseType(typeof(ProcedureView))]
        public async Task<IHttpActionResult> GetProcedureView(int id)
        {
            ProcedureView procedureView = await db.ProcedureViews.FirstOrDefaultAsync(p => p.ProcedureID == id);
            if (procedureView == null)
            {
                return NotFound();
            }

            return Ok(procedureView);
        }

        // PUT: api/ProcedureViews/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutProcedureView(int id, ProcedureView procedureView)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != procedureView.ProcedureID)
            {
                return BadRequest();
            }

            db.Entry(procedureView).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProcedureViewExists(id))
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

        // POST: api/ProcedureViews
        [ResponseType(typeof(ProcedureView))]
        public IHttpActionResult PostProcedureView(ProcedureView procedureView)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.ProcedureViews.Add(procedureView);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (ProcedureViewExists(procedureView.ProcedureID))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = procedureView.ProcedureID }, procedureView);
        }

        // DELETE: api/ProcedureViews/5
        [ResponseType(typeof(ProcedureView))]
        public IHttpActionResult DeleteProcedureView(int id)
        {
            ProcedureView procedureView = db.ProcedureViews.Find(id);
            if (procedureView == null)
            {
                return NotFound();
            }

            db.ProcedureViews.Remove(procedureView);
            db.SaveChanges();

            return Ok(procedureView);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ProcedureViewExists(int id)
        {
            return db.ProcedureViews.Count(e => e.ProcedureID == id) > 0;
        }
    }
}