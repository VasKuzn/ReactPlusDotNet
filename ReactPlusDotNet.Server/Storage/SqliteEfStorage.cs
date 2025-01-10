using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ReactPlusDotNet.Server.DataContext;
using ReactPlusDotNet.Server.Interfaces;
using ReactPlusDotNet.Server.Models;
using ReactPlusDotNet.Server.ModelsDTO;

namespace ReactPlusDotNet.Server.Storage
{
    public class SqliteEfStorage : IPaginationStorage
    {
        private readonly SqliteDbContext context;

        public SqliteEfStorage(SqliteDbContext context)
        {
            this.context = context;
        }

        public Contact AddContact(Contact contact)
        {
            context.Add(contact);
            context.SaveChanges();
            return contact;
        }

        public Contact FindContactById(int id)
        {
            return context.Contacts.ToList().Find(x => x.Id == id);
        }

        public List<Contact> GetAll()
        {
            return context.Contacts.ToList();
        }

        public (List<Contact>, int TotalCount) GetContacts(int pageNumber, int pageSize)
        {
            int total = context.Contacts.Count();

            List<Contact> contacts = context.Contacts.Skip((pageNumber-1)*pageSize).Take(pageSize).ToList();

            return (contacts, total);
        }

        public bool RemoveContact(int id)
        {
            var contact = context.Contacts.Find(id);
            if (contact == null) 
            {
                return false;
            }
            context.Contacts.Remove(contact);
            context.SaveChanges();
            return true;
        }

        public bool UpdateContact(int id, [FromBody] ContactDTO contactDTO)
        {
            var contact = context.Contacts.Find(id);
            if (contact == null)
            {
                return false;
            }
            contact.Name = contactDTO.Name;
            contact.PhoneNumber = contactDTO.PhoneNumber;
            contact.Email = contactDTO.Email;
            context.SaveChanges();
            return true;
        }
    }
}
