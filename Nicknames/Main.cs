using Exiled.API.Features;
using System;
using Player = Exiled.Events.Handlers.Player;
using System.Collections.Generic;
using System.IO;

namespace Nicknames
{
    public class Main : Plugin<Config>
    {
        EventHandlers handlers = new();
        public static Dictionary<string, string> nicknames;
        public static Main Instance { get; private set; }
        public static string path = Environment.ExpandEnvironmentVariables(@"%appdata%\EXILED\Configs\nicknames.bin");

        private void GetOrCreateBinaryFile()
        {
            if (!File.Exists(path))
            {
                nicknames = new();
                Binary.WriteToBinaryFile(path, nicknames);
                return;
            }
            nicknames = Binary.ReadFromBinaryFile<Dictionary<string, string>>(path);
        }

        public override void OnEnabled()
        {
            Instance = this;
            handlers = new();
            Player.Verified += handlers.OnVerified;
            GetOrCreateBinaryFile();
            base.OnEnabled();
        }

        public override void OnDisabled()
        {
            handlers = null;
            Player.Verified -= handlers.OnVerified;
            base.OnDisabled();
        }
    }
}