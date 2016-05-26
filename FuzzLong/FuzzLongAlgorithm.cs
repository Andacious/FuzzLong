using System;
using System.Collections.Generic;
using System.Linq;

namespace FuzzLong
{
    public class FuzzLongAlgorithm : IFuzzyComparer
    {
        public double Compare(string x, string y)
        {
            // If exactly one of the values are null, then they cannot be equal at all (0 = 0% similar)
            if (x == null ^ y == null) return 0;
             
            // Convert both to an easily comparable form
            string xComparer = StringUtil.StandardizeForComparison(x);
            string yComparer = StringUtil.StandardizeForComparison(y);

            // Check if the standardized strings are equal (1 = 100% similar) at this point to circumvent full algorithm logic
            if (string.Equals(xComparer, yComparer, StringComparison.Ordinal)) return 1;

            // Get the char counts for each string (O(N) for each)
            Dictionary<char, int> xMap = GetCharCountMap(xComparer);
            Dictionary<char, int> yMap = GetCharCountMap(yComparer);

            int intersection = 0;

            // Get the unique set of keys and loop through them, comparing the count from each
            foreach (char key in xMap.Keys.Union(yMap.Keys)) // union should be O(N) since this is using Set<T> internally
            {
                // The full intersection is simply the smallest of the two values since the smallest amount is guaranteed to occur in both strings
                intersection += Math.Min(GetValue(xMap, key), GetValue(yMap, key));
            }

            // Similar to Jaccard, take the intersection and divide it by the union. In this case, the union is the count of the longest string
            // since that is the maximum amount of characters that the strings could possibly have in common.
            return (double)intersection / Math.Max(xComparer.Length, yComparer.Length);
        }

        private Dictionary<char, int> GetCharCountMap(string x)
        {
            Dictionary<char, int> charCountMap = new Dictionary<char, int>();

            foreach (char c in x)
            {
                if (charCountMap.ContainsKey(c))
                    charCountMap[c]++;
                else
                    charCountMap.Add(c, 1);
            }

            return charCountMap;
        }

        private int GetValue(Dictionary<char, int> map, char key)
        {
            int count;
            map.TryGetValue(key, out count);

            return count;
        }
    }
}
