using Vectron.InteractiveConsole.Commands;

namespace Vectron.InteractiveConsole.Sandbox.Ads;

/// <summary>
/// A <see cref="IConsoleCommand"/> for connecting to the PLC through ADS.
/// </summary>
internal sealed class AdsConnectCommand : IConsoleCommand
{
    /// <inheritdoc/>
    public string[]? ArgumentNames => null;

    /// <inheritdoc/>
    public string[] CommandParameters => ["ads", "connect"];

    /// <inheritdoc/>
    public string HelpText => "Connect to the PLC through ADS";

    /// <inheritdoc/>
    public int MaxArguments => 0;

    /// <inheritdoc/>
    public int MinArguments => 0;

    /// <inheritdoc/>
    public void Execute(string[] arguments)
        => Console.WriteLine("Connected to PLC");
}
