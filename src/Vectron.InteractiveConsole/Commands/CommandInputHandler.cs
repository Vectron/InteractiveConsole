using System.Globalization;
using Vectron.InteractiveConsole.Input;

namespace Vectron.InteractiveConsole.Commands;

/// <summary>
/// An input handler for processing commands.
/// </summary>
/// <remarks>
/// Initializes a new instance of the <see cref="CommandInputHandler"/> class.
/// </remarks>
/// <param name="consoleCommands">All the registered <see cref="IConsoleCommand"/>.</param>
internal sealed class CommandInputHandler(IConsoleCommandHierarchy consoleCommands) : IInputHandler
{
    /// <summary>
    /// The separator for command arguments.
    /// </summary>
    internal const char ArgumentSeparator = ' ';

    /// <inheritdoc/>
    public void ProcessInput(string input)
    {
        var inputArguments = input.Split(ArgumentSeparator);
        if (!consoleCommands.TryGetCommand(inputArguments, out var command))
        {
            Console.WriteLine("Invalid command.");
            return;
        }

        var commandArguments = inputArguments.Except(command.CommandParameters, StringComparer.OrdinalIgnoreCase).ToArray();
        if (commandArguments.Length < command.MinArguments)
        {
            Console.WriteLine($"Command needs at least {command.MinArguments.ToString(CultureInfo.CurrentCulture)} arguments");
            Console.WriteLine(command.HelpText);
            return;
        }

        if (commandArguments.Length > command.MaxArguments)
        {
            Console.WriteLine($"Command needs maximum of {command.MaxArguments.ToString(CultureInfo.CurrentCulture)} arguments");
            Console.WriteLine(command.HelpText);
            return;
        }

        command.Execute(commandArguments);
    }
}
