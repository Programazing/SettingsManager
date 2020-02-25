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
        private readonly string JsonPath;
        public ICollection<Settings> Settings;

        public SettingsManager(string projectPath)
        {
            ValidateProjectPathString(projectPath);

            JsonPath = $"{projectPath}{Path.DirectorySeparatorChar}settings.json";

            GetFromFile();
        }

        private void ValidateProjectPathString(string projectPath)
        {
            if (!string.IsNullOrEmpty(projectPath)) { return; }
            throw new ArgumentException("Project Path Cannot Be Null or Empty.", nameof(projectPath));
        }

        private void GetFromFile() => Settings = FileManager.GetSettingsFromFile(JsonPath);


        public void UpdateSetting(Settings settings)
        {
            var toUpdate = Settings.Where(x => x.User == settings.User).FirstOrDefault();
            toUpdate = settings;

            FileManager.WriteToSettingsFile(Settings, JsonPath);

            GetFromFile();
        }

        public void AddUser(User user)
        {
            if(!UserExists(user.UserName))
            {
                var settings = SettingsBuilder(user, new UserSettings());

                var currentSettings = FileManager.GetSettingsFromFile(JsonPath);
                var newSettings = currentSettings.Union(settings);

                FileManager.WriteToSettingsFile(newSettings, JsonPath);

                GetFromFile();
            }
        }

        public void AddUsers(List<User> list)
        {
            foreach (var user in list)
            {
                AddUser(user);
            }
        }

        public void RemoveUser(string userName)
        {
            var user = Settings.Where(x => x.User.UserName == userName).FirstOrDefault();
            Settings.Remove(user);

            FileManager.WriteToSettingsFile(Settings, JsonPath);

            GetFromFile();
        }

        private bool UserExists(string userName)
        {
            return Settings.Any(x => x.User.UserName == userName);
        }

        private ICollection<Settings> SettingsBuilder(User user, UserSettings userSettings) => new List<Settings>
        {
            new Settings() { User =user, UserSettings = userSettings }
        };

        public void RemoveUsers(List<string> list)
        {
            foreach (var user in list)
            {
                RemoveUser(user);
            }
        }
    }
}
