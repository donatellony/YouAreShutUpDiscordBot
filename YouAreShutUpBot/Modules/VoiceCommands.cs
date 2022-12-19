using Discord;
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
    public class VoiceCommands : ModuleBase<ShardedCommandContext>
    {
        [Command("muteDenis", RunMode = RunMode.Async)]
        public async Task MuteDenis()
        {
            Random rand = new();
            int number = rand.Next(0, 101);

            var denis = Context.Guild.GetUser(Consts.DenisId);
            bool selfMute = Context.Message.Author.Id == Consts.DenisId;
            bool isDenisOnline = denis?.VoiceChannel != null;

            if (denis is not null && (selfMute || (isDenisOnline && number <= 50))) // ne zarandomilo ili denis ne w seti
            {
                if (!denis.IsMuted)
                {
                    await Context.Message.ReplyAsync($"Zakroi ebalo {denis.Mention}");
                }
                else
                {
                    await Context.Message.ReplyAsync($"Otkroi ebalo {denis.Mention}");
                }

                await denis.ModifyAsync(func =>
                {
                    func.Mute = !denis.IsMuted;
                });
            }
            else
            {
                var kickMessage = "Ne powezlo, eblan";
                var trigger = Context.Guild.GetUser(Context.Message.Author.Id);
                await Context.Message.ReplyAsync($"{kickMessage} {Context.Message.Author.Mention}");
                await trigger.ModifyAsync(func =>
                {
                    func.Channel = null;
                });
            }
        }
    }
}
