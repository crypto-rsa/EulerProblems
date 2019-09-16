using NUnit.Framework;

namespace EulerProblems.Solutions.Tests
{
    [TestFixture]
    internal class Problem679Tests
    {
        #region Methods

        [TestCase( 8, 0 )]
        [TestCase( 9, 1 )]
        [TestCase( 15, 72863)]
        public void SolutionsShouldMatch( int maxLength, long expectedValue )
        {
            var problem = new Problem679();

            var result = problem.Solve( new SingleLimitProblemArgs( maxLength ) );

            Assert.AreEqual( expectedValue.ToString(), result );
        }

        #endregion
    }
}
