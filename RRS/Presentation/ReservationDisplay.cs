public static class ReservationDisplay {

    private static string RestaurantFloorPlan = @"
        =============================================================================================
        |                                                    |                                      |
        |   ===============      ========      ========      |                KITCHEN               |
        |   |      T      |      |      |      |      |      |                                      |
        |   |      1      |      |      |      |      |      |                                      |
        |   ===============      |      |      |      |      =======================================|
        |                        |  T4  |      |  T5  |                                             |
        |   ===============      |      |      |      |             =============================   |
        |   |      T      |      |      |      |      |             |           TABLE           |   |
        |   |      2      |      |      |      |      |             |             8             |   |
        |   ===============      ========      ========             =============================   |
        |                                                                                           |
        |   ===============      ========      ========             ========      ===============   |
        |   |      T      |      |  T6  |      |  T7  |             |  T9  |      |     T10     |   |
        |   |      3      |      |      |      |      |             |      |      |             |   |
        |   ===============      ========      ========             ========      ===============   |
        |                                                                                           |
        =============================================================================================
        ";

    public static void ReservationMenu_Customer(int restaurantID, Accounts LoggedInAccount) {
        Console.Clear();
        PrintHeader();
    }

    public static void ReservationMenu_Admin (int restaurantID, Accounts LoggedInAccount) {
        bool CanChangeReservations = AccountLogic.CanDisplay("cancelReservations", LoggedInAccount);
        // THIS NEEDS TO BE IMPLEMENTED
        // bool CanCancelReservations = AccountLogic.CanDisplay_CancelReservations(LoggedInAccount);
        while (true) {
            Console.Clear();
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
        Console.Clear();
        PrintHeader();
        Console.WriteLine(ReservationLogic.RetrieveReservations(restaurantID));
        Console.WriteLine("====================================================================\n\n");
        Console.WriteLine("Press ENTER to exit");
        Console.ReadLine();
    }

    public static void DisplayForRestaurantCustomer(int restaurantID, Accounts LoggedInAccount) {
        Console.Clear();
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
        Thread.Sleep(1000);
        // string TimeSlotID = TimeSlotLogic.GetIDFromDisplayString()
        int timeslotID = TimeSlotLogic.GetIDFromDisplayString(Functions.OptionSelector("Select a timeslot:\n\n", TimeSlotLogic.ToDisplayString(TimeSlotLogic.FilterUpcoming(TimeSlotLogic.GetTimeSlots(restaurantID), false)), true), restaurantID);
        // string TimeSlotID = TimeSlotLogic.GetSelectedTimeSlot_Reservation();
        int Table = Functions.RequestValidInt("Table number");
        Console.WriteLine("====================================================================\n\n");
        Console.WriteLine("Creating reservation");
        if(ReservationLogic.CreateReservation(restaurantID, timeslotID, LoggedInAccount.ID, Table)) {
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

    public static void PrintFloorPlan() {
        Console.Clear();
        Console.WriteLine("=============================================================================================================");
        
        Console.WriteLine("=============================================================================================================");
        Console.WriteLine(RestaurantFloorPlan);
        Console.WriteLine("=============================================================================================================\nPress ENTER to exit");
        Console.ReadLine();
    }
}