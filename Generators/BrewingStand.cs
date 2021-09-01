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
    public static class BrewingStand
    {
        public static void Generate(string javaName, string modelName, DungeonsRP_Blocks.Definition definition, Logic_BlockMapData conversionDataList)
        {
            string statePath = Assembler.GetTemplatePath("brewing_stand", "brewing_stand_blockstate.json");
            string json = File.ReadAllText(statePath);
            string edited_json = json.Replace("{!}", modelName);
            JavaRP_BlockState state = JsonConvert.DeserializeObject<JavaRP_BlockState>(edited_json);

            string stand = Assembler.GetBlockTexture(definition.Textures.Up, conversionDataList.DungeonsData);
            string _base = Assembler.GetBlockTexture(definition.Textures.Down, conversionDataList.DungeonsData);

            CreateModel(modelName, Assembler.GetTemplatePath("brewing_stand", "brewing_stand_model.json"), "", _base, stand);
            CreateModel(modelName, Assembler.GetTemplatePath("brewing_stand", "brewing_stand_bottle0_model.json"), "_bottle0", _base, stand);
            CreateModel(modelName, Assembler.GetTemplatePath("brewing_stand", "brewing_stand_bottle1_model.json"), "_bottle1", _base, stand);
            CreateModel(modelName, Assembler.GetTemplatePath("brewing_stand", "brewing_stand_bottle2_model.json"), "_bottle2", _base, stand);
            CreateModel(modelName, Assembler.GetTemplatePath("brewing_stand", "brewing_stand_empty0_model.json"), "_empty0", _base, stand);
            CreateModel(modelName, Assembler.GetTemplatePath("brewing_stand", "brewing_stand_empty1_model.json"), "_empty1", _base, stand);
            CreateModel(modelName, Assembler.GetTemplatePath("brewing_stand", "brewing_stand_empty2_model.json"), "_empty2", _base, stand);


            foreach (var entry in state.variants) Assembler.AddStateToBlockStates(Assembler.GetBlockName(javaName), entry.Key, entry.Value);
            foreach (var entry in state.multipart) Assembler.AddMultipartToBlockStates(Assembler.GetBlockName(javaName), entry);
        }

        private static void CreateModel(string modelName, string modelPath, string extension, string _base, string stand)
        {
            string json2 = File.ReadAllText(modelPath);
            string edited_json2 = json2.Replace("{!2}", _base).Replace("{!1}", stand);
            var result = JsonConvert.DeserializeObject<JavaRP_BlockModel>(edited_json2);
            Assembler.AddBlockModel(modelName + extension, result);
        }
    }
}
