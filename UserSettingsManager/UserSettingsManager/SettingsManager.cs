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
        public ICollection<Settings> Settings;
        private User User;

        public SettingsManager(string projectName, string userName)
        {
            SetFields(projectName, userName);
            FileManager.SetPaths(projectName);
            FileManager.CreateDirectory();
            AddSettings();
            UpdateSettingsFromFile();
        }

        private void UpdateSettingsFromFile()
        {
            Settings = FileManager.GetSettingsFromFile();
        }

        private void SetFields(string projectName, string userName)
        {
            userName = string.IsNullOrEmpty(userName) ? "DefaultUser" : userName;
            User = new User { UserName = userName };
        }

        public void AddUser(User user)
        {
            if(!UserExists(user.UserName))
            {
                var settings = SettingsBuilder(user, new UserSettings());

                FileManager.UpdateSettings(settings);

                UpdateSettingsFromFile();
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

            FileManager.RemoveSettings(Settings);

            UpdateSettingsFromFile();
        }

        private bool UserExists(string userName)
        {
            return Settings.Any(x => x.User.UserName == userName);
        }

        
        private ICollection<Settings> SettingsBuilder(User user, UserSettings userSettings)
        {
            return new List<Settings>
            {
                new Settings() { User =user, UserSettings = userSettings }
            };
        }

        public void RemoveUsers(List<string> list)
        {
            foreach (var user in list)
            {
                RemoveUser(user);
            }
        }

        private void AddSettings()
        {
            var settings = SettingsBuilder(User, new UserSettings());
            FileManager.CreateSettingsFile(settings);
        }

        public void SetSettings(ICollection<Settings> input)
        {
            Settings = input;

            FileManager.CreateSettingsFile(Settings);
        }
    }
}
