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
    public static class SnowLayer
    {
        public static void Generate(string javaName, string modelName, DungeonsRP_Blocks.Definition definition, Logic_BlockMapData conversionDataList)
        {
            string statePath = Assembler.GetTemplatePath("snow_layer", "snow_layer_blockstate.json");
            string json = File.ReadAllText(statePath);
            string edited_json = json.Replace("{!}", modelName);
            JavaRP_BlockState state = JsonConvert.DeserializeObject<JavaRP_BlockState>(edited_json);

            string all = Assembler.GetBlockTexture(definition.Textures.All, conversionDataList.DungeonsData);

            CreateModel(modelName, Assembler.GetTemplatePath("snow_layer", "snow_layer_model.json"), "_block", all);
            CreateModel(modelName, Assembler.GetTemplatePath("snow_layer", "snow_layer_height2_model.json"), "_height2", all);
            CreateModel(modelName, Assembler.GetTemplatePath("snow_layer", "snow_layer_height4_model.json"), "_height4", all);
            CreateModel(modelName, Assembler.GetTemplatePath("snow_layer", "snow_layer_height6_model.json"), "_height6", all);
            CreateModel(modelName, Assembler.GetTemplatePath("snow_layer", "snow_layer_height8_model.json"), "_height8", all);
            CreateModel(modelName, Assembler.GetTemplatePath("snow_layer", "snow_layer_height10_model.json"), "_height10", all);
            CreateModel(modelName, Assembler.GetTemplatePath("snow_layer", "snow_layer_height12_model.json"), "_height12", all);
            CreateModel(modelName, Assembler.GetTemplatePath("snow_layer", "snow_layer_height14_model.json"), "_height14", all);


            foreach (var entry in state.variants) Assembler.AddStateToBlockStates(Assembler.GetBlockName(javaName), entry.Key, entry.Value);
            foreach (var entry in state.multipart) Assembler.AddMultipartToBlockStates(Assembler.GetBlockName(javaName), entry);
        }

        private static void CreateModel(string modelName, string modelPath, string extension, string texture1)
        {
            string json2 = File.ReadAllText(modelPath);
            string edited_json2 = json2.Replace("{!}", texture1);
            var result = JsonConvert.DeserializeObject<JavaRP_BlockModel>(edited_json2);
            Assembler.AddBlockModel(modelName + extension, result);
        }
    }
}
