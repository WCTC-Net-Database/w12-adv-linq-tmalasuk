using ConsoleRpg.Helpers.EntityHelper;
using ConsoleRpg.Helpers.Menus;
using ConsoleRpgEntities.Data;
using ConsoleRpgEntities.Models.Abilities.PlayerAbilities;
using ConsoleRpgEntities.Models.Characters;
using ConsoleRpgEntities.Models.Containers;
using ConsoleRpgEntities.Models.Equipments;
using ConsoleRpgEntities.Models.Rooms;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static ConsoleRpgEntities.Models.Enums;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace ConsoleRpg.Helpers.Admin
{
    public class AdminBrains
    {
        private readonly OutputManager _outputManager;
        private readonly PlayerManager _playerManager;
        private readonly GameContext _context;
        
        public AdminBrains(OutputManager outputManager, PlayerManager playerManager, GameContext context)
        {
            _context = context;
            _outputManager = outputManager;
            _playerManager = playerManager;
            
        }

        public Player QueryPlayers()
        {
            _outputManager.Clear();

            // If no players exist
            if (_playerManager.GetPlayers().Count == 0)
            {
                _outputManager.Clear();
                _outputManager.WriteLine("No heroes to load. Create a new hero.", ConsoleColor.Red);
                _outputManager.Display();
                Thread.Sleep(1500);
                return null;
            }

            // Draw initial table
            _outputManager.WriteLine(_playerManager.PlayersTable(_playerManager.GetPlayers()));
            Console.WriteLine();
            _outputManager.Display();

            int tableEndTop = Console.GetCursorPosition().Top;

            // MAIN LOOP
            while (true)
            {
                _outputManager.ClearBelow(tableEndTop);

                _outputManager.Write("Select: ", ConsoleColor.Cyan);
                _outputManager.Write("1. Query 2. Select Player for Editing 3. Back ");
                _outputManager.Write(">> ");
                _outputManager.Display();

                var choice = Console.ReadLine();

                // ========================================
                // 1. QUERY PLAYERS
                // ========================================
                if (choice == "1")
                {
                    _outputManager.ClearBelow(tableEndTop);
                    _outputManager.Write("Query names: ");
                    _outputManager.Display();

                    var desiredName = Console.ReadLine();

                    var players = _context.Players
                        .Where(p => p.Name.ToLower().Contains(desiredName.ToLower()))
                        .ToList();

                    // Redraw filtered table under original table header
                    _outputManager.Clear();
                    _outputManager.WriteLine(_playerManager.PlayersTable(players));
                    _outputManager.Display();

                    tableEndTop = Console.GetCursorPosition().Top;
                    continue;
                }

                // ========================================
                // 2. SELECT PLAYER
                // ========================================
                if (choice == "2")
                {
                    _outputManager.ClearBelow(tableEndTop);
                    _outputManager.Write("Enter player name to select: ");
                    _outputManager.Display();

                    var nameToEdit = Console.ReadLine();

                    var player = _context.Players
                        .FirstOrDefault(p => p.Name.ToLower() == nameToEdit.ToLower());

                    if (player == null)
                    {
                        _outputManager.ClearBelow(tableEndTop);
                        _outputManager.WriteLine("Player not found.", ConsoleColor.Red);
                        _outputManager.Display();
                        Thread.Sleep(1200);
                        continue;
                    }

                    // SUCCESS — return player for editing
                    return player;
                }

                // ========================================
                // 3. BACK
                // ========================================
                if (choice == "3")
                {
                    _outputManager.Clear();
                    return null;
                }

                // ========================================
                // INVALID INPUT
                // ========================================
                _outputManager.ClearBelow(tableEndTop);
                _outputManager.WriteLine("Invalid selection. Choose 1, 2, or 3.", ConsoleColor.Red);
                _outputManager.Display();
                Thread.Sleep(1200);
            }
        }

        public void AdminNewPlayer()
        {
            _outputManager.Clear();
            var newPlayer = new Player();

            // ---------- Name ----------
            while (true)
            {
                _outputManager.Write("Enter player name: ");
                _outputManager.Display();
                var nameInput = Console.ReadLine()?.Trim();

                if (string.IsNullOrEmpty(nameInput))
                {
                    _outputManager.Clear();
                    _outputManager.WriteLine("Name cannot be empty.", ConsoleColor.Red);
                    _outputManager.Display();
                    Thread.Sleep(1500);
                    continue;
                }

                // Check for uniqueness
                if (_context.Players.Any(p => p.Name.ToLower() == nameInput.ToLower()))
                {
                    _outputManager.Clear();
                    _outputManager.WriteLine("A player with this name already exists. Choose another.", ConsoleColor.Red);
                    _outputManager.Display();
                    Thread.Sleep(1500);
                    continue;
                }

                newPlayer.Name = nameInput;
                break;
            }

            // ---------- Level ----------

            while (true)
            {
                _outputManager.Clear();
                _outputManager.Write("Enter level: ");
                _outputManager.Display();
                
                var stringLevel = Console.ReadLine();
                if (int.TryParse(stringLevel, out int number))
                {
                    newPlayer.Level = number;
                    break;
                }
                else
                {
                    _outputManager.Clear();
                    _outputManager.WriteLine("Invalid number", ConsoleColor.Red);
                    _outputManager.Display();
                    Thread.Sleep(1500);
                    Thread.Sleep(1500);
                }
            }

            // ----------- Strength, Intelligence, Agility -----------

            while (true)
            {
                _outputManager.Clear();
                _outputManager.Write("Enter Strength: ");
                _outputManager.Display();
                var stringLevel = Console.ReadLine();
                if (int.TryParse(stringLevel, out int number))
                {
                    newPlayer.Strength = number;
                    break;
                }
                else
                {
                    _outputManager.Clear();
                    _outputManager.WriteLine("Invalid number", ConsoleColor.Red);
                    _outputManager.Display();
                    Thread.Sleep(1500);

                }
            }

            while (true)
            {
                _outputManager.Clear();
                _outputManager.Write("Enter Intelligence: ");
                _outputManager.Display();
                var stringLevel = Console.ReadLine();
                if (int.TryParse(stringLevel, out int number))
                {
                    newPlayer.Intelligence = number;
                    break;
                }
                else
                {
                    _outputManager.Clear();
                    _outputManager.WriteLine("Invalid number", ConsoleColor.Red);
                    _outputManager.Display();
                    Thread.Sleep(1500);

                }
            }

            while (true)
            {
                _outputManager.Clear();
                _outputManager.Write("Enter Agility: ");
                _outputManager.Display();
                var stringLevel = Console.ReadLine();
                if (int.TryParse(stringLevel, out int number))
                {
                    newPlayer.Agility = number;
                    break;
                }
                else
                {
                    _outputManager.Clear();
                    _outputManager.WriteLine("Invalid number", ConsoleColor.Red);
                    _outputManager.Display();
                    Thread.Sleep(1500);
        
                }
            }

            // ---------- Class ----------
            while (true)
            {
                _outputManager.Clear();
                _outputManager.WriteLine("Select class type:");
                foreach (var val in Enum.GetValues(typeof(PlayerClass)))
                {
                    _outputManager.WriteLine($"{(int)val} - {val}");
                }
                _outputManager.Write(">> ");
                _outputManager.Display();
                var classInput = Console.ReadLine();
                if (Enum.TryParse<PlayerClass>(classInput, out var classType))
                {
                    newPlayer.classType = classType;
                    break;
                }
                
                else
                {
                    _outputManager.WriteLine("Invalid class selection. Please enter the number corresponding to the class.", ConsoleColor.Red);
                    _outputManager.Display();
                    Thread.Sleep(1500);
                }
            }

            // ---------- Initialize Health & Mana ----------
            newPlayer.Health = newPlayer.MaxHealth;
            newPlayer.Mana = newPlayer.MaxMana;

            // ---------- Inventory & Equipped ----------
            newPlayer.Inventory = new Inventory
            {
                Items = new List<Item>()
            };
            newPlayer.Equipped = new Equipped();

            // ---------- Save to Database ----------
            newPlayer.CurrentRoom = _context.Rooms.First(r => r.Id == 1);
            _context.Players.Add(newPlayer);
            _context.SaveChanges();

            _outputManager.Clear();
            Console.WriteLine($"Player '{newPlayer.Name}' created successfully with ID {newPlayer.Id}!", ConsoleColor.Green);
            _outputManager.Display();
            Thread.Sleep(1500);
        }

        public void AdminDeletePlayer(Player player)
        {
            _context.Players.Remove(player);
            _context.SaveChanges();
        }

        internal void GrantAbilityToPlayer(int tableEndTop, Player player)
        {
            Ability retrievedAbility = null;
            while (true) 
            {
                _outputManager.ClearBelow(tableEndTop);
                _outputManager.Write("Enter ID of Ability: ");
                _outputManager.Display();
                var ability = Console.ReadLine();
                
                if (int.TryParse(ability, out int number))
                {

                    retrievedAbility = _context.Abilities.FirstOrDefault(a => a.Id == number);

                    if (retrievedAbility != null)
                    {
                        break;
                    }
                    else
                    {
                        _outputManager.ClearBelow(tableEndTop);
                        _outputManager.WriteLine($"No ability found with ID: {number}", ConsoleColor.Red);
                        _outputManager.Display();
                        Thread.Sleep(1000);
                        continue;
                    }
                }
                else
                {
                    _outputManager.WriteLine("Invalid number", ConsoleColor.Red);
                    _outputManager.Display();
                    Thread.Sleep(1500);
                }   
             }

            
            player.Abilities.Add(retrievedAbility);
            _context.SaveChanges();

            _outputManager.Clear();
            _outputManager.WriteLine($"{player.Name} has been granted the ability {retrievedAbility.Name}!", ConsoleColor.Green);
            _outputManager.Display();
            Thread.Sleep(1500);

        }

        public void AdminCreateAbility()
        {
            Ability ability = null;

            
            // CLASS ------------
            while (true)
            {
                _outputManager.WriteLine("========= Ability Classes =========");
                _outputManager.WriteLine("[1] Arcane Barrage    | Returns damage based on intelligence");
                _outputManager.WriteLine("[2] Nature Embrace    | Returns large heal and linger HoT based on intelligence");
                _outputManager.WriteLine("[3] Nullifying Aegis  | Returns a deflection shield");
                _outputManager.WriteLine("[4] Shadow Veil       | Returns damage based on agility with chance to stun based on agility");
                _outputManager.WriteLine("[5] Siphon Strike     | Returns damage based on sum of all skills multiplied, heals for same amount");
                _outputManager.Display();
                int tableEndTop = Console.GetCursorPosition().Top;
                _outputManager.ClearBelow(tableEndTop);
                _outputManager.Write("Please select class number >> ");
                
                _outputManager.Display();
                var classChoice = Console.ReadLine();
                if (classChoice == "1")
                {
                    ability = new ArcaneBarrage();
                    ability.AbilityType = "Arcane";
                    break;
                }
                else if (classChoice == "2")
                {
                    ability = new NatureEmbrace();
                    ability.AbilityType = "Healing";
                    break;
                }
                else if (classChoice == "3")
                {
                    ability = new NullifyingAegis();
                    ability.AbilityType = "Defensive";
                    break;
                }
                else if (classChoice == "4")
                {
                    ability = new ShadowVeil();
                    ability.AbilityType = "Physical";
                    break;
                }
                else if (classChoice == "3")
                {
                    ability = new SiphoningStrike();
                    ability.AbilityType = "Hybrid";
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
            _outputManager.Clear();
            // NAME ------------------
            while (true)
            {
                _outputManager.Write("Enter Ability Name: ");
                _outputManager.Display();
                var nameInput = Console.ReadLine()?.Trim();

                if (string.IsNullOrEmpty(nameInput))
                {
                    _outputManager.Clear();
                    _outputManager.WriteLine("Name cannot be empty.", ConsoleColor.Red);
                    _outputManager.Display();
                    Thread.Sleep(1500);
                    continue;
                }

                // Check for uniqueness
                if (_context.Abilities.Any(p => p.Name.ToLower() == nameInput.ToLower()))
                {
                    _outputManager.Clear();
                    _outputManager.WriteLine("An ability with this name already exists. Choose another.", ConsoleColor.Red);
                    _outputManager.Display();
                    Thread.Sleep(1500);
                    continue;
                }

                ability.Name = nameInput;
                break;
            }
            // DESCRIPTION ------------------
            _outputManager.Clear();
            while (true)
            {
                _outputManager.Write("Enter Description: ");
                _outputManager.Display();
                var descriptionInput = Console.ReadLine()?.Trim();

                if (string.IsNullOrEmpty(descriptionInput))
                {
                    _outputManager.Clear();
                    _outputManager.WriteLine("Description cannot be empty.", ConsoleColor.Red);
                    _outputManager.Display();
                    Thread.Sleep(1500);
                    continue;
                }

                ability.Description = descriptionInput;
                break;
            }
            
            // ManaCost ------------------
            _outputManager.Clear();
            while (true)
            {
                _outputManager.Clear();
                _outputManager.Write("Enter ManaCost: ");
                _outputManager.Display();
                var stringLevel = Console.ReadLine();
                if (int.TryParse(stringLevel, out int number))
                {
                    ability.ManaCost = number;
                    break;
                }
                else
                {
                    _outputManager.Clear();
                    _outputManager.WriteLine("Invalid number", ConsoleColor.Red);
                    _outputManager.Display();
                    Thread.Sleep(1500);

                }
            }
            // Buff duration --------------
            while (true)
            {
                _outputManager.Clear();
                _outputManager.Write("Enter Buff Duration: ");
                _outputManager.Display();
                var stringLevel = Console.ReadLine();
                if (int.TryParse(stringLevel, out int number))
                {
                    ability.BuffDuration = number;
                    break;
                }
                else
                {
                    _outputManager.Clear();
                    _outputManager.WriteLine("Invalid number", ConsoleColor.Red);
                    _outputManager.Display();
                    Thread.Sleep(1500);

                }
            }

            _context.Abilities.Add(ability);
            _context.SaveChanges();

            _outputManager.Clear();
            _outputManager.WriteLine($"{ability.Name} has been saved!");
            _outputManager.Display();
            Thread.Sleep(1500);
        }

        public void PutPlayerInRoom(int tableEndTop, Player SelectedPlayer)
        {
            if (SelectedPlayer == null)
            {
                _outputManager.ClearBelow(tableEndTop);
                _outputManager.WriteLine("No Player Selected", ConsoleColor.Red);
                _outputManager.Display();
                Thread.Sleep(1500);
            }
            else
            {
                _outputManager.ClearBelow(tableEndTop);
                _outputManager.WriteLine("Enter Room by ID: ");
                _outputManager.Display();
                var roomId = Console.ReadLine();
                if (int.TryParse(roomId, out int number) && _context.Rooms.Any(r => r.Id == number))
                {
                    SelectedPlayer.CurrentRoom.Id = number;
                }
                else
                {
                    _outputManager.ClearBelow(tableEndTop);
                    _outputManager.WriteLine("Invalid number", ConsoleColor.Red);
                    _outputManager.Display();
                    Thread.Sleep(1500);
                }
            }
        }

        internal void AdminCreateRoom(int tableEndTop)
        {
            Room room;
            while (true)
            {
                _outputManager.ClearBelow(tableEndTop);
                _outputManager.WriteLine("[1] Armory\n[2] Barracks\n[3]Dungeon\n[4] Garden\n[5] Guard Room\n[6] Hallway\n[7] Scullery\n[8] Stairwell\n[9] Torture Chamber");
                _outputManager.Write(">>");
                _outputManager.Display();
                var type = Console.ReadLine();
                switch (type)
                {
                    case "1": room = new Armory(); break;
                    case "2": room = new Barracks(); break;
                    case "3": room = new Dungeon(); break;
                    case "4": room = new Garden(); break;
                    case "5": room = new GuardRoom(); break;
                    case "6": room = new Hallway(); break;
                    case "7": room = new Scullery(); break;
                    case "8": room = new Stairwell(); break;
                    case "9": room = new TortureChamber(); break;

                    default:
                        _outputManager.WriteLine("Invalid input. Please choose an option 1–9.");
                        _outputManager.Display();
                        Thread.Sleep(1000); 
                        continue; 
                }

                break;
            }
            while (true)
            {
                _outputManager.ClearBelow(tableEndTop);
                _outputManager.Write("Enter room name: ");
                _outputManager.Display();
                var roomName = Console.ReadLine();
                if (string.IsNullOrEmpty(roomName))
                {
                    _outputManager.Clear();
                    _outputManager.WriteLine("Name cannot be empty.", ConsoleColor.Red);
                    _outputManager.Display();
                    Thread.Sleep(1500);
                    continue;
                }

                // Check for uniqueness
                else if (_context.Rooms.Any(p => p.Name.ToLower() == roomName.ToLower()))
                {
                    _outputManager.Clear();
                    _outputManager.WriteLine("A room with this name already exists. Choose another.", ConsoleColor.Red);
                    _outputManager.Display();
                    Thread.Sleep(1500);
                    continue;
                }
                else
                {
                    room.Name = roomName;
                    break;
                }

            }
            while (true)
            {
                _outputManager.ClearBelow(tableEndTop);
                _outputManager.Write("Enter description: ");
                _outputManager.Display();
                var description = Console.ReadLine();
                if (string.IsNullOrEmpty(description))
                {
                    _outputManager.Clear();
                    _outputManager.WriteLine("Description cannot be empty.", ConsoleColor.Red);
                    _outputManager.Display();
                    Thread.Sleep(1500);
                    continue;
                }
                else
                {
                    room.Description = description;
                    break;
                }
            }

            _context.Rooms.Add(room);
            _context.SaveChanges();
        }

        internal void QueryRoomPlayers(int tableEndTop)
        {
            _outputManager.ClearBelow(tableEndTop);
            _outputManager.Write("Enter Room by ID: ");
            _outputManager.Display();
            var roomId = Console.ReadLine();
            if (int.TryParse(roomId, out int number) && _context.Rooms.Any(r => r.Id == number))
            {
                _outputManager.Clear();
                List<Player> players = new List<Player>();
                foreach (Player player in _context.Players.Where(p => p.CurrentRoom.Id == number))
                {
                    players.Add(player);
                }
                _outputManager.WriteLine(_playerManager.PlayersTable(players));
                _outputManager.Display();
                var playerID = Console.ReadLine();
            }
            else
            {
                _outputManager.ClearBelow(tableEndTop);
                _outputManager.WriteLine("Invalid number", ConsoleColor.Red);
                _outputManager.Display();
                Thread.Sleep(1500);
            }
        }
    }
    
}
