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
    public class AdministradoresController : ApiController
    {
        private ChrysallisEntities db = new ChrysallisEntities();

        // GET: api/Administradores
        public IQueryable<Administradores> GetAdministradores()
        {
            db.Configuration.LazyLoadingEnabled = false;
            return db.Administradores;
        }

        // GET: api/Administradores/5
        [ResponseType(typeof(Administradores))]
        public IHttpActionResult GetAdministradores(byte id)
        {
            IHttpActionResult resultado;

            db.Configuration.LazyLoadingEnabled = false;

            Administradores admin =
                (from a in db.Administradores
                 where a.id == id
                 select a).FirstOrDefault();

            if(admin == null)
            {
                resultado = NotFound();
            }
            else
            {
                resultado = Ok(admin);
            }
            return resultado;
        }

        // PUT: api/Administradores/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutAdministradores(byte id, Administradores administradores)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != administradores.id)
            {
                return BadRequest();
            }

            db.Entry(administradores).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AdministradoresExists(id))
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

        // POST: api/Administradores
        [ResponseType(typeof(Administradores))]
        public IHttpActionResult PostAdministradores(Administradores administradores)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Administradores.Add(administradores);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = administradores.id }, administradores);
        }

        // DELETE: api/Administradores/5
        [ResponseType(typeof(Administradores))]
        public IHttpActionResult DeleteAdministradores(byte id)
        {
            Administradores administradores = db.Administradores.Find(id);
            if (administradores == null)
            {
                return NotFound();
            }

            db.Administradores.Remove(administradores);
            db.SaveChanges();

            return Ok(administradores);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool AdministradoresExists(byte id)
        {
            return db.Administradores.Count(e => e.id == id) > 0;
        }
    }
}