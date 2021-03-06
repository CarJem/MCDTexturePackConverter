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
    public static class TreeBlock
    {
        public static void Generate(string javaName, string modelName, DungeonsRP_Blocks.Definition definition, Logic_BlockMapData conversionDataList)
        {
            string statePath = Assembler.GetTemplatePath("tree", "log_blockstate.json");
            string json = File.ReadAllText(statePath);
            string edited_json = json.Replace("{!}", modelName);
            JavaRP_BlockState state = JsonConvert.DeserializeObject<JavaRP_BlockState>(edited_json);

            string end = Assembler.GetBlockTexture(definition.Textures.Up, conversionDataList.DungeonsData);
            string side = Assembler.GetBlockTexture(definition.Textures.Side, conversionDataList.DungeonsData);

            string modelPath = Assembler.GetTemplatePath("tree", "log_model.json");
            string json2 = File.ReadAllText(modelPath);
            string edited_json2 = json2.Replace("{!}", side).Replace("{!2}", end);
            JavaRP_BlockModel model = JsonConvert.DeserializeObject<JavaRP_BlockModel>(edited_json2);

            string modelPath2 = Assembler.GetTemplatePath("tree", "log_horizontal_model.json");
            string json3 = File.ReadAllText(modelPath2);
            string edited_json3 = json3.Replace("{!}", side).Replace("{!2}", end);
            JavaRP_BlockModel model2 = JsonConvert.DeserializeObject<JavaRP_BlockModel>(edited_json3);

            foreach (var entry in state.variants) Assembler.AddStateToBlockStates(Assembler.GetBlockName(javaName), entry.Key, entry.Value);
            foreach (var entry in state.multipart) Assembler.AddMultipartToBlockStates(Assembler.GetBlockName(javaName), entry);



            Assembler.AddBlockModel(modelName, model);
            Assembler.AddBlockModel(modelName + "_horizontal", model2);
        }
    }
}
