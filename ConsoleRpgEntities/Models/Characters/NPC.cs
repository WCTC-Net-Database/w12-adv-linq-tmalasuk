using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleRpgEntities.Models.Characters
{
    public class NPC
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Role { get; set; } 
        public string Dialogue { get; set; }
        public NPC()
        {

        }

        public void Talk()
        {

        }
    }
}
