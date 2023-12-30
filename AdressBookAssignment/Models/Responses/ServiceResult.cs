using AddressBookAssignment.Enums;

namespace AddressBookAssignment.Models.Responses;

public interface IServiceResult
{
    ServiceStatus Status { get; set; }
    string Message { get; set; }
}

public class ServiceResult : IServiceResult
{
    public ServiceStatus Status { get; set; }
    public string Message { get; set; } = null!;
}
