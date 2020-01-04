using System;

namespace Space_Adventure
{
    public class Room
    {
        public Room NorthRoom;
        public Room SouthRoom;
        public Room EastRoom;
        public Room WestRoom;
        public bool BeenHereBefore;
        public bool IsPodroom;

        public string RoomName { get; set; }
        public int AmountOfCreditInRoom { get; set; }
        public string ItemInRoom { get; set; }
        public string RoomDescription { get; set; }

        public Room(string aName, int credit, bool firstVisit, bool podroom)
        {
            RoomName = aName;
            AmountOfCreditInRoom = credit;
            BeenHereBefore = firstVisit;
            IsPodroom = podroom;
        }

        public void AssignAdjoiningRooms(Room aNorth, Room aSouth, Room aEast, Room aWest)
        {
            NorthRoom = aNorth;
            SouthRoom = aSouth;
            EastRoom = aEast;
            WestRoom = aWest;
        }

        public void AssignAttributes(string aName, string aDescription, string anItem)
        {
            RoomName = aName;
            RoomDescription = aDescription;
            ItemInRoom = anItem;
        }

        public int LookForCredit()
        {
            Console.WriteLine("\nOoh, you found " + AmountOfCreditInRoom + " credits!");
            Console.WriteLine("They've been automatically added to your Credit-Chip.");
            return AmountOfCreditInRoom;
        }

        public string SearchRoom()
        {
            if (ItemInRoom == null)
            {
                Console.WriteLine("\nLooks like there's nothing interesting to find here...");
                return null;
            }

            Console.WriteLine("\nYou found " + ItemInRoom + ". Lucky you!");
            Console.WriteLine("It's been added to your inventory.");
            return ItemInRoom;
        }

        public void CheckRoomStatus()
        {
            //I want to make sure the player knows when they're entering a room they've already been in
            Console.WriteLine("\nYou've entered " + RoomName + ".");
            if (BeenHereBefore == true)
            {
                Console.WriteLine("\nThis room looks familiar... You've been here before.");
            }
            else
            {
                Console.WriteLine(RoomDescription);
                BeenHereBefore = true;
            }
        }
    }
}