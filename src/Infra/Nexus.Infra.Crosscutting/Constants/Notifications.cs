using Nexus.Infra.Crosscutting.Notifications;

namespace Nexus.Infra.Crosscutting.Constants;

public class Notifications
{
    public readonly struct Shared
    {
        public static readonly Notification UNEXPECTED_ERROR = new(nameof(UNEXPECTED_ERROR), "We're sorry... An unexpected problem has occurred. Please wait, our team is already working to resolve it as soon as possible.");
        public static readonly Notification RESOURCE_NOT_FOUND = new(nameof(RESOURCE_NOT_FOUND), $"The requested resource was not found");
        public static readonly Notification DOMAIN_VIOLATION = new(nameof(DOMAIN_VIOLATION), $"An business rule violation has occurred.");
        public static readonly Notification REQUEST_VALIDATION = new(nameof(REQUEST_VALIDATION), $"The send data is not valid.");
        public static readonly Notification SERVICE_UNAVAILABLE = new(nameof(SERVICE_UNAVAILABLE), $"One or more services may be unavailable");
        public static readonly Notification SAVING_DATA_FAILURE = new(nameof(SAVING_DATA_FAILURE), "Opss... An error occurred while saving the data");

        public static Notification UnexpectedError(string message) => new("SRD-0001", message);
    }

    public readonly struct Person
    {
        public static readonly Notification PERSON_FIRST_NAME_REQUIRED = new(nameof(PERSON_FIRST_NAME_REQUIRED), "Person first name is required");
        public static readonly Notification PERSON_FIRST_NAME_LENGTH = new(nameof(PERSON_FIRST_NAME_LENGTH), "Person first name length must be less then or equals to 50 characters");
        public static readonly Notification PERSON_LAST_NAME_REQUIRED = new(nameof(PERSON_LAST_NAME_REQUIRED), "Person last name is required");
        public static readonly Notification PERSON_LAST_NAME_LENGTH = new(nameof(PERSON_LAST_NAME_LENGTH), "Person last name length must be less then or equals to 50 characters");
        public static readonly Notification PERSON_CPF_REQUIRED = new(nameof(PERSON_CPF_REQUIRED), "Person cpf is required");
        public static readonly Notification PERSON_CPF_INVALID = new(nameof(PERSON_CPF_INVALID), "Person cpf is invalid");
        public static readonly Notification PERSON_CPF_DUPLICATED = new(nameof(PERSON_CPF_DUPLICATED), "Person cpf is duplicated");
        public static readonly Notification PERSON_BIRTHDATE_REQUIRED = new(nameof(PERSON_BIRTHDATE_REQUIRED), "Person birth date is required");
        public static readonly Notification PERSON_BIRTHDATE_LENGTH_INVALID = new(nameof(PERSON_BIRTHDATE_LENGTH_INVALID), "Person's age must be greater than or equal to 18 years");
        public static readonly Notification PERSON_EMAIL_REQUIRED = new(nameof(PERSON_EMAIL_REQUIRED), "Person e-mail is required");
        public static readonly Notification PERSON_EMAIL_INVALID = new(nameof(PERSON_EMAIL_INVALID), "Person e-mail address is invalid");
        public static readonly Notification PERSON_EMAIL_DUPLICATED = new(nameof(PERSON_EMAIL_DUPLICATED), "Person e-mail has already been taken");
        public static readonly Notification PERSON_GENDER_INVALID = new(nameof(PERSON_GENDER_INVALID), "Person's gender is invalid");
        public static readonly Notification PERSON_ADDRESS_LIST_REQUIRED = new(nameof(PERSON_ADDRESS_LIST_REQUIRED), "At least one address is required");
        public static readonly Notification PERSON_EMAIL_LIST_REQUIRED = new(nameof(PERSON_EMAIL_LIST_REQUIRED), "At least one email is required");
        public static readonly Notification PERSON_PHONE_NUMBER_LIST_REQUIRED = new(nameof(PERSON_PHONE_NUMBER_LIST_REQUIRED), "At least one phone number is required");
        public static readonly Notification PERSON_PHONE_NUMBER_REQUIRED = new(nameof(PERSON_PHONE_NUMBER_REQUIRED), "Person phone number is required");
        public static readonly Notification PERSON_PHONE_NUMBER_INVALID = new(nameof(PERSON_PHONE_NUMBER_INVALID), "Person phone number is invalid");
        public static readonly Notification PERSON_PHONE_COUNTRY_CODE_REQUIRED = new(nameof(PERSON_PHONE_COUNTRY_CODE_REQUIRED), "Person phone country code is required");
        public static readonly Notification PERSON_PHONE_COUNTRY_CODE_INVALID = new(nameof(PERSON_PHONE_COUNTRY_CODE_INVALID), "Person phone country code is invalid");
        public static readonly Notification PERSON_ID_REQUIRED = new(nameof(PERSON_ID_REQUIRED), "ID is required");
    }

    public readonly struct BusinessGroup
    {
        public static readonly Notification BUSINESS_GROUP_NAME_REQUIRED = new(nameof(BUSINESS_GROUP_NAME_REQUIRED), "Name is required.");
        public static readonly Notification BUSINESS_GROUP_NAME_LENGTH = new(nameof(BUSINESS_GROUP_NAME_LENGTH), "Name length must be less then or equals to 128 characters.");
        public static readonly Notification BUSINESS_GROUP_NAME_DUPLICATED = new(nameof(BUSINESS_GROUP_NAME_DUPLICATED), "Already exists another Business Group with the same name.");
        public static readonly Notification BUSINESS_GROUP_DESCRIPTION_LENGTH = new(nameof(BUSINESS_GROUP_DESCRIPTION_LENGTH), "Name length must be less then or equals to 256 characters.");
        public static readonly Notification BUSINESS_GROUP_TAX_ID_LENGTH = new(nameof(BUSINESS_GROUP_TAX_ID_LENGTH), "Tax ID length must be less then or equals to 32 characters.");
        public static readonly Notification BUSINESS_GROUP_ID_REQUIRED = new(nameof(BUSINESS_GROUP_ID_REQUIRED), "ID is required");
        public static readonly Notification BUSINESS_GROUP_NOT_FOUND = new(nameof(BUSINESS_GROUP_NOT_FOUND), "Business Group not found.");
    }
}
