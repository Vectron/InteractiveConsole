using Vectron.Ansi;
using Vectron.InteractiveConsole.Ansi;

namespace Vectron.InteractiveConsole;

/// <summary>
/// Options for <see cref="AnsiConsoleInput"/>.
/// </summary>
public class ConsoleInputOptions
{
    /// <summary>
    /// Initializes a new instance of the <see cref="ConsoleInputOptions"/> class.
    /// </summary>
    public ConsoleInputOptions()
        => Marker = string.Empty;

    /// <summary>
    /// Gets or sets the color to write to the console with.
    /// </summary>
    public AnsiColor InputTextColor
    {
        get;
        set;
    }

    /// <summary>
    /// Gets or sets the marker to show before the console input.
    /// </summary>
    public string Marker
    {
        get;
        set;
    }
}
