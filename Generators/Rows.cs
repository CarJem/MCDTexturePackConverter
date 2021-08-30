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
    public static class Rows
    {
        public static void Generate(string javaName, string modelName, DungeonsRP_Blocks.Definition definition, Logic_BlockMapData conversionDataList)
        {
            string statePath = Assembler.GetTemplatePath("rows", "rows_blockstate.json");
            string json = File.ReadAllText(statePath);
            string edited_json = json.Replace("{!}", modelName);
            JavaRP_BlockState state = JsonConvert.DeserializeObject<JavaRP_BlockState>(edited_json);

            string stage0 = Assembler.GetBlockTexture(definition.Textures.All, conversionDataList.DungeonsData+0);
            string stage1 = Assembler.GetBlockTexture(definition.Textures.All, conversionDataList.DungeonsData+1);
            string stage2 = Assembler.GetBlockTexture(definition.Textures.All, conversionDataList.DungeonsData+2);
            string stage3 = Assembler.GetBlockTexture(definition.Textures.All, conversionDataList.DungeonsData+3);
            string stage4 = Assembler.GetBlockTexture(definition.Textures.All, conversionDataList.DungeonsData+4);
            string stage5 = Assembler.GetBlockTexture(definition.Textures.All, conversionDataList.DungeonsData+5);
            string stage6 = Assembler.GetBlockTexture(definition.Textures.All, conversionDataList.DungeonsData+6);
            string stage7 = Assembler.GetBlockTexture(definition.Textures.All, conversionDataList.DungeonsData+7);

            CreateModel(modelName, Assembler.GetTemplatePath("rows", "rows_stage_model.json"), "_stage0", stage0);
            CreateModel(modelName, Assembler.GetTemplatePath("rows", "rows_stage_model.json"), "_stage1", stage1);
            CreateModel(modelName, Assembler.GetTemplatePath("rows", "rows_stage_model.json"), "_stage2", stage2);
            CreateModel(modelName, Assembler.GetTemplatePath("rows", "rows_stage_model.json"), "_stage3", stage3);
            CreateModel(modelName, Assembler.GetTemplatePath("rows", "rows_stage_model.json"), "_stage4", stage4);
            CreateModel(modelName, Assembler.GetTemplatePath("rows", "rows_stage_model.json"), "_stage5", stage5);
            CreateModel(modelName, Assembler.GetTemplatePath("rows", "rows_stage_model.json"), "_stage6", stage6);
            CreateModel(modelName, Assembler.GetTemplatePath("rows", "rows_stage_model.json"), "_stage7", stage7);



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
