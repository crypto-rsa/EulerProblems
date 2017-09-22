using System.IO;

namespace EulerProblems.Solutions
{
    /// <summary>
    /// Represents problem arguments with an input file
    /// </summary>
    public class InputFileProblemArgs : StringInputProblemArgs
    {
        #region Fields

        /// <summary>
        /// An array of lines read from the file
        /// </summary>
        private readonly string[] _lines;

        #endregion

        #region Constructors

        /// <summary>
        /// Constructs an instance of the <see cref="InputFileProblemArgs"/> class with the given file
        /// </summary>
        /// <param name="path">The path to the file to read</param>
        public InputFileProblemArgs( string path )
            : base( File.ReadAllText( path ) )
        {
            _lines = File.ReadAllLines( path );
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the lines read from the file
        /// </summary>
        public string[] Lines => _lines;

        #endregion
    }
}
