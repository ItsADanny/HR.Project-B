using System;

namespace RRS.Presentation;

public class AccountLevelDisplay
{
  public void CreateAccountLevelDisplay(){
    Console.WriteLine("Account Level Display");

    System.Console.WriteLine("Enter Level Name: ");
    string Name = Console.ReadLine();

    Console.WriteLine("Can change reservations? (1 for Yes, 0 for No): ");
    int CanChangeReservation = int.Parse(Console.ReadLine());

    System.Console.WriteLine("Can change time slots? (1 for Yes, 0 for No): ");
    int CanChangeTimeSlots = int.Parse(Console.ReadLine());

    System.Console.WriteLine("Can cancel reservations? (1 for Yes, 0 for No): ");
    int CanCancelReservations = int.Parse(Console.ReadLine());

    System.Console.WriteLine("Is this an admin account? (1 for Yes, 0 for No): ");
    int IsAnAdmin = int.Parse(Console.ReadLine());

    System.Console.WriteLine("Can create new admins? (1 for Yes, 0 for No): ");
    int CanCreateAdmins = int.Parse(Console.ReadLine());

    AccountLevel NewAccountLevel = new AccountLevel(Name, CanChangeReservation, CanChangeTimeSlots, CanCancelReservations, IsAnAdmin, CanCreateAdmins);
    Console.WriteLine($"Name:{Name}\nCan change reservations:{CanChangeReservation}\nCan change timeslots:{CanChangeTimeSlots}\nCan cancel reservations:{CanCancelReservations}\nIs admin:{IsAnAdmin}\nCan create Admins:{CanCreateAdmins}");

  }

    public static int ChooseAccountLevel(){
        Database.SelectAccountLevel();
        Console.WriteLine("Choose Account Level");
        int AccountSelection = int.Parse(Console.ReadLine());
        return AccountSelection;

        

    }
}
