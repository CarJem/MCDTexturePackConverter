using MCDTexturePackConverter.Classes;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCDTexturePackConverter
{
    public static class Paths
    {
        public static string MCD_PACK_ICON = "pack_icon.png";
        public static string MCD_PACK_RESOURCES = "resources.json";
        public static string MCD_PACK_BLOCKS = "blocks.json";
        public static string MCJ_PACK_ICON = "pack.png";
        public static string MCJ_PACK_META = "pack.mcmeta";
        public static string MCD_PACK_TERRAIN_TEXTURE = Path.Combine("images", "terrain_texture.json");

        public static string MCD_BLOCK_FOLDER = Path.Combine("images", "blocks");
        public static string MCJ_BLOCK_FOLDER = Path.Combine("assets", "minecraft", "textures", "block");
        public static string MCJ_BLOCKSTATES_FOLDER = Path.Combine("assets", "minecraft", "blockstates");
        public static string MCJ_BLOCKMODELS_FOLDER = Path.Combine("assets", "minecraft", "models", "block");


        public static DungeonsRP_Resources Resources;
        public static DungeonsRP_TerrainTextures TerrainTextures;
        public static DungeonsRP_Blocks Blocks;

        public static void Init(string path)
        {
            string Resources_JSON = File.ReadAllText(Path.Combine(path, MCD_PACK_RESOURCES));
            string TerrainTextures_JSON = File.ReadAllText(Path.Combine(path, MCD_PACK_TERRAIN_TEXTURE));
            string Blocks_JSON = File.ReadAllText(Path.Combine(path, MCD_PACK_BLOCKS));

            Resources = JsonConvert.DeserializeObject<DungeonsRP_Resources>(Resources_JSON);
            TerrainTextures = JsonConvert.DeserializeObject<DungeonsRP_TerrainTextures>(TerrainTextures_JSON);
            Blocks = DungeonsRP_Blocks.Get(Blocks_JSON);
        }
        public static string BlockTextureDataToBlockPath(string texture, int? data)
        {
            return GetBlockPathFromResource(GetResourceByBlockTextureData(texture, data));
        }
        public static string GetResourceByBlockTextureData(string texture, int? data)
        {
            if (texture == null) return null;
            if (TerrainTextures.texture_data.ContainsKey(texture))
            {
                var textures = TerrainTextures.texture_data[texture].Textures.Paths;
                if (data != null && textures.Count() > data && data.Value >= 0) return textures[data.Value];
                else if (textures.Count() != 0) return textures[0];
                else return null;
            }
            return null;
        }
        public static string GetBlockPathFromResource(string resource)
        {
            if (resource == null) return null;
            if (Resources.Resources.Textures.ContainsKey(resource))
                return Resources.Resources.Textures[resource];
            return null;
        }
    }
}
