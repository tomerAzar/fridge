using System;
using System.Collections.Generic;

namespace Fridge
{
    public class Refrigerator
    {

        private static int _generateUniqeNumber = 0;
        private readonly int _fridgeId;
        private readonly string _type;
        private readonly Color _color;
        private readonly int _numberOfShelfs;
        private List<Shelf> _shelfs;

        public Refrigerator()
        {
            Console.WriteLine("fridge is creating: ");
            this._fridgeId = _generateUniqeNumber++;
            this._type = GettingValidInput.GettingString("enter type of fridge: ");
            this._color = GettingValidInput.ParseEnum<Color>();
            this._numberOfShelfs = GettingValidInput.GettingPositiveInt("enter number of maximum shelfs in friedg: ");
            this._shelfs = new List<Shelf>();
            InItShelfs();
        }

        private void InItShelfs()
        {
            bool initShelfs = GettingValidInput.GettingYesNo("do you want to initialize values of shelfs in refregator: yes/no: ");
            if (initShelfs)
            {
                int numberOfShelfs = GettingValidInput.GettingPositiveInt("enter number of shelfs you wish to insert: ");
                while (numberOfShelfs > this._numberOfShelfs)
                {
                    Console.WriteLine("you cannot initialize more shelfs than you declaired earlier");
                    numberOfShelfs = GettingValidInput.GettingPositiveInt("enter number of shelfs you wish to insert");
                }
                for (int i = 0; i < numberOfShelfs; i++)
                    this._shelfs.Add(new Shelf());
            }
        }

        public override string ToString()
        {
            return $"id: {this._fridgeId}, type: {this._type}, color: {this._color}, number of maximum shelfs: {this._numberOfShelfs}, actual shelfs: {this._shelfs.Count}";
        }
        public void PrintItems ()
        {
            foreach (Shelf shelf in this._shelfs)
                foreach (Item item in shelf.Items)
                    Console.WriteLine(item.ToString());
        }
        public int FreeSpace ()
        {
            int freeSpace = 0;
            foreach (Shelf shelf in _shelfs)
                freeSpace += shelf.FreeSpace;
            return freeSpace;
        }
        public void AddItem ()
        {
            if (this._shelfs.Count == 0)
                Console.WriteLine("cannot add item because there are no shelfs");
            else
            {
                MostSpacedShalfe(this._shelfs).AddItem();
            }
        }
       
        public Shelf MostSpacedShalfe (List<Shelf> shelves)
        {
            int maxFreeSpace = 0;
            Shelf mostSpaced = null;
            foreach (Shelf shelf in shelves)
            {
                if (shelf.FreeSpace > maxFreeSpace)
                {
                    maxFreeSpace = shelf.FreeSpace;
                    mostSpaced = shelf;
                }
            }
            return mostSpaced;
        }
        public void RemoveItem (int id)
        {
            foreach (Shelf shelf in _shelfs)
            {
                foreach(Item item in shelf.Items)
                {
                    if (item.ItemId == id)
                    {
                        shelf.RemoveItem(id);
                        return;
                    }
                }
            }
        }
        public void ClearExpiredItems ()
        {
            foreach (Shelf shelf in _shelfs)
            {
                for(int i=0; i<shelf.Items.Count; i++)
                {
                    if (DateTime.Compare(shelf.Items[i].ExpiryDate, DateTime.Today) <0 )
                    {
                        shelf.RemoveItem(shelf.Items[i].ItemId);
                        i--;
                    }
                }
            }
        }
        public int WantToEat (Kashrut kashrut, TypeOfFood typeOfFood)
        {
            int numberOfFoods = 0;
            foreach (Shelf shelf in _shelfs)
            {
                foreach (Item item in shelf.Items)
                {
                    if (item.Kashrot.Equals(kashrut) && item.Type.Equals(typeOfFood) && DateTime.Compare(item.ExpiryDate, DateTime.Today) >= 0)
                        numberOfFoods++;
                }
            }
            return numberOfFoods;
        }

        public List <Item> SortItems ()
        {
            List<Shelf> shelfsCopy = new List<Shelf>(this._shelfs);
            List<Item> items = new List<Item>();
            int numberOfItems = 0;
            foreach (Shelf shelf in _shelfs)
                numberOfItems += shelf.Items.Count;
            for (int i=0; i<numberOfItems; i++)
                items.Add(FirstItemToExpireAndRemove(shelfsCopy));
            return items;
        }
        public Item FirstItemToExpireAndRemove (List<Shelf> shelfs)
        {
            DateTime sonnest= new DateTime(2030,10,10);
            Shelf soonestToExpire = null;
            Item firstToExpire=null;
            foreach (Shelf shelf in shelfs)
            {
                foreach (Item item in shelf.Items)
                {
                    if (DateTime.Compare(sonnest, item.ExpiryDate) > 0)
                    {
                        sonnest = item.ExpiryDate;
                        firstToExpire = item;
                        soonestToExpire = shelf;
                    }
                }
            }
            soonestToExpire.RemoveItem(firstToExpire.ItemId);
            return firstToExpire;
        }

        public List<Shelf> SortShelfsBySpace ()
        {
            List<Shelf> shelfsCopy = new List<Shelf>(this._shelfs);
            List<Shelf> shelfsSorted = new List<Shelf>();
            int numberOfShelfs = shelfsCopy.Count;
            for (int i=0; i<numberOfShelfs; i++)
            {
                shelfsSorted.Add(this.MostSpacedShalfe(shelfsCopy));
                shelfsCopy.Remove(this.MostSpacedShalfe(shelfsCopy));
            }
            return shelfsSorted;
        }
        public void MakeFridgeToShopping ()
        {
            if (this.FreeSpace() >= 29) return;
            this.ClearExpiredItems();
            if (this.FreeSpace() >= 29) return;
            int optionalFreeSpace = 0;
            List<Item> deleteItems = new List<Item>();
            foreach (Shelf shelf in _shelfs)
            {
                foreach (Item item in shelf.Items)
                {
                    int days = item.ExpiryDate.Year * 365 + item.ExpiryDate.Month * 30 + item.ExpiryDate.Day;
                    int Todaydays = DateTime.Today.Year * 365 + DateTime.Today.Month * 30 + DateTime.Today.Day;

                    if (item.Kashrot == Kashrut.dairy && days - Todaydays <3)
                    {
                        optionalFreeSpace += item.Space;
                        deleteItems.Add(item);
                    }
                    if (item.Kashrot == Kashrut.meat && days - Todaydays < 7)
                    {
                        optionalFreeSpace += item.Space;
                        deleteItems.Add(item);
                    }
                    if (item.Kashrot == Kashrut.pareve && days - Todaydays < 1)
                    {
                        optionalFreeSpace += item.Space;
                        deleteItems.Add(item);
                    }
                }
            }
            if (this.FreeSpace() + optionalFreeSpace >= 29)
            {
                for (int i = 0; i < deleteItems.Count; i++)
                {
                    Console.WriteLine("deleting: " + deleteItems[i].ToString());
                    RemoveItem(deleteItems[i].ItemId);
                }
            }
            else
                Console.WriteLine("canoot make fridge to shopping");
        }
    }
}
