using System;
using System.Collections.Generic;

namespace Space_Adventure
{
    public class GameEngine
    {
        // tiny method to insert a line 
        private static void InsertLine()
        {
            Console.WriteLine("-----------------------------------------------------------------");
        }

        // making some static variables to be used throughout the game
        private const int NumberOfRooms = 14;
        private static List<Room> _rooms;
        private static bool _gameIsOn;

        // game engine constructor, setting up rooms and executing game loop
        static GameEngine()
        {
            _gameIsOn = true;
            var currentPlayer = new Player();
            
            SetupRoomConnectionsAndAttributes();
            Room currentRoom = _rooms[0];

            // welcome player and run start up story
            Console.WriteLine("Welcome to the Glitch Space Station.");
            Console.Write("What is your name?: ");
            Console.ForegroundColor = ConsoleColor.Cyan;
            currentPlayer.PlayerName = Console.ReadLine();
            
            Console.ResetColor();
            Console.WriteLine("\nWelcome, " + currentPlayer.PlayerName + "!");
            Console.WriteLine(_rooms[0].RoomDescription);
            Console.WriteLine("\nI'll give you a few actions to choose from: ");
            Console.WriteLine("");

            // array of actions to display
            string[] actions =
            {
                "Go north", "Go south", "Go east", "Go west", "Scan for credit",
                "Search room", "Show me the money", "Check inventory", "Look around", "Quit"
            };

            Console.ForegroundColor = ConsoleColor.DarkCyan;
            Array.ForEach(actions, Console.WriteLine);
            Console.ResetColor();

            Console.WriteLine("\nHit enter when you're ready to continue...");
            Console.ReadKey();
            Console.WriteLine("\nI'm the Super Experimental Intelligence, by the way.");
            Console.WriteLine("You can call me SExI for short... ;)");
            Console.WriteLine("\nIf you at any point need to be reminded of the actions, just call for help.");
            Console.WriteLine("Now, what would you like to do?: ");

            // game loop
            // TODO replace with switch statement
            while (_gameIsOn)
            {
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.Write("\n> ");
                string input = Console.ReadLine().ToLower();
                if (input.Equals("go north"))
                {
                    Console.ResetColor();
                    if (currentRoom.NorthRoom == null)
                        Console.WriteLine("\nThere is no spoon. Try another direction.");
                    else
                    {
                        currentRoom = currentRoom.NorthRoom;
                        InsertLine();
                        currentRoom.CheckRoomStatus(); // has player been here before
                        StartEndStory(currentRoom, currentPlayer, _rooms); // is this the end room
                    }
                }
                else if (input.Equals("go south"))
                {
                    Console.ResetColor();
                    if (currentRoom.SouthRoom == null)
                        Console.WriteLine(
                            "\nThat's what she said... But this door is locked, and you can't get through." +
                            "\nTry another direction.");
                    else
                    {
                        currentRoom = currentRoom.SouthRoom;
                        InsertLine();
                        currentRoom.CheckRoomStatus(); // has player been here before
                        StartEndStory(currentRoom, currentPlayer, _rooms); // is this the end room
                    }
                }
                else if (input.Equals("go east"))
                {
                    Console.ResetColor();
                    if (currentRoom.EastRoom == null)
                        Console.WriteLine("\nThere is a forcefield blocking the doorway." +
                                          "\nI suggest another direction.");
                    else
                    {
                        currentRoom = currentRoom.EastRoom;
                        InsertLine();
                        currentRoom.CheckRoomStatus(); // has player been here before
                        StartEndStory(currentRoom, currentPlayer, _rooms); // is this the end room
                    }
                }
                else if (input.Equals("go west"))
                {
                    Console.ResetColor();
                    if (currentRoom.WestRoom == null)
                        Console.WriteLine("\nLife is peaceful there. But you can't get through." +
                                          "\nTry another direction.");
                    else
                    {
                        currentRoom = currentRoom.WestRoom;
                        InsertLine();
                        currentRoom.CheckRoomStatus(); //has player been here before
                        StartEndStory(currentRoom, currentPlayer, _rooms); //is this the end room
                    }
                }
                else if (input.Equals("scan for credit"))
                {
                    Console.ResetColor();
                    if (currentRoom.AmountOfCreditInRoom == 0)
                    {
                        Console.WriteLine(Description.NoCredit);
                    }
                    else
                    {
                        currentRoom.LookForCredit();
                        currentPlayer.PutCreditOnCard(currentRoom
                            .AmountOfCreditInRoom); // adding room credit to player credit
                        currentRoom.AmountOfCreditInRoom = 0;
                    }
                }
                else if (input.Equals("show me the money"))
                {
                    Console.ResetColor();
                    currentPlayer.ShowMeTheMoney();
                }
                else if (input.Equals("search room"))
                {
                    Console.ResetColor();
                    currentRoom.SearchRoom();
                    if (currentRoom.ItemInRoom != null)
                    {
                        currentPlayer.PlaceInInventory(currentRoom.ItemInRoom);
                        currentRoom.ItemInRoom = null;
                    } //if item in room is null, nothing more should happen
                }
                else if (input.Equals("check inventory"))
                {
                    Console.ResetColor();
                    currentPlayer.CheckInventory();
                }
                else if (input.Equals("look around"))
                {
                    Console.ResetColor();
                    Console.WriteLine(currentRoom.RoomDescription);
                }
                else if (input.Equals("sexi"))
                {
                    Console.ForegroundColor = ConsoleColor.Magenta;
                    Console.WriteLine("\nYeah, what is it, honey?");
                }
                else if (input.Equals("help"))
                {
                    Console.ForegroundColor = ConsoleColor.DarkCyan;
                    InsertLine();
                    foreach (string item in actions)
                    {
                        Console.WriteLine(item);
                    }

                    InsertLine();
                    Console.ResetColor();
                }
                else if (input.Equals("quit"))
                {
                    Console.ResetColor();
                    Console.WriteLine(Description.Quitter);
                    Console.ReadKey();
                    return; //jump out of game loop
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine(
                        "\nI didn't quite get that, please try again. " +
                        "\nIf you can't remember the actions, call for help.");
                }
            }

            Console.ReadKey();
        }

