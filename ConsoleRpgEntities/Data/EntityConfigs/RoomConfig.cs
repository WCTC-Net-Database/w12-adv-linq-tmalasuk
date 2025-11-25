using ConsoleRpgEntities.Models.Rooms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ConsoleRpgEntities.Models.Equipments;
using ConsoleRpgEntities.Models.Items;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class DungeonConfig : IEntityTypeConfiguration<Dungeon>
{
    public void Configure(EntityTypeBuilder<Dungeon> builder)
    {
        builder.HasData(
            new Dungeon
            {
                Id = 1,
                Name = "Dungeon",
                Description = "A dark, damp cell with chains on the walls.",
                RoomType = "Dungeon",
                IsLocked = true,
                KeyFormed = false,
                StoneGrabbed = false,
                CrackFound = false,
                Difficulty = 1
            }
        );
    }
}

public class TortureChamberConfig : IEntityTypeConfiguration<TortureChamber>
{
    public void Configure(EntityTypeBuilder<TortureChamber> builder)
    {
        builder.HasData(
            new TortureChamber
            {
                Id = 2,
                Name = "Torture Chamber",
                Description = "An ominous room filled with instruments of pain.",
                RoomType = "Torture Chamber",
                Difficulty = 2
            }
        );
    }
}

public class StairwellConfig : IEntityTypeConfiguration<Stairwell>
{
    public void Configure(EntityTypeBuilder<Stairwell> builder)
    {
        builder.HasData(
            new Stairwell
            {
                Id = 3,
                Name = "Spiral Stairwell",
                Description = "A narrow stone stairwell leading upward.",
                RoomType = "Stairwell",
                Difficulty = 1
            }
        );
    }
}

public class GuardRoomConfig : IEntityTypeConfiguration<GuardRoom>
{
    public void Configure(EntityTypeBuilder<GuardRoom> builder)
    {
        builder.HasData(
            new GuardRoom
            {
                Id = 4,
                Name = "Guard Room",
                Description = "A well-lit room with tables, chairs, and weapons racks.",
                RoomType = "Guard Area",
                Difficulty = 3
            }
        );
    }
}

public class BarracksConfig : IEntityTypeConfiguration<Barracks>
{
    public void Configure(EntityTypeBuilder<Barracks> builder)
    {
        builder.HasData(
            new Barracks
            {
                Id = 5,
                Name = "Barracks",
                Description = "Rows of beds and personal lockers for the castle guards.",
                RoomType = "Barracks",
                Difficulty = 2
            }
        );
    }
}

public class SculleryConfig : IEntityTypeConfiguration<Scullery>
{
    public void Configure(EntityTypeBuilder<Scullery> builder)
    {
        builder.HasData(
            new Scullery
            {
                Id = 6,
                Name = "Scullery",
                Description = "The kitchen’s cleaning and prep area, filled with utensils and sinks.",
                RoomType = "Scullery",
                Difficulty = 1
            }
        );
    }
}

public class ArmoryConfig : IEntityTypeConfiguration<Armory>
{
    public void Configure(EntityTypeBuilder<Armory> builder)
    {
        builder.HasData(
            new Armory
            {
                Id = 7,
                Name = "Armory",
                Description = "A room stacked with weapons, armor, and training gear.",
                RoomType = "Armory",
                Difficulty = 4
            }
        );
    }
}

public class GardenConfig : IEntityTypeConfiguration<Garden>
{
    public void Configure(EntityTypeBuilder<Garden> builder)
    {
        builder.HasData(
            new Garden
            {
                Id = 8,
                Name = "Castle Garden",
                Description = "A beautiful, well-kept garden leading to the castle entrance.",
                RoomType = "Garden",
                Difficulty = 1
            }
        );
    }
}

