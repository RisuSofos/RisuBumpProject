using System;
using System.Threading.Tasks;
using Discord.Commands;

namespace RisuBump.Commands
{
    public class GeneralCommands : ModuleBase<SocketCommandContext> {
        
        [Command("bump", true)]
        public async Task Bump() {
            await Context.Channel.SendMessageAsync("The developer has not completed this command");
        }
    }
}