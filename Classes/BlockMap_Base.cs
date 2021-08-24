using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCDTexturePackConverter.Classes
{
    public class BlockMap_Base
    {
        public Dictionary<string, JToken> definitions { get; set; }
    }
}
