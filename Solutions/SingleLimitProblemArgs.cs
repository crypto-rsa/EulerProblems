namespace EulerProblems.Solutions
{
    /// <summary>
    /// Represents problem arguments with a single limit of type <see cref="long"/>
    /// </summary>
    public class SingleLimitProblemArgs
    {
        #region Constructors

        /// <summary>
        /// Constructs an instance of the <see cref="SingleLimitProblemArgs"/> with the given limit
        /// </summary>
        /// <param name="limit">The limit</param>
        public SingleLimitProblemArgs( long limit )
        {
            Limit = limit;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or set the limit
        /// </summary>
        public long Limit { get; private set; }

        #endregion
    }
}
