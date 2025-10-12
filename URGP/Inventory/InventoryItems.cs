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

        public static void InitializeItems()
        {
            Items = new List<InventoryItem>();
            AddItem("Calling Card", 0);


        }

        public static void AddItem(string name, int id)
        {
            InventoryItem item = new InventoryItem
            {
                name = name,
                id = id
            };
            Items.Add(item);
        }
    }

}
