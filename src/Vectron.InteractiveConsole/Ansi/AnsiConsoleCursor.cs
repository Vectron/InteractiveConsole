using Vectron.Ansi;
using Vectron.InteractiveConsole.Cursor;

namespace Vectron.InteractiveConsole.Ansi;

/// <summary>
/// A <see cref="IConsoleCursor"/> implementation for an ANSI capable console.
/// </summary>
internal sealed class AnsiConsoleCursor : IConsoleCursor
{
    private readonly IConsoleInput consoleInput;
    private readonly IConsoleOutput consoleOutput;
    private int cursorIndex;

    /// <summary>
    /// Initializes a new instance of the <see cref="AnsiConsoleCursor"/> class.
    /// </summary>
    /// <param name="consoleInput">A <see cref="IConsoleInput"/> instance.</param>
    /// <param name="consoleOutput">A <see cref="IConsoleOutput"/> instance.</param>
    public AnsiConsoleCursor(IConsoleInput consoleInput, IConsoleOutput consoleOutput)
    {
        this.consoleInput = consoleInput;
        this.consoleOutput = consoleOutput;
    }

    /// <inheritdoc/>
    public int CursorIndex
    {
        get => cursorIndex;
        set
        {
            cursorIndex = Math.Min(Math.Max(0, value), consoleInput.CurrentInput.Length);
            UpdateCursorPosition();
        }
    }

    /// <inheritdoc/>
    public void MoveBackward(int amount) => CursorIndex -= amount;

    /// <inheritdoc/>
    public void MoveEnd() => CursorIndex = consoleInput.CurrentInput.Length;

    /// <inheritdoc/>
    public void MoveForward(int amount) => CursorIndex += amount;

    /// <inheritdoc/>
    public void MoveStart() => CursorIndex = 0;

    private void UpdateCursorPosition()
    {
        var inputLength = consoleInput.CurrentInput.Length;
        var newStaticText = AnsiHelper.RemoveAnsiCursorCode(consoleOutput.StaticText);
        if (CursorIndex >= inputLength || inputLength == 0)
        {
            consoleOutput.StaticText = newStaticText;
            return;
        }

        // calculate how many positions we need to move to the left
        var endOffset = inputLength - CursorIndex;
        var (lines, characters) = Math.DivRem(endOffset, Console.WindowWidth);

        // when the remainder is 0 we are at the end of a line. this means the cursor did not wrap yet
        if (characters == 0)
        {
            lines -= 1;
            characters = Console.WindowWidth;
        }

        var cursorMoveLeft = AnsiHelper.GetAnsiEscapeCode(AnsiCursorDirection.Left, characters);
        var cursorMoveUp = AnsiHelper.GetAnsiEscapeCode(AnsiCursorDirection.Up, lines);
        consoleOutput.StaticText = newStaticText + cursorMoveLeft + cursorMoveUp;
    }
}
