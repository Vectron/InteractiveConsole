namespace Vectron.InteractiveConsole.Cursor;

/// <summary>
/// Helper class to manipulate the cursor position.
/// </summary>
public interface IConsoleCursor
{
    /// <summary>
    /// Gets or sets the current position of the cursor.
    /// </summary>
    int CursorIndex
    {
        get;
        set;
    }

    /// <summary>
    /// Move the cursor back <paramref name="amount"/> positions.
    /// </summary>
    /// <param name="amount">The number of positions to move the cursor.</param>
    void MoveBackward(int amount);

    /// <summary>
    /// Move the cursor to the end position.
    /// </summary>
    void MoveEnd();

    /// <summary>
    /// Move the cursor forward <paramref name="amount"/> positions.
    /// </summary>
    /// <param name="amount">The number of positions to move the cursor.</param>
    void MoveForward(int amount);

    /// <summary>
    /// Move the cursor to the start position.
    /// </summary>
    void MoveStart();
}
