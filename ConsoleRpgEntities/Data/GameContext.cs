using ConsoleRpgEntities.Models.Abilities.PlayerAbilities;
using ConsoleRpgEntities.Models.Characters;
using ConsoleRpgEntities.Models.Characters.Monsters;
using ConsoleRpgEntities.Models.Equipments;
using Microsoft.EntityFrameworkCore;


namespace ConsoleRpgEntities.Data
{
    public class GameContext : DbContext
    {
        public DbSet<Player> Players { get; set; }
        public DbSet<Monster> Monsters { get; set; }
        public DbSet<Ability> Abilities { get; set; }
        public DbSet<Item> Items { get; set; }
        public DbSet<Equipment> Equipments { get; set; }

        public GameContext(DbContextOptions<GameContext> options) : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(
                "Server=(localdb)\\MSSQLLocalDB;Database=GameDb4;Trusted_Connection=True;");
        }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configure TPH for Character hierarchy
            modelBuilder.Entity<Monster>()
                .HasDiscriminator<string>(m=> m.MonsterType)
                .HasValue<Goblin>("Goblin");

            // Configure TPH for Ability hierarchy
            modelBuilder.Entity<Ability>()
                .HasDiscriminator<string>(pa=>pa.AbilityType)
                .HasValue<ShoveAbility>("ShoveAbility");

            // Configure many-to-many relationship
            modelBuilder.Entity<Player>()
                .HasMany(p => p.Abilities)
                .WithMany(a => a.Players)
                .UsingEntity(j => j.ToTable("PlayerAbilities"));

            modelBuilder.Entity<Player>()
                .HasOne(p => p.Inventory)
                .WithOne(i => i.Player)
                .HasForeignKey<Inventory>(i => i.PlayerId);

            // Call the separate configuration method to set up Equipment entity relationships
            ConfigureEquipmentRelationships(modelBuilder);

            base.OnModelCreating(modelBuilder);
        }

        private void ConfigureEquipmentRelationships(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Item>()
                .Property(i => i.ItemCategory)
                .HasConversion<string>()
                .HasColumnName("ItemCategory");

            modelBuilder.Entity<Item>()
                  .HasDiscriminator<string>("ItemCategory")
                  .HasValue<Equipment>("Equipment")
                  .HasValue<Consumable>("Consumable");

            modelBuilder.Entity<Equipment>().Property(i => i.EquipmentType).HasConversion<string>().HasColumnName("EquipmentType");
            modelBuilder.Entity<Equipment>().Property(i => i.Slot).HasConversion<string>().HasColumnName("EquipmentSlot");
            modelBuilder.Entity<Consumable>().Property(i => i.ConsumableType).HasConversion<string>().HasColumnName("ConsumableType");



        }
    }
}


