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
            db.Configuration.LazyLoadingEnabled = false;
            return db.Asistir;
        }

        // GET: api/Asistirs/5
        [ResponseType(typeof(Asistir))]
        [Route("api/asistirs/{idSocio}")]
        public IHttpActionResult GetAsistir(int idSocio)
        {
            db.Configuration.LazyLoadingEnabled = false;

            //Asistir asistir = db.Asistir.Find(id);
            List<Asistir> asistirs =
                (from a in db.Asistir
                 where a.idSocio == idSocio
                 select a).ToList();

            if (asistirs == null || asistirs.Count < 1)
            {
                return NotFound();
            }

            return Ok(asistirs);
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

        // NEW UPDATE:
        //  Creado método (copiando cabecera y body del método Delete) para poder actualizar
        //  asistirs
        [HttpPost]
        [Route("api/Asistirs/update/{idSocio}/{idEvento}")]
        public IHttpActionResult updateAsistir(int idSocio, short idEvento, Asistir asistir)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (idSocio != asistir.idSocio && idEvento != asistir.idEvento)
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
                if (!asistirExiste(idSocio, idEvento))
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


        // NEW DELETE:
        //  Creado método (copiando cabecera y body del método Delete) para poder eliminar
        //  asistirs
        [HttpPost]
        [Route("api/Asistirs/delete/{idSocio}/{idEvento}")]
        public IHttpActionResult DeleteAsistir(int idSocio, short idEvento)
        {
            // Modificamos la búsqueda del asistir, la cual por defecto la buscaría por una ID,
            // pero como no la pk son dos ids (socio y evento), se ha tenido que cambair
            //Asistir asistir = db.Asistir.Find(id);
            Asistir asistir =
                (from a in db.Asistir
                 where a.idSocio == idSocio && a.idEvento == idEvento
                 select a).FirstOrDefault();
            
            if (asistir == null)
            {
                return NotFound();
            }

            db.Asistir.Remove(asistir);
            db.SaveChanges();

            return Ok(asistir);
        }

        //// PUT: api/Asistirs/5
        //[ResponseType(typeof(void))]
        //public IHttpActionResult PutAsistir(int id, Asistir asistir)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    if (id != asistir.idSocio)
        //    {
        //        return BadRequest();
        //    }

        //    db.Entry(asistir).State = EntityState.Modified;

        //    try
        //    {
        //        db.SaveChanges();
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        if (!AsistirExists(id))
        //        {
        //            return NotFound();
        //        }
        //        else
        //        {
        //            throw;
        //        }
        //    }

        //    return StatusCode(HttpStatusCode.NoContent);
        //}

        //// DELETE: api/Asistirs/5
        //[ResponseType(typeof(Asistir))]
        //public IHttpActionResult DeleteAsistir(int id)
        //{
        //    Asistir asistir = db.Asistir.Find(id);
        //    if (asistir == null)
        //    {
        //        return NotFound();
        //    }

        //    db.Asistir.Remove(asistir);
        //    db.SaveChanges();

        //    return Ok(asistir);
        //}

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool asistirExiste(int idSocio, int idEvento)
        {
            return db.Asistir.Count(e => e.idSocio == idSocio && e.idEvento == idEvento) > 0;
        }

        //private bool AsistirExists(int id)
        //{
        //    return db.Asistir.Count(e => e.idSocio == id) > 0;
        //}
    }
}