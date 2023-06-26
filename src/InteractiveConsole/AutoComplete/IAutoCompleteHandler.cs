namespace InteractiveConsole.AutoComplete;

/// <summary>
/// Class handles the auto complete suggestions.
/// </summary>
public interface IAutoCompleteHandler
{
    /// <summary>
    /// Gets the next Autocomplete suggestion.
    /// </summary>
    /// <param name="text">The text to get suggestions for.</param>
    /// <returns>The next suggestion when available, otherwise <see langword="null"/>.</returns>
    string? NextSuggestions(string text);

    /// <summary>
    /// Gets the previous Autocomplete suggestion.
    /// </summary>
    /// <param name="text">The text to get suggestions for.</param>
    /// <returns>The previous suggestion when available, otherwise <see langword="null"/>.</returns>
    string? PreviousSuggestions(string text);
}
