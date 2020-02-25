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
    public class UserTests
    {
        private SettingsManager SettingsManager;
        readonly string Path;

        public UserTests()
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
        public void SettingsManager_WillAddA_DefaultUser()
        {
            SettingsManager.Settings.FirstOrDefault().User.UserName.Should().Be("DefaultUserName");
        }

        [Test]
        public void SettingsManager_WillAdd_AUser()
        {
            SettingsManager.AddUser(SharedData.Users.FirstOrDefault());

            var sut = SettingsManager.Settings.Where(x => x.User.UserName == "Programazing").FirstOrDefault();

            sut.User.UserName.Should().Be("Programazing");
        }

        [Test]
        public void SettingsManager_WillAdd_AListOfUsers()
        {
            SettingsManager.AddUsers(SharedData.Users);

            var lastUserName = SharedData.Users.LastOrDefault().UserName;

            var sut = SettingsManager.Settings.LastOrDefault().User.UserName == lastUserName;

            sut.Should().BeTrue();
        }

        [Test]
        public void SettingsManager_WillRemove_AUser()
        {
            SettingsManager.RemoveUser("DefaultUser");

            var sut = SettingsManager.Settings.Any(x => x.User.UserName == "DefaultUser");

            sut.Should().BeFalse();
        }

        [Test]
        public void SettingsManager_WillRemove_AListOfUsers()
        {
            var userNames = SharedData.Users.Select(x => x.UserName).ToList();

            SettingsManager.AddUsers(SharedData.Users);
            SettingsManager.RemoveUsers(userNames);

            SettingsManager.Settings.Count().Should().Be(1);                         
        }
    }
}
