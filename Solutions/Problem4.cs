using System;
using Tools;

namespace EulerProblems.Solutions
{
    /// <summary>
    /// Problem #4 (https://projecteuler.net/problem=4)
    /// </summary>
    public class Problem4 : IProblem<SingleLimitProblemArgs>
    {
        #region Methods

        public string Solve( SingleLimitProblemArgs arguments )
        {
            long maxFactor = (long) Math.Pow( 10, arguments.Limit ) - 1;
            long minFactor = (long) Math.Pow( 10, arguments.Limit - 1 );

            long maxPalindrome = 0;

            for( long f1 = maxFactor; f1 >= minFactor; f1-- )
            {
                if( f1 * maxFactor < maxPalindrome )
                    break;

                for( long f2 = maxFactor; f2 >= minFactor; f2-- )
                {
                    long result = f1 * f2;

                    if( result > maxPalindrome && Palindromes.IsPalindromic( result, 10 ) )
                    {
                        maxPalindrome = result;
                    }

                    if( result <= maxPalindrome )
                        break;
                }
            }

            return maxPalindrome.ToString();
        }

        #endregion
    }
}
