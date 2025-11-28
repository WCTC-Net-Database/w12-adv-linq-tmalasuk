using System.ComponentModel.DataAnnotations.Schema;
using static ConsoleRpgEntities.Models.Enums;

namespace ConsoleRpgEntities.Models.Equipments;

public class Equipment : Item
{

    
    
    public Enums.EquipmentType EquipmentType { get; set; }
    public Enums.EquipmentSlot? Slot { get; set; }

    //TODO: Add cool effects to rare items
    public string? SpecialType { get; set; }

    public int SpecialValue { get; set; }

    public string? SpecialDescription { get; set; }

    public void ListEquipment(Dictionary<Enums.EquipmentSlot, Equipment> equipped)
    {
        foreach (Enums.EquipmentSlot slot in Enum.GetValues(typeof(Enums.EquipmentSlot)))
        {
            if (equipped.TryGetValue(slot, out var item))
                Console.WriteLine($"{slot}: {item.Name}");
            else
                Console.WriteLine($"{slot}: none");
        }
    }

    public override string ToString()
    {
        string stat;
        if (EquipmentType == Enums.EquipmentType.Attack)
        {
            stat = $"Attack Power: {Value}";
        }
        else // Defense
        {
            stat = $"Defense Power: {Value}";
        }

        return string.Format(
            ColumnFormat,
            Name,
            ItemCategory,
            EquipmentType.ToString(),
            Slot.ToString(),
            stat,
            Weight
        );
    }



}


