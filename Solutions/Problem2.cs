using System;

namespace EulerProblems.Solutions
{
    /// <summary>
    /// Problem #2 (https://projecteuler.net/problem=2)
    /// </summary>
    public class Problem2 : IProblem<SingleLimitProblemArgs>
    {
        #region Methods

        public string Solve( SingleLimitProblemArgs args )
        {
            long[] fib = { 1, 1, 2 };
            long sum = 0;

            while( fib[0] + fib[1] <= args.Limit )
            {
                fib[2] = fib[0] + fib[1];
                sum += fib[2];

                fib[0] = fib[1] + fib[2];
                fib[1] = fib[2] + fib[0];
            }

            return sum.ToString();
        }

        #endregion
    }
}
