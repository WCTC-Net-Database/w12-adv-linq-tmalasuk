using ConsoleRpgEntities.Models.Attributes;
using ConsoleRpgEntities.Models.Characters;
using ConsoleRpgEntities.Models.Characters.Monsters;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleRpg.Helpers.Environments
{
    public class BattleMenu
    {
        public List<(string Message, int Delay)> _combatLog = new();
        private const int MaxCombatLogEntries = 10;
        public string actorTurn;
        private int entriesAddedThisRound = 0;


        public void RenderBattleHUD(Player player, Monster monster)
        {
            Console.Clear();
            int width = 100;
            string topBorder = new string('=', width);
            int middle = width / 2;

            // Header
            string playerName = player.Name.PadRight(middle - 1);
            string monsterName = monster.Name.PadLeft(width - middle - 1);
            Console.WriteLine(topBorder);
            Console.WriteLine($"|{playerName}{monsterName}|");

            // HP row
            string playerHp = $"HP: {player.Health}/{player.MaxHealth}".PadRight(middle - 1);
            string monsterHp = $"HP: {monster.Health}/{monster.MaxHealth}".PadLeft(width - middle - 1);
            Console.WriteLine($"|{playerHp}{monsterHp}|");

            // MP / Status row
            string playerMp = $"MP: {player.Mana}/{player.MaxMana}".PadRight(middle - 1);
            string monsterStatus = "".PadLeft(width - middle - 1);
            Console.WriteLine($"|{playerMp}{monsterStatus}|");

            Console.WriteLine(topBorder);

            // Actions
            string actions = "Actions: [1] Attack [2] Ability [3] Item";
            Console.WriteLine($"| {actions.PadRight(width - 3)}|");

            Console.WriteLine(topBorder);

            // Combat Log
            Console.WriteLine("| Combat Log:".PadRight(width - 1) + "|");
            int logStartLine = Console.GetCursorPosition().Top;

            // Print all previous log entries instantly
            for (int i = 0; i < _combatLog.Count - entriesAddedThisRound; i++)
            {
                
                var entry = _combatLog[i];
                string message = entry.Message;
                bool isSilent = message.StartsWith("[SILENT]");
                string clean = isSilent ? message.Replace("[SILENT]", "") : message;
                string line = $"> {clean}";
                if (line.Length > width - 3) line = line.Substring(0, width - 3);
                Console.WriteLine($"| {line.PadRight(width - 3)}|");
                
            }
            for (int i = 0; i < entriesAddedThisRound + 1; i++)
            {
                Console.WriteLine($"| {"".PadRight(width - 3)}|");
            }

            // Bottom border & input prompt
            Console.WriteLine(topBorder);
            Console.Write(">> "); // input prompt
            var promptPos = Console.GetCursorPosition();

            // Animate only the latest entry
            
            if (_combatLog.Count > 0)
            {
                var newEntries = _combatLog.Skip(Math.Max(0, _combatLog.Count - entriesAddedThisRound)).ToList();

                int firstAnimatedLine = logStartLine + (_combatLog.Count - entriesAddedThisRound);
                int cursorRow = firstAnimatedLine;

                foreach (var entry in newEntries)                           
                {
                    string text;
                    int delay;
                    if (entry is ValueTuple<string, int> e)
                    {
                        text = e.Item1;
                        delay = e.Item2;
                    }
                    else
                    {
                        text = entry.ToString();
                        delay = 20;
                    }
                    bool isSilent = text.StartsWith("[SILENT]");
                    string latestClean = isSilent ? text.Replace("[SILENT]", "") : text;

                    Console.SetCursorPosition(2, cursorRow);

                    if (!isSilent)
                    {
                        TypeWriterLine(latestClean, width);
                        Thread.Sleep(delay);
                    }
                    else
                        Console.Write(latestClean);

                    cursorRow++; 
                }
            }
            Console.SetCursorPosition(promptPos.Left, promptPos.Top);
            entriesAddedThisRound = 0;

        }


        public void AddCombatLog(string message, int delay)
        {
            _combatLog.Add((message, delay));
            entriesAddedThisRound++;

            // Remove oldest entry if over the limit
            while (_combatLog.Count > MaxCombatLogEntries)
            {
                _combatLog.RemoveAt(0);
                
            }
        }


        private void TypeWriterLine(string text, int width, int delay = 20)
        {

            foreach (char c in text)
            {
                Console.Write(c);
                Thread.Sleep(delay);
            }

            // pad the rest of the line WITHOUT moving to next line
            int used = 2 + text.Length; // "| " + typed text
            int remaining = width - used - 1; // subtract right border
            Console.Write(new string(' ', Math.Max(0, remaining)));

            Console.Write("|"); // right border

            // do NOT call WriteLine here; move the cursor manually
        }

    }
}
