using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MCDTexturePackConverter.Classes;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace MCDTexturePackConverter
{
    public static class ModelAssembler
    {
        public static BlockMap_Base BlockMap { get; private set; } = new BlockMap_Base();
        public static BlockNames_Base BlockNames { get; private set; } = new BlockNames_Base();
        public static Dictionary<string, JavaRP_BlockModel> BlockModels { get; set; } = new Dictionary<string, JavaRP_BlockModel>();
        public static Dictionary<string, JavaRP_BlockState> BlockStates { get; set; } = new Dictionary<string, JavaRP_BlockState>();
        public static void Init()
        {
            string mapPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "BlockList.json");
            string namesPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "BlockList_Names.json");

            BlockMap = JsonConvert.DeserializeObject<BlockMap_Base>(File.ReadAllText(mapPath));
            BlockNames = JsonConvert.DeserializeObject<BlockNames_Base>(File.ReadAllText(namesPath));
        }

        //Actual Block Model Steps
        //1. Block State Determines Everything... Based on Java Namespace
        //2. Block State Calls the Model

        //Create Block Model in Java RP from Dungeons Block:
        //  1. - use Java Name for the Converted Dungeons Block
        //  2. - use Dungeons Blockshape to Determine which Premade Java Model to Use (blocks.json)
        //  3. - use Dungeons Textures based on the Block's ID/Mask for the Java Model (blocks.json)
        //      a. - Pick the Right Texture (terrain_texture.json)
        //      b. - Get the Texture Path (resources.json)
        //  4. - Move the Textures over to the Java RP

        public const string BlockPrefix = "minecraft:block/";



        public static void AssembleBlock(string key, IBlockMap_Value value)
        {
            List<BlockMapData> blocks = BlockMapData.Convert(value);
            string javaName = key;
            //Get Dungeons Name for the First Block (all of other blocks should be the same data ID)
            string dungeonsName = BlockNames.GetName(blocks.FirstOrDefault().DungeonsID);
            if (StoredResources.Blocks.blocks.ContainsKey(dungeonsName))
            {
                var definition = StoredResources.Blocks.blocks[dungeonsName];
                Generators.ModelGenerator.Generate(javaName, dungeonsName, definition, blocks);
            }
            else
            {
                Debug.WriteLine("Missing " + dungeonsName);
            }
        }

        public static void ApplyFarmlandFix(DirectoryInfo outDir, string ModelsFolder, string StatesFolder)
        {
            string Farmland_Fix = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Templates", "FarmlandFix", "blockstates", "farmland.json");
            string Farmland_FixPath = Path.Combine(StatesFolder, "farmland" + ".json");
            File.WriteAllText(Farmland_FixPath, File.ReadAllText(Farmland_Fix));

            FixFarmlandBlock("farmland");
            FixFarmlandBlock("farmland_moist");
            FixFarmlandBlock("farmland1");
            FixFarmlandBlock("farmland2");
            FixFarmlandBlock("farmland3");
            FixFarmlandBlock("farmland4");
            FixFarmlandBlock("farmland5");
            FixFarmlandBlock("farmland6");

            void FixFarmlandBlock(string name)
            {
                string Farmland_Fix = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Templates", "FarmlandFix", "blocks", name + ".json");
                string Farmland_FixPath = Path.Combine(ModelsFolder, name + ".json");
                File.WriteAllText(Farmland_FixPath, File.ReadAllText(Farmland_Fix));
            }
        }

        public static void Save(DirectoryInfo outDir)
        {
            string ModelsFolder = Path.Combine(outDir.FullName, StoredResources.MCJ_BLOCKMODELS_FOLDER);
            string StatesFolder = Path.Combine(outDir.FullName, StoredResources.MCJ_BLOCKSTATES_FOLDER);
                      
            Directory.CreateDirectory(ModelsFolder);
            foreach (var entry in BlockModels)
            {
                string filePath = Path.Combine(ModelsFolder, entry.Key + ".json");
                File.WriteAllText(filePath, JsonConvert.SerializeObject(entry.Value));
            }

            Directory.CreateDirectory(StatesFolder);
            foreach (var entry in BlockStates)
            {
                string filePath = Path.Combine(StatesFolder, entry.Key + ".json");
                File.WriteAllText(filePath, JsonConvert.SerializeObject(entry.Value));
            }

            ApplyFarmlandFix(outDir, ModelsFolder, StatesFolder);
        }

    }
}
