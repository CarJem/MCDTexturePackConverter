using MCDTexturePackConverter.Classes;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCDTexturePackConverter.Generators
{
    public static class CrossBlock
    {
        public static void Generate(string javaName, string modelName, DungeonsRP_Blocks.Definition definition, Logic_BlockMapData conversionDataList)
        {
            JObject props = new JObject();
            props.Add("model", Assembler.BlockPrefix + modelName);

            string texture_path = definition.Textures.All;
            string texture = Assembler.GetBlockTexture(texture_path, conversionDataList.DungeonsData);

            JavaRP_BlockModel model = new JavaRP_BlockModel();
            model.parent = "minecraft:block/cross";
            model.textures.Add("cross", string.Format("minecraft:block/{0}", texture));

            Assembler.AddStateToBlockStates(Assembler.GetBlockName(javaName), "", props);
            Assembler.AddBlockModel(modelName, model);
        }
    }
}
