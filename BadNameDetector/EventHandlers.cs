using System;
using System.Collections.Generic;
using System.Linq;
using Exiled.Events.EventArgs;
using static BadNameDetector.Handlers;

namespace BadNameDetector
{
    internal class EventHandlers
    {
        private static readonly Config Config = Plugin.Instance.Config;

        public void OnVerified(VerifiedEventArgs ev)
        {
            string name = ev.Player.Nickname;
            List<int> removedCharacters = new List<int>();

            if (Config.RemoveSpecialCharacters)
                RemoveSpecialCharacters(name, out name, out removedCharacters);

            if (Config.ReplaceNumbersWithWords)
                name = name.Replace("0", "o").Replace("9", "g").Replace("8", "b").Replace("7", "i").Replace("5", "s").Replace("4", "a").Replace("3", "e").Replace("1", "i");

            if (Config.PerfectBadNameComparisons.Contains(name, StringComparison.CurrentCultureIgnoreCase))
            {
                SetName(ev.Player);
                return;
            }

            if (Config.ContainsBadNameComparisons.Any(s =>
                name.ToLower().Contains(s.ToLower()) && !string.IsNullOrEmpty(name)))
            {
                SetName(ev.Player);
                return;
            }

            if (name.Length == ev.Player.Nickname.Length) return;

            foreach (var comparison in Config.PerfectBadNameComparisons)
            {
                foreach (var removed in removedCharacters.Where(removed => comparison.Length < removed))
                    comparison.Remove(removed);

                if ((float)comparison.Length / ev.Player.Nickname.Length < Config.PercentageComparision) continue;
                if (!string.Equals(comparison, name, StringComparison.CurrentCultureIgnoreCase)) continue;

                SetName(ev.Player);
                return;
            }

            foreach (var comparison in Config.ContainsBadNameComparisons)
            {
                foreach (var removed in removedCharacters.Where(removed => comparison.Length < removed))
                    comparison.Remove(removed);

                if ((float)comparison.Length / ev.Player.Nickname.Length < Config.PercentageComparision) continue;
                if (!name.ToLower().Contains(comparison.ToLower())) continue;

                SetName(ev.Player);
                return;
            }
        }

        public void OnRestartingRound() => ResetValues();
    }
}
