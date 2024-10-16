namespace Nexus.Infra.Crosscutting.Constants;

public partial class ApplicationConstants
{
    public struct Messages
    {
        // SHARED
        public const string UNEXPECTED_ERROR = "We're sorry... An unexpected problem has occurred. Please wait, our team is already working to resolve it as soon as possible.";
        public const string RESOURCE_NOT_FOUND = $"The requested resource was not found";
        public const string DOMAIN_VIOLATION = $"An business rule violation has occurred.";
        public const string REQUEST_VALIDATION = $"The send data is not valid.";
        public const string SERVICE_UNAVAILABLE = $"One or more services may be unavailable";
        public const string SAVING_DATA_FAILURE = "Opss... An error occurred while saving the data";

        // CUSTOMER
        public const string CUSTOMER_NAME_REQUIRED = "Customer name is required";
        public const string CUSTOMER_NAME_LENGTH_INVALID = "Customer name length must be between 2 and 150 characters";
        public const string CUSTOMER_BIRTHDATE_REQUIRED = "Customer birth date is required";
        public const string CUSTOMER_BIRTHDATE_LENGTH_INVALID = "Customer's age must be greater than or equal to 18 years";
        public const string CUSTOMER_EMAIL_REQUIRED = "Customer e-mail is required";
        public const string CUSTOMER_EMAIL_INVALID = "Customer e-mail address is invalid";
        public const string CUSTOMER_ID_REQUIRED = "Customer id is required";
        public const string CUSTOMER_EMAIL_DUPLICATED = "Customer e-mail has already been taken";
        public const string CUSTOMER_NOT_FOUND = "Customer was not found";

        // PERSON
        public const string PERSON_FIRST_NAME_REQUIRED = "Person first name is required";
        public const string PERSON_FIRST_NAME_MAXIMUM_LENGTH = "Customer first name length must be less then or equals to 50 characters";
        public const string PERSON_LAST_NAME_REQUIRED = "Person last name is required";
        public const string PERSON_LAST_NAME_MAXIMUM_LENGTH = "Customer last name length must be less then or equals to 50 characters";
        public const string PERSON_CPF_REQUIRED = "Person cpf is required";
        public const string PERSON_CPF_INVALID = "Person cpf is invalid";

        // USER
        public const string USER_NOT_FOUND = "User was not found";
        public const string USER_ID_REQUIRED = "User id is required";
        public const string USER_BIOGRAPHY_MAXIMUM_LENGTH = "User biography length must be less then or equals to 1024 characters";
        public const string USER_HEADLINE_MAXIMUM_LENGTH = "User headline length must be less then or equals to 128 characters";
    }
}
