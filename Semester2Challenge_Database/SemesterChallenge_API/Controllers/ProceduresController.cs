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
    public class ProceduresController : ApiController
    {
        private Semester2ChallengeEntities db = new Semester2ChallengeEntities();

        // GET: api/Procedures
        public IQueryable<Procedure> GetProcedures()
        {
            return db.Procedures;
        }

        // GET: api/Procedures/5
        [ResponseType(typeof(Procedure))]
        public IHttpActionResult GetProcedure(int id)
        {
            Procedure procedure = db.Procedures.Find(id);
            if (procedure == null)
            {
                return NotFound();
            }

            return Ok(procedure);
        }

        // PUT: api/Procedures/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutProcedure(int id, Procedure procedure)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != procedure.ProcedureID)
            {
                return BadRequest();
            }

            db.Entry(procedure).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProcedureExists(id))
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

        // POST: api/Procedures
        [ResponseType(typeof(Procedure))]
        public IHttpActionResult PostProcedure(Procedure procedure)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Procedures.Add(procedure);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (ProcedureExists(procedure.ProcedureID))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = procedure.ProcedureID }, procedure);
        }

        // DELETE: api/Procedures/5
        [ResponseType(typeof(Procedure))]
        public IHttpActionResult DeleteProcedure(int id)
        {
            Procedure procedure = db.Procedures.Find(id);
            if (procedure == null)
            {
                return NotFound();
            }

            db.Procedures.Remove(procedure);
            db.SaveChanges();

            return Ok(procedure);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ProcedureExists(int id)
        {
            return db.Procedures.Count(e => e.ProcedureID == id) > 0;
        }
    }
}