using Microsoft.Data.Sqlite;

public static class Database {
    //[16-10-2024] - [82924077] – [ItsDanny]
    //[Created a function for connecting to our database]
    //This function will create a connection to the SQLite database
    public static SqliteConnection CreateConn() {
        //This is purely to make this part work with Windows and MacOS
        string dbLocation;
        // if (System.Runtime.InteropServices.RuntimeInformation.OSDescription.Contains("Darwin")) {
        //     dbLocation = $"Data/Database/rrs.db";
        // } else {
            dbLocation = $"{Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent}/Data/Database/rrs.db";
        // }

        //Create a new database connection
        SqliteConnection db_conn = new SqliteConnection($"Data Source={dbLocation}");

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
        string sql_foodtype = "CREATE TABLE IF NOT EXISTS \"MenuFoodTypes\" (\"ID\" INTEGER, \"RestaurantID\" INTEGER NOT NULL, \"Name\" TEXT NOT NULL, PRIMARY KEY(\"ID\" AUTOINCREMENT), FOREIGN KEY(\"RestaurantID\") REFERENCES \"\")";
        string sql_reservations = "CREATE TABLE IF NOT EXISTS \"Reservations\" (\"ID\" INTEGER, \"RestaurantID\" INTEGER NOT NULL, \"TimeSlotID\" INTEGER NOT NULL, \"TableID\" INTEGER NOT NULL, \"AccountID\" INTEGER NOT NULL, \"Status\" INTEGER DEFAULT 0, PRIMARY KEY(\"ID\" AUTOINCREMENT), FOREIGN KEY(\"AccountID\") REFERENCES \"Accounts\"(\"ID\"), FOREIGN KEY(\"RestaurantID\") REFERENCES \"Restaurants\"(\"ID\"), FOREIGN KEY(\"TableID\") REFERENCES \"Tables\"(\"ID\"), FOREIGN KEY(\"TimeSlotID\") REFERENCES \"ReservationTimeSlots\"(\"ID\"))";
        string sql_reservationsTimeSlots = "CREATE TABLE IF NOT EXISTS \"ReservationTimeSlots\" (\"ID\" INTEGER, \"RestaurantID\" INTEGER NOT NULL, \"ReservationDate\" TEXT NOT NULL, \"StartTime\" TEXT NOT NULL, \"EndTime\" TEXT NOT NULL, PRIMARY KEY(\"ID\" AUTOINCREMENT), FOREIGN KEY(\"RestaurantID\") REFERENCES \"\")";

        string sql_insert_foodtype_breakfast = "INSERT INTO MenuFoodTypes(\"RestaurantID\", \"Name\") VALUES (0, 'Breakfast');";
        string sql_insert_foodtype_lunch = "INSERT INTO MenuFoodTypes(\"RestaurantID\", \"Name\") VALUES (0, 'Lunch');";
        string sql_insert_foodtype_dinner = "INSERT INTO MenuFoodTypes(\"RestaurantID\", \"Name\") VALUES (0, 'Dinner');";
        string sql_insert_foodtype_desert = "INSERT INTO MenuFoodTypes(\"RestaurantID\", \"Name\") VALUES (0, 'Desert');";
        string sql_insert_foodtype_Nonalcoholic = "INSERT INTO MenuFoodTypes(\"RestaurantID\", \"Name\") VALUES (0, 'Non-alcoholic beverages');";
        string sql_insert_foodtype_Alcoholic = "INSERT INTO MenuFoodTypes(\"RestaurantID\", \"Name\") VALUES (0, 'Alcoholic beverages');";

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

            //TEMP
            // sqlite_cmd.CommandText = sql_insert_foodtype_breakfast;
            // sqlite_cmd.ExecuteNonQuery();
            // sqlite_cmd.CommandText = sql_insert_foodtype_lunch;
            // sqlite_cmd.ExecuteNonQuery();
            // sqlite_cmd.CommandText = sql_insert_foodtype_dinner;
            // sqlite_cmd.ExecuteNonQuery();
            // sqlite_cmd.CommandText = sql_insert_foodtype_desert;
            // sqlite_cmd.ExecuteNonQuery();
            // sqlite_cmd.CommandText = sql_insert_foodtype_Nonalcoholic;
            // sqlite_cmd.ExecuteNonQuery();
            // sqlite_cmd.CommandText = sql_insert_foodtype_Alcoholic;
            // sqlite_cmd.ExecuteNonQuery();

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
            sqlite_cmd.CommandText = $"INSERT INTO Menu (RestaurantID, Name, Description, Foodtype, Price) VALUES ({menuItem.RestaurantID}, '{menuItem.Name}', '{menuItem.Description}', '{menuItem.Foodtype}', {menuItem.Price});";
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
            sqlite_cmd.CommandText = $"INSERT INTO Reservations (RestaurantID, TimeSlotID, TableID, AccountID, Status) VALUES ({reservation.RestaurantID}, '{reservation.TimeSlotID}', '{reservation.TableID}', '{reservation.AccountID}', {reservation.Status});";
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
            sqlite_cmd.CommandText = $"INSERT INTO Accounts (Email, Password, FirstName, LastName, PhoneNumber, Language, AccountLevel) VALUES ('{account.Email}', '{account.GetPassword()}', '{account.FirstName}', '{account.LastName}', '{account.PhoneNumber}', 'EN', {account.AccountLevel});";
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

    //Timeslot
    public static bool Insert(ReservationTimeSlots timeSlot) {
        if (timeSlot is not null) {
            //Creating a connection to the database
            SqliteConnection db_conn = CreateConn();

            SqliteCommand sqlite_cmd;
            sqlite_cmd = db_conn.CreateCommand();
            sqlite_cmd.CommandText = $"INSERT INTO ReservationTimeSlots (RestaurantID, ReservationDate, StartTime, EndTime) VALUES ({timeSlot.RestaurantID}, '{timeSlot.GetDate()}', '{timeSlot.GetStartTime24()}', '{timeSlot.GetEndTime24()}');";
            sqlite_cmd.ExecuteNonQuery();

            //Close the connection to the database
            CloseConn(db_conn);

            return true;
        }
        return false;
    }

    public static bool Insert(Review reviews) {
        if (reviews is not null) {
            //Creating a connection to the database
            SqliteConnection db_conn = CreateConn();

            SqliteCommand sqlite_cmd;
            sqlite_cmd = db_conn.CreateCommand();
            sqlite_cmd.CommandText = $"INSERT INTO Review (RestaurantID, UserID, ReservationID, Rating, Comment) VALUES ({reviews.RestaurantID}, '{reviews.AccountID}', '{reviews.ReservationID}', '{reviews.Rating}', \"{reviews.Comment}\");";
            sqlite_cmd.ExecuteNonQuery();

            //Close the connection to the database
            CloseConn(db_conn);

            return true;
        }
        return false;
    }

    public static bool Insert(List<Review> Reviews) {
        if (Reviews is not null) {
            foreach (Review review in Reviews) { 
                Insert(review);
            }
            return true;
        }
        return false;
    }

    public static bool InsertInformationShare(Accounts accountOne, Accounts accountTwo) {
        if (accountOne is not null & accountTwo is not null) {
            //Creating a connection to the database
            SqliteConnection db_conn = CreateConn();

            SqliteCommand sqlite_cmd;
            sqlite_cmd = db_conn.CreateCommand();
            sqlite_cmd.CommandText = $"INSERT INTO InfoShared (AccountID_One, AccountID_Two) VALUES ({accountOne.ID}, {accountTwo.ID});";
            sqlite_cmd.ExecuteNonQuery();

            //Close the connection to the database
            CloseConn(db_conn);

            return true;
        }
        return false;
    }

    //[23-10-2024] - [82924077] – [ItsDanny]
    //[Added the select function for the Menu, Reservations and Account]
    //Select - Account

    //Select by Email and Password
    public static Accounts SelectAccount(string email, string password) {
        SqliteConnection db_conn = CreateConn();

        if (db_conn is not null) {
            SqliteDataReader sqlite_datareader;
            SqliteCommand sqlite_cmd;
            sqlite_cmd = db_conn.CreateCommand();
            if (password == "firstname") {
                sqlite_cmd.CommandText = $"SELECT * FROM Accounts WHERE FirstName = '{email}'";
            } else if (password == "lastname") {
                sqlite_cmd.CommandText = $"SELECT * FROM Accounts WHERE LastName = '{email}'";
            } else if (password == "email") {
                sqlite_cmd.CommandText = $"SELECT * FROM Accounts WHERE Email = '{email}'";
            } else {
                sqlite_cmd.CommandText = $"SELECT * FROM Accounts WHERE Email = '{email}' AND Password = '{password}'";
            }
            
            sqlite_datareader = sqlite_cmd.ExecuteReader();
            while (sqlite_datareader.Read())
            {                
                int AccountID = sqlite_datareader.GetInt32(0);
                string AccountEmail = sqlite_datareader.GetString(1);
                string AccountFirstName = sqlite_datareader.GetString(3);
                string AccountLastName = sqlite_datareader.GetString(4);
                string AccountPhoneNumber = sqlite_datareader.GetString(5);
                string AccountLanguage = sqlite_datareader.GetString(6);
                int AccountAccountLevel = sqlite_datareader.GetInt32(7);

                CloseConn(db_conn);
                return new Accounts(AccountID, AccountEmail, AccountFirstName, AccountLastName, AccountPhoneNumber, AccountLanguage, AccountAccountLevel);
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
                string AccountLanguage = sqlite_datareader.GetString(6);
                int AccountAccountLevel = sqlite_datareader.GetInt32(7);

                CloseConn(db_conn);
                return new Accounts(AccountID, AccountEmail, AccountFirstName, AccountLastName, AccountPhoneNumber, AccountLanguage, AccountAccountLevel);
            }
        }
        return null;
    }

    public static List<Accounts> SelectAccount() {
        SqliteConnection db_conn = CreateConn();

        if (db_conn is not null) {
            List<Accounts> results = [];

            SqliteDataReader sqlite_datareader;
            SqliteCommand sqlite_cmd;
            sqlite_cmd = db_conn.CreateCommand();
            sqlite_cmd.CommandText = $"SELECT * FROM Accounts";

            sqlite_datareader = sqlite_cmd.ExecuteReader();
            while (sqlite_datareader.Read())
            {                
                int AccountID = sqlite_datareader.GetInt32(0);
                string AccountEmail = sqlite_datareader.GetString(1);
                string AccountFirstName = sqlite_datareader.GetString(3);
                string AccountLastName = sqlite_datareader.GetString(4);
                string AccountPhoneNumber = sqlite_datareader.GetString(5);
                string AccountLanguage = sqlite_datareader.GetString(6);
                int AccountAccountLevel = sqlite_datareader.GetInt32(7);

                results.Add(new Accounts(AccountID, AccountEmail, AccountFirstName, AccountLastName, AccountPhoneNumber, AccountLanguage, AccountAccountLevel));
            }
            CloseConn(db_conn);
            return results;
        }
        return null;
    }

    public static List<Accounts> SelectAccount(string input, string choice, bool returnList) {
        if (returnList) {
            SqliteConnection db_conn = CreateConn();

            if (db_conn is not null) {
                List<Accounts> results = [];

                SqliteDataReader sqlite_datareader;
                SqliteCommand sqlite_cmd;
                sqlite_cmd = db_conn.CreateCommand();
                if (choice == "firstname") {
                    sqlite_cmd.CommandText = $"SELECT * FROM Accounts WHERE FirstName = '{input}'";
                } else if (choice == "lastname") {
                    sqlite_cmd.CommandText = $"SELECT * FROM Accounts WHERE LastName = '{input}'";
                }

                sqlite_datareader = sqlite_cmd.ExecuteReader();
                while (sqlite_datareader.Read())
                {                
                    int AccountID = sqlite_datareader.GetInt32(0);
                    string AccountEmail = sqlite_datareader.GetString(1);
                    string AccountFirstName = sqlite_datareader.GetString(3);
                    string AccountLastName = sqlite_datareader.GetString(4);
                    string AccountPhoneNumber = sqlite_datareader.GetString(5);
                    string AccountLanguage = sqlite_datareader.GetString(6);
                    int AccountAccountLevel = sqlite_datareader.GetInt32(7);

                    results.Add(new Accounts(AccountID, AccountEmail, AccountFirstName, AccountLastName, AccountPhoneNumber, AccountLanguage, AccountAccountLevel));
                }
                CloseConn(db_conn);
                return results;
            }
            return null;
        } else {
            return [SelectAccount(input, choice)];
        }
    }

    //Select - From Account - Return Hashed Password Only
    public static string SelectAccountPassword(int ID) {
        SqliteConnection db_conn = CreateConn();

        if (db_conn is not null) {
            SqliteDataReader sqlite_datareader;
            SqliteCommand sqlite_cmd;
            sqlite_cmd = db_conn.CreateCommand();
            sqlite_cmd.CommandText = $"SELECT * FROM Accounts WHERE ID = {ID}";

            sqlite_datareader = sqlite_cmd.ExecuteReader();
            while (sqlite_datareader.Read())
            {
                CloseConn(db_conn);
                return sqlite_datareader.GetString(2);
            }
        }
        return null;
    }

    //Select - From Account - Returns a True if an inputted Password matches with the current Password
    public static bool CheckAccountPassword(int ID, string Input) {
        SqliteConnection db_conn = CreateConn();

        if (db_conn is not null) {
            SqliteDataReader sqlite_datareader;
            SqliteCommand sqlite_cmd;
            sqlite_cmd = db_conn.CreateCommand();
            sqlite_cmd.CommandText = $"SELECT * FROM Accounts WHERE ID = {ID}";

            sqlite_datareader = sqlite_cmd.ExecuteReader();
            while (sqlite_datareader.Read())
            {
                string hashed_password = sqlite_datareader.GetString(2);
                CloseConn(db_conn);
                if (hashed_password == Input) {
                    return true;
                }
            }
        }
        return false;
    }

    public static bool DoesEmailAlreadyExist(string input) {
        SqliteConnection db_conn = CreateConn();

        if (db_conn is not null) {
            SqliteDataReader sqlite_datareader;
            SqliteCommand sqlite_cmd;
            sqlite_cmd = db_conn.CreateCommand();
            sqlite_cmd.CommandText = $"SELECT * FROM Accounts WHERE Email = \"{input}\"";
            
            sqlite_datareader = sqlite_cmd.ExecuteReader();
            while (sqlite_datareader.Read())
            {
                string email = sqlite_datareader.GetString(1);
                CloseConn(db_conn);
                if (email == input) {
                    return true;
                }
            }
        }
        return false;
    }

    //Select - AccountLevel

    //Select Everything
    public static List<AccountLevel> SelectAccountLevel() {
        SqliteConnection db_conn = CreateConn();

        if (db_conn is not null) {
            List<AccountLevel> results = [];

            SqliteDataReader sqlite_datareader;
            SqliteCommand sqlite_cmd;
            sqlite_cmd = db_conn.CreateCommand();
            sqlite_cmd.CommandText = $"SELECT * FROM AccountLevel";

            sqlite_datareader = sqlite_cmd.ExecuteReader();
            while (sqlite_datareader.Read())
            {                
                int AccountLevel_ID = sqlite_datareader.GetInt32(0);
                string AccountLevel_Name = sqlite_datareader.GetString(1);
                int AccountLevel_CanChangeReservations = sqlite_datareader.GetInt32(2);
                int AccountLevel_CanChangeTimeSlots = sqlite_datareader.GetInt32(3);
                int AccountLevel_CanCancelReservations = sqlite_datareader.GetInt32(4);
                int AccountLevel_IsAnAdmin = sqlite_datareader.GetInt32(5);
                int AccountLevel_CanCreateAdmins = sqlite_datareader.GetInt32(6);
                int AccountLevel_CanViewLogs = sqlite_datareader.GetInt32(7);

                results.Add(new AccountLevel(AccountLevel_ID, AccountLevel_Name, AccountLevel_CanChangeReservations, AccountLevel_CanChangeTimeSlots, AccountLevel_CanCancelReservations, AccountLevel_IsAnAdmin, AccountLevel_CanCreateAdmins, AccountLevel_CanViewLogs));
            }
            CloseConn(db_conn);
            return results;
        }
        return null;
    }

    //Select By ID
    public static AccountLevel SelectAccountLevel(int AccountLevelID) {
        SqliteConnection db_conn = CreateConn();

        if (db_conn is not null) {
            SqliteDataReader sqlite_datareader;
            SqliteCommand sqlite_cmd;
            sqlite_cmd = db_conn.CreateCommand();
            sqlite_cmd.CommandText = $"SELECT * FROM AccountLevel WHERE ID = {AccountLevelID}";

            sqlite_datareader = sqlite_cmd.ExecuteReader();
            while (sqlite_datareader.Read())
            {                
                int AccountLevel_ID = sqlite_datareader.GetInt32(0);
                string AccountLevel_Name = sqlite_datareader.GetString(1);
                int AccountLevel_CanChangeReservations = sqlite_datareader.GetInt32(2);
                int AccountLevel_CanChangeTimeSlots = sqlite_datareader.GetInt32(3);
                int AccountLevel_CanCancelReservations = sqlite_datareader.GetInt32(4);
                int AccountLevel_IsAnAdmin = sqlite_datareader.GetInt32(5);
                int AccountLevel_CanCreateAdmins = sqlite_datareader.GetInt32(6);
                int AccountLevel_CanViewLogs = sqlite_datareader.GetInt32(7);

                CloseConn(db_conn);
                return new AccountLevel(AccountLevel_ID, AccountLevel_Name, AccountLevel_CanChangeReservations, AccountLevel_CanChangeTimeSlots, AccountLevel_CanCancelReservations, AccountLevel_IsAnAdmin, AccountLevel_CanCreateAdmins, AccountLevel_CanViewLogs);
            }
        }
        return null;
    }

    //Select - Menu
    public static List<Menu> SelectMenu(int RestaurantID) {
        SqliteConnection db_conn = CreateConn();

        if (db_conn is not null) {
            List<Menu> results = [];

            SqliteDataReader sqlite_datareader;
            SqliteCommand sqlite_cmd;
            sqlite_cmd = db_conn.CreateCommand();
            sqlite_cmd.CommandText = $"SELECT * FROM Menu WHERE RestaurantID = \"{RestaurantID}\"";

            sqlite_datareader = sqlite_cmd.ExecuteReader();
            while (sqlite_datareader.Read())
            {                
                int Menu_ID = sqlite_datareader.GetInt32(0);
                int Menu_RestaurantID = sqlite_datareader.GetInt32(1);
                string Menu_Name = sqlite_datareader.GetString(2);
                string Menu_Description = sqlite_datareader.GetString(3);
                int Menu_FoodType = sqlite_datareader.GetInt32(4);
                double Menu_Price = sqlite_datareader.GetDouble(5);

                results.Add(new Menu(Menu_ID, Menu_RestaurantID, Menu_Name, Menu_Description, Menu_Price, Menu_FoodType));
            }
            CloseConn(db_conn);
            return results;
        }
        return null;
    }

    //Select Everything


    //Select Everything for a certain Restaurant


    //Select Menu by ID and Restaurant


    //Select - FoodType

    //Select Everything
    public static List<FoodType> SelectFoodType() {
        SqliteConnection db_conn = CreateConn();

        if (db_conn is not null) {
            List<FoodType> results = [];

            SqliteDataReader sqlite_datareader;
            SqliteCommand sqlite_cmd;
            sqlite_cmd = db_conn.CreateCommand();
            sqlite_cmd.CommandText = $"SELECT * FROM MenuFoodTypes";

            sqlite_datareader = sqlite_cmd.ExecuteReader();
            while (sqlite_datareader.Read())
            {
                int FoodTypeID = sqlite_datareader.GetInt32(0);
                int FoodTypeRestaurantID = sqlite_datareader.GetInt32(1);
                string FoodTypeName = sqlite_datareader.GetString(2);

                results.Add(new FoodType(FoodTypeID, FoodTypeRestaurantID, FoodTypeName));
            }

            CloseConn(db_conn);
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
            sqlite_cmd.CommandText = $"SELECT * FROM MenuFoodTypes WHERE RestaurantID = {restaurantID}";

            sqlite_datareader = sqlite_cmd.ExecuteReader();
            while (sqlite_datareader.Read())
            {
                int FoodTypeID = sqlite_datareader.GetInt32(0);
                int FoodTypeRestaurantID = sqlite_datareader.GetInt32(1);
                string FoodTypeName = sqlite_datareader.GetString(2);

                results.Add(new FoodType(FoodTypeID, FoodTypeRestaurantID, FoodTypeName));
            }

            CloseConn(db_conn);
            return results;
        }
        return null;
    }

    public static int SelectFoodType(int restaurantID, string foodTypeName) {
        SqliteConnection db_conn = CreateConn();

        if (db_conn is not null) {
            SqliteDataReader sqlite_datareader;
            SqliteCommand sqlite_cmd;
            sqlite_cmd = db_conn.CreateCommand();
            sqlite_cmd.CommandText = $"SELECT * FROM MenuFoodTypes WHERE Name = \"{foodTypeName}\" AND RestaurantID = {restaurantID}";

            sqlite_datareader = sqlite_cmd.ExecuteReader();
            while (sqlite_datareader.Read())
            {   
                int SelectedFoodType = sqlite_datareader.GetInt32(0);
                CloseConn(db_conn);
                return SelectedFoodType;
            }
        }
        return 0;
    }

    //Select Name by ID and Restaurant
    public static string SelectFoodType(int FoodTypeID, int restaurantID) {
        SqliteConnection db_conn = CreateConn();

        if (db_conn is not null) {
            SqliteDataReader sqlite_datareader;
            SqliteCommand sqlite_cmd;
            sqlite_cmd = db_conn.CreateCommand();
            sqlite_cmd.CommandText = $"SELECT * FROM MenuFoodTypes WHERE ID = {FoodTypeID} AND RestaurantID = {restaurantID}";

            sqlite_datareader = sqlite_cmd.ExecuteReader();
            while (sqlite_datareader.Read())
            {
                CloseConn(db_conn);
                return sqlite_datareader.GetString(2);
            }
        }
        return null;
    }

    //Select - Reservations

    //Select Everything
    public static List<Reservations> SelectReservations() {
        SqliteConnection db_conn = CreateConn();

        if (db_conn is not null) {
            List<Reservations> results = [];

            SqliteDataReader sqlite_datareader;
            SqliteCommand sqlite_cmd;
            sqlite_cmd = db_conn.CreateCommand();
            sqlite_cmd.CommandText = $"SELECT * FROM Reservations";

            sqlite_datareader = sqlite_cmd.ExecuteReader();
            while (sqlite_datareader.Read())
            {
                int ReservationID = sqlite_datareader.GetInt32(0);
                int ReservationRestaurantID = sqlite_datareader.GetInt32(1);
                int ReservationTimeSlotID = sqlite_datareader.GetInt32(2);
                int ReservationTableID = sqlite_datareader.GetInt32(3);
                int ReservationAccountID = sqlite_datareader.GetInt32(4);
                int ReservationStatus = sqlite_datareader.GetInt32(5);

                results.Add(new Reservations(ReservationID, ReservationRestaurantID, ReservationTimeSlotID, ReservationTableID, ReservationAccountID, ReservationStatus));
            }

            CloseConn(db_conn);
            return results;
        }
        return null;
    }

    //Select Everything for a certain Restaurant
    public static List<Reservations> SelectReservations(int restaurantID) {
        SqliteConnection db_conn = CreateConn();

        if (db_conn is not null) {
            List<Reservations> results = [];

            SqliteDataReader sqlite_datareader;
            SqliteCommand sqlite_cmd;
            sqlite_cmd = db_conn.CreateCommand();
            sqlite_cmd.CommandText = $"SELECT * FROM Reservations WHERE RestaurantID = {restaurantID}";

            sqlite_datareader = sqlite_cmd.ExecuteReader();
            while (sqlite_datareader.Read())
            {
                int ReservationID = sqlite_datareader.GetInt32(0);
                int ReservationRestaurantID = sqlite_datareader.GetInt32(1);
                int ReservationTimeSlotID = sqlite_datareader.GetInt32(2);
                int ReservationTableID = sqlite_datareader.GetInt32(3);
                int ReservationAccountID = sqlite_datareader.GetInt32(4);
                int ReservationStatus = sqlite_datareader.GetInt32(5);

                results.Add(new Reservations(ReservationID, ReservationRestaurantID, ReservationTimeSlotID, ReservationTableID, ReservationAccountID, ReservationStatus));
            }

            CloseConn(db_conn);
            return results;
        }
        return null;
    }

    //Select - Reservations
    public static List<Reservations> SelectReservations(int restaurantID, int accountID) {
        SqliteConnection db_conn = CreateConn();

        if (db_conn is not null) {
            List<Reservations> results = [];

            SqliteDataReader sqlite_datareader;
            SqliteCommand sqlite_cmd;
            sqlite_cmd = db_conn.CreateCommand();
            sqlite_cmd.CommandText = $"SELECT * FROM Reservations WHERE RestaurantID = {restaurantID} AND AccountID = {accountID}";

            sqlite_datareader = sqlite_cmd.ExecuteReader();
            while (sqlite_datareader.Read())
            {
                int ReservationID = sqlite_datareader.GetInt32(0);
                int ReservationRestaurantID = sqlite_datareader.GetInt32(1);
                int ReservationTimeSlotID = sqlite_datareader.GetInt32(2);
                int ReservationTableID = sqlite_datareader.GetInt32(3);
                int ReservationAccountID = sqlite_datareader.GetInt32(4);
                int ReservationStatus = sqlite_datareader.GetInt32(5);

                results.Add(new Reservations(ReservationID, ReservationRestaurantID, ReservationTimeSlotID, ReservationTableID, ReservationAccountID, ReservationStatus));
            }

            CloseConn(db_conn);
            return results;
        }
        return null;
    }

    //Select Everything
    public static List<ReservationTimeSlots> SelectReservationTimeSlots() {
        SqliteConnection db_conn = CreateConn();

        if (db_conn is not null) {
            List<ReservationTimeSlots> results = [];

            SqliteDataReader sqlite_datareader;
            SqliteCommand sqlite_cmd;
            sqlite_cmd = db_conn.CreateCommand();
            sqlite_cmd.CommandText = $"SELECT * FROM ReservationTimeSlots";

            sqlite_datareader = sqlite_cmd.ExecuteReader();
            while (sqlite_datareader.Read())
            {
                int ReservationTimeSlotsID = sqlite_datareader.GetInt32(0);
                int ReservationTimeSlotsRestaurantID = sqlite_datareader.GetInt32(1);
                string ReservationTimeSlotsDate = sqlite_datareader.GetString(2);
                string ReservationTimeSlotsStartTime = sqlite_datareader.GetString(3);
                string ReservationTimeSlotsEndTime = sqlite_datareader.GetString(4);

                results.Add(new ReservationTimeSlots(ReservationTimeSlotsID, ReservationTimeSlotsRestaurantID, ReservationTimeSlotsDate, ReservationTimeSlotsStartTime, ReservationTimeSlotsEndTime));
            }

            CloseConn(db_conn);
            return results;
        }
        return null;
    }

    public static List<ReservationTimeSlots> SelectReservationTimeSlots(int restaurantID) {
        SqliteConnection db_conn = CreateConn();

        if (db_conn is not null) {
            List<ReservationTimeSlots> results = [];

            SqliteDataReader sqlite_datareader;
            SqliteCommand sqlite_cmd;
            sqlite_cmd = db_conn.CreateCommand();
            sqlite_cmd.CommandText = $"SELECT * FROM ReservationTimeSlots WHERE RestaurantID = {restaurantID}";

            sqlite_datareader = sqlite_cmd.ExecuteReader();
            while (sqlite_datareader.Read())
            {
                int ReservationTimeSlotsID = sqlite_datareader.GetInt32(0);
                int ReservationTimeSlotsRestaurantID = sqlite_datareader.GetInt32(1);
                string ReservationTimeSlotsDate = sqlite_datareader.GetString(2);
                string ReservationTimeSlotsStartTime = sqlite_datareader.GetString(3);
                string ReservationTimeSlotsEndTime = sqlite_datareader.GetString(4);

                results.Add(new ReservationTimeSlots(ReservationTimeSlotsID, ReservationTimeSlotsRestaurantID, ReservationTimeSlotsDate, ReservationTimeSlotsStartTime, ReservationTimeSlotsEndTime));
            }

            CloseConn(db_conn);
            return results;
        }
        return null;
    }

    //Select by ID
    public static ReservationTimeSlots SelectReservationTimeSlot(int timeSlotID) {
        SqliteConnection db_conn = CreateConn();

        if (db_conn is not null) {
            List<ReservationTimeSlots> results = [];

            SqliteDataReader sqlite_datareader;
            SqliteCommand sqlite_cmd;
            sqlite_cmd = db_conn.CreateCommand();
            sqlite_cmd.CommandText = $"SELECT * FROM ReservationTimeSlots WHERE ID = {timeSlotID}";

            sqlite_datareader = sqlite_cmd.ExecuteReader();
            while (sqlite_datareader.Read())
            {
                int ReservationTimeSlotsID = sqlite_datareader.GetInt32(0);
                int ReservationTimeSlotsRestaurantID = sqlite_datareader.GetInt32(1);
                string ReservationTimeSlotsDate = sqlite_datareader.GetString(2);
                string ReservationTimeSlotsStartTime = sqlite_datareader.GetString(3);
                string ReservationTimeSlotsEndTime = sqlite_datareader.GetString(4);

                CloseConn(db_conn);

                return new ReservationTimeSlots(ReservationTimeSlotsID, ReservationTimeSlotsRestaurantID, ReservationTimeSlotsDate, ReservationTimeSlotsStartTime, ReservationTimeSlotsEndTime);
            }
        }
        return null;
    }

    public static List<Review> Reviews(int restaurantID) {
        SqliteConnection db_conn = CreateConn();

        if (db_conn is not null) {
            List<Review> results = [];

            SqliteDataReader sqlite_datareader;
            SqliteCommand sqlite_cmd;
            sqlite_cmd = db_conn.CreateCommand();
            sqlite_cmd.CommandText = $"SELECT * FROM Review WHERE RestaurantID = {restaurantID}";

            sqlite_datareader = sqlite_cmd.ExecuteReader();
            while (sqlite_datareader.Read())
            {
                int ReviewID = sqlite_datareader.GetInt32(0);
                int ReviewRestaurantID = sqlite_datareader.GetInt32(1);
                int ReviewUserID = sqlite_datareader.GetInt32(2);
                int ReviewReservationID = sqlite_datareader.GetInt32(3);
                int ReviewRating = sqlite_datareader.GetInt32(4);
                string ReviewComment = sqlite_datareader.GetString(5);

                results.Add(new Review(ReviewID, ReviewRestaurantID, ReviewUserID, ReviewReservationID, ReviewRating, ReviewComment));
            }
            
            CloseConn(db_conn);
            return results;
        }
        return null;
    }

    public static List<Review> Reviews(int restaurantID, Accounts account) {
        SqliteConnection db_conn = CreateConn();

        if (db_conn is not null) {
            List<Review> results = [];

            SqliteDataReader sqlite_datareader;
            SqliteCommand sqlite_cmd;
            sqlite_cmd = db_conn.CreateCommand();
            sqlite_cmd.CommandText = $"SELECT * FROM Review WHERE RestaurantID = {restaurantID} AND UserID = {account.ID}";

            sqlite_datareader = sqlite_cmd.ExecuteReader();
            while (sqlite_datareader.Read())
            {
                int ReviewID = sqlite_datareader.GetInt32(0);
                int ReviewRestaurantID = sqlite_datareader.GetInt32(1);
                int ReviewUserID = sqlite_datareader.GetInt32(2);
                int ReviewReservationID = sqlite_datareader.GetInt32(3);
                int ReviewRating = sqlite_datareader.GetInt32(4);
                string ReviewComment = sqlite_datareader.GetString(5);

                results.Add(new Review(ReviewID, ReviewRestaurantID, ReviewUserID, ReviewReservationID, ReviewRating, ReviewComment));
            }
            
            CloseConn(db_conn);
            return results;
        }
        return null;
    }

    public static List<Review> Reviews (Accounts account) {
        SqliteConnection db_conn = CreateConn();

        if (db_conn is not null) {
            List<Review> results = [];

            SqliteDataReader sqlite_datareader;
            SqliteCommand sqlite_cmd;
            sqlite_cmd = db_conn.CreateCommand();
            sqlite_cmd.CommandText = $"SELECT * FROM Review WHERE UserID = {account.ID}";

            sqlite_datareader = sqlite_cmd.ExecuteReader();
            while (sqlite_datareader.Read())
            {
                int ReviewID = sqlite_datareader.GetInt32(0);
                int ReviewRestaurantID = sqlite_datareader.GetInt32(1);
                int ReviewUserID = sqlite_datareader.GetInt32(2);
                int ReviewReservationID = sqlite_datareader.GetInt32(3);
                int ReviewRating = sqlite_datareader.GetInt32(4);
                string ReviewComment = sqlite_datareader.GetString(5);

                results.Add(new Review(ReviewID, ReviewRestaurantID, ReviewUserID, ReviewReservationID, ReviewRating, ReviewComment));
            }
            
            CloseConn(db_conn);
            return results;
        }
        return null;
    }

    public static bool UpdatePasswordForAccount(Accounts account, string oldPassword, string newPassword) {
        if (account is not null && oldPassword is not null && newPassword is not null) {
            //Creating a connection to the database
            SqliteConnection db_conn = CreateConn();

            SqliteCommand sqlite_cmd;
            sqlite_cmd = db_conn.CreateCommand();
            sqlite_cmd.CommandText = $"UPDATE Accounts SET Password = \"{newPassword}\" WHERE ID = {account.ID};";
            sqlite_cmd.ExecuteNonQuery();

            //Close the connection to the database
            CloseConn(db_conn);

            return true;
        }
        return false;
    }

    public static bool UpdateFirstNameForAccount(Accounts account, string firstname) {
        if (account is not null && firstname is not null && firstname != "") {
            //Creating a connection to the database
            SqliteConnection db_conn = CreateConn();

            SqliteCommand sqlite_cmd;
            sqlite_cmd = db_conn.CreateCommand();
            sqlite_cmd.CommandText = $"UPDATE Accounts SET FirstName = {firstname} WHERE ID = {account.ID};";
            sqlite_cmd.ExecuteNonQuery();

            //Close the connection to the database
            CloseConn(db_conn);

            return true;
        }
        return false;
    }

    public static bool UpdateLastNameForAccount(Accounts account, string lastname) {
        if (account is not null && lastname is not null && lastname != "") {
            //Creating a connection to the database
            SqliteConnection db_conn = CreateConn();

            SqliteCommand sqlite_cmd;
            sqlite_cmd = db_conn.CreateCommand();
            sqlite_cmd.CommandText = $"UPDATE Accounts SET LastName = {lastname} WHERE ID = {account.ID};";
            sqlite_cmd.ExecuteNonQuery();

            //Close the connection to the database
            CloseConn(db_conn);

            return true;
        }
        return false;
    }

    public static bool UpdatePhoneNumberForAccount(Accounts account, string phonenumber) {
        if (account is not null && phonenumber is not null && phonenumber != "") {
            //Creating a connection to the database
            SqliteConnection db_conn = CreateConn();

            SqliteCommand sqlite_cmd;
            sqlite_cmd = db_conn.CreateCommand();
            sqlite_cmd.CommandText = $"UPDATE Accounts SET PhoneNumber = {phonenumber} WHERE ID = {account.ID};";
            sqlite_cmd.ExecuteNonQuery();

            //Close the connection to the database
            CloseConn(db_conn);

            return true;
        }
        return false;
    }

    public static bool UpdateAccountLevelForAccount(Accounts account, AccountLevel accountLevel, Accounts adminAccount) {
        if (account is not null && accountLevel is not null && adminAccount is not null) {
            //Creating a connection to the database
            SqliteConnection db_conn = CreateConn();

            SqliteCommand sqlite_cmd;
            sqlite_cmd = db_conn.CreateCommand();
            sqlite_cmd.CommandText = $"UPDATE Accounts SET AccountLevel = {accountLevel.ID} WHERE ID = {account.ID};";
            sqlite_cmd.ExecuteNonQuery();

            //Close the connection to the database
            CloseConn(db_conn);

            return true;
        }
        return false;
    }

    public static bool UpdateLanguagePreference(Accounts account, int selectedOption)
    {
        if (account is not null && selectedOption == 0)
        {
            //Creating a connection to the database
            SqliteConnection db_conn = CreateConn();

            SqliteCommand sqlite_cmd;
            sqlite_cmd = db_conn.CreateCommand();
            if (selectedOption == 1)
            {
                sqlite_cmd.CommandText = $"UPDATE Accounts SET Language = \"NL\" WHERE ID = {account.ID};";
            }
            else
            {
                sqlite_cmd.CommandText = $"UPDATE Accounts SET Language = \"EN\" WHERE ID = {account.ID};";
            }
            sqlite_cmd.ExecuteNonQuery();

            //Close the connection to the database
            CloseConn(db_conn);

            return true;
        }
        return false;
    }

    public static bool UpdateMenuItem(int restaurantID, string choice, string itemname, string input) {
        if (choice is not null && itemname is not null && input is not null) {
            //Creating a connection to the database
            SqliteConnection db_conn = CreateConn();

            SqliteCommand sqlite_cmd;
            sqlite_cmd = db_conn.CreateCommand();
            switch (choice) {
                case "name":
                    sqlite_cmd.CommandText = $"UPDATE Menu SET Name = \"{input}\" WHERE Name = \"{itemname}\";";
                    //DEBUG
                    // Console.WriteLine($"name: UPDATE Menu SET Name = \"{input}\" WHERE Name = \"{itemname}\";");
                    break;
                case "description":
                    sqlite_cmd.CommandText = $"UPDATE Menu SET Description = \"{input}\" WHERE Name = \"{itemname}\";";
                    //DEBUG
                    // Console.WriteLine($"description: UPDATE Menu SET Description = \"{input}\" WHERE Name = \"{itemname}\";");
                    break;
                case "price":
                    sqlite_cmd.CommandText = $"UPDATE Menu SET Price = {input} WHERE Name = \"{itemname}\";";
                    //DEBUG
                    // Console.WriteLine($"price: UPDATE Menu SET Price = {input} WHERE Name = \"{itemname}\";");
                    break;
                case "foodtype":
                    sqlite_cmd.CommandText = $"UPDATE Menu SET FoodType = {input} WHERE Name = \"{itemname}\";";
                    //DEBUG
                    // Console.WriteLine($"foodtype: UPDATE Menu SET FoodType = {input} WHERE Name = \"{itemname}\";");
                    break;
            }
            sqlite_cmd.ExecuteNonQuery();

            //Close the connection to the database
            CloseConn(db_conn);

            return true;
        }
        return false;
    }

    public static bool UpdateReservationStatus(int TimeSlotID, int StatusCode) {
        if (TimeSlotID != 0 && StatusCode == 0) {
            //Creating a connection to the database
            SqliteConnection db_conn = CreateConn();

            SqliteCommand sqlite_cmd;
            sqlite_cmd = db_conn.CreateCommand();
            sqlite_cmd.CommandText = $"UPDATE ReservationTimeSlots SET Status = {StatusCode} WHERE TimeSlotID = {TimeSlotID};";
            sqlite_cmd.ExecuteNonQuery();

            //Close the connection to the database
            CloseConn(db_conn);

            return true;
        }
        return false;
    }

    public static bool ConfirmInformationShare(Accounts ShareToAccount, Accounts ShareFromAccount) {
        if (ShareToAccount is not null && ShareFromAccount is not null) {
            //Creating a connection to the database
            SqliteConnection db_conn = CreateConn();

            SqliteCommand sqlite_cmd;
            sqlite_cmd = db_conn.CreateCommand();
            sqlite_cmd.CommandText = $"UPDATE Accounts SET Confirmation = 1 WHERE AccountID_One = {ShareFromAccount.ID} AND AccountID_Two = {ShareToAccount.ID};";
            sqlite_cmd.ExecuteNonQuery();

            //Close the connection to the database
            CloseConn(db_conn);

            return true;
        }
        return false;
    }

    public static string DeleteAccount(int accountID) {
        if (accountID != 0 | accountID != 1) {
            //Creating a connection to the database
            SqliteConnection db_conn = CreateConn();

            SqliteCommand sqlite_cmd;
            sqlite_cmd = db_conn.CreateCommand();
            sqlite_cmd.CommandText = $"DELETE FROM Accounts WHERE ID = {accountID};";
            sqlite_cmd.ExecuteNonQuery();

            //Close the connection to the database
            CloseConn(db_conn);

            return "Account has been succesfully removed from system";
        } else if (accountID != 1) {
            return "Can't Remove the main superadmin account";
        }
        return "invalid input";
    }

    public static bool DeleteTimeSlot(int timeslotID) {
        //Creating a connection to the database
        SqliteConnection db_conn = CreateConn();

        SqliteCommand sqlite_cmd;
        sqlite_cmd = db_conn.CreateCommand();
        sqlite_cmd.CommandText = $"DELETE FROM ReservationTimeSlots WHERE ID = {timeslotID};";
        sqlite_cmd.ExecuteNonQuery();

        //Close the connection to the database
        CloseConn(db_conn);

        return true;
    }

    public static bool DeleteTimeSlot(string timeslotID) {
        //Creating a connection to the database
        SqliteConnection db_conn = CreateConn();

        SqliteCommand sqlite_cmd;
        sqlite_cmd = db_conn.CreateCommand();
        sqlite_cmd.CommandText = $"DELETE FROM ReservationTimeSlots WHERE ID = {timeslotID};";
        sqlite_cmd.ExecuteNonQuery();

        //Close the connection to the database
        CloseConn(db_conn);

        return true;
    }

    public static bool DeleteMenuItem(int restaurantID, string name) {
        //Creating a connection to the database
        SqliteConnection db_conn = CreateConn();

        SqliteCommand sqlite_cmd;
        sqlite_cmd = db_conn.CreateCommand();
        sqlite_cmd.CommandText = $"DELETE FROM Menu WHERE RestaurantID = {restaurantID} AND Name = {name};";
        sqlite_cmd.ExecuteNonQuery();

        //Close the connection to the database
        CloseConn(db_conn);

        return true;
    }

    public static bool DeleteMenuItem(int restaurantID, int ID) {
        //Creating a connection to the database
        SqliteConnection db_conn = CreateConn();

        SqliteCommand sqlite_cmd;
        sqlite_cmd = db_conn.CreateCommand();
        sqlite_cmd.CommandText = $"DELETE FROM Menu WHERE RestaurantID = {restaurantID} AND ID = {ID};";
        sqlite_cmd.ExecuteNonQuery();

        //Close the connection to the database
        CloseConn(db_conn);

        return true;
    }

    public static bool DeleteReview(string ReviewID) {
        //Creating a connection to the database
        SqliteConnection db_conn = CreateConn();

        SqliteCommand sqlite_cmd;
        sqlite_cmd = db_conn.CreateCommand();
        sqlite_cmd.CommandText = $"DELETE FROM Review WHERE ID = {ReviewID};";
        sqlite_cmd.ExecuteNonQuery();

        //Close the connection to the database
        CloseConn(db_conn);

        return true;
    }

    public static bool DeleteReview(int ReviewID) {
        //Creating a connection to the database
        SqliteConnection db_conn = CreateConn();

        SqliteCommand sqlite_cmd;
        sqlite_cmd = db_conn.CreateCommand();
        sqlite_cmd.CommandText = $"DELETE FROM Review WHERE ID = {ReviewID};";
        sqlite_cmd.ExecuteNonQuery();

        //Close the connection to the database
        CloseConn(db_conn);

        return true;
    }

    public static int Temp_GetLastID(string table) {
        SqliteConnection db_conn = CreateConn();

        if (db_conn is not null) {
            List<ReservationTimeSlots> results = [];

            SqliteDataReader sqlite_datareader;
            SqliteCommand sqlite_cmd;
            sqlite_cmd = db_conn.CreateCommand();
            sqlite_cmd.CommandText = $"select seq from sqlite_sequence where name='{table}'";

            sqlite_datareader = sqlite_cmd.ExecuteReader();
            while (sqlite_datareader.Read())
            {
                int ID = sqlite_datareader.GetInt32(0);
                CloseConn(db_conn);

                return ID;
            }
        }
        return 0;
    }

}