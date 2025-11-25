using ConsoleRpg.Helpers.EntityHelper;
using ConsoleRpgEntities.Data;
using ConsoleRpgEntities.Models.Characters;
using ConsoleRpgEntities.Models.Equipments;
using ConsoleRpgEntities.Models.Rooms;
using Microsoft.Identity.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleRpg.Helpers.Admin
{
    public class AdminMenu
    {

        private readonly OutputManager _outputManager;
        private readonly PlayerManager _playerManager;
        public Player? SelectedPlayer;
        private readonly AdminBrains _adminPlayer;
        private readonly GameContext _context;
        private readonly PlayerMenu _playerMenu;


        public AdminMenu(OutputManager outputManager, PlayerManager playerManager, AdminBrains adminPlayer, GameContext context, PlayerMenu playerMenu)
        {
            _outputManager = outputManager;
            _playerManager = playerManager;
            _adminPlayer = adminPlayer;
            _context = context;
            _playerMenu = playerMenu;
        }

        public void ShowAdminMenu()
        {
            const string adminPassword = "admin123"; // Simple hard-coded password
            _outputManager.Clear();
            _outputManager.Write("Enter admin password: ", ConsoleColor.Green);
            _outputManager.Display();

            string input = Console.ReadLine();

            if (input == adminPassword)
            {
                MainMenu();
            }
            else
            {
                _outputManager.Clear();
                _outputManager.WriteLine("Access denied.", ConsoleColor.Red);
                _outputManager.Display();
                Thread.Sleep(1500);
            }
        }

        public void MainMenu()
        {

            bool exit = false;
            while (!exit)
            {  
                _outputManager.Clear();
                _outputManager.WriteLine(
                        "Selected Player: " + ((SelectedPlayer != null) ? SelectedPlayer.Name : "None"),
                        ConsoleColor.Yellow
                    );
                _outputManager.WriteLine("=== Admin Menu ===", ConsoleColor.Cyan);
                _outputManager.WriteLine("1. View System Logs");
                _outputManager.WriteLine("2. Manage Users");
                _outputManager.WriteLine("3. Manage Abilities");
                _outputManager.WriteLine("4. Manage Rooms");
                _outputManager.WriteLine("5. Exit Admin Menu");
                _outputManager.Write("Select an option: ");
                _outputManager.Display();
                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        ViewSystemLogs();
                        break;
                    case "2":
                        ManageUsersMenu();
                        break;
                    case "3":
                        ManageAbilities();
                        break;
                    case "4":
                        ManageRoomsMenu();
                        break;
                    case "5":
                        exit = true;
                        return;
                    default:
                        _outputManager.WriteLine("Invalid choice. Please try again.", ConsoleColor.Red);
                        Thread.Sleep(1500);
                        MainMenu();
                        break;
                }
            }
        }

        private void ManageRoomsMenu()
        {
            while (true)
            {
                _outputManager.Clear();
                var rooms = _context.Rooms.ToList();
                DisplayRoomsWithStats(rooms);
                int tableEndTop = Console.GetCursorPosition().Top;
                // Define the display name based on the SelectedPlayer's status
                string selectedPlayerName = (SelectedPlayer != null)
                    ? SelectedPlayer.Name
                    : "None";

                // Integrate the name into the prompt
                _outputManager.Write($"1. Add Selected Player to Room (Selected: {selectedPlayerName})\n2. Create New Room\n3. Query Players in Room\n4.Back\n>> ");
                _outputManager.Display();
                
                var choice = Console.ReadLine();
                if (choice == "1")
                {
                    _adminPlayer.PutPlayerInRoom(tableEndTop, SelectedPlayer);
                }
                else if (choice == "2") {
                    _adminPlayer.AdminCreateRoom(tableEndTop);
                }
                else if (choice == "3") {
                    _adminPlayer.QueryRoomPlayers(tableEndTop);
                }
                else if (choice == "4") {
                    break;
                }

            }


        }

        public void DisplayRoomsWithStats(IEnumerable<Room> rooms)
        {
            Console.WriteLine("\n=============================== WORLD ROOMS ================================");

            string header =
                $"{"ID",-3} | {"Room Name",-30} | {"Room Type",-15} | {"Monsters",8} | {"Players",8}";

            Console.WriteLine(header);
            Console.WriteLine(new string('-', header.Length));

            foreach (var room in rooms)
            {
                int monsterCount = _context.Monsters.Where(m => m.RoomId == room.Id).Count();
                int playerCount = _context.Players.Where(p => p.CurrentRoom.Id == room.Id).Count();

                string line =
                    $"{room.Id,-3} | " +
                    $"{room.Name,-30} | " +
                    $"{room.RoomType,-15} | " +
                    $"{monsterCount,8} | " +   
                    $"{playerCount,8}";        

                Console.WriteLine(line);
            }

            Console.WriteLine(new string('-', header.Length));

            Console.WriteLine();
        }




        public void ViewSystemLogs()
        {
            var logPath = Path.Combine(AppContext.BaseDirectory, "Logs", "log.txt");

            if (File.Exists(logPath))
            {
                var contents = File.ReadAllText(logPath);
                Console.WriteLine("==== Log File Contents ====");
                Console.WriteLine(contents);
            }
            else
            {
                Console.WriteLine("Log file not found at: " + logPath);
            }
            //wait for key press
            Console.WriteLine("Press any key to return to the Admin Menu...", ConsoleColor.Green);
            Console.ReadKey();
        }

        public void ManageUsersMenu()
        {
            while (true)
            {
                _outputManager.Clear();
                string leftBox = SelectedPlayer?.ToString() ?? "No player selected.";

                string rightMenu =
                    "==== Admin Player Menu ====\n" +
                    "1. View, Query, and Select Player\n" +
                    "2. Add Player\n" +
                    "3. Delete Player\n" +
                    "4. Level Player\n" +
                    "5. Increase Player Attributes\n" +
                    "6. Edit Player Health or Class\n" +
                    "7. Back to Admin Menu\n" +
                    "Select an option: ";

                PrintSideBySide(leftBox, rightMenu);
                int tableEndTop = Console.GetCursorPosition().Top;

                string choice = Console.ReadLine();
                switch (choice)
                {
                    case "1":
                        var selectedPlayer = _adminPlayer.QueryPlayers();
                        if (selectedPlayer != null)
                        {
                            SelectedPlayer = selectedPlayer;
                        }
                        break;
                    case "2":
                        _adminPlayer.AdminNewPlayer();
                        break;
                    case "3":
                        if (SelectedPlayer == null)
                        {
                            _outputManager.ClearBelow(tableEndTop);
                            _outputManager.WriteLine("No player selected.", ConsoleColor.Red);
                            _outputManager.Display();
                            Thread.Sleep(1500);
                        }
                        else
                        {
                            _adminPlayer.AdminDeletePlayer(SelectedPlayer);
                            SelectedPlayer = null;
                        }
                        break;
                    case "4":
                        if (SelectedPlayer == null)
                        {
                            _outputManager.ClearBelow(tableEndTop);
                            _outputManager.WriteLine("No player selected.", ConsoleColor.Red);
                            _outputManager.Display();
                            Thread.Sleep(1500);
                        }
                        else
                        {
                            SelectedPlayer.Level++;
                        }
                        break;
                    case "5":
                        if (SelectedPlayer == null)
                        {
                            _outputManager.ClearBelow(tableEndTop);
                            _outputManager.WriteLine("No player selected.", ConsoleColor.Red);
                            _outputManager.Display();
                            Thread.Sleep(1500);
                        }
                        else
                        {
                            while (true)
                            {
                                _outputManager.ClearBelow(tableEndTop);
                                _outputManager.Write("Select attribute to increase 1.Strength 2.Intelligence 3.Agility >> ");
                                _outputManager.Display();
                                var attribute = Console.ReadLine();
                                if (attribute == "1")
                                {
                                    SelectedPlayer.Strength += 1;
                                    break;
                                }
                                else if (attribute == "2")
                                {
                                    SelectedPlayer.Intelligence += 1;
                                    break;
                                }
                                else if (attribute == "3")
                                {
                                    SelectedPlayer.Agility += 1;
                                    break;
                                }
                                else
                                {
                                    _outputManager.ClearBelow(tableEndTop);
                                    _outputManager.WriteLine("Invalid choice. Please try again.", ConsoleColor.Red);
                                    _outputManager.Display();
                                    Thread.Sleep(1500);
                                }
                            }

                        }
                        break;
                    case "6":
                        if (SelectedPlayer == null)
                        {
                            _outputManager.ClearBelow(tableEndTop);
                            _outputManager.WriteLine("No player selected.", ConsoleColor.Red);
                            _outputManager.Display();
                            Thread.Sleep(1500);
                        }
                        else
                        {
                            while (true)
                            {
                                _outputManager.ClearBelow(tableEndTop);
                                _outputManager.Write("Select new class 1.Knight 2.Mage 3.Archer >> ");
                                _outputManager.Display();
                                var classType = Console.ReadLine();
                                if (classType == "1")
                                {
                                    SelectedPlayer.classType = Enums.PlayerClass.Knight;
                                    break;
                                }
                                else if (classType == "2")
                                {
                                    SelectedPlayer.classType = Enums.PlayerClass.Mage;
                                    break;
                                }
                                else if (classType == "2")
                                {
                                    SelectedPlayer.classType = Enums.PlayerClass.Archer;
                                    break;
                                }
                                else
                                {
                                    _outputManager.ClearBelow(tableEndTop);
                                    _outputManager.WriteLine("Invalid choice. Please try again.", ConsoleColor.Red);
                                    _outputManager.Display();
                                    Thread.Sleep(1500);
                                }
                            }
                        }
                        break;
                    case "7":
                        return;
                    default:
                        _outputManager.ClearBelow(tableEndTop);
                        _outputManager.WriteLine("Invalid choice. Please try again.", ConsoleColor.Red);
                        Thread.Sleep(1500);
                        ManageUsersMenu();
                        break;

                }
            }
        }

        public void ManageAbilities()
        {
            while (true) {
                var abilities = _context.Abilities.ToList();

                //============DISPLAY===============
                if (abilities == null || !abilities.Any())
                {
                    _outputManager.WriteLine("You have no abilities to display!", ConsoleColor.Red);
                    _outputManager.Display();
                    Thread.Sleep(1000);
                    return;
                }

                _outputManager.Clear();
                _outputManager.WriteLine("--- ALL Created Abilities ---", ConsoleColor.Cyan);

                string headerFormat = "{0,-3} | {1,-20} | {2,-65} | {3,-10} | {4,-10}";

                _outputManager.WriteLine(
                    string.Format(headerFormat, "ID", "Name", "Description", "Type", "Mana Cost"),
                    ConsoleColor.Yellow);

                _outputManager.WriteLine(new string('-', 120), ConsoleColor.DarkGray);

                foreach (var ability in abilities)
                {
                    // Use a different color for the actual data
                    _outputManager.WriteLine(
                        string.Format(
                            headerFormat,
                            ability.Id, // Display 1-based index instead of the raw ID for the menu
                            ability.Name,
                            // Truncate the description if it's too long for the column
                            (ability.Description.Length > 62 ? ability.Description.Substring(0, 62) + "..." : ability.Description),
                            ability.AbilityType,
                            ability.ManaCost
                        ));
                }
                _outputManager.WriteLine(new string('-', 120), ConsoleColor.DarkGray);
                _outputManager.Display();
                int tableEndTop = Console.GetCursorPosition().Top;

                //================logic============
                // Define the display name based on the SelectedPlayer's status
                string selectedPlayerName = (SelectedPlayer != null)
                    ? SelectedPlayer.Name
                    : "None";

                // Integrate the name into the prompt
                _outputManager.Write($"1.View Selected Player Abilities  (Selected: {selectedPlayerName})\n2.Grant Ability to Selected Player  (Selected: {selectedPlayerName})\n3.Create New Ability\n4.Back\n>> ");
                _outputManager.Display();

                string choice = Console.ReadLine();
                switch (choice)
                {
                    case "1":
                        if (SelectedPlayer != null)
                        {
                            _outputManager.Clear();
                            _playerMenu.DisplayAbilitiesMenu(SelectedPlayer);
                        }
                        else
                        {
                            _outputManager.ClearBelow(tableEndTop);
                            _outputManager.WriteLine("No Player Selected.", ConsoleColor.Red);
                            _outputManager.Display();
                            Thread.Sleep(1500);
                        }
                        break;
                    case "2":
                        if (SelectedPlayer != null)
                        {
                            _adminPlayer.GrantAbilityToPlayer(tableEndTop, SelectedPlayer);
                        }
                        else
                        {
                            _outputManager.ClearBelow(tableEndTop);
                            _outputManager.WriteLine("No Player Selected.", ConsoleColor.Red);
                            _outputManager.Display();
                            Thread.Sleep(1500);
                        }
                        break;
                    case "3":
                        _outputManager.Clear();
                        _adminPlayer.AdminCreateAbility();
                        break;
                    case "4":
                        return;
                    default:
                        _outputManager.ClearBelow(tableEndTop);
                        _outputManager.WriteLine("Invalid choice. Please try again.", ConsoleColor.Red);
                        _outputManager.Display();
                        Thread.Sleep(1500);
                        ManageAbilities();
                        break;
                }

            }
        }

        public void PrintSideBySide(string left, string right, int padding = 4)
        {
            var leftLines = left.Split('\n');
            var rightLines = right.Split('\n');

            int maxLines = Math.Max(leftLines.Length, rightLines.Length);

            for (int i = 0; i < maxLines; i++)
            {
                string leftLine = i < leftLines.Length ? leftLines[i].TrimEnd() : "";
                string rightLine = i < rightLines.Length ? rightLines[i].TrimEnd() : "";

                // Pad left, then add spacer, then print right
                Console.Write(leftLine.PadRight(40));
                Console.Write(new string(' ', padding));  // space between columns
                if (i == maxLines - 1)
                    Console.Write(rightLine);  // cursor stays at the end
                else
                    Console.WriteLine(rightLine);
            }
        }



    }
}
