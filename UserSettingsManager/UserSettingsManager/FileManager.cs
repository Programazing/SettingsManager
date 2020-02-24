using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;

namespace UserSettingsManager
{
    internal class FileManager
    {
        private string JsonFilePath;

        public FileManager(string projectName)
        {
            SetPaths(projectName);
        }

        private void SetPaths(string projectName)
        {
            projectName = string.IsNullOrEmpty(projectName) ? "DefaultProjectName" : projectName;

            var appDataPath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
            JsonFilePath = $"{appDataPath}/{projectName}/settings.json";
        }
        internal void CreateDirectory()
        {
            if (!DirectoryExists())
            {
                Directory.CreateDirectory(SettingsDirectory());
            }
        }
        internal bool SettingsFileExists() => File.Exists(JsonFilePath);
        internal bool DirectoryExists() => Directory.Exists(SettingsDirectory());
        internal string SettingsDirectory() => Directory.GetParent(JsonFilePath).ToString();
        internal void WriteToSettingsFile(IEnumerable<Settings> data)
        {
            string jsonString = Json.Serialize(data);

            File.WriteAllText(JsonFilePath, jsonString);
        }
        internal ICollection<Settings> GetSettingsFromFile()
        {
            string jsonString = File.ReadAllText(JsonFilePath);
            return Json.Deserialize(jsonString);
        }
    }
}
