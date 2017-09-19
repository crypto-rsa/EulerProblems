namespace EulerProblems.Solutions
{
    /// <summary>
    /// Represents a single problem
    /// </summary>
    /// <typeparam name="TArgs">The type of the problem arguments</typeparam>
    public interface IProblem<in TArgs>
    {
        #region Methods

        /// <summary>
        /// Solves the problem
        /// </summary>
        /// <param name="arguments">The problem arguments</param>
        /// <returns>The string representation of the problem solution</returns>
        string Solve( TArgs arguments );

        #endregion
    }
}
