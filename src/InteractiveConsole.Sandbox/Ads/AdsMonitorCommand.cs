using InteractiveConsole.Commands;

namespace InteractiveConsole.Sandbox.Ads;

/// <summary>
/// A <see cref="IConsoleCommand"/> for monitor a variable in the PLC.
/// </summary>
internal sealed class AdsMonitorCommand : IConsoleCommand
{
    /// <inheritdoc/>
    public string[] CommandParameters => new[] { "ads", "monitor" };

    /// <inheritdoc/>
    public string HelpText => "Monitor a value from the PLC through ADS";

    /// <inheritdoc/>
    public int MaxArguments => 1;

    /// <inheritdoc/>
    public int MinArguments => 1;

    /// <inheritdoc/>
    public void Execute(string[] arguments)
        => Console.WriteLine($"Monitoring {arguments[0]}");
}
