namespace RRSTests;
using Microsoft.Data.Sqlite;

[TestClass]
public sealed class Test1
{
    [TestMethod]
    public void ConnectToDatabase()
    {
        //Arrange
        string dbFileLocation = $"{Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent}/../RRS/Data/Database/rrs.db";
        SqliteConnection SQLiteDBConnection = null;
        
        //Act
        SQLiteDBConnection = Database.CreateConn(dbFileLocation);
        
        //Assert
        Assert.AreNotEqual(null, SQLiteDBConnection);
    }

    [TestMethod]
    public void InsertNewAccount()
    {
        //Arrange
        Accounts account = new Accounts("Peter.Pannekoek@test.nl", "Peter", "Pannekoek", "0634777548", "NL", 1);
        string dbFileLocation = $"{Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent}/../RRS/Data/Database/rrs.db";
        SqliteConnection SQLiteDBConnection = null;
        string AccountFirstName = "";

        //Act
        SQLiteDBConnection = Database.CreateConn(dbFileLocation);

        SqliteCommand sqlite_cmd;
        sqlite_cmd = SQLiteDBConnection.CreateCommand();
        sqlite_cmd.CommandText = $"INSERT INTO Accounts (Email, Password, FirstName, LastName, PhoneNumber, Language, AccountLevel) VALUES ('{account.Email}', '{account.GetPassword()}', '{account.FirstName}', '{account.LastName}', '{account.PhoneNumber}', 'EN', {account.AccountLevel});";
        sqlite_cmd.ExecuteNonQuery();

        SqliteDataReader sqlite_datareader;
        sqlite_cmd = SQLiteDBConnection.CreateCommand();
        sqlite_cmd.CommandText = $"SELECT * FROM Accounts WHERE Email = \"testy.testing@test.nl\"";

        sqlite_datareader = sqlite_cmd.ExecuteReader();
        while (sqlite_datareader.Read())
        {
            AccountFirstName = sqlite_datareader.GetString(3);
        }

        //Assert
        Assert.AreNotEqual("", AccountFirstName);

    }

}
