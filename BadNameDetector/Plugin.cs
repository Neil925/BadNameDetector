using System;
using Exiled.API.Features;

using Player = Exiled.Events.Handlers.Player;

namespace BadNameDetector
{
    public class Plugin : Plugin<Config>
    {
        public override string Name => "BadNameDetector";
        public override string Prefix => "bnd";
        public override string Author => "Neil";
        public override Version RequiredExiledVersion => new Version(3, 2, 1);

        public static Plugin Instance;
        private EventHandlers _handler;

        public override void OnEnabled()
        {
            Instance = this;

            _handler = new EventHandlers();

            Player.Verified += _handler.OnVerified;

            base.OnEnabled();
        }

        public override void OnDisabled()
        {
            Player.Verified -= _handler.OnVerified;

            _handler = null;

            Instance = null;

            base.OnDisabled();
        }
    }
}