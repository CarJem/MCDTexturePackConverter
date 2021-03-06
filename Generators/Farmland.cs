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
    public static class Farmland
    {
        public static void Generate(string javaName, string modelName, DungeonsRP_Blocks.Definition definition, Logic_BlockMapData conversionDataList, bool failSafeMode = false)
        {
            JObject props = new JObject();
            props.Add("model", Assembler.BlockPrefix + modelName);

            string top = Assembler.GetBlockTexture(definition.Textures.Up, conversionDataList.DungeonsData);
            string bottom = Assembler.GetBlockTexture(definition.Textures.Down, conversionDataList.DungeonsData);
            string side = Assembler.GetBlockTexture(definition.Textures.Side, conversionDataList.DungeonsData);

            CreateModel(modelName, Assembler.GetTemplatePath("dirt_path", "dirt_path_model.json"), "", top, bottom, side);
            Assembler.AddStateToBlockStates(Assembler.GetBlockName(javaName), conversionDataList.PropertiesString, props);
        }

        private static void CreateModel(string modelName, string modelPath, string extension, string top, string bottom, string side)
        {
            string json2 = File.ReadAllText(modelPath);
            string edited_json2 = json2.Replace("{!1}", bottom).Replace("{!2}", top).Replace("{!3}", side);
            var result = JsonConvert.DeserializeObject<JavaRP_BlockModel>(edited_json2);
            Assembler.AddBlockModel(modelName + extension, result);
        }
    }
}
