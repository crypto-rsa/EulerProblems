using System;
using System.Collections.Generic;
using System.Linq;

namespace EulerProblems.Solutions;

public class Problem828 : IProblem<InputFileProblemArgs>
{
    #region Methods

    private static int Solve(long target, int[] numbers)
    {
        var fullMask = (1 << numbers.Length) - 1;

        var solutions = new Dictionary<int, HashSet<long>>();
        var maskScore = new int[fullMask + 1];
        var maskBits = new int[fullMask + 1];

        for (var i = 0; i <= fullMask; i++)
        {
            maskScore[i] = Enumerable.Range(0, numbers.Length).Sum(b => (i & (1 << b)) != 0 ? numbers[b] : 0);
            maskBits[i] = Enumerable.Range(0, numbers.Length).Count(b => (i & (1 << b)) != 0);
        }

        for (var i = 0; i < numbers.Length; i++)
        {
            solutions[(1 << i)] = new HashSet<long> { numbers[i] };
        }

        var minScore = solutions.Where(p => p.Value.Contains(target)).Select(p => maskScore[p.Key]).FirstOrDefault();

        for (var count = 2; count <= numbers.Length; count++)
        {
            foreach (var group in GetSolutions(count, minScore).GroupBy(i => i.Mask))
            {
                var solutionSet = new HashSet<long>(group.Select(i => i.Value));
                solutions[group.Key] = solutionSet;

                if ((minScore == 0 || maskScore[group.Key] < minScore) && solutionSet.Contains(target))
                {
                    minScore = maskScore[group.Key];
                }
            }
        }

        return minScore;

        IEnumerable<(int Mask, long Value)> GetSolutions(int count, int currentScore)
        {
            foreach (var leftSolution in solutions)
            {
                foreach (var rightSolution in solutions)
                {
                    if ((leftSolution.Key & rightSolution.Key) != 0)
                        continue;

                    var mask = leftSolution.Key | rightSolution.Key;

                    if (maskBits[mask] != count)
                        continue;

                    if (currentScore > 0 && maskScore[mask] > currentScore)
                        continue;

                    foreach (var left in leftSolution.Value)
                    {
                        foreach (var right in rightSolution.Value)
                        {
                            if (left <= right)
                            {
                                yield return (mask, left + right);
                                yield return (mask, left * right);
                            }

                            yield return (mask, left - right);

                            if (right == 0)
                                continue;

                            var quotient = Math.DivRem(left, right, out var remainder);

                            if (remainder != 0)
                                continue;

                            yield return (mask, quotient);
                        }
                    }
                }
            }
        }
    }

    #endregion

    #region IProblem

    public string Solve(InputFileProblemArgs arguments)
    {
        long total = 0;
        long exp = 3;
        const int mod = 1005075251;

        foreach (string line in arguments.Lines)
        {
            var parts = line.Split(':');
            long result = Solve(long.Parse(parts[0]), parts[1].Split(',').Select(int.Parse).ToArray());

            total = (total + exp * result) % mod;
            exp = (exp * 3) % mod;
        }

        return total.ToString();
    }

    #endregion
}
