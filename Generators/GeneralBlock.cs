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
        public static void Generate(string javaName, string dungeonsName, DungeonsRP_Blocks.Definition definition, BlockMapData conversionDataList, bool failSafeMode = false, bool farmlandMode = false)
        {
            JavaRP_BlockState state = new JavaRP_BlockState();
            JObject props = new JObject();
            string shortenedName = javaName.Remove("minecraft:");
            props.Add("model", ModelAssembler.BlockPrefix + shortenedName);
            state.variants.Add("", props);

            if (definition.Textures != null)
            {
                if (definition.Textures.Up != null && definition.Textures.Down != null && definition.Textures.Side != null)
                {
                    GenerateBottomTop(javaName, dungeonsName, definition, conversionDataList, failSafeMode);
                }
                else
                {
                    GenerateAllSided(javaName, dungeonsName, definition, conversionDataList, failSafeMode);
                }
            }
            else GenerateAllSided(javaName, dungeonsName, definition, conversionDataList, failSafeMode);

            ModelAssembler.BlockStates.Add(shortenedName, state);
        }

        public static void GenerateShort(string javaName, string dungeonsName, DungeonsRP_Blocks.Definition definition, BlockMapData conversionDataList, bool failSafeMode = false)
        {
            JavaRP_BlockState state = new JavaRP_BlockState();
            JObject props = new JObject();
            string shortenedName = javaName.Remove("minecraft:");
            props.Add("model", ModelAssembler.BlockPrefix + shortenedName);
            state.variants.Add("", props);

            string top = ModelGenerator.ReducePath(StoredResources.BlockTextureDataToBlockPath(definition.Textures.Up, conversionDataList.DungeonsData));
            string bottom = ModelGenerator.ReducePath(StoredResources.BlockTextureDataToBlockPath(definition.Textures.Down, conversionDataList.DungeonsData));
            string side = ModelGenerator.ReducePath(StoredResources.BlockTextureDataToBlockPath(definition.Textures.Side, conversionDataList.DungeonsData));


            JavaRP_BlockModel model = new JavaRP_BlockModel();
            model.parent = "minecraft:block/template_farmland";
            model.textures.Add("top", string.Format("minecraft:block/{0}", top));
            model.textures.Add("bottom", string.Format("minecraft:block/{0}", bottom));
            model.textures.Add("side", string.Format("minecraft:block/{0}", side));

            ModelAssembler.BlockModels.Add(shortenedName, model);
            ModelAssembler.BlockStates.Add(shortenedName, state);
        }

        private static void GenerateBottomTop(string javaName, string dungeonsName, DungeonsRP_Blocks.Definition definition, BlockMapData conversionDataList, bool failSafeMode = false)
        {
            string shortenedName = javaName.Remove("minecraft:");

            string top = ModelGenerator.ReducePath(StoredResources.BlockTextureDataToBlockPath(definition.Textures.Up, conversionDataList.DungeonsData));
            string bottom = ModelGenerator.ReducePath(StoredResources.BlockTextureDataToBlockPath(definition.Textures.Down, conversionDataList.DungeonsData));
            string side = ModelGenerator.ReducePath(StoredResources.BlockTextureDataToBlockPath(definition.Textures.Side, conversionDataList.DungeonsData));


            JavaRP_BlockModel model = new JavaRP_BlockModel();
            model.parent = "minecraft:block/cube_bottom_top";
            model.textures.Add("top", string.Format("minecraft:block/{0}", top));
            model.textures.Add("bottom", string.Format("minecraft:block/{0}", bottom));
            model.textures.Add("side", string.Format("minecraft:block/{0}", side));

            ModelAssembler.BlockModels.Add(shortenedName, model);
        }

        private static void GenerateAllSided(string javaName, string dungeonsName, DungeonsRP_Blocks.Definition definition, BlockMapData conversionDataList, bool failSafeMode = false)
        {
            string shortenedName = javaName.Remove("minecraft:");
            string texture_path = (definition.Textures != null ? definition.Textures.All : null);

            if (failSafeMode && texture_path == null)
            {
                if (definition.Textures.Down != null) texture_path = definition.Textures.Down;
                else if (definition.Textures.Up != null) texture_path = definition.Textures.Up;
                else if (definition.Textures.Side != null) texture_path = definition.Textures.Side;
                else if (definition.Textures.North != null) texture_path = definition.Textures.North;
                else if (definition.Textures.East != null) texture_path = definition.Textures.South;
                else if (definition.Textures.South != null) texture_path = definition.Textures.East;
                else if (definition.Textures.West != null) texture_path = definition.Textures.West;
            }

            string texture = ModelGenerator.ReducePath(StoredResources.BlockTextureDataToBlockPath(texture_path, conversionDataList.DungeonsData));

            JavaRP_BlockModel model = new JavaRP_BlockModel();
            model.parent = "minecraft:block/cube_all";
            model.textures.Add("all", string.Format("minecraft:block/{0}", texture));

            ModelAssembler.BlockModels.Add(shortenedName, model);
        }






    }
}
