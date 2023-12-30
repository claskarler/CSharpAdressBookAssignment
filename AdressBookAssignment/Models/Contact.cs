using AddressBookAssignment.Interfaces;

namespace AddressBookAssignment.Models
{
    /// <summary>
    /// Detta är en kontakt
    /// </summary>
    public class Contact : IContact
    {
        /// <summary>
        /// Kontaktens Id
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Kontaktens förnamn
        /// </summary>
        public string FirstName { get; set; } = null!;
        /// <summary>
        /// Kontaktens efternamn
        /// </summary>
        public string LastName { get; set; } = null!;
        /// <summary>
        /// Kontaktens mejladdress
        /// </summary>
        public string Email { get; set; } = null!;
        /// <summary>
        /// Kontaktens nummer
        /// </summary>
        public string PhoneNumber { get; set; } = null!;
        /// <summary>
        /// Kontaktens address
        /// </summary>
        public string Address { get; set; } = null!;
    }
}

// FirstName, LastName, Email, PhoneNumber och Address är properties (den är public + (get; set;)) om de hade varit private & inte (get; set;) hade de varit ett fält istället :)
