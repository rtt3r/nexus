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

        public static Notification UnexpectedError(string message) => new(nameof(UNEXPECTED_ERROR), message);
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

    public readonly struct Addresses
    {
        public static readonly Notification ZIP_CODE_REQUIRED = new(nameof(ZIP_CODE_REQUIRED), "Zip code is required.");
        public static readonly Notification ZIP_CODE_INVALID_FORMAT = new(nameof(ZIP_CODE_INVALID_FORMAT), "Zip code must be a valid 8-digit number.");
        public static readonly Notification STREET_REQUIRED = new(nameof(STREET_REQUIRED), "Street is required.");
        public static readonly Notification STREET_MAX_LENGTH = new(nameof(STREET_MAX_LENGTH), "Street must not exceed 150 characters.");
        public static readonly Notification NUMBER_REQUIRED = new(nameof(NUMBER_REQUIRED), "Number is required.");
        public static readonly Notification NUMBER_MAX_LENGTH = new(nameof(NUMBER_MAX_LENGTH), "Number must not exceed 10 characters.");
        public static readonly Notification NEIGHBORHOOD_REQUIRED = new(nameof(NEIGHBORHOOD_REQUIRED), "Neighborhood is required.");
        public static readonly Notification NEIGHBORHOOD_MAX_LENGTH = new(nameof(NEIGHBORHOOD_MAX_LENGTH), "Neighborhood must not exceed 100 characters.");
        public static readonly Notification CITY_REQUIRED = new(nameof(CITY_REQUIRED), "City is required.");
        public static readonly Notification CITY_MAX_LENGTH = new(nameof(CITY_MAX_LENGTH), "City must not exceed 100 characters.");
        public static readonly Notification STATE_REQUIRED = new(nameof(STATE_REQUIRED), "State is required.");
        public static readonly Notification STATE_MAX_LENGTH = new(nameof(STATE_MAX_LENGTH), "State must be exactly 2 characters.");
        public static readonly Notification COUNTRY_REQUIRED = new(nameof(COUNTRY_REQUIRED), "Country is required.");
        public static readonly Notification COUNTRY_MAX_LENGTH = new(nameof(COUNTRY_MAX_LENGTH), "Country must not exceed 100 characters.");
        public static readonly Notification COMPLEMENT_MAX_LENGTH = new(nameof(COMPLEMENT_MAX_LENGTH), "Complement must not exceed 100 characters.");
    }

    public readonly struct Contacts
    {
        public static readonly Notification CONTACT_NAME_REQUIRED = new(nameof(CONTACT_NAME_REQUIRED), "Contact name is required.");
        public static readonly Notification CONTACT_NAME_MAX_LENGTH = new(nameof(CONTACT_NAME_MAX_LENGTH), "Contact name must not exceed 100 characters.");
        public static readonly Notification EMAIL_INVALID_FORMAT = new(nameof(EMAIL_INVALID_FORMAT), "Invalid email format.");
        public static readonly Notification LANDLINE_PHONE_INVALID_FORMAT = new(nameof(LANDLINE_PHONE_INVALID_FORMAT), "Invalid landline phone format.");
        public static readonly Notification MOBILE_PHONE_INVALID_FORMAT = new(nameof(MOBILE_PHONE_INVALID_FORMAT), "Invalid mobile phone format.");
        public static readonly Notification WHATSAPP_INVALID_FORMAT = new(nameof(WHATSAPP_INVALID_FORMAT), "Invalid WhatsApp phone format.");
    }

    public readonly struct Companies
    {
        public static readonly Notification COMPANY_NAME_REQUIRED = new(nameof(COMPANY_NAME_REQUIRED), "Company name is required.");
        public static readonly Notification COMPANY_NAME_MAX_LENGTH = new(nameof(COMPANY_NAME_MAX_LENGTH), "Company name must not exceed 100 characters.");
        public static readonly Notification COMPANY_NAME_DUPLICATED = new(nameof(COMPANY_NAME_DUPLICATED), "Company name is duplicated.");
        public static readonly Notification BRANDING_NAME_REQUIRED = new(nameof(BRANDING_NAME_REQUIRED), "Branding name is required.");
        public static readonly Notification BRANDING_NAME_MAX_LENGTH = new(nameof(BRANDING_NAME_MAX_LENGTH), "Branding name must not exceed 100 characters.");
        public static readonly Notification CNPJ_REQUIRED = new(nameof(CNPJ_REQUIRED), "CNPJ is required.");
        public static readonly Notification CNPJ_INVALID_FORMAT = new(nameof(CNPJ_INVALID_FORMAT), "CNPJ must be a valid 14-digit number.");
        public static readonly Notification ADDRESS_REQUIRED = new(nameof(ADDRESS_REQUIRED), "Address is required.");
        public static readonly Notification CONTACTS_REQUIRED = new(nameof(CONTACTS_REQUIRED), "At least one contact must be provided.");
        public static readonly Notification CONTACT_AT_LEAST_ONE_REQUIRED = new(nameof(CONTACT_AT_LEAST_ONE_REQUIRED), "A contact must have at least one phone number, WhatsApp, or email.");
    }
}
