using ContactsAPI.Data;
using Microsoft.AspNetCore.Mvc;

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
        public IActionResult GetAllContacts()
        {
            return Ok(contactsAPIDbContext.Contacts.ToList());
        }
    }
}
