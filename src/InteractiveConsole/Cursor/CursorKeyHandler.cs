using InteractiveConsole.KeyHandlers;

namespace InteractiveConsole.Cursor;

/// <summary>
/// Handles the keys for moving the cursor.
/// </summary>
public class CursorKeyHandler : IKeyHandler
{
    private readonly IConsoleCursor consoleCursor;
    private readonly IConsoleInput consoleInput;

    /// <summary>
    /// Initializes a new instance of the <see cref="CursorKeyHandler"/> class.
    /// </summary>
    /// <param name="consoleInput">A <see cref="IConsoleInput"/> instance.</param>
    /// <param name="consoleCursor">A <see cref="IConsoleCursor"/> instance.</param>
    public CursorKeyHandler(IConsoleInput consoleInput, IConsoleCursor consoleCursor)
    {
        this.consoleInput = consoleInput;
        this.consoleCursor = consoleCursor;
    }

    /// <inheritdoc/>
    public void Handle(ConsoleKeyInfo keyInfo)
    {
        if ((keyInfo.Modifiers == 0 && keyInfo.Key is ConsoleKey.LeftArrow)
            || (keyInfo.Modifiers == ConsoleModifiers.Control && keyInfo.Key == ConsoleKey.B))
        {
            consoleCursor.MoveBackward(1);
            return;
        }

        if (keyInfo.Modifiers == ConsoleModifiers.Control && keyInfo.Key is ConsoleKey.LeftArrow)
        {
            var input = consoleInput.CurrentInput.AsSpan();
            var remainingInput = input[..consoleCursor.CursorIndex];
            var nextSpace = remainingInput.LastIndexOf(' ');
            consoleCursor.CursorIndex = Math.Max(nextSpace - 1, 0);
            return;
        }

        if ((keyInfo.Modifiers == 0 && keyInfo.Key is ConsoleKey.RightArrow)
            || (keyInfo.Modifiers == ConsoleModifiers.Control && keyInfo.Key == ConsoleKey.F))
        {
            consoleCursor.MoveForward(1);
            return;
        }

        if (keyInfo.Modifiers == ConsoleModifiers.Control && keyInfo.Key is ConsoleKey.RightArrow)
        {
            var input = consoleInput.CurrentInput.AsSpan();
            var remainingInput = input[consoleCursor.CursorIndex..];
            var nextSpace = remainingInput.LastIndexOf(' ');
            consoleCursor.CursorIndex += Math.Max(nextSpace + 1, 0);
            return;
        }

        if ((keyInfo.Modifiers == 0 && keyInfo.Key is ConsoleKey.Home)
            || (keyInfo.Modifiers == ConsoleModifiers.Control && keyInfo.Key == ConsoleKey.A))
        {
            consoleCursor.MoveStart();
            return;
        }

        if ((keyInfo.Modifiers == 0 && keyInfo.Key is ConsoleKey.End)
            || (keyInfo.Modifiers == ConsoleModifiers.Control && keyInfo.Key == ConsoleKey.E))
        {
            consoleCursor.MoveEnd();
            return;
        }
    }
}
