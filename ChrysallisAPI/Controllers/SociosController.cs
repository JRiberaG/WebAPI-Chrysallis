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
using ChrysallisAPI;

namespace ChrysallisAPI.Controllers
{
    public class SociosController : ApiController
    {
        private ChrysallisEntities db = new ChrysallisEntities();

        // GET: api/Socios
        public IQueryable<Socios> GetSocios()
        {
            db.Configuration.LazyLoadingEnabled = false;
            return db.Socios;
        }

        // GET: api/Socios/5
        [ResponseType(typeof(Socios))]
        public IHttpActionResult GetSocios(int id)
        {
            db.Configuration.LazyLoadingEnabled = false;

            Socios socios = db.Socios.Find(id);
            if (socios == null)
            {
                return NotFound();
            }

            return Ok(socios);
        }

        // PUT: api/Socios/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutSocios(int id, Socios socios)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != socios.id)
            {
                return BadRequest();
            }

            db.Entry(socios).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SociosExists(id))
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

        // POST: api/Socios
        [ResponseType(typeof(Socios))]
        public IHttpActionResult PostSocios(Socios socios)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Socios.Add(socios);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = socios.id }, socios);
        }

        // DELETE: api/Socios/5
        [ResponseType(typeof(Socios))]
        public IHttpActionResult DeleteSocios(int id)
        {
            Socios socios = db.Socios.Find(id);
            if (socios == null)
            {
                return NotFound();
            }

            db.Socios.Remove(socios);
            db.SaveChanges();

            return Ok(socios);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool SociosExists(int id)
        {
            return db.Socios.Count(e => e.id == id) > 0;
        }
    }
}