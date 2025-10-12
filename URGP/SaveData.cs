// Signed by: Akumarin :3
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

   
        public float BottomMidX { get; set; }
        public float BottomMidY { get; set; }
        public float BottomLeftX { get; set; }
        public float BottomLeftY { get; set; }
        public float BottomRightX { get; set; }
        public float BottomRightY { get; set; }


    }

}
