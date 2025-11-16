using System.ComponentModel.DataAnnotations.Schema;
using static ConsoleRpgEntities.Models.Equipments.Enums;

namespace ConsoleRpgEntities.Models.Equipments;

public class Equipment : Item
{
    
    public Enums.EquipmentType Type { get; set; }
    public Enums.EquipmentSlot? Slot { get; set; }

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
        string stat = Type == EquipmentType.Attack ? $"Attack: {Value}" : $"Defense: {Value}";
        return $"{Name} (Equipment, Slot: {Slot}, {stat})";
    }


}


