using AddressBookAssignment.Enums;
using AddressBookAssignment.Interfaces;
using AddressBookAssignment.Models;
using AddressBookAssignment.Services;
using Moq;
using Newtonsoft.Json;
namespace AddressBookAssignment.Tests;

public class ContactService_Tests
{
    [Fact]
    public void AddContactToListShould_AddOneContactToContactList_ThenReturnSUCCEEDED()
    {
        //Arrange
        IContact contact = new Contact() { FirstName = "Namn", LastName = "Efternamn", Email = "Namn@mejl.com", PhoneNumber ="1234", Address = "Gatunamn"};
       
        var fileServiceMock = new Mock<IFileService>();                                                                                                        
        fileServiceMock.Setup(fs => fs.SaveContentToFile(It.IsAny<string>()));

        IContactService contactService = new ContactService(fileServiceMock.Object);

        //Act

        var result = contactService.AddContactToList(contact);

        //Assert
        Assert.Equal(ServiceStatus.SUCCEEDED, result.Status);
    }

    [Fact]
    public void GetContactListShould_GetAllContactsInContactList_ThenReturnListOfContacts()
    {
        //Arrange
        var contacts = new List<Contact>
    {
        new Contact { FirstName = "Namn", LastName = "Efternamn", Email = "Namn@mejl.com", PhoneNumber = "1234", Address = "Gatunamn" }
    };

        var fileServiceMock = new Mock<IFileService>();
        fileServiceMock.Setup(fs => fs.GetContentFromFile()).Returns(JsonConvert.SerializeObject(contacts));

        IContactService contactService = new ContactService(fileServiceMock.Object);

        //Act
        var result = contactService.GetContactList();

        //Assert
        Assert.NotNull(result.Item1);
        Assert.True(result.Item1.Any());
    }

    [Fact]
    public void DeleteContactFromListShould_RemoveOneContactFromContactList_ThenReturnSUCCEEDED()
    {
        //Arrange
        var contactToDelete = new Contact { Email = "Namn@mejl.com" };
        var contacts = new List<Contact>
    {
        new Contact { FirstName = "Namn", LastName = "Efternamn", Email = "Namn@mejl.com", PhoneNumber = "1234", Address = "Gatunamn" },
    };
        var fileServiceMock = new Mock<IFileService>();
        fileServiceMock.Setup(fs => fs.GetContentFromFile()).Returns(JsonConvert.SerializeObject(contacts));
        fileServiceMock.Setup(fs => fs.SaveContentToFile(It.IsAny<string>()));

        IContactService contactService = new ContactService(fileServiceMock.Object);


        //Act
        var result = contactService.DeleteContactFromList(contactToDelete);

        //Assert
        Assert.Equal(ServiceStatus.SUCCEEDED, result.Status);
    }
}
