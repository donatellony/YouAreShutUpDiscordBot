using Discord.Commands;
using Discord.WebSocket;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YouAreShutUpBot.Common;

namespace YouAreShutUpBot.Modules
{
    public class BaseCommands : ModuleBase<ShardedCommandContext>
    {
        public SocketGuildUser? GetSocketGuildUserById(ulong id)
        {
            return Context.Guild.GetUser(id);
        }
    }
}
