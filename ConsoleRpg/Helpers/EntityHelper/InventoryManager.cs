using ConsoleRpg.Helpers.Battle;
using ConsoleRpg.Helpers.Environments;
using ConsoleRpgEntities.Data;
using ConsoleRpgEntities.Models;
using ConsoleRpgEntities.Models.Characters;
using ConsoleRpgEntities.Models.Containers;
using ConsoleRpgEntities.Models.Equipments;
using ConsoleRpgEntities.Models.Items;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static ConsoleRpgEntities.Models.Enums;

namespace ConsoleRpg.Helpers.EntityHelper
{
    public class InventoryManager
    {
       
        private readonly PlayerManager _playerManager;
        private readonly BattleManager _battleManager;
        private readonly GameContext _context;
        private readonly Random _rng = new Random();
        private readonly RoomManager _roomManager;

        public InventoryManager(PlayerManager playerManager, BattleManager battleManager, GameContext context, RoomManager roomManager)
        {
            _context = context;
            _playerManager = playerManager;
            _battleManager = battleManager;
            _roomManager = roomManager;
            
        }
        public List<Item> ViewInventory()
        {
            return _playerManager.Player.Inventory.Items.ToList<Item>();
        }

        public void ViewEquippedItems()
        {
            Console.Clear();
            Console.WriteLine(_playerManager.Player.Equipped.ToString());
            Console.WriteLine("Press any key to return...");
            Console.ReadKey();
        }

        public List<Item> ViewEquippableItems()
        {
            var currentWeight = _playerManager.Player.Equipped.EquipmentWeight;

            var freeweight = _playerManager.Player.EquipmentCarryLimit - currentWeight;
            var equippableItems = _playerManager.Player.Inventory.Items.Where(i => i.Weight < freeweight && i is Equipment).ToList();
            return equippableItems;
        }


        public Item ItemSelector(string request, IItemContainer container)
        {
            Item selectedItem = null;
            switch (container)
            {
                case Inventory: selectedItem = _playerManager.Player.Inventory.Items.FirstOrDefault(i => i.Name.Equals(request, StringComparison.OrdinalIgnoreCase)); break;
                case Equipped: selectedItem = _playerManager.Player.Equipped.Items.FirstOrDefault(i => i.Name.Equals(request, StringComparison.OrdinalIgnoreCase)); break;
                case RoomItems: selectedItem = _roomManager.Room.DroppedLoot.Items.FirstOrDefault(i => i.Name.Equals(request, StringComparison.OrdinalIgnoreCase)); break;
            }
            return selectedItem;
        }

        public bool IsItemEquipped(Item item)
        {
            return(_playerManager.Player.Equipped.Items.Contains(item));
        }

        

        public void SortItems(string sortType, string ad)
        {
            switch (sortType)
            {
                case "n":
                    if (ad == "d")
                        _playerManager.Player.Inventory.Items = _playerManager.Player.Inventory.Items.OrderByDescending(i => i.Name).ToList();
                    else
                        _playerManager.Player.Inventory.Items = _playerManager.Player.Inventory.Items.OrderBy(i => i.Name).ToList();
                    break;

                case "v":
                    if (ad == "d")
                        _playerManager.Player.Inventory.Items = _playerManager.Player.Inventory.Items.OrderByDescending(i => i.Value).ToList();
                    else
                        _playerManager.Player.Inventory.Items = _playerManager.Player.Inventory.Items.OrderBy(i => i.Value).ToList();

                    break;

                case "w":
                    if (ad == "d")
                        _playerManager.Player.Inventory.Items = _playerManager.Player.Inventory.Items.OrderByDescending(i => i.Weight).ToList();
                    else
                        _playerManager.Player.Inventory.Items = _playerManager.Player.Inventory.Items.OrderBy(i => i.Weight).ToList();
                    break;
                case "t":
                    _playerManager.Player.Inventory.Items =
                        _playerManager.Player.Inventory.Items
                            .OrderByDescending(i => i.ItemCategory)
                            .ThenByDescending(i =>
                                i is Equipment e
                                    ? (EquipmentSlot?)e.Slot
                                    : null
                            )
                            .ThenByDescending(i =>
                                i is Consumable c
                                    ? (ConsumableType?)c.ConsumableType
                                    : null
                            )
                            .ToList();
                    break;
            }
        }

        public List<Item> SearchInventory(string request)
        {
            var matchingItems = _playerManager.Player.Inventory.Items
                            .Where(i =>
                                // Name & Category
                                i.Name.Contains(request, StringComparison.OrdinalIgnoreCase) ||
                                i.ItemCategory.Contains(request, StringComparison.OrdinalIgnoreCase) ||

                                // Equipment (Type or Slot)
                                (i is Equipment eq &&
                                    (eq.EquipmentType.ToString().Contains(request, StringComparison.OrdinalIgnoreCase) ||
                                        eq.Slot.ToString().Contains(request, StringComparison.OrdinalIgnoreCase))) ||

                                // Consumable Type
                                (i is Consumable c &&
                                    c.ConsumableType.ToString().Contains(request, StringComparison.OrdinalIgnoreCase))
                            )
                            .ToList();
            return matchingItems;
        }

        public bool AddItemToInventory(Item item)
        {
            if (_playerManager.Player.Inventory.InventoryWeight + item.Weight > _playerManager.Player.InventoryCarryLimit)
            {
                return false;
            }
            _playerManager.Player.Inventory.Items.Add(item);
            return true;
        }

