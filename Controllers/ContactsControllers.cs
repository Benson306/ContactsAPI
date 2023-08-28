using ContactsAPI.Data;
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
    }
}
