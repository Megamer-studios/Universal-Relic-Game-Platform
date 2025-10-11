using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace URGP
{
    public class SaveData
    {
        public int Line { get; set; }
        public string FilePath { get; set; }
        public List<int> InventoryItemIds { get; set; } = new();
    }
}
