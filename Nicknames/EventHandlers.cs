using System.Collections.Generic;
using Exiled.Events.EventArgs;

namespace Nicknames
{
    internal class EventHandlers
    {
        public void OnVerified(VerifiedEventArgs ev)
        {
            if (Main.nicknames.TryGetValue(ev.Player.UserId, out var nickname))
            {
                ev.Player.DisplayNickname = nickname;
            }
        }
    }
}
