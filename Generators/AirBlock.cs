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
        public static void Generate(string javaName, string modelName, DungeonsRP_Blocks.Definition definition, Logic_BlockMapData conversionDataList)
        {
            JObject props = new JObject();
            string shortenedName = javaName.Remove("minecraft:");
            props.Add("model", Assembler.BlockPrefix + shortenedName);

            JavaRP_BlockModel model = new JavaRP_BlockModel();

            Assembler.AddStateToBlockStates(shortenedName, "", props);
            Assembler.AddBlockModel(shortenedName, model);
        }
    }
}
