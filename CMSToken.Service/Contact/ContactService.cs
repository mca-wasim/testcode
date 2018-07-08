using System.Linq;
using Evolent.Repository;
using Evolent.Model;
using System;
using System.Configuration;
using System.Collections.Generic;
using Evolent.Data;
using System.ComponentModel.DataAnnotations;
using System.Data;

namespace Evolent.Service.Contacts
{
    public class ContactService : IContactService
    {
        private readonly IUnitOfWork unitOfWork;

        public ContactService(IUnitOfWork uOW)
        {
            this.unitOfWork = uOW;
        }

        #region Public Methods


        public Contact GetContactById(int id)
        {
            return unitOfWork.Contacts.Get(id);
        }

        public IEnumerable<Contact> GetAllContacts()
        {
            return unitOfWork.Contacts.GetAll();
        }

        public int AddContact(Contact contact)
        {
            if (contact != null)
            {
                unitOfWork.Contacts.Add(contact);
                unitOfWork.Complete();

                return contact.ContactId;
            }
            else
                return 0;
        }

        public int UpdateContact(Contact contact)
        {
            var contactData = unitOfWork.Contacts.Get(contact.ContactId);

            if (contactData != null) {
                contactData.Email= contact.Email;
                contactData.FirstName = contact.FirstName;
                contactData.LastName = contact.LastName;
                contactData.PhoneNumber = contact.PhoneNumber;
                contactData.Status = contact.Status;

                unitOfWork.Contacts.Update(contactData);
                unitOfWork.Complete();
                return contactData.ContactId;
            }
            else
            {
                return 0;
            }
        }

        public int DeleteContact(int id)
        {
            var contactData = unitOfWork.Contacts.Get(id);

            if (contactData != null)
            {
                unitOfWork.Contacts.Remove(contactData);
                unitOfWork.Complete();
                return contactData.ContactId;
            }
            else
            {
                return 0;
            }
        }
        
        #endregion
    }
}
