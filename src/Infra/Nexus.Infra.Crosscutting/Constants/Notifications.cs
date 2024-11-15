using Nexus.Infra.Crosscutting.Notifications;

namespace Nexus.Infra.Crosscutting.Constants;

public class Notifications
{
    public readonly struct Shared
    {
        public static readonly Notification UNEXPECTED_ERROR = new("SRD-0001", "We're sorry... An unexpected problem has occurred. Please wait, our team is already working to resolve it as soon as possible.");
        public static readonly Notification RESOURCE_NOT_FOUND = new("SRD-0002", $"The requested resource was not found");
        public static readonly Notification DOMAIN_VIOLATION = new("SRD-0003", $"An business rule violation has occurred.");
        public static readonly Notification REQUEST_VALIDATION = new("SRD-0004", $"The send data is not valid.");
        public static readonly Notification SERVICE_UNAVAILABLE = new("SRD-0005", $"One or more services may be unavailable");
        public static readonly Notification SAVING_DATA_FAILURE = new("SRD-0006", "Opss... An error occurred while saving the data");

        public static Notification UnexpectedError(string message) => new("SRD-0001", message);
    }

    public readonly struct Customer
    {
        public static readonly Notification CUSTOMER_NAME_REQUIRED = new("CST-0001", "Customer name is required");
        public static readonly Notification CUSTOMER_NAME_LENGTH_INVALID = new("CST-0002", "Customer name length must be between 2 and 150 characters");
        public static readonly Notification CUSTOMER_BIRTHDATE_REQUIRED = new("CST-0003", "Customer birth date is required");
        public static readonly Notification CUSTOMER_BIRTHDATE_LENGTH_INVALID = new("CST-0004", "Customer's age must be greater than or equal to 18 years");
        public static readonly Notification CUSTOMER_EMAIL_REQUIRED = new("CST-0005", "Customer e-mail is required");
        public static readonly Notification CUSTOMER_EMAIL_INVALID = new("CST-0006", "Customer e-mail address is invalid");
        public static readonly Notification CUSTOMER_ID_REQUIRED = new("CST-0007", "Customer id is required");
        public static readonly Notification CUSTOMER_EMAIL_DUPLICATED = new("CST-0008", "Customer e-mail has already been taken");
        public static readonly Notification CUSTOMER_NOT_FOUND = new("CST-0009", "Customer was not found");
    }

    public readonly struct Person
    {
        public static readonly Notification PERSON_FIRST_NAME_REQUIRED = new("PSN-0001", "Person first name is required");
        public static readonly Notification PERSON_FIRST_NAME_MAXIMUM_LENGTH = new("PSN-0002", "Customer first name length must be less then or equals to 50 characters");
        public static readonly Notification PERSON_LAST_NAME_REQUIRED = new("PSN-0003", "Person last name is required");
        public static readonly Notification PERSON_LAST_NAME_MAXIMUM_LENGTH = new("PSN-0004", "Customer last name length must be less then or equals to 50 characters");
        public static readonly Notification PERSON_CPF_REQUIRED = new("PSN-0005", "Person cpf is required");
        public static readonly Notification PERSON_CPF_INVALID = new("PSN-0006", "Person cpf is invalid");
    }

    public readonly struct User
    {
        public static readonly Notification USER_NOT_FOUND = new("USR-0001", "User was not found");
        public static readonly Notification USER_ID_REQUIRED = new("USR-0002", "User id is required");
        public static readonly Notification USER_BIOGRAPHY_MAXIMUM_LENGTH = new("USR-0003", "User biography length must be less then or equals to 1024 characters");
        public static readonly Notification USER_HEADLINE_MAXIMUM_LENGTH = new("USR-0004", "User headline length must be less then or equals to 128 characters");
    }
}