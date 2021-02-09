using Extensions.Console;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Mathf;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace TankiOnlineCacheSorter
{
    class Cache
    {
        private static async Task CopyFileAsync(string sourceFile, string destinationFile)
        {
            Directory.CreateDirectory(Path.GetDirectoryName(destinationFile));
            using (var sourceStream = new FileStream(sourceFile, FileMode.Open, FileAccess.Read, FileShare.Read, 4096, FileOptions.Asynchronous | FileOptions.SequentialScan))
            using (var destinationStream = new FileStream(destinationFile + "", FileMode.CreateNew, FileAccess.Write, FileShare.None, 4096, FileOptions.Asynchronous | FileOptions.SequentialScan))
                await sourceStream.CopyToAsync(destinationStream);
        }

        public static async Task<bool> Sort(string Path, string OutputPath)
        {
            MConsole.WriteLine($"Please wait, copying files from:\n{Path}\n to:\n{OutputPath}");
            string[] cacheFiles = Directory.GetFiles(Path);
            string currentName = "";
            for (int i = 0; i < cacheFiles.Length; i++)
            {
                if (i % 100 == 1)
                    MConsole.WriteLine(Mathf.Clamp(value: (i / cacheFiles.Length) * 100, min: 0, max: 100) + "%");
                currentName = DecryptFileName(cacheFiles[i]);
                await CopyFileAsync(cacheFiles[i], OutputPath + "/" + Regex.Replace(currentName, "(https|http)://.*.com/", ""));
            }
            MConsole.WriteLine("100%\nDone!", ConsoleColor.Magenta);
            return true;
        }
        private static string DecryptFileName(string fileName) =>
            Encoding.UTF8.GetString(Convert.FromBase64String(Path.GetFileName(fileName)));
    }
}
