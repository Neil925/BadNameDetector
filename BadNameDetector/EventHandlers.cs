using System;
using System.Collections.Generic;
using System.Linq;
using Exiled.Events.EventArgs;
using static BadNameDetector.Handlers;

namespace BadNameDetector
{
    internal class EventHandlers
    {
        private static readonly Config _config = Plugin.Instance.Config;

        public void OnVerified(VerifiedEventArgs ev)
        {
            string name = ev.Player.Nickname;
            List<int> removedCharacters = new List<int>();

            if (_config.RemoveSpecialCharacters)
                RemoveSpecialCharacters(name, out name, out removedCharacters);

            if (_config.ReplaceNumbersWithWords)
                name = name.Replace("0", "o").Replace("9", "g").Replace("8", "b").Replace("7", "i").Replace("5", "s").Replace("4", "a").Replace("3", "e").Replace("1", "i");

            if (_config.PerfectBadNameComparisons.Contains(name, StringComparison.CurrentCultureIgnoreCase))
            {
                SetName(ev.Player);
                return;
            }

            if (_config.ContainsBadNameComparisons.Any(s =>
                name.ToLower().Contains(s.ToLower()) && !string.IsNullOrEmpty(name)))
            {
                SetName(ev.Player);
                return;
            }

            if (name.Length == ev.Player.Nickname.Length) return;

            foreach (var comparison in _config.PerfectBadNameComparisons)
            {
                foreach (var removed in removedCharacters)
                    comparison.Remove(removed);

                if ((float)comparison.Length / ev.Player.Nickname.Length < _config.PercentageComparision) continue;
                if (!string.Equals(comparison, name, StringComparison.CurrentCultureIgnoreCase)) continue;

                SetName(ev.Player);
                return;
            }

            foreach (var comparison in _config.ContainsBadNameComparisons)
            {
                foreach (var removed in removedCharacters)
                    comparison.Remove(removed);

                if ((float)comparison.Length / ev.Player.Nickname.Length < _config.PercentageComparision) continue;
                if (!name.ToLower().Contains(comparison.ToLower())) continue;

                SetName(ev.Player);
                return;
            }
        }

        public void OnRestartingRound() => ResetValues();
    }
}
