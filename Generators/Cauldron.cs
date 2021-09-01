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
    public static class Cauldron
    {
        public static void Generate(string javaName, string modelName, DungeonsRP_Blocks.Definition definition, Logic_BlockMapData conversionDataList, bool failSafeMode = false)
        {
            JObject props = new JObject();
            props.Add("model", Assembler.BlockPrefix + modelName);

            string top = Assembler.GetBlockTexture(definition.Textures.Up, conversionDataList.DungeonsData);
            string bottom = Assembler.GetBlockTexture(definition.Textures.Down, conversionDataList.DungeonsData);
            string side = Assembler.GetBlockTexture(definition.Textures.North, conversionDataList.DungeonsData);
            string inside = Assembler.GetBlockTexture(definition.Textures.South, conversionDataList.DungeonsData);


            CreateModel(modelName, Assembler.GetTemplatePath("cauldron", "cauldron_model.json"), "", top, bottom, side, inside);
            Assembler.AddStateToBlockStates(Assembler.GetBlockName(javaName), conversionDataList.PropertiesString, props);
        }

        private static void CreateModel(string modelName, string modelPath, string extension, string top, string bottom, string side, string inside)
        {
            string json2 = File.ReadAllText(modelPath);
            string edited_json2 = json2.Replace("{!1}", top).Replace("{!2}", bottom).Replace("{!3}", side).Replace("{!4}", inside);
            var result = JsonConvert.DeserializeObject<JavaRP_BlockModel>(edited_json2);
            Assembler.AddBlockModel(modelName + extension, result);
        }
    }
}
