using System;
using System.Collections.Generic;
using System.Text;
using UserSettingsManager;

namespace UserSettingsManagerTests
{
    internal static class SharedData
    {
        internal static readonly string AppData = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);

        internal static string GetPath(string folderName) => $"{AppData}/{folderName}/settings.json";

        internal static string GetDefaultPath() => GetPath("DefaultProjectName");

        internal static List<User> Users => new List<User>()
        {
            new User { UserName = "Programazing", FirstName = "Christopher", LastName = "Johnson" },
            new User { UserName = "Rando1", FirstName = "John", LastName = "Smith" }
        };
    }
}
