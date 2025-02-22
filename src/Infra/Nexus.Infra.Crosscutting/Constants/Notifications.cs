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

    public readonly struct Person
    {
        public static readonly Notification PERSON_FIRST_NAME_REQUIRED = new("PSN-0001", "Person first name is required");
        public static readonly Notification PERSON_FIRST_NAME_LENGTH = new("PSN-0002", "Person first name length must be less then or equals to 50 characters");
        public static readonly Notification PERSON_LAST_NAME_REQUIRED = new("PSN-0003", "Person last name is required");
        public static readonly Notification PERSON_LAST_NAME_LENGTH = new("PSN-0004", "Person last name length must be less then or equals to 50 characters");
        public static readonly Notification PERSON_CPF_REQUIRED = new("PSN-0005", "Person cpf is required");
        public static readonly Notification PERSON_CPF_INVALID = new("PSN-0006", "Person cpf is invalid");
        public static readonly Notification PERSON_CPF_DUPLICATED = new("PSN-0007", "Person cpf is duplicated");
        public static readonly Notification PERSON_BIRTHDATE_REQUIRED = new("PSN-0008", "Person birth date is required");
        public static readonly Notification PERSON_BIRTHDATE_LENGTH_INVALID = new("PSN-0009", "Person's age must be greater than or equal to 18 years");
        public static readonly Notification PERSON_EMAIL_REQUIRED = new("PSN-0010", "Person e-mail is required");
        public static readonly Notification PERSON_EMAIL_INVALID = new("PSN-0011", "Person e-mail address is invalid");
        public static readonly Notification PERSON_EMAIL_DUPLICATED = new("PSN-0012", "Person e-mail has already been taken");
        public static readonly Notification PERSON_GENDER_INVALID = new("PSN-0013", "Person's gender is invalid");
        public static readonly Notification PERSON_ADDRESS_LIST_REQUIRED = new("PSN-0014", "At least one address is required");
        public static readonly Notification PERSON_EMAIL_LIST_REQUIRED = new("PSN-0015", "At least one email is required");
        public static readonly Notification PERSON_PHONE_NUMBER_LIST_REQUIRED = new("PSN-0016", "At least one phone number is required");
        public static readonly Notification PERSON_PHONE_NUMBER_REQUIRED = new("PSN-0017", "Person phone number is required");
        public static readonly Notification PERSON_PHONE_NUMBER_INVALID = new("PSN-0018", "Person phone number is invalid");
        public static readonly Notification PERSON_PHONE_COUNTRY_CODE_REQUIRED = new("PSN-0019", "Person phone country code is required");
        public static readonly Notification PERSON_PHONE_COUNTRY_CODE_INVALID = new("PSN-0020", "Person phone country code is invalid");
        public static readonly Notification ID_REQUIRED = new("PSN-0021", "Person id is required");
    }
}
