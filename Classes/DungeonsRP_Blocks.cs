using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCDTexturePackConverter.Classes
{
    public class DungeonsRP_Blocks
    {
        public class TexturesTypeConverter<Textures> : JsonConverter
        {
            public override bool CanConvert(Type objectType)
            {
                return true;
            }

            public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
            {
                if (reader.TokenType == JsonToken.String)
                {
                    var value = serializer.Deserialize<string>(reader);
                    return new DungeonsRP_Blocks.Textures() { All = value };
                }
                else return serializer.Deserialize<Textures>(reader);
            }

            public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
            {
                serializer.Serialize(writer, value);
            }
        }

        public class Definition
        {
            public object Isotropic { get; set; }

            public string Blockshape { get; set; }

            [JsonConverter(typeof(TexturesTypeConverter<Textures>))]
            public Textures Textures { get; set; }

            [JsonConverter(typeof(TexturesTypeConverter<Textures>))]
            public Textures CarriedTextures { get; set; }
        }
        public class Textures
        {
            public string Up { get; set; }
            public string Down { get; set; }
            public string North { get; set; }
            public string South { get; set; }
            public string West { get; set; }
            public string East { get; set; }
            public string Side { get; set; }
            public string All { get; set; }
        }

        public Dictionary<string, Definition> blocks { get; set; }


        public static DungeonsRP_Blocks Get(string json)
        {
            var result = JsonConvert.DeserializeObject<Dictionary<string, Definition>>(json);
            return new DungeonsRP_Blocks() { blocks = result };
        }
    }
}
