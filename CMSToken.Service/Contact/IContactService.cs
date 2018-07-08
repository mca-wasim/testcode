using Evolent.Data;

using System.Collections.Generic;

namespace Evolent.Service.Contacts
{
    public interface IContactService
    {
        Contact GetContactById(int id);
        IEnumerable<Contact> GetAllContacts();
        int AddContact(Contact contact);
        int UpdateContact(Contact contact);
        int DeleteContact(int id);
    }
}