using System;
using System.Threading.Tasks;
using Discord;
using Discord.WebSocket;

namespace RisuBump
{
    class Program
    {
        DiscordSocketClient _client;
        EventHandler _handler;

        static void Main() => new Program().StartAsync().GetAwaiter().GetResult();
        private async Task StartAsync() {
            if (string.IsNullOrWhiteSpace(Config.Bot.Token) || string.IsNullOrWhiteSpace(Config.Bot.DefaultPrefix)) 
                return;
                
            _client = new DiscordSocketClient(new DiscordSocketConfig {
                LogLevel = LogSeverity.Verbose
            });
            _client.Log += Log;
            await _client.LoginAsync(TokenType.Bot, Config.Bot.Token);
            _handler = new EventHandler();
            await _handler.InitializeAsync(_client);
            await _client.StartAsync();
            await Task.Delay(-1);
        }

        private async Task Log(LogMessage message) {
            Console.WriteLine($"[{message.Severity}] {message.Exception}\n{message.Message} \n(source: {message.Source})");
        }
    }
}
