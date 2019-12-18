﻿using SettingsManager.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.Json;

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
            var jsonString = JsonSerializer.Serialize(data, SerializeOptions());
            File.WriteAllText(JsonFilePath, jsonString);
        }

        private static void GetSettingsFromFile()
        {
            var jsonString = File.ReadAllText(JsonFilePath);
            Settings = JsonSerializer.Deserialize<SettingsModel>(jsonString);
        }

        public static void SetSettings(SettingsModel input)
        {
            Settings = input;

            CreateSettingsFile(Settings);
        }

        private static JsonSerializerOptions SerializeOptions()
        {
            return new JsonSerializerOptions
            {
                WriteIndented = true
            };
        }
    }
}
