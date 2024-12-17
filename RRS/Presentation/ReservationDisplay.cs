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

    private static string header = "====================================================================\n▗▄▄▖ ▗▄▄▄▖ ▗▄▄▖▗▄▄▄▖▗▄▄▖ ▗▖  ▗▖ ▗▄▖▗▄▄▄▖▗▄▄▄▖ ▗▄▖ ▗▖  ▗▖ ▗▄▄▖\n▐▌ ▐▌▐▌   ▐▌   ▐▌   ▐▌ ▐▌▐▌  ▐▌▐▌ ▐▌ █    █  ▐▌ ▐▌▐▛▚▖▐▌▐▌   \n▐▛▀▚▖▐▛▀▀▘ ▝▀▚▖▐▛▀▀▘▐▛▀▚▖▐▌  ▐▌▐▛▀▜▌ █    █  ▐▌ ▐▌▐▌ ▝▜▌ ▝▀▚▖\n▐▌ ▐▌▐▙▄▄▖▗▄▄▞▘▐▙▄▄▖▐▌ ▐▌ ▝▚▞▘ ▐▌ ▐▌ █  ▗▄█▄▖▝▚▄▞▘▐▌  ▐▌▗▄▄▞▘\n====================================================================\n\n";

    public static void ReservationMenu_Customer(int restaurantID, Accounts LoggedInAccount) {
        Console.Clear();
        Console.WriteLine(header);
    }

    public static void ReservationMenu_Admin (int restaurantID, Accounts LoggedInAccount) {
        bool CanChangeReservations = AccountLogic.CanDisplay("cancelReservations", LoggedInAccount);
        List<string> options = ["Make a reservation for a customer", "View reservations"];
        if (CanChangeReservations) {
            options.Add("Cancel reservations");
        }
        options.Add("Exit");
        
        bool done = false;

        while (!done) {
            switch (Functions.OptionSelector(header, options)) {
                case 0:
                    //TODO IMPLEMENT THIS FEATURE FOR THE ADMIN
                    break;
                case 1:
                    DisplayForRestaurant(restaurantID);
                    break;
                case 2:
                    if (CanChangeReservations) {
                        //TODO IMPLEMENT THIS FEATURE FOR THE ADMIN WITHOUT DELETING TIMESLOTS (WHICH IS WHAT WE CURRENTLY HAVE AS THE ONLY WAY TO CANCEL RESERVATIONS)
                    } else {
                        done = true;
                    }
                    break;
                case 3:
                    done = true;
                    break;
            }
        }
    }

    public static void DisplayForRestaurant(int restaurantID) 
    {
        Console.Clear();
        Console.WriteLine(header);
        Console.WriteLine(ReservationLogic.RetrieveReservations(restaurantID));
        Console.WriteLine("====================================================================\n\n");
        Console.WriteLine("Press ENTER to exit");
        Console.ReadLine();
    }

    public static void DisplayForRestaurantCustomer(int restaurantID, Accounts LoggedInAccount) {
        Console.Clear();
        Console.WriteLine(header);
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
        Console.WriteLine(header);
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

    public static void PrintFloorPlan() {
        Console.Clear();
        Console.WriteLine(header);
        Console.WriteLine(RestaurantFloorPlan);
        Console.WriteLine("=============================================================================================================\nPress ENTER to exit");
        Console.ReadLine();
    }
}