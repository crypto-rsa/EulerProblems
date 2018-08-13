﻿using System;
using System.Collections.Generic;
using EulerProblems.Solutions;

namespace EulerProblems.CLI
{
    /// <summary>
    /// Represents a launcher for the problem solutions
    /// </summary>
    internal static class ProblemRunner
    {
        #region Fields

        private readonly static Dictionary<int, Func<string>> _solvers = InitializeSolvers();

        #endregion

        #region Methods

        private static Dictionary<int, Func<string>> InitializeSolvers()
        {
            return new Dictionary<int, Func<string>>()
            {
                [1] = () => new Problem1().Solve( new SingleLimitProblemArgs( 1000 ) ),
                [2] = () => new Problem2().Solve( new SingleLimitProblemArgs( 4_000_000 ) ),
                [3] = () => new Problem3().Solve( new SingleLimitProblemArgs( 600_851_475_143 ) ),
                [4] = () => new Problem4().Solve( new SingleLimitProblemArgs( 3 ) ),
                [5] = () => new Problem5().Solve( new SingleLimitProblemArgs( 20 ) ),
                [6] = () => new Problem6().Solve( new SingleLimitProblemArgs( 100 ) ),
                [7] = () => new Problem7().Solve( new SingleLimitProblemArgs( 10_001 ) ),
                [8] = () => new Problem8().Solve( Problem8.Arguments.FromFile( @"Input\problem8.txt", 13 ) ),
            };
        }

        public static string Solve( int number )
        {
            if( _solvers.TryGetValue( number, out var solver ) )
                return solver();

            throw new NotImplementedException();
        }

        #endregion
    }
}
