using NUnit.Framework;

namespace Tools.Tests
{
    [TestFixture]
    internal class NumbersTests
    {
        [TestCase( 843, 0, 1 )]
        [TestCase( 2, 2, 4 )]
        [TestCase( 3, 5, 243 )]
        [TestCase( 1500, 1, 1500 )]
        [TestCase( 14, 7, 105_413_504 )]
        public void Pow_ShouldReturnCorrectValue( long @base, long exponent, long expected )
        {
            Assert.That( Numbers.Pow( @base, exponent ), Is.EqualTo( expected ) );
        }
    }
}
