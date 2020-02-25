using FluentAssertions;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using UserSettingsManager;

namespace UserSettingsManagerTests
{
    [TestFixture]
    public class SettingsTests
    {
        private SettingsManager SettingsManager;
        readonly string Path;

        public SettingsTests()
        {
            Path = SharedData.GetDefaultFolderPath("DefaultFolderName");
        }

        [SetUp]
        public void Init()
        {
            SettingsManager = new SettingsManager(Path);
        }

        [TearDown]
        public void Cleanup()
        {
            File.Delete(SharedData.GetFilePath(Path));
            Directory.Delete(Directory.GetParent(SharedData.GetFilePath(Path)).ToString());
        }

        [Test]
        public void SettingsManager_CanUpdate_Settings()
        {
            var settingsManager = new SettingsManager(Path);

            settingsManager.Settings.FirstOrDefault().UserSettings.NumberSetting = 87;

            settingsManager.UpdateSettings();

            settingsManager.Settings.FirstOrDefault().UserSettings.NumberSetting.Should().Be(87);
        }
    }
}
