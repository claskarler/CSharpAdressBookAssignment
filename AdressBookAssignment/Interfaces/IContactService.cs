using AddressBookAssignment.Models;
using AddressBookAssignment.Models.Responses;

namespace AddressBookAssignment.Interfaces;

public interface IContactService // CRUD = Create, Read, Update, Delete
{ 
    ServiceResult AddContactToList(IContact contact);
    ServiceResult DeleteContactFromList(IContact contact);
    (IContact, ServiceResult) GetContactDetails(string email);
    (IEnumerable<IContact>, ServiceResult) GetContactList();

}


// Lambda expression (contact) => customerFirstName == "namn"