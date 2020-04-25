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
    public class DocumentosController : ApiController
    {
        private ChrysallisEntities db = new ChrysallisEntities();

        // GET: api/Documentos
        public IQueryable<Documentos> GetDocumentos()
        {
            db.Configuration.LazyLoadingEnabled = false;

            return db.Documentos;
        }

        // GET: api/Documentos/5
        [HttpGet]
        [Route("api/Documentos/{idEvento}")]
        public IHttpActionResult GetDocumentos(short idEvento)
        {
            db.Configuration.LazyLoadingEnabled = false;

            List<Documentos> documentos =
                (from d in db.Documentos
                 where d.idEvento == idEvento
                 select d).ToList();

            if (documentos == null || documentos.Count == 0)
            {
                return NotFound();
            }

            return Ok(documentos);
        }

        //// GET: api/Documentos/5
        //[ResponseType(typeof(Documentos))]
        //public IHttpActionResult GetDocumentos(int id)
        //{
        //    Documentos documentos = db.Documentos.Find(id);
        //    if (documentos == null)
        //    {
        //        return NotFound();
        //    }

        //    return Ok(documentos);
        //}
        //
        //// PUT: api/Documentos/5
        //[ResponseType(typeof(void))]
        //public IHttpActionResult PutDocumentos(int id, Documentos documentos)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    if (id != documentos.id)
        //    {
        //        return BadRequest();
        //    }

        //    db.Entry(documentos).State = EntityState.Modified;

        //    try
        //    {
        //        db.SaveChanges();
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        if (!DocumentosExists(id))
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

        //// POST: api/Documentos
        //[ResponseType(typeof(Documentos))]
        //public IHttpActionResult PostDocumentos(Documentos documentos)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    db.Documentos.Add(documentos);
        //    db.SaveChanges();

        //    return CreatedAtRoute("DefaultApi", new { id = documentos.id }, documentos);
        //}

        //// DELETE: api/Documentos/5
        //[ResponseType(typeof(Documentos))]
        //public IHttpActionResult DeleteDocumentos(int id)
        //{
        //    Documentos documentos = db.Documentos.Find(id);
        //    if (documentos == null)
        //    {
        //        return NotFound();
        //    }

        //    db.Documentos.Remove(documentos);
        //    db.SaveChanges();

        //    return Ok(documentos);
        //}

        //protected override void Dispose(bool disposing)
        //{
        //    if (disposing)
        //    {
        //        db.Dispose();
        //    }
        //    base.Dispose(disposing);
        //}

        //private bool DocumentosExists(int id)
        //{
        //    return db.Documentos.Count(e => e.id == id) > 0;
        //}
    }
}