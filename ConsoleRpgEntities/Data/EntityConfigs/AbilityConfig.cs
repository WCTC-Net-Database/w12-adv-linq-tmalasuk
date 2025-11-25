using ConsoleRpgEntities.Models.Abilities.PlayerAbilities;
using ConsoleRpgEntities.Models.Equipments;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
public class ArcaneConfig : IEntityTypeConfiguration<ArcaneBarrage>
{
    public void Configure(EntityTypeBuilder<ArcaneBarrage> builder)
    { 
        builder.HasData(
            new ArcaneBarrage
            {
                Id = 1,
                Name = "Arcane Barrage",
                Description = "Blasts a foe with high magic damage based on intelligence",
                AbilityType = "Arcane",
                ManaCost = 20,
                BuffDuration = 0,
                TurnUsed = 0
            }      
        );

    }
}
public class NatureEmbraceConfig : IEntityTypeConfiguration<NatureEmbrace>
{
    public void Configure(EntityTypeBuilder<NatureEmbrace> builder)
    {
        builder.HasData(
            new NatureEmbrace
            {
                Id = 2,
                Name = "Nature's Embrace",
                Description = "Initial heal with lingering heal over 3 turns",
                AbilityType = "Healing",
                ManaCost = 12,
                BuffDuration = 3,
                TurnUsed = 0
            }
        );
    }
}
public class ShadowVeilConfig : IEntityTypeConfiguration<ShadowVeil>
{
    public void Configure(EntityTypeBuilder<ShadowVeil> builder)
    {
        builder.HasData(
            new ShadowVeil
            {
                Id = 3,
                Name = "Shadow Veil",
                Description = "A swift attack that deals damage and has a chance to stun based on agility",
                AbilityType = "Physical",
                ManaCost = 8,
                BuffDuration = 1,
                TurnUsed = 0
            }
        );
    }
}
public class NullifyingAegisConfig : IEntityTypeConfiguration<NullifyingAegis>
{
    public void Configure(EntityTypeBuilder<NullifyingAegis> builder)
    {
        builder.HasData(
            new NullifyingAegis
            {
                Id = 4,
                Name = "Nullifying Aegis",
                Description = "Protects against next enemy attack and deflects it back",
                AbilityType = "Defensive",
                ManaCost = 10,
                BuffDuration = 1,
                TurnUsed = 0
            }
        );
    }
}
public class SiphoningStrikeConfig : IEntityTypeConfiguration<SiphoningStrike>
{
    public void Configure(EntityTypeBuilder<SiphoningStrike> builder)
    {
        builder.HasData(
            new SiphoningStrike
            {
                Id = 5,
                Name = "Siphoning Strike",
                Description = "Strike enemy and heal for damage done",
                AbilityType = "Hybrid",
                ManaCost = 8,
                BuffDuration = 0,
                TurnUsed = 0
            }
        );
    }
}
