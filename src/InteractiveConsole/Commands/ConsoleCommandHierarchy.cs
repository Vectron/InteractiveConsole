using System.Diagnostics.CodeAnalysis;

namespace InteractiveConsole.Commands;

/// <summary>
/// A tree collection for console commands.
/// </summary>
internal sealed class ConsoleCommandHierarchy : IConsoleCommandHierarchy
{
    private readonly ConsoleCommandNode root = new();

    /// <summary>
    /// Initializes a new instance of the <see cref="ConsoleCommandHierarchy"/> class.
    /// </summary>
    /// <param name="consoleCommands">The commands to add to this collection.</param>
    public ConsoleCommandHierarchy(IEnumerable<IConsoleCommand> consoleCommands)
    {
        foreach (var command in consoleCommands)
        {
            var node = root;
            foreach (var parameter in command.CommandParameters)
            {
                node = node.CreateOrGetChildNode(parameter);
            }

            if (node == root)
            {
                throw new InvalidOperationException("not allowed to add a command to the root node.");
            }

            node.SetCommand(command);
        }
    }

    /// <inheritdoc/>
    public IEnumerable<IConsoleCommand> GetDescendantsFor(string[] commandArguments)
    {
        var node = (IConsoleCommandNode)root;
        foreach (var key in commandArguments)
        {
            if (!node.TryGetChildNode(key, out var nextNode))
            {
                break;
            }

            node = nextNode;
        }

        return node.Children;
    }

    /// <inheritdoc/>
    public bool TryGetCommand(string[] commandArguments, [NotNullWhen(true)] out IConsoleCommand? consoleCommand)
    {
        consoleCommand = null;
        var node = (IConsoleCommandNode)root;
        foreach (var key in commandArguments)
        {
            if (!node.TryGetChildNode(key, out var nextNode))
            {
                break;
            }

            node = nextNode;
        }

        if (node != null
            && node.TryGetCommand(out consoleCommand))
        {
            return true;
        }

        return false;
    }
}
