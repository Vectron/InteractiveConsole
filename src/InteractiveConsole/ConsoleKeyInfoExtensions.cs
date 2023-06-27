namespace InteractiveConsole;

/// <summary>
/// Extension methods for <see cref="ConsoleKeyInfo"/>.
/// </summary>
internal static class ConsoleKeyInfoExtensions
{
    /// <summary>
    /// Check if no modifiers are pressed.
    /// </summary>
    /// <param name="consoleKeyInfo">The pressed console key info.</param>
    /// <returns><see langword="true"/> when no modifiers are pressed, otherwise <see langword="false"/>.</returns>
    public static bool NoModifiersPressed(this ConsoleKeyInfo consoleKeyInfo)
        => !consoleKeyInfo.Modifiers.HasFlag(ConsoleModifiers.Control)
            && !consoleKeyInfo.Modifiers.HasFlag(ConsoleModifiers.Shift)
            && !consoleKeyInfo.Modifiers.HasFlag(ConsoleModifiers.Alt);
}
