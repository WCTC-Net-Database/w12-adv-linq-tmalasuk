using ConsoleRpgEntities.Models.Attributes;
using ConsoleRpgEntities.Models.Equipments;

namespace ConsoleRpgEntities.Models.Characters.Monsters
{
    public abstract class Monster : IMonster, ITargetable
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public int RoomId { get; set; }

        public int Difficulty { get; set; }
        public int MaxHealth => Difficulty * 100;

        private int _health;
        public int Health
        {
            get => _health;
            set => _health = Math.Min(value, MaxHealth); // ensures Health never exceeds MaxHealth
        }
        public string MonsterType { get; set; }

        public int StunStack { get; set; }

        public int experienceGiven => Difficulty * 10;
        public List<Item> itemDrop { get; set; } = new List<Item>();

        protected Monster()
        {

        }

        public abstract void Attack(ITargetable target);

    }
}
