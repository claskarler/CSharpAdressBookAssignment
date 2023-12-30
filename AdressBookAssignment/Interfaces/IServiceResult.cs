using AddressBookAssignment.Enums;

namespace AddressBookAssignment.Interfaces;

public interface IServiceResult
{
    object Result { get; set; }
    ServiceStatus Status { get; set; }
}