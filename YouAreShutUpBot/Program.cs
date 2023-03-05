// See https://aka.ms/new-console-template for more information

using Discord;
using Discord.Commands;
using Discord.WebSocket;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using YouAreShutUpBot.Common;
using YouAreShutUpBot.Init;
using YouAreShutUpBot.Services;

var config = new ConfigurationBuilder()
    //.AddJsonFile($"appsettings.json")
    .AddEnvironmentVariables()
    .Build();
var client = new DiscordShardedClient();


var commands = new CommandService(new CommandServiceConfig
{
    // Again, log level:
    LogLevel = LogSeverity.Info,

    // There's a few more properties you can set,
    // for example, case-insensitive commands.
    CaseSensitiveCommands = false,
});

// Setup your DI container.
Bootstrapper.Init();
Bootstrapper.RegisterInstance(client);
Bootstrapper.RegisterInstance(commands);
Bootstrapper.RegisterType<ICommandHandler, CommandHandler>();
Bootstrapper.RegisterInstance(config);

await MainAsync();

async Task MainAsync()
{
    await Bootstrapper.ServiceProvider.GetRequiredService<ICommandHandler>().InitializeAsync();

    client.ShardReady += async shard =>
    {
        await Logger.Log(LogSeverity.Info, "ShardReady", $"Shard Number {shard.ShardId} is connected and ready!");
    };

    // Login and connect.
    var token = Environment.GetEnvironmentVariable("DiscordBotToken");
    await Logger.Log(LogSeverity.Info, "Start!", $"Starting! {token}");
    Console.WriteLine($"Starting! {token}");
    if (string.IsNullOrWhiteSpace(token))
    {
        var tokenErrorMessage = "Token is null or empty.";
        await Logger.Log(LogSeverity.Error, $"{nameof(Program)} | {nameof(MainAsync)}", tokenErrorMessage);
        throw new ArgumentNullException(tokenErrorMessage);
    }

    await client.LoginAsync(TokenType.Bot, token);
    await client.StartAsync();
    await client.SetGameAsync("Firs simpit");
    // Wait infinitely so your bot actually stays connected.
    await Task.Delay(Timeout.Infinite);
}