public class Accounts {
    public int ID {get; private set;}
    public string Email;
    private string Password;
    public string FirstName;
    public string LastName;
    public string PhoneNumber;
    public int AccountLevel {get; private set;}

    public Accounts(string email, string password, string firstname, string lastname, string phonenumber, int accountlevel) {
        ID = 999999999;
        Email = email;
        Password = password;
        FirstName = firstname;
        LastName = lastname;
        PhoneNumber = phonenumber;
        AccountLevel = accountlevel;
    }

    public Accounts(int id, string email, string password, string firstname, string lastname, string phonenumber, int accountlevel) {
        ID = id;
        Email = email;
        Password = password;
        FirstName = firstname;
        LastName = lastname;
        PhoneNumber = phonenumber;
        AccountLevel = accountlevel;
    }

    public Accounts(int id, string email, string firstname, string lastname, string phonenumber, int accountlevel) {
        ID = id;
        Email = email;
        Password = "";
        FirstName = firstname;
        LastName = lastname;
        PhoneNumber = phonenumber;
        AccountLevel = accountlevel;
    }

    public string GetPassword() {
        return Password;
    }

    public override string ToString()
    {
        return $"ID: {ID}\nEmail: {Email}\nFirstName: {FirstName}\nLastName: {LastName}\nPhoneNumber: {PhoneNumber}\nAccountLevel: {AccountLevel}";
    }
}