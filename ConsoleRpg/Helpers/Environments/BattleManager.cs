using ConsoleRpg.Helpers.EntityHelper;
using ConsoleRpgEntities.Models.Abilities.PlayerAbilities;
using ConsoleRpgEntities.Models.Attributes;
using ConsoleRpgEntities.Models.Characters;
using ConsoleRpgEntities.Models.Characters.Monsters;
using ConsoleRpgEntities.Models.Equipments;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Schema;

namespace ConsoleRpg.Helpers.Environments
{

    public class BattleManager
    {
        private ITargetable _actor;
        private ITargetable _notActor;
        private int _totalTurns;
        private readonly PlayerManager _playerManager;
        private readonly MonsterManager _monsterManager;
        private readonly Random _random;
        public List<Consumable> activeConsumableBuffs;
        private List<Ability> activeAbilityBuffs;
        private BattleMenu _battleMenu;
        private bool _reflectiveShieldActiveOnPlayer;
        private bool _reflectiveShieldActiveOnMonster;
        private OutputManager _outputManager;

  



        public BattleManager(PlayerManager playerManager, MonsterManager monsterManager, BattleMenu battleMenu, OutputManager outputManager)
        {
            _outputManager = outputManager;
            _playerManager = playerManager;
            _monsterManager = monsterManager;
            _actor = _playerManager.Player;
            _notActor = _monsterManager.Monster;
            _totalTurns = 0;
            _random = new Random();
            activeConsumableBuffs = new List<Consumable>();
            activeAbilityBuffs = new List<Ability>();
            _battleMenu = battleMenu;
        }

        public void BattleLoop()
        {
            // upkeep
            setUpEntities();
            _battleMenu._combatLog.Clear();
            _totalTurns = 0;

            while (_playerManager.Player.Health > 0 && _monsterManager.Monster.Health > 0)
            {
                ClearBuffs();
                                
                _playerManager.Player.Mana += 2;
                _battleMenu.RenderBattleHUD(_playerManager.Player, _monsterManager.Monster);
                var embrace = activeAbilityBuffs.FirstOrDefault(b => b.Id == 2) as NatureEmbrace;
                if (embrace != null)
                {
                    embrace.Stacks = Math.Max(embrace.Stacks - 1, 0);
                    int heal = embrace.SecondaryHeal(_playerManager.Player);
                    _playerManager.Player.Health += heal;                  

                    _battleMenu.AddCombatLog($"{_playerManager.Player.Name} heals for {heal} health from Nature's Embrace!", 100);
                }
                


                if (_actor is Player)
                {
                    if (_playerManager.Player.StunStack > 0)
                    {
                        Thread.Sleep(1000);
                        _battleMenu.AddCombatLog($"{_playerManager.Player.Name} is stunned and cannot act this turn!", 100);
                        
                        _playerManager.Player.StunStack--;
                        SwitchTurns();
                        _totalTurns++;
                        continue;
                    }
                    bool validChoice = false;

                    while (!validChoice)
                    {   
                        var key = Console.ReadLine();

                        switch (key)
                        {
                            case "1":
                                validChoice = true;
                                ActorAttacks();
                                break;

                            case "2":
                                var ability = SelectAbilities();

                                if (ability == null)
                                {
                                    
                                    _battleMenu.AddCombatLog("[SILENT]Ability selection canceled.", 0);
                                    
                                    break; 
                                }
                                validChoice = true;
                                UseAbility(ability);

                                break;
                                
                            case "3":
                                validChoice = true;
                                // Use item here
                                break;

                            default:
                                _battleMenu.AddCombatLog("Invalid selection", 0);
                                _battleMenu.RenderBattleHUD(_playerManager.Player, _monsterManager.Monster);
                                break;
                        }
                    }
                    
                }
                else if (_actor is Monster)
                {
                    if (_monsterManager.Monster.StunStack > 0)
                    {
                        Thread.Sleep(1000);
                        _battleMenu.AddCombatLog($"{_monsterManager.Monster.Name} is stunned and cannot act this turn!", 100);
                        
                        _monsterManager.Monster.StunStack--;
                        SwitchTurns();
                        _totalTurns++;
                        continue;
                    }
                    _battleMenu.AddCombatLog($"{_actor.Name} prepares to their attacks...", 1500);
                    _playerManager.Player.Health -= 1; // Placeholder damage value
                }
                SwitchTurns();
                _totalTurns++;
            }

        }

        public void SwitchTurns()
        {
            var temp = _actor;
            _actor = _notActor;
            _notActor = temp;
        }

        public void ClearBuffs()
        {
            
            var abilityBuffsToRemove = new List<Ability>();
            var consumableBuffsToRemove = new List<Consumable>();
            foreach (var buff in activeConsumableBuffs)
            {
                if (buff.TurnUsed + buff.BuffDuration > _totalTurns)
                {
                    activeConsumableBuffs.Remove(buff);
                }
            }

            foreach (var buff in activeAbilityBuffs)
            {
                if (buff.TurnUsed + buff.BuffDuration < _totalTurns)
                {
                    abilityBuffsToRemove.Add(buff);
                }
            }

            foreach (var buff in activeConsumableBuffs)
            {
                if (buff.TurnUsed + buff.BuffDuration < _totalTurns)
                {
                    consumableBuffsToRemove.Add(buff);
                }
            }

            // Now remove safely
            foreach (var buff in abilityBuffsToRemove)
            {
                activeAbilityBuffs.Remove(buff);
            }

            foreach (var buff in consumableBuffsToRemove)
            {
                activeConsumableBuffs.Remove(buff);
            }

        }

