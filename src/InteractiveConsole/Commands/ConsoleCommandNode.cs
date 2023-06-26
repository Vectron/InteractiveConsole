using System.Diagnostics.CodeAnalysis;

namespace InteractiveConsole.Commands;

/// <summary>
/// A node for the <see cref="ConsoleCommandCollection"/>.
/// </summary>
internal sealed class ConsoleCommandNode : IConsoleCommandNode
{
    private Dictionary<string, ConsoleCommandNode>? children;
    private IConsoleCommand? command;

    /// <inheritdoc/>
    public IEnumerable<IConsoleCommand> Children
    {
        get
        {
            if (command != null)
            {
                yield return command;
            }

            if (children == null)
            {
                yield break;
            }

            foreach (var child in children.Values)
            {
                foreach (var childCommand in child.Children)
                {
                    yield return childCommand;
                }
            }
        }
    }

    /// <summary>
    /// Gets the leaf for the given key, or create one if none found yet.
    /// </summary>
    /// <param name="key">The key to look for.</param>
    /// <returns>The found leaf node.</returns>
    public ConsoleCommandNode CreateOrGetChildNode(string key)
    {
        children ??= new Dictionary<string, ConsoleCommandNode>(StringComparer.OrdinalIgnoreCase);
        if (children.TryGetValue(key, out var child))
        {
            return child;
        }

        child = new ConsoleCommandNode();
        children.Add(key, child);
        return child;
    }

    /// <summary>
    /// Set the command for this node.
    /// </summary>
    /// <param name="command">The command to set.</param>
    /// <exception cref="InvalidOperationException">
    /// Thrown when the command has already been set.
    /// </exception>
    public void SetCommand(IConsoleCommand command)
    {
        if (this.command != null)
        {
            throw new InvalidOperationException($"Command already set for this node. ({string.Join(' ', command.CommandParameters)})");
        }

        this.command = command;
    }

    /// <inheritdoc/>
    public bool TryGetChildNode(string key, [NotNullWhen(true)] out IConsoleCommandNode? consoleCommandNode)
    {
        consoleCommandNode = null;
        if (children == null)
        {
            return false;
        }

        if (children.TryGetValue(key, out var foundCommandNode))
        {
            consoleCommandNode = foundCommandNode;
            return true;
        }

        return false;
    }

    /// <inheritdoc/>
    public bool TryGetCommand([NotNullWhen(true)] out IConsoleCommand? consoleCommand)
    {
        consoleCommand = command;
        return command != null;
    }
}
