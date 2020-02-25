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
    public class FileTests
    {
        private SettingsManager SettingsManager;
        readonly string Path;

        public FileTests()
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
        public void SettingsManager_ThrowsException_WhenCalled_WithABlankString()
        {
            Assert.Throws<ArgumentException>(() => new SettingsManager(""));
        }

        [Test]
        public void SettingsManager_Creates_CorrectlyNamedSettingsFiles_WhenCalled_WithString()
        {
            var sut = File.Exists(SharedData.GetFilePath(Path));

            sut.Should().BeTrue();
        }
    }
}
