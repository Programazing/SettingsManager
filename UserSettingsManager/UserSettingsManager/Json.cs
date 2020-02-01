using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;

namespace UserSettingsManager
{
    static class Json
    {
        internal static string Serialize(object input)
        {
            return JsonSerializer.Serialize(input, SerializeOptions());
        }

        internal static ICollection<Settings> Deserialize(string input)
        {
            return JsonSerializer.Deserialize<ICollection<Settings>>(input);
        }

        private static JsonSerializerOptions SerializeOptions()
        {
            return new JsonSerializerOptions
            {
                WriteIndented = true
            };
        }
    }
}
