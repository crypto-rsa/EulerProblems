using System.Collections.Generic;
using System.Linq;
using Tools;

namespace EulerProblems.Solutions
{
    /// <summary>
    /// Problem #152 (https://projecteuler.net/problem=152)
    /// </summary>
    public class Problem152 : IProblem<SingleLimitProblemArgs>
    {
        private class ModNumber
        {
            #region Constructors

            public ModNumber(IReadOnlyList<long> remainders)
            {
                Remainders = remainders;
            }

            #endregion

            #region Properties

            public static IReadOnlyList<long> Moduli { get; set; }

            public IReadOnlyList<long> Remainders { get; }

            public long MinSize { get; init; }

            public long MaxSize { get; init; }

            #endregion

            #region Overrides

            public override bool Equals(object obj)
            {
                if (obj is not ModNumber other)
                    return false;

                for (int i = 0; i < Moduli.Count; i++)
                {
                    if (Remainders[i] != other.Remainders[i])
                        return false;
                }

                return true;
            }

            public override int GetHashCode()
            {
                int hashCode = 17;

                for (int i = 0; i < 6; i++)
                {
                    if (i >= Remainders.Count)
                        break;

                    hashCode = (int)(hashCode + 31 * Remainders[i]);
                }

                return hashCode;
            }

            public override string ToString() => $"[{string.Join(", ", Remainders)}]";

            #endregion

            #region Methods

            public static ModNumber operator +(ModNumber first, ModNumber second)
            {
                var list = new List<long>(Moduli.Count);

                for (int i = 0; i < Moduli.Count; i++)
                {
                    list.Add((first.Remainders[i] + second.Remainders[i]) % Moduli[i]);
                }

                return new ModNumber(list)
                {
                    MinSize = first.MinSize + second.MinSize,
                    MaxSize = first.MaxSize + second.MaxSize,
                };
            }

            public static ModNumber GetZero() => new(Moduli.Select(_ => 0L).ToList());

            #endregion
        }

        public string Solve(SingleLimitProblemArgs arguments)
        {
            // Since we're dealing with fractions, we will instead represent them as if multiplied by their LCM.
            // Also, since the LCM is too big, we will store just its prime factorization. Same for the partial sums.
            // Then, the factorization of each input number 1 / i^2 will be missing precisely the prime powers in i^2.
            // The factorization of 1/2 is the same as the factorization of the LCM, except the power of 2 is one less.
            // Therefore, for each other prime p, the remainders of the summands modulo p must cancel out to zero.
            // We can precalculate all possible remainders for each prime after each step.
            // Also, we can calculate the approximate size of each partial sum as a multiple of 'steps'.
            // The 'stepCount' is calculated in such a way that the size of the last two fractions differs by at least 1.
            // We can then discard sums which are either too small or too large.

            int maxNumber = (int)arguments.Limit;
            int numCount = maxNumber - 1;

            long stepCount = Square(maxNumber) * Square(maxNumber - 1) / (Square(maxNumber) - Square(maxNumber - 1)) + 1;

            if (stepCount % 2 == 1)
            {
                stepCount++;
            }

            var factorizations = Enumerable.Range(2, numCount).Select(i => i > 0 ? Factorization.Of(i * i) : null).ToArray();
            var primes = factorizations.SelectMany(f => f.Keys).Distinct().OrderBy(l => l).ToList();
            var lcm = new Factorization(primes.Select(p => (p, factorizations.Max(f => f.GetExponent(p)))));

            ModNumber.Moduli = primes.Select(p => Numbers.Pow(p, lcm[p])).ToList();

            var targetNumber = new ModNumber(primes.Select((p, i) => Numbers.Pow(p, lcm[p] - (p == 2 ? 1 : 0)) % ModNumber.Moduli[i]).ToList())
            {
                MinSize = stepCount / 2,
                MaxSize = stepCount / 2,
            };

            var modNumbers = factorizations.Select(GetMod).ToList();

            var possibleRemainders = new HashSet<long>[ModNumber.Moduli.Count][];

            for (int modIndex = 0; modIndex < ModNumber.Moduli.Count; modIndex++)
            {
                possibleRemainders[modIndex] = new HashSet<long>[numCount + 1];

                possibleRemainders[modIndex][numCount] = new HashSet<long> { 0 };

                for (int usedCount = numCount - 1; usedCount >= 0; usedCount--)
                {
                    var prev = possibleRemainders[modIndex][usedCount + 1];
                    var cur = new HashSet<long>(prev);

                    foreach (long n in prev)
                    {
                        cur.Add((n + modNumbers[usedCount].Remainders[modIndex]) % ModNumber.Moduli[modIndex]);
                    }

                    possibleRemainders[modIndex][usedCount] = cur;
                }
            }

            var fullSum = new long[numCount + 1];
            fullSum[numCount] = 0;

            for (int usedCount = numCount - 1; usedCount >= 0; usedCount--)
            {
                fullSum[usedCount] = fullSum[usedCount + 1] + modNumbers[usedCount].MaxSize;
            }

            var ways = new Dictionary<ModNumber, long>
            {
                [ModNumber.GetZero()] = 1,
            };

            for (int usedCount = 0; usedCount < numCount; usedCount++)
            {
                var oldWays = new List<KeyValuePair<ModNumber, long>>(ways.Count);
                oldWays.AddRange(ways);

                foreach (var item in oldWays)
                {
                    var sum = item.Key + modNumbers[usedCount];

                    if (sum.MinSize > targetNumber.MaxSize)
                        continue;

                    if (sum.MaxSize + fullSum[usedCount] < targetNumber.MinSize)
                        continue;

                    if (CanExtend(sum, usedCount))
                    {
                        ways.TryGetValue(sum, out long existingCount);

                        ways[sum] = existingCount + item.Value;
                    }
                }
            }

            ways.TryGetValue(targetNumber, out long solutions);

            return solutions.ToString();

            ModNumber GetMod(Factorization f)
            {
                var division = lcm / f;

                var remainders = ModNumber.Moduli
                    .Select(modulus => division.Aggregate(1L, (current, pair) => (current * Numbers.Pow(pair.Key, pair.Value)) % modulus)).ToList();

                return new ModNumber(remainders)
                {
                    MinSize = stepCount / f.ToNumber(),
                    MaxSize = stepCount / f.ToNumber() + 1,
                };
            }

            bool CanExtend(ModNumber sum, int usedCount)
            {
                for (int modIndex = 0; modIndex < ModNumber.Moduli.Count; modIndex++)
                {
                    var targetRemainder = (targetNumber.Remainders[modIndex] + ModNumber.Moduli[modIndex] - sum.Remainders[modIndex]) % ModNumber.Moduli[modIndex];

                    if (!possibleRemainders[modIndex][usedCount].Contains(targetRemainder))
                        return false;
                }

                return true;
            }

            long Square(long n) => n * n;
        }
    }
}
