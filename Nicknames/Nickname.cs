using CommandSystem;
using Exiled.API.Features;
using System;
using System.Text.RegularExpressions;

namespace Nicknames
{
    [CommandHandler(typeof(ClientCommandHandler))]
    internal class Nickname : ParentCommand
    {
        public Nickname() => LoadGeneratedCommands();
        public override string Command => "nickname";

        public override string[] Aliases => new string[] { "nick" };

        public override string Description => "Sets your nickname on this server. Put your nickname inside quotes.";

        public override void LoadGeneratedCommands()
        {
        }

        protected override bool ExecuteParent(ArraySegment<string> arguments, ICommandSender sender, out string response)
        {
            if (arguments.Count == 0)
            {
                response = ".nickname \"Your nickname here\".";
                return false;
            }
            var match = Regex.Match(string.Join(" ", arguments), "(?<=\").*(?=\")");
            var player = Player.Get(sender);
            if (!match.Success)
            {
                response = "Use quotes to encapsulate your nickname.";
            }
            else if (match.Value.Length > Main.Instance.Config.CharacterLimit)
            {
                response = "Nickname is too long.";
            }
            else if (string.IsNullOrWhiteSpace(match.Value))
            {
                response = "your nickname can't be empty.";
            }
            else if (player.DoNotTrack)
            {
                response = "You have DNT mode on in your settings.";
            }
            else
            {
                player.DisplayNickname = match.Value;
                if (!Main.nicknames.TryGetValue(player.UserId, out _))
                {
                    Main.nicknames.Add(player.UserId, match.Value);
                }
                else
                {
                    Main.nicknames[player.UserId] = match.Value;
                }
                Binary.WriteToBinaryFile(Main.path, Main.nicknames);
                response = $"Nickname set to {match.Value}";
                return true;
            }
            return false;
        }
    }
}
