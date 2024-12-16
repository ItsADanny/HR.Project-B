using System;
using System.ComponentModel.DataAnnotations;

public class Menu : IDBRestaurantClass
{
    public int ID {get;}
    public int RestaurantID {get;}
    public string Name;
    public string Description;
    public double Price;
    public int Foodtype; //alcohol/softdrinks/lunch/dinner

    public Menu(int RestaurantID, string Name, string Description, double Price, int Foodtype) 
    {
        this.RestaurantID = RestaurantID;
        this.Name = Name;
        this.Description = Description;
        this.Price = Price;
        this.Foodtype = Foodtype;
    }

    public Menu(int ID, int RestaurantID, string Name, string Description, double Price, int Foodtype)
    {
        this.ID = ID;
        this.RestaurantID = RestaurantID;
        this.Name = Name;
        this.Description = Description;
        this.Price = Price;
        this.Foodtype = Foodtype;
    }

    public override string ToString() {
        return $"ID: {ID}\nRestaurantID: {RestaurantID}\nName: {Name}\nDescription: {Description}\nPrice: {Price}\nFoodType: {Foodtype}";
    }

    public string ToStringDisplay()
    {
        return $"Name: {Name}\nDescription: {Description}\nPrice: {Price}\nFoodType: {Database.SelectFoodType(RestaurantID, Foodtype)}";
    }

}