using Newtonsoft.Json;
using SettingsManager.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace SettingsManager
{
    public static class SettingsManager
    {
        private static string AppFolderPath;
        private static string JsonFilePath;
        public static SettingsModel Settings;

        static SettingsManager()
        {
            Instance();
        }

        private static void Instance()
        {
            var appData = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            AppFolderPath = $"{appData}/YourProjectName";
            JsonFilePath = $"{AppFolderPath}/settings.json";

            if (!Directory.Exists(AppFolderPath))
            {
                Directory.CreateDirectory(AppFolderPath);

                CreateSettingsFile(new SettingsModel());
            }

            GetSettingsFromFile();
        }

        private static void CreateSettingsFile(SettingsModel data)
        {
            using StreamWriter file = File.CreateText(JsonFilePath);
            JsonSerializer serializer = new JsonSerializer();
            serializer.Serialize(file, data);
        }

        private static void GetSettingsFromFile()
        {
            Settings = JsonConvert.DeserializeObject<SettingsModel>(File.ReadAllText(JsonFilePath));
        }

        public static void SetSettings(SettingsModel input)
        {
            Settings = input;

            CreateSettingsFile(Settings);
        }
    }
}