        private static void SetupRoomConnectionsAndAttributes()
        {
            // random number generator to make a random amount of gold in each room
            Random randomGen = new Random();
            
            // loop make the room objects and add them to the list of rooms
            _rooms = new List<Room>();
            for (int i = 0; i < NumberOfRooms; i++)
            {
                int randomNumber = randomGen.Next(1, 21);
                _rooms.Add(new Room("Room " + i, randomNumber, false, false));
            }

            // assign adjoining rooms to each room, the null is a choice, don't want player to enter same room when
            // no adjoining rooms are present
            _rooms[0].AssignAdjoiningRooms(_rooms[1], null, null, null);
            _rooms[1].AssignAdjoiningRooms(null, _rooms[0], _rooms[2], _rooms[3]);
            _rooms[2].AssignAdjoiningRooms(_rooms[7], null, null, _rooms[1]);
            _rooms[3].AssignAdjoiningRooms(_rooms[4], null, _rooms[1], null);
            _rooms[4].AssignAdjoiningRooms(null, _rooms[3], _rooms[6], _rooms[5]);
            _rooms[5].AssignAdjoiningRooms(null, null, _rooms[4], null);
            _rooms[6].AssignAdjoiningRooms(_rooms[9], null, _rooms[7], _rooms[4]);
            _rooms[7].AssignAdjoiningRooms(null, _rooms[2], null, _rooms[6]);
            _rooms[8].AssignAdjoiningRooms(_rooms[11], null, _rooms[13], null);
            _rooms[9].AssignAdjoiningRooms(null, _rooms[6], _rooms[10], null);
            _rooms[10].AssignAdjoiningRooms(_rooms[12], _rooms[7], _rooms[11], _rooms[9]);
            _rooms[11].AssignAdjoiningRooms(null, _rooms[8], null, _rooms[10]);
            _rooms[12].AssignAdjoiningRooms(null, _rooms[10], null, null);
            _rooms[13].AssignAdjoiningRooms(null, null, null, _rooms[8]);

            // assigning attributes to rooms; name, description, item
            _rooms[0].AssignAttributes("the Decompression Room", Description.DecompRoom0, "a Flashlight");
            _rooms[1].AssignAttributes("the Shuttle Bay", Description.ShuttleBay1, "a Wrench");
            _rooms[2].AssignAttributes("the Turbolift Shaft", Description.TurboliftShaft2, null);
            _rooms[3].AssignAttributes("a hallway", Description.Hallway3, null);
            _rooms[4].AssignAttributes("the Mess Hall", Description.MessHall4, "a Cup Noodle from 2001");
            _rooms[5].AssignAttributes("a Casino", Description.Casino5, "42 Plastic Chips");
            _rooms[6].AssignAttributes("the restrooms", Description.Restrooms6, null);
            _rooms[7].AssignAttributes("the Movie Theater", Description.MovieTheater7, null);
            _rooms[8].AssignAttributes("another hallway", Description.BoxRoom8, "a Bow Tie");
            _rooms[9].AssignAttributes("the Holodeck", Description.Holodeck9, null);
            _rooms[10].AssignAttributes("the Main Bridge and Nav-Room", Description.MainBridge10, null);
            _rooms[11].AssignAttributes("The Trillian Tip", Description.SpaceBar11, null);
            _rooms[12].AssignAttributes("the Captains Quarters", Description.CaptainsQuarters12, "the Picard Keycard");
            _rooms[13].AssignAttributes("the Pod Room", Description.PodRoom13, null);

            // trigger for the end story
            _rooms[13].IsPodroom = true;
        }
        
        //method to check if player has entered the end room, and initiate end story if yes
        private static void StartEndStory(Room currentRoom, Player currentPlayer, List<Room> rooms)
        {
            if (!currentRoom.IsPodroom) return;
            
            // end story should display every time the user enters this room
            currentRoom.BeenHereBefore = false; 
            Console.ReadKey();
                
            // can't win without this card
            if (!currentPlayer.PlayerInventory.Contains("the Picard Keycard")) 
            {
                Console.WriteLine(Description.NoPicardKeycard);
                return;
            }

            // if player's got card, she also needs 50 credits to win
            if (currentPlayer.CurrentAmountOfCredit < 50)
            {
                Console.WriteLine(Description.NotEnoughCredit); 
            }
            else
            {
                //end the game loop
                Console.WriteLine(Description.EscapeTheStation);
                _gameIsOn = false; 
            }
        }
    }
}