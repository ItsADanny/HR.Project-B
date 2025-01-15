public static class TableLogic {
    public static string ToDisplayString(Table table, ReservationTimeSlots timeslot, int restaurantID) {
        int OpenPositions = Database.GetOpenPositions(table.ID, timeslot.ID, restaurantID);
        if (OpenPositions != 999999999) {
            if (OpenPositions >= 1) {
                return $"{table.Name}, open spots: {OpenPositions}";
            }
            return $"{table.Name}, No seats available";
        }
        throw new Exception("Open positions for table not found");
    }

    public static List<string> ToDisplayString(List<Table> ListOfTables, ReservationTimeSlots timeslot, int restaurantID) {
        List<string> returnValue = new ();
        foreach (Table table in ListOfTables) {
            returnValue.Add(ToDisplayString(table, timeslot, restaurantID));
        }
        return returnValue;
    }

    public static Table GetTableFromDisplayString(string displayString, ReservationTimeSlots timeslot, int restaurantID) {
        List<Table> tables = GetTable(restaurantID);

        foreach (Table table in tables) {
            if (ToDisplayString(table, timeslot, restaurantID) == displayString) {
                return table;
            }
        }

        return null;
    }

    public static List<Table> GetTable(int restaurantID) => Database.SelectTableForRestaurant(restaurantID);

    public static Table GetTable(int TableID, int restaurantID) => Database.SelectTable(TableID);

    public static List<Table> GetOpenTables(int timeslotID, int restaurantID) => Database.GetOpenTables(timeslotID, restaurantID); 

    public static Table randomTable(List<Table> tables) => tables[new Random().Next(1, tables.Count) - 1];

    public static List<Table> TableFilter(List<Table> tables, int SelectedTableOccupanceCount) {
        List<Table> returnValue = new ();
        foreach (Table table in tables) {
            if (table.MaxSize == SelectedTableOccupanceCount) {
                returnValue.Add(table);
            }
        }
        return returnValue;
    }

    public static Table matchMaking(ReservationTimeSlots timeslot, int restaurantID) {
        List<Table> tables = Database.GetOpenTables(timeslot.ID, restaurantID);
        return randomTable(TableFilter(tables, Functions.IntSelector("Select with how many you want to be at the table", 2, 8, step:2)));
    }
}