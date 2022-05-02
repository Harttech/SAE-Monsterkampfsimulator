namespace SharedUtilities.Extensions
{
    /// <summary>
    /// A utility class to improve efficiency for work with System.Console.
    /// </summary>
    public static class ConsoleHelper
    {
        /// <summary>
        /// Equivalent to setting Console.ForegroundColor and Console.BackgroundColor and then calling Console.Write;
        /// </summary>
        /// <param name="message">The message to be printed.</param>
        /// <param name="foregroundColor">The foreground color the message should be printed in.</param>
        /// <param name="backgroundColor">The background color the message should be printed in.</param>
        /// <param name="resetToDefault">Whether the colors should be reset to default after printing the message.</param>
        public static void Print(string message, ConsoleColor foregroundColor = ConsoleColor.Gray, ConsoleColor backgroundColor = ConsoleColor.Black, bool resetToDefault = true) =>
            ConsolePrint(message, false, foregroundColor, backgroundColor, resetToDefault);

        /// <summary>
        /// Equivalent to setting Console.ForegroundColor and Console.BackgroundColor and then calling Console.WriteLine;
        /// </summary>
        /// <param name="message">The message to be printed.</param>
        /// <param name="foregroundColor">The foreground color the message should be printed in.</param>
        /// <param name="backgroundColor">The background color the message should be printed in.</param>
        /// <param name="resetToDefault">Whether the colors should be reset to default after printing the message.</param>
        public static void PrintLine(string message, ConsoleColor foregroundColor = ConsoleColor.Gray, ConsoleColor backgroundColor = ConsoleColor.Black, bool resetToDefault = true) =>
            ConsolePrint(message, true, foregroundColor, backgroundColor, resetToDefault);

        /// <summary>
        /// Prints an empty line.
        /// </summary>
        public static void PrintEmptyLine() => PrintEmptyLines();

        /// <summary>
        /// Prints multiple empty lines.
        /// </summary>
        /// <param name="amount">The amount of lines to be printed.</param>
        public static void PrintEmptyLines(byte amount = 1)
        {
            for (int i = 0; i < amount; i++)
                Console.WriteLine();
        }

        private static void ConsolePrint(string message, bool printLine, ConsoleColor foregroundColor = ConsoleColor.Gray, ConsoleColor backgroundColor = ConsoleColor.Black, bool resetToDefault = true)
        {
            Console.ForegroundColor = foregroundColor;
            Console.BackgroundColor = backgroundColor;

            if (printLine)
                Console.WriteLine(message);
            else
                Console.Write(message);

            if (resetToDefault)
                Console.ResetColor();
        }

        /// <summary>
        /// Prompts for an input and attempts to parse the user's input to an <see cref="int"/>. It will print a message if the parsing failed.
        /// </summary>
        /// <param name="prompt">The prompt printed to the console.</param>
        /// <param name="cancelCallword">If this expression is entered by the user, the prompt will be cancelled and null returned. This argument is case insensitive. Pass null to disallow cancelling.</param>
        /// <param name="min">Entered value must be greater than or equal to this value.</param>
        /// <param name="max">Entered value must be less than or equal to this value.</param>
        /// <param name="promptForegroundColor">The foreground color for the prompt text.</param>
        /// <param name="promptBackgroundColor">The background color for the prompt text.</param>
        /// <param name="errorForegroundColor">The foreground color for error messages.</param>
        /// <param name="errorBackgroundColor">The background color for error messages.</param>
        /// <param name="resetToDefault">Whether the console colors should be reset after a message was printed.</param>
        /// <returns>Null if the prompt was cancelled. Otherwise, an <see cref="int"/>.</returns>
        public static int? ReadInt32(string prompt, string cancelCallword = "cancel", int min = int.MinValue, int max = int.MaxValue, ConsoleColor promptForegroundColor = ConsoleColor.Gray, ConsoleColor promptBackgroundColor = ConsoleColor.Black,
            ConsoleColor errorForegroundColor = ConsoleColor.Gray, ConsoleColor errorBackgroundColor = ConsoleColor.Black, bool resetToDefault = true)
        {
            int? result;
            if (cancelCallword.IsNullOrWhiteSpace())
                cancelCallword = string.Empty;

            while (true)
            {
                result = null;
                Print(prompt, promptForegroundColor, promptBackgroundColor, resetToDefault);
                var input = Console.ReadLine();
                if (input!.IsNullOrWhiteSpace())
                {
                    if (cancelCallword == string.Empty)
                        break;

                    PrintLine("A number has to be entered.", errorForegroundColor, errorBackgroundColor, resetToDefault);
                }
                else
                {
                    if (input!.ToLowerInvariant() == cancelCallword.ToLowerInvariant())
                        break;

                    if (int.TryParse(input, out var parseResult))
                    {
                        if (parseResult < min || parseResult > max)
                        {
                            PrintLine($"{input} is out of the allowed range (Min: {min}, Max: {max}).", errorForegroundColor, errorBackgroundColor, resetToDefault);
                            continue;
                        }

                        result = parseResult;
                        break;
                    }

                    PrintLine($"{input} is not a valid integer.", errorForegroundColor, errorBackgroundColor, resetToDefault);

                    if (!resetToDefault)
                    {
                        Console.ForegroundColor = promptForegroundColor;
                        Console.BackgroundColor = promptBackgroundColor;
                    }
                }
            }

            return result;
        }

        /// <summary>
        /// Prompts for an input and attempts to parse the user's input to a <see cref="float"/>. It will print a message if the parsing failed.
        /// </summary>
        /// <param name="prompt">The prompt printed to the console.</param>
        /// <param name="cancelCallword">If this expression is entered by the user, the prompt will be cancelled and null returned. This argument is case insensitive. Pass null to disallow cancelling.</param>
        /// <param name="min">Entered value must be greater than or equal to this value.</param>
        /// <param name="max">Entered value must be less than or equal to this value.</param>
        /// <param name="promptForegroundColor">The foreground color for the prompt text.</param>
        /// <param name="promptBackgroundColor">The background color for the prompt text.</param>
        /// <param name="errorForegroundColor">The foreground color for error messages.</param>
        /// <param name="errorBackgroundColor">The background color for error messages.</param>
        /// <param name="resetToDefault">Whether the console colors should be reset after a message was printed.</param>
        /// <returns>Null if the prompt was cancelled. Otherwise, a <see cref="float"/>.</returns>
        public static float? ReadFloat(string prompt, string cancelCallword = "cancel", float min = float.MinValue, float max = float.MaxValue, ConsoleColor promptForegroundColor = ConsoleColor.Gray, ConsoleColor promptBackgroundColor = ConsoleColor.Black,
            ConsoleColor errorForegroundColor = ConsoleColor.Gray, ConsoleColor errorBackgroundColor = ConsoleColor.Black, bool resetToDefault = true)
        {
            float? result;
            if (cancelCallword != null && cancelCallword.IsNullOrWhiteSpace())
                cancelCallword = string.Empty;

            while (true)
            {
                result = null;
                Print(prompt, promptForegroundColor, promptBackgroundColor, resetToDefault);
                var input = Console.ReadLine();
                if (input!.IsNullOrWhiteSpace())
                {
                    if (cancelCallword == string.Empty)
                        break;

                    PrintLine("A number has to be entered.", errorForegroundColor, errorBackgroundColor, resetToDefault);
                }
                else
                {
                    if (cancelCallword != null && input!.ToLowerInvariant() == cancelCallword.ToLowerInvariant())
                        break;

                    if (float.TryParse(input, out var parseResult))
                    {
                        if (parseResult < min || parseResult > max)
                        {
                            PrintLine($"{input} is out of the allowed range (Min: {min}, Max: {max}).", errorForegroundColor, errorBackgroundColor, resetToDefault);
                            continue;
                        }

                        result = parseResult;
                        break;
                    }

                    PrintLine($"{input} is not a valid integer.", errorForegroundColor, errorBackgroundColor, resetToDefault);

                    if (!resetToDefault)
                    {
                        Console.ForegroundColor = promptForegroundColor;
                        Console.BackgroundColor = promptBackgroundColor;
                    }
                }
            }

            return result;
        }

        /// <summary>
        /// Waits until the user presses any key.
        /// </summary>
        public static void WaitForInput() => Console.ReadKey(true);
    }
}
