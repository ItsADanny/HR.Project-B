using System.Data.SQLite;
using Microsoft.Data.Sqlite;

public static class Database {
    //[16-10-2024] - [82924077] – [ItsDanny]
    //[Created a function for connecting to our database]
    //This function will create a connection to the SQLite database
    public static SqliteConnection CreateConn() {
        //Create a new database connection
        SqliteConnection db_conn = new SqliteConnection("Data Source=Data/Database/rrs.db");

        //Try to open the connection, if its not possible then we return a null
        try {
            db_conn.Open();
            //When this has been a succes, write a line to our log file
            Log.Write("Connection to database has been succesfully created");
            return db_conn;
        } catch (Exception ex) {
            //Write the error to a current logfile
            Log.WriteError("There was an error when trying to create a connection to the database", ex);
            return null;
        }
    }

    //[16-10-2024] - [82924077] – [ItsDanny]
    //[Created a function for closing a connection to our database]
    //This function will close a connection to the SQLite database
    public static bool CloseConn(SqliteConnection db_conn) {
        try {
            db_conn.Close();
            //Write that a connection has been closed to the logfile
            Log.Write("Connection to database has been succesfully closed");
            return true;
        } catch (Exception ex) {
            //Write the error to our current logfile
            Log.WriteError("There was an error while closing the database connection", ex);
            return false;
        }
    }

