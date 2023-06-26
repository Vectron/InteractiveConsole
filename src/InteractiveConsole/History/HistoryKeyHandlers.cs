using InteractiveConsole.Cursor;
using InteractiveConsole.KeyHandlers;

namespace InteractiveConsole.History;

/// <summary>
/// Handles the keys for navigating the history.
/// </summary>
public class HistoryKeyHandlers : IKeyHandler
{
    private readonly IConsoleCursor consoleCursor;
    private readonly IConsoleInput consoleInput;
    private readonly IHistoryHandler historyHandler;

    /// <summary>
    /// Initializes a new instance of the <see cref="HistoryKeyHandlers"/> class.
    /// </summary>
    /// <param name="historyHandler">A <see cref="IHistoryHandler"/> instance.</param>
    /// <param name="consoleInput">A <see cref="IConsoleInput"/> instance.</param>
    /// <param name="consoleCursor">A <see cref="IConsoleCursor"/> instance.</param>
    public HistoryKeyHandlers(IHistoryHandler historyHandler, IConsoleInput consoleInput, IConsoleCursor consoleCursor)
    {
        this.historyHandler = historyHandler;
        this.consoleInput = consoleInput;
        this.consoleCursor = consoleCursor;
    }

    /// <inheritdoc/>
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE0072:Add missing cases", Justification = "Only subset of items is needed")]
    public void Handle(ConsoleKeyInfo keyInfo)
    {
        if (keyInfo.Key == ConsoleKey.Enter
            && keyInfo.Modifiers == 0)
        {
            historyHandler.AddEntry(consoleInput.CurrentInput);
            return;
        }

        var entry = keyInfo.Key switch
        {
            ConsoleKey.UpArrow when keyInfo.Modifiers == 0 => historyHandler.PreviousEntry(),
            ConsoleKey.P when keyInfo.Modifiers == ConsoleModifiers.Control => historyHandler.PreviousEntry(),
            ConsoleKey.DownArrow when keyInfo.Modifiers == 0 => historyHandler.NextEntry(),
            ConsoleKey.N when keyInfo.Modifiers == ConsoleModifiers.Control => historyHandler.NextEntry(),
            _ => null,
        };

        if (entry == null)
        {
            return;
        }

        consoleInput.CurrentInput = entry;
        consoleCursor.MoveEnd();
    }
}
