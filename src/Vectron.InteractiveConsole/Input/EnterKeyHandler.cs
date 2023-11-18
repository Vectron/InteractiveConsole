using Vectron.InteractiveConsole.Cursor;
using Vectron.InteractiveConsole.KeyHandlers;

namespace Vectron.InteractiveConsole.Input;

/// <summary>
/// A key handler for the enter key to start processing the command.
/// </summary>
/// <remarks>
/// Initializes a new instance of the <see cref="EnterKeyHandler"/> class.
/// </remarks>
/// <param name="consoleInput">A <see cref="IConsoleInput"/> instance.</param>
/// <param name="consoleCursor">A <see cref="IConsoleCursor"/> instance.</param>
/// <param name="inputHandler">A <see cref="IInputHandler"/> instance.</param>
internal sealed class EnterKeyHandler(IConsoleInput consoleInput, IConsoleCursor consoleCursor, IInputHandler inputHandler) : IKeyHandler
{
    /// <inheritdoc/>
    public void Handle(ConsoleKeyInfo keyInfo)
    {
        if (keyInfo.Key != ConsoleKey.Enter)
        {
            return;
        }

        var input = consoleInput.CurrentInput;
        consoleInput.CurrentInput = string.Empty;
        consoleCursor.MoveStart();
        inputHandler.ProcessInput(input);
    }
}
