using System;
using System.Collections.Generic;
using NUnit.Framework;

namespace Tools.Tests
{
    [TestFixture]
    internal class FactorizationTests
    {
        [TestCase( 0 )]
        [TestCase( -10 )]
        public void Of_ShouldThrowArgumentOutOfRangeException_WhenLessThanOne( long number )
        {
            Assert.That( () => Factorization.Of( number ), Throws.Exception.TypeOf<ArgumentOutOfRangeException>() );
        }

        [Test]
        public void Of_ShouldReturnEmpty_ForOne()
        {
            Assert.That( Factorization.Of( 1 ), Is.EqualTo( Factorization.Empty ) );
        }

        [TestCaseSource( nameof( GetFactorizationTestCases ) )]
        public void Of_ShouldReturnCorrectValue( long number, IEnumerable<(long, int)> expected )
        {
            Assert.That( Factorization.Of( number ), Is.EqualTo( new Factorization( expected ) ) );
        }

        private static IEnumerable<object[]> GetFactorizationTestCases()
        {
            yield return new object[] { 2, new(long, int)[] { (2, 1), (11, 0) } };
            yield return new object[] { 37, new(long, int)[] { (3, 0), (5, 0), (37, 1) } };
            yield return new object[] { 48, new(long, int)[] { (2, 4), (3, 1) } };
            yield return new object[] { 128, new(long, int)[] { (2, 7) } };
            yield return new object[] { 900, new(long, int)[] { (2, 2), (3, 2), (5, 2) } };
            yield return new object[] { 1_348, new(long, int)[] { (2, 2), (337, 1) } };
            yield return new object[] { 8_778, new(long, int)[] { (2, 1), (3, 1), (7, 1), (11, 1), (19, 1) } };
            yield return new object[] { 48_841, new(long, int)[] { (13, 2), (17, 2) } };
        }

        [Test]
        public void OfFactorial_ShouldReturnEmpty_ForOne()
        {
            Assert.That( Factorization.OfFactorial( 1 ), Is.EqualTo( Factorization.Empty ) );
        }

        [TestCaseSource( nameof( GetFactorialFactorizationTestCases ) )]
        public void OfFactorial_ShouldReturnCorrectValue( int number, IEnumerable<(long, int)> expected )
        {
            Assert.That( Factorization.OfFactorial( number ), Is.EqualTo( new Factorization( expected ) ) );
        }

        private static IEnumerable<object[]> GetFactorialFactorizationTestCases()
        {
            yield return new object[] { 0, new(long, int)[] { } };
            yield return new object[] { 2, new(long, int)[] { (2, 1) } };
            yield return new object[] { 3, new(long, int)[] { (2, 1), (3, 1) } };
            yield return new object[] { 5, new(long, int)[] { (2, 3), (3, 1), (5, 1) } };
            yield return new object[] { 25, new(long, int)[] { (2, 22), (3, 10), (5, 6), (7, 3), (11, 2), (13, 1), (17, 1), (19, 1), (23, 1) } };
        }

        [TestCaseSource( nameof( GetFactorizationTestCases ) )]
        public void ToNumber_ShouldReturnCorrectValue( long number, IEnumerable<(long, int)> factors )
        {
            Assert.That( new Factorization( factors ).ToNumber(), Is.EqualTo( number ) );
        }

        [TestCase( 25, 5, 2 )]
        [TestCase( 25, 3, 0 )]
        [TestCase( 128, 2, 7 )]
        [TestCase( 210, 11, 0 )]
        public void GetExponent_ShouldReturnCorrectValue( long number, long prime, long expected )
        {
            Assert.That( Factorization.Of( number ).GetExponent( prime ), Is.EqualTo( expected ) );
        }

        [TestCase( 15, 6 )]
        [TestCase( 22, 12 )]
        public void OperatorDivide_ThrowsInvalidOperationException_WhenNotMultiple( long dividend, long divisor )
        {
            Assert.That( () => Factorization.Of( dividend ) / Factorization.Of( divisor ), Throws.InvalidOperationException );
        }

        [TestCase( 15, 3, 5 )]
        [TestCase( 99, 1, 99 )]
        [TestCase( 180, 4, 45 )]
        public void OperatorDivide_ShouldReturnCorrectValue( long dividend, long divisor, long expected )
        {
            Assert.That( Factorization.Of( dividend ) / Factorization.Of( divisor ), Is.EqualTo( Factorization.Of( expected ) ) );
        }

    }
}
