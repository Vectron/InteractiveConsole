using InteractiveConsole.Cursor;
using InteractiveConsole.KeyHandlers;

namespace InteractiveConsole.AutoComplete;

/// <summary>
/// Handles the keys for auto completion.
/// </summary>
internal sealed class AutoCompleteKeyHandler : IKeyHandler
{
    private readonly IAutoCompleteHandler autoCompleteHandler;
    private readonly IConsoleCursor consoleCursor;
    private readonly IConsoleInput consoleInput;
    private string rootText = string.Empty;
    private bool started;

    /// <summary>
    /// Initializes a new instance of the <see cref="AutoCompleteKeyHandler"/> class.
    /// </summary>
    /// <param name="autoCompleteHandler">A <see cref="IAutoCompleteHandler"/> instance.</param>
    /// <param name="consoleInput">A <see cref="IConsoleInput"/> instance.</param>
    /// <param name="consoleCursor">A <see cref="IConsoleCursor"/> instance.</param>
    public AutoCompleteKeyHandler(IAutoCompleteHandler autoCompleteHandler, IConsoleInput consoleInput, IConsoleCursor consoleCursor)
    {
        this.autoCompleteHandler = autoCompleteHandler;
        this.consoleInput = consoleInput;
        this.consoleCursor = consoleCursor;
    }

    /// <inheritdoc/>
    public void Handle(ConsoleKeyInfo keyInfo)
    {
        if (keyInfo.Key != ConsoleKey.Tab)
        {
            rootText = string.Empty;
            started = false;
            return;
        }

        if (!started)
        {
            rootText = consoleInput.CurrentInput;
            started = true;
        }

        var suggestion = keyInfo.Modifiers == ConsoleModifiers.Shift
            ? autoCompleteHandler.PreviousSuggestions(rootText)
            : autoCompleteHandler.NextSuggestions(rootText);

        consoleInput.CurrentInput = suggestion ?? rootText;
        consoleCursor.MoveEnd();
    }
}
