using ConsoleRpgEntities.Models.Abilities.PlayerAbilities;
using ConsoleRpgEntities.Models.Characters;
using ConsoleRpgEntities.Models.Characters.Monsters;
using ConsoleRpgEntities.Models.Equipments;
using ConsoleRpgEntities.Models.Items;
using ConsoleRpgEntities.Models.Rooms;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Reflection;


namespace ConsoleRpgEntities.Data
{
    public class GameContext : DbContext
    {
        public DbSet<Player> Players { get; set; }
        public DbSet<Monster> Monsters { get; set; }
        public DbSet<Ability> Abilities { get; set; }
        public DbSet<Item> Items { get; set; }
        public DbSet<Equipment> Equipments { get; set; }

        public DbSet<Room> Rooms { get; set; }

        public GameContext(DbContextOptions<GameContext> options) : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(
                "Server=(localdb)\\MSSQLLocalDB;Database=GameDb;Trusted_Connection=True;");
        }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //Configure Room TPH
            //modelBuilder.Entity<Room>()
            //.HasOne(r => r.North)
            //.WithOne()
            //.HasForeignKey<Room>(r => r.Id);

            //modelBuilder.Entity<Room>()
            //.HasOne(r => r.East)
            //.WithOne()
            //.HasForeignKey<Room>(r => r.Id);

            //modelBuilder.Entity<Room>()
            //.HasOne(r => r.South)
            //.WithOne()
            //.HasForeignKey<Room>(r => r.Id);

            //modelBuilder.Entity<Room>()
            //.HasOne(r => r.West)
            //.WithOne()
            //.HasForeignKey<Room>(r => r.Id);

            //modelBuilder.Entity<Room>()
            //.HasOne(r => r.Up)
            //.WithOne()
            //.HasForeignKey<Room>(r => r.Id);

            //modelBuilder.Entity<Room>()
            //.HasOne(r => r.Down)
            //.WithOne()
            //.HasForeignKey<Room>(r => r.Id);

            modelBuilder.Entity<Room>()
            .HasDiscriminator<string>("RoomType")
            .HasValue<Dungeon>("Dungeon")
            .HasValue<TortureChamber>("TortureChamber")
            .HasValue<Stairwell>("Stairwell")
            .HasValue<GuardRoom>("GuardRoom")
            .HasValue<Barracks>("Barracks")
            .HasValue<Scullery>("Scullery")
            .HasValue<Armory>("Armory")
            .HasValue<Garden>("Garden")
            .HasValue<Hallway>("Hallway");

            modelBuilder.Entity<Spellbook>().HasOne(s => s.GrantedAbility)
               .WithMany()
               .HasForeignKey(s => s.GrantedAbilityId);

            // Configure TPH for Character hierarchy
            modelBuilder.Entity<Monster>()
                .HasDiscriminator<string>(m=> m.MonsterType)
                .HasValue<Goblin>("Goblin");

            
            modelBuilder.Entity<Ability>().HasKey(a => a.Id);
            modelBuilder.Entity<Ability>()
                .HasDiscriminator<string>("AbilityType")
                .HasValue<ArcaneBarrage>("Arcane")
                .HasValue<NatureEmbrace>("Healing")
                .HasValue<NullifyingAegis>("Defensive")
                .HasValue<ShadowVeil>("Physical")
                .HasValue<SiphoningStrike>("Hybrid");


            modelBuilder.Entity<Room>()
            .HasDiscriminator<string>("RoomType") // <-- Use your existing column
            .HasValue<Armory>("Armory")
            .HasValue<TortureChamber>("Torture Chamber")
            .HasValue<Hallway>("Hallway")
            .HasValue<Barracks>("Barracks")
            .HasValue<Dungeon>("Dungeon")
            .HasValue<Garden>("Garden")
            .HasValue<GuardRoom>("Guard Area")
            .HasValue<Scullery>("Scullery")
            .HasValue<Stairwell>("Stairwell");

            modelBuilder.Entity<Dungeon>()
                .Property(d => d.IsLocked)
                .HasDefaultValue(true);

            modelBuilder.Entity<Dungeon>()
                .Property(d => d.KeyFormed)
                .HasDefaultValue(false);

            modelBuilder.Entity<Dungeon>()
                .Property(d => d.StoneGrabbed)
                .HasDefaultValue(false);

            modelBuilder.Entity<Dungeon>()
                .Property(d => d.CrackFound)
                .HasDefaultValue(false);


            // Configure many-to-many relationship
            modelBuilder.Entity<Player>()
                .HasMany(p => p.Abilities)
                .WithMany(a => a.Players)
                .UsingEntity(j => j.ToTable("PlayerAbilities"));

            modelBuilder.Entity<Player>()
                .HasOne(p => p.Inventory)
                .WithOne(i => i.Player)
                .HasForeignKey<Inventory>(i => i.PlayerId);

            modelBuilder.Entity<Player>()
                .Property(p => p.classType)
                .HasConversion<string>()
                .HasColumnName("ClassType");

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


