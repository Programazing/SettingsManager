using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;

namespace UserSettingsManager
{
    static class FileManager
    {
        private static string AppFolderPath;
        private static string JsonFilePath;

        internal static void SetPaths(string projectName)
        {
            projectName = string.IsNullOrEmpty(projectName) ? "DefaultProjectName" : projectName;

            var appDataPath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
            AppFolderPath = $"{appDataPath}/{projectName}";
            JsonFilePath = $"{AppFolderPath}/settings.json";
        }

        internal static void CreateDirectory()
        {
            if (!Directory.Exists(AppFolderPath))
            {
                Directory.CreateDirectory(AppFolderPath);
            }
        }

        internal static void CreateSettingsFile(IEnumerable<Settings> data)
        {
            string jsonString = Json.Serialize(data);

            File.WriteAllText(JsonFilePath, jsonString);
        }

        internal static void UpdateSettings(ICollection<Settings> input)
        {
            var currentSettings = GetSettingsFromFile();
            var newSettings = currentSettings.Union(input);
            CreateSettingsFile(newSettings);
        }

        internal static void RemoveSettings(ICollection<Settings> input)
        {
            CreateSettingsFile(input);
        }

        internal static ICollection<Settings> GetSettingsFromFile()
        {
            string jsonString = File.ReadAllText(JsonFilePath);
            return Json.Deserialize(jsonString);
        }
    }
}
