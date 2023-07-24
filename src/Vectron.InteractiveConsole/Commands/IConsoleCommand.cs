namespace Vectron.InteractiveConsole.Commands;

/// <summary>
/// A interface describing a command that can be executed.
/// </summary>
public interface IConsoleCommand
{
    /// <summary>
    /// Gets a collection of names for the arguments.
    /// </summary>
    string[]? ArgumentNames
    {
        get;
    }

    /// <summary>
    /// Gets the command parameters.
    /// </summary>
    string[] CommandParameters
    {
        get;
    }

    /// <summary>
    /// Gets text explaining this <see cref="IConsoleCommand"/>.
    /// </summary>
    string HelpText
    {
        get;
    }

    /// <summary>
    /// Gets the maximum arguments needed to run this command.
    /// </summary>
    int MaxArguments
    {
        get;
    }

    /// <summary>
    /// Gets the minimum arguments needed to run this command.
    /// </summary>
    int MinArguments
    {
        get;
    }

    /// <summary>
    /// Execute this command.
    /// </summary>
    /// <param name="arguments">Collection of arguments for this command.</param>
    void Execute(string[] arguments);
}
