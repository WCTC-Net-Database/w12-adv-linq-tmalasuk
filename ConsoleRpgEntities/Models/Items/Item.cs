using ConsoleRpgEntities.Models.Items;
using System.ComponentModel.DataAnnotations.Schema;
using static ConsoleRpgEntities.Models.Enums;

namespace ConsoleRpgEntities.Models.Equipments;


// TODO note this model has been updated from the previous version so a migration will be needed
public abstract class Item
{
    public int Id { get; set; }
    public string Name { get; set; }

    public string Rarity { get; set; }

    public string Description { get; set; }

    public static readonly string ColumnFormat = "{0,-35}{2,-10}{1,-12}{2,-12}{3,-10}{4,-20}{5,6:F2}";


    public required string ItemCategory { get; set; }


    [Column(TypeName = "decimal(5, 2)")]
    public decimal Weight { get; set; }

    public int Value { get; set; }

    public virtual (string Name, Dictionary<string, ConsoleColor> rarity, string Category, string Slot, string Type, string Stat, decimal Weight) GetColumnData(Item item)
    {
        var rarity = new Dictionary<string, ConsoleColor> { { item.Rarity, GetColorFromRarity(item.Rarity) } };
        // Assuming 'item.Value' is where the stat value comes from.
        var stat = item.Value.ToString(); // <-- Changed from just 'Value.ToString()'
        var slot = "";
        var type = "";
        if (item is Equipment e)
        {
            slot = e.Slot.ToString();
            type = e.EquipmentType.ToString();
        }
        else if (item is Consumable c)
        {
            slot = "-";
            type = c.ConsumableType.ToString();
        }
        else
        {
            slot = "-";
            type = "-";
        }
        // Assuming 'item.ItemCategory' is a property/field
        return (item.Name, rarity, item.ItemCategory, slot, type, stat, item.Weight); // <-- Changed from bare variables Name, ItemCategory, Weight
    }

    private ConsoleColor GetColorFromRarity(string rarity)
    {
        return rarity.ToLower() switch
        {
            "common" => ConsoleColor.Gray,
            "uncommon" => ConsoleColor.Green,
            "rare" => ConsoleColor.Blue,
            "epic" => ConsoleColor.Magenta,
            "legendary" => ConsoleColor.Yellow,
            "mythic" => ConsoleColor.DarkYellow,
            "ancient" => ConsoleColor.Red,
            _ => ConsoleColor.White
        };
    }


    public override string ToString()
    {
        
        return string.Format(
            ColumnFormat,
            Name,
            Rarity,
            ItemCategory,
            "-",        
            "-",       
            "-",       
            Weight
        );
    }
}
