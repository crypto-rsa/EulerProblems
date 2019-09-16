using System.Collections.Generic;
using System.Linq;

namespace EulerProblems.Solutions
{
    public class Problem679 : IProblem<SingleLimitProblemArgs>
    {
        #region Fields

        private readonly List<char> _chars = new List<char> { 'A', 'E', 'F', 'R' };

        private readonly List<string> _keywords = new List<string> { "FREE", "FARE", "AREA", "REEF" };

        #endregion

        #region Methods

        public string Solve(SingleLimitProblemArgs arguments)
        {
            int maxLength = (int)arguments.Limit;
            var wordCounts = new Dictionary<(string Suffix, int Mask), long>[maxLength + 1];

            var suffixes = GetSuffixes().ToList();
            var transitions = GetTransitions();
            var masks = _keywords.Select((_, i) => (1 << i)).ToArray();
            int fullMask = (1 << _keywords.Count) - 1;

            wordCounts[0] = new Dictionary<(string Suffix, int Mask), long>()
            {
                [(string.Empty, 0)] = 1
            };

            for (int length = 1; length <= maxLength; length++)
            {
                wordCounts[length] = new Dictionary<(string Suffix, int Mask), long>();

                foreach (var item in wordCounts[length - 1])
                {
                    foreach (var nextChar in _chars)
                    {
                        var (keywordIndex, newSuffix) = transitions[(item.Key.Suffix, nextChar)];

                        int newMask = item.Key.Mask;

                        if (keywordIndex >= 0)
                        {
                            // disallow duplicate occurrences
                            if ((item.Key.Mask & masks[keywordIndex]) == masks[keywordIndex])
                                continue;

                            newMask |= masks[keywordIndex];
                        }

                        var newKey = (newSuffix, newMask);

                        if (!wordCounts[length].ContainsKey(newKey))
                        {
                            wordCounts[length][newKey] = 0;
                        }

                        wordCounts[length][newKey] += item.Value;
                    }
                }
            }

            var total = wordCounts[maxLength].Where(i => i.Key.Mask == fullMask).Sum(i => i.Value);

            return total.ToString();

            IEnumerable<string> GetSuffixes()
            {
                var parts = new List<string> { string.Empty };
                parts.AddRange(_chars.Select(c => c.ToString()));

                return parts.SelectMany(s1 => parts.SelectMany(s2 => parts.Select(s3 => $"{s1}{s2}{s3}"))).Distinct();
            }

            Dictionary<(string OldSuffix, char NextChar), (int KeywordIndex, string NewSuffix)> GetTransitions()
            {
                var dictionary = new Dictionary<(string, char), (int, string)>();

                foreach (var suffix in suffixes)
                {
                    foreach (var lastChar in _chars)
                    {
                        var word = suffix + lastChar.ToString();

                        dictionary[(suffix, lastChar)] = (_keywords.FindIndex(s => s.Equals(word)), word.Substring(System.Math.Max(0, word.Length - 3)));
                    }
                }

                return dictionary;
            }
        }

        #endregion
    }
}
