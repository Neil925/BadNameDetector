using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Exiled.API.Features;

namespace BadNameDetector
{
    internal static class Handlers
    {
        public static List<string> InUseCustomNames = new List<string>();
        public static int nameReplacements;

        private static readonly Config _config = Plugin.Instance.Config;
        private static readonly Random rand = new Random();

        public static string RemoveSpecialCharacters(this string str)
        {
            StringBuilder sb = new StringBuilder();
            foreach (var c in str.Where(c => c >= '0' && c <= '9' || c >= 'A' && c <= 'Z' || c >= 'a' && c <= 'z' || c == '.' || c == '_'))
                sb.Append(c);

            return sb.ToString();
        }

        public static void SetName(Player player, string name)
        {
            string nameReplacement = _config.EnableCustomNameGenerations ?
                _config.NameReplacements.Where(s => !InUseCustomNames.Contains(s)).ElementAt(rand.Next(_config.NameReplacements.Count(s => !InUseCustomNames.Contains(s))))
                : _config.StaticNameValue.Replace("{count}", nameReplacements.ToString());

            player.DisplayNickname = nameReplacement;
            InUseCustomNames.Add(nameReplacement);
        }
    }
}
