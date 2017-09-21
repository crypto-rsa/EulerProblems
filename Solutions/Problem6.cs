namespace EulerProblems.Solutions
{
    /// <summary>
    /// Problem #6 (https://projecteuler.net/problem=6)
    /// </summary>
    public class Problem6 : IProblem<SingleLimitProblemArgs>
    {
        #region Methods

        public string Solve( SingleLimitProblemArgs arguments )
        {
            long result = 0;

            for( long i = 0; i <= arguments.Limit; i++ )
            {
                for( long j = 0; j <= arguments.Limit; j++ )
                {
                    if( i == j )
                        continue;

                    result += i * j;
                }
            }

            return result.ToString();
        }

        #endregion
    }
}
