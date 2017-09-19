namespace EulerProblems.Solutions
{
    /// <summary>
    /// Problem #1 (https://projecteuler.net/problem=1)
    /// </summary>
    public class Problem1 : IProblem<SingleLimitProblemArgs>
    {
        #region Methods

        public string Solve( SingleLimitProblemArgs args )
        {
            const int d1 = 3;
            const int d2 = 5;
            const int d3 = d1 * d2;

            long maxMultiple1 = (args.Limit - 1) / d1;
            long maxMultiple2 = (args.Limit - 1) / d2;
            long maxMultiple3 = (args.Limit - 1) / d3;

            long total = 0;
            total += d1 * maxMultiple1 * (maxMultiple1 + 1) / 2;
            total += d2 * maxMultiple2 * (maxMultiple2 + 1) / 2;
            total -= d3 * maxMultiple3 * (maxMultiple3 + 1) / 2;

            return total.ToString();
        }

        #endregion
    }
}
