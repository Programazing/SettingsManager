using System;
using System.Collections.Generic;
using System.Text;
using UserSettingsManager.Models;

namespace UserSettingsManager.Models
{
    public class User
    {
        public string UserName { get; set; } = string.Empty;
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
    }
}
