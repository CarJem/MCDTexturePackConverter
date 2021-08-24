﻿using MCDTexturePackConverter.Classes;
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
        public static void Generate(string javaName, string dungeonsName, DungeonsRP_Blocks.Definition definition, BlockMapData conversionDataList)
        {
            string shortenedName = javaName.Remove("minecraft:");

            string statePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Templates", "wall_blockstate.json");
            string json = File.ReadAllText(statePath);
            string edited_json = json.Replace("{!}", shortenedName);
            JavaRP_BlockState state = JsonConvert.DeserializeObject<JavaRP_BlockState>(edited_json);

            string all = ModelGenerator.ReducePath(StoredResources.BlockTextureDataToBlockPath(definition.Textures.All, conversionDataList.DungeonsData));

            string modelPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Templates", "wall_post_model.json");
            string json2 = File.ReadAllText(modelPath);
            string edited_json2 = json2.Replace("{!}", all);
            JavaRP_BlockModel model = JsonConvert.DeserializeObject<JavaRP_BlockModel>(edited_json2);

            string modelPath2 = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Templates", "wall_side_model.json");
            string json3 = File.ReadAllText(modelPath2);
            string edited_json3 = json3.Replace("{!}", all);
            JavaRP_BlockModel model2 = JsonConvert.DeserializeObject<JavaRP_BlockModel>(edited_json3);

            string modelPath3 = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Templates", "wall_side_tall_model.json");
            string json4 = File.ReadAllText(modelPath3);
            string edited_json4 = json4.Replace("{!}", all);
            JavaRP_BlockModel model3 = JsonConvert.DeserializeObject<JavaRP_BlockModel>(edited_json4);

            ModelAssembler.BlockStates.Add(shortenedName, state);

            ModelAssembler.BlockModels.Add(shortenedName + "_post", model);
            ModelAssembler.BlockModels.Add(shortenedName + "_side", model2);
            ModelAssembler.BlockModels.Add(shortenedName + "_side_tall", model3);
        }
    }
}
