
using ContactApplication.Data;
using ContactApplication.Entities;
using Microsoft.AspNetCore.Identity;
using System.Drawing;

namespace ContactApplication.Services
{
    public class ContactService : IContactService
    {
        private readonly ContactContext _contactContext;
        private readonly IPasswordHasher<Contact> _passwordHasher;

        public ContactService(ContactContext contactContext, IPasswordHasher<Contact> passwordHasher)
        {
            _contactContext = contactContext;
            _passwordHasher = passwordHasher;
        }

        //Metoda tworząca nowey kontakt, hasło jest hashowane i kontakt zapisywany jest do bazy danych. 
        public void Create(Contact contact)
        {
            var newContact = new Contact()
            {
                FirstName = contact.FirstName,
                LaseName = contact.LaseName,
                DateOfBirth = contact.DateOfBirth,
                Category = contact.Category,
                Subcategory = contact.Subcategory,
                Email = contact.Email,
                PhoneNumber = contact.PhoneNumber,
                CategoryId = contact.CategoryId,
            };
            var hashedPassword = _passwordHasher.HashPassword(newContact, contact.Password);

            newContact.Password = hashedPassword;
            _contactContext.Add(contact);
            _contactContext.SaveChanges();                                            
        }

        //Metoda usuwająca kontakt z bazy danych 
        public void Delete(int id)
        {
            var contact = _contactContext.Contacts.FirstOrDefault(c => c.Id == id);

            if (contact is null)
                throw new Exception("Contact not found");

            _contactContext.Contacts.Remove(contact);
            _contactContext.SaveChanges();
        }

        // Metoda przyjmująca id i pobierająca kontakt o danym id
        public Contact GetContactById(int id)
        {
            if (!_contactContext.Contacts.Any(c => c.Id == id))
                throw new Exception("Contacts not found");

            var contact = _contactContext.Contacts.Select(c => new Contact()
            {
                Id = c.Id,
                FirstName = c.FirstName,
                LaseName = c.LaseName,
                PhoneNumber = c.PhoneNumber,
                DateOfBirth = c.DateOfBirth,
                Category = c.Category,
                Subcategory = c.Subcategory,
                CategoryId = c.CategoryId,
                Email = c.Email,
                Password = c.Password
            }).FirstOrDefault(b => b.Id == id);


            return contact;
        }

        //Metoda pobierająca wszystkie kontakty bez szczegółów kontaktu
        public IEnumerable<Contact> GetAllWithoutDetails()
        {
            if (!_contactContext.Contacts.Any())
                throw new Exception("Contacts not found");

            var contacts = _contactContext.Contacts.Select(c => new Contact()
            {
                FirstName = c.FirstName,
                LaseName = c.LaseName,
                PhoneNumber = c.PhoneNumber,
            }).AsQueryable();


            return contacts;
        }

        // Metoda edytująca kontakt przyjmująca id kontaktu i nowy zedytowany kontakt. 
        public void Update(int id, Contact contact)
        {
            var item = _contactContext.Contacts.FirstOrDefault(c => c.Id == id);

            if (item is null)
                throw new Exception("Contact not found");

            item.Email = contact.Email;
            item.PhoneNumber = contact.PhoneNumber;
            item.FirstName = contact.FirstName;
            item.LaseName = contact.LaseName;
            item.DateOfBirth = contact.DateOfBirth;
            item.Password = contact.Password;
            item.CategoryId = contact.CategoryId;
            item.Category = contact.Category;
            item.Subcategory = contact.Subcategory;

            _contactContext.SaveChanges();
            
        }
    }
}
