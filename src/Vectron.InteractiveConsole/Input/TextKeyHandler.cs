using Vectron.InteractiveConsole.Cursor;
using Vectron.InteractiveConsole.KeyHandlers;

namespace Vectron.InteractiveConsole.Input;

/// <summary>
/// Handles the manipulating of text input.
/// </summary>
/// <remarks>
/// Initializes a new instance of the <see cref="TextKeyHandler"/> class.
/// </remarks>
/// <param name="consoleInput">A <see cref="IConsoleInput"/> instance.</param>
/// <param name="consoleCursor">A <see cref="IConsoleCursor"/> instance.</param>
internal sealed class TextKeyHandler(IConsoleInput consoleInput, IConsoleCursor consoleCursor) : IKeyHandler
{
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
