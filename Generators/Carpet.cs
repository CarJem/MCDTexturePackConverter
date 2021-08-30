using MCDTexturePackConverter.Classes;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCDTexturePackConverter.Generators
{
    public static class Carpet
    {
        public static void Generate(string javaName, string modelName, DungeonsRP_Blocks.Definition definition, Logic_BlockMapData conversionDataList, bool failSafeMode = false)
        {
            JObject props = new JObject();
            props.Add("model", Assembler.BlockPrefix + modelName);

            string all = Assembler.GetBlockTexture(definition.Textures.All, conversionDataList.DungeonsData);

            JavaRP_BlockModel model = new JavaRP_BlockModel();
            model.parent = "minecraft:block/carpet";
            model.textures.Add("wool", string.Format("minecraft:block/{0}", all));

            Assembler.AddBlockModel(modelName, model);
            Assembler.AddStateToBlockStates(Assembler.GetBlockName(javaName), "", props);
        }
    }
}
