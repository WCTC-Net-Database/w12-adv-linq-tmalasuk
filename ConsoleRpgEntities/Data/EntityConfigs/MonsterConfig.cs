using ConsoleRpgEntities.Models.Characters.Monsters;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class MonsterConfig : IEntityTypeConfiguration<Goblin>
{
    public void Configure(EntityTypeBuilder<Goblin> builder)
    {
        builder.HasData(
            new Goblin { Id = 201, Name = "Goblin Grunt", Difficulty = 1, MonsterType = "Goblin", Health = 100, StunStack = 0, RoomId = 1},
            new Goblin { Id = 202, Name = "Goblin Sneak", Difficulty = 1, MonsterType = "Goblin", Health = 100, StunStack = 0, RoomId = 1},
            new Goblin { Id = 203, Name = "Goblin Cutthroat", Difficulty = 2, MonsterType = "Goblin", Health = 200, StunStack = 0 , RoomId = 2 },
            new Goblin { Id = 204, Name = "Goblin Scout", Difficulty = 2, MonsterType = "Goblin", Health = 200, StunStack = 0, RoomId = 2 },
            new Goblin { Id = 205, Name = "Goblin Bruiser", Difficulty = 3, MonsterType = "Goblin", Health = 300, StunStack = 0 , RoomId = 3 },
            new Goblin { Id = 206, Name = "Goblin Raider", Difficulty = 3, MonsterType = "Goblin", Health = 300, StunStack = 0 , RoomId = 3 },
            new Goblin { Id = 207, Name = "Goblin Slinger", Difficulty = 2, MonsterType = "Goblin", Health = 200, StunStack = 0 , RoomId = 2 },
            new Goblin { Id = 208, Name = "Goblin Firestarter", Difficulty = 4, MonsterType = "Goblin", Health = 400, StunStack = 0 , RoomId = 4 },
            new Goblin { Id = 209, Name = "Goblin Shadowblade", Difficulty = 4, MonsterType = "Goblin", Health = 400, StunStack = 0 , RoomId = 4 },
            new Goblin { Id = 210, Name = "Goblin Berserker", Difficulty = 5, MonsterType = "Goblin", Health = 500, StunStack = 0 , RoomId = 5 },
            new Goblin { Id = 211, Name = "Goblin Bonecrusher", Difficulty = 5, MonsterType = "Goblin", Health = 500, StunStack = 0 , RoomId = 5 },
            new Goblin { Id = 212, Name = "Goblin Poisonblade", Difficulty = 3, MonsterType = "Goblin", Health = 300, StunStack = 0 , RoomId = 3 },
            new Goblin { Id = 213, Name = "Goblin Stormcaller", Difficulty = 4, MonsterType = "Goblin", Health = 400, StunStack = 0 , RoomId = 4 }
        );
    }
}
