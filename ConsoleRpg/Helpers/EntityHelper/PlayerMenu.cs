using ConsoleRpgEntities.Models.Characters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleRpg.Helpers.EntityHelper
{
    public class PlayerMenu
    {
        private readonly OutputManager _outputManager;
        private readonly InventoryManager _inventoryManager;
        private readonly PlayerManager _playerManager;

        public PlayerMenu(OutputManager outputManager, InventoryManager inventoryManager, PlayerManager playerManager)
        {
            _outputManager = outputManager;
            _inventoryManager = inventoryManager;
            _playerManager = playerManager;
        }

        public void MainMenu()
        {
            bool exitMenu = false;
            while (!exitMenu)
            {
                _outputManager.Clear();
                _outputManager.WriteLine(_playerManager.Player.ToString());
                _outputManager.WriteLine("");
                _outputManager.WriteLine("1. Level Attributes");
                _outputManager.WriteLine("2. View Abilities");
                _outputManager.WriteLine("3. Back to Main Menu");
                _outputManager.Write(">> ");
                _outputManager.Display();
                var menuChoice = Console.ReadLine();
                if (menuChoice == "1")
                {
                    InteractWithAttributeMenu();
                }
                else if (menuChoice == "2")
                {
                    DisplayAbilitiesMenu(_playerManager.Player);
                }
                else if (menuChoice == "3")
                {
                    exitMenu = true; // go back to main menu
                }
                else
                {
                    _outputManager.WriteLine("Invalid entry", ConsoleColor.Red);
                    _outputManager.Display();
                    Thread.Sleep(1000);
                }
            }
        }
        public void InteractWithAttributeMenu()
        {
            _outputManager.Clear();
            bool exitMenu = false;
            _outputManager.WriteLine(_playerManager.BuildAttributeTable());

            while (!exitMenu)
            {
                // Show options
                Console.WriteLine();
                _outputManager.WriteLine("-----Select-----", ConsoleColor.Cyan);
                _outputManager.WriteLine($"1. Spend Skillpoints ({_playerManager.Player.SkillPoints} available)\n2. Back");
                _outputManager.Write(">> ");
                _outputManager.Display();

                var menuChoice = Console.ReadLine();

                if (menuChoice == "1")
                {
                    if (_playerManager.Player.SkillPoints <= 0)
                    {
                        _outputManager.WriteLine("No Skillpoints Available.", ConsoleColor.Red);
                        _outputManager.Display();
                        Thread.Sleep(1000);
                    }
                    else
                    {
                        bool spending = true;

                        while (spending && _playerManager.Player.SkillPoints > 0)
                        {
                            _outputManager.Clear();
                            _outputManager.WriteLine(_playerManager.BuildAttributeTable());
                            _outputManager.WriteLine($"Skill Points available: {_playerManager.Player.SkillPoints}");
                            _outputManager.WriteLine("Choose an attribute to increase:");
                            _outputManager.WriteLine("[1] Intelligence");
                            _outputManager.WriteLine("[2] Strength");
                            _outputManager.WriteLine("[3] Agility");
                            _outputManager.WriteLine("[4] Done");
                            _outputManager.Write(">> ");
                            _outputManager.Display();

                            var choice = Console.ReadLine();

                            if (choice == "1")
                            {
                                _playerManager.Player.Intelligence++;
                                _playerManager.Player.SkillPoints--;
                            }
                            else if (choice == "2")
                            {
                                _playerManager.Player.Strength++;
                                _playerManager.Player.SkillPoints--;
                            }
                            else if (choice == "3")
                            {
                                _playerManager.Player.Agility++;
                                _playerManager.Player.SkillPoints--;
                            }
                            else if (choice == "4")
                            {
                                spending = false; // exit spending loop
                            }
                            else
                            {
                                _outputManager.WriteLine("Invalid entry. Try again.", ConsoleColor.Red);
                                _outputManager.Display();
                                Thread.Sleep(1000);
                            }

                            if (_playerManager.Player.SkillPoints <= 0)
                            {
                                spending = false;
                                _outputManager.WriteLine("You have spent all your skill points.", ConsoleColor.Green);
                                _outputManager.Display();
                                Thread.Sleep(1000);
                            }
                        }
                    }
                }
                else if (menuChoice == "2")
                {
                    exitMenu = true; // go back to previous menu
                }
                else
                {
                    _outputManager.WriteLine("Invalid entry", ConsoleColor.Red);
                    _outputManager.Display();
                    Thread.Sleep(1000);
                }

            }
        }

        public void DisplayAbilitiesMenu(Player player)
        {
            // 1. Fetch the player's abilities
            var abilities = player.Abilities.ToList();

            // Check if the player has any abilities
            if (abilities == null || !abilities.Any())
            {
                _outputManager.WriteLine("You have no abilities to display!", ConsoleColor.Red);
                _outputManager.Display();
                Thread.Sleep(1000);
                return;
            }

            _outputManager.Clear();
            _outputManager.WriteLine("--- Player Abilities ---", ConsoleColor.Cyan);

            // 2. Define the Column Headers and Spacing
            // We'll use fixed spacing for neat alignment.
            // Columns: [ID] [NAME] [DESCRIPTION] [TYPE] [MANA COST]
            string headerFormat = "{0,-3} | {1,-20} | {2,-65} | {3,-10} | {4,-10}";

            // Display the Header Row
            _outputManager.WriteLine(
                string.Format(headerFormat, "ID", "Name", "Description", "Type", "Mana Cost"),
                ConsoleColor.Yellow);

            // Draw a Separator Line
            _outputManager.WriteLine(new string('-', 120), ConsoleColor.DarkGray);

            // 3. Loop Through and Display Abilities
            int counter = 1;
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

            // 4. Prompt for exit
            _outputManager.WriteLine(new string('-', 120), ConsoleColor.DarkGray);
            _outputManager.WriteLine("Press any key to return to the Player Menu...", ConsoleColor.Green);
            _outputManager.Display(); // Ensure all output is flushed to the console
            Console.ReadKey(true);
        }

        public void DisplayPlayerDetailsWithBorder()
        {
            // 1. Get the content lines
            var contentLines = _playerManager.DisplayDetails().ToList();

            if (contentLines.Count == 0)
            {
                _outputManager.WriteLine("No player details available.", ConsoleColor.Yellow);
                return;
            }

            // 2. Calculate the Required Width
            // Find the length of the longest line, and add padding for the side borders.
            int maxContentWidth = contentLines.Max(line => line.Length);
            int totalWidth = maxContentWidth + 4; // 2 spaces of padding + 2 vertical bars (| |)

            // 3. Define the Border Characters
            // Use simple ASCII characters: + for corners, - for horizontal, | for vertical.
            char horizontal = '-';
            char vertical = '|';
            char corner = '+';
            char padding = ' ';

            // 4. Draw the TOP BORDER
            // Example: +-----------------+
            _outputManager.WriteLine(
                corner + new string(horizontal, totalWidth - 2) + corner,
                ConsoleColor.DarkCyan);

            // 5. Draw the CONTENT LINES with Side Borders
            foreach (var line in contentLines)
            {
                // Calculate the number of spaces needed to fill the rest of the line width
                int paddingNeeded = maxContentWidth - line.Length;
                string paddedLine = line + new string(padding, paddingNeeded);

                // Example: |  Player Name: Bob  |
                _outputManager.WriteLine(
                    vertical + padding + paddedLine + padding + vertical,
                    ConsoleColor.White);
            }

            // 6. Draw the BOTTOM BORDER (Same as the top)
            _outputManager.WriteLine(
                corner + new string(horizontal, totalWidth - 2) + corner,
                ConsoleColor.DarkCyan);

            _outputManager.Display(); // Flush output
        }
    }
}
