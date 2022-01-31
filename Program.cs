using System;
using System.Collections.Generic;

namespace Fridge
{
    class Program
    {
        static void Main(string[] args)
        {
            Refrigerator fridge = new Refrigerator();
            Console.WriteLine("fridge: " + fridge.ToString());
            fridge.PrintItems();
            Console.WriteLine("free space: " + fridge.FreeSpace());
            bool addItem = GettingValidInput.GettingYesNo("do you want to enter an item: yes/no: ");
            if (addItem)
                fridge.AddItem();
            bool removeItem = GettingValidInput.GettingYesNo("do you want to remove an item: yes/no: ");
            if (removeItem)
                fridge.RemoveItem(GettingValidInput.GettingPositiveInt("enter id of item: "));
            Console.WriteLine("now there is a cleaning");
            fridge.ClearExpiredItems();
            fridge.PrintItems();
            Console.WriteLine("enter what you want to eat : ");
            Kashrut kashrut = GettingValidInput.ParseEnum<Kashrut>();
            TypeOfFood typeOfFood = GettingValidInput.ParseEnum<TypeOfFood>();
            Console.WriteLine("number of items you have : " + fridge.WantToEat(kashrut, typeOfFood));
            Console.WriteLine("going to shopping ");
            fridge.MakeFridgeToShopping();
            List<Shelf> shelves = fridge.SortShelfsBySpace();
            Console.WriteLine("sorted shelfs");
            foreach (Shelf shelf in shelves)
                Console.WriteLine(shelf.ToString());
            List<Item> items = fridge.SortItems();
            Console.WriteLine("sorted items");
            foreach (Item item in items)
                Console.WriteLine(item.ToString());
            
        }

    }
}
