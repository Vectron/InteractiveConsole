using InteractiveConsole.Commands;

namespace InteractiveConsole.Sandbox.Ads;

/// <summary>
/// A <see cref="IConsoleCommand"/> for connecting to the PLC through OPC.
/// </summary>
internal sealed class OpcConnectCommand : IConsoleCommand
{
    /// <inheritdoc/>
    public string[] CommandParameters => new[] { "opc", "connect" };

    /// <inheritdoc/>
    public string HelpText => "Connect to the PLC through OPC";

    /// <inheritdoc/>
    public int MaxArguments => 0;

    /// <inheritdoc/>
    public int MinArguments => 0;

    /// <inheritdoc/>
    public void Execute(string[] arguments)
        => Console.WriteLine("Connected to PLC");
}
