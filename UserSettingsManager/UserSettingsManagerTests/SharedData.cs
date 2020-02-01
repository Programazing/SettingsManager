using System;
using System.Collections.Generic;
using System.Text;

namespace UserSettingsManagerTests
{
    internal static class SharedData
    {
        internal static readonly string AppData = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);

        internal static string GetPath(string folderName)
        {
            return $"{AppData}/{folderName}/settings.json";
        }
    }
}
