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
    public static class Lever
    {
        public static void Generate(string javaName, string modelName, DungeonsRP_Blocks.Definition definition, Logic_BlockMapData conversionDataList)
        {
            string statePath = Assembler.GetTemplatePath("lever", "lever_blockstate.json");
            string json = File.ReadAllText(statePath);
            string edited_json = json.Replace("{!}", modelName);
            JavaRP_BlockState state = JsonConvert.DeserializeObject<JavaRP_BlockState>(edited_json);

            string lever = Assembler.GetBlockTexture(definition.Textures.Up, conversionDataList.DungeonsData);
            string rest = Assembler.GetBlockTexture("cobblestone", 0);

            CreateModel(modelName, Assembler.GetTemplatePath("lever", "lever_model.json"), "", lever, rest);
            CreateModel(modelName, Assembler.GetTemplatePath("lever", "lever_on_model.json"), "_on", lever, rest);

            foreach (var entry in state.variants) Assembler.AddStateToBlockStates(Assembler.GetBlockName(javaName), entry.Key, entry.Value);
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
