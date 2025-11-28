using Castle.Components.DictionaryAdapter.Xml;
using ConsoleRpg.Helpers.Menus;
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


        public void RenderBattleHUD(Player player, Monster monster, OutputManager outputManager)
        {
            outputManager.Clear();
            int width = 100;
            int middle = width / 2;
            string topBorder = new string('=', width);
            string reset = "\u001b[0m";

            // --- Header: Names ---
            string playerName = TruncateOrPad(player.Name, middle - 1, padRight: true);
            string monsterName = TruncateOrPad(monster.Name, width - middle - 1, padRight: false);
            Console.WriteLine(topBorder);
            Console.WriteLine($"|{playerName}{monsterName}|");

            // --- HP Row ---
            string playerHpVisible = $"HP: {player.Health}/{player.MaxHealth}";
            string monsterHpVisible = $"HP: {monster.Health}/{monster.MaxHealth}";

            string pColor = GetHpColor(player.Health, player.MaxHealth);
            string mColor = GetHpColor(monster.Health, monster.MaxHealth);

            string playerHpText = pColor + playerHpVisible + reset;
            string monsterHpText = mColor + monsterHpVisible + reset;

            // --- mp row ---
            string playerMpVisible = $"MP: {player.Mana}/{player.MaxMana}";
            string mpColor = GetMpColor(player.Mana, player.MaxMana);
            string playerMpText = mpColor + playerMpVisible + reset;

            // Pad manually to account for ANSI codes
            playerHpText = PadVisible(playerHpText, middle - 1, padRight: true);
            monsterHpText = PadVisible(monsterHpText, width - middle - 1, padRight: false);
            playerMpText = PadVisible(playerMpText, width - 2, padRight: true);

            Console.WriteLine($"|{playerHpText}{monsterHpText}|", ConsoleColor.White);
            Console.WriteLine($"|{playerMpText}|", ConsoleColor.White);
            Console.WriteLine(topBorder, ConsoleColor.White);

            // --- Actions ---
            string actions = "Actions: [1] Attack [2] Ability [3] Item";
            Console.WriteLine($"| {actions.PadRight(width - 3)}|", ConsoleColor.White);
            Console.WriteLine(topBorder, ConsoleColor.White);

            // --- Combat Log ---
            Console.WriteLine("| Combat Log:".PadRight(width - 1) + "|", ConsoleColor.White);
            int logStartLine = Console.GetCursorPosition().Top;

            for (int i = 0; i < _combatLog.Count - entriesAddedThisRound; i++)
            {
                string message = _combatLog[i].Message;

                bool isSilent = message.StartsWith("[SILENT]");
                string clean = isSilent ? message.Replace("[SILENT]", "") : message;
                string line = $"> {clean}";
                if (line.Length > width - 3) line = line.Substring(0, width - 3);
                Console.WriteLine($"| {line.PadRight(width - 3)}|", ConsoleColor.White);
            }

            // Empty lines for new entries
            for (int i = 0; i < entriesAddedThisRound + 1; i++)
                Console.WriteLine($"| {"".PadRight(width - 3)}|", ConsoleColor.White);

            Console.WriteLine(topBorder, ConsoleColor.White);
            var prePrompt = Console.GetCursorPosition();

            // --- Animate Latest Entries ---
            if (_combatLog.Count > 0)
            {
                var newEntries = _combatLog.Skip(Math.Max(0, _combatLog.Count - entriesAddedThisRound)).ToList();
                int cursorRow = logStartLine + (_combatLog.Count - entriesAddedThisRound);

                foreach (var entry in newEntries)
                {
                    string text = entry.Message;
                    int delay = entry.Delay;
                    

                    bool isSilent = text.StartsWith("[SILENT]");
                    string latestClean = isSilent ? text.Replace("[SILENT]", "") : text;

                    Console.SetCursorPosition(2, cursorRow);
                    if (!isSilent)
                        TypeWriterLine(latestClean, width);
                    else
                        Console.Write(latestClean, ConsoleColor.White);
                    Thread.Sleep(delay);
                    cursorRow++;
                }
            }

            Console.SetCursorPosition(prePrompt.Left, prePrompt.Top);
            Console.Write(">> ", ConsoleColor.White);
            entriesAddedThisRound = 0;
        }

        // --- Helpers ---

        // Pads or truncates text to fit desired visible width
        private string TruncateOrPad(string text, int width, bool padRight = true)
        {
            if (text.Length > width)
                return text.Substring(0, width);
            return padRight ? text.PadRight(width) : text.PadLeft(width);
        }

        // Pads a string based on visible length (ignores ANSI codes)
        private string PadVisible(string text, int width, bool padRight = true)
        {
            int visibleLength = StripAnsi(text).Length;
            int padding = width - visibleLength;
            if (padding <= 0) return text;

            return padRight ? text + new string(' ', padding) : new string(' ', padding) + text;
        }

        // Remove ANSI codes for length calculation
        private string StripAnsi(string text)
        {
            return System.Text.RegularExpressions.Regex.Replace(text, @"\u001b\[[0-9;]*m", "");
        }

        // HP color helper
        private static string GetHpColor(int hp, int max)
        {
            double pct = (double)hp / max;
            if (pct >= 0.7) return "\u001b[32m"; // green
            if (pct >= 0.3) return "\u001b[33m"; // yellow
            return "\u001b[31m";                  // red
        }

        private static string GetMpColor(int mp, int max)
        {
            double pct = (double)mp / max;
            if (pct >= 0.7) return "\u001b[32m"; // green
            if (pct >= 0.3) return "\u001b[33m"; // yellow
            return "\u001b[31m";                  // red
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

    }
}
