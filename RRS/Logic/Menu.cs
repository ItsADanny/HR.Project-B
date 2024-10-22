using System;
using System.ComponentModel.DataAnnotations;

namespace RRS.Logic;

public class Menu
{
    // set ID for number 3 e.g. 3001 3002 3003
    public List<Menu> MenuList = new List<Menu>();
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




    public Menu(int ID, int RestaurantID, string Name,string Description,double Price,string Foodtype)
    {
        this.ID = ID;
        this.RestaurantID = RestaurantID;
        this.Name = Name;
        this.Description = Description;
        this.Price = Price;
        this.Foodtype = Foodtype;
    }


    public static void AddMenuItem(int ID, string Name,string Description,double Price,string Foodtype)
    {
        
        
        //new menuitem object
        Menu NewItem = new Menu();

        var existingItem = MenuList.FirstOrDefault(NewItem => NewItem.Name.Equals(NewItem.Name, StringComparison.OrdinalIgnoreCase));

        if (existingItem != null)
        {

        }
        //retrieve data from presentation layer name/desc/type/price
        //make sure name isnt the same as existing items
        //add a description
        //ask a foodtype



    }

    public string deletemenuitem(int ID,string Name,string Description,double Price,string Foodtype)
    {
        //receive menu item name
        //check if its an actual object in the list 
        //try a way to delete object?
       ;

    }

    public string editmenuitemName(int ID, string Name)
    {
        //receive ID from item 
        //remove name from item
        //receive new name for item
        //add the new name for item
        return;
    }

    public string editmenuitemDescription(int ID, string Description)
    {
        //receive id from item 
        //remove description from item
        //receive new desc for item
        //add the new name for item
        return;

    }

    public string editmenuitemPrice(int ID, double Price)
    {
        //receive id from item
        //remove price from item
        //receive new price
        //add new price 
        return;
    }

    public string editmenuitemType(int ID, string Foodtype)
    {
        //receive id from item
        //delete type of item
        //receive type of item
        //edit type item
        return;
    }




}
