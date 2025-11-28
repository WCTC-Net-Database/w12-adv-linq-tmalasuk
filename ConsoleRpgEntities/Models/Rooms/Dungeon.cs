using ConsoleRpgEntities.Models.Characters;
using ConsoleRpgEntities.Models.Rooms.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleRpgEntities.Models.Rooms
{
    public class Dungeon : Room, ILockedRoom, IInteractableRoom, ICombatRoom
    {
        public bool IsLocked { get; set; }

        public bool KeyFormed { get; set; }

        public bool StoneGrabbed { get; set; }

        public bool CrackFound { get; set; }

        public List<string> Interact(Player player)
        {
            var messages = new List<string>();
            if (!IsLocked)
            {
                return new List<string> { "The door to the dungeon is open." };
            }
            else
            {
                messages.Add( "The damp air in the dungeon is intriguing..." );
            }
            return messages;
        }

        public List<string> IntelligenceInteraction(Player player)
        {
            var results = new List<string>();

            if (!IsLocked) {
                results.Add("The dungeon is already unlocked.");
                return results;
            }

            // Set the difficulty of unlocking this dungeon
            int difficultyClass = 10; // TODO: This could be a property of the Dungeon

            // Roll a d20
            Random rand = new Random();
            int roll = rand.Next(1, 21);

            // Get player INT modifier
            int intMod = player.Intelligence;

            int total = roll + intMod;

            // Add messages for flavor
            results.Add($"You atempt to conjure a key from thin air...");
            results.Add($"You rolled: {roll} (d20) + {intMod} INT modifier = {total}");

            // Check success
            if (total >= difficultyClass)
            {
                results.Add($"SUCCESS! The key forms in your hand.");
                KeyFormed = true;
            }
            else
            {
                results.Add($"FAILURE. The sir dissipates quickly, rejecting your attempt.");
            }

            return results;
        }

        public List<string> StrengthInteraction(Player player)
        {
            var results = new List<string>();

            int difficultyClass = 10; 
            Random rand = new Random();

            int roll = rand.Next(1, 21);
            int strMod = player.Strength;

            int total = roll + strMod;

            results.Add("The damp air smells of stone. You try to hoist a stone from the floor...");
            results.Add($"You rolled: {roll} (d20) + {strMod} STR modifier = {total}");

            if (total >= difficultyClass)
            {
                results.Add("SUCCESS! You pry the stone loose!");
                StoneGrabbed = true;
            }
            else
            {
                results.Add("FAILURE. The stone holds firm.");
            }

            return results;
        }

        public List<string> AgilityInteraction(Player player)
        {
            var results = new List<string>();

            results.Add("You sense the air flowing faster in one direction...");

            if (player.classType == Enums.PlayerClass.Archer)
            {
                results.Add("Aha! a small crack in the wall is hidden behind some moss.");
            }
            else
            {
                results.Add("An archer with a dexterious focus might be able to find something of use...");
            }
            return results;
        }

        public List<string> TryUnlock(string choice, Player player)
        {
            var results = new List<string>();
            choice = choice.ToLower();

            if (!IsLocked)
            {
                results.Add("The dungeon is already unlocked.");
                return results;
            }

            switch (choice)
            {
                case "s": // Stone unlock
                    if (StoneGrabbed == true)
                    {
                        IsLocked = false;
                        results.Add("Using the stone as leverage, you pop the mechanism loose. The dungeon unlocks!");
                    }
                    else
                    {
                        results.Add("You don't have a stone to use.");
                    }
                    break;

                case "k": // Magic key
                    if (KeyFormed == true)
                    {
                        IsLocked = false;
                        results.Add("The magical key slides in effortlessly. The lock clicks open!");
                    }
                    else
                    {
                        results.Add("You haven't formed a magical key.");
                    }
                    break;

                case "c": // Crack (agility discovery)
                    if (CrackFound == true)
                    {
                        IsLocked = false;
                        results.Add("You slip your tool into the crack and trigger the internal latch. The dungeon unlocks!");
                    }
                    else
                    {
                        results.Add("You haven't found a usable crack to work with.");
                    }
                    break;

                case "f": // Force door - ONLY ONE WITH A ROLL
                    Random rand = new Random();
                    int roll = rand.Next(1, 21);
                    int total = roll + player.Strength;

                    results.Add($"You smash your shoulder against the door...");
                    results.Add($"Roll: {roll} + STR {player.Strength} = {total}");

                    if (total >= 18) // easy DC
                    {
                        IsLocked = false;
                        results.Add("With a loud crack, the door bursts open!");
                    }
                    else
                    {
                        results.Add("You slam into the door, but it doesn’t budge.");
                    }
                    break;

                default:
                    results.Add("Key input not reconized.");
                    break;
            }

            return results;
        }

    }
}
