using Vectron.InteractiveConsole.Commands;

namespace Vectron.InteractiveConsole.Sandbox.Opc;

/// <summary>
/// A <see cref="IConsoleCommand"/> for disconnecting to the PLC through ADS.
/// </summary>
internal sealed class OpcDisconnectCommand : IConsoleCommand
{
    /// <inheritdoc/>
    public string[] CommandParameters => new[] { "opc", "disconnect" };

    /// <inheritdoc/>
    public string HelpText => "Disconnect from the PLC through OPC";

    /// <inheritdoc/>
    public int MaxArguments => 0;

    /// <inheritdoc/>
    public int MinArguments => 0;

    /// <inheritdoc/>
    public void Execute(string[] arguments)
        => Console.WriteLine("Disconnected from the PLC");
}
