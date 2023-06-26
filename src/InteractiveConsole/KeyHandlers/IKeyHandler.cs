namespace InteractiveConsole.KeyHandlers;

/// <summary>
/// Class for handling key strokes.
/// </summary>
public interface IKeyHandler
{
    /// <summary>
    /// Handle the keystroke.
    /// </summary>
    /// <param name="keyInfo">The pressed key.</param>
    void Handle(ConsoleKeyInfo keyInfo);
}
