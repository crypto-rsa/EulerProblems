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
            var values = Primes.GetPrimes( upperLimit );

            Assert.IsTrue( values.SequenceEqual( expectedValues ) );
        }

        [Test]
        [TestCase( 0, new long[] { } )]
        [TestCase( 1, new long[] { 2 } )]
        [TestCase( 5, new long[] { 2, 3, 5, 7, 11 } )]
        public void GetAllPrimes_ShouldReturnCorrectSequence( int count, long[] expectedValues )
        {
            var values = Primes.GetAllPrimes().Take( count );

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
            bool value = Primes.IsPrime( number );

            Assert.AreEqual( expectedValue, value );
        }

        #endregion
    }
}
