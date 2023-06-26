using InteractiveConsole.Ansi;
using InteractiveConsole.AutoComplete;
using InteractiveConsole.Commands;
using InteractiveConsole.Cursor;
using InteractiveConsole.History;
using InteractiveConsole.Input;
using InteractiveConsole.KeyHandlers;
using Microsoft.Extensions.DependencyInjection;

namespace InteractiveConsole;

/// <summary>
/// Extension methods for adding services to an <see cref="IServiceCollection"/>.
/// </summary>
public static class ServiceCollectionExtensions
{
    /// <summary>
    /// Adds the services needed for running interactive console with commands.
    /// </summary>
    /// <param name="services">The <see cref="IServiceCollection"/> to add the service to.</param>
    /// <returns>A reference to this instance after the operation has completed.</returns>
    public static IServiceCollection AddConsoleCommand(this IServiceCollection services)
        => services
            .AddInteractiveConsole()
            .AddSingleton<IAutoCompleteHandler, CommandAutoCompleteHandler>()
            .AddSingleton<IInputHandler, CommandInputHandler>()
            .AddScoped<IConsoleCommandCollection, ConsoleCommandCollection>();

    /// <summary>
    /// Adds the services needed for running interactive console.
    /// </summary>
    /// <param name="services">The <see cref="IServiceCollection"/> to add the service to.</param>
    /// <returns>A reference to this instance after the operation has completed.</returns>
    public static IServiceCollection AddInteractiveConsole(this IServiceCollection services)
        => services
            .AddSingleton<IConsoleInput, AnsiConsoleInput>()
            .AddSingleton<IConsoleOutput>(new AnsiConsoleOutput())
            .AddSingleton<IConsoleCursor, AnsiConsoleCursor>()
            .AddSingleton<IHistoryHandler, HistoryHandler>()
            .AddHostedService<ConsoleInputHost>()
            .AddSingleton<IKeyHandler, AutoCompleteKeyHandler>()
            .AddSingleton<IKeyHandler, CursorKeyHandler>()
            .AddSingleton<IKeyHandler, HistoryKeyHandlers>()
            .AddSingleton<IKeyHandler, TextKeyHandler>()
            .AddSingleton<IKeyHandler, EnterKeyHandler>()
            .ConfigureOptions<ConsoleInputOptionsDefaults>()
        ;
}
