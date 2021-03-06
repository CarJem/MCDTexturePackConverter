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
    public static class Assembler
    {

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
        public static Logic_BlockMap BlockMap { get; private set; } = new Logic_BlockMap();
        public static Logic_BlockPairs BlockNames { get; private set; } = new Logic_BlockPairs();
        public static Dictionary<string, JavaRP_BlockModel> BlockModels { get; set; } = new Dictionary<string, JavaRP_BlockModel>();
        public static Dictionary<string, JavaRP_BlockState> BlockStates { get; set; } = new Dictionary<string, JavaRP_BlockState>();
        public static Logic_IntentionalBlockPairs IntentionalPairs { get; set; } = new Logic_IntentionalBlockPairs();

        #region Common Functions

        public static void Init()
        {
            string mapPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Config", "BlockMap.json");
            string namesPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Config", "BlockPairs.json");
            string intentionalPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Config", "IntentionalBlockPairs.json");


            BlockMap = JsonConvert.DeserializeObject<Logic_BlockMap>(File.ReadAllText(mapPath));
            BlockNames = JsonConvert.DeserializeObject<Logic_BlockPairs>(File.ReadAllText(namesPath));
            IntentionalPairs = JsonConvert.DeserializeObject<Logic_IntentionalBlockPairs>(File.ReadAllText(intentionalPath));

        }
        public static void AddStateToBlockStates(string block, string key, JContainer value)
        {
            if (!BlockStates.ContainsKey(block)) BlockStates.Add(block, new JavaRP_BlockState());
            if (!BlockStates[block].variants.ContainsKey(key))
            {
                BlockStates[block].variants.Add(key, value);
            }
        }
        public static void AddMultipartToBlockStates(string block, JObject value)
        {
            if (!BlockStates.ContainsKey(block)) BlockStates.Add(block, new JavaRP_BlockState());
            BlockStates[block].multipart.Add(value);
        }
        public static void AddBlockModel(string key, JavaRP_BlockModel value)
        {
            if (!BlockModels.ContainsKey(key)) BlockModels.Add(key, value);
        }
        public static string GetTemplatePath(string folder, string fileName)
        {
            return Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Templates", folder, fileName);
        }
        public static string GetBlockTexture(string texture, int? data)
        {
            return ReducePath(Paths.BlockTextureDataToBlockPath(texture, data));

            string ReducePath(string path)
            {
                if (path == null) return path;
                var filename = path.Split('/').Last();
                string extension = System.IO.Path.GetExtension(filename);
                string result = filename.Substring(0, filename.Length - extension.Length);
                return result;
            }
        }
        public static string GetBlockName(string javaName)
        {
            return javaName.Remove("minecraft:");
        }

        #endregion

        #region Assembly

        private static bool Evaluation(string javaName, List<Logic_BlockMapData> blocks)
        {
            List<Tuple<int, string>> coveredDungeonsIds = new List<Tuple<int, string>>();
            foreach (var entry in blocks)
            {
                int dungeonsID = entry.DungeonsID;
                string dungeonsName = BlockNames.GetName(dungeonsID);
                var tuple = new Tuple<int, string>(dungeonsID, dungeonsName);
                if (!coveredDungeonsIds.Contains(tuple)) coveredDungeonsIds.Add(tuple);
            }

            if (coveredDungeonsIds.Count >= 2)
            {
                coveredDungeonsIds.RemoveAll(x => IntentionalPairs.pairs.Contains(x.Item2));
                if (coveredDungeonsIds.Count >= 1)
                {
                    Debug.WriteLine("{0} contains multiple blocks: \r\n{1}\r\n", javaName, String.Join("\r\n", coveredDungeonsIds.ConvertAll(x => x.Item2).ToArray()));
                    return true;
                }
            }
            return false;
        }
        public static void AssembleBlock(string key, Logic_IBlockMapValue value)
        {
            List<Logic_BlockMapData> blocks = Logic_BlockMapData.Convert(value);
            string javaName = key;
            //Get Dungeons Name for the First Block (all of other blocks should be the same data ID)
            bool multiple = Evaluation(javaName, blocks);

            if (multiple) foreach (var block in blocks) Loop(block);
            else Loop(blocks.FirstOrDefault());

            void Loop(Logic_BlockMapData block)
            {
                string dungeonsName = BlockNames.GetName(block.DungeonsID);
                if (Paths.Blocks.blocks.ContainsKey(dungeonsName))
                {
                    var definition = Paths.Blocks.blocks[dungeonsName];
                    Generators.Generator.Generate(javaName, dungeonsName, definition, block);
                }
                else Debug.WriteLine("Missing " + dungeonsName);
            }
        }
        public static void Save(DirectoryInfo outDir)
        {
            string ModelsFolder = Path.Combine(outDir.FullName, Paths.MCJ_BLOCKMODELS_FOLDER);
            string StatesFolder = Path.Combine(outDir.FullName, Paths.MCJ_BLOCKSTATES_FOLDER);
                      
            Directory.CreateDirectory(ModelsFolder);
            foreach (var entry in BlockModels)
            {
                if (entry.Value.elements.Count == 0) entry.Value.elements = null;
                string filePath = Path.Combine(ModelsFolder, entry.Key + ".json");
                File.WriteAllText(filePath, JsonConvert.SerializeObject(entry.Value, Formatting.Indented, GetSettings()));
            }

            Directory.CreateDirectory(StatesFolder);
            foreach (var entry in BlockStates)
            {
                if (entry.Value.multipart.Count == 0) entry.Value.multipart = null;
                string filePath = Path.Combine(StatesFolder, entry.Key + ".json");
                File.WriteAllText(filePath, JsonConvert.SerializeObject(entry.Value, Formatting.Indented, GetSettings()));
            }

            ApplyFixes(outDir, ModelsFolder, StatesFolder);

            JsonSerializerSettings GetSettings()
            {
                return new JsonSerializerSettings()
                {
                    NullValueHandling = NullValueHandling.Ignore
                };
            }
        }
        public static void ApplyFixes(DirectoryInfo outDir, string ModelsFolder, string StatesFolder)
        {
            //No Longer Needed: New System Implemented

            //PatchBlockstateFix("farmland_blockstate", "farmland");
            //PatchModelFix("farmland_model", "farmland");
            //PatchModelFix("farmland_moist_model", "farmland_moist");
            //PatchModelFix("farmland1_model", "farmland1");
            //PatchModelFix("farmland2_model", "farmland2");
            //PatchModelFix("farmland3_model", "farmland3");
            //PatchModelFix("farmland4_model", "farmland4");
            //PatchModelFix("farmland5_model", "farmland5");
            //PatchModelFix("farmland6_model", "farmland6");

            PatchModelFix("leaves_model", "leaves");

            void PatchBlockstateFix(string filename, string name)
            {
                string Farmland_Fix = GetTemplatePath("fix", filename + ".json");
                string Farmland_FixPath = Path.Combine(StatesFolder, name + ".json");
                File.WriteAllText(Farmland_FixPath, File.ReadAllText(Farmland_Fix));
            }

            void PatchModelFix(string filename, string name)
            {
                string Farmland_Fix = GetTemplatePath("fix", filename + ".json");
                string Farmland_FixPath = Path.Combine(ModelsFolder, name + ".json");
                File.WriteAllText(Farmland_FixPath, File.ReadAllText(Farmland_Fix));
            }
        }

        #endregion

    }
}
