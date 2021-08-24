using MCDTexturePackConverter.Classes;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCDTexturePackConverter.Generators
{
    public static class TestCode
    {
        public static void IrritableBlockState(string javaName, string dungeonsName, List<BlockMapData> conversionDataList)
        {
            foreach (var conversionData in conversionDataList)
            {
                //Check if the Block has textures in the resource pack
                if (StoredResources.Blocks.blocks.ContainsKey(dungeonsName))
                {
                    string variant = conversionData.PropertiesString;

                    //Check if we have (and add if we don't) a block state already for this block
                    if (!ModelAssembler.BlockStates.ContainsKey(javaName)) ModelAssembler.BlockStates.Add(javaName, new JavaRP_BlockState());

                    //Check if we have this specific variant already, if not proceed
                    if (!ModelAssembler.BlockStates[javaName].variants.ContainsKey(variant))
                    {
                        ModelAssembler.BlockStates[javaName].variants.Add(variant, GetBlockStateProps(javaName, dungeonsName, conversionData.Properties));
                    }
                }
            }
        }

        private static JObject GetBlockStateProps(string javaName, string dungeonsName, Dictionary<string, string> Properties)
        {
            JObject props = new JObject();
            if (StoredResources.Blocks.blocks.ContainsKey(dungeonsName))
            {
                var definition = StoredResources.Blocks.blocks[dungeonsName];
                string blockShape = definition.Blockshape;
                string shortenedName = javaName.Remove(javaName.IndexOf("minecraft:"), javaName.Length);

                switch (blockShape)
                {
                    default:
                        //Generic Block
                        props.Add("model", ModelAssembler.BlockPrefix + shortenedName);
                        break;
                }
            }
            return props;
        }
    }
}
