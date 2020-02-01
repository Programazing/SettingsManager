using UserSettingsManager.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.Json;
using System.Linq;

namespace UserSettingsManager
{
    public class SettingsManager
    {
        private string AppFolderPath;
        private string JsonFilePath;
        public List<Settings> Settings;
        private List<User> Users;
        private string ProjectName;
        private string UserName;

        public SettingsManager(string projectName, string userName)
        {
            SetFields(projectName, userName);
            SetPaths();
            CreateDirectory();
            GetSettingsFromFile();
        }

        private void SetFields(string projectName, string userName)
        {
            ProjectName = string.IsNullOrEmpty(projectName) ? "DefaultProjectName" : projectName;
            UserName = string.IsNullOrEmpty(userName) ? "DefaultUser" : userName;
            Users = new List<User>() { new User { UserName = UserName } };
        }

        private void SetPaths()
        {
            var appDataPath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
            AppFolderPath = $"{appDataPath}/{ProjectName}";
            JsonFilePath = $"{AppFolderPath}/settings.json";
        }

        private void CreateDirectory()
        {
            if (!Directory.Exists(AppFolderPath))
            {
                Directory.CreateDirectory(AppFolderPath);

                var users = new List<Settings>
                {
                    new Settings() { User = Users.FirstOrDefault(), UserSettings = new SettingsModel() }
                };

                CreateSettingsFile(users);
            }
        }

        private void CreateSettingsFile(List<Settings> data)
        {
            var jsonString = JsonSerializer.Serialize(data, SerializeOptions());
            File.WriteAllText(JsonFilePath, jsonString);
        }

        private void GetSettingsFromFile()
        {
            var jsonString = File.ReadAllText(JsonFilePath);
            Settings = JsonSerializer.Deserialize<List<Settings>>(jsonString);
        }

        public void SetSettings(List<Settings> input)
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
