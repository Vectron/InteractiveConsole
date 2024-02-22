using Vectron.InteractiveConsole.Commands;

namespace Vectron.InteractiveConsole.Sandbox.Ads;

/// <summary>
/// A <see cref="IConsoleCommand"/> for disconnecting to the PLC through ADS.
/// </summary>
internal sealed class AdsDisconnectCommand : IConsoleCommand
{
    /// <inheritdoc/>
    public string[]? ArgumentNames => null;

    /// <inheritdoc/>
    public string[] CommandParameters => ["ads", "disconnect"];

    /// <inheritdoc/>
    public string HelpText => "Disconnect from the PLC through ADS";

    /// <inheritdoc/>
    public int MaxArguments => 0;

    /// <inheritdoc/>
    public int MinArguments => 0;

    /// <inheritdoc/>
    public void Execute(string[] arguments)
        => Console.WriteLine("Disconnected from the PLC");
}