        public int CalculateDamageBaseAttack(ITargetable attacker, ITargetable defender)
        {
            if (attacker is Player player)
            {
                var consumableBuffs = activeConsumableBuffs.Where(buff => buff.ConsumableType == Enums.ConsumableType.Attack).Sum(buff => buff.Value);
                var weaponAttackBuff = _playerManager.Player.Equipped.Where(kvp => kvp.Value.EquipmentType == Enums.EquipmentType.Attack).Sum(kvp => kvp.Value.Value);
                int damage = (player.Strength * 3) + weaponAttackBuff + consumableBuffs;
                int damageVariance = _random.Next(-2, 3); // Random variance between -2 and +2
                return damage + damageVariance;
            }
            else if (attacker is Monster monster)
            {
                return 0;
            }
            return 0;
        }

        public void ActorAttacks()
        {
            int damage = CalculateDamageBaseAttack(_actor, _notActor);

            bool hitReflectShield =
                (_reflectiveShieldActiveOnMonster && _actor is Player) ||
                (_reflectiveShieldActiveOnPlayer && _actor is Monster);

            if (hitReflectShield)
            {
                // Reflect damage back to attacker
                _actor.Health -= damage;

                _battleMenu.AddCombatLog(
                    $"{_notActor.Name}'s reflective shield sends {damage} damage back to {_actor.Name}!", 100
                );
                

                // Remove the shield after reflecting once
                if (_actor is Player) _reflectiveShieldActiveOnMonster = false;
                if(_actor is Monster) _reflectiveShieldActiveOnPlayer = false;

                return; 
            }

            // Normal damage
            _notActor.Health -= damage;

            _battleMenu.AddCombatLog(
                $"{_actor.Name} attacks {_notActor.Name}, dealing {damage} damage!", 100
            );
            
        }


        public void UseAbility(Ability ability)
        {
            activeAbilityBuffs.Add(ability);
            int value = ability.Activate(_playerManager.Player);
            if (ability is ArcaneBarrage || ability is ShadowVeil || ability is SiphoningStrike)
            {
                _monsterManager.Monster.Health -= value;
                _battleMenu.AddCombatLog($"{ability.Name} deals {value} damage to {_monsterManager.Monster.Name}!", 100);
                

                if (ability is ShadowVeil)
                {
                    var stunChance = _playerManager.Player.Agility * 2;
                    var roll = _random.Next(1, 51);
                    if (roll <= stunChance)
                    {
                        _battleMenu.AddCombatLog($"{_monsterManager.Monster.Name} is stunned by Shadow Veil!", 100);
                        
                        _monsterManager.Monster.StunStack++;
                        
                    }
                }
            }
            if (ability is NatureEmbrace || ability is SiphoningStrike)
            {
                _playerManager.Player.Health += value;
                _battleMenu.AddCombatLog($"{ability.Name} heals for {value} health!", 100);
                
            }
            if (ability is NullifyingAegis)
            {
                _reflectiveShieldActiveOnPlayer = true;
                _battleMenu.AddCombatLog($"{ability.Name} cloaks {_playerManager.Player.Name} in a mystifying shield.", 100);
                
            }
        }

        public Ability SelectAbilities()
        {
            Console.Clear();
            var abilities = _playerManager.Player.Abilities.ToList();

            // Check if the player has any abilities;

            // Check if the player has any abilities
            if (abilities == null || !abilities.Any())
            {
                _outputManager.WriteLine("You have no abilities to display!", ConsoleColor.Red);
                _outputManager.Display();
                Thread.Sleep(1500);
                return null;
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

            _outputManager.Display();

            Ability chosenAbility = null;

            while (true)
            {
                Console.Write("Which ability would you like to use? (Enter ID or 0 to cancel): ");
                var choice = Console.ReadLine();

                if (choice == "0")
                    return null;

                chosenAbility = abilities.FirstOrDefault(a => a.Id.ToString() == choice);

                if (chosenAbility == null)
                {
                    Console.WriteLine("You have no ability with that ID!", ConsoleColor.Red);
                    Thread.Sleep(1000);
                    continue;
                }

                if (chosenAbility.ManaCost > _playerManager.Player.Mana)
                {
                    Console.WriteLine("You do not have enough mana to use that ability!", ConsoleColor.Red);
                    Thread.Sleep(1000);
                    continue;
                }

                break; // valid ability selected
            }

            return chosenAbility;

        }

        // Battle Engine handles the battle logic
        public List<string> HandleMonsterDefeat(Player player, Monster monster, InventoryManager inventory)
        {
            var endEvents = new List<string>();

            if (monster.Health <= 0)
            {
                endEvents.Add($"{monster.Name} has been defeated");

                var item = monster.itemDrop.FirstOrDefault();
                if (item != null)
                {
                    bool addedToInventory = inventory.AddItemToInventory(item);
                    if (addedToInventory)
                        endEvents.Add($"{item.Name} has been added to your inventory!");
                    else
                        endEvents.Add("There isn't enough room in your inventory to add anymore items...battle loot lost.");
                }

                player.Experience += monster.experienceGiven;
                endEvents.Add($"{player.Name} gained {monster.experienceGiven} experience!");

                if (player.CheckForLevelUp())
                {
                    endEvents.Add($"{player.Name} leveled up to level {player.Level}!");
                }
            }

            return endEvents;
        }


        public void setUpEntities()
        {
            _actor = _playerManager.Player;
            _notActor = _monsterManager.Monster;
        }

        

    }
}
