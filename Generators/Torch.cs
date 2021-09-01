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
    public static class Torch
    {

        public static void Generate(string javaName, string modelName, DungeonsRP_Blocks.Definition definition, Logic_BlockMapData conversionDataList)
        {
            switch (Assembler.GetBlockName(javaName))
            {
                case "torch":
                    GenerateNormal(javaName, modelName, definition, conversionDataList);
                    break;
                case "wall_torch":
                    GenerateWall(javaName, modelName, definition, conversionDataList);
                    break;
                case "redstone_torch":
                    GenerateNormal(javaName, modelName, definition, conversionDataList);
                    break;
                case "redstone_torch_off":
                    GenerateNormal(javaName, modelName, definition, conversionDataList);
                    break;
                case "redstone_wall_torch":
                    GenerateWall(javaName, modelName, definition, conversionDataList);
                    break;
                case "redstone_wall_torch_off":
                    GenerateWall(javaName, modelName, definition, conversionDataList);
                    break;
            }
        }

        public static void GenerateNormal(string javaName, string modelName, DungeonsRP_Blocks.Definition definition, Logic_BlockMapData conversionDataList)
        {
            string statePath = Assembler.GetTemplatePath("torch", "torch_blockstate.json");
            string json = File.ReadAllText(statePath);
            string edited_json = json.Replace("{!}", modelName);
            JavaRP_BlockState state = JsonConvert.DeserializeObject<JavaRP_BlockState>(edited_json);

            string all = Assembler.GetBlockTexture(definition.Textures.All, conversionDataList.DungeonsData);

            string modelPath = Assembler.GetTemplatePath("torch", "torch_model.json");
            string json2 = File.ReadAllText(modelPath);
            string edited_json2 = json2.Replace("{!}", all);
            JavaRP_BlockModel model = JsonConvert.DeserializeObject<JavaRP_BlockModel>(edited_json2);

            foreach (var entry in state.variants)
            {
                string key = entry.Key;
                if (conversionDataList.Properties.ContainsKey("lit")) key = string.Format("{0}={1},{2}", "lit", conversionDataList.Properties["lit"] , key);
                Assembler.AddStateToBlockStates(Assembler.GetBlockName(javaName), key, entry.Value);
            }

            Assembler.AddBlockModel(modelName, model);
        }

        private static void GenerateWall(string javaName, string modelName, DungeonsRP_Blocks.Definition definition, Logic_BlockMapData conversionDataList)
        {
            string statePath = Assembler.GetTemplatePath("torch", "wall_torch_blockstate.json");
            string json = File.ReadAllText(statePath);
            string edited_json = json.Replace("{!}", modelName);
            JavaRP_BlockState state = JsonConvert.DeserializeObject<JavaRP_BlockState>(edited_json);

            string all = Assembler.GetBlockTexture(definition.Textures.All, conversionDataList.DungeonsData);

            string modelPath = Assembler.GetTemplatePath("torch", "wall_torch_model.json");
            string json2 = File.ReadAllText(modelPath);
            string edited_json2 = json2.Replace("{!}", all);
            JavaRP_BlockModel model = JsonConvert.DeserializeObject<JavaRP_BlockModel>(edited_json2);

            foreach (var entry in state.variants)
            {
                string key = entry.Key;
                if (conversionDataList.Properties.ContainsKey("lit")) key = string.Format("{0}={1},{2}", "lit", conversionDataList.Properties["lit"], key);
                Assembler.AddStateToBlockStates(Assembler.GetBlockName(javaName), key, entry.Value);
            }

            Assembler.AddBlockModel(modelName, model);
        }
    }
}
