using System.Collections.Generic;
using Linguist.DataLayer.Model;

namespace Linguist.Web.Extensions
{
    internal static class WordFormattingExtensions
    {
        public static IEnumerable<Word> TransformStarSigns(this IEnumerable<Word> words)
        {
            foreach (var word in words)
            {
                var substrings = word.OriginalWord.Split('*');

                for (int i = 1; i < substrings.Length; i++)
                {
                    if (substrings[i].Length == 0)
                        continue;

                    substrings[i] = substrings[i].Insert(1, "</b>");
                    substrings[i] = "<b>" + substrings[i];
                }

                word.OriginalWord = string.Join("", substrings);
            }

            return words;
        }

        public static string RemoveStarSigns(this string word)
        {
            return word.Replace("*", string.Empty).Replace("<b>", string.Empty).Replace("</b>", string.Empty);
        }
    }
}