using Vectron.InteractiveConsole.Cursor;
using Vectron.InteractiveConsole.KeyHandlers;

namespace Vectron.InteractiveConsole.AutoComplete;

/// <summary>
/// Handles the keys for auto completion.
/// </summary>
/// <remarks>
/// Initializes a new instance of the <see cref="AutoCompleteKeyHandler"/> class.
/// </remarks>
/// <param name="autoCompleteHandlers">A <see cref="IAutoCompleteHandler"/> instance.</param>
/// <param name="consoleInput">A <see cref="IConsoleInput"/> instance.</param>
/// <param name="consoleCursor">A <see cref="IConsoleCursor"/> instance.</param>
internal sealed class AutoCompleteKeyHandler(IEnumerable<IAutoCompleteHandler> autoCompleteHandlers, IConsoleInput consoleInput, IConsoleCursor consoleCursor) : IKeyHandler
{
    private string rootText = string.Empty;
    private bool started;

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
