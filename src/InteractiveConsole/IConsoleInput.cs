namespace InteractiveConsole;

/// <summary>
/// Class that maintains the current console input.
/// </summary>
public interface IConsoleInput
{
    /// <summary>
    /// Gets or sets the current user input.
    /// </summary>
    string CurrentInput
    {
        get;
        set;
    }
}
