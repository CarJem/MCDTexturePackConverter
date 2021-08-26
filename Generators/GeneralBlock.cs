using MCDTexturePackConverter.Classes;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCDTexturePackConverter.Generators
{
    public static class GeneralBlock
    {
        public static void Generate(string javaName, string modelName, DungeonsRP_Blocks.Definition definition, Logic_BlockMapData conversionDataList, bool failSafeMode = false, bool farmlandMode = false)
        {
            if (definition.Textures != null)
            {
                var textures = definition.Textures;
                if (textures.Up != null && textures.Down != null && textures.North != null && textures.South != null && textures.East != null && textures.West != null)
                {
                    GenerateUniqueSided(javaName, modelName, definition, conversionDataList);
                }
                else if (textures.Up != null && textures.Down != null && textures.Side != null)
                {
                    GenerateBottomTop(javaName, modelName, definition, conversionDataList);
                }
                else
                {
                    GenerateAllSided(javaName, modelName, definition, conversionDataList, failSafeMode);
                }
            }
            else GenerateAllSided(javaName, modelName, definition, conversionDataList, failSafeMode);


            JObject props = new JObject();
            props.Add("model", Assembler.BlockPrefix + modelName);

            Assembler.AddStateToBlockStates(Assembler.GetBlockName(javaName), conversionDataList.PropertiesString, props);
        }

        private static void GenerateBottomTop(string javaName, string modelName, DungeonsRP_Blocks.Definition definition, Logic_BlockMapData conversionDataList)
        {
            string top = Assembler.GetBlockTexture(definition.Textures.Up, conversionDataList.DungeonsData);
            string bottom = Assembler.GetBlockTexture(definition.Textures.Down, conversionDataList.DungeonsData);
            string side = Assembler.GetBlockTexture(definition.Textures.Side, conversionDataList.DungeonsData);


            JavaRP_BlockModel model = new JavaRP_BlockModel();
            model.parent = "minecraft:block/cube_bottom_top";
            model.textures.Add("top", string.Format("minecraft:block/{0}", top));
            model.textures.Add("bottom", string.Format("minecraft:block/{0}", bottom));
            model.textures.Add("side", string.Format("minecraft:block/{0}", side));

            Assembler.AddBlockModel(modelName, model);
        }

        private static void GenerateUniqueSided(string javaName, string modelName, DungeonsRP_Blocks.Definition definition, Logic_BlockMapData conversionDataList)
        {
            string down = Assembler.GetBlockTexture(definition.Textures.Down, conversionDataList.DungeonsData);
            string up = Assembler.GetBlockTexture(definition.Textures.Up, conversionDataList.DungeonsData);
            string north = Assembler.GetBlockTexture(definition.Textures.North, conversionDataList.DungeonsData);
            string south = Assembler.GetBlockTexture(definition.Textures.South, conversionDataList.DungeonsData);
            string east = Assembler.GetBlockTexture(definition.Textures.East, conversionDataList.DungeonsData);
            string west = Assembler.GetBlockTexture(definition.Textures.West, conversionDataList.DungeonsData);

            JavaRP_BlockModel model = new JavaRP_BlockModel();
            model.parent = "minecraft:block/cube_all";
            model.textures.Add("down", string.Format("minecraft:block/{0}", down));
            model.textures.Add("up", string.Format("minecraft:block/{0}", up));
            model.textures.Add("north", string.Format("minecraft:block/{0}", north));
            model.textures.Add("south", string.Format("minecraft:block/{0}", south));
            model.textures.Add("east", string.Format("minecraft:block/{0}", east));
            model.textures.Add("west", string.Format("minecraft:block/{0}", west));


            Assembler.AddBlockModel(modelName, model);
        }

        private static void GenerateAllSided(string javaName, string modelName, DungeonsRP_Blocks.Definition definition, Logic_BlockMapData conversionDataList, bool failSafeMode = false)
        {
            string texture_path = (definition.Textures != null ? definition.Textures.All : null);

            if (failSafeMode && texture_path == null)
            {
                if (definition.Textures.Down != null) texture_path = Assembler.GetBlockTexture(definition.Textures.Down, conversionDataList.DungeonsData);
                else if (definition.Textures.Up != null) texture_path = Assembler.GetBlockTexture(definition.Textures.Up, conversionDataList.DungeonsData);
                else if (definition.Textures.Side != null) texture_path = Assembler.GetBlockTexture(definition.Textures.Side, conversionDataList.DungeonsData);
                else if (definition.Textures.North != null) texture_path = Assembler.GetBlockTexture(definition.Textures.North, conversionDataList.DungeonsData);
                else if (definition.Textures.East != null) texture_path = Assembler.GetBlockTexture(definition.Textures.South, conversionDataList.DungeonsData);
                else if (definition.Textures.South != null) texture_path = Assembler.GetBlockTexture(definition.Textures.East, conversionDataList.DungeonsData);
                else if (definition.Textures.West != null) texture_path = Assembler.GetBlockTexture(definition.Textures.West, conversionDataList.DungeonsData);
            }

            string texture = Assembler.GetBlockTexture(texture_path, conversionDataList.DungeonsData);

            JavaRP_BlockModel model = new JavaRP_BlockModel();
            model.parent = "minecraft:block/cube_all";
            model.textures.Add("all", string.Format("minecraft:block/{0}", texture));

            Assembler.AddBlockModel(modelName, model);
        }






    }
}
