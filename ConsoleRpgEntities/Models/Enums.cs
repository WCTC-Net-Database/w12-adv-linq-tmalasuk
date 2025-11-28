namespace ConsoleRpgEntities.Models;

public class Enums
{
    public enum ConsumableType
    {
        Heal,
        Attack,
        Defense,
        Mana
    }

    public enum EquipmentType
    {
        Attack,
        Defense
    }

    public enum ItemCategory
    {
        Consumable,
        Equipment
    }

    public enum EquipmentSlot
    {
        Head,
        Chest,
        Legs,
        Feet,
        Hands,
        Weapon
    }

    public enum PlayerClass
    {
        Knight,
        Mage,
        Archer
    }

}
