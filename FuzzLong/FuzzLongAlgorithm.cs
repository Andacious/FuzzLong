using System;
using System.Collections.Generic;
using System.Linq;

namespace FuzzLong
{
    public class FuzzLongAlgorithm : IFuzzyComparer
    {
        public double Compare(string x, string y)
        {
            if (x == null ^ y == null) return 0;
             
            string xComparer = StringUtil.Canonicalize(x);
            string yComparer = StringUtil.Canonicalize(y);

            if (string.Equals(xComparer, yComparer, StringComparison.Ordinal)) return 1;

            Dictionary<char, int> xMap = GetCharCountMap(xComparer);
            Dictionary<char, int> yMap = GetCharCountMap(yComparer);

            int intersection = 0;

            foreach (char key in xMap.Keys.Union(yMap.Keys))
            {
                intersection += Math.Min(GetKeyCount(xMap, key), GetKeyCount(yMap, key));
            }

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

        private int GetKeyCount(Dictionary<char, int> map, char key)
        {
            if (map.ContainsKey(key))
            {
                return map[key];
            }

            return 0;
        }
    }
}
