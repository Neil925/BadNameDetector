using System;
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

            if (_config.RemoveSpecialCharacters)
                name = name.RemoveSpecialCharacters();

            if (_config.ReplaceNumbersWithWords)
                name = name.Replace("0", "o").Replace("9", "g").Replace("8", "b").Replace("7", "i").Replace("5", "s").Replace("4", "a").Replace("3", "e").Replace("1", "i");

            if (_config.PerfectBadNameComparisons.Contains(name, StringComparison.CurrentCultureIgnoreCase))
            {
                SetName(ev.Player);
                return;
            }

            if (_config.ContainsBadNameComparisons.Any(s => s.ToLower().Contains(name.ToLower())))
                SetName(ev.Player);
        }

        public void OnRestartingRound() => ResetValues();
    }
}
