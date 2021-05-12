using System.Collections.Generic;
using System.Linq;

namespace EulerProblems.Solutions
{
    /// <summary>
    /// Problem #201 (https://projecteuler.net/problem=201)
    /// </summary>
    public class Problem201 : IProblem<Problem201.Arguments>
    {
        #region Nested Types

        public record Arguments(int Maximum, int TargetSize);

        #endregion
        
        #region Methods

        public string Solve(Arguments arguments)
        {
            var numbers = Enumerable.Range(1, arguments.Maximum).Select(i => i * i).ToList();
            var uniqueSums = new Dictionary<long, (int Max1, int? Max2)> { [0] = (-1, null) };
            
            // The uniqueSums dictionary holds information about subset of a particular size.
            // The key is the subset sum, the value holds the indices of the *maximum* elements
            // of the subsets with the given sum. If there is just one such subset, the Max2
            // element is null; otherwise Max1 holds the lowest such index and Max2 the second
            // lowest index.

            for (int subsetSize = 1; subsetSize <= arguments.TargetSize; subsetSize++)
            {
                var prev = uniqueSums;
                uniqueSums = new();

                int maxIndex = numbers.Count - (arguments.TargetSize - subsetSize) - 1;

                foreach (var item in prev)
                {
                    for (int i = item.Value.Max1 + 1; i <= maxIndex; i++)
                    {
                        Add(item, i);

                        if (item.Value.Max2.HasValue && i > item.Value.Max2)
                        {
                            // there are at least two ways to create a subset with sum equal to item.Key; we need to keep this information
                            Add(item, i);
                        }
                    }
                }
            }

            var total = uniqueSums.Where(i => !i.Value.Max2.HasValue).Select(i => i.Key).Sum();

            return total.ToString();

            void Add(KeyValuePair<long, (int Max1, int? Max2)> item, int numberIndex)
            {
                var sum = item.Key + numbers[numberIndex];

                var (min, max) = uniqueSums.TryGetValue(sum, out var existing) switch
                {
                    false => (numberIndex, (int?) null),
                    true when !existing.Max2.HasValue => GetMinMax(existing.Max1, numberIndex),
                    _ => GetMinPair(existing.Max1, existing.Max2.Value, numberIndex),
                };

                uniqueSums[sum] = (min, max);
            }

            static (int Min, int Max) GetMinMax(int a, int b) => a > b ? (b, a) : (a, b);

            static (int Min, int Max) GetMinPair(int a, int b, int c)
            {
                if (a >= b && a >= c)
                    return GetMinMax(b, c);

                if (b >= a && b >= c)
                    return GetMinMax(a, c);

                return GetMinMax(a, b);
            }
        }

        #endregion
    }
}