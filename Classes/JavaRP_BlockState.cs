using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace MCDTexturePackConverter.Classes
{
    public class JavaRP_BlockState
    {
        public Dictionary<string, JObject> variants { get; set; } = new Dictionary<string, JObject>();
        public List<JObject> multipart { get; set; } = new List<JObject>();
    }
}
