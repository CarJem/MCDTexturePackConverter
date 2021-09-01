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
        public static Dictionary<string, string> DungeonsRedirects { get; set; } = new Dictionary<string, string>()
        {
            { "double_stone_slab", "block_half" },
            { "double_wooden_slab", "block_half" },
            { "double_stonefloor_slab", "block_half" },
            { "grass_path", "converter_short" },
            { "farmland", "converter_short" },
            { "iron_trapdoor", "converter_trapdoor" },
            { "trapdoor", "converter_trapdoor" },
            { "carpet", "converter_carpet" },
            { "snow_layer", "converter_snow_layer" },
            { "quartz_block", "converter_needs_fix_log" }
        };

        //CONVERTING:
        //Rules:
        // - Java Block = Dungeons ID and Data
        // - Can Not Be Occupied Twice (Limitation of Resource Pack Converting Logic)


        public static void Generate(string javaName, string dungeonsName, DungeonsRP_Blocks.Definition definition, Logic_BlockMapData mappings)
        {
            string blockShape = definition.Blockshape;

            if (DungeonsRedirects.ContainsKey(dungeonsName)) blockShape = DungeonsRedirects[dungeonsName];

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
                    FenceGate.Generate(javaName, modelName, definition, mappings);
                    break;
                case "converter_needs_fix_log":
                    if (javaName == "minecraft:quartz_pillar") TreeBlock.Generate(javaName, modelName, definition, mappings);
                    else GeneralBlock.Generate(javaName, modelName, definition, mappings, true);
                    break;
                case "tree":
                    TreeBlock.Generate(javaName, modelName, definition, mappings);
                    break;
                case "leaves":
                    LeavesBlock.Generate(javaName, modelName, definition, mappings);
                    break;
                case "double_plant_poly":
                    DoublePlantPoly.Generate(javaName, modelName, definition, mappings);
                    break;
                case "cross_texture":
                    CrossBlock.Generate(javaName, modelName, definition, mappings);
                    break;
                case "converter_carpet":
                    Carpet.Generate(javaName, modelName, definition, mappings);
                    break;
                case "converter_trapdoor":
                    Trapdoor.Generate(javaName, modelName, definition, mappings);
                    break;
                case "converter_short":
                    Farmland.Generate(javaName, modelName, definition, mappings);
                    break;
                case "ladder":
                    Ladder.Generate(javaName, modelName, definition, mappings);
                    break;
                case "rail":
                    Rail.Generate(javaName, modelName, definition, mappings);
                    break;
                case "door":
                    Door.Generate(javaName, modelName, definition, mappings);
                    break;
                case "rows":
                    Rows.Generate(javaName, modelName, definition, mappings);
                    break;
                case "red_dust":
                    RedDust.Generate(javaName, modelName, definition, mappings);
                    break;
                case "chest":
                    GeneralBlock.Generate(javaName, modelName, definition, mappings, true);
                    break;
                case "invisible":
                    AirBlock.Generate(javaName, modelName, definition, mappings);
                    break;
                case "fire":
                    Fire.Generate(javaName, modelName, definition, mappings);
                    break;
                case "anvil":
                    Anvil.Generate(javaName, modelName, definition, mappings);
                    break;
                case "iron_fence":
                    IronFence.Generate(javaName, modelName, definition, mappings);
                    break;
                case "cross_texture_poly":
                    CrossBlock.Generate(javaName, modelName, definition, mappings);
                    break;
                case "tallgrass":
                    CrossBlock.Generate(javaName, modelName, definition, mappings);
                    break;
                case "slime_block":
                    GeneralBlock.Generate(javaName, modelName, definition, mappings, true);
                    break;
                case "converter_snow_layer":
                    SnowLayer.Generate(javaName, modelName, definition, mappings);
                    break;
                case "cactus":
                    Cactus.Generate(javaName, modelName, definition, mappings);
                    break;
                case "void":
                    AirBlock.Generate(javaName, modelName, definition, mappings);
                    break;
                case "cauldron":
                    Cauldron.Generate(javaName, modelName, definition, mappings);
                    break;
                case "brewing_stand":
                    BrewingStand.Generate(javaName, modelName, definition, mappings);
                    break;
                case "lilypad":
                    Lilypad.Generate(javaName, modelName, definition, mappings);
                    break;
                case "vine":
                    Vine.Generate(javaName, modelName, definition, mappings);
                    break;
                case "lever":
                    Lever.Generate(javaName, modelName, definition, mappings);
                    break;
                case "torch":
                    Torch.Generate(javaName, modelName, definition, mappings);
                    break;
                case "cocoa":
                    Cocoa.Generate(javaName, modelName, definition, mappings);
                    break;
                case "bed":
                    //TODO: Implement
                    break;
                case "water":
                    //TODO: Implement
                    break;
                case "stem":
                    //TODO: Implement (Not a Priority)
                    //Stem.Generate(javaName, modelName, definition, mappings);
                    break;
                case "hopper":
                    //TODO: Implement (not a priority)
                    break;
                case "comparator":
                    //TODO: Implement (not a priority)
                    break;
                case "tripwire":
                    //TODO: Implement (not a priority)
                    break;
                case "tripwire_hook":
                    //TODO: Implement (not a priority)
                    break;
                case "portal_frame":
                    //TODO: Implement (not a priority)
                    break;
                case "repeater":
                    //TODO: Implement (not a priority)
                    break;
                case "piston":
                    //TODO: Implement (not a priority)
                    break;
                default:
                    GeneralBlock.Generate(javaName, modelName, definition, mappings, true);
                    break;
            }
        }
    }
}
