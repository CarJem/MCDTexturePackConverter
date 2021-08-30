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
    public static class FenceGate
    {
        public static void Generate(string javaName, string modelName, DungeonsRP_Blocks.Definition definition, Logic_BlockMapData conversionDataList)
        {
            string statePath = Assembler.GetTemplatePath("fence_gate", "fence_gate_blockstate.json");
            string json = File.ReadAllText(statePath);
            string edited_json = json.Replace("{!}", modelName);
            JavaRP_BlockState state = JsonConvert.DeserializeObject<JavaRP_BlockState>(edited_json);

            string all = Assembler.GetBlockTexture(definition.Textures.All, conversionDataList.DungeonsData);

            CreateModel(modelName, Assembler.GetTemplatePath("fence_gate", "fence_gate_model.json"), "", all);
            CreateModel(modelName, Assembler.GetTemplatePath("fence_gate", "fence_gate_open_model.json"), "_open", all);
            CreateModel(modelName, Assembler.GetTemplatePath("fence_gate", "fence_gate_wall_model.json"), "_wall", all);
            CreateModel(modelName, Assembler.GetTemplatePath("fence_gate", "fence_gate_wall_open_model.json"), "_wall_open", all);

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
