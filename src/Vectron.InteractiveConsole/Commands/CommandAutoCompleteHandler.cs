using Vectron.InteractiveConsole.AutoComplete;

namespace Vectron.InteractiveConsole.Commands;

/// <summary>
/// A Autocomplete handler for commands.
/// </summary>
/// <remarks>
/// Initializes a new instance of the <see cref="CommandAutoCompleteHandler"/> class.
/// </remarks>
/// <param name="consoleCommands">All the registered <see cref="IConsoleCommand"/>.</param>
internal sealed class CommandAutoCompleteHandler(IConsoleCommandHierarchy consoleCommands) : IAutoCompleteHandler
{
    private LinkedList<string> autoCompletions = new();
    private LinkedListNode<string>? current;
    private string rootCommand = string.Empty;

    /// <inheritdoc/>
    public string? NextSuggestions(string text)
    {
        Init(text);
        if (current == null)
        {
            current = autoCompletions.First;
            return current?.Value;
        }

        current = current.Next;
        return current?.Value;
    }

    /// <inheritdoc/>
    public string? PreviousSuggestions(string text)
    {
        Init(text);
        if (current == null)
        {
            current = autoCompletions.Last;
            return current?.Value;
        }

        current = current.Previous;
        return current?.Value;
    }

    private void Init(string text)
    {
        if (rootCommand.Equals(text, StringComparison.OrdinalIgnoreCase)
            && autoCompletions.Count > 0)
        {
            return;
        }

        rootCommand = text;
        var arguments = rootCommand.Split(CommandInputHandler.ArgumentSeparator);
        current = null;
        autoCompletions.Clear();

        var validCommandOptions = consoleCommands
            .GetDescendantsFor(arguments)
            .Select(x => string.Join(' ', x.CommandParameters))
            .Where(x => x.StartsWith(text, StringComparison.OrdinalIgnoreCase));

        autoCompletions = new LinkedList<string>(validCommandOptions);
    }
}
