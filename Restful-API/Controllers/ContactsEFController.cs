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
using Restful_API.Models;

namespace Restful_API.Controllers
{
    [RoutePrefix("api/ContactsEF")]
    public class ContactsEFController : ApiController
    {
        private Restful_APIContext db = new Restful_APIContext();

        // GET: api/ContactsEF
        [Route("")]
        public IQueryable<Contacts> GetContacts()
        {
            return db.Contacts;
        }

        // GET: api/ContactsEF/5
        [ResponseType(typeof(Contacts))]
        [Route("{id:int}")]
        public IHttpActionResult GetContacts(int id)
        {
            Contacts contacts = db.Contacts.Find(id);
            if (contacts == null)
            {
                return NotFound();
            }

            return Ok(contacts);
        }

        // PUT: api/ContactsEF/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutContacts(int id, Contacts contacts)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != contacts.Id)
            {
                return BadRequest();
            }

            db.Entry(contacts).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ContactsExists(id))
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

        // POST: api/ContactsEF
        [ResponseType(typeof(Contacts))]
        [Route("")]
        public IHttpActionResult PostContacts(Contacts contacts)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Contacts.Add(contacts);
            db.SaveChanges();

            return Ok(contacts)
        }

        // DELETE: api/ContactsEF/5
        [ResponseType(typeof(Contacts))]
        public IHttpActionResult DeleteContacts(int id)
        {
            Contacts contacts = db.Contacts.Find(id);
            if (contacts == null)
            {
                return NotFound();
            }

            db.Contacts.Remove(contacts);
            db.SaveChanges();

            return Ok(contacts);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ContactsExists(int id)
        {
            return db.Contacts.Count(e => e.Id == id) > 0;
        }
    }
}