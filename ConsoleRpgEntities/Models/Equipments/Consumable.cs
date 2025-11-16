using System.Xml.Serialization;
using ConsoleRpgEntities.Models.Characters;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using static ConsoleRpgEntities.Models.Equipments.Enums;

namespace ConsoleRpgEntities.Models.Equipments;

public class Consumable : Item
{
    
    public int? BuffDuration { get; set; } // Duration in turns
    public Enums.ConsumableType Type { get; set; } // e.g., "Attack", "Defense", "Heal"

    public void Consume(Player player)
    {
        Console.WriteLine("Which consumable would you like to use?");
        foreach (var item in player.Inventory.Items.OfType<Consumable>())
        {
            Console.WriteLine($"- {item.Name}");
        }
        Console.Write("> ");
        var choice = Console.ReadLine();
        var consumable = player.Inventory.Items.OfType<Consumable>().FirstOrDefault(i => i.Name.Equals(choice, StringComparison.OrdinalIgnoreCase));

        switch (consumable.Type)
        {
            case Enums.ConsumableType.Heal:
                if (consumable is Item healItem)
                {
                    player.Health += healItem.Value;
                    Console.WriteLine($"{player.Name} healed for {healItem.Value} health!");
                    //remove item
                    player.Inventory.Items.Remove(consumable);
                }

                break;
            case Enums.ConsumableType.Attack:
                if (consumable is Item attackItem)
                {
                    // Apply attack buff logic here
                    Console.WriteLine($"{player.Name}'s attack increased by {attackItem.Value} for {consumable.BuffDuration.Value} turns!");
                    //remove item
                    player.Inventory.Items.Remove(consumable);
                }
                break;
            case Enums.ConsumableType.Defense:
                if (consumable is Item defenseItem)
                {
                    // Apply defense buff logic here
                    Console.WriteLine($"{player.Name}'s defense increased by {defenseItem.Value} for {consumable.BuffDuration.Value} turns!");
                    //remove item
                    player.Inventory.Items.Remove(consumable);
                }
                break;
            default:
                Console.WriteLine("Unknown consumable type.");
                break;
        }
    }

    public override string ToString()
    {
        string details = Name + " (Consumable";

        switch (Type)
        {
            case ConsumableType.Heal:
                details += $", Heal: {Value}";
                break;
            case ConsumableType.Attack:
                details += $", Attack Buff: {Value} for {BuffDuration} turns";
                break;
            case ConsumableType.Defense:
                details += $", Defense Buff: {Value} for {BuffDuration} turns";
                break;
        }

        details += ")";
        return details;
    }

}