using System.Collections.Generic;
using System.ComponentModel;
using Exiled.API.Interfaces;

namespace BadNameDetector
{
    public class Config : IConfig
    {
        public bool IsEnabled { get; set; } = true;

        [Description("If the name of a user is an EXACT match, the events of the plugin will trigger.")]
        public List<string> PerfectBadNameComparisons { get; private set; } = new List<string>();

        [Description("If the name of a user CONTAINS the strings provided, the events of the plugin will trigger.")]
        public List<string> ContainsBadNameComparisons { get; private set; } = new List<string>();

        [Description("If 'EnableCustomNameGenerations' is not enabled, this will be the name set to any users who's names are replaced.")]
        public string StaticNameValue { get; private set; } = "BadNameUser{count}";

        [Description("Allows for the use of custom name replacements rather than the static one above.")]
        public bool EnableCustomNameGenerations { get; private set; } = true;

        [Description("Names to replace previosuly naughty ones if the config above is enabled.")]
        public List<string> NameReplacements { get; private set; } = new List<string>
        {
            "Nice Neighbor",
            "Good Guy",
            "David",
            "Anonymous Apple",
            "Happy Kitten",
            "Keyboard Warrior",
            "Fancy Pants",
            "Politically Correct Fellow",
            "Innocent Ringtop",
            "Galapagosian Treasurechest",
            "Cuddly Huger",
            "Radical Marshmallow",
            "Soup95"
        };

        [Description("To find special characters in names that may be used to buffer bad words and remove them.")]
        public bool RemoveSpecialCharacters { get; private set; } = true;

        [Description("Replace 4 with A(a), 3 with E(e), 1 with I(i), etc.")]
        public bool ReplaceNumbersWithWords { get; private set; } = true;

        [Description("If the name contains special characters, this will be the amount of similarity required to rename the user.")]
        public float PercentageComparision { get; private set; } = .70f;
    }
}
