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
    public class SettingsTests
    {
        [Test]
        public void SettingsManager_CanUpdate_Settings()
        {
            var settingsManager = new SettingsManager("", "");

            var user = settingsManager.Settings.FirstOrDefault();

            user.UserSettings.NumberSetting = 87;

            settingsManager.UpdateSetting(user);


        }
    }
}
