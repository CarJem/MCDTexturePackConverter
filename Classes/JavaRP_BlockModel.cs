using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCDTexturePackConverter.Classes
{
    public class JavaRP_BlockModel
    {
        public string parent { get; set; }
        public Dictionary<string, string> textures { get; set; } = new Dictionary<string, string>();
        public List<JObject> elements { get; set; } = new List<JObject>();
    }
}
