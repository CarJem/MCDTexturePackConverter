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
    public static class Anvil
    {
        public static void Generate(string javaName, string modelName, DungeonsRP_Blocks.Definition definition, Logic_BlockMapData conversionDataList)
        {
            string statePath = Assembler.GetTemplatePath("anvil", "anvil_blockstate.json");
            string json = File.ReadAllText(statePath);
            string edited_json = json.Replace("{!}", modelName);
            JavaRP_BlockState state = JsonConvert.DeserializeObject<JavaRP_BlockState>(edited_json);

            string up = Assembler.GetBlockTexture(definition.Textures.Up, conversionDataList.DungeonsData);
            string down = Assembler.GetBlockTexture(definition.Textures.Down, conversionDataList.DungeonsData);
            string side = Assembler.GetBlockTexture(definition.Textures.Side, conversionDataList.DungeonsData);

            CreateModel(modelName, Assembler.GetTemplatePath("anvil", "anvil_model.json"), "", down, up);

            foreach (var entry in state.variants) Assembler.AddStateToBlockStates(Assembler.GetBlockName(javaName), entry.Key, entry.Value);
            foreach (var entry in state.multipart) Assembler.AddMultipartToBlockStates(Assembler.GetBlockName(javaName), entry);
        }

        private static void CreateModel(string modelName, string modelPath, string extension, string texture1, string texture2)
        {
            string json2 = File.ReadAllText(modelPath);
            string edited_json2 = json2.Replace("{!1}", texture1).Replace("{!2}", texture2);
            var result = JsonConvert.DeserializeObject<JavaRP_BlockModel>(edited_json2);
            Assembler.AddBlockModel(modelName + extension, result);
        }
    }
}
