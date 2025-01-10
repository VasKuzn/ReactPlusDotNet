using Microsoft.AspNetCore.Mvc;
using ReactPlusDotNet.Server.Models;
using ReactPlusDotNet.Server.ModelsDTO;

namespace ReactPlusDotNet.Server.Interfaces
{
    public interface IStorage
    {
        Contact AddContact(Contact contact);
        bool RemoveContact(int id);

        bool UpdateContact(int id, [FromBody] ContactDTO contactDTO);

        List<Contact> GetAll();
    }
}
