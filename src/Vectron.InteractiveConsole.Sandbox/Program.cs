using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Vectron.InteractiveConsole;
using Vectron.InteractiveConsole.Commands;
using Vectron.InteractiveConsole.Sandbox.Ads;
using Vectron.InteractiveConsole.Sandbox.Opc;

await Host
    .CreateDefaultBuilder()
    .ConfigureServices(ConfigureServices)
    .UseConsoleLifetime()
    .Build()
    .RunAsync()
    .ConfigureAwait(false);

static void ConfigureServices(HostBuilderContext context, IServiceCollection collection)
    => collection
    .AddInteractiveConsole()
    .AddConsoleCommand()
    .AddScoped<IConsoleCommand, AdsConnectCommand>()
    .AddScoped<IConsoleCommand, AdsDisconnectCommand>()
    .AddScoped<IConsoleCommand, AdsReadCommand>()
    .AddScoped<IConsoleCommand, AdsWriteCommand>()
    .AddScoped<IConsoleCommand, AdsMonitorCommand>()
    .AddScoped<IConsoleCommand, AdsUnmonitorCommand>()
    .AddScoped<IConsoleCommand, OpcConnectCommand>()
    .AddScoped<IConsoleCommand, OpcDisconnectCommand>()
    .AddScoped<IConsoleCommand, OpcReadCommand>()
    .AddScoped<IConsoleCommand, OpcWriteCommand>()
    .AddScoped<IConsoleCommand, OpcMonitorCommand>()
    .AddScoped<IConsoleCommand, OpcUnmonitorCommand>();