    //[22-10-2024] - [82924077] – [ItsDanny]
    //[Updated the Accounts Table script so that it incorperates the two new fields ("FirstName" and "LastName")]
    //[21-10-2024] - [82924077] – [ItsDanny]
    //[Finished creating the DBCheck function]
    //[16-10-2024] - [82924077] – [ItsDanny]
    //[Created a function for performing a check on our database]
    //This function will perform a check on the database to see
    //if all the required tables have been created
    public static void DBCheck() {
        //TODO: Write to the log file that a DBCheck has been started
        Log.Write("Starting Database Check");

        //SQL scripts for creating the required tables if they don't exist
        string sql_restaurant = "CREATE TABLE IF NOT EXISTS \"Restaurants\" (\"ID\" INTEGER, \"Name\" INTEGER NOT NULL, \"MaxTables\" INTEGER DEFAULT 1, \"MaxOccupancy\" INTEGER DEFAULT 1, \"Country\" TEXT DEFAULT 'NL', \"City\" TEXT NOT NULL, \"Street\" TEXT NOT NULL, \"PostCode\" TEXT NOT NULL, \"HouseNumber\"	TEXT NOT NULL, \"OpeningTime\"	TEXT DEFAULT '09:00', \"ClosingTime\"	TEXT DEFAULT '20:00', PRIMARY KEY(\"ID\" AUTOINCREMENT))";
        string sql_account = "CREATE TABLE IF NOT EXISTS \"Accounts\" (\"ID\" INTEGER, \"Email\" TEXT NOT NULL UNIQUE, \"Password\" TEXT, \"FirstName\" TEXT NOT NULL, \"LastName\" TEXT NOT NULL, \"AccountLevel\" INTEGER NOT NULL, \"PhoneNumber\" TEXT, PRIMARY KEY(\"ID\" AUTOINCREMENT), FOREIGN KEY(\"AccountLevel\") REFERENCES \"AccountLevel\"(\"ID\"))";
        string sql_accountlevel = "CREATE TABLE IF NOT EXISTS \"AccountLevel\" (\"ID\" INTEGER,\"Name\" TEXT NOT NULL, \"CanChangeReservations\" INTEGER DEFAULT 0, \"CanChangeTimeSlots\" INTEGER DEFAULT 0,\"CanCancelReservations\" INTEGER DEFAULT 0, \"IsAnAdmin\" INTEGER DEFAULT 0, \"CanCreateAdmins\" INTEGER DEFAULT 0, PRIMARY KEY(\"ID\" AUTOINCREMENT))";
        string sql_tables = "CREATE TABLE IF NOT EXISTS \"Tables\" (\"ID\" INTEGER, \"RestaurantID\" INTEGER NOT NULL, \"Name\" TEXT NOT NULL DEFAULT 'Table', \"MaxSize\" INTEGER DEFAULT 1, PRIMARY KEY(\"ID\" AUTOINCREMENT), FOREIGN KEY(\"RestaurantID\") REFERENCES \"\")";
        string sql_menu = "CREATE TABLE IF NOT EXISTS \"Menu\" (\"ID\" INTEGER, \"RestaurantID\" INTEGER NOT NULL, \"Name\" TEXT NOT NULL, \"Description\" TEXT, \"FoodType\" INTEGER NOT NULL, \"Price\" NUMERIC NOT NULL DEFAULT 0, PRIMARY KEY(\"ID\" AUTOINCREMENT), FOREIGN KEY(\"FoodType\") REFERENCES \"FoodType\"(\"ID\"), FOREIGN KEY(\"RestaurantID\") REFERENCES \"Restaurants\"(\"ID\"))";
        string sql_foodtype = "CREATE TABLE IF NOT EXISTS \"FoodType\" (\"ID\" INTEGER, \"RestaurantID\" INTEGER NOT NULL, \"Name\" TEXT NOT NULL, PRIMARY KEY(\"ID\" AUTOINCREMENT), FOREIGN KEY(\"RestaurantID\") REFERENCES \"\")";
        string sql_reservations = "CREATE TABLE IF NOT EXISTS \"Reservations\" (\"ID\" INTEGER, \"RestaurantID\" INTEGER NOT NULL, \"TimeSlotID\" INTEGER NOT NULL, \"TableID\" INTEGER NOT NULL, \"AccountID\" INTEGER NOT NULL, \"Status\" INTEGER DEFAULT 0, PRIMARY KEY(\"ID\" AUTOINCREMENT), FOREIGN KEY(\"AccountID\") REFERENCES \"Accounts\"(\"ID\"), FOREIGN KEY(\"RestaurantID\") REFERENCES \"Restaurants\"(\"ID\"), FOREIGN KEY(\"TableID\") REFERENCES \"Tables\"(\"ID\"), FOREIGN KEY(\"TimeSlotID\") REFERENCES \"ReservationTimeSlots\"(\"ID\"))";
        string sql_reservationsTimeSlots = "CREATE TABLE IF NOT EXISTS \"ReservationTimeSlots\" (\"ID\" INTEGER, \"RestaurantID\" INTEGER NOT NULL, \"Date\" TEXT NOT NULL, \"StartTime\" TEXT NOT NULL, \"EndTime\" TEXT NOT NULL, PRIMARY KEY(\"ID\" AUTOINCREMENT), FOREIGN KEY(\"RestaurantID\") REFERENCES \"\")";

        //Creating a connection to the database
        SqliteConnection db_conn = CreateConn();
        
        //Check to see if we have a connection
        if (db_conn is not null) {
            SqliteCommand sqlite_cmd;
            sqlite_cmd = db_conn.CreateCommand();
            //If needed create the restaurant database
            sqlite_cmd.CommandText = sql_restaurant;
            sqlite_cmd.ExecuteNonQuery();
            sqlite_cmd.CommandText = sql_account;
            sqlite_cmd.ExecuteNonQuery();
            sqlite_cmd.CommandText = sql_accountlevel;
            sqlite_cmd.ExecuteNonQuery();
            sqlite_cmd.CommandText = sql_tables;
            sqlite_cmd.ExecuteNonQuery();
            sqlite_cmd.CommandText = sql_menu;
            sqlite_cmd.ExecuteNonQuery();
            sqlite_cmd.CommandText = sql_foodtype;
            sqlite_cmd.ExecuteNonQuery();
            sqlite_cmd.CommandText = sql_reservations;
            sqlite_cmd.ExecuteNonQuery();
            sqlite_cmd.CommandText = sql_reservationsTimeSlots;
            sqlite_cmd.ExecuteNonQuery();

            //After completing the Queries. Close the database connection
            CloseConn(db_conn);

            Log.Write("Database check succesfully ended");
        } else {
            //If we don't have a connection, we write a line to our log file that
            //we can't preform a check on the database because we can't make
            //a connection to it.
            Log.WriteError("Can't perform database check due to being unable to connect to the SQLite database\nClosing Database Check");
        }
    }

