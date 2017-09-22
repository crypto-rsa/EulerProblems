using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace EulerProblems.Solutions.Tests
{
    [TestFixture]
    internal class Problem4Tests
    {
        #region Methods

        [Test]
        [TestCase( 2, 9009 )]
        [TestCase( 3, 906609 )]
        public void SolutionsShouldMatch( int length, long expectedValue )
        {
            var problem = new Problem4();

            var result = problem.Solve( new SingleLimitProblemArgs( length ) );

            Assert.AreEqual( expectedValue.ToString(), result );
        }

        #endregion
    }
}
