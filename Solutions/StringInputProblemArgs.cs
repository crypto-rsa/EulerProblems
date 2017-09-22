namespace EulerProblems.Solutions
{
    /// <summary>
    /// Represents problem arguments with a single string input
    /// </summary>
    public class StringInputProblemArgs
    {
        #region Constructors

        /// <summary>
        /// Constructs an instance of the <see cref="StringInputProblemArgs"/> class with the given string
        /// </summary>
        /// <param name="input">The input string</param>
        public StringInputProblemArgs( string input )
        {
            Input = input;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the input string
        /// </summary>
        public string Input { get; private set; }

        #endregion
    }
}
