﻿using ContactsAPI.Data;
using ContactsAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ContactsAPI.Controllers
{
    [ApiController]
    //[Route("/api/[controller]")]
    [Route("/api/contacts")]
    public class ContactsControllers : Controller
    {
        private readonly ContactsAPIDbContext contactsAPIDbContext;

        public ContactsControllers(ContactsAPIDbContext contactsAPIDbContext)
        {
            this.contactsAPIDbContext = contactsAPIDbContext;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllContacts()
        {
            return Ok(await contactsAPIDbContext.Contacts.ToListAsync());
        }

        [HttpGet]
        [Route("{id:guid}")]
        public async Task<IActionResult> GetSingleContact([FromRoute] Guid id)
        {
            var contact = await contactsAPIDbContext.Contacts.FindAsync(id);

            if(contact == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(contact);
            }
        }

        [HttpPost]
        public async Task<IActionResult> AddContact(AddContactRequest addContactRequest)
        {
            var contact = new Contact()
            {
                Id = Guid.NewGuid(),
                Address = addContactRequest.Address,
                Email = addContactRequest.Email,
                FullName = addContactRequest.FullName,
                Phone = addContactRequest.Phone,
            };

            await contactsAPIDbContext.Contacts.AddAsync(contact);

            await contactsAPIDbContext.SaveChangesAsync();

            return Ok(contact);
        }

        [HttpPut]
        [Route("{id:guid}")]
        public async Task<IActionResult> updateContact([FromRoute] Guid id, UpdateContactRequest updateContactRequest)
        {
            var contact = await contactsAPIDbContext.Contacts.FindAsync(id);

            if(contact != null)
            {
                contact.FullName = updateContactRequest.FullName;
                contact.Phone = updateContactRequest.Phone;
                contact.Email = updateContactRequest.Email;
                contact.Address = updateContactRequest.Address;

                await contactsAPIDbContext.SaveChangesAsync();

                return Ok(contact);
            }

            return NotFound();
        }

        [HttpDelete]
        [Route("{id:guid}")]
        public async Task<IActionResult> deleteContact([FromRoute] Guid id)
        {
            var contact = await contactsAPIDbContext.Contacts.FindAsync(id);

            if(contact != null)
            {
                contactsAPIDbContext.Remove(contact);

                await contactsAPIDbContext.SaveChangesAsync();

                return Ok();
            }
            return NotFound();
        }
    }
}
