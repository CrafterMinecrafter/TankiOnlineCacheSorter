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
        //0 is start time, 1 is end time
        private static DateTime[] times = new DateTime[2];
        private static TimeSpan calcedTime
        {
            get => times[1] - times[0];
        }
        public static async Task<bool> Sort(string Path, string OutputPath)
        {
            times[0] = DateTime.Now;
            MConsole.WriteLine($"Please wait, copying files from:\n{Path}\nto:\n{OutputPath}");
            string[] cacheFiles = Directory.GetFiles(Path);
            int cl = (int)(((float)cacheFiles.Length) * 0.1f);
            string currentName = "";
            for (int i = 0; i < cacheFiles.Length; i++)
            {
                currentName = DecryptFileName(cacheFiles[i]);
                await CopyFileAsync(cacheFiles[i], OutputPath + "/" + Regex.Replace(currentName, "(https|http)://.*.com/", ""));
                if (i % cl == 0)
                    MConsole.WriteLine((int)Mathf.Clamp(value: ((float)i / (float)cacheFiles.Length) * 100, min: 0, max: 100) + "% | " + i + "/" + cacheFiles.Length);
            }
            times[1] = DateTime.Now;

            MConsole.WriteLine($"Done!\nWork time:\n[ Hours: {calcedTime.Hours} | Minutes: {calcedTime.Minutes} | Seconds: {calcedTime.TotalSeconds} ]", ConsoleColor.Magenta);
            GC.Collect();
            return true;
        }
        private static string DecryptFileName(string fileName) =>
            Encoding.UTF8.GetString(Convert.FromBase64String(Path.GetFileName(fileName)));
    }
}
