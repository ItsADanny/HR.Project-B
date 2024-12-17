using System.Data;
using System.Globalization;

public class ReservationTimeSlots : IDBRestaurantClass {
    public int ID {get;}
    public int RestaurantID {get;}
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

    public override string ToString() {
        string Date = StartDateTime.ToString("dd/MM/yyyy");
        string startTime = StartDateTime.ToString("HH:mm");
        string endTime = EndDateTime.ToString("HH:mm");
        return $"Reservation Timeslot {Date} | {startTime} - {endTime}";
    }

    public static List<string> ConvertToString(DateTime datetime) {
        List<string> returnValue = [];
        //Time
        returnValue.Add(datetime.ToString("HH:mm"));
        //Date
        returnValue.Add(datetime.ToString("dd/MM/yyyy"));
        return returnValue;
    }

    public static DateTime ConvertToDateTime(string time, string date) {
        return DateTime.ParseExact($"{date} {time}", "dd/MM/yyyy HH:mm", System.Globalization.CultureInfo.InvariantCulture);
    }

    public string GetStartTime() => StartDateTime.ToString("hh:mm");

    public string GetEndTime() => EndDateTime.ToString("hh:mm");

    public string GetStartTime24() => StartDateTime.ToString("HH:mm");

    public string GetEndTime24() => EndDateTime.ToString("HH:mm"); 

    public string GetDate() => StartDateTime.ToString("dd/MM/yyyy");
}