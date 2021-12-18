using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Exiled.API.Features;

namespace BadNameDetector
{
    internal static class Handlers
    {
        private static readonly List<string> InUseCustomNames = new List<string>();
        private static int _nameReplacements;

        private static readonly Config Config = Plugin.Instance.Config;
        private static readonly Random Rand = new Random();

        public static void RemoveSpecialCharacters(string str, out string newName, out List<int> removedChars)
        {
            StringBuilder sb = new StringBuilder();
            List<int> indexArray = new List<int>();
            int count = 0;

            foreach (var c in str)
            {
                if (c >= '0' && c <= '9' || c >= 'A' && c <= 'Z' || c >= 'a' && c <= 'z')
                    sb.Append(c);
                else
                    indexArray.Add(count);
                count++;
            }

            newName = sb.ToString();
            removedChars = indexArray;
        }

        public static void SetName(Player player)
        {
            string nameReplacement = Config.EnableCustomNameGenerations ?
                Config.NameReplacements.Where(s => !InUseCustomNames.Contains(s)).ElementAt(Rand.Next(Config.NameReplacements.Count(s => !InUseCustomNames.Contains(s))))
                : Config.StaticNameValue.Replace("{count}", _nameReplacements.ToString());

            player.DisplayNickname = nameReplacement;
            InUseCustomNames.Add(nameReplacement);
            _nameReplacements++;
        }

        public static void ResetValues()
        {
            _nameReplacements = 0;
            InUseCustomNames.Clear();
        }
    }
}
