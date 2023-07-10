namespace Vectron.InteractiveConsole.History;

/// <summary>
/// This class handles the history.
/// </summary>
public interface IHistoryHandler
{
    /// <summary>
    /// Add a new item to the history.
    /// </summary>
    /// <param name="entry">The entry to add.</param>
    void AddEntry(string entry);

    /// <summary>
    /// Gets the next inserted command.
    /// </summary>
    /// <returns>The next history item when available.</returns>
    string NextEntry();

    /// <summary>
    /// Gets the previous inserted command.
    /// </summary>
    /// <returns>The previous history item when available.</returns>
    string PreviousEntry();
}
