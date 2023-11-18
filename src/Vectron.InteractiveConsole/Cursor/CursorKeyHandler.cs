using Vectron.InteractiveConsole.KeyHandlers;

namespace Vectron.InteractiveConsole.Cursor;

/// <summary>
/// Handles the keys for moving the cursor.
/// </summary>
/// <remarks>
/// Initializes a new instance of the <see cref="CursorKeyHandler"/> class.
/// </remarks>
/// <param name="consoleInput">A <see cref="IConsoleInput"/> instance.</param>
/// <param name="consoleCursor">A <see cref="IConsoleCursor"/> instance.</param>
internal sealed class CursorKeyHandler(IConsoleInput consoleInput, IConsoleCursor consoleCursor) : IKeyHandler
{
    /// <inheritdoc/>
    public void Handle(ConsoleKeyInfo keyInfo)
    {
        if ((keyInfo.NoModifiersPressed() && keyInfo.Key is ConsoleKey.LeftArrow)
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

        if ((keyInfo.NoModifiersPressed() && keyInfo.Key is ConsoleKey.RightArrow)
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

        if ((keyInfo.NoModifiersPressed() && keyInfo.Key is ConsoleKey.Home)
            || (keyInfo.Modifiers == ConsoleModifiers.Control && keyInfo.Key == ConsoleKey.A))
        {
            consoleCursor.MoveStart();
            return;
        }

        if ((keyInfo.NoModifiersPressed() && keyInfo.Key is ConsoleKey.End)
            || (keyInfo.Modifiers == ConsoleModifiers.Control && keyInfo.Key == ConsoleKey.E))
        {
            consoleCursor.MoveEnd();
            return;
        }
    }
}
