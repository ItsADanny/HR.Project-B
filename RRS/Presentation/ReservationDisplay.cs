public static class ReservationDisplay {

    public static void ReservationMenu_Customer(int restaurantID, Accounts LoggedInAccount) {
        PrintHeader();
    }

    public static void ReservationMenu_Admin (int restaurantID, Accounts LoggedInAccount) {
        bool CanChangeReservations = AccountLogic.CanDisplay_ChangeReservations(LoggedInAccount);
        // THIS NEEDS TO BE IMPLEMENTED
        // bool CanCancelReservations = AccountLogic.CanDisplay_CancelReservations(LoggedInAccount);
        while (true) {
            PrintHeader();
            Console.WriteLine("1 - View reservations");
                if (CanChangeReservations) {
                    Console.WriteLine("2 - Cancel reservation");
                }
                Console.WriteLine("================================================================");
                Console.WriteLine("Please select an account to change (enter Q to exit):");
        }
    }

    public static void DisplayForRestaurant(int restaurantID) 
    {
        PrintHeader();
        Console.WriteLine(ReservationLogic.RetrieveReservations(restaurantID));
        Console.WriteLine("====================================================================\n\n");
        Console.WriteLine("Press ENTER to exit");
        Console.ReadLine();
    }

    public static void DisplayForRestaurantCustomer(int restaurantID, Accounts LoggedInAccount) {
        PrintHeader();
        Console.WriteLine(ReservationLogic.RetrieveReservations(restaurantID, LoggedInAccount.ID));
        Console.WriteLine("====================================================================\n\n");
        Console.WriteLine("Press ENTER to exit");
        Console.ReadLine();
    }

    // public static void DisplayCreateReservation(int restaurantID) {
    //     PrintHeader();
    //     Console.WriteLine("Please input following information requests to create an reservation.\n");
    //     string firstName = Functions.RequestValidString("First name");
    //     string lastName = Functions.RequestValidString("Last name");
    //     string email = Functions.RequestValidEmail();
    //     string phoneNumber = Functions.RequestValidEmail();
    //     string date = Functions.RequestValidDate();
    //     string startTime = Functions.RequestValidTime("Start time");
    //     string endTime = Functions.RequestValidTime("End time");
    //     int Table = Functions.RequestValidInt("Table number:");
    //     Console.WriteLine("====================================================================\n\n");
    //     Console.WriteLine("Creating reservation");
    //     if(ReservationLogic.CreateReservation(restaurantID, firstName, lastName, email, phoneNumber, date, startTime, endTime, Table)) {
    //         Console.WriteLine("Reservation created succesfully\n\n");
    //     } else {
    //         Console.WriteLine("There was an error while trying to create the reservation, please try it again later.\n\n");
    //     }
    //     Console.WriteLine("====================================================================");
    // }

    public static void DisplayCreateReservation_Customer(int restaurantID, Accounts LoggedInAccount) {
        PrintHeader();
        Console.WriteLine("Please input following information requests to create a reservation.\n");
        string TimeSlotID = TimeSlotLogic.GetSelectedTimeSlot_Reservation();
        int Table = Functions.RequestValidInt("Table number");
        Console.WriteLine("====================================================================\n\n");
        Console.WriteLine("Creating reservation");
        if(ReservationLogic.CreateReservation(restaurantID, Convert.ToInt32(TimeSlotID), LoggedInAccount.ID, Table)) {
            Console.WriteLine("Reservation created successfully\n\n");
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