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
        private readonly FileManager FileManager;
        private User User;

        public SettingsManager(string projectName, string userName)
        {
            FileManager = new FileManager(projectName);
            SetFields(userName);

            GetSettingsFromFile();
        }

        private void SetFields(string userName)
        {
            userName = string.IsNullOrEmpty(userName) ? "DefaultUser" : userName;
            User = new User { UserName = userName };
        }

        private void GetSettingsFromFile()
        {
            try
            {
                Settings = FileManager.GetSettingsFromFile();
            }
            catch (Exception ex) when (ex is DirectoryNotFoundException || ex is FileNotFoundException)
            {
                AddDefaultSettings();

                Settings = FileManager.GetSettingsFromFile();
            }
        }

        private void AddDefaultSettings()
        {
            FileManager.CreateDirectory();

            var settings = SettingsBuilder(User, new UserSettings());
            FileManager.WriteToSettingsFile(settings);

        }

        public void UpdateSetting(Settings settings)
        {
            var toUpdate = Settings.Where(x => x.User == settings.User).FirstOrDefault();
            toUpdate = settings;

            FileManager.WriteToSettingsFile(Settings);

            GetSettingsFromFile();
        }



        public void AddUser(User user)
        {
            if(!UserExists(user.UserName))
            {
                var settings = SettingsBuilder(user, new UserSettings());

                var currentSettings = FileManager.GetSettingsFromFile();
                var newSettings = currentSettings.Union(settings);

                FileManager.WriteToSettingsFile(settings);

                GetSettingsFromFile();
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

            FileManager.WriteToSettingsFile(Settings);

            GetSettingsFromFile();
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
