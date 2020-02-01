using FluentAssertions;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UserSettingsManager;

namespace UserSettingsManagerTests
{
    [TestFixture]
    public class UserTests
    {
        [Test]
        public void SettingsManager_WillAddA_DefaultUser_IfPassedABlankString()
        {
            var settingsManager = new SettingsManager("", "");

            settingsManager.Settings.FirstOrDefault().User.UserName.Should().Be("DefaultUser");

        }

        [Test]
        public void SettingsManager_WillAdd_AUser()
        {
            var settingsManager = new SettingsManager("", "");

            settingsManager.AddUser(SharedData.Users().FirstOrDefault());

            var sut = settingsManager.Settings.Where(x => x.User.UserName == "Programazing").FirstOrDefault();

            sut.User.UserName.Should().Be("Programazing");
        }

        [Test]
        public void SettingsManager_WillAdd_AListOfUsers()
        {
            var settingsManager = new SettingsManager("", "");

            settingsManager.AddUsers(SharedData.Users());

            var lastUserName = SharedData.Users().LastOrDefault().UserName;

            var sut = settingsManager.Settings.LastOrDefault().User.UserName == lastUserName;

            sut.Should().BeTrue();
        }

        [Test]
        public void SettingsManager_WillRemove_AUser()
        {
            var settingsManager = new SettingsManager("", "");

            settingsManager.RemoveUser("DefaultUser");

            var sut = settingsManager.Settings.Any(x => x.User.UserName == "DefaultUser");

            sut.Should().BeFalse();
        }

        [Test]
        public void SettingsManager_WillRemove_AListOfUsers()
        {
            var settingsManager = new SettingsManager("", "");
            var userNames = SharedData.Users().Select(x => x.UserName).ToList();

            settingsManager.AddUsers(SharedData.Users());
            settingsManager.RemoveUsers(userNames);

            settingsManager.Settings.Count().Should().Be(1);                         
        }
    }
}
