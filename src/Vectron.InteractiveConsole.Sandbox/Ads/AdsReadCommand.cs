using Vectron.InteractiveConsole.Commands;

namespace Vectron.InteractiveConsole.Sandbox.Ads;

/// <summary>
/// A <see cref="IConsoleCommand"/> for reading a variable from the PLC.
/// </summary>
internal sealed class AdsReadCommand : IConsoleCommand
{
    /// <inheritdoc/>
    public string[]? ArgumentNames => ["tag"];

    /// <inheritdoc/>
    public string[] CommandParameters => ["ads", "read"];

    /// <inheritdoc/>
    public string HelpText => "Read a value from the PLC through ADS";

    /// <inheritdoc/>
    public int MaxArguments => 1;

    /// <inheritdoc/>
    public int MinArguments => 1;

    /// <inheritdoc/>
    public void Execute(string[] arguments)
        => Console.WriteLine("Read value from PLC");
}
