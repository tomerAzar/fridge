using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fridge
{
    public class Item
    {
        private static int _generateUniqeNumber = 0;
        public int ItemId { get; private set; }
        private readonly string _name;
        public Shelf OnShelf { get; set; }
        public TypeOfFood Type { get; private set; }
        public Kashrut Kashrot { get; private set; }
        public DateTime ExpiryDate { get; private set; }
        public int Space { get; private set; }

        public Item()
        {
            Console.WriteLine("item is now creating: ");
            this.ItemId = _generateUniqeNumber++;
            this._name = GettingValidInput.GettingString("enter item name: ");
            // initionalize onshelfe in the shelf class
            this.Type = GettingValidInput.ParseEnum<TypeOfFood>();
            this.Kashrot = GettingValidInput.ParseEnum<Kashrut>();
            this.ExpiryDate = GettingValidInput.GettingDate("enter expiery date : dd/mm/yyyy: ");
            this.Space = GettingValidInput.GettingPositiveInt("enter space of item: ");
        }

        public override string ToString()
        {
            return $"id: {this.ItemId}, name: {this._name}, on shelf: {this.OnShelf.ShelfId}, type: {this.Type}, kashrot: {this.Kashrot}, expiry date: {this.ExpiryDate}, space: {this.Space}";
        }
    }
}
