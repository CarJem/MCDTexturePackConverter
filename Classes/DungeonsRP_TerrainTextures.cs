using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Converters;

namespace MCDTexturePackConverter.Classes
{
    public class DungeonsRP_TerrainTextures
    {
        public class TexturesDataTypeConverter<TexturesData> : JsonConverter
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
                    var data = new DungeonsRP_TerrainTextures.TexturesData();
                    data.Paths.Add(value);
                    reader.Skip();
                    return data;
                }
                else if (reader.TokenType == JsonToken.StartArray)
                {
                    var data = new DungeonsRP_TerrainTextures.TexturesData();
                    while (reader.TokenType != JsonToken.EndArray)
                    {
                        if (reader.TokenType == JsonToken.String)
                        {
                            var value = serializer.Deserialize<string>(reader);
                            data.Paths.Add(value);                          
                        }
                        reader.Read();
                    }
                    reader.Skip();
                    return data;
                }
                else
                {
                    reader.Skip();
                    return new DungeonsRP_TerrainTextures.TexturesData();
                }
            }

            public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
            {
                serializer.Serialize(writer, value);
            }
        }

        public class TexturesData
        {
            public List<string> Paths { get; set; } = new List<string>();
        }

        public class TerrainData
        {
            [JsonConverter(typeof(TexturesDataTypeConverter<TexturesData>))]
            public TexturesData Textures { get; set; }
        }

        public string resource_pack_name { get; set; }
        public string texture_name { get; set; }
        public int padding { get; set; }
        public int num_mip_levels { get; set; }
        public Dictionary<string, TerrainData> texture_data { get; set; }

    }
}
