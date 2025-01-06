using Microsoft.AspNetCore.Mvc;
using ReactPlusDotNet.Server.Interfaces;
using ReactPlusDotNet.Server.Models;
using ReactPlusDotNet.Server.ModelsDTO;

namespace ReactPlusDotNet.Server.Storage
{
    public class InMemoryStorage : IStorage
    {
        public List<Contact> Contacts { get; set; }
        public InMemoryStorage()
        {
            this.Contacts = new List<Contact>();
            for (int i = 0; i < 5; i++)
            {
                this.Contacts.Add(new Contact()
                {
                    Id = i,
                    Name = $"Чел №{i}",
                    PhoneNumber = $"123{i}",
                    Email = $"{Guid.NewGuid().ToString().Substring(0, 5)}@va.ru"
                });
            }
        }
        public Contact AddContact(Contact contact)
        {
            if (Contacts.Any(c => c.Id == contact.Id))
            {
                return null;
            }
            else
            {
                this.Contacts.Add(contact);
            }
            return contact;

        }

        public List<Contact> GetAll()
        {
            return Contacts;
        }

        public bool RemoveContact(int id)
        {
            var contact = this.Contacts.FirstOrDefault(c => c.Id == id);
            if (contact != null)
            {
                this.Contacts.Remove(contact);
                return true;
            }
            return false;

        }
        public bool UpdateContact(int id, [FromBody] ContactDTO contactDTO)
        {
            var contact = this.Contacts.FirstOrDefault(c => c.Id == id);
            if (contact != null)
            {
                contact.Email = contactDTO.Email;
                contact.PhoneNumber = contactDTO.PhoneNumber;
                contact.Name = contactDTO.Name;
                return true;
            }
            return false;

        }
        public Contact FindContactById(int id)
        {
            return this.Contacts.FirstOrDefault(c => c.Id == id);
        }
    }
}
