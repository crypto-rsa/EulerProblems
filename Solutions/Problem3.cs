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
            return Factorization.Of( arguments.Limit ).Factors.Last().Prime.ToString();
        }

        #endregion
    }
}
