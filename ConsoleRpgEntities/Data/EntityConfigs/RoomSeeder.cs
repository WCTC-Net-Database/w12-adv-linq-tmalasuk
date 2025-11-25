using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ConsoleRpgEntities.Models.Rooms;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace ConsoleRpgEntities.Data.Seeding
{
    public class RoomSeeder
    {
        /// <summary>
        /// Call this AFTER loading rooms from the database.
        /// Links all rooms according to your castle labyrinth layout.
        /// </summary>
        public void LinkRooms(DbContext context)
        {
            // Load all rooms from the database
            var rooms = context.Set<Room>().ToList();

            // Find rooms by type (or Id)
            var dungeon = rooms.OfType<Dungeon>().FirstOrDefault(r => r.Id == 1);
            var tortureChamber = rooms.OfType<TortureChamber>().FirstOrDefault(r => r.Id == 2);
            var stairwell = rooms.OfType<Stairwell>().FirstOrDefault(r => r.Id == 3);
            var guardRoom = rooms.OfType<GuardRoom>().FirstOrDefault(r => r.Id == 4);
            var barracks = rooms.OfType<Barracks>().FirstOrDefault(r => r.Id == 5);
            var scullery = rooms.OfType<Scullery>().FirstOrDefault(r => r.Id == 6);
            var armory = rooms.OfType<Armory>().FirstOrDefault(r => r.Id == 7);
            var garden = rooms.OfType<Garden>().FirstOrDefault(r => r.Id == 8);

            // --- Link Rooms ---

            // Dungeon -> Torture Chamber
            if (dungeon != null && tortureChamber != null)
            {
                dungeon.Up = tortureChamber;
                tortureChamber.Down = dungeon;
            }

            // Torture Chamber -> Stairwell
            if (tortureChamber != null && stairwell != null)
            {
                tortureChamber.Up = stairwell;
                stairwell.Down = tortureChamber;
            }

            // Stairwell -> Guard Room
            if (stairwell != null && guardRoom != null)
            {
                stairwell.Up = guardRoom;
                guardRoom.Down = stairwell;
            }

            // Guard Room -> Barracks (West)
            if (guardRoom != null && barracks != null)
            {
                guardRoom.West = barracks;
                barracks.East = guardRoom;
            }

            // Barracks -> Scullery (North)
            if (barracks != null && scullery != null)
            {
                barracks.North = scullery;
                scullery.South = barracks;
            }

            // Barracks -> Armory (South)
            if (barracks != null && armory != null)
            {
                barracks.South = armory;
                armory.North = barracks;
            }

            // Guard Room -> Garden (North)
            if (guardRoom != null && garden != null)
            {
                guardRoom.North = garden;
                garden.South = guardRoom;
            }

            context.SaveChanges();
        }
    }
}
