namespace InteractiveConsole.Input;

/// <summary>
/// Implements the functions for processing user input.
/// </summary>
public interface IInputHandler
{
    /// <summary>
    /// Process the user input.
    /// </summary>
    /// <param name="input">The input to process.</param>
    void ProcessInput(string input);
}
