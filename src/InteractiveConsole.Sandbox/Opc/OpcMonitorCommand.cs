using InteractiveConsole.Commands;

namespace InteractiveConsole.Sandbox.Ads;

/// <summary>
/// A <see cref="IConsoleCommand"/> for monitor a variable in the PLC.
/// </summary>
internal sealed class OpcMonitorCommand : IConsoleCommand
{
    /// <inheritdoc/>
    public string[] CommandParameters => new[] { "opc", "monitor" };

    /// <inheritdoc/>
    public string HelpText => "Monitor a value from the PLC through OPC";

    /// <inheritdoc/>
    public int MaxArguments => 1;

    /// <inheritdoc/>
    public int MinArguments => 1;

    /// <inheritdoc/>
    public void Execute(string[] arguments)
        => Console.WriteLine($"Monitoring {arguments[0]}");
}
