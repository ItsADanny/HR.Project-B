public static class ReservationLogic {
    public static string RetrieveReservations(int restaurantID) 
    {
        string returnResult = "";
        int i = 0;

        //grab reserv list
        List<Reservations> reservations = Database.SelectReservations(restaurantID);
        if (reservations.Count > 0) {
            foreach (Reservations reservation in reservations) 
            {
                i++;
                //grab reservation timeslot
                ReservationTimeSlots reservationTimeSlot = Database.SelectReservationTimeSlot(reservation.TimeSlotID);
                if (reservationTimeSlot.EndDateTime >= DateTime.Now) {
                    //retrieve acc info
                    Accounts accounts = Database.SelectAccount(reservation.AccountID);

                    string reservationStatus = "";
                    
                    //check actual status
                    if (reservation.Status == 1)
                    {
                        reservationStatus = "CANCELLED";
                    }
                    
                    //add enter to every line
                    if (returnResult != "") 
                    {
                        returnResult += "\n";
                    }

                    //format return
                    returnResult += $"Reservation for: {accounts.FirstName} {accounts.LastName}, Table: {reservation.TableID}, Date: {reservationTimeSlot.GetDate()}, from {reservationTimeSlot.GetStartTime24()} to {reservationTimeSlot.GetEndTime24()}";
                }
            }
        } else {
            returnResult = "No reservations found";
        }
        
        return returnResult;
    }

    public static string RetrieveCurrentReservations(int restaurantID) 
    {
        string returnResult = "";
        int i = 0;

        //grab reserv list
        List<Reservations> reservations = Database.SelectReservations(restaurantID);
        if (reservations.Count > 0) {
            foreach (Reservations reservation in reservations) 
            {
                i++;
                //grab reservation timeslot
                ReservationTimeSlots reservationTimeSlot = Database.SelectReservationTimeSlot(reservation.TimeSlotID);
                if (reservationTimeSlot.StartDateTime <= DateTime.Now & reservationTimeSlot.EndDateTime >= DateTime.Now) {
                    //retrieve acc info
                    Accounts accounts = Database.SelectAccount(reservation.AccountID);

                    string reservationStatus = "";
                    
                    //check actual status
                    if (reservation.Status == 1)
                    {
                        reservationStatus = "CANCELLED";
                    }
                    
                    //add enter to every line
                    if (returnResult != "") 
                    {
                        returnResult += "\n";
                    }

                    //format return
                    returnResult += $"Reservation for: {accounts.FirstName} {accounts.LastName}, Table: {reservation.TableID}, Date: {reservationTimeSlot.GetDate()}, from {reservationTimeSlot.GetStartTime24()} to {reservationTimeSlot.GetEndTime24()}";
                }
            }
        } else {
            returnResult = "No reservations found";
        }
        
        return returnResult;
    }

    public static List<Reservations> RetrievePreviousReservations(int restaurantID, int AccountID) 
    {
        List<Reservations> returnResult = [];
        int i = 0;

        //grab reserv list
        List<Reservations> reservations = Database.SelectReservations(restaurantID, AccountID);
        if (reservations.Count > 0) {
            foreach (Reservations reservation in reservations) 
            {
                i++;
                //grab reservation timeslot
                ReservationTimeSlots reservationTimeSlot = Database.SelectReservationTimeSlot(reservation.TimeSlotID);
                if (reservationTimeSlot.StartDateTime <= DateTime.Now) {
                    returnResult.Add(reservation);
                }
            }
        }
        
        return returnResult;
    }

    public static string RetrieveReservations(int restaurantID, int AccountID) {
        string returnResult = "";
        int i = 0;

        //grab reserv list
        List<Reservations> reservations = Database.SelectReservations(restaurantID, AccountID);

        foreach (Reservations reservation in reservations) 
        {
            i++;
            //grab reservation timeslot
            ReservationTimeSlots reservationTimeSlot = Database.SelectReservationTimeSlot(reservation.TimeSlotID);
            if (reservationTimeSlot.EndDateTime >= DateTime.Now) {
                //retrieve acc info
                Accounts accounts = Database.SelectAccount(reservation.AccountID);

                string reservationStatus = "";
                
                //check actual status
                if (reservation.Status == 1) 
                {
                    reservationStatus = "CANCELLED";
                }
                
                //add enter to every line
                if (returnResult != "") 
                {
                    returnResult += "\n";
                }
                //format return
                returnResult += $"{i} - Reservation: {reservation.ID}, Table: {reservation.TableID}, Date: {reservationTimeSlot.GetDate()}, from {reservationTimeSlot.GetStartTime24()} to {reservationTimeSlot.GetEndTime24()}";
            }
        }
        return returnResult;
    }

    public static List<Reservations> RetrieveReservations(int restaurantID, Reservations reservations) => Database.SelectReservations(restaurantID, reservations);

    public static bool CreateReservation(int restaurantID, int TimeSlotID, int AccountID, int table) {
        return Database.Insert(new Reservations(restaurantID, TimeSlotID, table, AccountID, 0));
    }

    public static bool CancelReservation(int timeslotID, Accounts LoggedAccount) {
        //1 defines that a reservation has been cancelled
        return Database.UpdateReservationStatus(timeslotID, 1);
    }

    public static List<string> ConvertToDisplayString(List<Reservations> reservations) {
        List<string> returnValue = [];
        foreach (Reservations reservation in reservations) {
            ReservationTimeSlots reservationTimeSlot = Database.SelectReservationTimeSlot(reservation.TimeSlotID);
            returnValue.Add($"Reservation: {reservation.ID}, Table: {reservation.TableID}, Date: {reservationTimeSlot.GetDate()}, from {reservationTimeSlot.GetStartTime24()} to {reservationTimeSlot.GetEndTime24()}");
        }
        return returnValue;
    }
}