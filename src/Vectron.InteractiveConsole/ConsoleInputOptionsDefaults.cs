using Microsoft.Extensions.Options;
using Vectron.Ansi;

namespace Vectron.InteractiveConsole;

/// <summary>
/// A <see cref="IConfigureOptions{TOptions}"/> for settings the default values on.
/// </summary>
public class ConsoleInputOptionsDefaults : IConfigureOptions<ConsoleInputOptions>
{
    /// <inheritdoc/>
    public void Configure(ConsoleInputOptions options)
    {
        options.Marker = ">>>";
        options.InputTextColor = AnsiColor.Cyan;
    }
}
