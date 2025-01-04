using Microsoft.AspNetCore.Mvc;
using ReactPlusDotNet.Server.ModelsDTO;
using ReactPlusDotNet.Server.Models;
using ReactPlusDotNet.Server.Storage;

namespace ReactPlusDotNet.Server.Controllers
{
    public class ContactManagmentController : BaseController
    {
        private readonly ContactStorage _contactStorage;
        public ContactManagmentController(ContactStorage contactStorage)
        {
            this._contactStorage = contactStorage;
        }
        [HttpPost("contacts")]
        public IActionResult Create([FromBody] Contact contact)
        {
            if (_contactStorage.AddContact(contact))
            {
                return Created();
            }
            return BadRequest("Контакт такой уже есть");
        }
        [HttpGet("contacts")]
        public IActionResult GetContacts()
        {
            return Ok(_contactStorage.Contacts);
        }
        [HttpDelete("contacts/{id}")]
        public IActionResult Delete(int id)
        {
            if (_contactStorage.RemoveContact(id))
            {
                return Ok(id);
            }
            return BadRequest("Контакт удалить не удалось");

        }
        [HttpPut("contacts/{id}")]
        public IActionResult Update(int id, [FromBody] ContactDTO contactDTO)
        {
            if (_contactStorage.UpdateContact(id, contactDTO))
            {
                return Ok(id);
            }
            return NotFound(id);

        }
        [HttpGet("contact/{id}")]
        public IActionResult GetContact(int id)
        {
            if (id <= 0)
            {
                return BadRequest("Неверный формат ID.");
            }

            var contact = _contactStorage.FindContactById(id);
            if (contact == null)
            {
                return NotFound($"Контакт с ID {id} не найден.");
            }

            return Ok(contact);
        }
    }
}
