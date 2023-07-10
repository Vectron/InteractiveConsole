using System.Globalization;
using System.Text.RegularExpressions;

namespace Vectron.InteractiveConsole.Ansi;

/// <summary>
/// A helper class for generating ANSI escape sequences.
/// </summary>
internal static class AnsiHelper
{
    /// <summary>
    /// The control marker for starting the ANSI code.
    /// </summary>
    public const char EscapeSequence = '\x1B';

    /// <summary>
    /// The ANSI reset code.
    /// </summary>
    public const string ResetAnsiEscapeCode = "\x1B[0m";

    private static readonly Regex FindCursorEscapeSequence = new(@"\x1B\[[^@-~]*[ABCDabcd]", RegexOptions.Compiled, TimeSpan.FromSeconds(1));
    private static readonly Regex RemoveEscapeSequence = new(@"\x1B\[[^@-~]*[@-~]", RegexOptions.Compiled, TimeSpan.FromSeconds(1));

    /// <summary>
    /// Dumps all possible colors to the screen.
    /// </summary>
    public static void DumpColorsToConsole()
    {
        Console.WriteLine("System Colors");
        byte color = 0;
        for (; color < 16; color++)
        {
            WriteColor(color);

            if ((color + 1) % 8 == 0)
            {
                Console.WriteLine();
            }
        }

        Console.WriteLine();
        Console.WriteLine("Color cube, 6x6x6");
        for (; color < 232; color++)
        {
            WriteColor(color);

            if ((color - 15) % 6 == 0)
            {
                Console.WriteLine();
            }

            if ((color - 15) % 36 == 0)
            {
                Console.WriteLine();
            }
        }

        Console.WriteLine("Grayscale ramp");
        for (; color < 255; color++)
        {
            WriteColor(color);
        }

        Console.WriteLine();
        Console.WriteLine();

        static void WriteColor(byte color)
        {
            var colorCode = GetAnsiEscapeCode(color, background: false);
            Console.Write($"{colorCode}{color.ToString(CultureInfo.InvariantCulture),-4}");
        }
    }

    /// <summary>
    /// Gets the ANSI clear code for the given option.
    /// </summary>
    /// <param name="clearScreen">How much to clear.</param>
    /// <returns>A <see cref="string"/> containing the ANSI code.</returns>
    /// <exception cref="NotSupportedException">When an unknown option is given.</exception>
    public static string GetAnsiEscapeCode(AnsiClearOption clearScreen)
    {
        var value = clearScreen switch
        {
            AnsiClearOption.CursorToEndOfScreen => 0,
            AnsiClearOption.CursorToEndStartOfScreen => 1,
            AnsiClearOption.EntireScreen => 2,
            AnsiClearOption.CursorToEndOfLine => 0,
            AnsiClearOption.CursorToEndStartOfLine => 1,
            AnsiClearOption.EntireLine => 2,
            _ => throw new NotSupportedException("Unknown options"),
        };

        var option = clearScreen switch
        {
            AnsiClearOption.CursorToEndOfScreen => 'J',
            AnsiClearOption.CursorToEndStartOfScreen => 'J',
            AnsiClearOption.EntireScreen => 'J',
            AnsiClearOption.CursorToEndOfLine => 'K',
            AnsiClearOption.CursorToEndStartOfLine => 'K',
            AnsiClearOption.EntireLine => 'K',
            _ => throw new NotSupportedException("Unknown options"),
        };

        return $"{EscapeSequence}[{value.ToString(CultureInfo.InvariantCulture)}{option}";
    }

    /// <summary>
    /// Gets the ANSI color code for the given color.
    /// </summary>
    /// <param name="color">The color.</param>
    /// <param name="bright"><see langword="true"/> if color should be bright.</param>
    /// <param name="background"><see langword="true"/> when the color is the background color.</param>
    /// <returns>A <see cref="string"/> containing the ANSI code.</returns>
    public static string GetAnsiEscapeCode(AnsiColor color, bool bright, bool background)
    {
        var colorCode = color switch
        {
            AnsiColor.Black => 30,
            AnsiColor.Red => 31,
            AnsiColor.Green => 32,
            AnsiColor.Yellow => 33,
            AnsiColor.Blue => 34,
            AnsiColor.Magenta => 35,
            AnsiColor.Cyan => 36,
            AnsiColor.White => 37,
            _ => 0,
        };

        if (background)
        {
            colorCode += 10;
        }

        return CreateAnsiEscapeColorCode(colorCode, bright);
    }

