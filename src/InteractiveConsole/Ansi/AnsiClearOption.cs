namespace InteractiveConsole.Ansi;

/// <summary>
/// Possible ANSI clear commands.
/// </summary>
public enum AnsiClearOption
{
    /// <summary>
    /// Clears from cursor to end of screen.
    /// </summary>
    CursorToEndOfScreen,

    /// <summary>
    /// Clears from cursor to start of screen.
    /// </summary>
    CursorToEndStartOfScreen,

    /// <summary>
    /// Clear the entire screen.
    /// </summary>
    EntireScreen,

    /// <summary>
    /// Clears from cursor to end of line.
    /// </summary>
    CursorToEndOfLine,

    /// <summary>
    /// Clears from cursor to start of line.
    /// </summary>
    CursorToEndStartOfLine,

    /// <summary>
    /// Clear the entire line.
    /// </summary>
    EntireLine,
}
