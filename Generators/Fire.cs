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
    public static class Fire
    {
        public static void Generate(string javaName, string modelName, DungeonsRP_Blocks.Definition definition, Logic_BlockMapData conversionDataList)
        {
            string statePath = Assembler.GetTemplatePath("fire", "fire_blockstate.json");
            string json = File.ReadAllText(statePath);
            string edited_json = json.Replace("{!}", modelName);
            JavaRP_BlockState state = JsonConvert.DeserializeObject<JavaRP_BlockState>(edited_json);

            string fire_0 = Assembler.GetBlockTexture(definition.Textures.All, conversionDataList.DungeonsData);
            string fire_1 = Assembler.GetBlockTexture(definition.Textures.All, conversionDataList.DungeonsData);

            CreateModel(modelName, Assembler.GetTemplatePath("fire", "fire_floor0_model.json"), "_floor0", fire_0, fire_1);
            CreateModel(modelName, Assembler.GetTemplatePath("fire", "fire_floor1_model.json"), "_floor1", fire_0, fire_1);
            CreateModel(modelName, Assembler.GetTemplatePath("fire", "fire_side_alt0_model.json"), "_side_alt0", fire_0, fire_1);
            CreateModel(modelName, Assembler.GetTemplatePath("fire", "fire_side_alt1_model.json"), "_side_alt1", fire_0, fire_1);
            CreateModel(modelName, Assembler.GetTemplatePath("fire", "fire_side0_model.json"), "_side0", fire_0, fire_1);
            CreateModel(modelName, Assembler.GetTemplatePath("fire", "fire_side1_model.json"), "_side1", fire_0, fire_1);
            CreateModel(modelName, Assembler.GetTemplatePath("fire", "fire_up_alt0_model.json"), "_up_alt0", fire_0, fire_1);
            CreateModel(modelName, Assembler.GetTemplatePath("fire", "fire_up_alt1_model.json"), "_up_alt1", fire_0, fire_1);
            CreateModel(modelName, Assembler.GetTemplatePath("fire", "fire_up0_model.json"), "_up0", fire_0, fire_1);
            CreateModel(modelName, Assembler.GetTemplatePath("fire", "fire_up1_model.json"), "_up1", fire_0, fire_1);

            foreach (var entry in state.variants) Assembler.AddStateToBlockStates(Assembler.GetBlockName(javaName), entry.Key, entry.Value);
            foreach (var entry in state.multipart) Assembler.AddMultipartToBlockStates(Assembler.GetBlockName(javaName), entry);
        }

        private static void CreateModel(string modelName, string modelPath, string extension, string texture1, string texture2)
        {
            string json2 = File.ReadAllText(modelPath);
            string edited_json2 = json2.Replace("{!0}", texture1).Replace("{!1}", texture2);
            var result = JsonConvert.DeserializeObject<JavaRP_BlockModel>(edited_json2);
            Assembler.AddBlockModel(modelName + extension, result);
        }
    }
}
