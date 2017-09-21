using NUnit.Framework;

namespace EulerProblems.Solutions.Tests
{
    [TestFixture]
    internal class Problem6Tests
    {
        #region Methods

        [Test]
        [TestCase( 10, 2640 )]
        public void SolutionsShouldMatch( int maxNumber, long expectedValue )
        {
            var problem = new Problem6();

            var result = problem.Solve( new SingleLimitProblemArgs( maxNumber ) );

            Assert.AreEqual( result, expectedValue.ToString() );
        }

        #endregion
    }
}
