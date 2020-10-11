using System.Collections.Generic;
using System.Linq;

namespace EulerProblems.Solutions
{
    /// <summary>
    /// Problem #143 (https://projecteuler.net/problem=143)
    /// </summary>
    public class Problem143 : IProblem<SingleLimitProblemArgs>
    {
        public string Solve(SingleLimitProblemArgs arguments)
        {
            // The angle at the Torricelli point T of triangle ABT (and similarly the two other ones) is 120 degrees
            // (because AMBT is a cyclic quadrilateral and AMC is an equilateral triangle with angle 60 degrees at M).
            // Thus, from the law of cosines:
            // a^2 = p^2 + q^2 + pq
            // b^2 = p^2 + r^2 + pr
            // c^2 = q^2 + r^2 + qr

            long limit = arguments.Limit;
            long pLimit = limit / 3;
            long limitSquared = limit * limit;

            // precalculate a table of squares (possible values of a^2, b^2, c^2)
            var squares = new HashSet<long>();

            for (long i = 1; ; i++)
            {
                long square = i * i;

                if (square > 3 * limitSquared)
                    break;

                squares.Add(square);
            }

            var candidates = new List<long>();
            var torricelliSums = new HashSet<long>();

            // for each 'p' find a list of 'q' values such that p^2 + q^2 + pq is a square
            for (long p = 1; p <= pLimit; p++)
            {
                candidates.Clear();

                long aSquared = 3 * p * p;
                long increment = 3 * p + 1;

                for (long q = p; q <= limit; q++)
                {
                    if (squares.Contains(aSquared))
                    {
                        candidates.Add(q);
                    }

                    aSquared += increment;
                    increment += 2;
                }

                // the list contains candidates for 'q' and 'r', we will find all pairs such that
                // q^2 + r^2 + qr is a square
                for (int i = 0; i < candidates.Count - 1; i++)
                {
                    long q = candidates[i];

                    for (int j = i; j < candidates.Count; j++)
                    {
                        long r = candidates[j];
                        long candidate = p + q + r;

                        if (candidate > limit)
                            break;

                        if (squares.Contains(q * q + r * r + q * r))
                        {
                            torricelliSums.Add(candidate);
                        }
                    }
                }
            }

            return torricelliSums.Sum().ToString();
        }
    }
}
