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
    public static class TreeBlock
    {
        public static void Generate(string javaName, string dungeonsName, DungeonsRP_Blocks.Definition definition, BlockMapData conversionDataList)
        {

            string shortenedName = javaName.Remove("minecraft:");

            string statePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Templates", "log_blockstate.json");
            string json = File.ReadAllText(statePath);
            string edited_json = json.Replace("{!}", shortenedName);
            JavaRP_BlockState state = JsonConvert.DeserializeObject<JavaRP_BlockState>(edited_json);

            string end = ModelGenerator.ReducePath(StoredResources.BlockTextureDataToBlockPath(definition.Textures.Up, conversionDataList.DungeonsData));
            string side = ModelGenerator.ReducePath(StoredResources.BlockTextureDataToBlockPath(definition.Textures.Side, conversionDataList.DungeonsData));

            string modelPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Templates", "log_model.json");
            string json2 = File.ReadAllText(modelPath);
            string edited_json2 = json2.Replace("{!}", side).Replace("{!2}", end);
            JavaRP_BlockModel model = JsonConvert.DeserializeObject<JavaRP_BlockModel>(edited_json2);

            string modelPath2 = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Templates", "log_horizontal_model.json");
            string json3 = File.ReadAllText(modelPath2);
            string edited_json3 = json3.Replace("{!}", side).Replace("{!2}", end);
            JavaRP_BlockModel model2 = JsonConvert.DeserializeObject<JavaRP_BlockModel>(edited_json3);

            ModelAssembler.BlockStates.Add(shortenedName, state);

            ModelAssembler.BlockModels.Add(shortenedName, model);
            ModelAssembler.BlockModels.Add(shortenedName + "_horizontal", model2);
        }
    }
}
