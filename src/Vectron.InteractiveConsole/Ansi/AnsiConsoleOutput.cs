using System.Buffers;
using System.Text;
using Vectron.Ansi;

namespace Vectron.InteractiveConsole.Ansi;

/// <summary>
/// A wrapper over the console output.
/// </summary>
internal sealed class AnsiConsoleOutput : TextWriter, IConsoleOutput, IDisposable
{
    private static readonly Lazy<AnsiConsoleOutput> LazyInstance = new(() => new AnsiConsoleOutput());

    private readonly TextWriter stdOut;
    private readonly ReaderWriterLockSlim textLock = new(LockRecursionPolicy.SupportsRecursion);
    private string sanitizedText = string.Empty;
    private string staticText = string.Empty;

    /// <summary>
    /// Initializes a new instance of the <see cref="AnsiConsoleOutput"/> class.
    /// </summary>
    private AnsiConsoleOutput()
    {
        stdOut = Console.Out;
        Console.SetOut(Synchronized(this));
    }

    /// <summary>
    /// Gets the singleton instance of this class.
    /// </summary>
    public static AnsiConsoleOutput Instance => LazyInstance.Value;

    /// <inheritdoc/>
    public override Encoding Encoding
        => stdOut.Encoding;

    /// <inheritdoc/>
    public string StaticText
    {
        get
        {
            textLock.EnterReadLock();
            try
            {
                return staticText;
            }
            finally
            {
                textLock.ExitReadLock();
            }
        }

        set
        {
            textLock.EnterWriteLock();
            try
            {
                var cursorReset = GetCursorReset();
                var resetCode = AnsiHelper.ResetColorAndStyleAnsiEscapeCode;
                staticText = value;
                sanitizedText = staticText.RemoveAnsiCodes();
                stdOut.Write(cursorReset + staticText + resetCode);
            }
            finally
            {
                textLock.ExitWriteLock();
            }
        }
    }

    /// <inheritdoc/>
    public override ValueTask DisposeAsync()
    {
        Console.SetOut(stdOut);
        textLock.Dispose();
        return base.DisposeAsync();
    }

    /// <inheritdoc/>
    public override void Write(char value)
    {
        textLock.EnterReadLock();
        try
        {
            var cursorReset = GetCursorReset();
            var resetCode = AnsiHelper.ResetColorAndStyleAnsiEscapeCode;

            stdOut.Write(cursorReset + value + resetCode + staticText + resetCode);
        }
        finally
        {
            textLock.ExitReadLock();
        }
    }

    /// <inheritdoc/>
    public override void Write(char[] buffer, int index, int count)
    {
        textLock.EnterReadLock();
        try
        {
            var cursorReset = GetCursorReset();
            var resetCode = AnsiHelper.ResetColorAndStyleAnsiEscapeCode;

            var writeLength = cursorReset.Length
                + buffer.Length
                + resetCode.Length
                + staticText.Length
                + resetCode.Length;
            var stagingBuffer = ArrayPool<char>.Shared.Rent(writeLength);

            try
            {
                var stagingBufferSpan = stagingBuffer.AsSpan();
                var start = 0;
                cursorReset.AsSpan().CopyTo(stagingBufferSpan[start..cursorReset.Length]);
                start += cursorReset.Length;
                buffer.AsSpan().CopyTo(stagingBufferSpan[start..]);
                start += buffer.Length;
                resetCode.AsSpan().CopyTo(stagingBufferSpan[start..]);
                start += resetCode.Length;
                staticText.AsSpan().CopyTo(stagingBufferSpan[start..]);
                start += staticText.Length;
                resetCode.AsSpan().CopyTo(stagingBufferSpan[start..]);

                stdOut.Write(stagingBuffer, 0, writeLength);
            }
            finally
            {
                ArrayPool<char>.Shared.Return(stagingBuffer);
            }
        }
        finally
        {
            textLock.ExitReadLock();
        }
    }

    /// <inheritdoc/>
    public override void WriteLine(string? value)
        => Write(value + Environment.NewLine);

    /// <inheritdoc/>
    protected override void Dispose(bool disposing)
    {
        if (disposing)
        {
            textLock.Dispose();
        }

        base.Dispose(disposing);
    }

    private string GetCursorReset()
    {
        textLock.EnterReadLock();
        try
        {
            if (sanitizedText.Length <= 0)
            {
                return string.Empty;
            }

            var (quotient, remainder) = Math.DivRem(sanitizedText.Length, Console.WindowWidth);
            if (remainder == 0)
            {
                quotient -= 1;
                remainder = Console.WindowWidth;
            }

            var cursorMoveLeft = AnsiHelper.GetAnsiEscapeCode(AnsiCursorDirection.Left, remainder);
            var cursorMoveUp = AnsiHelper.GetAnsiEscapeCode(AnsiCursorDirection.Up, quotient);
            var clearTextEscapeCode = AnsiHelper.GetAnsiEscapeCode(AnsiClearOption.CursorToEndOfScreen);
            return cursorMoveLeft + cursorMoveUp + clearTextEscapeCode;
        }
        finally
        {
            textLock.ExitReadLock();
        }
    }
}
