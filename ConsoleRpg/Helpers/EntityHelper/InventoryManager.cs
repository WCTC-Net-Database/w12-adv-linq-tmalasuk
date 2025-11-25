using ConsoleRpg.Helpers.Environments;
using ConsoleRpgEntities.Models.Characters;
using ConsoleRpgEntities.Models.Equipments;
using ConsoleRpgEntities.Models.Items;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleRpg.Helpers.EntityHelper
{
    public class InventoryManager
    {
       
        private readonly PlayerManager _playerManager;
        private readonly BattleManager _battleManager;

        public InventoryManager(PlayerManager playerManager, BattleManager battleManager)
        {
            _playerManager = playerManager;
            _battleManager = battleManager;
            
        }
        public List<Item> ViewInventory()
        {
            return _playerManager.Player.Inventory.Items.ToList<Item>();
        }

        public List<Item> ViewEquippedItems()
        {
            return _playerManager.Player.Equipped.Values.Cast<Item>().ToList();
        }

        public List<Item> ViewEquippableItems()
        {
            var currentWeight = _playerManager.Player.Equipped.Where(kv => kv.Value != null).Sum(kvp => kvp.Value.Weight);

            var freeweight = _playerManager.Player.EquipmentCarryLimit - currentWeight;
            var equippableItems = _playerManager.Player.Inventory.Items.Where(i => i.Weight < freeweight && i is Equipment).ToList();
            return equippableItems;
        }


        public Item ItemSelector(string request)
        {
            var selectedItem = _playerManager.Player.Inventory.Items
                                        .FirstOrDefault(i => i.Name.Equals(request, StringComparison.OrdinalIgnoreCase));
            if (selectedItem == null)
            {
                selectedItem = _playerManager.Player.Equipped.Values
                                        .FirstOrDefault(i => i != null && i.Name.Equals(request, StringComparison.OrdinalIgnoreCase));
            }
            return selectedItem;
        }

        public bool IsItemEquipped(Item item)
        {
            return(_playerManager.Player.Equipped.Values.Contains(item));
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
                if (equipment.Slot.HasValue && _playerManager.Player.Equipped.ContainsKey(equipment.Slot.Value))
                {
                    // Unequip the currently equipped item in this slot
                    var currentlyEquipped = _playerManager.Player.Equipped[equipment.Slot.Value];
                    string unequipMessage = UnequipItem(currentlyEquipped);
                    List<string> equipMessage = EquipItem(equipment);
                    messages.Add(unequipMessage);
                    foreach (var message in equipMessage)
                    {
                        messages.Add(message);
                    }
                    return messages;
                }
                else
                {
                    List<string> equipMessage = EquipItem(equipment);
                    foreach (var message in equipMessage)
                    {
                        messages.Add(message);
                    }
                    return messages;
                }
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

            var targetSlot = item.Slot.Value;
            if (_playerManager.Player.Equipped.TryGetValue(targetSlot, out var currentlyEquipped) && currentlyEquipped != null)
            {
                string unequipMessage = UnequipItem(currentlyEquipped);
                messages.Add(unequipMessage);
            }

            var currentlyEquippedWeight = _playerManager.Player.Equipped.Values.Where(e => e != null).Sum(e => e.Weight);
            if (currentlyEquippedWeight + item.Weight > _playerManager.Player.EquipmentCarryLimit)
            {

                messages.Add($"{_playerManager.Player.Name} cannot equip {item.Name}. Exceeds equipment carry weight limit.");
                return messages;
                
            }
           
            _playerManager.Player.Equipped.Add(targetSlot, item);
            _playerManager.Player.Inventory.Items.Remove(item);

            messages.Add($"{item.Name} equipped in {targetSlot} slot.");
            return messages;

        }
        public string UnequipItem(Equipment item)
        {
            var itemToRemove = ItemSelector(item.Name);
            _playerManager.Player.Inventory.Items.Add(itemToRemove);
            if (itemToRemove is Equipment eq) { 
            _playerManager.Player.Equipped.Remove(eq.Slot.Value);
            }
            return($"{item.Name} unequipped from {item.Slot}.");


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
                   
            }
            return ("Unknown consumable type.");
        }
        
    }
}



