using ConsoleRpgEntities.Models.Abilities.PlayerAbilities;
using ConsoleRpgEntities.Models.Attributes;
using ConsoleRpgEntities.Models.Containers;
using ConsoleRpgEntities.Models.Equipments;
using ConsoleRpgEntities.Models.Rooms;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.Design;
using System.Diagnostics.CodeAnalysis;
using System.Numerics;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using static ConsoleRpgEntities.Models.Enums;


namespace ConsoleRpgEntities.Models.Characters
{

    public class EquipmentSettings
    {
        public Dictionary<EquipmentSlot, Equipment> Equipped { get; set; } = new();
    }

    public class Player: ITargetable
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Experience { get; set; }

        private int _skillPoints;
        public int SkillPoints
        {
            get => _skillPoints;
            set => _skillPoints = value;
        }
        public int MaxHealth => Level * 20;

        private int _health;
        public int Health
        {
            get => _health;
            set => _health = Math.Min(value, MaxHealth); // ensures Health never exceeds MaxHealth
        }
        public int MaxMana => Intelligence * 10;

        private int _mana;
        public int Mana
        {
            get => _mana;
            set => _mana = Math.Min(value, MaxMana); // ensures Mana never exceeds MaxMana
        }
        public int Strength { get; set; }
        public int Agility { get; set; }
        public int Intelligence { get; set; }

        private int _level; // Private backing field
        public int Level
        {
            get => _level;
            set
            {
              
                if (value > _level)
                {
                    int levelsGained = value - _level;
                    const int pointsPerLevel = 5; 

                    // Award skill points for each level gained
                    SkillPoints++;

                }
                
                _level = value;
            }
        }
        public int StunStack { get; set; }
        public PlayerClass classType { get; set; }
        public int InventoryCarryLimit => Strength * 5;
        public int EquipmentCarryLimit => Strength * 3;
        
        public int DodgeChance => Agility * 2;

        public List<Room> ExploredRooms = new List<Room>();

        // Foreign key
        public int? InventoryId { get; set; } // Foreign key for Inventory
        

        // Navigation properties
        public virtual Inventory Inventory { get; set; } 
        public virtual ICollection<Ability> Abilities { get; set; }

        [NotMapped]
        public virtual Equipped Equipped { get; set; }

        public virtual Room CurrentRoom { get; set; }

     

        public Player()
        {
            Inventory = new Inventory();
            Equipped = new Equipped();
            Abilities = new List<Ability>();
        }
        public Player(string name, PlayerClass playerClass)
        {
            Name = name;
            classType = playerClass;
            Abilities = new List<Ability>();
            Equipped = new Equipped { Player = this, PlayerId = this.Id};
            Inventory = new Inventory { Player = this, PlayerId = this.Id, Items = new List<Item>() };
            Experience = 0;
            SkillPoints = 0;
            _level = 1;
            Level = 1;
            switch (playerClass)
            {
                case PlayerClass.Knight:
                    Strength = 5;
                    Agility = 3;
                    Intelligence = 2;
                    Mana = 5;
                    break;
                case PlayerClass.Mage:
                    Strength = 2;
                    Agility = 3;
                    Intelligence = 5;
                    break;
                case PlayerClass.Archer:
                    Strength = 3;
                    Agility = 5;
                    Intelligence = 2;
                    break;
                default:
                    Strength = 3;
                    Agility = 3;
                    Intelligence = 3;
                    break;
            }
            Health = MaxHealth;
            Mana = MaxMana;

        }


        public bool CheckForLevelUp()
        {
            // Level up as long as current XP meets requirement
            while (Experience >= ExperienceRequiredForNextLevel())
            {
                Experience -= ExperienceRequiredForNextLevel();

                // Increase Level (this triggers your SkillPoints logic in setter)
                Level++;

                // Optional: fully restore HP & Mana on level up
                Health = MaxHealth;
                Mana = MaxMana;

                Console.WriteLine($"{Name} leveled up! You are now level {Level}!");
                return true;
            }
            return false;
        }

        private int ExperienceRequiredForNextLevel()
        {
            return Level * 20;
        }






        public override string ToString()
        {
            var currentInvWeight = Inventory.InventoryWeight;
            var currentEquipWeight = Equipped.EquipmentWeight;

            // Decorative name line
            string rawName = Name;
            string embellished = $"+-- {rawName} --+";

            // Width of everything below (set this once)
            int innerWidth = 26; // or whatever width you want the underline to be

            // Center the embellished name within that width
            string centeredName = embellished
                .PadLeft((innerWidth + embellished.Length) / 2)
                .PadRight(innerWidth);

            return
        $@"{centeredName}
{new string('-', innerWidth)}
Class:         {classType}
Level:         {Level}
Experience:    {Experience}
Skill Points:  {SkillPoints}

Health:        {Health}/{MaxHealth}
Mana:          {Mana}/{MaxMana}

 STR: {Strength} AGI: {Agility} INT:{Intelligence}

Dodge Chance:  {DodgeChance}%
Inv Weight:    {currentInvWeight} / {InventoryCarryLimit}
Equip Weight:  {currentEquipWeight} / {EquipmentCarryLimit}

Current Room:  {(CurrentRoom != null ? CurrentRoom.Name : "None")}
";
        }



        private string PadLabel(string label)
        {
            return label.PadRight(12); // tweak spacing here
        }


    }
}
