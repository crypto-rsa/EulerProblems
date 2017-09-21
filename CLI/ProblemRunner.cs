using System;
using EulerProblems.Solutions;

namespace EulerProblems.CLI
{
    /// <summary>
    /// Represents a launcher for the problem solutions
    /// </summary>
    internal static class ProblemRunner
    {
        public static string Solve( int number )
        {
            switch( number )
            {
                case 1: return new Problem1().Solve( new SingleLimitProblemArgs( 1000 ) );
                case 2: return new Problem2().Solve( new SingleLimitProblemArgs( 4_000_000 ) );
                case 3: return new Problem3().Solve( new SingleLimitProblemArgs( 600_851_475_143 ) );
                case 4: return new Problem4().Solve( new SingleLimitProblemArgs( 3 ) );
                case 5: return new Problem5().Solve( new SingleLimitProblemArgs( 20 ) );
            }

            throw new NotImplementedException();
        }
    }
}
