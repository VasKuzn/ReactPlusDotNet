using ReactPlusDotNet.Server.Models;

namespace ReactPlusDotNet.Server.Interfaces
{
    public interface IPaginationStorage : IStorage
    {
        Contact FindContactById(int id);
        (List<Contact>, int TotalCount) GetContacts(int pageNumber, int pageSize);
    }
}
