﻿using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

namespace Tools.Tests
{
    [TestFixture]
    internal class PrimesTests
    {
        #region Methods

        [Test]
        [TestCase( 1, new long[] { } )]
        [TestCase( 2, new long[] { 2 } )]
        [TestCase( 10, new long[] { 2, 3, 5, 7 } )]
        [TestCase( 19, new long[] { 2, 3, 5, 7, 11, 13, 17, 19 } )]
        public void GetPrimes_ShouldReturnCorrectSequence( int upperLimit, long[] expectedValues )
        {
            var primes = new Primes();

            var values = primes.GetPrimes( upperLimit );

            Assert.IsTrue( values.SequenceEqual( expectedValues ) );
        }

        [Test]
        [TestCase( 0, false )]
        [TestCase( 1, false )]
        [TestCase( 2, true )]
        [TestCase( 11, true )]
        [TestCase( 14, false )]
        [TestCase( 35, false )]
        [TestCase( 5827, true )]
        [TestCase( 10_000, false )]
        public void IsPrime_ShouldBeTrueForPrimesOnly( long number, bool expectedValue )
        {
            var primes = new Primes();

            bool value = primes.IsPrime( number );

            Assert.AreEqual( value, expectedValue );
        }

        [Test]
        [TestCaseSource( nameof( GetFactorTestCases ) )]
        public void Factor_ShouldReturnCorrectFactorization( long number, (long, int)[] expectedValues )
        {
            var primes = new Primes();

            var values = primes.Factor( number );

            Assert.IsTrue( values.SequenceEqual( expectedValues ) );
        }

        private static IEnumerable<object[]> GetFactorTestCases()
        {
            yield return new object[] { 1, new(long, int)[] { } };
            yield return new object[] { 2, new(long, int)[] { (2, 1) } };
            yield return new object[] { 25, new(long, int)[] { (5, 2) } };
            yield return new object[] { 30, new(long, int)[] { (2, 1), (3, 1), (5, 1) } };
            yield return new object[] { 128, new(long, int)[] { (2, 7) } };
            yield return new object[] { 48_841, new(long, int)[] { (13, 2), (17, 2) } };
        }

        #endregion
    }
}
