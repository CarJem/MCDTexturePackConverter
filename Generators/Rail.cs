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
    public static class Rail
    {
        public static void Generate(string javaName, string modelName, DungeonsRP_Blocks.Definition definition, Logic_BlockMapData conversionDataList)
        {
            string statePath = Assembler.GetTemplatePath("rail", "rail_blockstate.json");
            string json = File.ReadAllText(statePath);
            string edited_json = json.Replace("{!}", modelName);
            JavaRP_BlockState state = JsonConvert.DeserializeObject<JavaRP_BlockState>(edited_json);

            string side = Assembler.GetBlockTexture(definition.Textures.Side, conversionDataList.DungeonsData);
            string down = Assembler.GetBlockTexture(definition.Textures.Down, conversionDataList.DungeonsData);
            string up = Assembler.GetBlockTexture(definition.Textures.Up, conversionDataList.DungeonsData);

            CreateModel(modelName, Assembler.GetTemplatePath("rail", "rail_model.json"), "", down);
            CreateModel(modelName, Assembler.GetTemplatePath("rail", "rail_corner_model.json"), "_corner", up);
            CreateModel(modelName, Assembler.GetTemplatePath("rail", "rail_raised_ne_model.json"), "_raised_ne", down);
            CreateModel(modelName, Assembler.GetTemplatePath("rail", "rail_raised_sw_model.json"), "_raised_sw", down);

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
