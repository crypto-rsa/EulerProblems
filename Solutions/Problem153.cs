using System.Collections.Generic;

namespace EulerProblems.Solutions;

/// <summary>
/// Problem #153 (https://projecteuler.net/problem=153)
/// </summary>
public class Problem153 : IProblem<SingleLimitProblemArgs>
{
    public string Solve(SingleLimitProblemArgs arguments)
    {
        long maxNumber = arguments.Limit;
        long total = 0;

        // add all integral divisors
        for (int i = 1; i <= maxNumber; i++)
        {
            total += i * (maxNumber / i);
        }

        // add all complex divisors of the form (k +- k*i)
        for (int k = 1;; k++)
        {
            long m = maxNumber / (k * 2);

            if (m == 0)
                break;

            total += m * 2 * k;
        }

        // add the rest of the complex divisors by finding all coprime pairs (by constructing the Farey sequence)
        var queue = new Queue<(int, int, int, int)>();

        queue.Enqueue((0, 1, 1, 1));

        while (queue.Count > 0)
        {
            (int a1, int b1, int a2, int b2) = queue.Dequeue();

            (int a, int b) = (a1 + a2, b1 + b2);

            long squareSum = a * a + b * b;

            for (int k = 1;; k++)
            {
                long m = maxNumber / (k * squareSum);

                if (m == 0)
                    break;

                // 'm' numbers are divisible by (k*a +- k*i*b)
                total += m * 2 * a * k;

                if (a != b)
                {
                    total += m * 2 * b * k;
                }
            }

            if (a2 > 0 && squareSum < maxNumber)
            {
                queue.Enqueue((a1, b1, a, b));
                queue.Enqueue((a, b, a2, b2));
            }
        }

        return total.ToString();
    }
}
