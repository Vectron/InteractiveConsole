using Vectron.InteractiveConsole.Cursor;
using Vectron.InteractiveConsole.KeyHandlers;

namespace Vectron.InteractiveConsole.AutoComplete;

/// <summary>
/// Handles the keys for auto completion.
/// </summary>
internal sealed class AutoCompleteKeyHandler : IKeyHandler
{
    private readonly IEnumerable<IAutoCompleteHandler> autoCompleteHandlers;
    private readonly IConsoleCursor consoleCursor;
    private readonly IConsoleInput consoleInput;
    private string rootText = string.Empty;
    private bool started;

    /// <summary>
    /// Initializes a new instance of the <see cref="AutoCompleteKeyHandler"/> class.
    /// </summary>
    /// <param name="autoCompleteHandlers">A <see cref="IAutoCompleteHandler"/> instance.</param>
    /// <param name="consoleInput">A <see cref="IConsoleInput"/> instance.</param>
    /// <param name="consoleCursor">A <see cref="IConsoleCursor"/> instance.</param>
    public AutoCompleteKeyHandler(IEnumerable<IAutoCompleteHandler> autoCompleteHandlers, IConsoleInput consoleInput, IConsoleCursor consoleCursor)
    {
        this.autoCompleteHandlers = autoCompleteHandlers;
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

        string? suggestion = null;
        foreach (var handler in autoCompleteHandlers)
        {
            suggestion = keyInfo.Modifiers == ConsoleModifiers.Shift
                ? handler.PreviousSuggestions(rootText)
                : handler.NextSuggestions(rootText);

            if (suggestion != null)
            {
                break;
            }
        }

        consoleInput.CurrentInput = suggestion ?? rootText;
        consoleCursor.MoveEnd();
    }
}
