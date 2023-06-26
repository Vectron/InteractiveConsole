using InteractiveConsole.Cursor;
using InteractiveConsole.KeyHandlers;

namespace InteractiveConsole.Input;

/// <summary>
/// Handles the manipulating of text input.
/// </summary>
public class TextKeyHandler : IKeyHandler
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
        if (keyInfo.Key == ConsoleKey.Backspace && keyInfo.Modifiers == 0)
        {
            BackSpace();
            return;
        }

        if (keyInfo.Key == ConsoleKey.H && keyInfo.Modifiers == ConsoleModifiers.Control)
        {
            BackSpace();
            return;
        }

        if (keyInfo.Key == ConsoleKey.Delete && keyInfo.Modifiers == 0)
        {
            Delete();
            return;
        }

        if (keyInfo.Key == ConsoleKey.D && keyInfo.Modifiers == ConsoleModifiers.Control)
        {
            Delete();
            return;
        }

        if (keyInfo.Key == ConsoleKey.Escape && keyInfo.Modifiers == 0)
        {
            ClearLine();
            return;
        }

        if (keyInfo.Key == ConsoleKey.L && keyInfo.Modifiers == ConsoleModifiers.Control)
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
