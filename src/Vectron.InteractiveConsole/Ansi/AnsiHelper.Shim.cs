#if !NET7_0_OR_GREATER

namespace Vectron.InteractiveConsole.Ansi;

/// <summary>
/// A shim when using older dotnet version that don't contain the regex source generator.
/// </summary>
internal static partial class AnsiHelper
{
    private static readonly System.Text.RegularExpressions.Regex FindCursorEscapeSequenceRegex = new(
        @"\x1B\[[^@-~]*[ABCDabcd]",
        System.Text.RegularExpressions.RegexOptions.Compiled,
        TimeSpan.FromSeconds(1));

    private static readonly System.Text.RegularExpressions.Regex RemoveEscapeSequenceRegex = new(
        @"\x1B\[[^@-~]*[@-~]",
        System.Text.RegularExpressions.RegexOptions.Compiled,
        TimeSpan.FromSeconds(1));

    private static System.Text.RegularExpressions.Regex FindCursorEscapeSequence() => FindCursorEscapeSequenceRegex;

    private static System.Text.RegularExpressions.Regex RemoveEscapeSequence() => RemoveEscapeSequenceRegex;
}

#endif
