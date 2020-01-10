using FluentAssertions;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace UserSettingsManagerTests
{
    [TestFixture]
    public class FileTests
    {
        string AppData = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);

        [Test]
        public void SettingsManager_CreatesDefaultSettingsFiles_WhenCalled_WithABlankString()
        {
            var settings = new UserSettingsManager.SettingsManager("");
            var path = $"{AppData}/DefaultProjectName/settings.json";

            var sut = File.Exists(path);

            sut.Should().BeTrue();
        }

        [Test]
        public void SettingsManager_Creates_CorrectlyNamedSettingsFiles_WhenCalled_WithString()
        {
            var folderName = "My Project";
            var settings = new UserSettingsManager.SettingsManager(folderName);
            var path = $"{AppData}/{folderName}/settings.json";

            var sut = File.Exists(path);

            sut.Should().BeTrue();
        }
    }
}
