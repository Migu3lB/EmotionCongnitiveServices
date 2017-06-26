﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using EmotionPlatzi.web.Models;

namespace EmotionPlatzi.web.Controllers
{
    public class EmoUploaderAPIController : ApiController
    {
        private EmotionPlatziwebContext db = new EmotionPlatziwebContext();

        // GET: api/EmoUploaderAPI
        public IQueryable<EmoPicture> GetEmoPictures()
        {
            return db.EmoPictures;
        }

        // GET: api/EmoUploaderAPI/5
        [ResponseType(typeof(EmoPicture))]
        public IHttpActionResult GetEmoPicture(int id)
        {
            EmoPicture emoPicture = db.EmoPictures.Find(id);
            if (emoPicture == null)
            {
                return NotFound();
            }

            return Ok(emoPicture);
        }

        // PUT: api/EmoUploaderAPI/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutEmoPicture(int id, EmoPicture emoPicture)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != emoPicture.Id)
            {
                return BadRequest();
            }

            db.Entry(emoPicture).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EmoPictureExists(id))
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

        // POST: api/EmoUploaderAPI
        [ResponseType(typeof(EmoPicture))]
        public IHttpActionResult PostEmoPicture(EmoPicture emoPicture)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.EmoPictures.Add(emoPicture);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = emoPicture.Id }, emoPicture);
        }

        // DELETE: api/EmoUploaderAPI/5
        [ResponseType(typeof(EmoPicture))]
        public IHttpActionResult DeleteEmoPicture(int id)
        {
            EmoPicture emoPicture = db.EmoPictures.Find(id);
            if (emoPicture == null)
            {
                return NotFound();
            }

            db.EmoPictures.Remove(emoPicture);
            db.SaveChanges();

            return Ok(emoPicture);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool EmoPictureExists(int id)
        {
            return db.EmoPictures.Count(e => e.Id == id) > 0;
        }
    }
}