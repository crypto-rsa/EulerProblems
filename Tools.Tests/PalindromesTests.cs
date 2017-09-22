using NUnit.Framework;

namespace Tools.Tests
{
    [TestFixture]
    internal class PalindromesTests
    {
        #region Methods

        [Test]
        [TestCase( 1, 10, true )]
        [TestCase( 9, 10, true )]
        [TestCase( 2221, 10, false )]
        [TestCase( 0b11011, 2, true )]
        [TestCase( 0xABBA, 16, true )]
        [TestCase( 15, 10, false )]
        [TestCase( 0b101111, 2, false )]
        [TestCase( 0xDEADBEEF, 16, false )]
        public void IsPalindromic_ShouldBeTrueForPalindromesOnly( long number, byte @base, bool expectedValue )
        {
            bool value = Palindromes.IsPalindromic( number, @base );

            Assert.AreEqual( expectedValue, value );
        }

        #endregion
    }
}
