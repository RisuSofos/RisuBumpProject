using Discord.WebSocket;

namespace RisuBump
{
    internal class Global {
        internal static DiscordSocketClient Client {get; set;}
        internal static ulong MessageIdTrack {get; set;}
    }
}