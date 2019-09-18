using NUnit.Framework;

namespace EulerProblems.Solutions.Tests
{
    internal class Problem206Tests
    {
        #region Methods

        [TestCase( new int[] { 4, -1, 1 }, 12 )]
        [TestCase( new int[] { 0, -1, 9, -1, 8, -1, 3, 3 }, 5830 )]
        [TestCase( new int[] { 0, -1, 9, -1, 8, -1, 7, -1, 6, -1, 5, -1, 4, -1, 3, -1, 2 }, 156025830)]
        [TestCase( new int[] { 0, -1, 9, -1, 8, -1, 7, -1, 4, -1, 6, -1, 8, -1, 8, -1, 8, -1, 3 }, 1786025830)]
        public void SolutionsShouldMatch( int[] digits, long expectedResult )
        {
            var problem = new Problem206();

            var result = problem.Solve( new Problem206.Arguments( digits ) );

            Assert.AreEqual(expectedResult.ToString(), result);
        }

        #endregion
    }
}
