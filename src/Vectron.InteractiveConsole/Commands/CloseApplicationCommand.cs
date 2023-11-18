using Microsoft.Extensions.Hosting;

namespace Vectron.InteractiveConsole.Commands;

/// <summary>
/// A <see cref="IConsoleCommand"/> that closes the program.
/// </summary>
/// <remarks>
/// Initializes a new instance of the <see cref="CloseApplicationCommand"/> class.
/// </remarks>
/// <param name="hostApplicationLifetime">A <see cref="IHostApplicationLifetime"/> implementation.</param>
internal sealed class CloseApplicationCommand(IHostApplicationLifetime hostApplicationLifetime) : IConsoleCommand
{
    /// <inheritdoc/>
    public string[]? ArgumentNames => null;

    /// <inheritdoc/>
    public string[] CommandParameters => new[] { "exit" };

    /// <inheritdoc/>
    public string HelpText => "Close the application";

    /// <inheritdoc/>
    public int MaxArguments => 0;

    /// <inheritdoc/>
    public int MinArguments => 0;

    /// <inheritdoc/>
    public void Execute(string[] arguments)
        => hostApplicationLifetime.StopApplication();
}
