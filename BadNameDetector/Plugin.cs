using System;
using Exiled.API.Features;

using Player = Exiled.Events.Handlers.Player;
using Server = Exiled.Events.Handlers.Server;


namespace BadNameDetector
{
    public class Plugin : Plugin<Config>
    {
        public override string Name => "BadNameDetector";
        public override string Prefix => "bnd";
        public override string Author => "Neil";
        public override Version RequiredExiledVersion => new Version(4, 1, 5);

        public static Plugin Instance;
        private EventHandlers _handler;

        public override void OnEnabled()
        {
            Instance = this;

            _handler = new EventHandlers();

            Player.Verified += _handler.OnVerified;

            Server.RestartingRound += _handler.OnRestartingRound;

            base.OnEnabled();
        }

        public override void OnDisabled()
        {
            Player.Verified -= _handler.OnVerified;

            Server.RestartingRound -= _handler.OnRestartingRound;

            _handler = null;

            Instance = null;

            base.OnDisabled();
        }
    }
}