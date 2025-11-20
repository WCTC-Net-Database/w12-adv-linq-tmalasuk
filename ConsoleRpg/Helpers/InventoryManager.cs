using ConsoleRpgEntities.Models.Characters;
using ConsoleRpgEntities.Models.Equipments;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleRpg.Helpers
{
    public class InventoryManager
    {
        public readonly Player _player;

        public InventoryManager(Player player)
        {
            _player = player;
        }
        public List<Item> ViewInventory()
        {
            return _player.Inventory.Items.ToList<Item>();
        }

        public List<Item> ViewEquippedItems()
        {
            return _player.Equipped.Values.Cast<Item>().ToList();
        }

        public List<Item> ViewEquippableItems()
        {
            var currentWeight = _player.Equipped.Where(kv => kv.Value != null).Sum(kvp => kvp.Value.Weight);

            var freeweight = _player.EquipmentCarryLimit - currentWeight;
            var equippableItems = _player.Inventory.Items.Where(i => i.Weight < freeweight && i is Equipment).ToList();
            return equippableItems;
        }


        public Item ItemSelector(string request)
        {
            var selectedItem = _player.Inventory.Items
                                        .FirstOrDefault(i => i.Name.Equals(request, StringComparison.OrdinalIgnoreCase));
            if (selectedItem == null)
            {
                selectedItem = _player.Equipped.Values
                                        .FirstOrDefault(i => i != null && i.Name.Equals(request, StringComparison.OrdinalIgnoreCase));
            }
            return selectedItem;
        }

        public bool IsItemEquipped(Item item)
        {
            return(_player.Equipped.Values.Contains(item));
        }

        

        public void SortItems(string sortType, bool descending)
        {
            switch (sortType)
            {
                case "1":
                    if (descending)
                        _player.Inventory.Items = _player.Inventory.Items.OrderByDescending(i => i.Name).ToList();
                    else
                        _player.Inventory.Items = _player.Inventory.Items.OrderBy(i => i.Name).ToList();
                    break;

                case "2":
                    if (descending)
                        _player.Inventory.Items = _player.Inventory.Items.OrderByDescending(i => i.Value).ToList();
                    else
                        _player.Inventory.Items = _player.Inventory.Items.OrderBy(i => i.Value).ToList();

                    break;

                case "3":
                    if (descending)
                        _player.Inventory.Items = _player.Inventory.Items.OrderByDescending(i => i.Weight).ToList();
                    else
                        _player.Inventory.Items = _player.Inventory.Items.OrderBy(i => i.Weight).ToList();
                    break;
            }
        }

        public List<Item> SearchInventory(string request)
        {
            var matchingItems = _player.Inventory.Items
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

        public void AddItemToInventory(Item item)
        {
            if (_player.Inventory.InventoryWeight + item.Weight > _player.InventoryCarryLimit)
            {
                Console.WriteLine($"{_player.Name} cannot carry {item.Name}. Exceeds carry weight limit.");
                return;
            }
            _player.Inventory.Items.Add(item);
            Console.WriteLine($"{item.Name} added to {_player.Name}'s inventory.");
        }

        public void RemoveItemFromInventory(Item item)
        {
            if (_player.Inventory.Items.Contains(item))
            {
                _player.Inventory.Items.Remove(item);
                Console.WriteLine($"{item.Name} removed from {_player.Name}'s inventory.");
            }
            else
            {
                Console.WriteLine($"{item.Name} not found in {_player.Name}'s inventory.");
            }
        }

        public void InteractWithItem(Item item)
        {
            if (item is Consumable consumable)
            {
                Consume(consumable);

            }
            else if (item is Equipment equipment)
            {
                if (equipment.Slot.HasValue && _player.Equipped.ContainsKey(equipment.Slot.Value))
                {
                    // Unequip the currently equipped item in this slot
                    var currentlyEquipped = _player.Equipped[equipment.Slot.Value];
                    UnequipItem(currentlyEquipped);
                    EquipItem(equipment);
                }
                else
                {
                    EquipItem(equipment);
                }
            }
            else
            {
                Console.WriteLine("Unknown item type.");
            }
        }

        private void EquipItem(Equipment item)
        {
            var targetSlot = item.Slot.Value;
            if (_player.Equipped.TryGetValue(targetSlot, out var currentlyEquipped) && currentlyEquipped != null)
            {
                _player.Equipped.Remove(targetSlot);
                _player.Inventory.Items.Add(currentlyEquipped);
            }

            var currentlyEquippedWeight = _player.Equipped.Values.Where(e => e != null).Sum(e => e.Weight);
            if (currentlyEquippedWeight + item.Weight > _player.EquipmentCarryLimit)
            {
                Console.WriteLine($"{_player.Name} cannot equip {item.Name}. Exceeds equipment carry weight limit.");
                return;
            }
           

            // Equip the new item
            _player.Equipped.Add(targetSlot, item);
            _player.Inventory.Items.Remove(item);
            Console.WriteLine($"{item.Name} equipped in {targetSlot} slot.");
        }
        public void UnequipItem(Equipment item)
        {
            var itemToRemove = ItemSelector(item.Name);
            _player.Inventory.Items.Add(itemToRemove);
            if (itemToRemove is Equipment eq) { 
            _player.Equipped.Remove(eq.Slot.Value);
            }
            Console.WriteLine($"{item.Name} unequipped from {item.Slot}.");


        }

        private void Consume(Consumable consumable)
        {

            switch (consumable.ConsumableType)
            {
                case Enums.ConsumableType.Heal:
                    if (consumable is Item healItem)
                    {
                        _player.Health += healItem.Value;
                        Console.WriteLine($"{_player.Name} healed for {healItem.Value} health!");
                        //remove item
                        _player.Inventory.Items.Remove(consumable);
                    }

                    break;
                case Enums.ConsumableType.Attack:
                    if (consumable is Item attackItem)
                    {
                        // Apply attack buff logic here
                        Console.WriteLine($"{_player.Name}'s attack increased by {attackItem.Value} for {consumable.BuffDuration.Value} turns!");
                        //remove item
                        _player.Inventory.Items.Remove(consumable);
                    }
                    break;
                case Enums.ConsumableType.Defense:
                    if (consumable is Item defenseItem)
                    {
                        // Apply defense buff logic here
                        Console.WriteLine($"{_player.Name}'s defense increased by {defenseItem.Value} for {consumable.BuffDuration.Value} turns!");
                        //remove item
                        _player.Inventory.Items.Remove(consumable);
                    }
                    break;
                default:
                    Console.WriteLine("Unknown consumable type.");
                    break;
            }
        }
        
    }
}



