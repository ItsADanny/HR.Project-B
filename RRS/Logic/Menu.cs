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

//as an admin I want my menu items to be stored so I can remove and delete any menu items as wished
 //have a menu class
 //add functions to remove and add items
 //add function to view the list

 //addmenuitem method
 //deletemenuitem method
 //editmenuiteminfo
 //editmenuitemname
 //viewmenulist

    //Since in CreateItem (In the presentation layer) we assign the values to the menu item
    //we can start with an empty constructor which doesn't take any variables
    public Menu() { }

    public Menu(int ID, int RestaurantID, string Name,string Description,double Price,string Foodtype)
    {
        this.ID = ID;
        this.RestaurantID = RestaurantID;
        this.Name = Name;
        this.Description = Description;
        this.Price = Price;
        this.Foodtype = Foodtype;
    }


    public void AddMenuItem(int ID, string Name,string Description,double Price,string Foodtype)
    {
        //new menuitem object
        
        //retrieve data from presentation layer name/desc/type/price
        //make sure name isnt the same as existing items
        //add a description
        //ask a foodtype

    }

    public string deletemenuitem(int ID,string Name,string Description,double Price,string Foodtype)
    {
        //receive menu item ID 
        //try a way to delete object?

    }

    public string editmenuitemName(int ID, string Name)
    {
        //receive ID from item 
        //remove name from item
        //receive new name for item
        //add the new name for item
    }

    public string editmenuitemDescription(int ID, string Description)
    {
        //receive id from item 
        //remove description from item
        //receive new desc for item
        //add the new name for item

    }

    public string editmenuitemPrice(int ID, double Price)
    {
        //receive id from item
        //remove price from item
        //receive new price
        //add new price 
    }

    public string editmenuitemType(int ID, string Foodtype)
    {
        //receive id from item
        //delete type of item
        //receive type of item
        //edit type item
    }




}
