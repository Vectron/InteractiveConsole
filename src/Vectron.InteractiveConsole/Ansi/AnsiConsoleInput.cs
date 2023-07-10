using Microsoft.Extensions.Options;
using Vectron.InteractiveConsole.Cursor;

namespace Vectron.InteractiveConsole.Ansi;

/// <summary>
/// A <see cref="IConsoleCursor"/> implementation for an ANSI capable console.
/// </summary>
internal sealed class AnsiConsoleInput : IConsoleInput
{
    private readonly IConsoleOutput consoleOutput;
    private readonly string inputColor;
    private readonly ConsoleInputOptions settings;
    private string currentInput = string.Empty;

    /// <summary>
    /// Initializes a new instance of the <see cref="AnsiConsoleInput"/> class.
    /// </summary>
    /// <param name="options">A <see cref="IOptions{TOptions}"/> containing the <see cref="ConsoleInputOptions"/>.</param>
    /// <param name="consoleOutput">A <see cref="IConsoleOutput"/> instance.</param>
    public AnsiConsoleInput(IOptions<ConsoleInputOptions> options, IConsoleOutput consoleOutput)
    {
        this.consoleOutput = consoleOutput;
        settings = options.Value;
        inputColor = AnsiHelper.GetAnsiEscapeCode(settings.InputTextColor);
        UpdateText();
    }

    /// <inheritdoc/>
    public string CurrentInput
    {
        get => currentInput;
        set
        {
            currentInput = value;
            UpdateText();
        }
    }

    private void UpdateText()
        => consoleOutput.StaticText = inputColor + settings.Marker + CurrentInput;
}
