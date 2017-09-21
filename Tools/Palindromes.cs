using System;
using System.Collections.Generic;
using System.Linq;

namespace Tools
{
    /// <summary>
    /// Contains methods for working with palindromes
    /// </summary>
    public static class Palindromes
    {
        #region Methods

        public static bool IsPalindromic( long number, byte @base )
        {
            if( @base < 2 )
                throw new ArgumentOutOfRangeException( nameof( @base ), "The base must be at least 2!" );

            var digits = new List<int>( 64 );

            long n = number;
            while( n > 0 )
            {
                int d = (int) (n % @base);
                n /= @base;

                digits.Add( d );
            }

            return Enumerable.Range( 0, (digits.Count + 1) / 2 ).All( i => digits[i] == digits[digits.Count - i - 1] );
        }

        #endregion
    }
}
