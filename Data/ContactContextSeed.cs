
using ContactApplication.Entities;
using System.Data;

namespace ContactApplication.Data
{
    public class ContactContextSeed
    {
        private readonly ContactContext _contactContext;

        public ContactContextSeed(ContactContext contactContext)
        {
            _contactContext = contactContext;
        }

        public void Seed()
        {
            if (_contactContext.Database.CanConnect())
            {
                if (!_contactContext.Categories.Any())
                {
                    var categories = GetCategories();
                    _contactContext.Categories.AddRange(categories);
                    _contactContext.SaveChanges();
                }
                if (!_contactContext.Contacts.Any())
                {
                    var contacts = GetContacts();
                    _contactContext.Contacts.AddRange(contacts);
                    _contactContext.SaveChanges();
                }
            }
        }

        private IEnumerable<Category> GetCategories()
        {
            var categories = new List<Category>()
            {
                new Category()
                {
                    Name = "Służbowy"
                },
                new Category()
                {
                    Name = "Prywatny"
                },
                new Category()
                {
                    Name = "Inny"
                },
            };
            return categories;
        }

        private IEnumerable<Contact> GetContacts()
        {
            var contacts = new List<Contact>()
            {
                new Contact()
                {
                    Email = "test123@wp.pl",
                    FirstName = "Adam",
                    LaseName = "Nowak",
                    Password = "Password12@",
                    CategoryId = 1,
                    DateOfBirth = new DateTime(1990,6,6),
                    PhoneNumber = "123123123"
                },
                new Contact()
                {
                    Email = "test1234@wp.pl",
                    FirstName = "Adam",
                    LaseName = "Nycz",
                    Password = "Password123@",
                    CategoryId = 1,
                    DateOfBirth = new DateTime(1991,6,6),
                    PhoneNumber = "123123147"
                },
            };
            return contacts;
        }
    }
}
