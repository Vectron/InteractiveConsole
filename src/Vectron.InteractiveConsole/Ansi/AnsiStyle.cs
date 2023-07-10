namespace Vectron.InteractiveConsole.Ansi;

/// <summary>
/// Flag for settings the text style.
/// </summary>
[Flags]
public enum AnsiStyle
{
    /// <summary>
    /// Make the text bold.
    /// </summary>
    Bold,

    /// <summary>
    /// Underline the text.
    /// </summary>
    Underlined,

    /// <summary>
    /// Switch foreground and background color.
    /// </summary>
    Reversed,
}
