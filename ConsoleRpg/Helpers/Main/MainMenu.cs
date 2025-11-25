using ConsoleRpg.Helpers.Admin;
using ConsoleRpg.Helpers.EntityHelper;
using ConsoleRpgEntities.Models.Characters;
using ConsoleRpgEntities.Models.Equipments;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleRpg.Helpers.Menus
{
    public class MainMenu
    {
        private readonly OutputManager _outputManager;
        private readonly InventoryManager _inventoryManager;
        private readonly PlayerManager _playerManager;
        private readonly AdminMenu _adminMenu;

        public MainMenu(OutputManager outputManager, InventoryManager inventoryManager, PlayerManager playerManager, AdminMenu adminMenu)
        {
            _outputManager = outputManager;
            _inventoryManager = inventoryManager;
            _playerManager = playerManager;
            _adminMenu = adminMenu;
        }

        
        public bool ShowMainMenu()
        {
            Console.Clear();

            // Draw title from dictionary
            if (AsciiArt.Art.TryGetValue("Title", out var title))
            {
                foreach (var line in title)
                    Console.WriteLine(line);
            }

            Console.WriteLine();

            // Get hero details (already padded)
            var heroDetails = _playerManager.DisplayDetails();

            // Box the hero details
            string[] boxedHero = BoxPanel(heroDetails);

            // Right-side menu
            string[] menuPanel = new string[]
            {
        "==================== MENU ====================",
        "                                             |",
        "  [1] Start Adventure                        |",
        "  [2] New Hero                               |",
        "  [3] Load Hero                              |",
        "  [4] Admin                                  |",
        "  [5] Quit                                   |",
        "                                             |",
        "=============================================="
            };

            // Determine number of rows to print
            int rows = Math.Max(boxedHero.Length, menuPanel.Length);

            int heroOffset = 5; // spaces from left edge
            int menuOffset = heroOffset + boxedHero.Max(line => line.Length) + 3; // 3 spaces between panels

            for (int i = 0; i < rows; i++)
            {
                string left = i < boxedHero.Length ? new string(' ', heroOffset) + boxedHero[i] : "".PadLeft(heroOffset + boxedHero.Max(l => l.Length));
                string right = i < menuPanel.Length ? menuPanel[i].PadRight(boxedHero.Max(l => l.Length)) : "";
                Console.WriteLine(left + right);
            }


            return HandleMainMenuInput();
        }

        // Helper method to box a panel of strings
        private string[] BoxPanel(string[] lines)
        {
            int maxWidth = lines.Max(line => line.Length);
            string border = "=" + new string('=', maxWidth + 2) + "=";

            string[] boxed = new string[lines.Length + 2];
            boxed[0] = border;

            for (int i = 0; i < lines.Length; i++)
            {
                boxed[i + 1] = "| " + lines[i].PadRight(maxWidth) + " |";
            }

            boxed[boxed.Length - 1] = border;

            return boxed;
        }


        private bool HandleMainMenuInput()
        {
            _outputManager.Write(">> ", ConsoleColor.White);
            _outputManager.Display();

            var input = Console.ReadLine();

            switch (input)
            {
                case "1":
                    if (_playerManager.Player != null)
                    {
                        _outputManager.WriteLine("Starting game...", ConsoleColor.Green);
                        _outputManager.Display();
                        return true; // ← EXIT MENU AND START GAME
                    }
                    else
                    {
                        _outputManager.WriteLine("Please select a hero first.", ConsoleColor.Red);
                        _outputManager.Display();
                        Thread.Sleep(1000);
                        return false;
                    }
                case "2":
                    CreateHeroMenu();
                    return false;
                case "3":
                    LoadHeroMenu();
                    return false;
                case "4":
                    _adminMenu.ShowAdminMenu();
                    return false;
                case "5":
                    _outputManager.WriteLine("Exiting...", ConsoleColor.Red);
                    _outputManager.Display();
                    Environment.Exit(0);
                    return true;

                default:
                    _outputManager.WriteLine("Invalid choice.", ConsoleColor.Red);
                    _outputManager.Display();
                    return false;
            }
        }


        public void CreateHeroMenu()
        {
            bool namevalid = false;
            var name = "";
            var playerClass = "";
            while (!namevalid)
            {
                _outputManager.Clear();
                _outputManager.Write("Enter Hero's Name: ", ConsoleColor.White);
                _outputManager.Display();
                name = Console.ReadLine();
                var currentPlayers = _playerManager.GetPlayers();
                List<string> currentPlayerNames = new List<string>();
                foreach (Player player in currentPlayers)
                {
                    currentPlayerNames.Add(player.Name);
                }

                if (string.IsNullOrWhiteSpace(name) || currentPlayerNames.Contains(name))
                {
                    _outputManager.Clear();
                    _outputManager.WriteLine("Name taken or not valid. Please try again.", ConsoleColor.Red);
                    _outputManager.Display();
                    Thread.Sleep(1500);
                    continue;
                }
                else
                {
                    namevalid = true;
                }
            }
            _outputManager.Clear();
            bool classvalid = false;

            while (!classvalid)
            {
                _outputManager.WriteLine($"Is {name} a [1] Knight [2] Mage [3] Archer", ConsoleColor.White);
                _outputManager.Write(">> ");
                _outputManager.Display();
                playerClass = Console.ReadLine();
                switch (playerClass)
                {
                    case "1":
                        playerClass = "Knight";
                        classvalid = true;
                        break;
                    case "2":
                        playerClass = "Mage";
                        classvalid = true;
                        break;
                    case "3":
                        playerClass = "Archer";
                        classvalid = true;
                        break;
                    default:
                        _outputManager.Clear();
                        _outputManager.WriteLine("That wasn't a valid response. Please try again.", ConsoleColor.Red);
                        _outputManager.Display();
                        break;
                }
            }

            _outputManager.Clear();
            _outputManager.WriteLine("Creating your hero...", ConsoleColor.Green);
            _outputManager.Display();
            Thread.Sleep(1000);
            _outputManager.Clear();
            _playerManager.CreatePlayer(name, playerClass);
        }

        public void LoadHeroMenu()
        {
            _outputManager.Clear();
            if (_playerManager.GetPlayers().Count == 0)
            {
                _outputManager.WriteLine("No heros to load. Create a new hero.", ConsoleColor.Red);
                _outputManager.Display();
                Thread.Sleep(1000);
            }
            else
            {
                _outputManager.WriteLine(_playerManager.PlayersTable(_playerManager.GetPlayers()));
                Console.WriteLine();
                _outputManager.Write("Select name you'd like to interact with >> ");
                _outputManager.Display();
                var desiredName = Console.ReadLine();
                _outputManager.Clear();
                _playerManager.selectPlayer(desiredName);
                if (_playerManager.Player == null)
                {
                    _outputManager.WriteLine("That was not a valid entry. Please try again.", ConsoleColor.Red);
                    _outputManager.Display();
                    Thread.Sleep(1000);
                    LoadHeroMenu();
                }
                bool successfulSelection = false;
                _outputManager.WriteLine(_playerManager.Player.ToString());
                _outputManager.WriteLine("Would you like to [1] Select Hero for Adventure or [2] Delete Hero");
                while (!successfulSelection)
                {
                    _outputManager.Write(">> ");
                    _outputManager.Display();
                    var menuChoice = Console.ReadLine();
                    switch (menuChoice)
                    {
                        case "1":
                            successfulSelection = true;

                            break;
                        case "2":
                            successfulSelection = true;
                            _playerManager.DeletePlayer(desiredName);
                            _playerManager.Player = null;
                            break;
                        default:
                            _outputManager.WriteLine("Not a valid request. Please try again", ConsoleColor.Red);
                            _outputManager.Display();
                            Thread.Sleep(1000);
                            break;
                    }
                }
            }
        }

    }
}
