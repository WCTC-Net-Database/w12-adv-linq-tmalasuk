using System.Xml.Serialization;
using ConsoleRpgEntities.Models.Characters;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using static ConsoleRpgEntities.Models.Equipments.Enums;

namespace ConsoleRpgEntities.Models.Equipments;

public class Consumable : Item
{
    
    public int? BuffDuration { get; set; } // Duration in turns
    public Enums.ConsumableType ConsumableType { get; set; } // e.g., "Attack", "Defense", "Heal"



    public override string ToString()
    {
        string stat = $"{Value}";
        if (this.ConsumableType != ConsumableType.Heal){
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