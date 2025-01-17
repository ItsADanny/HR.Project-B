using System.Linq;

public static class ReservationDisplay {

    private static string header = "====================================================================\n▗▄▄▖ ▗▄▄▄▖ ▗▄▄▖▗▄▄▄▖▗▄▄▖ ▗▖  ▗▖ ▗▄▖▗▄▄▄▖▗▄▄▄▖ ▗▄▖ ▗▖  ▗▖ ▗▄▄▖\n▐▌ ▐▌▐▌   ▐▌   ▐▌   ▐▌ ▐▌▐▌  ▐▌▐▌ ▐▌ █    █  ▐▌ ▐▌▐▛▚▖▐▌▐▌   \n▐▛▀▚▖▐▛▀▀▘ ▝▀▚▖▐▛▀▀▘▐▛▀▚▖▐▌  ▐▌▐▛▀▜▌ █    █  ▐▌ ▐▌▐▌ ▝▜▌ ▝▀▚▖\n▐▌ ▐▌▐▙▄▄▖▗▄▄▞▘▐▙▄▄▖▐▌ ▐▌ ▝▚▞▘ ▐▌ ▐▌ █  ▗▄█▄▖▝▚▄▞▘▐▌  ▐▌▗▄▄▞▘\n====================================================================\n\n";

    public static void ReservationMenu_Customer(int restaurantID, Accounts LoggedInAccount) {
        Console.Clear();
        Console.WriteLine(header);
    }

    public static void ReservationMenu_Admin (int restaurantID, Accounts LoggedInAccount) {
        bool CanChangeReservations = AccountLogic.CanDisplay("cancelReservations", LoggedInAccount);
        List<string> options = ["View currently running reservations", "View all reservations"];
        if (CanChangeReservations) {
            options.Add("Cancel reservations");
        }
        options.Add("Exit");
        
        bool done = false;

        while (!done) {
            switch (Functions.OptionSelector(header, options)) {
                case 0:
                    DisplayCurrentForRestaurant(restaurantID);
                    break;
                case 1:
                    DisplayForRestaurant(restaurantID);
                    break;
                case 2:
                    if (CanChangeReservations) {
                        DisplayReservationToCancel(restaurantID);
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
                break;
        }
        Thread.Sleep(2000);
    }

    public static void PrintFloorPlan() {
        Console.Clear();
        Console.WriteLine(header);
        Console.WriteLine(RestuarantPlans.GetFloorPlan());
        Console.WriteLine("=============================================================================================================\nPress ENTER to exit");
        Console.ReadLine();
    }

    public static void DisplayReservationToCancel(int restaurantID) {
        List<Reservations> currReservation = ReservationLogic.RemoveCancelled(ReservationLogic.RetrieveCurrReservations(restaurantID));
        List<string> DisplayStrings = ReservationLogic.ConvertToDisplayString(currReservation);
        List<string> options = DisplayStrings;
        options.Add("Exit");

        bool done = false;

        while (!done) {
            int selected = Functions.OptionSelector(header, options);
            if (options.Count() != 1) {
                if (selected == currReservation.Count()) {
                    done = true;
                } else {
                    Reservations selectedReservation = currReservation[selected];
                    if (ReservationLogic.CancelReservation(selectedReservation)) {
                        Console.WriteLine($"Reservation {selectedReservation.ID} has been canceld");
                    } else {
                        Console.WriteLine($"There was an error when trying to cancel the reservation {selectedReservation.ID}, please try again later");
                    }
                    Thread.Sleep(2000);
                    done = true;
                }
            } else {
                done = true;
            }
        }
    }
}