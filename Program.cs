using System;
using System.IO;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json;
using System.Diagnostics;
using MCDTexturePackConverter.Classes;

namespace MCDTexturePackConverter
{
    class Program
    {


        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            ModelAssembler.Init();
            string path = @"D:\UserData\Modding Workspaces\MC Dungeons Modding\Game Dumps\Current\Dungeons\Content\data\resourcepacks\squidcoast";
            string outpath = @"D:\UserData\Saved Games\MultiMC\instances\Minecraft Dungeons Development\.minecraft\resourcepacks\DungeonsConvertedPack";
            Convert(path, outpath, true);
        }

        static void Convert(string path, string outpath, bool verbose)
        {
            StoredResources.Init(path);

            var outDir = Init(path, outpath, verbose);

            foreach (var block in ModelAssembler.BlockMap.definitions)
            {
                IBlockMap_Value real_block = IBlockMap_Value.Convert(block.Value);
                ModelAssembler.AssembleBlock(block.Key, real_block);
            }

            ModelAssembler.Save(outDir);

            Debug.WriteLine("Done Making Models");
        }

        static DirectoryInfo Init(string path, string outPath, bool verbose)
        {
            Directory.CreateDirectory(outPath);
            var outDir = new DirectoryInfo(outPath);

            //Creating directory to store blocks textures
            Directory.CreateDirectory(Path.Combine(outDir.FullName, StoredResources.MCJ_BLOCK_FOLDER));

            //Copying icon pack
            File.Copy(Path.Combine(path, StoredResources.MCD_PACK_ICON), Path.Combine(outDir.FullName, StoredResources.MCJ_PACK_ICON), true);

            Extensions.CopyFilesRecursively(Path.Combine(path, StoredResources.MCD_BLOCK_FOLDER), Path.Combine(outDir.FullName, StoredResources.MCJ_BLOCK_FOLDER));

            //Creating meta description file
            var meta_contents = "{ \"pack\": { \"pack_format\": 5, \"description\": \"auto generated resources pack\" } }";
            File.WriteAllText(Path.Combine(outDir.FullName, StoredResources.MCJ_PACK_META), meta_contents);
            return outDir;
        }


    }
}
