using System.Xml.Serialization;
using ConsoleRpgEntities.Models.Characters;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using static ConsoleRpgEntities.Models.Enums;

namespace ConsoleRpgEntities.Models.Equipments;

public class Consumable : Item
{
    
    public int? BuffDuration { get; set; } // Duration in turns
    public Enums.ConsumableType ConsumableType { get; set; } 
    public int TurnUsed { get; set; } = 0;

    public override string ToString()
    {
        string stat = $"{Value}";
        if (this.ConsumableType != ConsumableType.Heal && this.ConsumableType != ConsumableType.Mana){
            stat += $" / {BuffDuration} turns";
        }

        return string.Format(
            ColumnFormat,
            Name,
            ItemCategory,
            ConsumableType.ToString(),
            "-",      // no slot
            stat,
            Weight
        );
    }

}