    //INSERT
    //========================================================================================

    //[22-10-2024] - [82924077] – [ItsDanny]
    //[Added the insert function for the Menu, Reservations and Account]
    //Menu
    public static bool Insert(Menu menuItem) {
        if (menuItem is not null) {
            //Creating a connection to the database
            SqliteConnection db_conn = CreateConn();

            SqliteCommand sqlite_cmd;
            sqlite_cmd = db_conn.CreateCommand();
            sqlite_cmd.CommandText = $"INSERT INTO Menu (RestaurantID, Name, Description, FoodType, Price) VALUES ({menuItem.RestaurantID}, '{menuItem.Name}', '{menuItem.Description}', '{menuItem.Foodtype}', {menuItem.Price});";
            sqlite_cmd.ExecuteNonQuery();

            //Close the connection to the database
            CloseConn(db_conn);

            return true;
        }
        return false;
    }

    public static bool Insert(List<Menu> menuItems) {
        if (menuItems is not null) {
            foreach (Menu menuItem in menuItems) { 
                Insert(menuItem);
            }
            return true;
        }
        return false;
    }

    //Reservations
    public static bool Insert(Reservations reservation) {
        if (reservation is not null) {
            //Creating a connection to the database
            SqliteConnection db_conn = CreateConn();

            SqliteCommand sqlite_cmd;
            sqlite_cmd = db_conn.CreateCommand();
            sqlite_cmd.CommandText = $"INSERT INTO Reservations (RestaurantID, TimeSlotID, TableID, Account, Status) VALUES ({reservation.RestaurantID}, '{reservation.TimeSlotID}', '{reservation.TableID}', '{reservation.AccountID}', {reservation.Status});";
            sqlite_cmd.ExecuteNonQuery();

            //Close the connection to the database
            CloseConn(db_conn);

            return true;
        }
        return false;
    }

    public static bool Insert(List<Reservations> reservations) {
        if (reservations is not null) {
            foreach (Reservations reservation in reservations) { 
                Insert(reservation);
            }
            return true;
        }
        return false;
    }

    //Accounts
    public static bool Insert(Accounts account) {
        if (account is not null) {
            //Creating a connection to the database
            SqliteConnection db_conn = CreateConn();

            SqliteCommand sqlite_cmd;
            sqlite_cmd = db_conn.CreateCommand();
            sqlite_cmd.CommandText = $"INSERT INTO Accounts (Email, Password, FirstName, LastName, PhoneNumber, AccountLevel) VALUES ('{account.Email}', '{account.GetPassword()}', '{account.FirstName}', '{account.LastName}', '{account.PhoneNumber}', {account.AccountLevel});";
            sqlite_cmd.ExecuteNonQuery();

            //Close the connection to the database
            CloseConn(db_conn);

            return true;
        }
        return false;
    }

    public static bool Insert(List<Accounts> accounts) {
        if (accounts is not null) {
            foreach (Accounts account in accounts) { 
                Insert(account);
            }
            return true;
        }
        return false;
    }


    //Select - Account

    //Select by Email and Password
    public static Accounts SelectAccount(string email, string password) {
        SqliteConnection db_conn = CreateConn();

        if (db_conn is not null) {
            SqliteDataReader sqlite_datareader;
            SqliteCommand sqlite_cmd;
            sqlite_cmd = db_conn.CreateCommand();
            sqlite_cmd.CommandText = $"SELECT * FROM Accounts WHERE Email = '{email}' AND Password = '{password}'";

            sqlite_datareader = sqlite_cmd.ExecuteReader();
            while (sqlite_datareader.Read())
            {                
                int AccountID = sqlite_datareader.GetInt32(0);
                string AccountEmail = sqlite_datareader.GetString(1);
                string AccountFirstName = sqlite_datareader.GetString(3);
                string AccountLastName = sqlite_datareader.GetString(4);
                string AccountPhoneNumber = sqlite_datareader.GetString(5);
                int AccountAccountLevel = sqlite_datareader.GetInt32(6);

                return new Accounts(AccountID, AccountEmail, AccountFirstName, AccountLastName, AccountPhoneNumber, AccountAccountLevel);
            }
        }
        return null;
    }

