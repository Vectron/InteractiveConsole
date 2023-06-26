namespace InteractiveConsole;

/// <summary>
/// A wrapper over the console output stream.
/// </summary>
public interface IConsoleOutput
{
    /// <summary>
    /// Gets or sets the static text to always show at the end.
    /// </summary>
    /// <param name="value">The <see cref="string"/> to write.</param>
    public string StaticText
    {
        get;
        set;
    }

    /// <summary>
    /// Writes a <see cref="string"/> to the text stream.
    /// </summary>
    /// <param name="value">The <see cref="string"/> to write.</param>
    void Write(string? value);

    /// <summary>
    /// Writes a <see cref="string"/> followed by a line terminator to the text stream.
    /// </summary>
    /// <param name="value">The <see cref="string"/> to write.</param>
    void WriteLine(string? value);
}
