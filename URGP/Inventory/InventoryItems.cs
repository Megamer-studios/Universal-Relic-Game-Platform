// Signed by: Akumarin :3
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace URGP.Inventory
{
    public class InventoryItems
    {
        public static List<InventoryItem> Items { get; set; }

        public static void InitializeItems(Game1 game1)
        {
            Items = new List<InventoryItem>();
            InventoryItem CallingCard = new InventoryItem
            {
                name = "Calling card",
                id = 0
            };
            CallingCard.OnUse += () =>
            {
                game1.ConsoleText = "This is a calling card. It has the name 'Calling card' and the id 0.";
            };
            Items.Add(CallingCard);


        }

    
      

      
    }

}
