using AddressBookAssignment.Interfaces;
using AddressBookAssignment.Models;
using System.Collections.Generic;

namespace AddressBookAssignment.Services;

public interface IMenuService
{
    /// <summary>
    /// Detta är metoden för att visa menyn i konsollen
    /// </summary>
    void ShowMainMenu();

}
public class MenuService : IMenuService
{
    private readonly IContactService _contactService;

    public MenuService(IContactService contactService)
    {
        _contactService = contactService;
    }
    /// <summary>
    /// Meny-alternativet för att lägga till en ny kontakt i listan, där applikationen steg för steg ber användaren fylla i informationen för kontakten
    /// </summary>
    private void ShowAddContactOption()
    {
        IContact contact = new Contact();

        DisplayMenuTitle("ADD NEW CONTACT");

        Console.Write("First name: ");
        contact.FirstName = Console.ReadLine()!;

        Console.Write("Last name: ");
        contact.LastName = Console.ReadLine()!;

        Console.Write("Email address: ");
        contact.Email = Console.ReadLine()!;

        Console.Write("Phone number: ");
        contact.PhoneNumber = Console.ReadLine()!;

        Console.Write("Home address: ");
        contact.Address = Console.ReadLine()!;

        var result = _contactService.AddContactToList(contact);

        switch (result.Status)
        {
            case Enums.ServiceStatus.SUCCEEDED:
                Console.WriteLine("The contact was successfully created.");
                break;
            case Enums.ServiceStatus.ALREADY_EXISTS:
                Console.WriteLine("This contact already exists.");
                break;
            case Enums.ServiceStatus.FAILED:
                Console.WriteLine("Could not add the contact to the list.");
                Console.WriteLine("See error message ::" + result.Message.ToString());
                break;
        }

        DisplayPressAnyKey();
    }
    /// <summary>
    /// Meny-alternativet för att visa all information om en viss kontakt
    /// </summary>
    private void ShowContactDetailOption()
    {
        DisplayMenuTitle("CONTACT DETAILS");
        var (list, res) = _contactService.GetContactList();

        if (res.Status == Enums.ServiceStatus.SUCCEEDED && list.Any())
        {
            Console.WriteLine("Available Contacts:");

            foreach (var contact in list)
            {
                Console.WriteLine($"- {contact.Email}");
            }
            Console.Write("\nEnter the email of the contact you want to view the details for: ");
            var contactToView = Console.ReadLine();

            if (!string.IsNullOrEmpty(contactToView))
            {
                var (contactDetails, result) = _contactService.GetContactDetails(contactToView);

                switch (result.Status)
                {
                    case Enums.ServiceStatus.SUCCEEDED:
                        Console.WriteLine($"Contact Details:");
                        Console.WriteLine($"First Name: {contactDetails.FirstName}");
                        Console.WriteLine($"Last Name: {contactDetails.LastName}");
                        Console.WriteLine($"Email: {contactDetails.Email}");
                        Console.WriteLine($"Phone Number: {contactDetails.PhoneNumber}");
                        Console.WriteLine($"Address: {contactDetails.Address}");
                        break;
                    case Enums.ServiceStatus.NOT_FOUND:
                        Console.WriteLine("Contact not found.");
                        break;
                    case Enums.ServiceStatus.FAILED:
                        Console.WriteLine("Could not retrieve contact details.");
                        Console.WriteLine("See error message: " + result.Message.ToString());
                        break;
                }
            }
            else
            {
                ErrorMessage("Invalid Input. Please try again");
            }
        }
        else
        {
            Console.WriteLine("No contacts found.");
        }

        DisplayPressAnyKey();
    }
    /// <summary>
    /// Meny-alternativet för att visa alla kontakter som finns i listan
    /// </summary>
    private void ShowContactListOption()
    {
        DisplayMenuTitle("CONTACT LIST");
        var (list, res) = _contactService.GetContactList();
        if (res.Status == Enums.ServiceStatus.SUCCEEDED)
        {
                if (!list.Any())
                {
                    Console.WriteLine("No contacts found.");
                }
                else 
                {
                    foreach (var contact in list)
                    {
                        Console.WriteLine($"{contact.FirstName} {contact.LastName} <{contact.Email}>");
                    }
                }
        }
        else
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"Ett fel uppstod. Felmeddelande: {res.Message}");
            Console.ForegroundColor = ConsoleColor.White;
        }
        DisplayPressAnyKey();
        

    }
    /// <summary>
    /// Meny-alternativet för att ta bort en kontakt i filen/listan där användaren ombeds fylla i mejladdressen på kontakten som ska tas bort
    /// </summary>
    private void ShowDeleteContactOption()
    {
        DisplayMenuTitle("DELETE CONTACT");
        Console.Write("Enter the email you want to delete: ");
        var contentToDelete = Console.ReadLine();

        if (!string.IsNullOrEmpty(contentToDelete))
        {
            var contactToDelete = new Contact { Email = contentToDelete };
            var result = _contactService.DeleteContactFromList(contactToDelete);

            switch (result.Status)
            {
                case Enums.ServiceStatus.SUCCEEDED:
                    Console.WriteLine("The contact was successfully removed.");
                    break;
                case Enums.ServiceStatus.NOT_FOUND:
                    Console.WriteLine("Contact not found. No changes were made.");
                    break;
                case Enums.ServiceStatus.FAILED:
                    Console.WriteLine("Could not remove the contact from the file.");
                    Console.WriteLine("See error message: " + result.Message.ToString());
                    break;
            }
        }
        else
        {
            ErrorMessage("Invalid Input. Please try again");
        }

        DisplayPressAnyKey();
    }

    /// <summary>
    /// Självaste huvudmenyn i applikationen, alternativen listas upp och användaren får därefter skriva numret på det alternativet som hen vill välja
    /// </summary>
    public void ShowMainMenu()
    {
        while(true) 
        {
            DisplayMenuTitle("MENU OPTIONS");
            Console.WriteLine($"{"1.", -3} Add New Contact");
            Console.WriteLine($"{"2.",-3} Edit Contact");
            Console.WriteLine($"{"3.",-3} Delete Contact");
            Console.WriteLine($"{"4.",-3} View Contact List");
            Console.WriteLine($"{"5.",-3} View Contact Details");
            Console.WriteLine($"{"0.",-3} Exit Application");
            Console.WriteLine(); 
            Console.Write("Enter Menu Option: ");
            var option = Console.ReadLine();

            switch(option)
            {
                case "1":
                    ShowAddContactOption(); break;
                case "2": 
                    ShowUpdateContactOption(); break;
                case "3":
                    ShowDeleteContactOption(); break;
                case "4":
                    ShowContactListOption(); break;
                case "5":
                    ShowContactDetailOption(); break;
                case "0":
                    ShowExitApplicationOption(); break;
                default:
                    Console.WriteLine("\nInvalid option. Press any key to try again");
                    Console.ReadKey();
                    break;
            }

        }
    }
    /// <summary>
    /// Meny-alternativet för att uppdatera en existerande kontakt
    /// </summary>
    private void ShowUpdateContactOption()
    {
        DisplayMenuTitle("EDIT CONTACT");
        Console.WriteLine("This option is currently not available");
        DisplayPressAnyKey();
    }

    /// <summary>
    /// Metoden för designen på titeln som kallas in i varje meny-alternativ, istället för att skriva i varje separat.
    /// </summary>
    private void DisplayMenuTitle(string title) 
    {
        Console.Clear();
        Console.WriteLine($"## {title} ##");
        Console.WriteLine();
    }

    /// <summary>
    /// Meny-alternativet för att lämna applikationen, här blir
    /// </summary>
    private void ShowExitApplicationOption()
    {
        Console.Clear();
        Console.Write("Are you sure you want to close the application? (y/n): ");
        var option = Console.ReadLine() ?? "";

        if (option.Equals("y", StringComparison.CurrentCultureIgnoreCase)) 
            Environment.Exit(0);
    }

    private void DisplayPressAnyKey()
    {
        Console.WriteLine("\nPress any key to continue.");
        Console.ReadKey();
    }
    /// <summary>
    /// En metod för att skriva ut error-meddelanden som gör att texten blir röd
    /// </summary>
    private static void ErrorMessage(string message)
    {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine($"{message}");
        Console.ForegroundColor = ConsoleColor.White;
    }
}
