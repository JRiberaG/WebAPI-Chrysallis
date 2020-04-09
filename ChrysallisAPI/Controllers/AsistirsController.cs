using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using ChrysallisAPI;

namespace ChrysallisAPI.Controllers
{
    public class AsistirsController : ApiController
    {
        private ChrysallisEntities db = new ChrysallisEntities();

        // GET: api/Asistirs
        public IQueryable<Asistir> GetAsistir()
        {
            return db.Asistir;
        }

        // GET: api/Asistirs/5
        [ResponseType(typeof(Asistir))]
        public IHttpActionResult GetAsistir(int id)
        {
            Asistir asistir = db.Asistir.Find(id);
            if (asistir == null)
            {
                return NotFound();
            }

            return Ok(asistir);
        }

        // PUT: api/Asistirs/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutAsistir(int id, Asistir asistir)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != asistir.idSocio)
            {
                return BadRequest();
            }

            db.Entry(asistir).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AsistirExists(id))
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

        // POST: api/Asistirs
        [ResponseType(typeof(Asistir))]
        public IHttpActionResult PostAsistir(Asistir asistir)
        {
            String mensaje = "";

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Asistir.Add(asistir);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException ex)
            {
                //if (AsistirExists(asistir.idSocio))
                //{
                //    return Conflict();
                //}
                //else
                //{
                //    throw;
                //}

                SqlException sqlException = (SqlException)ex.InnerException.InnerException;
                mensaje = sqlException.Number + " - " + sqlException.Message;
            }

            return CreatedAtRoute("DefaultApi", new { id = asistir.idSocio }, asistir);
        }

        // DELETE: api/Asistirs/5
        [ResponseType(typeof(Asistir))]
        public IHttpActionResult DeleteAsistir(int id)
        {
            Asistir asistir = db.Asistir.Find(id);
            if (asistir == null)
            {
                return NotFound();
            }

            db.Asistir.Remove(asistir);
            db.SaveChanges();

            return Ok(asistir);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool AsistirExists(int id)
        {
            return db.Asistir.Count(e => e.idSocio == id) > 0;
        }
    }
}