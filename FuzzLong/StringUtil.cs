namespace FuzzLong
{
    public class StringUtil
    {
        public static string Canonicalize(string x)
        {
            return x?.Trim().ToUpperInvariant().Normalize();
        }
    }
}