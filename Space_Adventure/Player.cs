using System;
using System.Collections.Generic;

namespace Space_Adventure
{
    public class Player
    {
        public int CurrentAmountOfCredit { get; set; }
        public string PlayerName { get; set; }
        public int PlayerHealth { get; set; }
        public List<string> PlayerInventory = new List<string>();

        public Player()
        {
            PlayerName = "unknown player name";
            PlayerHealth = 10;
            PlayerInventory.Add("a Credit-Chip");
        }

        public int PutCreditOnCard(int amountOfCredit)
        {
            CurrentAmountOfCredit += amountOfCredit;
            return CurrentAmountOfCredit;
        }

        public void ShowMeTheMoney()
        {
            Console.WriteLine("\nYou've got " + CurrentAmountOfCredit + " credits so far!");
        }

        public void PlaceInInventory(string item)
        {
            PlayerInventory.Add(item);
        }

        public void CheckInventory()
        {
            Console.WriteLine("\nThese are the items in your inventory: ");
            foreach (string item in PlayerInventory)
            {
                Console.WriteLine(item);
            }
        }
    }
}