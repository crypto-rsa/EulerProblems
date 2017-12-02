using System;
using NUnit.Framework;

namespace Tools.Tests
{
    [TestFixture]
    internal class CombinatoricsTests
    {
        #region Methods

        [TestCase( -10 )]
        [TestCase( -1 )]
        [TestCase( 21 )]
        public void Factorial_ShouldThrowArgumentOutOfRangeException_WhenLessThanZeroOrGreaterThanTwenty( long number )
        {
            Assert.That( () => Combinatorics.Factorial( number ), Throws.Exception.TypeOf<ArgumentOutOfRangeException>() );
        }

        [TestCase( 0, 1 )]
        [TestCase( 1, 1 )]
        [TestCase( 4, 24 )]
        [TestCase( 10, 3_628_800 )]
        [TestCase( 15, 1_307_674_368_000 )]
        public void Factorial_ShouldReturnCorrectValue( long number, long expected )
        {
            Assert.That( Combinatorics.Factorial( number ), Is.EqualTo( expected ) );
        }

        [TestCase( new long[] { 2, 5 }, 1 )]
        [TestCase( new long[] { 6, 15 }, 3 )]
        [TestCase( new long[] { 6, 10, 15, }, 1 )]
        [TestCase( new long[] { 99, 121 }, 11 )]
        [TestCase( new long[] { 75, 243, 231 }, 3 )]
        public void GCD_ShouldReturnCorrectValue( long[] numbers, long expected )
        {
            Assert.That( Combinatorics.GCD( numbers ), Is.EqualTo( Factorization.Of( expected ) ) );
        }

        #endregion
    }
}
