using ConsoleRpgEntities.Models.Attributes;
using ConsoleRpgEntities.Models.Characters;

namespace ConsoleRpgEntities.Models.Abilities.PlayerAbilities
{
    public abstract class Ability
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string AbilityType { get; set; } // eventually make this an enum
        public int ManaCost { get; set; }
        public int TurnUsed { get; set; }
        public int BuffDuration { get; set; }

        public virtual ICollection<Player> Players { get; set; }

        protected Ability()
        {
            Players = new List<Player>();
        }

        public abstract int Activate(Player user);
    }
}
