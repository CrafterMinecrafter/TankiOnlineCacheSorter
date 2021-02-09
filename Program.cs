using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Extensions.Console;

namespace TankiOnlineCacheSorter
{
    class Program
    {
        class Settings
        {
            public static readonly string MainDrive = Path.GetPathRoot(Environment.SystemDirectory);

            public static readonly string DefaultPathToCache =
                MainDrive + "Users\\" +
                Environment.UserName + "\\AppData\\Roaming\\tankionline\\Local Store\\cache";

            public static string CurrentPathToCacheFolder = DefaultPathToCache;



            public static readonly string DefaultOutputPath = 
                MainDrive + "TO_Cache\\";

            public static string CurrentOutputPath = DefaultOutputPath;


        }

        static async Task Main(string[] args)
        {
            string[] cfg = File.ReadAllLines("Config.txt");
            #region Cache Folder Select
            MConsole.WriteLine("Use default path to cache folder? Y\\N");
            if (Utils.YNorUnknown(Console.ReadLine()[0]) == 1)
            {
                Settings.CurrentPathToCacheFolder = cfg[0];
            }
            MConsole.WriteLine("Setted path:\n" + Settings.CurrentPathToCacheFolder + "\n", ConsoleColor.Green);

            #endregion

            #region Output Folder Select
            MConsole.WriteLine("Use default output folder? Y\\N");

            if (Utils.YNorUnknown(Console.ReadLine()[0]) == 1)
            {
                Settings.CurrentPathToCacheFolder = cfg[1];
                MConsole.WriteLine("Setted path:\n" + Settings.CurrentPathToCacheFolder + "\n", ConsoleColor.Green);
            }
            MConsole.WriteLine("Setted path:\n" + Settings.CurrentPathToCacheFolder + "\n", ConsoleColor.Green);

            if (Directory.Exists(Settings.CurrentOutputPath))
            {
                if (Directory.GetFiles(Settings.CurrentOutputPath).Length != 0)
                {
                    MConsole.WriteLine("Delete files in output path!", ConsoleColor.Red);
                    Console.ReadLine();
                    return;
                }
            }

            MConsole.WriteLine("Setted path:\n" + Settings.CurrentOutputPath + "\n", ConsoleColor.Green);
            #endregion

            await Cache.Sort(Settings.CurrentPathToCacheFolder, Settings.CurrentOutputPath);
        }
    }
}
