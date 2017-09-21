using Tools;

namespace EulerProblems.Solutions
{
    /// <summary>
    /// Problem #5 (https://projecteuler.net/problem=5)
    /// </summary>
    public class Problem5 : IProblem<SingleLimitProblemArgs>
    {
        #region Methods

        public string Solve( SingleLimitProblemArgs arguments )
        {
            long result = 1;
            foreach( var factor in Primes.GetPrimes( arguments.Limit ) )
            {
                long n = 1;
                while( n * factor <= arguments.Limit )
                {
                    n *= factor;
                }

                result *= n;
            }

            return result.ToString();
        }

        #endregion
    }
}
