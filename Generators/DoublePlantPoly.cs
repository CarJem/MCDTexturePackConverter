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
    public static class DoublePlantPoly
    {
        public static void Generate(string javaName, string modelName, DungeonsRP_Blocks.Definition definition, Logic_BlockMapData conversionDataList)
        {
            string statePath = Assembler.GetTemplatePath("doubleplant", "doubeplant_blockstate.json");
            string json = File.ReadAllText(statePath);
            string edited_json = json.Replace("{!1}", modelName).Replace("{!2}", modelName);

            JavaRP_BlockState state = JsonConvert.DeserializeObject<JavaRP_BlockState>(edited_json);


            string up = Assembler.GetBlockTexture(definition.Textures.Up, conversionDataList.DungeonsData);
            string down = Assembler.GetBlockTexture(definition.Textures.Down, conversionDataList.DungeonsData);

            string side_front = Assembler.GetBlockTexture(definition.Textures.Side, conversionDataList.DungeonsData);
            string side_back = Assembler.GetBlockTexture(definition.Textures.Side, conversionDataList.DungeonsData+1);

            string modelPath1 = Assembler.GetTemplatePath("doubleplant", "doubleplant_general_model.json");
            JavaRP_BlockModel model1 = CreateModel(modelPath1, down);

            string modelPath2 = Assembler.GetTemplatePath("doubleplant", "doubleplant_general_model.json");
            JavaRP_BlockModel model2 = CreateModel(modelPath2, up);

            /*
            if (side_front != null && side_back != null)
            {
                modelPath2 = Assembler.GetTemplatePath("doubleplant", "doubeplant_sunflower_model.json");
                model2 = CreateModel(modelPath2, up, side_front, side_back);
            }
            else
            {
                modelPath2 = Assembler.GetTemplatePath("doubleplant", "doubleplant_general_model.json");
                model2 = CreateModel(modelPath2, up);
            }
            */






            foreach (var entry in state.variants) Assembler.AddStateToBlockStates(Assembler.GetBlockName(javaName), entry.Key, entry.Value);



            Assembler.AddBlockModel(modelName + "_bottom", model1);
            Assembler.AddBlockModel(modelName + "_top", model2);
        }


        private static JavaRP_BlockModel CreateModel(string modelPath, string texture, string texture2 = "", string texture3 = "")
        {
            string json2 = File.ReadAllText(modelPath);
            string edited_json2 = json2.Replace("{!1}", texture).Replace("{!2}", texture2).Replace("{!3}", texture3);
            return JsonConvert.DeserializeObject<JavaRP_BlockModel>(edited_json2);
        }
    }
}
