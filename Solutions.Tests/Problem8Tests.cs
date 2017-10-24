using NUnit.Framework;

namespace EulerProblems.Solutions.Tests
{
    [TestFixture]
    internal class Problem8Tests
    {
        #region Methods

        [Test]
        [TestCase( "00000", 3, 0 )]
        [TestCase( "011111", 4, 1 )]
        [TestCase( "122222", 5, 32 )]
        [TestCase( "999909999", 7, 0 )]
        [TestCase( "99123499", 5, 2 * 3 * 4 * 9 * 9 )]
        [TestCase( "12345678912345678990", 5, 6 * 7 * 8 * 9 * 9 )]
        [TestCase( "222202222202222", 5, 32 )]
        public void SolutionsShouldMatch( string input, int sequenceLength, long expectedValue )
        {
            var problem = new Problem8();

            var result = problem.Solve( new Problem8.Arguments( input, sequenceLength ) );

            Assert.AreEqual( expectedValue.ToString(), result );
        }

        #endregion
    }
}
