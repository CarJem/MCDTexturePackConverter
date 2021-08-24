using MCDTexturePackConverter.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCDTexturePackConverter.Generators
{
    public static class ModelGenerator
    {
        
        public static void Generate(string javaName, string dungeonsName,  DungeonsRP_Blocks.Definition definition, List<BlockMapData> conversionDataList)
        {
            string blockShape = definition.Blockshape;

            switch (javaName)
            {
                case "minecraft:air":
                    AirBlock.Generate(javaName, dungeonsName, definition, conversionDataList.FirstOrDefault());
                    return;
                case "minecraft:farmland":
                    GeneralBlock.GenerateShort(javaName, dungeonsName, definition, conversionDataList.FirstOrDefault());
                    return;
            }

            switch (dungeonsName)
            {
                case "double_stone_slab":
                    SlabBlock.Generate(javaName, dungeonsName, definition, conversionDataList.FirstOrDefault());
                    return;
                case "double_wooden_slab":
                    SlabBlock.Generate(javaName, dungeonsName, definition, conversionDataList.FirstOrDefault());
                    return;
                case "double_stonefloor_slab":
                    SlabBlock.Generate(javaName, dungeonsName, definition, conversionDataList.FirstOrDefault());
                    return;
                case "grass_path":
                    GeneralBlock.GenerateShort(javaName, dungeonsName, definition, conversionDataList.FirstOrDefault());
                    return;
            }


            switch (blockShape)
            {
                case "block_half":
                    SlabBlock.Generate(javaName, dungeonsName, definition, conversionDataList.FirstOrDefault());
                    break;
                case "stairs":
                    StairBlock.Generate(javaName, dungeonsName, definition, conversionDataList.FirstOrDefault());
                    break;
                case "wall":
                    Wall.Generate(javaName, dungeonsName, definition, conversionDataList.FirstOrDefault());
                    break;
                case "fence":
                    Fence.Generate(javaName, dungeonsName, definition, conversionDataList.FirstOrDefault());
                    break;
                case "tree":
                    TreeBlock.Generate(javaName, dungeonsName, definition, conversionDataList.FirstOrDefault());
                    break;
                case "double_plant_poly":
                    CrossBlock.GeneratePlant(javaName, dungeonsName, definition, conversionDataList.FirstOrDefault());
                    break;
                case "cross_texture":
                    CrossBlock.Generate(javaName, dungeonsName, definition, conversionDataList.FirstOrDefault());
                    break;
                default:
                    GeneralBlock.Generate(javaName, dungeonsName, definition, conversionDataList.FirstOrDefault(), true);
                    break;
            }

        }

        public static string ReducePath(string path)
        {
            if (path == null) return path;
            var filename = path.Split('/').Last();
            string extension = System.IO.Path.GetExtension(filename);
            string result = filename.Substring(0, filename.Length - extension.Length);
            return result;
        }
    }
}
