using System.Linq;
using Tools;

namespace EulerProblems.Solutions
{
    /// <summary>
    /// Problem #7 (https://projecteuler.net/problem=7)
    /// </summary>
    public class Problem7 : IProblem<SingleLimitProblemArgs>
    {
        #region Methods

        public string Solve( SingleLimitProblemArgs arguments )
        {
            return Primes.GetAllPrimes().Take( (int) arguments.Limit ).Last().ToString();
        }

        #endregion
    }
}
