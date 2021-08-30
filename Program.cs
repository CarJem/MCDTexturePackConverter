using System;
using System.IO;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json;
using System.Diagnostics;
using MCDTexturePackConverter.Classes;
using ImageMagick;
using FolderBrowserEx;
using System.Windows.Forms;
using FolderBrowserDialog = FolderBrowserEx.FolderBrowserDialog;
using System.Threading;

namespace MCDTexturePackConverter
{
    class Program
    {

        public const string VERSION = "0.1";

        //private const string DefaultInputDir = @"D:\UserData\Modding Workspaces\MC Dungeons Modding\Game Dumps\Current\Dungeons\Content\data\resourcepacks\squidcoast";
        //public const string DefaultOutputDir = @"D:\UserData\Saved Games\MultiMC\instances\Minecraft Dungeons Development\.minecraft\resourcepacks\DungeonsConvertedPack";

        //private const string DefaultInputDir = @"D:\UserData\Modding Workspaces\MC Dungeons Modding\Game Dumps\Current\Dungeons\Content\data\resourcepacks\coralrise";
        //public const string DefaultOutputDir = @"D:\UserData\Saved Games\MultiMC\instances\Minecraft Dungeons Development\.minecraft\resourcepacks\DungeonsCoralRise";

        private const string DefaultInputDir = "";
        public const string DefaultOutputDir = "";

        static void Main(string[] args)
        {
            Console.WriteLine("--------------------------------------------------");
            Console.WriteLine("Minecraft Dungeons to Java Resource Pack Converter");
            Console.WriteLine("          Developed by CarJem Generations         ");
            Console.WriteLine("                 Version {0}                      ", VERSION);
            Console.WriteLine("--------------------------------------------------\r\n");

            string input_start_directory = DefaultInputDir;
            string output_start_directory = DefaultOutputDir;

            if (args.Length >= 2)
            {
                input_start_directory = args[0];
                output_start_directory = args[1];
            }

            Assembler.Init();
            string path = GetInputPath(input_start_directory);
            if (path == null) return;
            string outpath = GetOutputPath(output_start_directory);
            if (outpath == null) return;
            Convert(path, outpath, true);
        }

        static string GetInputPath(string startDir = "")
        {
            FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog();
            folderBrowserDialog.Title = "Select input folder";
            folderBrowserDialog.InitialFolder = startDir;
            folderBrowserDialog.AllowMultiSelect = false;
            if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
            {
                string resource = folderBrowserDialog.SelectedFolder;
                Console.WriteLine("Dungeons Resource Pack: \r\n{0}\r\n", resource);
                return resource;
            }

            else return null;
        }

        static string GetOutputPath(string startDir = "")
        {
            FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog();
            folderBrowserDialog.Title = "Select output folder";
            folderBrowserDialog.InitialFolder = startDir;
            folderBrowserDialog.AllowMultiSelect = false;
            if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
            {
                string resource = folderBrowserDialog.SelectedFolder;
                Console.WriteLine("Java Resource Pack: \r\n{0}\r\n", resource);
                return resource;
            }
            else return null;
        }

        static void Convert(string path, string outpath, bool verbose)
        {
            Paths.Init(path);

            var outDir = GeneratePack(path, outpath, verbose);

            int current = 0;
            int maximum = Assembler.BlockMap.definitions.Count;

            InitProgress("Generating Models");

            foreach (var block in Assembler.BlockMap.definitions)
            {
                Logic_IBlockMapValue real_block = Logic_IBlockMapValue.Convert(block.Value);
                Assembler.AssembleBlock(block.Key, real_block);

                current += 1;
                UpdateProgress("Generating Models", maximum, current);
            }

            Console.WriteLine("Saving...");
            Assembler.Save(outDir);
            Console.WriteLine("Finished!");
        }

        static void InitProgress(string message)
        {
            Console.WriteLine("{1}: {0}%", 0, message);
        }

        static void UpdateProgress(string message, double maximum, double current)
        {
            Console.SetCursorPosition(Console.CursorLeft, Console.CursorTop - 1);

            // this could be made a free-standing static method for multiple calls
            char[] blankline = new char[80];
            Console.Write(blankline, 0, 80); // this will line-wrap at 80 so use Write()

            int progress = (int)Math.Round((double)(100 * current) / maximum);
            Console.WriteLine("{1}: {0}%", progress, message);
            //Thread.Sleep(1);
        }

        static DirectoryInfo GeneratePack(string path, string outPath, bool verbose)
        {
            Directory.CreateDirectory(outPath);
            var outDir = new DirectoryInfo(outPath);

            //Creating directory to store blocks textures
            Directory.CreateDirectory(Path.Combine(outDir.FullName, Paths.MCJ_BLOCK_FOLDER));

            CreatePackData(path, outDir); ;

            MigrateImages(Path.Combine(path, Paths.MCD_BLOCK_FOLDER), Path.Combine(outDir.FullName, Paths.MCJ_BLOCK_FOLDER));

            return outDir;
        }

        private static void CreatePackData(string path, DirectoryInfo outDir)
        {
            Console.WriteLine("Creating Pack Data...");

            //Copying icon pack
            File.Copy(Path.Combine(path, Paths.MCD_PACK_ICON), Path.Combine(outDir.FullName, Paths.MCJ_PACK_ICON), true);

            //Creating meta description file
            var meta_contents = "{ \"pack\": { \"pack_format\": 5, \"description\": \"auto generated resources pack\" } }";
            File.WriteAllText(Path.Combine(outDir.FullName, Paths.MCJ_PACK_META), meta_contents);
        }

        public static void MigrateImages(string sourcePath, string targetPath)
        {

            var files = Directory.GetFiles(sourcePath, "*.*", SearchOption.AllDirectories);

            //Now Create all of the directories
            foreach (string dirPath in Directory.GetDirectories(sourcePath, "*", SearchOption.AllDirectories))
            {
                Directory.CreateDirectory(dirPath.Replace(sourcePath, targetPath));
            }

            int current = 0;
            int maximum = files.Count();

            InitProgress("Migrating Textures");

            //Copy all the files & Replaces any files with the same name
            foreach (string newPath in files)
            {
                string filePath = newPath.Replace(sourcePath, targetPath).Replace(Path.GetFileName(newPath), Path.GetFileName(newPath).ToLower());

                if (Path.GetExtension(newPath) == ".tga")
                {
                    using (var image = new MagickImage(newPath))
                    {
                        image.Write(filePath, MagickFormat.Png);
                    }
                }
                else File.Copy(newPath, filePath, true);

                current += 1;
                UpdateProgress("Migrating Textures", maximum, current);
            }
        }


    }
}
