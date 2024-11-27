public class AccountLevel {
    public int ID;
    public string Name;
    public bool CanChangeReservation;
    public bool CanChangeTimeSlots;
    public bool CanCancelReservations;
    public bool IsAnAdmin;
    public bool CanCreateAdmins;
    public bool CanViewLogs;

    public AccountLevel(string name, int canChangeReservation, int canChangeTimeSlots, int canCancelReservations, 
                        int isAnAdmin, int canCreateAdmins, int canViewLogs) {
        ID = 999999999;
        Name = name;
        CanChangeReservation = ConvertToBool(canChangeReservation);
        CanChangeTimeSlots = ConvertToBool(canChangeTimeSlots);
        CanCancelReservations = ConvertToBool(canCancelReservations);
        IsAnAdmin = ConvertToBool(isAnAdmin);
        CanCreateAdmins = ConvertToBool(canCreateAdmins);
        CanViewLogs = ConvertToBool(canViewLogs);
    }

    public AccountLevel(string name, bool canChangeReservation, bool canChangeTimeSlots, bool canCancelReservations, 
                        bool isAnAdmin, bool canCreateAdmins, bool canViewLogs) {
        ID = 999999999;
        Name = name;
        CanChangeReservation = canChangeReservation;
        CanChangeTimeSlots = canChangeTimeSlots;
        CanCancelReservations = canCancelReservations;
        IsAnAdmin = isAnAdmin;
        CanCreateAdmins = canCreateAdmins;
        CanViewLogs = canViewLogs;
    }

    public AccountLevel(int id, string name, int canChangeReservation, int canChangeTimeSlots, int canCancelReservations, 
                        int isAnAdmin, int canCreateAdmins, int canViewLogs) {
        ID = id;
        Name = name;
        CanChangeReservation = ConvertToBool(canChangeReservation);
        CanChangeTimeSlots = ConvertToBool(canChangeTimeSlots);
        CanCancelReservations = ConvertToBool(canCancelReservations);
        IsAnAdmin = ConvertToBool(isAnAdmin);
        CanCreateAdmins = ConvertToBool(canCreateAdmins);
        CanViewLogs = ConvertToBool(canViewLogs);
    }

    public AccountLevel(int id, string name, bool canChangeReservation, bool canChangeTimeSlots, 
                        bool canCancelReservations, bool isAnAdmin, bool canCreateAdmins, bool canViewLogs) {
        ID = id;
        Name = name;
        CanChangeReservation = canChangeReservation;
        CanChangeTimeSlots = canChangeTimeSlots;
        CanCancelReservations = canCancelReservations;
        IsAnAdmin = isAnAdmin;
        CanCreateAdmins = canCreateAdmins;
        CanViewLogs = canViewLogs;
    }

    private bool ConvertToBool(int input) {
        if (input == 1) {
            return true;
        }
        return false;
    }

    private static int ConvertToInt(bool input) {
        if (input) {
            return 1;
        }
        return 0;
    }
}