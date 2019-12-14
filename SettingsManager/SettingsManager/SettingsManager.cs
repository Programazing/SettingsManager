using Newtonsoft.Json;
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
        private static SettingsModel Settings;

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

                var data = new SettingsModel
                {
                    SomeSetting = ""
                };

                CreateSettingsFile(data);
            }

            GetSettings();
        }

        private static void CreateSettingsFile(SettingsModel data)
        {
            using StreamWriter file = File.CreateText(JsonFilePath);
            JsonSerializer serializer = new JsonSerializer();
            serializer.Serialize(file, data);
        }

        private static void GetSettings()
        {
            Settings = JsonConvert.DeserializeObject<SettingsModel>(File.ReadAllText(JsonFilePath));
        }

        public static string GetSomeSetting()
        {
            return Settings.SomeSetting;
        }

        public static void SetSomeSetting(string input)
        {
            Settings.SomeSetting = input;

            CreateSettingsFile(Settings);
        }
    }
}
