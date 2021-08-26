using MCDTexturePackConverter.Classes;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
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


            JavaRP_BlockModel model = new JavaRP_BlockModel();
            model.parent = "minecraft:block/template_farmland";
            model.textures.Add("top", string.Format("minecraft:block/{0}", top));
            model.textures.Add("bottom", string.Format("minecraft:block/{0}", bottom));
            model.textures.Add("side", string.Format("minecraft:block/{0}", side));

            Assembler.AddBlockModel(modelName, model);
            Assembler.AddStateToBlockStates(Assembler.GetBlockName(javaName), "", props);
        }
    }
}
