using System;
using System.Threading.Tasks;
using Discord.WebSocket;
using Discord.Commands;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace RisuBump {
    class EventHandler {
        DiscordSocketClient _client;
        CommandService _commands;
        IServiceProvider _service;

        public async Task InitializeAsync(DiscordSocketClient client) {
            _client = client;
            _commands = new CommandService();
            _service = new ServiceCollection()
                .AddSingleton(_client)
                .AddSingleton(_commands)
                .BuildServiceProvider();

            #region Client Event Handlers
                _client.MessageReceived += HandleMessagesAsync;
            #endregion

            await _commands.AddModulesAsync(Assembly.GetEntryAssembly(), _service);
        }

        private async Task HandleMessagesAsync(SocketMessage message) {
            if (message.Author.IsBot || message.Author.IsWebhook) return;

            SocketUserMessage uMessage = (SocketUserMessage)message;
            SocketCommandContext context = new SocketCommandContext(_client, uMessage);

            int argPos = 0;
            if (uMessage.HasMentionPrefix(context.Client.CurrentUser, ref argPos ) || uMessage.HasStringPrefix(Config.Bot.Token, ref argPos)) await RunCommand(context, argPos);
        }

        private async Task RunCommand(SocketCommandContext context, int argPos) {
            var result = await _commands.ExecuteAsync(context, argPos, _service, MultiMatchHandling.Best);
            if (!result.IsSuccess && result.Error != CommandError.UnknownCommand) Console.WriteLine($"[Error]\n{result.ErrorReason}");
        }
    }
}