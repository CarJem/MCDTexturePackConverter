using MCDTexturePackConverter.Classes;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCDTexturePackConverter.Generators
{
    public static class StairBlock
    {
        public static void Generate(string javaName, string dungeonsName, DungeonsRP_Blocks.Definition definition, BlockMapData conversionDataList)
        {
            string shortenedName = javaName.Remove("minecraft:");

            string statePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Templates", "stair_blockstate.json");
            string json = File.ReadAllText(statePath);
            string edited_json = json.Replace("{!}", shortenedName);
            JavaRP_BlockState state = JsonConvert.DeserializeObject<JavaRP_BlockState>(edited_json);

            string all = ModelGenerator.ReducePath(StoredResources.BlockTextureDataToBlockPath(definition.Textures.All, conversionDataList.DungeonsData));
            string bottom = ModelGenerator.ReducePath(StoredResources.BlockTextureDataToBlockPath(definition.Textures.Up, conversionDataList.DungeonsData));
            string top = ModelGenerator.ReducePath(StoredResources.BlockTextureDataToBlockPath(definition.Textures.Down, conversionDataList.DungeonsData));
            string side = ModelGenerator.ReducePath(StoredResources.BlockTextureDataToBlockPath(definition.Textures.Side, conversionDataList.DungeonsData));

            bool useAll = bottom == null || top == null || side == null;

            string modelPath1 = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Templates", "stairs_model.json");
            string modelPath2 = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Templates", "stairs_inner_model.json");
            string modelPath3 = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Templates", "stairs_outer_model.json");

            var model1 = CreateModel(modelPath1, (useAll ? all : bottom), (useAll ? all : top), (useAll ? all : side));
            var model2 = CreateModel(modelPath2, (useAll ? all : bottom), (useAll ? all : top), (useAll ? all : side));
            var model3 = CreateModel(modelPath3, (useAll ? all : bottom), (useAll ? all : top), (useAll ? all : side));


            ModelAssembler.BlockStates.Add(shortenedName, state);

            ModelAssembler.BlockModels.Add(shortenedName, model1);
            ModelAssembler.BlockModels.Add(shortenedName + "_inner", model2);
            ModelAssembler.BlockModels.Add(shortenedName + "_outer", model3);
        }


        private static JavaRP_BlockModel CreateModel(string modelPath, string bottom, string top, string side)
        {
            string json2 = File.ReadAllText(modelPath);
            string edited_json2 = json2.Replace("{!1}", bottom).Replace("{!2}", top).Replace("{!3}", side);
            return JsonConvert.DeserializeObject<JavaRP_BlockModel>(edited_json2);
        }
    }
}
