using UserSettingsManager.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.Json;

namespace UserSettingsManager
{
    public class SettingsManager
    {
        private string AppFolderPath;
        private string JsonFilePath;
        public SettingsModel Settings;
        private string ProjectName;

        public SettingsManager(string projectName)
        {
            ProjectName = string.IsNullOrEmpty(projectName) ? "DefaultProjectName" : projectName;
            Instance();
        }

        private void Instance()
        {
            var appData = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            AppFolderPath = $"{appData}/{ProjectName}";
            JsonFilePath = $"{AppFolderPath}/settings.json";

            if (!Directory.Exists(AppFolderPath))
            {
                Directory.CreateDirectory(AppFolderPath);

                CreateSettingsFile(new SettingsModel());
            }

            GetSettingsFromFile();
        }

        private void CreateSettingsFile(SettingsModel data)
        {
            var jsonString = JsonSerializer.Serialize(data, SerializeOptions());
            File.WriteAllText(JsonFilePath, jsonString);
        }

        private void GetSettingsFromFile()
        {
            var jsonString = File.ReadAllText(JsonFilePath);
            Settings = JsonSerializer.Deserialize<SettingsModel>(jsonString);
        }

        public void SetSettings(SettingsModel input)
        {
            Settings = input;

            CreateSettingsFile(Settings);
        }

        private JsonSerializerOptions SerializeOptions()
        {
            return new JsonSerializerOptions
            {
                WriteIndented = true
            };
        }
    }
}
