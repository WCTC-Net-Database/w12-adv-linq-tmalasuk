using ConsoleRpgEntities.Models.Characters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleRpgEntities.Models.Rooms.Interfaces
{
    public interface IInteractableRoom
    {
        List<string> AgilityInteraction(Player player);
        List<string> IntelligenceInteraction(Player player);
        List<string> Interact(Player player);
        List<string> StrengthInteraction(Player player);
    }

}
