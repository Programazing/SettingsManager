using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UserSettingsManager;

namespace UserSettingsManagerTests
{
    internal static class SharedData
    {
        internal static string GetDefaultFolderPath(string folderName)
        {
            var currentPath = Directory.GetCurrentDirectory().ToString();
            var parentPath = Directory.GetParent(currentPath).ToString();

            return Path.Combine(parentPath, folderName);
        }

        internal static string GetFilePath(string folderName) => $"{folderName}{Path.DirectorySeparatorChar}settings.json";

        internal static List<User> Users => new List<User>()
        {
            new User { UserName = "Programazing", FirstName = "Christopher", LastName = "Johnson" },
            new User { UserName = "Rando1", FirstName = "John", LastName = "Smith" }
        };
    }
}
