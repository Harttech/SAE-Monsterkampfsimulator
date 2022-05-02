namespace SharedUtilities.Extensions
{
    public static class StringExtensions
    {
        /// <summary>
        /// Calls <see cref="string.IsNullOrWhiteSpace"/>. Improves flow of reading.
        /// </summary>
        /// <param name="source">The source string to check.</param>
        /// <returns>A bool whether <see cref="source"/> null or consists only of whitespace.</returns>
        public static bool IsNullOrWhiteSpace(this string source) => string.IsNullOrWhiteSpace(source);
        /// <summary>
        /// Returns whether <see cref="source"/> has any visible characters apart from whitespace.
        /// </summary>
        /// <param name="source">The source string to check.</param>
        /// <returns>A bool whether <see cref="source"/> has any visible characters apart from whitespace.</returns>
        public static bool HasText(this string source) => !source.IsNullOrWhiteSpace();
    }
}
