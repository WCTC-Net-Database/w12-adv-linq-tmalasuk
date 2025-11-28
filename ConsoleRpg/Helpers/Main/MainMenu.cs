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
            _outputManager.Clear();

            var heroDetails = _playerManager.DisplayDetails();
            string[] boxedHero = _outputManager.BoxPanel(heroDetails);
            string[] menuOptions = { "Start Adventure", "New Hero", "Load Hero", "Admin", "Quit" };
            string[] boxedMenu = _outputManager.CraftMenu(menuOptions, boxedHero.Count(), "MENU", 35);
            int totalWidth = boxedMenu[0].Length + boxedHero[0].Length;         
            if (AsciiArt.Art.TryGetValue("Title", out var title))
            {
                foreach (var line in title)
                {
                    int lineWidth = line.Length;
                    int centerOffset = (totalWidth / 4) - 6;
                    centerOffset = Math.Max(centerOffset, 0); 
                    _outputManager.WriteLine(new string(' ', centerOffset) + line);
                }
                _outputManager.WriteLine("");
            }
            var mainMenuLastLine = _outputManager.PrintSideBySide(boxedHero, boxedMenu, 0);
            
            int underMenu = Console.GetCursorPosition().Top;


            return HandleMainMenuInput(underMenu, mainMenuLastLine);
        }


        private bool HandleMainMenuInput(int underMenu, int mainMenuLastLine)
        {
            _outputManager.ClearBelow(underMenu);

            var input = Console.ReadLine();

            switch (input)
            {
                case "1":
                    if (_playerManager.Player != null)
                    {
                        _outputManager.ClearBelow(underMenu);
                        _outputManager.WriteLine(" Starting game...", ConsoleColor.Green);
                        _outputManager.Display();
                        return true; 
                    }
                    else
                    {
                        _outputManager.DisplayErrorBelow(" Please select hero first.", underMenu);
                        return false;
                    }
                case "2":
                    CreateHeroMenu(underMenu, mainMenuLastLine);
                    return false;
                case "3":
                    LoadHeroMenu(underMenu);
                    return false;
                case "4":
                    _adminMenu.ShowAdminMenu(underMenu);
                    return false;
                case "5":
                    _outputManager.ClearBelow(underMenu);
                    _outputManager.WriteLine(" Exiting...", ConsoleColor.Red);
                    _outputManager.Display();
                    Environment.Exit(0);
                    return true;

                default:
                    _outputManager.DisplayErrorBelow(" Invalid Choice.", underMenu);
                    return false;
            }
        }


        public void CreateHeroMenu(int underMenu, int mainMenuLastLine)
        {
            _outputManager.ClearBelow(underMenu);
            var name = "";
            var playerClass = "";
            while (true)
            {
                _outputManager.ClearBelow(underMenu);
                _outputManager.Write(" Enter Hero's Name: ", ConsoleColor.White);
                _outputManager.Display();
                name = Console.ReadLine();           
                if (string.IsNullOrWhiteSpace(name) || _playerManager.GetPlayers().FirstOrDefault(p => p.Name.ToLower() == name) != null)
                {
                    _outputManager.DisplayErrorBelow(" Name taken or not valid. Please try again.", underMenu);

                    continue;
                }
                else
                {
                    break;
                }
            }
            
            while (true)
            {
                _outputManager.ClearBelow(underMenu);
                ChooseClassMenu(underMenu, mainMenuLastLine);
                
                _outputManager.Display();
                underMenu = Console.GetCursorPosition().Top;
                _outputManager.WriteandDisplay(" Choose your class >> ");              
                playerClass = Console.ReadLine();
                switch (playerClass)
                {
                    case "1":
                        playerClass = "Knight";
                        break;
                    case "2":
                        playerClass = "Mage";
                        break;
                    case "3":
                        playerClass = "Archer";
                        break;
                    default:
                        _outputManager.DisplayErrorBelow("Invalid input", underMenu);
                        continue;
                }
                break;
            }

            _outputManager.ClearBelow(underMenu);
            _outputManager.WriteLine(" Creating your hero...", ConsoleColor.Green);
            _outputManager.Display();
            Thread.Sleep(1500);
            _playerManager.CreatePlayer(name, playerClass);
        }

        public void ChooseClassMenu(int underMenu, int mainMenuLastLine)
        {
            int totalWidth = mainMenuLastLine - 2;

            int leftPad = 1;
            string pad = new string(' ', leftPad);

            int classColWidth = 11; // Shrink class column
            int paddingForBorders = 6; // "| " + " | " + " |" = 6 total border chars

            // Calculate description width
            int descColWidth = totalWidth - paddingForBorders - classColWidth;
            if (descColWidth < 15)
                descColWidth = 15;

            // Build a row without wrapping, just honoring \n
            string BuildRow(string c, string d)
            {
                // Split the description by newline
                var lines = d.Split('\n');

                List<string> rows = new List<string>();

                for (int i = 0; i < lines.Length; i++)
                {
                    if (i == 0)
                    {
                        // First line shows the class label
                        rows.Add(
                            pad + "| " +
                            c.PadRight(classColWidth) +
                            " | " +
                            lines[i].PadRight(descColWidth) +
                            " |"
                        );
                    }
                    else
                    {
                        // Additional lines: class column is empty
                        rows.Add(
                            pad + "| " +
                            new string(' ', classColWidth) +
                            " | " +
                            lines[i].PadRight(descColWidth) +
                            " |"
                        );
                    }
                }

                return string.Join("\n", rows);
            }

            // Separator line — must match visible width
            string separator = pad + "+" + new string('-', totalWidth - 1) + "+";

            _outputManager.WriteLine(
                BuildRow("[1] Knight", "STRENGTH - \nhigh carrying capacity and raw attack damage."),
                ConsoleColor.White
            );
            _outputManager.WriteLine(separator, ConsoleColor.White);

            _outputManager.WriteLine(
                BuildRow("[2] Mage", "INTELLIGENCE - \nhigh mana pool, excels at casting spells."),
                ConsoleColor.White
            );
            _outputManager.WriteLine(separator, ConsoleColor.White);

            _outputManager.WriteLine(
                BuildRow("[3] Archer", "AGILITY - \nincreased dodge chance and crafty explorers."),
                ConsoleColor.White
            );

            _outputManager.WriteLine(separator, ConsoleColor.Gray);
        }



        public void LoadHeroMenu(int underMenu)
        {
            
            if (_playerManager.GetPlayers().Count == 0)
            {
                _outputManager.DisplayErrorBelow(" No heros to load. Create a new hero.", underMenu);
            }
            else
            {
               
                while (true)
                {
                    _outputManager.Clear();
                    var playerLines = _playerManager.PlayersTable(_playerManager.GetPlayers())
                                    .Split(Environment.NewLine);
                    var boxedPlayers = _outputManager.BoxPanel(playerLines);
                    foreach (string line in boxedPlayers)
                    {
                        _outputManager.WriteLine($" {line}");
                    }
                    _outputManager.Display();
                    underMenu = Console.GetCursorPosition().Top;
                    _outputManager.WriteandDisplay(" Enter Name: ");
                    var name = Console.ReadLine();
                    if (HeroSheet(name)) break;
                    else
                    {
                        _outputManager.DisplayErrorBelow(" No hero with that name exists", underMenu);
                        continue;
                    }
                    
                }
            }
        }

        private bool HeroSheet(string name)
        {        
            _playerManager.selectPlayer(name);
            if (_playerManager.Player == null)
            {
                return false;
            }
            _outputManager.Clear();
            var heroDetails = _playerManager.Player.ToString().TrimEnd().Split(Environment.NewLine);
            string[] boxedHero = _outputManager.BoxPanel(heroDetails);
            string[] menuOptions = { "", "[1] Select", "[2] Delete" };
            _outputManager.PrintSideBySide(boxedHero, menuOptions, 2);
            
            int underMenu = Console.GetCursorPosition().Top;
            while (true)
            {
                var choice = Console.ReadLine();
                switch (choice)
                {
                    case "1":
                        return true;
                    case "2":
                        _playerManager.DeletePlayer(name);
                        _playerManager.Player = null;
                        return true;
                    default:
                        _outputManager.DisplayErrorBelow(" Invalid selection.", underMenu);
                        _outputManager.ClearBelow(underMenu);
                        continue;
                }
            }
        }

    }
}
