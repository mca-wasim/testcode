using Evolent.Data;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;
using Evolent.Service.Contacts;
using Evolent.Repository;
using System.Linq;

namespace Evolent.UnitTests.Service
{
    [TestClass]
    public class ContactServiceTest
    {
        [TestMethod]
        public void Save_Contact_Check_Return_Value()
        {
            //Arrange
            var unitOfWorkMock = new Mock<IUnitOfWork>();
            unitOfWorkMock.Setup(x => x.Contacts.Add(new Contact()));

            unitOfWorkMock.Setup(x => x.Complete()).Returns(0);
            var service = new ContactService(unitOfWorkMock.Object);
            Contact contact = new Contact() { FirstName = "name", LastName = "wasim", Email = "wasim@gmail.com", Status = "Active" };

            // Act
            var result = service.AddContact(contact);

            // Assert
            Assert.AreEqual(result, 0);
        }

        [TestMethod]
        public void Update_Contact_Check_Return_Value()
        {
            //Arrange
            var unitOfWorkMock = new Mock<IUnitOfWork>();
            unitOfWorkMock.Setup(x => x.Contacts.Update(new Contact()));
            Contact contact = new Contact() { ContactId = 2, FirstName = "name", LastName = "wasim", Email = "wasim@gmail.com", Status = "Active" };

            unitOfWorkMock.Setup(x => x.Contacts.Get(contact.ContactId)).Returns(contact);
            
            unitOfWorkMock.Setup(x => x.Complete()).Returns(1);

            var service = new ContactService(unitOfWorkMock.Object);
            

            // Act
            var result = service.UpdateContact(contact);

            // Assert
            Assert.AreEqual(result, contact.ContactId);
        }

        [TestMethod]
        public void Delete_Contact_Check_Return_Value()
        {
            //Arrange
            var unitOfWorkMock = new Mock<IUnitOfWork>();
            unitOfWorkMock.Setup(x => x.Contacts.Remove(new Contact()));
            Contact contact = new Contact() { ContactId = 1, FirstName = "name", LastName = "wasim", Email = "wasim@gmail.com", Status = "Active" };

            unitOfWorkMock.Setup(x => x.Contacts.Get(contact.ContactId)).Returns(contact);


            unitOfWorkMock.Setup(x => x.Complete()).Returns(1);

            var service = new ContactService(unitOfWorkMock.Object);


            // Act
            var result = service.DeleteContact(contact.ContactId);

            // Assert
            Assert.AreEqual(result, contact.ContactId);
        }

        [TestMethod]
        public void Get_Contact_Check_Return_Value()
        {
            //Arrange

            var contacts = new List<Contact>{
                new Contact  {ContactId = 1, FirstName ="Alejandra Camino", LastName ="Gran Vía, 1", PhoneNumber ="(953) 10956", Status="Active", Email="someemail@hotmail.com"},
                new Contact  {ContactId = 2, FirstName ="Alejandra Camino", LastName ="Gran Vía, 1", PhoneNumber ="(953) 10956", Status="Active", Email="someemail@hotmail.com"},
                new Contact  {ContactId = 3, FirstName ="Alejandra Camino", LastName ="Gran Vía, 1", PhoneNumber ="(953) 10956", Status="Active", Email="someemail@hotmail.com"}

            };

            var unitOfWorkMock = new Mock<IUnitOfWork>();
            unitOfWorkMock.Setup(x => x.Contacts.GetAll()).Returns(contacts);
            
            var service = new ContactService(unitOfWorkMock.Object);
            
            // Act
            var result = service.GetAllContacts();

            // Assert
            Assert.AreEqual(result.ToList().Count, contacts.Count);
            Assert.AreEqual(result, contacts);
        }
    }
}
