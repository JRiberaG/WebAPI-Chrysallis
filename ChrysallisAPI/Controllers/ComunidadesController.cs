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
    public class ComunidadesController : ApiController
    {
        private ChrysallisEntities db = new ChrysallisEntities();

        // GET: api/Comunidades
        public IQueryable<Comunidades> GetComunidades()
        {
            db.Configuration.LazyLoadingEnabled = false;
            return db.Comunidades;
        }

        // GET: api/Comunidades/5
        [ResponseType(typeof(Comunidades))]
        public IHttpActionResult GetComunidades(byte id)
        {
            db.Configuration.LazyLoadingEnabled = false;


            //Comunidades comunidades = db.Comunidades.Find(id);
            Comunidades comunidad =
                (from c in db.Comunidades
                 //.Include("Socios")   //socios de la CCAA
                 .Include("Socios1")    //socios interesados en la CCAA
                 .Include("Eventos")
                 where c.id == id
                 select c).FirstOrDefault();

            if (comunidad == null)

            {
                return NotFound();
            }

            return Ok(comunidad);
        }

        // PUT: api/Comunidades/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutComunidades(byte id, Comunidades comunidades)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != comunidades.id)
            {
                return BadRequest();
            }

            db.Entry(comunidades).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ComunidadesExists(id))
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

        // POST: api/Comunidades
        [ResponseType(typeof(Comunidades))]
        public IHttpActionResult PostComunidades(Comunidades comunidades)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Comunidades.Add(comunidades);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (ComunidadesExists(comunidades.id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = comunidades.id }, comunidades);
        }

        // DELETE: api/Comunidades/5
        [ResponseType(typeof(Comunidades))]
        public IHttpActionResult DeleteComunidades(byte id)
        {
            Comunidades comunidades = db.Comunidades.Find(id);
            if (comunidades == null)
            {
                return NotFound();
            }

            db.Comunidades.Remove(comunidades);
            db.SaveChanges();

            return Ok(comunidades);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ComunidadesExists(byte id)
        {
            return db.Comunidades.Count(e => e.id == id) > 0;
        }
    }
}