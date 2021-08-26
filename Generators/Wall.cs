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
    public static class Wall
    {
        public static void Generate(string javaName, string modelName, DungeonsRP_Blocks.Definition definition, Logic_BlockMapData conversionDataList)
        {
            string statePath = Assembler.GetTemplatePath("wall", "wall_blockstate.json");
            string json = File.ReadAllText(statePath);
            string edited_json = json.Replace("{!}", modelName);
            JavaRP_BlockState state = JsonConvert.DeserializeObject<JavaRP_BlockState>(edited_json);

            string all = Assembler.GetBlockTexture(definition.Textures.All, conversionDataList.DungeonsData);

            string modelPath = Assembler.GetTemplatePath("wall", "wall_post_model.json");
            string json2 = File.ReadAllText(modelPath);
            string edited_json2 = json2.Replace("{!}", all);
            JavaRP_BlockModel model = JsonConvert.DeserializeObject<JavaRP_BlockModel>(edited_json2);

            string modelPath2 = Assembler.GetTemplatePath("wall", "wall_side_model.json");
            string json3 = File.ReadAllText(modelPath2);
            string edited_json3 = json3.Replace("{!}", all);
            JavaRP_BlockModel model2 = JsonConvert.DeserializeObject<JavaRP_BlockModel>(edited_json3);

            string modelPath3 = Assembler.GetTemplatePath("wall", "wall_side_tall_model.json");
            string json4 = File.ReadAllText(modelPath3);
            string edited_json4 = json4.Replace("{!}", all);
            JavaRP_BlockModel model3 = JsonConvert.DeserializeObject<JavaRP_BlockModel>(edited_json4);

            foreach (var entry in state.variants) Assembler.AddStateToBlockStates(Assembler.GetBlockName(javaName), entry.Key, entry.Value);
            foreach (var entry in state.multipart) Assembler.AddMultipartToBlockStates(Assembler.GetBlockName(javaName), entry);


            Assembler.AddBlockModel(modelName + "_post", model);
            Assembler.AddBlockModel(modelName + "_side", model2);
            Assembler.AddBlockModel(modelName + "_side_tall", model3);
        }
    }
}
