using MCDTexturePackConverter.Classes;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCDTexturePackConverter.Generators
{
    public static class CrossBlock
    {
        public static void Generate(string javaName, string dungeonsName, DungeonsRP_Blocks.Definition definition, BlockMapData conversionDataList)
        {
            JavaRP_BlockState state = new JavaRP_BlockState();
            JObject props = new JObject();
            string shortenedName = javaName.Remove("minecraft:");
            props.Add("model", ModelAssembler.BlockPrefix + shortenedName);
            state.variants.Add("", props);

            string texture_path = definition.Textures.All;
            string texture = ModelGenerator.ReducePath(StoredResources.BlockTextureDataToBlockPath(texture_path, conversionDataList.DungeonsData));

            JavaRP_BlockModel model = new JavaRP_BlockModel();
            model.parent = "minecraft:block/cross";
            model.textures.Add("cross", string.Format("minecraft:block/{0}", texture));

            ModelAssembler.BlockStates.Add(shortenedName, state);
            ModelAssembler.BlockModels.Add(shortenedName, model);
        }

        public static void GeneratePlant(string javaName, string dungeonsName, DungeonsRP_Blocks.Definition definition, BlockMapData conversionDataList)
        {
            JavaRP_BlockState state = new JavaRP_BlockState();
            JObject props = new JObject();
            string shortenedName = javaName.Remove("minecraft:");
            props.Add("model", ModelAssembler.BlockPrefix + shortenedName);
            state.variants.Add("", props);

            string texture_path = definition.Textures.Down;
            string texture = ModelGenerator.ReducePath(StoredResources.BlockTextureDataToBlockPath(texture_path, conversionDataList.DungeonsData));

            JavaRP_BlockModel model = new JavaRP_BlockModel();
            model.parent = "minecraft:block/cross";
            model.textures.Add("cross", string.Format("minecraft:block/{0}", texture));

            ModelAssembler.BlockStates.Add(shortenedName, state);
            ModelAssembler.BlockModels.Add(shortenedName, model);
        }
    }
}
