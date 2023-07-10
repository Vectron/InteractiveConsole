using Vectron.InteractiveConsole.Cursor;
using Vectron.InteractiveConsole.KeyHandlers;

namespace Vectron.InteractiveConsole.Input;

/// <summary>
/// A key handler for the enter key to start processing the command.
/// </summary>
internal sealed class EnterKeyHandler : IKeyHandler
{
    private readonly IConsoleCursor consoleCursor;
    private readonly IConsoleInput consoleInput;
    private readonly IInputHandler inputHandler;

    /// <summary>
    /// Initializes a new instance of the <see cref="EnterKeyHandler"/> class.
    /// </summary>
    /// <param name="consoleInput">A <see cref="IConsoleInput"/> instance.</param>
    /// <param name="consoleCursor">A <see cref="IConsoleCursor"/> instance.</param>
    /// <param name="inputHandler">A <see cref="IInputHandler"/> instance.</param>
    public EnterKeyHandler(IConsoleInput consoleInput, IConsoleCursor consoleCursor, IInputHandler inputHandler)
    {
        this.consoleInput = consoleInput;
        this.consoleCursor = consoleCursor;
        this.inputHandler = inputHandler;
    }

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
