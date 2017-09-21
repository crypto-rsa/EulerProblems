using NUnit.Framework;

namespace EulerProblems.Solutions.Tests
{
    [TestFixture]
    internal class Problem5Tests
    {
        #region Methods

        [Test]
        [TestCase( 10, 2520 )]
        public void SolutionsShouldMatch( int maxNumber, long expectedValue )
        {
            var problem = new Problem5();

            var result = problem.Solve( new SingleLimitProblemArgs( maxNumber ) );

            Assert.AreEqual( result, expectedValue.ToString() );
        }

        #endregion
    }
}
