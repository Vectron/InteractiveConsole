using System.Text;

namespace Vectron.InteractiveConsole.Commands;

/// <summary>
/// A <see cref="IConsoleCommand"/> that prints a help text.
/// </summary>
public sealed class HelpCommand : IConsoleCommand
{
    private readonly IEnumerable<IConsoleCommand> consoleCommands;

    /// <summary>
    /// Initializes a new instance of the <see cref="HelpCommand"/> class.
    /// </summary>
    /// <param name="consoleCommands">The commands to add to this collection.</param>
    public HelpCommand(IEnumerable<IConsoleCommand> consoleCommands)
        => this.consoleCommands = consoleCommands;

    /// <inheritdoc/>
    public string[]? ArgumentNames => null;

    /// <inheritdoc/>
    public string[] CommandParameters => new[] { "help" };

    /// <inheritdoc/>
    public string HelpText => "prints usages information";

    /// <inheritdoc/>
    public int MaxArguments => 0;

    /// <inheritdoc/>
    public int MinArguments => 0;

    /// <inheritdoc/>
    public void Execute(string[] arguments)
    {
        var builder = new StringBuilder();
        foreach (var command in consoleCommands)
        {
            _ = builder.AppendJoin(' ', command.CommandParameters)
                .Append(": ")
                .Append(command.HelpText)
                .AppendLine();
        }

        Console.WriteLine(builder.ToString());
    }
}
