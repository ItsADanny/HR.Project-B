using System;

namespace Reservationcs;
public class RestaurantDisplay{

    public static string Name = "The Social Table";
    public static string Address = "123 Flavor Street, Food City, FC 98765";
    public static string ContactNumber = "+1 (555) 123-4567";
    public static string Description = "Welcome to a dining experience like no other! At The Social Table, we bring people together by seating you with others. Whether you're here to make new friends or just enjoy a delicious meal, our tables are set for lively conversations and memorable encounters.";
    public static string OpeningHours = "Mon-Sun: 11:00 AM - 11:00 PM";


    public static void ViewRestaurantInfo(){
        Console.Clear();
        Console.WriteLine("===================================");
        Console.WriteLine("         Welcome to " + Name);
        Console.WriteLine("===================================");
        Console.WriteLine($"Address:          {Address}");
        Console.WriteLine($"Contact Number:   {ContactNumber}");
        Console.WriteLine($"Opening Hours:    {OpeningHours}");
        Console.WriteLine("-----------------------------------");
        Console.WriteLine("About Us:");
        Console.WriteLine(Description);
        Console.WriteLine("===================================");

        System.Console.WriteLine("Would you like to leave a review?:");
        System.Console.WriteLine("1. Yes");
        System.Console.WriteLine("2. No");
        System.Console.Write("Enter your choice: ");
        int choice = Convert.ToInt32(Console.ReadLine());
        if (choice == 1){
            System.Console.Write("Enter your review: ");
            string review = Console.ReadLine();
            System.Console.WriteLine("Thank you for your review!");
            System.Console.WriteLine(review);
        }
        if (choice == 2){
            System.Console.WriteLine("Thank you for visiting us!");
        }
}}


