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
            }

            throw new NotImplementedException();
        }
    }
}
