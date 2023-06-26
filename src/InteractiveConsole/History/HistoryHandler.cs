namespace InteractiveConsole.History;

/// <summary>
/// A implementation of <see cref="IHistoryHandler"/>.
/// </summary>
internal sealed class HistoryHandler : IHistoryHandler
{
    private const int MaxHistory = 10;
    private readonly LinkedList<string> history = new();
    private LinkedListNode<string>? current;

    /// <inheritdoc/>
    public void AddEntry(string entry)
    {
        if (history.Count > MaxHistory)
        {
            history.RemoveFirst();
        }

        _ = history.AddLast(entry);
        current = null;
    }

    /// <inheritdoc/>
    public string NextEntry()
    {
        if (current == null)
        {
            return string.Empty;
        }

        current = current.Next;
        return current?.Value ?? string.Empty;
    }

    /// <inheritdoc/>
    public string PreviousEntry()
    {
        if (current == null)
        {
            current = history.Last;
            return current?.Value ?? string.Empty;
        }

        if (current == history.First)
        {
            return current.Value;
        }

        var previousItem = current.Previous;
        if (previousItem == null)
        {
            return string.Empty;
        }

        current = previousItem;
        return current.Value;
    }
}
