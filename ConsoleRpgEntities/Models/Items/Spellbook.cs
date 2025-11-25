using ConsoleRpgEntities.Models.Abilities.PlayerAbilities;
using ConsoleRpgEntities.Models.Equipments;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleRpgEntities.Models.Items
{
    public class Spellbook : Item
    {
        public int GrantedAbilityId { get; set; }
        public virtual Ability GrantedAbility { get; set; }

        public Spellbook()
        {
           
        }
    }
}
