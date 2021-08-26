using MCDTexturePackConverter.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCDTexturePackConverter.Generators
{
    public static class Generator
    {
        public static Dictionary<string, string> Redirects { get; set; } = new Dictionary<string, string>()
        {
            { "double_stone_slab", "block_half" },
            { "double_wooden_slab", "block_half" },
            { "double_stonefloor_slab", "block_half" },
            { "grass_path", "converter_short" },
            { "farmland", "converter_short" },
            { "iron_trapdoor", "converter_trapdoor" },
            { "trapdoor", "converter_trapdoor" },
            { "carpet", "converter_carpet" }
        };

        //CONVERTING:
        //Rules:
        // - Java Block = Dungeons ID and Data
        // - Can Not Be Occupied Twice (Limitation of Resource Pack Converting Logic)


        public static void Generate(string javaName, string dungeonsName, DungeonsRP_Blocks.Definition definition, Logic_BlockMapData mappings)
        {
            string blockShape = definition.Blockshape;

            if (Redirects.ContainsKey(dungeonsName)) blockShape = Redirects[dungeonsName];

            string shortenedName = javaName.Remove("minecraft:");
            string modelName = shortenedName;
            int retry = 1;

            while (Assembler.BlockModels.ContainsKey(modelName))
            {
                modelName = javaName.Remove("minecraft:") + retry.ToString();
                retry += 1;
            }

            switch (blockShape)
            {
                case "block_half":
                    SlabBlock.Generate(javaName, modelName, definition, mappings);
                    break;
                case "stairs":
                    StairBlock.Generate(javaName, modelName, definition, mappings);
                    break;
                case "wall":
                    Wall.Generate(javaName, modelName, definition, mappings);
                    break;
                case "fence":
                    Fence.Generate(javaName, modelName, definition, mappings);
                    break;
                case "fence_gate":
                    //TODO: Implement
                    break;
                case "tree":
                    TreeBlock.Generate(javaName, modelName, definition, mappings);
                    break;
                case "leaves":
                    LeavesBlock.Generate(javaName, modelName, definition, mappings);
                    break;
                case "double_plant_poly":
                    CrossBlock.GeneratePlant(javaName, modelName, definition, mappings);
                    break;
                case "cross_texture":
                    CrossBlock.Generate(javaName, modelName, definition, mappings);
                    break;
                case "converter_carpet":
                    //TODO: Implement
                    break;
                case "converter_trapdoor":
                    //TODO: Implement
                    break;
                case "converter_short":
                    Farmland.Generate(javaName, modelName, definition, mappings);
                    break;
                case "ladder":
                    //TODO: Implement
                    break;
                case "rail":
                    //TODO: Implement
                    break;
                case "door":
                    //TODO: Implement
                    break;
                case "rows":
                    //TODO: Implement
                    break;
                case "red_dust":
                    //TODO: Implement
                    break;
                case "chest":
                    //TODO: Implement
                    break;
                case "invisible":
                    AirBlock.Generate(javaName, modelName, definition, mappings);
                    break;
                case "fire":
                    //TODO: Implement
                    break;
                case "void":
                    //TODO: Implement
                    break;
                case "hopper":
                    //TODO: Implement
                    break;
                case "comparator":
                    //TODO: Implement
                    break;
                case "anvil":
                    //TODO: Implement
                    break;
                case "tripwire":
                    //TODO: Implement
                    break;
                case "tripwire_hook":
                    //TODO: Implement
                    break;
                case "portal_frame":
                    //TODO: Implement
                    break;
                case "cocoa":
                    //TODO: Implement
                    break;
                case "cauldron":
                    //TODO: Implement
                    break;
                case "brewing_stand":
                    //TODO: Implement
                    break;
                case "lilypad":
                    //TODO: Implement
                    break;
                case "vine":
                    //TODO: Implement
                    break;
                case "stem":
                    //TODO: Implement
                    break;
                case "iron_fence":
                    //TODO: Implement
                    break;
                case "repeater":
                    //TODO: Implement
                    break;
                case "cactus":
                    //TODO: Implement
                    break;
                case "piston":
                    //TODO: Implement
                    break;
                case "tallgrass":
                    //TODO: Implement
                    break;
                case "bed":
                    //TODO: Implement
                    break;
                case "water":
                    //TODO: Implement
                    break;
                case "lever":
                    //TODO: Implement
                    break;
                case "torch":
                    //TODO: Implement
                    break;
                default:
                    GeneralBlock.Generate(javaName, modelName, definition, mappings, true);
                    break;
            }
        }
    }
}
