using Vectron.InteractiveConsole.Commands;

namespace Vectron.InteractiveConsole.Sandbox.Ads;

/// <summary>
/// A <see cref="IConsoleCommand"/> for monitor a variable in the PLC.
/// </summary>
internal sealed class AdsUnmonitorCommand : IConsoleCommand
{
    /// <inheritdoc/>
    public string[]? ArgumentNames => ["tag"];

    /// <inheritdoc/>
    public string[] CommandParameters => ["ads", "unmonitor"];

    /// <inheritdoc/>
    public string HelpText => "Stop monitor a value in the PLC through ADS";

    /// <inheritdoc/>
    public int MaxArguments => 1;

    /// <inheritdoc/>
    public int MinArguments => 1;

    /// <inheritdoc/>
    public void Execute(string[] arguments)
        => Console.WriteLine($"stopped monitoring {arguments[0]}");
}
