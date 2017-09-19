using NUnit.Framework;

namespace EulerProblems.Solutions.Tests
{
    [TestFixture]
    internal class Problem2Tests
    {
        #region Methods

        [Test]
        [TestCaseSource( nameof( TestCases ) )]
        public void SolutionShouldMatch( SingleLimitProblemArgs arguments, string expectedResult )
        {
            var problem = new Problem2();

            var result = problem.Solve( arguments );

            Assert.AreEqual( result, expectedResult );
        }

        private static object[] TestCases =
        {
            new object[] { new SingleLimitProblemArgs( 89 ), 44.ToString() },
            new object[] { new SingleLimitProblemArgs( 143 ), 44.ToString() },
            new object[] { new SingleLimitProblemArgs( 144 ), 188.ToString() },
        };

        #endregion
    }
}
