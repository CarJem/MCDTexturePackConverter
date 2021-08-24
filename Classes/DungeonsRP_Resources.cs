using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace MCDTexturePackConverter.Classes
{
    public class DungeonsRP_Resources
    {
        public class Metas
        {
            [JsonPropertyName("atlas.items.meta")]
            public string AtlasItemsMeta;

            [JsonPropertyName("atlas.terrain.meta")]
            public string AtlasTerrainMeta;

            [JsonPropertyName("block.graphics.meta")]
            public string BlockGraphicsMeta;

            [JsonPropertyName("item.server.meta")]
            public string ItemServerMeta;
        }

        public class _Resources
        {
            [JsonPropertyName("models")]
            public Dictionary<string, string> Models;

            [JsonPropertyName("textures")]
            public Dictionary<string, string> Textures;

            [JsonPropertyName("metas")]
            public Metas Metas;
        }

        [JsonPropertyName("pack_id")]
        public string PackId;

        [JsonPropertyName("name")]
        public string Name;

        [JsonPropertyName("description")]
        public string Description;

        [JsonPropertyName("resources")]
        public _Resources Resources;
    }
}
