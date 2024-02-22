using Vectron.InteractiveConsole.Commands;

namespace Vectron.InteractiveConsole.Sandbox.Opc;

/// <summary>
/// A <see cref="IConsoleCommand"/> for monitor a variable in the PLC.
/// </summary>
internal sealed class OpcUnmonitorCommand : IConsoleCommand
{
    /// <inheritdoc/>
    public string[]? ArgumentNames => ["tag"];

    /// <inheritdoc/>
    public string[] CommandParameters => ["opc", "unmonitor"];

    /// <inheritdoc/>
    public string HelpText => "Stop monitor a value in the PLC through OPC";

    /// <inheritdoc/>
    public int MaxArguments => 1;

    /// <inheritdoc/>
    public int MinArguments => 1;

    /// <inheritdoc/>
    public void Execute(string[] arguments)
        => Console.WriteLine($"stopped monitoring {arguments[0]}");
}
