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
    public static class IronFence
    {
        public static void Generate(string javaName, string modelName, DungeonsRP_Blocks.Definition definition, Logic_BlockMapData conversionDataList)
        {
            string statePath = Assembler.GetTemplatePath("iron_fence", "iron_fence_blockstate.json");
            string json = File.ReadAllText(statePath);
            string edited_json = json.Replace("{!}", modelName);
            JavaRP_BlockState state = JsonConvert.DeserializeObject<JavaRP_BlockState>(edited_json);

            string up = Assembler.GetBlockTexture(definition.Textures.Up, conversionDataList.DungeonsData);

            CreateModel(modelName, Assembler.GetTemplatePath("iron_fence", "iron_fence_cap_alt_model.json"), "_cap_alt", up);
            CreateModel(modelName, Assembler.GetTemplatePath("iron_fence", "iron_fence_cap_model.json"), "_cap", up);
            CreateModel(modelName, Assembler.GetTemplatePath("iron_fence", "iron_fence_post_ends_model.json"), "_post_ends", up);
            CreateModel(modelName, Assembler.GetTemplatePath("iron_fence", "iron_fence_post_model.json"), "_post", up);
            CreateModel(modelName, Assembler.GetTemplatePath("iron_fence", "iron_fence_side_alt_model.json"), "_side_alt", up);
            CreateModel(modelName, Assembler.GetTemplatePath("iron_fence", "iron_fence_side_model.json"), "_side", up);

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
