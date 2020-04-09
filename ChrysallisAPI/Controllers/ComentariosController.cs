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
    public class ComentariosController : ApiController
    {
        private ChrysallisEntities db = new ChrysallisEntities();

        // GET: api/Comentarios
        public IQueryable<Comentarios> GetComentario()
        {
            db.Configuration.LazyLoadingEnabled = false;
            return db.Comentarios;
        }

        /*// GET: api/Comentarios/5
        [ResponseType(typeof(Comentarios))]
        public IHttpActionResult GetComentario(short id)
        {
            Comentarios comentario = db.Comentarios.Find(id);
            if (comentario == null)
            {
                return NotFound();
            }

            return Ok(comentario);
        }*/

        // GET: api/Comentarios/ComentariosByEvento/5
        [Route("api/Comentarios/ComentariosByEvento/{idEvento}")]
        public IHttpActionResult GetComentarioByEvento(short idEvento)
        {
            IHttpActionResult resultado;

            db.Configuration.LazyLoadingEnabled = false;

            List<Comentarios> comentarios =
                (from c in db.Comentarios
                 .Include("Socios")
                 where c.idEvento == idEvento
                 select c).ToList();

            if (comentarios == null)
            {
                resultado = NotFound();
            }
            else
            {
                resultado = Ok(comentarios);
            }
            return resultado;
        }

        // GET: api/Comentarios/ComentariosBySocio/5
        [Route("api/Comentarios/ComentariosBySocio/{idSocio}")]
        public IHttpActionResult GetComentarioBySocio(int idSocio)
        {
            IHttpActionResult resultado;

            db.Configuration.LazyLoadingEnabled = false;

            List<Comentarios> comentarios =
                (from c in db.Comentarios
                 where c.idSocio == idSocio
                 select c).ToList();

            if (comentarios == null)
            {
                resultado = NotFound();
            }
            else
            {
                resultado = Ok(comentarios);
            }
            return resultado;
        }

        // PUT: api/Comentarios/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutComentario(short id, Comentarios comentario)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != comentario.idEvento)
            {
                return BadRequest();
            }

            db.Entry(comentario).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ComentarioExists(id))
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

        // POST: api/Comentarios
        [ResponseType(typeof(Comentarios))]
        public IHttpActionResult PostComentario(Comentarios comentario)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Comentarios.Add(comentario);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (ComentarioExists(comentario.idEvento))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = comentario.idEvento }, comentario);
        }

        // DELETE: api/Comentarios/5
        [ResponseType(typeof(Comentarios))]
        public IHttpActionResult DeleteComentario(short id)
        {
            Comentarios comentario = db.Comentarios.Find(id);
            if (comentario == null)
            {
                return NotFound();
            }

            db.Comentarios.Remove(comentario);
            db.SaveChanges();

            return Ok(comentario);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ComentarioExists(short id)
        {
            return db.Comentarios.Count(e => e.idEvento == id) > 0;
        }
    }
}