    /// <summary>
    /// Gets the ANSI color code for the given color.
    /// </summary>
    /// <param name="color">The color.</param>
    /// <returns>A <see cref="string"/> containing the ANSI code.</returns>
    public static string GetAnsiEscapeCode(AnsiColor color)
        => GetAnsiEscapeCode(color, bright: false, background: false);

    /// <summary>
    /// Gets the ANSI color code for the given color.
    /// </summary>
    /// <param name="xtermColor">The color.</param>
    /// <param name="background"><see langword="true"/> when the color is the background color.</param>
    /// <returns>A <see cref="string"/> containing the ANSI code.</returns>
    public static string GetAnsiEscapeCode(byte xtermColor, bool background)
        => background
            ? $"{EscapeSequence}[48;5;{xtermColor}m"
            : $"{EscapeSequence}[38;5;{xtermColor}m";

    /// <summary>
    /// Get the ANSI escape code for the given parameters.
    /// </summary>
    /// <param name="foregroundColor">The foreground color.</param>
    /// <param name="foregroundBright"><see langword="true"/> if foreground color should be bright.</param>
    /// <param name="backgroundColor">The background color.</param>
    /// <param name="backgroundBright"><see langword="true"/> if background color should be bright.</param>
    /// <param name="style">The text style.</param>
    /// <returns>A <see cref="string"/> containing the ANSI code.</returns>
    public static string GetAnsiEscapeCode(AnsiColor foregroundColor, bool foregroundBright, AnsiColor backgroundColor, bool backgroundBright, AnsiStyle style)
    {
        var foregroundColorCode = GetAnsiEscapeCode(foregroundColor, foregroundBright, background: false);
        var backgroundColorCode = GetAnsiEscapeCode(backgroundColor, backgroundBright, background: true);
        var styleCode = GetAnsiEscapeCode(style);

        return $"{foregroundColorCode}{backgroundColorCode}{styleCode}";
    }

    /// <summary>
    /// Get the ANSI escape code for the given style.
    /// </summary>
    /// <param name="style">The text style.</param>
    /// <returns>A <see cref="string"/> containing the ANSI code.</returns>
    public static string GetAnsiEscapeCode(AnsiStyle style)
    {
        var bold = style.HasFlag(AnsiStyle.Bold) ? CreateAnsiEscapeCode(1) : string.Empty;
        var underlined = style.HasFlag(AnsiStyle.Underlined) ? CreateAnsiEscapeCode(4) : string.Empty;
        var reversed = style.HasFlag(AnsiStyle.Reversed) ? CreateAnsiEscapeCode(7) : string.Empty;

        return $"{bold}{underlined}{reversed}";
    }

    /// <summary>
    /// Get the ANSI escape code for the given cursor movement.
    /// </summary>
    /// <param name="cursorDirection">The direction to move in.</param>
    /// <param name="amount">The amount of positions to move.</param>
    /// <returns>A <see cref="string"/> containing the ANSI code.</returns>
    /// <exception cref="NotSupportedException">When an unknown option is given.</exception>
    public static string GetAnsiEscapeCode(AnsiCursorDirection cursorDirection, int amount)
    {
        if (amount == 0)
        {
            return string.Empty;
        }

        var directions = cursorDirection switch
        {
            AnsiCursorDirection.Up => 'A',
            AnsiCursorDirection.Down => 'B',
            AnsiCursorDirection.Right => 'C',
            AnsiCursorDirection.Left => 'D',
            _ => throw new NotSupportedException("Unknown cursor direction"),
        };

        return $"{EscapeSequence}[{amount.ToString(CultureInfo.InvariantCulture)}{directions}";
    }

    /// <summary>
    /// Remove ANSI escape codes from the given string.
    /// </summary>
    /// <param name="input">The input string.</param>
    /// <returns>A string without escape codes.</returns>
    public static string RemoveAnsiCodes(this string input)
        => RemoveEscapeSequence.Replace(input, string.Empty);

    /// <summary>
    /// Remove ANSI cursor escape codes from the given string.
    /// </summary>
    /// <param name="input">The input string.</param>
    /// <returns>A string without cursor escape codes.</returns>
    public static string RemoveAnsiCursorCode(this string input)
        => FindCursorEscapeSequence.Replace(input, string.Empty);

    private static string CreateAnsiEscapeCode(int value)
        => $"{EscapeSequence}[{value.ToString(CultureInfo.InvariantCulture)}m";

    private static string CreateAnsiEscapeColorCode(int value, bool bright)
    {
        var brightCode = bright ? $";1" : string.Empty;
        return $"{EscapeSequence}[{value.ToString(CultureInfo.InvariantCulture)}{brightCode}m";
    }
}
