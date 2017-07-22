using Restful_API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Restful_API.Controllers
{
    //Add the prefix above the class
    [RoutePrefix("api/Contact")]
    public class ContactController : ApiController
    {
        Contacts[] contacts = new Contacts[]
        {
            new Contacts(){Id = 0, FirstName = "Peter", LastName = "Parker" },
            new Contacts(){Id = 1, FirstName = "Bruce", LastName = "Wayne" },
            new Contacts(){Id = 2, FirstName = "Bruce", LastName = "Banner" }
        };

        // GET: api/Contact
        [Route("")]
        [HttpGet]
        public IEnumerable<Contacts> Get()
        {
            return contacts;
        }

        // GET: api/Contact/5
        [Route("{id:int}")]
        [HttpGet]
        public IHttpActionResult Get(int id)
        {
            //Get the first contact in the contacts list with the specified id
            Contacts contact = contacts.FirstOrDefault<Contacts>(c => c.Id == id);

            if(contact == null)
            {
                return NotFound();
            }

            //If all goes well return the contact associated with the Id
            return Ok(contact);
        }

        [Route("{name}")]
        [HttpGet]
        public IEnumerable<Contacts> FindContactByName(string name)
        {
            //Get the first contact in the contacts list with the specified id
            Contacts[] contactArray = contacts.Where<Contacts>(c => c.FirstName.Contains(name)).ToArray();

            //Return an array with for any contacts with the specified name
            return contactArray;
        }

        // POST: api/Contact
        [Route("")]
        [HttpPost]
        public IEnumerable<Contacts> Post([FromBody]Contacts newContact)
        {
            List<Contacts> contactList = contacts.ToList<Contacts>();
            newContact.Id = contactList.Count; //Keep the id unique
            contactList.Add(newContact);

            //Convert back to array with the updated info
            contacts = contactList.ToArray();

            return contacts;
        }

        // PUT: api/Contact/5
        [Route("{id:int}")]
        [HttpPut]
        public IEnumerable<Contacts> Put(int id, [FromBody]Contacts changedContact)
        {
            Contacts contact = contacts.FirstOrDefault<Contacts>(c => c.Id == id);
            if(contact != null)
            {
                contact.FirstName = changedContact.FirstName;
                contact.LastName = changedContact.LastName;
            }
            return contacts;
        }

        // DELETE: api/Contact/5
        [Route("{id:int}")]
        [HttpDelete]
        public IEnumerable<Contacts> Delete(int id)
        {
            //Get the first contact in the contacts list with the specified id
            Contacts contact = contacts.FirstOrDefault<Contacts>(c => c.Id == id);

            //Change contacts to a list
            List<Contacts> contactList = contacts.ToList<Contacts>();

            //Remove the contact we found
            contactList.Remove(contact);

            //Convert list back to array
            contacts = contactList.ToArray();

            //Return the contact that was deleted
            return contacts;
        }
    }
}
