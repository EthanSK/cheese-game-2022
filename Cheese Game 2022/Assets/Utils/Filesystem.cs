using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;
using System.IO;
using System.Linq;
using Newtonsoft.Json;


namespace ETGgames.Utils
{
    public static class Filesystem
    {
        public static async Task WriteFileAsync(string folderPath, string data, bool append = false)
        {
            using (StreamWriter writer = new StreamWriter(folderPath, append))
            {
                await writer.WriteAsync(data); //this just writes to buffer                
                // await writer.FlushAsync(); //only actually writes to disk here //i think it does this automatically by default

            } //apparently using using closes and disposes automatically. calling it is wrong and causes lock ups
        }

        public static async Task<string> ReadFileAsync(string folderPath)
        {
            using (StreamReader reader = new StreamReader(folderPath))
            {
                var res = await reader.ReadToEndAsync();

                return res;
            }
        }
        public static void WriteFileSync(string folderPath, string data, bool append = false)
        {
            using (StreamWriter writer = new StreamWriter(folderPath, append))
            {
                writer.Write(data); //this just writes to buffer                
            }

        }

        public static List<string> GetFilePathsInFolder(string folderPath, string fileExt)
        {
            var dirInfo = new DirectoryInfo(folderPath);
            return dirInfo.GetFiles($"*.{fileExt}").Select(file => file.FullName).ToList();
            // important we specify the file extension so it doesn't return hidden files like DS_Store
        }
    }
}


