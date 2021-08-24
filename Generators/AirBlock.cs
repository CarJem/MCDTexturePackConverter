using MCDTexturePackConverter.Classes;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCDTexturePackConverter.Generators
{
    public static class AirBlock
    {
        public static void Generate(string javaName, string dungeonsName, DungeonsRP_Blocks.Definition definition, BlockMapData conversionDataList)
        {
            JavaRP_BlockState state = new JavaRP_BlockState();
            JObject props = new JObject();
            string shortenedName = javaName.Remove("minecraft:");
            props.Add("model", ModelAssembler.BlockPrefix + shortenedName);
            state.variants.Add("", props);

            JavaRP_BlockModel model = new JavaRP_BlockModel();

            ModelAssembler.BlockStates.Add(shortenedName, state);
            ModelAssembler.BlockModels.Add(shortenedName, model);
        }
    }
}
