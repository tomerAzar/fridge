using System;
using System.Collections.Generic;

namespace Fridge
{
    class Program
    {
        static void Main(string[] args)
        {
               Refrigerator r = new Refrigerator();
               Console.WriteLine("cleared refregator");
               r.ClearExpiredItems();
               Console.WriteLine("sorted shelfs");
               List<Shelf> s = r.SortShelfsBySpace();
               foreach (Shelf a in s)
                   Console.WriteLine(a.ToString());
                List<Item> s1 = r.SortItems();
                Console.WriteLine("sorted items");
               foreach (Item a in s1)
                    Console.WriteLine(a.ToString());
        }
    }
}
