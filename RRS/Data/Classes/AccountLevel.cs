public class AccountLevel {
    public int ID;
    public string Name;
    public bool CanChangeReservation;
    public bool CanChangeTimeSlots;
    public bool CanCancelReservations;
    public bool IsAnAdmin;
    public bool CanCreateAdmins;

    public AccountLevel(string name, int canChangeReservation, int canChangeTimeSlots, int canCancelReservations, 
                        int isAnAdmin, int canCreateAdmins) {
        ID = 999999999;
        Name = name;
        CanChangeReservation = ConvertToBool(canChangeReservation);
        CanChangeTimeSlots = ConvertToBool(canChangeTimeSlots);
        CanCancelReservations = ConvertToBool(canCancelReservations);
        IsAnAdmin = ConvertToBool(isAnAdmin);
        CanCreateAdmins = ConvertToBool(canCreateAdmins);
    }

    public AccountLevel(string name, bool canChangeReservation, bool canChangeTimeSlots, bool canCancelReservations, 
                        bool isAnAdmin, bool canCreateAdmins) {
        ID = 999999999;
        Name = name;
        CanChangeReservation = canChangeReservation;
        CanChangeTimeSlots = canChangeTimeSlots;
        CanCancelReservations = canCancelReservations;
        IsAnAdmin = isAnAdmin;
        CanCreateAdmins = canCreateAdmins;
    }

    public AccountLevel(int id, string name, int canChangeReservation, int canChangeTimeSlots, int canCancelReservations, 
                        int isAnAdmin, int canCreateAdmins) {
        ID = id;
        Name = name;
        CanChangeReservation = ConvertToBool(canChangeReservation);
        CanChangeTimeSlots = ConvertToBool(canChangeTimeSlots);
        CanCancelReservations = ConvertToBool(canCancelReservations);
        IsAnAdmin = ConvertToBool(isAnAdmin);
        CanCreateAdmins = ConvertToBool(canCreateAdmins);
    }

    public AccountLevel(int id, string name, bool canChangeReservation, bool canChangeTimeSlots, 
                        bool canCancelReservations, bool isAnAdmin, bool canCreateAdmins) {
        ID = id;
        Name = name;
        CanChangeReservation = canChangeReservation;
        CanChangeTimeSlots = canChangeTimeSlots;
        CanCancelReservations = canCancelReservations;
        IsAnAdmin = isAnAdmin;
        CanCreateAdmins = canCreateAdmins;
    }

    public bool ConvertToBool(int input) {
        if (input == 1) {
            return true;
        }
        return false;
    }

    public static int ConvertToInt(bool input) {
        if (input) {
            return 1;
        }
        return 0;
    }

    public override string ToString()
    {
        return $"{ID} - {Name}";
    }
}