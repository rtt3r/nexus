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

    public readonly struct Accounts
    {
        public static readonly Notification NAME_REQUIRED = new("CST-0001", "Account name is required");
        public static readonly Notification NAME_LENGTH_INVALID = new("CST-0002", "Account name length must be between 3 and 64 characters");
        public static readonly Notification TYPE_REQUIRED = new("CST-0003", "Account type is required");
        public static readonly Notification TYPE_LENGTH_INVALID = new("CST-0004", "Account type must be one of the following options (Wallet, CheckingAccount, Investiment, Other)");
        public static readonly Notification FINANCIAL_INSTITUTION_ID_REQUIRED = new("CST-0005", "Financial institution id is required");
        public static readonly Notification ICON_REQUIRED = new("CST-0006", "Icon is required");
        public static readonly Notification ICON_LENGTH_INVALID = new("CST-0007", "Icon length must be between 1 and 16 characters");
        public static readonly Notification ID_REQUIRED = new("CST-0008", "Account id is required");
        public static readonly Notification NAME_DUPLICATED = new("CST-0009", "Account name has already been taken");
        public static readonly Notification NOT_FOUND = new("CST-0010", "Account was not found");
        public static readonly Notification FINANCIAL_INSTITUTION_NOT_FOUND = new("CST-0011", "Financial institution was not found");
        public static readonly Notification INITIAL_BALANCE_REQUIRED = new("CST-0012", "Initial balance is required");
        public static readonly Notification OVERDRAFT_REQUIRED = new("CST-0013", "Overdraft is required");
        public static readonly Notification DESCRIPTION_LENGTH_INVALID = new("CST-0014", "Account description length must be less than 256 characters");
    }

    public readonly struct Person
    {
        public static readonly Notification PERSON_FIRST_NAME_REQUIRED = new("PSN-0001", "Person first name is required");
        public static readonly Notification PERSON_FIRST_NAME_MAXIMUM_LENGTH = new("PSN-0002", "Account first name length must be less then or equals to 50 characters");
        public static readonly Notification PERSON_LAST_NAME_REQUIRED = new("PSN-0003", "Person last name is required");
        public static readonly Notification PERSON_LAST_NAME_MAXIMUM_LENGTH = new("PSN-0004", "Account last name length must be less then or equals to 50 characters");
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