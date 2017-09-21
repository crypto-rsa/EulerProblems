using NUnit.Framework;

namespace EulerProblems.Solutions.Tests
{
    [TestFixture]
    internal class Problem7Tests
    {
        #region Methods

        [Test]
        [TestCase( 6, 13 )]
        [TestCase( 25, 97 )]
        public void SolutionsShouldMatch( int primeOrder, long expectedValue )
        {
            var problem = new Problem7();

            var result = problem.Solve( new SingleLimitProblemArgs( primeOrder ) );

            Assert.AreEqual( result, expectedValue.ToString() );
        }

        #endregion
    }
}
