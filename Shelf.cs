using System;
using System.Collections.Generic;

namespace Fridge
{
    public class Shelf
    {
        private static int _generateUniqeNumber = 0;
        public int ShelfId { get; private set; }
        private readonly int _floorInFridge;
        public int FreeSpace { get; private set; }
        public List<Item> Items { get; private set; }

        public Shelf ()
        {
            Console.WriteLine("a shelf is creating: ");
            this.ShelfId = _generateUniqeNumber++;
            this._floorInFridge = GettingValidInput.GettingPositiveInt("enter floor in fridge: ");
            this.FreeSpace = GettingValidInput.GettingPositiveInt("enter amount of space: ");
            this.Items = new List<Item>();
            InitItems();
        }

        private void InitItems()
        {
            bool initItems = GettingValidInput.GettingYesNo("do you want to initialize items in shelf: yes/no: ");
            if (initItems)
            {
                int numberOfItems = GettingValidInput.GettingPositiveInt("enter number of items you wish to insert: ");
                for (int i = 0; i < numberOfItems; i++)
                    AddItem();
            }
        }

        public void AddItem ()
        {
            Item tempItem = new Item();
            if (tempItem.Space > this.FreeSpace)
                Console.WriteLine("cant insert this item because lake of space");
            else
            {
                this.Items.Add(tempItem);
                tempItem.OnShelf = this;
                this.FreeSpace -= tempItem.Space;
            }
        }
        public void RemoveItem (int id)
        {
            int numberOfItems = this.Items.Count;
            for (int i=0; i<numberOfItems; i++)
                if (this.Items[i].ItemId == id)
                {
                    this.Items.Remove(this.Items[i]);
                    return;
                }
        }
        public override string ToString()
        {
            return $"id: {this.ShelfId}, floor in fridge: {this._floorInFridge}, free space: {this.FreeSpace}, number of items: {this.Items.Count}";
        }
    }
}
