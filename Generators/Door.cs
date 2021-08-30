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
    public static class Door
    {
        public static void Generate(string javaName, string modelName, DungeonsRP_Blocks.Definition definition, Logic_BlockMapData conversionDataList)
        {
            string statePath = Assembler.GetTemplatePath("door", "door_blockstate.json");
            string json = File.ReadAllText(statePath);
            string edited_json = json.Replace("{!}", modelName);
            JavaRP_BlockState state = JsonConvert.DeserializeObject<JavaRP_BlockState>(edited_json);

            string up = Assembler.GetBlockTexture(definition.Textures.Side, conversionDataList.DungeonsData);
            string down = Assembler.GetBlockTexture(definition.Textures.Down, conversionDataList.DungeonsData);


            CreateModel(modelName, Assembler.GetTemplatePath("door", "door_bottom_hinge_model.json"), "_bottom_hinge", up, down);
            CreateModel(modelName, Assembler.GetTemplatePath("door", "door_bottom_model.json"), "_bottom", up, down);
            CreateModel(modelName, Assembler.GetTemplatePath("door", "door_top_hinge_model.json"), "_top_hinge", up, down);
            CreateModel(modelName, Assembler.GetTemplatePath("door", "door_top_model.json"), "_top", up, down);

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
