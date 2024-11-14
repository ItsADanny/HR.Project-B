using System;
using System.ComponentModel.DataAnnotations;

public class Menu
{
    public int ID;
    public int RestaurantID;
    public string Name;
    public string Description;
    public double Price;
    public string Foodtype; //alcohol/softdrinks/lunch/dinner

    public Menu(int RestaurantID, string Name, string Description, double Price, string Foodtype) 
    {
        this.RestaurantID = RestaurantID;
        this.Name = Name;
        this.Description = Description;
        this.Price = Price;
        this.Foodtype = Foodtype;
    }

    public Menu(int ID, int RestaurantID, string Name, string Description, double Price, string Foodtype)
    {
        this.ID = ID;
        this.RestaurantID = RestaurantID;
        this.Name = Name;
        this.Description = Description;
        this.Price = Price;
        this.Foodtype = Foodtype;
    }

}