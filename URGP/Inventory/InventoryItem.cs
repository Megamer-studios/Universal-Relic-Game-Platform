// Signed by: Akumarin :3
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace URGP.Inventory
{
    public class InventoryItem
    {
        public string name { get; set; }
        public int id { get; set; }

        public event Action OnUse;

     
        public void Use()
        {
            OnUse?.Invoke();
        }

      
        public override string ToString()
        {
            return name ?? base.ToString();
        }
    }
}
