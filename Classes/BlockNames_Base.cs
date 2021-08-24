using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCDTexturePackConverter.Classes
{
    public class BlockNames_Base
    {
        public Dictionary<int, string> pairs { get; set; }

        public string GetName(int ID)
        {
            string name = "null";
            if (pairs.ContainsKey(ID)) name = pairs[ID];
            return name;
        }
    }
}
