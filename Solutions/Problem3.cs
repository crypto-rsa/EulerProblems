using System.Linq;
using Tools;

namespace EulerProblems.Solutions
{
    /// <summary>
    /// Problem #3 (https://projecteuler.net/problem=3)
    /// </summary>
    public class Problem3 : IProblem<SingleLimitProblemArgs>
    {
        #region Methods

        public string Solve( SingleLimitProblemArgs arguments )
        {
            return new Primes().Factor( arguments.Limit ).Last().prime.ToString();
        }

        #endregion
    }
}
