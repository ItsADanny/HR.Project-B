using System.Globalization;

public class ReservationTimeSlots {
    public int ID;
    public int RestaurantID;
    public DateTime StartDateTime;
    public DateTime EndDateTime;

    public ReservationTimeSlots(int restaurantID, string date, string startTime, string endTime) {
        ID = 999999999;
        RestaurantID = restaurantID;
        StartDateTime = ConvertToDateTime(startTime, date);
        EndDateTime = ConvertToDateTime(endTime, date);
    }

    public ReservationTimeSlots(int timeslotID, int restaurantID, string date, string startTime, string endTime) {
        ID = timeslotID;
        RestaurantID = restaurantID;
        StartDateTime = ConvertToDateTime(startTime, date);
        EndDateTime = ConvertToDateTime(endTime, date);
    }

    public ReservationTimeSlots(int restaurantID, DateTime startDateTime, DateTime endDateTime) {
        ID = 999999999;
        RestaurantID = restaurantID;
        StartDateTime = startDateTime;
        EndDateTime = endDateTime;
    }

    public ReservationTimeSlots(int timeslotID, int restaurantID, DateTime startDateTime, DateTime endDateTime) {
        ID = timeslotID;
        RestaurantID = restaurantID;
        StartDateTime = startDateTime;
        EndDateTime = endDateTime;
    }

    public override string ToString()
    {
        string Date = StartDateTime.ToString("dd/MM/yyyy");
        string startTime = StartDateTime.ToString("hh:mm");
        string endTime = EndDateTime.ToString("hh:mm");
        return $"Reservation Timeslot {Date} | {startTime} - {endTime}";
    }

    public static List<string> ConvertToString(DateTime datetime) {
        List<string> returnValue = [];
        //Time
        returnValue.Add(datetime.ToString("hh:mm"));
        //Date
        returnValue.Add(datetime.ToString("dd/MM/yyyy"));
        return returnValue;
    }

    public static DateTime ConvertToDateTime(string time, string date) {
        return DateTime.Parse($"{date} {time}", new CultureInfo("fr-FR", false));
    }

    public string GetStartTime() => StartDateTime.ToString("hh:mm");

    public string GetEndTime() => EndDateTime.ToString("hh:mm");

    public string GetDate() => StartDateTime.ToString("dd/MM/yyyy");
}