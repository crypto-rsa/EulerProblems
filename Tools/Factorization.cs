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
            foreach( var item in factors )
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

            return new Factorization( Primes.Factor( number ) );
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
