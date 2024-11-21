using System;
using Reservationcs;

namespace RRS.Presentation;


public class RestaurantMainDisplay{
    public void DisplayRestaurantMain(Accounts LoggedInAccount){
    System.Console.WriteLine("================================");
    System.Console.WriteLine("   Welcome to the restaurant");
    System.Console.WriteLine("================================");
    System.Console.WriteLine("   Please choose an option");
    System.Console.WriteLine("   1. Make a reservation");
    System.Console.WriteLine("   2. View Menu");
    System.Console.WriteLine("   3. View existing reservations");
    System.Console.WriteLine("   4. User settings");
    System.Console.WriteLine("   5. Restaurant information");
    System.Console.WriteLine("   6. Logout");
    System.Console.Write("   Enter your choice (1-6): ");
    int choice = Convert.ToInt32(Console.ReadLine());
    switch(choice){
    
    case 1: 
    ReservationDisplay.DisplayCreateReservation(0); // 0 == SelectedRestaurant || Restaurant ID
    break;


    case 2:
    RestaurantDisplay.ViewFoodMenu();
    break;

    case 3:
    ReservationDisplay.DisplayForRestaurant(0);
    break;

    case 4:
    UserSettingsDisplay.UserSettingsMenu(LoggedInAccount);
    break;

    case 5:
    RestaurantDisplay.ViewRestaurantInfo();
    break;

    case 6:
    Console.WriteLine("You have logged out");
    ProgramDisplay.Display();
    break;

    }}}
