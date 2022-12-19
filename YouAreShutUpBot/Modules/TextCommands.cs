using Discord;
using Discord.Commands;
using System.Text;
using YouAreShutUpBot.Common;
using RunMode = Discord.Commands.RunMode;

namespace YouAreShutUpBot.Modules;

public class TextCommands : ModuleBase<ShardedCommandContext>
{
    public CommandService CommandService { get; set; }

    [Command("hello", RunMode = RunMode.Async)]
    public async Task Hello()
    {
        await Context.Message.ReplyAsync($"Hello {Context.User.Username}. Nice to meet you!");
    }

    [Command("denis", RunMode = RunMode.Async)]
    public async Task Denis()
    {
        await Context.Message.ReplyAsync($"Hello {Context.User.Username}. I do agree that <@{Consts.DenisId}> is a bitch!");
    }

    [Command("simpReminder", RunMode = RunMode.Async)]
    public async Task SimpReminder()
    {
        await Context.Message.ReplyAsync($"Yes, {Context.User.Mention}. I do agree that <@{Consts.FirsId}> is a simp!");
        var firs = Context.Client.GetUser(Consts.FirsId);

        if (firs is null) return;

        string simpTxt = "Ty simp!!!";

        StringBuilder textToSend = new StringBuilder();
        for (int i = 0; i < simpTxt.Length; i++)
        {
            textToSend.Append(simpTxt[i]);
            await firs.SendMessageAsync(textToSend.ToString());
        }

        textToSend.Clear();

        for (int i = simpTxt.Length - 1; i > 0; i--)
        {
            await firs.SendMessageAsync(simpTxt.Substring(0, i));
        }

    }

}