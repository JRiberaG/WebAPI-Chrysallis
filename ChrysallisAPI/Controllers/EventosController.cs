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
    public class EventosController : ApiController
    {
        private ChrysallisEntities db = new ChrysallisEntities();

        // GET: api/Eventos
        public List<Eventos> GetEventos()
        {
            db.Configuration.LazyLoadingEnabled = false;

            List<Eventos> eventos = (
                from e in db.Eventos
                .Include("Asistir")

                //.Include("Comunidades")

                select e).ToList();


            return eventos;
        }

        // GET: api/Eventos/EventosByComunidad/5
        [Route("api/Eventos/EventosByComunidad/{idComunidad}")]
        public List<Eventos> GetEventosByComunidad(byte idComunidad)
        {
            db.Configuration.LazyLoadingEnabled = false;

            List<Eventos> eventos = (
                from e in db.Eventos
                .Include("Comunidades")
                .Include("Comentarios")
                where e.idComunidad == idComunidad
                select e).ToList();


            return eventos;
        }

        // GET: api/Eventos/5
        [ResponseType(typeof(Eventos))]
        public IHttpActionResult GetEventos(short id)
        {
            db.Configuration.LazyLoadingEnabled = false;

            //Eventos eventos = db.Eventos.Find(id);
            Eventos eventos = (
               from e in db.Eventos
               .Include("Comunidades")
               .Include("Comentarios")
               .Include("Asistir")
               .Include("Documentos")
               where e.id == id
               select e).FirstOrDefault();

            if (eventos == null)
            {
                return NotFound();
            }

            return Ok(eventos);
        }

        // NEW UPDATE:
        [HttpPost]
        [Route("api/Eventos/update/{id}")]
        public IHttpActionResult updateEvento(short id, Eventos eventos)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != eventos.id)
            {
                return BadRequest();
            }

            db.Entry(eventos).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EventosExists(id))
                {
                    return NotFound();
                }
                else
                {
                    return StatusCode(HttpStatusCode.NotAcceptable);
                }
            }

            return StatusCode(HttpStatusCode.OK);
        }

        //// PUT: api/Eventos/5
        //[ResponseType(typeof(void))]
        //public IHttpActionResult PutEventos(short id, Eventos eventos)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    if (id != eventos.id)
        //    {
        //        return BadRequest();
        //    }

        //    db.Entry(eventos).State = EntityState.Modified;

        //    try
        //    {
        //        db.SaveChanges();
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        if (!EventosExists(id))
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

        // POST: api/Eventos
        [ResponseType(typeof(Eventos))]
        public IHttpActionResult PostEventos(Eventos eventos)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Eventos.Add(eventos);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = eventos.id }, eventos);
        }

        // DELETE: api/Eventos/5
        [ResponseType(typeof(Eventos))]
        public IHttpActionResult DeleteEventos(short id)
        {
            Eventos eventos = db.Eventos.Find(id);
            if (eventos == null)
            {
                return NotFound();
            }

            db.Eventos.Remove(eventos);
            db.SaveChanges();

            return Ok(eventos);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool EventosExists(short id)
        {
            return db.Eventos.Count(e => e.id == id) > 0;
        }
    }
}