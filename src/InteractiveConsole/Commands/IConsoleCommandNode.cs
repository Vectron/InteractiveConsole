using System.Diagnostics.CodeAnalysis;

namespace InteractiveConsole.Commands;

/// <summary>
/// A leaf node for the commands tree.
/// </summary>
internal interface IConsoleCommandNode
{
    /// <summary>
    /// Gets an <see cref="IEnumerable{T}"/> of all child nodes in a depth first search.
    /// </summary>
    IEnumerable<IConsoleCommand> Children
    {
        get;
    }

    /// <summary>
    /// Try to get the node for the given <paramref name="key"/>.
    /// </summary>
    /// <param name="key">The key to get the child node for.</param>
    /// <param name="consoleCommandNode">
    /// The found child node, <see langword="null"/> when not found.
    /// </param>
    /// <returns><see langword="true"/> when a node is found otherwise <see langword="false"/>.</returns>
    bool TryGetChildNode(string key, [NotNullWhen(true)] out IConsoleCommandNode? consoleCommandNode);

    /// <summary>
    /// Try to get the <see cref="IConsoleCommand"/> for this node.
    /// </summary>
    /// <param name="consoleCommand">
    /// The <see cref="IConsoleCommand"/> associated with this node, <see langword="null"/> when not set.
    /// </param>
    /// <returns><see langword="true"/> when command is set otherwise <see langword="false"/>.</returns>
    bool TryGetCommand([NotNullWhen(true)] out IConsoleCommand? consoleCommand);
}
