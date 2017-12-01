using System;

namespace Tools
{
    /// <summary>
    /// Contains combinatorics methods
    /// </summary>
    public static class Combinatorics
    {
        #region Methods

        /// <summary>
        /// Calculates the factorial of a number
        /// </summary>
        /// <param name="number"></param>
        /// <returns></returns>
        public static long Factorial( long number )
        {
            if( number < 0 || number > 20 )
                throw new ArgumentOutOfRangeException( nameof( number ) );

            if( number <= 1 )
                return 1;

            return number * Factorial( number - 1 );
        }

        #endregion
    }
}
