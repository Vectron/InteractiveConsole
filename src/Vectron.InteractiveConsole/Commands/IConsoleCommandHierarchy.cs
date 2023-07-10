using System.Diagnostics.CodeAnalysis;

namespace Vectron.InteractiveConsole.Commands;

/// <summary>
/// A collection for holding <see cref="IConsoleCommand"/>.
/// </summary>
public interface IConsoleCommandHierarchy
{
    /// <summary>
    /// Get all the direct descendants for the given command arguments.
    /// </summary>
    /// <param name="commandArguments">The commands parts to look for.</param>
    /// <returns>An <see cref="IEnumerable{T}"/> of <see cref="IConsoleCommand"/>.</returns>
    IEnumerable<IConsoleCommand> GetDescendantsFor(string[] commandArguments);

    /// <summary>
    /// Get the command for the given key input.
    /// </summary>
    /// <param name="commandArguments">The user input to find the command for.</param>
    /// <param name="consoleCommand">The found command, <see langword="null"/> if none are found.</param>
    /// <returns><see langword="true"/> when a command is found, otherwise <see langword="false"/>.</returns>
    bool TryGetCommand(string[] commandArguments, [NotNullWhen(true)] out IConsoleCommand? consoleCommand);
}
