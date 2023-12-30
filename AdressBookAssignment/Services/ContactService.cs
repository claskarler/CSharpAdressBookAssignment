using AddressBookAssignment.Interfaces;
using AddressBookAssignment.Models.Responses;
using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Newtonsoft.Json;
using AddressBookAssignment.Models;


namespace AddressBookAssignment.Services;
public class ContactService : IContactService
{
    private readonly IFileService _fileService;

    private List<IContact> _contacts;

    public ContactService(IFileService fileService)                // (ctor är genväg till att skapa constructor) Constructor är en metod som kallas direkt när du skapar en instans av ContactService (new ContactService)
    {
        _fileService = fileService;
        _contacts = new List<IContact>();                          //ska ha ny tom lista
        var (list, res) = GetContactList();                        //hämtar innehåll från filen
        if (res.Status == Enums.ServiceStatus.SUCCEEDED)           //om lyckat så lägger vi listan i contacts-fältet
        {
            _contacts = list.ToList();                             // för att list var en IEnumerable
        }
        else
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"Ett fel uppstod. Felmeddelande: {res.Message}");
            Console.ForegroundColor = ConsoleColor.White;
        }
    }
   /// <summary>
   /// Metod som läggger till en ny kontakt till filen
   /// </summary>
    public ServiceResult AddContactToList(IContact contact)
    {
        var response = new ServiceResult();

        try
        {
            if (!_contacts.Any(x => x.Email == contact.Email))       // x är istället för contact
            {
                _contacts.Add(contact);
                _fileService.SaveContentToFile(JsonConvert.SerializeObject(_contacts));
                response.Status = Enums.ServiceStatus.SUCCEEDED;
            }
            else
            {
                response.Status = Enums.ServiceStatus.ALREADY_EXISTS;
            }
        }
        catch (Exception ex)
        { 
            Debug.WriteLine(ex.Message);
            response.Status = Enums.ServiceStatus.FAILED;
            response.Message = ex.Message;
        }

        return response;
    }
    /// <summary>
    /// Metod som tar bort ett viss kontakt från filen, baserat på input av en mejl som skrivs av användaren
    /// </summary>
    public ServiceResult DeleteContactFromList(IContact contact)
    {
        var response = new ServiceResult();

        try
        {
            var contentToDelete =  _contacts.FirstOrDefault(x => x.Email == contact.Email);

            if (contentToDelete != null)
            {
                _contacts.Remove(contentToDelete);
                _fileService.SaveContentToFile(JsonConvert.SerializeObject(_contacts));
                response.Status = Enums.ServiceStatus.SUCCEEDED;
            }
            else
            {
                response.Status = Enums.ServiceStatus.NOT_FOUND;
            }
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex.Message);
            response.Status = Enums.ServiceStatus.FAILED;
            response.Message = ex.Message;
        }

        return response;
    }
    /// <summary>
    /// Metod som hämtar alla kontakter som finns inuti filen
    /// </summary>
    public (IEnumerable<IContact>, ServiceResult) GetContactList()                 
    {
        var response = new ServiceResult();
        var list = new List<Contact>();

        try
        {
            var content = _fileService.GetContentFromFile();
            if (!string.IsNullOrEmpty(content))
            {
                list = JsonConvert.DeserializeObject<List<Contact>>(content)!; // Gick inte att deserializa IContact
            }
            response.Status = Enums.ServiceStatus.SUCCEEDED;
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex.Message);
            response.Status = Enums.ServiceStatus.FAILED;
            response.Message = ex.Message;
        }

        return (list, response);
    }
    /// <summary>
    /// Metod som hämtar detaljerna (alla parametrar) för en viss kontakt inuti filen, som hämtas efter användarens input av en mejl (såvida den existerar i filen alltså)
    /// </summary>
    public (IContact, ServiceResult) GetContactDetails(string email)
    {
        var response = new ServiceResult();
        IContact contactDetails = null;

        try
        {
            var contact = _contacts.FirstOrDefault(x => x.Email == email);

            if (contact != null)
            {
                contactDetails = contact;
                response.Status = Enums.ServiceStatus.SUCCEEDED;
            }
            else
            {
                response.Status = Enums.ServiceStatus.NOT_FOUND;
            }
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex.Message);
            response.Status = Enums.ServiceStatus.FAILED;
            response.Message = ex.Message;
        }

        return (contactDetails, response);
    }

}
