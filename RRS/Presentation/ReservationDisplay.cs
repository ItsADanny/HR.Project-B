public static class ReservationDisplay {

    private static string header = "====================================================================\n▗▄▄▖ ▗▄▄▄▖ ▗▄▄▖▗▄▄▄▖▗▄▄▖ ▗▖  ▗▖ ▗▄▖▗▄▄▄▖▗▄▄▄▖ ▗▄▖ ▗▖  ▗▖ ▗▄▄▖\n▐▌ ▐▌▐▌   ▐▌   ▐▌   ▐▌ ▐▌▐▌  ▐▌▐▌ ▐▌ █    █  ▐▌ ▐▌▐▛▚▖▐▌▐▌   \n▐▛▀▚▖▐▛▀▀▘ ▝▀▚▖▐▛▀▀▘▐▛▀▚▖▐▌  ▐▌▐▛▀▜▌ █    █  ▐▌ ▐▌▐▌ ▝▜▌ ▝▀▚▖\n▐▌ ▐▌▐▙▄▄▖▗▄▄▞▘▐▙▄▄▖▐▌ ▐▌ ▝▚▞▘ ▐▌ ▐▌ █  ▗▄█▄▖▝▚▄▞▘▐▌  ▐▌▗▄▄▞▘\n====================================================================\n\n";

    public static void ReservationMenu_Customer(int restaurantID, Accounts LoggedInAccount) {
        Console.Clear();
        Console.WriteLine(header);
    }

    public static void ReservationMenu_Admin (int restaurantID, Accounts LoggedInAccount) {
        bool CanChangeReservations = AccountLogic.CanDisplay("cancelReservations", LoggedInAccount);
        List<string> options = ["Make a reservation for a customer", "View currently running reservations", "View all reservations"];
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
                    DisplayCurrentForRestaurant(restaurantID);
                    break;
                case 2:
                    DisplayForRestaurant(restaurantID);
                    break;
                case 3:
                    if (CanChangeReservations) {
                        //TODO IMPLEMENT THIS FEATURE FOR THE ADMIN WITHOUT DELETING TIMESLOTS (WHICH IS WHAT WE CURRENTLY HAVE AS THE ONLY WAY TO CANCEL RESERVATIONS)
                    } else {
                        done = true;
                    }
                    break;
                case 4:
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

    public static void DisplayCurrentForRestaurant(int restaurantID) {
        Console.Clear();
        Console.WriteLine(header);
        Console.WriteLine(ReservationLogic.RetrieveCurrentReservations(restaurantID));
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

    public static void DisplayCreateReservation_Customer(int restaurantID, Accounts LoggedInAccount) {
        string reservationCustomerHeader = header + "Please input following information requests to create a reservation.\n";
        ReservationTimeSlots SelectedTimeSlot = TimeSlotLogic.GetSelectedTimeSlot(TimeSlotLogic.GetIDFromDisplayString(Functions.OptionSelector(reservationCustomerHeader, TimeSlotLogic.ToDisplayString(TimeSlotLogic.FilterUpcoming(TimeSlotLogic.GetTimeSlots(restaurantID), false)), true), restaurantID));
        reservationCustomerHeader = reservationCustomerHeader + TimeSlotLogic.ToDisplayString(SelectedTimeSlot) + "\nDo you want to use matchmaking, for getting assigned to an random table?";
        switch (Functions.OptionSelector(reservationCustomerHeader, ["Yes", "No"])) {
            case 0:
                Table MatchMadeTable = TableLogic.matchMaking(SelectedTimeSlot, restaurantID);
                if(ReservationLogic.CreateReservation(restaurantID, SelectedTimeSlot.ID, LoggedInAccount.ID, MatchMadeTable.ID)) {
                    Console.WriteLine("Reservation created successfully\n\n");
                } else {
                    Console.WriteLine("There was an error while trying to create the reservation, please try it again later.\n\n");
                }
                break;
            case 1:
                List<string> ListOfTables = TableLogic.ToDisplayString(TableLogic.GetOpenTables(SelectedTimeSlot.ID, restaurantID), SelectedTimeSlot, restaurantID);
                int selectedTable = Functions.OptionSelector(header + RestuarantPlans.GetFloorPlan() + "\nPlease select an table you want to join:", ListOfTables);
                Table table = TableLogic.GetTableFromDisplayString(ListOfTables[selectedTable], SelectedTimeSlot, restaurantID);

                reservationCustomerHeader = reservationCustomerHeader + $"\nPlease select an table you want to join:\n{TableLogic.ToDisplayString(table, SelectedTimeSlot, restaurantID)}";
                Console.WriteLine(reservationCustomerHeader);

                if(ReservationLogic.CreateReservation(restaurantID, SelectedTimeSlot.ID, LoggedInAccount.ID, table.ID)) {
                    Console.WriteLine("Reservation created successfully\n\n");
                } else {
                    Console.WriteLine("There was an error while trying to create the reservation, please try it again later.\n\n");
                }
                break;
            default:
                Console.WriteLine("I am a massive idiot, but even an idiot is right twice -Danny");
                Thread.Sleep(5000);
                break;
        }
    }

    public static void PrintFloorPlan() {
        Console.Clear();
        Console.WriteLine(header);
        Console.WriteLine(RestuarantPlans.GetFloorPlan());
        Console.WriteLine("=============================================================================================================\nPress ENTER to exit");
        Console.ReadLine();
    }
}