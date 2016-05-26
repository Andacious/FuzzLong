namespace FuzzLong
{
    public class StringUtil
    {
        /// <summary>
        /// Converts a string to a standard comparison representation
        /// </summary>
        /// <param name="x">String to be standardized</param>
        /// <returns><paramref name="x"/> converted to uppercase and normalized.</returns>
        public static string StandardizeForComparison(string x)
        {
            return x?.ToUpperInvariant().Normalize();
        }
    }
}