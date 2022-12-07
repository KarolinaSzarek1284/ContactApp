
using ContactApplication.Entities;

namespace ContactApplication.Services
{
    public interface IContactService
    {
        public Contact GetContactById(int id);
        public IEnumerable<Contact> GetAllWithoutDetails();
        public void Create(Contact contact);
        public void Update(int id, Contact contact);
        public void Delete(int id);    
    }
}
