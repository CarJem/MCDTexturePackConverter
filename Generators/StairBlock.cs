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
    public static class StairBlock
    {
        public static void Generate(string javaName, string modelName, DungeonsRP_Blocks.Definition definition, Logic_BlockMapData conversionDataList)
        {
            string statePath = Assembler.GetTemplatePath("stairs", "stairs_blockstate.json");
            string json = File.ReadAllText(statePath);
            string edited_json = json.Replace("{!}", modelName);
            JavaRP_BlockState state = JsonConvert.DeserializeObject<JavaRP_BlockState>(edited_json);

            string all = Assembler.GetBlockTexture(definition.Textures.All, conversionDataList.DungeonsData);
            string bottom = Assembler.GetBlockTexture(definition.Textures.Up, conversionDataList.DungeonsData);
            string top = Assembler.GetBlockTexture(definition.Textures.Down, conversionDataList.DungeonsData);
            string side = Assembler.GetBlockTexture(definition.Textures.Side, conversionDataList.DungeonsData);

            bool useAll = bottom == null || top == null || side == null;

            string modelPath1 = Assembler.GetTemplatePath("stairs", "stairs_model.json");
            string modelPath2 = Assembler.GetTemplatePath("stairs", "stairs_inner_model.json");
            string modelPath3 = Assembler.GetTemplatePath("stairs", "stairs_outer_model.json");

            var model1 = CreateModel(modelPath1, (useAll ? all : bottom), (useAll ? all : top), (useAll ? all : side));
            var model2 = CreateModel(modelPath2, (useAll ? all : bottom), (useAll ? all : top), (useAll ? all : side));
            var model3 = CreateModel(modelPath3, (useAll ? all : bottom), (useAll ? all : top), (useAll ? all : side));


            foreach (var entry in state.variants) Assembler.AddStateToBlockStates(Assembler.GetBlockName(javaName), entry.Key, entry.Value);


            Assembler.AddBlockModel(modelName, model1);
            Assembler.AddBlockModel(modelName + "_inner", model2);
            Assembler.AddBlockModel(modelName + "_outer", model3);
        }


        private static JavaRP_BlockModel CreateModel(string modelPath, string bottom, string top, string side)
        {
            string json2 = File.ReadAllText(modelPath);
            string edited_json2 = json2.Replace("{!1}", bottom).Replace("{!2}", top).Replace("{!3}", side);
            return JsonConvert.DeserializeObject<JavaRP_BlockModel>(edited_json2);
        }
    }
}
