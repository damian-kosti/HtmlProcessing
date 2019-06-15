using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HtmlScrapper.ProcessingModules
{
    class OpinionProcessor
    {
        public static int GetLevenshteinDistance(string a, string b)
        {
            if (String.IsNullOrEmpty(a) && String.IsNullOrEmpty(b))
            {
                return 0;
            }
            if (String.IsNullOrEmpty(a))
            {
                return b.Length;
            }
            if (String.IsNullOrEmpty(b))
            {
                return a.Length;
            }
            int lengthA = a.Length;
            int lengthB = b.Length;
            var distances = new int[lengthA + 1, lengthB + 1];
            for (int i = 0; i <= lengthA; distances[i, 0] = i++) ;
            for (int j = 0; j <= lengthB; distances[0, j] = j++) ;

            for (int i = 1; i <= lengthA; i++)
                for (int j = 1; j <= lengthB; j++)
                {
                    int cost = b[j - 1] == a[i - 1] ? 0 : 1;
                    distances[i, j] = Math.Min
                        (
                        Math.Min(distances[i - 1, j] + 1, distances[i, j - 1] + 1),
                        distances[i - 1, j - 1] + cost
                        );
                }
            return distances[lengthA, lengthB];
        }

        public static IEnumerable<string> SplitIntoWords(string text)
        {
            IEnumerable<string> words = text
                .Replace(".", "")
                .Replace(",", "")
                .Replace(";", "")
                .Replace("=", "")
                .Replace("-", "")
                .Replace("+", "")
                .Replace("!", "")
                .Replace("?", "")
                .Split(' ');

            return words.Where(w => w.Length > 2);
        }


        public static int CalculateStrength(string text, IEnumerable<string> adjectives)
        {
            var words = SplitIntoWords(text);
            int strength = 0;

            foreach (var word in words)
            {
                foreach (var ad in adjectives)
                {
                    int distance = GetLevenshteinDistance(word, ad);
                    if (distance < 2)
                    {
                        strength++;
                    }
                }
            }

            return strength;
        }
    }
}
