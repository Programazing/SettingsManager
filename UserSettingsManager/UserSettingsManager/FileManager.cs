using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;

namespace UserSettingsManager
{
    internal static class FileManager
    {
        internal static void WriteToSettingsFile(IEnumerable<Settings> data, string filePath)
        {
            string jsonString = Json.Serialize(data);

            File.WriteAllText(filePath, jsonString);
        }
        internal static ICollection<Settings> GetSettingsFromFile(string filePath)
        {
            AddDefaultSettingsIfNeeded(filePath);

            string jsonString = File.ReadAllText(filePath);
            return Json.Deserialize(jsonString);
        }

        private static void AddDefaultSettingsIfNeeded(string filePath)
        {
            if(!SettingsFileExists(filePath))
            {
                CreateDirectory(filePath);
                WriteToSettingsFile(DefaultSettings(), filePath);
            }
        }

        private static void CreateDirectory(string filePath)
        {
            if (!DirectoryExists(filePath))
            {
                var folder = Directory.GetParent(filePath).ToString();
                Directory.CreateDirectory(folder);
            }
        }

        private static IEnumerable<Settings> DefaultSettings()
        {
            var defaultSettings = new Settings() { User = new User { UserName = "DefaultUserName" }, UserSettings = new UserSettings() };
            return new List<Settings> { defaultSettings };
        }

        internal static bool SettingsFileExists(string filePath) => File.Exists(filePath);
        internal static bool DirectoryExists(string filePath) => Directory.Exists(filePath);
    }
}
