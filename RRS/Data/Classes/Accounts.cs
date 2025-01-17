public class Accounts : IDBClass {
    public int ID {get;}
    public string Email;
    private string Password;
    public string FirstName;
    public string LastName;
    public string PhoneNumber;
    public string Language;
    public int AccountLevel {get; private set;}

    public Accounts(string email, string password, string firstname, string lastname, string phonenumber, string language, int accountlevel) {
        ID = 999999999;
        Email = email;
        Password = password;
        FirstName = firstname;
        LastName = lastname;
        PhoneNumber = phonenumber;
        Language = language;
        AccountLevel = accountlevel;
    }

    public Accounts(int id, string email, string password, string firstname, string lastname, string phonenumber, string language, int accountlevel) {
        ID = id;
        Email = email;
        Password = password;
        FirstName = firstname;
        LastName = lastname;
        PhoneNumber = phonenumber;
        Language = language;
        AccountLevel = accountlevel;
    }

    public Accounts(int id, string email, string firstname, string lastname, string phonenumber, string language, int accountlevel) {
        ID = id;
        Email = email;
        Password = "";
        FirstName = firstname;
        LastName = lastname;
        PhoneNumber = phonenumber;
        Language = language;
        AccountLevel = accountlevel;
    }

    public Accounts(string email, string firstname, string lastname, string phonenumber, string language, int accountlevel) {
        ID = 999999999;
        Email = email;
        Password = "";
        FirstName = firstname;
        LastName = lastname;
        PhoneNumber = phonenumber;
        Language = language;
        AccountLevel = accountlevel;
    }

    public string GetPassword() => Password;

    public override string ToString() => $"ID: {ID}\nEmail: {Email}\nFirstName: {FirstName}\nLastName: {LastName}\nPhoneNumber: {PhoneNumber}\nLanguage: {Language}\nAccountLevel: {AccountLevel}";

    public string ToString_WithAccountLevelName() => $"ID: {ID}\nEmail: {Email}\nFirstName: {FirstName}\nLastName: {LastName}\nPhoneNumber: {PhoneNumber}\nLanguage: {Language}\nAccountLevel: {Database.SelectAccountLevel(AccountLevel).Name}";

    public string FancyToString_WithAccountLevelName() => $"===============================================================\nID           : {ID}\nEmail        : {Email}\nFirstName    : {FirstName}\nLastName     : {LastName}\nPhoneNumber  : {PhoneNumber}\nAccountLevel : {Database.SelectAccountLevel(AccountLevel).Name}\n===============================================================";

    public string FancyToString_WithAccountLevelName_NoBorder() => $"ID           : {ID}\nEmail        : {Email}\nFirstName    : {FirstName}\nLastName     : {LastName}\nPhoneNumber  : {PhoneNumber}\nAccountLevel : {Database.SelectAccountLevel(AccountLevel).Name}";

    public string ToStringDisplay() => $"ID           : {ID}\nEmail        : {Email}\nFirstName    : {FirstName}\nLastName     : {LastName}\nPhoneNumber  : {PhoneNumber}\nAccountLevel : {Database.SelectAccountLevel(AccountLevel).Name}";
}