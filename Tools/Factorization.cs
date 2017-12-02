using System;
using System.Collections.Generic;
using System.Linq;

namespace Tools
{
    /// <summary>
    /// Represents a factorization of a number
    /// </summary>
    public class Factorization : SortedDictionary<long, int>
    {
        #region Fields

        /// <summary>
        /// The empty factorization (ie. factorization of 1)
        /// </summary>
        private static Factorization _empty = new Factorization( Enumerable.Empty<(long, int)>() );

        #endregion

        #region Constructors

        /// <summary>
        /// Constructs the factorization from a sequence of primes and their exponents
        /// </summary>
        /// <param name="factors">The factors represented as a tuples of (prime, exponent)</param>
        public Factorization( IEnumerable<(long Prime, int Exponent)> factors )
        {
            foreach( var item in factors.Where( t => t.Exponent > 0 ) )
            {
                Add( item.Prime, item.Exponent );
            }
        }

        #endregion

        #region Overrides

        public override int GetHashCode()
        {
            return Count;
        }

        public override bool Equals( object obj )
        {
            var other = obj as Factorization;
            if( other == null )
                return false;

            if( !this.OrderBy( i => i.Key ).SequenceEqual( other.OrderBy( i => i.Key ) ) )
                return false;

            return true;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Returns the factorization of a number
        /// </summary>
        /// <param name="number">The number to get the factorization of</param>
        /// <returns>The factorization of <paramref name="number"/></returns>
        public static Factorization Of( long number )
        {
            if( number < 1 )
                throw new ArgumentOutOfRangeException( nameof( number ) );

            if( number == 1 )
                return Empty;

            return new Factorization( Factor( number ) );
        }

        /// <summary>
        /// Factors a number
        /// </summary>
        /// <param name="number">The number to factor</param>
        /// <returns>A sequence of (prime, exponent) tuples representing the prime factorization</returns>
        private static IEnumerable<(long prime, int exponent)> Factor( long number )
        {
            long remaining = number;
            long max = (long) Math.Ceiling( Math.Sqrt( number ) );

            foreach( var prime in Primes.GetPrimes( max ) )
            {
                if( remaining < 2 )
                    yield break;

                int exponent = 0;
                while( remaining % prime == 0 )
                {
                    remaining /= prime;
                    exponent++;
                }

                if( exponent > 0 )
                {
                    yield return (prime, exponent);
                }
            }

            if( remaining > 1 )
            {
                yield return (remaining, 1);
            }
        }

        /// <summary>
        /// Returns the factorization of a factorial
        /// </summary>
        /// <param name="number">The number for which the factorization of a factorial will be calculated</param>
        /// <returns>The factorization of <paramref name="number"/>!</returns>
        public static Factorization OfFactorial( int number )
        {
            return Enumerable.Range( 1, number ).Select( n => Of( n ) ).Aggregate( Empty, ( prev, cur ) => prev * cur );
        }

        /// <summary>
        /// Converts the factorization to a <see cref="long"/> number
        /// </summary>
        /// <returns>The numeric representation of this factorization</returns>
        public long ToNumber()
        {
            return Factors.Aggregate( 1L, ( prev, cur ) => prev * Numbers.Pow( cur.Prime, cur.Exponent ) );
        }

        /// <summary>
        /// Returns the exponent for a prime in this factorization
        /// </summary>
        /// <param name="prime">The prime to get the exponent for</param>
        /// <returns>The exponent for <paramref name="prime"/> (0 if not present in this factorization)</returns>
        public int GetExponent( long prime )
        {
            if( !TryGetValue( prime, out int exponent ) )
                return 0;

            return exponent;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the empty factorization (ie. the factorization of 1)
        /// </summary>
        public static Factorization Empty => _empty;

        /// <summary>
        /// Gets the collection of factors as a named tuple
        /// </summary>
        public IEnumerable<(long Prime, int Exponent)> Factors => this.Select( i => (i.Key, i.Value) );

        #endregion

        #region Operators

        public static Factorization operator *( Factorization factors1, Factorization factors2 )
        {
            return new Factorization( Enumerable.Concat( factors1, factors2 ).GroupBy( p => p.Key ).Select( g => (g.Key, g.Sum( p => p.Value )) ) );
        }

        #endregion
    }
}
