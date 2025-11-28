using ConsoleRpgEntities.Models.Equipments;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleRpgEntities.Models.Containers
{
    public interface IItemContainer
    {
        ICollection<Item> Items { get; }
        

    }

}
