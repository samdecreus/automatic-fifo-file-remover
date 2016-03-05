using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using FifoMachine.Models;
using Newtonsoft.Json;

namespace FifoMachine
{
    class FifoManager
    {
        public void Execute(string settingsLocation)
        {
            var settings = GetSettings(settingsLocation);

            foreach (var path in settings.Paths)
            {
                RemoveOldFiles(path, settings.MaxAgeInDays);
            }

            Console.WriteLine(@"Done...");
        }

        private void RemoveOldFiles(string folderPath, int maxAgeInDays)
        {
            Debug.WriteLine(@"Removing current path: " + folderPath);

            var files = new DirectoryInfo(folderPath).GetFiles();

            foreach (var file in files.Where(file => file.LastWriteTime < DateTime.Now.Subtract(new TimeSpan(maxAgeInDays * 24, 0, 0))))
            {
                Debug.WriteLine(@"Removeing file: " + file.Name);
                file.Delete();
            }
        }

        private Settings GetSettings(string settingsPath)
        {
            Debug.WriteLine(@"Getting settings on path: " + settingsPath);

            var streamReader = new StreamReader(settingsPath);
            var jsonReader = new JsonTextReader(streamReader);

            var serializer = new JsonSerializer();
            return serializer.Deserialize<Settings>(jsonReader);           
        }
    }
}
