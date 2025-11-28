using ConsoleRpgEntities.Data;
using ConsoleRpgEntities.Models;
using ConsoleRpgEntities.Models.Abilities.PlayerAbilities;
using ConsoleRpgEntities.Models.Characters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using static ConsoleRpgEntities.Models.Enums;

namespace ConsoleRpg.Helpers.EntityHelper
{
    public class PlayerManager
    {
        public Player Player;
        public GameContext Context { get; }
        public PlayerManager(GameContext context) 
        {
            Context = context;
        }

        public void CreatePlayer(string name, string playerClass)
        {
            var newPlayer = new Player(name, (Enums.PlayerClass)Enum.Parse(typeof(Enums.PlayerClass), playerClass));

            newPlayer.CurrentRoom = Context.Rooms.First(r => r.Id == 1);
            Context.Players.Add(newPlayer);
            Context.SaveChanges();
            selectPlayer(newPlayer.Name);
        }

        public void DeletePlayer(string playerName)
        {
            var player = Context.Players
                    .FirstOrDefault(p => p.Name.ToLower() == playerName.ToLower());
            if (player == null)
            {
                throw new Exception("Player not found");
            }
            Context.Players.Remove(player);
            Context.SaveChanges();
        }

        public List<Player> RetrievePlayers()
        {
             return Context.Players.ToList();          
        }

        public Player selectPlayer(string playerName)
        {
            if (string.IsNullOrWhiteSpace(playerName))
                return null;

            var player = Context.Players
                    .FirstOrDefault(p => p.Name.ToLower() == playerName.ToLower());

            if (player == null)
            {
                return null;
            }

            Player = player;
            return Player;
        }

        public List<Player> GetPlayers()
        {
             return Context.Players.ToList();
        }

        public string[] DisplayDetails()
        {
            int labelWidth = 8; // width for label column
            List<string> output = new();

            // Helper local function to pad labels
            string PadLabel(string label) => label.PadRight(labelWidth);

            if (Player == null)
            {
                output.Add("No Hero Selected");
                output.Add("--------------------");
                output.Add($"{PadLabel("Name:")} -----------");
                output.Add($"{PadLabel("Class:")} -----------");
                output.Add($"{PadLabel("Level:")} 0");
                output.Add($"{PadLabel("HP:")} ---");
                output.Add("STR: --   AGI: --   INT: --");
            }
            else
            {
                // Add hero details with padding
                output.Add("Currently Selected");
                output.Add("--------------------");
                output.Add($"{PadLabel("Name:")} {Player.Name}");
                output.Add($"{PadLabel("Class:")} {Player.classType}");
                output.Add($"{PadLabel("Level:")} {Player.Level}");
                output.Add($"{PadLabel("HP:")} {Player.Health}");

                // Stats line - no padding
                output.Add($"STR: {Player.Strength}   AGI: {Player.Agility}   INT: {Player.Intelligence}");
            }

            return output.ToArray();
        }

        public string PlayersTable(IEnumerable<Player> players)
        {
            if (!players.Any())
                return "No players available.";

            var sb = new StringBuilder();

            // Define column widths
            const int nameWidth = 15;
            const int classWidth = 8;
            const int levelWidth = 3;
            const int healthWidth = 5;
            const int manaWidth = 5;
            const int strWidth = 3;
            const int agiWidth = 3;
            const int intWidth = 3;

            

            // Header
            sb.AppendLine(
                $"{"Name",-nameWidth} | {"Class",-classWidth} | {"Lvl",-levelWidth} | {"HP",-healthWidth} | {"MP",-manaWidth} | {"Str",-strWidth} | {"Agi",-agiWidth} | {"Int",-intWidth}");
            sb.AppendLine(new string('-', nameWidth + classWidth + levelWidth + healthWidth + manaWidth + strWidth + agiWidth + intWidth + 27));

            // Rows
            foreach (var p in players)
            {
                var hp = $"{p.Health}/{p.MaxHealth}".PadRight(healthWidth);
                var mp = $"{p.Mana}/{p.MaxMana}".PadRight(manaWidth);
                sb.AppendLine(
                    $"{p.Name,-nameWidth} | {p.classType,-classWidth} | {p.Level,-levelWidth} | {hp} | {mp} | {p.Strength,-strWidth} | {p.Agility,-agiWidth} | {p.Intelligence,-intWidth}"
                );

            }

            return sb.ToString().TrimEnd();
        }

        public string BuildAttributeTable()
        {
            var p = Player;
            var attributes = new Dictionary<string, int>
            {
                { "Intelligence", p.Intelligence },
                { "Strength", p.Strength },
                { "Agility", p.Agility }
            };

            // Determine max width for attribute names and values
            int nameWidth = attributes.Keys.Max(k => k.Length);
            int valueWidth = attributes.Values.Max(v => v.ToString().Length);

            // Compute table width
            int tableWidth = nameWidth + 3 + valueWidth; // 3 for " | "

            // Build title
            string title = $"{p.Name}'s Attributes";
            string topLine = new string('-', Math.Max(tableWidth, title.Length));

            // Start building table
            var sb = new System.Text.StringBuilder();
            sb.AppendLine(topLine);
            sb.AppendLine(title);
            sb.AppendLine(topLine);

            // Rows
            foreach (var kvp in attributes)
            {
                sb.AppendLine($"{kvp.Key.PadRight(nameWidth)} | {kvp.Value.ToString().PadLeft(valueWidth)}");
            }

            sb.AppendLine(topLine);

            return sb.ToString();

        }

        public bool DisplayAbilities(OutputManager outputManager, int menuBelow = 0){
            var abilities = Player.Abilities.ToList();

            // Check if the player has any abilities
            if (abilities == null || !abilities.Any())
            {
                outputManager.DisplayErrorBelow("No spells to display", menuBelow);
                return false;
            }

            outputManager.Clear();
            outputManager.WriteLine("--- Player Spells ---", ConsoleColor.Cyan);

            // 2. Define the Column Headers and Spacing
            // We'll use fixed spacing for neat alignment.
            // Columns: [ID] [NAME] [DESCRIPTION] [TYPE] [MANA COST]
            string headerFormat = "{0,-3} | {1,-20} | {2,-65} | {3,-10} | {4,-10}";

            // Display the Header Row
            outputManager.WriteLine(
                string.Format(headerFormat, "ID", "Name", "Description", "Type", "Mana Cost"),
                ConsoleColor.Yellow);

            // Draw a Separator Line
            outputManager.WriteLine(new string('-', 120), ConsoleColor.DarkGray);

            // 3. Loop Through and Display Abilities
            int counter = 1;
            foreach (var ability in abilities)
            {
                // Use a different color for the actual data
                outputManager.WriteLine(
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
            outputManager.Display();
            return true;
        }
    }
}