        public string RemoveItemFromInventory(Item item)
        {
            if (_playerManager.Player.Inventory.Items.Contains(item))
            {
                _playerManager.Player.Inventory.Items.Remove(item);
                return($"{item.Name} removed from {_playerManager.Player.Name}'s inventory.");
            }
            else
            {
                return($"{item.Name} not found in {_playerManager.Player.Name}'s inventory.");
            }
        }

        public List<string> InteractWithItem(Item item)
        {
            var messages = new List<string>();
            if (item is Consumable consumable)
            {
                var consumeMessage = Consume(consumable);
                messages.Add(consumeMessage);
                return messages;
            }
            else if (item is Equipment equipment)
            {
                // Equip the new item
                List<string> equipMessages = EquipItem(equipment);
                messages.AddRange(equipMessages);

                return messages;

            }
            if (item is Spellbook spellbook)
            {
                if (_playerManager.Player.Abilities.Any(a => a.Id == spellbook.GrantedAbility.Id))
                {
                    messages.Add("The book doesn't reveal anything new.");
                    return messages;
                }
                else
                {
                    _playerManager.Player.Abilities.Add(spellbook.GrantedAbility);

                    _playerManager.Player.Inventory.Items.Remove(spellbook);

                    messages.Add("You find the book enlightening!");
                    messages.Add($"You've learned how to cast {spellbook.GrantedAbility.Name}");
                    return messages;

                }
            }
        
            else
            {
                messages.Add("Unknown item type.");
                return messages;
            }
        }

        private List<string> EquipItem(Equipment item)
        {
            var messages = new List<string>();
            var equipped = _playerManager.Player.Equipped;

            Equipment? currentlyEquipped = equipped.GetEquipmentFromSlot(item.Slot.ToString());
            // Weight check
            if (currentlyEquipped != null)
            {
                if (equipped.EquipmentWeight + item.Weight - currentlyEquipped.Weight > _playerManager.Player.EquipmentCarryLimit)
                {
                    messages.Add($"{_playerManager.Player.Name} cannot equip {item.Name}. Exceeds equipment carry weight limit.");
                    return messages;
                }
            }
            else
            {
                if (equipped.EquipmentWeight + item.Weight > _playerManager.Player.EquipmentCarryLimit)
                {
                    messages.Add($"{_playerManager.Player.Name} cannot equip {item.Name}. Exceeds equipment carry weight limit.");
                    return messages;
                }
            }


                // Unequip the current item in that slot, if any
                if (currentlyEquipped != null)
            {
                string unequipMessage = UnequipItem(currentlyEquipped);
                messages.Add(unequipMessage);
            }


            // Equip new
            equipped.AssignEquipmentToSlot(item);
            _playerManager.Player.Inventory.Items.Remove(item);
            messages.Add($"{item.Name} equipped in {item.Slot} slot.");
            return messages;
        }

        public string UnequipItem(Equipment item)
        {
            var itemToRemove = _playerManager.Player.Equipped.GetEquipmentFromSlot(item.Slot.ToString());
            _playerManager.Player.Equipped.ClearSlot(itemToRemove.Slot.ToString());
            _playerManager.Player.Inventory.Items.Add(itemToRemove); 
            return($"{item.Name} unequipped from {item.Slot.ToString}.");
        }

        private string Consume(Consumable consumable)
        {
            
            switch (consumable.ConsumableType)
            {
                case Enums.ConsumableType.Heal:
                    if (consumable is Item healItem)
                    {
                        _playerManager.Player.Health += healItem.Value;

                        _playerManager.Player.Inventory.Items.Remove(consumable);
                        return($"{_playerManager.Player.Name} healed for {healItem.Value} health!");
                    }

                    break;
                case Enums.ConsumableType.Attack:
                    if (consumable is Item attackItem)
                    {
                        _battleManager.activeConsumableBuffs.Add(consumable);
                        consumable.TurnUsed = 0;
                        _playerManager.Player.Inventory.Items.Remove(consumable);
                        return ($"{_playerManager.Player.Name}'s attack increased by {attackItem.Value} for {consumable.BuffDuration.Value} turns!");
                        
                    }
                    break;
                case Enums.ConsumableType.Defense:
                    if (consumable is Item defenseItem)
                    {
                        _battleManager.activeConsumableBuffs.Add(consumable);
                        consumable.TurnUsed = 0;
                        _playerManager.Player.Inventory.Items.Remove(consumable);
                        return ($"{_playerManager.Player.Name}'s defense increased by {defenseItem.Value} for {consumable.BuffDuration.Value} turns!");
                        
                    }
                    break;
                //TODO: add mana logic
            }
            return ("Unknown consumable type.");
        }

        public Item GetRandomConsumable(string rarity)
        {
            var possibleItems = _context.Items
                .Where(i => i.ItemCategory == "Consumable" && i.Rarity == rarity)
                .ToList();

            if (possibleItems.Count == 0)
                return null; 

            int index = _rng.Next(possibleItems.Count);
            return possibleItems[index];
        }

        internal Item GetRandomEquipment(string rarity)
        {
            var possibleItems = _context.Items
                .Where(i => i.ItemCategory == "Equipment" && i.Rarity == rarity)
                .ToList();

            if (possibleItems.Count == 0)
                return null;

            int index = _rng.Next(possibleItems.Count);
            return possibleItems[index];
        }

        internal Item GetRandomSpellbook()
        {
            var spellbooks = _context.Items.Where(i => i.ItemCategory == "Spellbook").ToList();
            int index = _rng.Next(spellbooks.Count);
            return spellbooks[index];
        }

        
    }
}



