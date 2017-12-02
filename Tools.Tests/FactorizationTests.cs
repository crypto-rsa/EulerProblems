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
            yield return new object[] { 2, new(long, int)[] { (2, 1) } };
            yield return new object[] { 37, new(long, int)[] { (37, 1) } };
            yield return new object[] { 48, new(long, int)[] { (2, 4), (3, 1) } };
            yield return new object[] { 900, new(long, int)[] { (2, 2), (3, 2), (5, 2) } };
            yield return new object[] { 1_348, new(long, int)[] { (2, 2), (337, 1) } };
            yield return new object[] { 8_778, new(long, int)[] { (2, 1), (3, 1), (7, 1), (11, 1), (19, 1) } };
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
    }
}
