using Microsoft.AspNetCore.Mvc;
using ReactPlusDotNet.Server.ModelsDTO;
using ReactPlusDotNet.Server.Models;
using ReactPlusDotNet.Server.Storage;
using ReactPlusDotNet.Server.Interfaces;

namespace ReactPlusDotNet.Server.Controllers
{
    public class ContactManagmentController : BaseController
    {
        private readonly IStorage _contactStorage;
        public ContactManagmentController(IStorage contactStorage)
        {
            this._contactStorage = contactStorage;
        }
        [HttpPost("contacts")]
        public IActionResult Create([FromBody] Contact contact)
        {
            Contact res = _contactStorage.AddContact(contact);
            if (res != null)
            {
                return Created();
            }
            return BadRequest("Контакт такой уже есть");
        }
        [HttpGet("contacts")]
        public IActionResult GetContacts()
        {
            return Ok(_contactStorage.GetAll());
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