    //Select by ID
    public static Accounts SelectAccount(int ID) {
        SqliteConnection db_conn = CreateConn();

        if (db_conn is not null) {
            SqliteDataReader sqlite_datareader;
            SqliteCommand sqlite_cmd;
            sqlite_cmd = db_conn.CreateCommand();
            sqlite_cmd.CommandText = $"SELECT * FROM Accounts WHERE ID = {ID}";

            sqlite_datareader = sqlite_cmd.ExecuteReader();
            while (sqlite_datareader.Read())
            {
                int AccountID = sqlite_datareader.GetInt32(0);
                string AccountEmail = sqlite_datareader.GetString(1);
                string AccountFirstName = sqlite_datareader.GetString(3);
                string AccountLastName = sqlite_datareader.GetString(4);
                string AccountPhoneNumber = sqlite_datareader.GetString(5);
                int AccountAccountLevel = sqlite_datareader.GetInt32(6);

                return new Accounts(AccountID, AccountEmail, AccountFirstName, AccountLastName, AccountPhoneNumber, AccountAccountLevel);
            }
        }
        return null;
    } 

    //Select - FoodType

    //Select Everything
    public static List<FoodType> SelectFoodType() {
        SqliteConnection db_conn = CreateConn();

        if (db_conn is not null) {
            List<FoodType> results = [];

            SqliteDataReader sqlite_datareader;
            SqliteCommand sqlite_cmd;
            sqlite_cmd = db_conn.CreateCommand();
            sqlite_cmd.CommandText = $"SELECT * FROM FoodType";

            sqlite_datareader = sqlite_cmd.ExecuteReader();
            while (sqlite_datareader.Read())
            {
                int FoodTypeID = sqlite_datareader.GetInt32(0);
                int FoodTypeRestaurantID = sqlite_datareader.GetInt32(1);
                string FoodTypeName = sqlite_datareader.GetString(2);

                results.Add(new FoodType(FoodTypeID, FoodTypeRestaurantID, FoodTypeName));
            }

            return results;
        }
        return null;
    }

    //Select Everything for a certain Restaurant
    public static List<FoodType> SelectFoodType(int restaurantID) {
        SqliteConnection db_conn = CreateConn();

        if (db_conn is not null) {
            List<FoodType> results = [];

            SqliteDataReader sqlite_datareader;
            SqliteCommand sqlite_cmd;
            sqlite_cmd = db_conn.CreateCommand();
            sqlite_cmd.CommandText = $"SELECT * FROM FoodType WHERE RestaurantID = {restaurantID}";

            sqlite_datareader = sqlite_cmd.ExecuteReader();
            while (sqlite_datareader.Read())
            {
                int FoodTypeID = sqlite_datareader.GetInt32(0);
                int FoodTypeRestaurantID = sqlite_datareader.GetInt32(1);
                string FoodTypeName = sqlite_datareader.GetString(2);

                results.Add(new FoodType(FoodTypeID, FoodTypeRestaurantID, FoodTypeName));
            }

            return results;
        }
        return null;
    }

    //Select Name by ID and Restaurant
    public static string SelectFoodType(int FoodTypeID, int restaurantID) {
        SqliteConnection db_conn = CreateConn();

        if (db_conn is not null) {
            SqliteDataReader sqlite_datareader;
            SqliteCommand sqlite_cmd;
            sqlite_cmd = db_conn.CreateCommand();
            sqlite_cmd.CommandText = $"SELECT * FROM FoodType";

            sqlite_datareader = sqlite_cmd.ExecuteReader();
            while (sqlite_datareader.Read())
            {
                return sqlite_datareader.GetString(2);
            }
        }
        return null;
    }
}