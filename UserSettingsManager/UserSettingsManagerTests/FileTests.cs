using FluentAssertions;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using UserSettingsManager.Models;

namespace UserSettingsManagerTests
{
    [TestFixture]
    public class FileTests
    {

        [Test]
        public void SettingsManager_CreatesDefaultSettingsFiles_WhenCalled_WithABlankString()
        {
            _ = new UserSettingsManager.SettingsManager("", "");

            var sut = File.Exists(SharedData.GetPath("DefaultProjectName"));

            sut.Should().BeTrue();
        }

        [Test]
        public void SettingsManager_Creates_CorrectlyNamedSettingsFiles_WhenCalled_WithString()
        {
            var folderName = "My Project";
            _ = new UserSettingsManager.SettingsManager(folderName, "");
            var path = SharedData.GetPath(folderName);

            var sut = File.Exists(path);

            sut.Should().BeTrue();
        }
    }
}
