using InteractiveConsole.Cursor;
using InteractiveConsole.KeyHandlers;

namespace InteractiveConsole.Input;

/// <summary>
/// Handles the manipulating of text input.
/// </summary>
internal sealed class TextKeyHandler : IKeyHandler
{
    private readonly IConsoleCursor consoleCursor;
    private readonly IConsoleInput consoleInput;

    /// <summary>
    /// Initializes a new instance of the <see cref="TextKeyHandler"/> class.
    /// </summary>
    /// <param name="consoleInput">A <see cref="IConsoleInput"/> instance.</param>
    /// <param name="consoleCursor">A <see cref="IConsoleCursor"/> instance.</param>
    public TextKeyHandler(IConsoleInput consoleInput, IConsoleCursor consoleCursor)
    {
        this.consoleInput = consoleInput;
        this.consoleCursor = consoleCursor;
    }

    /// <inheritdoc/>
    public void Handle(ConsoleKeyInfo keyInfo)
    {
        if (keyInfo.NoModifiersPressed()
            && keyInfo.Key == ConsoleKey.Backspace)
        {
            BackSpace();
            return;
        }

        if (keyInfo.Modifiers == ConsoleModifiers.Control
            && keyInfo.Key == ConsoleKey.H)
        {
            BackSpace();
            return;
        }

        if (keyInfo.NoModifiersPressed()
            && keyInfo.Key == ConsoleKey.Delete)
        {
            Delete();
            return;
        }

        if (keyInfo.Modifiers == ConsoleModifiers.Control
            && keyInfo.Key == ConsoleKey.D)
        {
            Delete();
            return;
        }

        if (keyInfo.NoModifiersPressed()
            && keyInfo.Key == ConsoleKey.Escape)
        {
            ClearLine();
            return;
        }

        if (keyInfo.Modifiers == ConsoleModifiers.Control
            && keyInfo.Key == ConsoleKey.L)
        {
            ClearLine();
            return;
        }

        if (keyInfo.KeyChar is >= (char)32 and <= (char)126)
        {
            var input = consoleInput.CurrentInput;
            if (consoleCursor.CursorIndex >= input.Length)
            {
                consoleInput.CurrentInput += keyInfo.KeyChar;
                consoleCursor.MoveForward(1);
                return;
            }

            consoleInput.CurrentInput = input.Insert(consoleCursor.CursorIndex, keyInfo.KeyChar.ToString());
            consoleCursor.MoveForward(1);
        }
    }

    private void BackSpace()
    {
        consoleCursor.MoveBackward(1);
        Delete();
    }

    private void ClearLine()
    {
        consoleCursor.MoveStart();
        consoleInput.CurrentInput = string.Empty;
    }

    private void Delete()
    {
        var input = consoleInput.CurrentInput;
        if (consoleCursor.CursorIndex >= input.Length)
        {
            return;
        }

        consoleInput.CurrentInput = input.Remove(consoleCursor.CursorIndex, 1);
        consoleCursor.MoveBackward(0);
    }
}
