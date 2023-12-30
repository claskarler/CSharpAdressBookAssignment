namespace AddressBookAssignment.Enums;
/// <summary>
/// De olika statusarna som samtliga services kan få :)
/// </summary>
public enum ServiceStatus
{
    FAILED = 0,
    SUCCEEDED = 1,
    ALREADY_EXISTS = 2,
    NOT_FOUND = 3,
    UPDATED = 4,
    DELETED = 5
}
