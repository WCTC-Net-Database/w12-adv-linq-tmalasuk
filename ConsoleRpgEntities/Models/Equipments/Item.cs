using System.ComponentModel.DataAnnotations.Schema;
using static ConsoleRpgEntities.Models.Equipments.Enums;

namespace ConsoleRpgEntities.Models.Equipments;


// TODO note this model has been updated from the previous version so a migration will be needed
public abstract class Item
{
    public int Id { get; set; }
    public string Name { get; set; }
    // Use one or the other depending on item

    public static readonly string ColumnFormat = "{0,-25}{1,-12}{2,-10}{3,-20}{4,6:F2}";

    public required string ItemCategory { get; set; }


    [Column(TypeName = "decimal(3, 2)")]
    public decimal Weight { get; set; }

    public int Value { get; set; }

    public override string ToString()
    {
        
        return string.Format(
            ColumnFormat,
            Name,
            ItemCategory,
            "-",       // slot
            "-",       // stat
            Weight
        );
    }
}
