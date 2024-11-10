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
}
    public static void ViewFoodMenu(){
    Console.Clear();
    System.Console.WriteLine("You have selected: View Menu");
    System.Console.WriteLine("Please select a category:");
    System.Console.WriteLine("1. lunch\n2. diner\n3. alchohol\n 4. softdrinks");
    System.Console.Write("Enter your choice (1-4): ");
    int choice2 = Convert.ToInt32(Console.ReadLine());
    if (choice2 == 1){
        LunchMenu();
    }

    if(choice2 == 2){
        DinnerMenu();
    }
    
    if(choice2 == 3){
        AlchoholMenu();
    }
    if (choice2 == 4){
        SoftDrinkMenu();
    }
    else{
        System.Console.WriteLine("Invalid choice, choose again (1-4)");
    }
    }

     public static void LunchMenu(){
        System.Console.WriteLine("Lunch");
        System.Console.WriteLine("====================");
        System.Console.Write("Name: Grilled Cheese Sandwich\nDescription: A delicious but simplistic sandwich with a melted blend of Gouda and Mozzarella on white bread, sauce of choice included\nPrice: 6.95");
        System.Console.WriteLine("\n");
        System.Console.WriteLine("Name: Panini Pesto\nDescription: half a baguette toasted to perfection, pesto tomato and mozzarella inside.\nPrice: 5.50");
        System.Console.WriteLine("\n");
        System.Console.WriteLine("Name: Creamy Tomato Basil Soup\nDescription: A thick rich soup with roasted tomatoes inside, served with breadsticks.\nPrice: 5.95");
        }

    public static void DinnerMenu(){
        System.Console.WriteLine("Dinner");
        System.Console.WriteLine("====================");
        System.Console.WriteLine("Pasta Pomodoro\nDescription: A delicious but simplistic pasta with tomato base sauce, dried tomatoes and a hint of pesto. served with rucola and pine nuts\nPrice: 13.95");
        System.Console.WriteLine("\n");
        System.Console.WriteLine("Ribeye Steak\nDescription: 3 ounces of delicious Ribeye steak with mushroom or pepper sauce.\nPrice: 23.99");
        System.Console.WriteLine("\n");
        System.Console.WriteLine("Wok vegetable blend with fried rice\nDescription: vegetables of choice, in a thick black bean sauce. choice of white or brown rice.\nPrice: 12.50");
    }

    public static void AlchoholMenu(){
        System.Console.WriteLine("Alchohol");
        System.Console.WriteLine("====================");
        System.Console.WriteLine("Pornstar Martini\nDescription: A delicious Vanilla Passionfruit cocktail with a dash of vodka.\nPrice: 11.99");
        System.Console.WriteLine("\n");
        System.Console.WriteLine("Jagerbomb\nDescription: 2 shots of jager blended with an ice cold redbull.\nPrice: 7.95");
        System.Console.WriteLine("\n");
        System.Console.WriteLine("Liefmans Fruitesse\nDescription: A low calorie fruity cider for a dashing time at our terrace.\nPrice: 3.95");
    }

    public static void SoftDrinkMenu(){
        System.Console.WriteLine("Soft drinks");
        System.Console.WriteLine("====================");
        System.Console.WriteLine("Coca Cola\nDescription: A delicious coke served with crushed ice\nPrice: 2.95");
        System.Console.WriteLine("\n");
        System.Console.WriteLine("Lipton ice tea sparkling\nDescription: A delicious Lemon ice tea with a nice sparkle, served with lemon slice.\nPrice: 2.95");
        System.Console.WriteLine("Ice Coffee mocha\nDescription: A tall ice latte with a dash of Mocha syrup\nPrice: 4.95");
    }
    }



