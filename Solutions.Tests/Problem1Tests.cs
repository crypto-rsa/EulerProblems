using NUnit.Framework;

namespace EulerProblems.Solutions.Tests
{
    [TestFixture]
    internal class Problem1Tests
    {
        #region Methods

        [Test]
        [TestCaseSource( nameof( TestCases ) )]
        public void SolutionShouldMatch( SingleLimitProblemArgs arguments, string expectedResult )
        {
            var problem = new Problem1();

            var result = problem.Solve( arguments );

            Assert.AreEqual( result, expectedResult );
        }

        private static object[] TestCases =
        {
            new object[] { new SingleLimitProblemArgs( 10 ), 23.ToString() },
            new object[] { new SingleLimitProblemArgs( 20 ), 78.ToString() },
        };

        #endregion
    }
}
