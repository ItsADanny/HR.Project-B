public static class ReservationDisplay {
    public static void DisplayForRestaurant(int restaurantID) {
        PrintHeader();
        Console.WriteLine(ReservationLogic.RetrieveReservations(restaurantID));
        Console.WriteLine("====================================================================\n\n");
        Console.WriteLine("Press ENTER to exit");
        Console.ReadLine();
    }

    public static void DisplayCreateReservation(int restaurantID) {
        PrintHeader();
        Console.WriteLine("Please input following information requests to create an reservation.\n");
        string firstName = Functions.RequestValidString("First name");
        string lastName = Functions.RequestValidString("Last name");
        string email = Functions.RequestValidEmail();
        string phoneNumber = Functions.RequestValidEmail();
        string date = Functions.RequestValidDate();
        string startTime = Functions.RequestValidTime("Start time");
        string endTime = Functions.RequestValidTime("End time");
        int Table = Functions.RequestValidInt("Table number:");
        Console.WriteLine("====================================================================\n\n");
        Console.WriteLine("Creating reservation");
        if(ReservationLogic.CreateReservation(restaurantID, firstName, lastName, email, phoneNumber, date, startTime, endTime, Table)) {
            Console.WriteLine("Reservation created succesfully\n\n");
        } else {
            Console.WriteLine("There was an error while trying to create the reservation, please try it again later.\n\n");
        }
        Console.WriteLine("====================================================================");
    }

    private static void PrintHeader() {
        Console.WriteLine("====================================================================");
        Console.WriteLine("▗▄▄▖ ▗▄▄▄▖ ▗▄▄▖▗▄▄▄▖▗▄▄▖ ▗▖  ▗▖ ▗▄▖▗▄▄▄▖▗▄▄▄▖ ▗▄▖ ▗▖  ▗▖ ▗▄▄▖");
        Console.WriteLine("▐▌ ▐▌▐▌   ▐▌   ▐▌   ▐▌ ▐▌▐▌  ▐▌▐▌ ▐▌ █    █  ▐▌ ▐▌▐▛▚▖▐▌▐▌   ");
        Console.WriteLine("▐▛▀▚▖▐▛▀▀▘ ▝▀▚▖▐▛▀▀▘▐▛▀▚▖▐▌  ▐▌▐▛▀▜▌ █    █  ▐▌ ▐▌▐▌ ▝▜▌ ▝▀▚▖");
        Console.WriteLine("▐▌ ▐▌▐▙▄▄▖▗▄▄▞▘▐▙▄▄▖▐▌ ▐▌ ▝▚▞▘ ▐▌ ▐▌ █  ▗▄█▄▖▝▚▄▞▘▐▌  ▐▌▗▄▄▞▘");
        Console.WriteLine("====================================================================");
    }
}