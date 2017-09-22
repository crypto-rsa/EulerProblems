using NUnit.Framework;

namespace EulerProblems.Solutions.Tests
{
    [TestFixture]
    internal class Problem3Tests
    {
        #region Methods

        [Test]
        [TestCase(2, 2)]
        [TestCase(8, 2)]
        [TestCase(21, 7)]
        [TestCase(97, 97)]
        [TestCase(1000, 5)]
        public void SolutionShouldMatch( long number, long expectedResult )
        {
            var problem = new Problem3();

            var result = problem.Solve( new SingleLimitProblemArgs( number ) );

            Assert.AreEqual( expectedResult.ToString(), result );
        }

        #endregion
    }
}
