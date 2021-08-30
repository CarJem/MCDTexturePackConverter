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
    public static class RedDust
    {
        public static void Generate(string javaName, string modelName, DungeonsRP_Blocks.Definition definition, Logic_BlockMapData conversionDataList)
        {
            string statePath = Assembler.GetTemplatePath("red_dust", "red_dust_blockstate.json");
            string json = File.ReadAllText(statePath);
            string edited_json = json.Replace("{!}", modelName);
            JavaRP_BlockState state = JsonConvert.DeserializeObject<JavaRP_BlockState>(edited_json);

            string up = Assembler.GetBlockTexture(definition.Textures.Up, conversionDataList.DungeonsData);
            string down = Assembler.GetBlockTexture(definition.Textures.Down, conversionDataList.DungeonsData);


            CreateModel(modelName, Assembler.GetTemplatePath("red_dust", "red_dust_dot_model.json"), "_dot", up, down);
            CreateModel(modelName, Assembler.GetTemplatePath("red_dust", "red_dust_side_alt_model.json"), "_side_alt", up, down);
            CreateModel(modelName, Assembler.GetTemplatePath("red_dust", "red_dust_side_alt0_model.json"), "_side_alt0", up, down);
            CreateModel(modelName, Assembler.GetTemplatePath("red_dust", "red_dust_side_alt1_model.json"), "_side_alt1", up, down);
            CreateModel(modelName, Assembler.GetTemplatePath("red_dust", "red_dust_side_model.json"), "_side", up, down);
            CreateModel(modelName, Assembler.GetTemplatePath("red_dust", "red_dust_side0_model.json"), "_side0", up, down);
            CreateModel(modelName, Assembler.GetTemplatePath("red_dust", "red_dust_side1_model.json"), "_side1", up, down);
            CreateModel(modelName, Assembler.GetTemplatePath("red_dust", "red_dust_up_model.json"), "_up", up, down);

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
