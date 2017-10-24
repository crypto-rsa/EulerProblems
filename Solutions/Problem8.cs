namespace EulerProblems.Solutions
{
    /// <summary>
    /// Problem #8 (https://projecteuler.net/problem=8)
    /// </summary>
    public class Problem8 : IProblem<Problem8.Arguments>
    {
        #region Nested Types

        /// <summary>
        /// Represents the arguments for <see cref="Problem8"/>
        /// </summary>
        public class Arguments
        {
            #region Constructors

            /// <summary>
            /// Constructs an instance of the <see cref="Arguments"/> class
            /// </summary>
            /// <param name="input">The input string</param>
            /// <param name="sequenceLength">The length of the sequence to find</param>
            public Arguments( string input, int sequenceLength )
            {
                Input = input;
                SequenceLength = sequenceLength;
            }

            #endregion

            #region Properties

            /// <summary>
            /// Gets or sets the input string
            /// </summary>
            public string Input { get; private set; }

            /// <summary>
            /// Gets or sets the length of the sequence to find
            /// </summary>
            public int SequenceLength { get; private set; }

            #endregion

            #region Methods

            /// <summary>
            /// Creates an instance of the <see cref="Arguments"/> class using an input file
            /// </summary>
            /// <param name="path">The path with the file which contains the input string</param>
            /// <param name="sequenceLength">The length of the sequence to find</param>
            /// <returns></returns>
            public static Arguments FromFile( string path, int sequenceLength )
            {
                return new Arguments( new InputFileProblemArgs( path ).Input, sequenceLength );
            }

            #endregion
        }

        #endregion

        #region Methods

        public string Solve( Arguments arguments )
        {
            var input = arguments.Input;
            long result = 0;

            long tempResult = 0;
            int position = 0;
            int length = 0;

            while( position < input.Length )
            {
                if( !int.TryParse( input[position].ToString(), out int digit ) )
                    return $"Error in input string at position {position}: {input[position]} is not a digit!";

                if( digit == 0 )
                {
                    length = 0;
                    tempResult = 0;
                }
                else if( tempResult == 0 )
                {
                    // first nonzero digit
                    length = 1;
                    tempResult = digit;
                }
                else if( length < arguments.SequenceLength )
                {
                    tempResult *= digit;
                    length++;
                }
                else
                {
                    tempResult /= int.Parse( input[position - arguments.SequenceLength].ToString() );
                    tempResult *= digit;
                }

                if( length == arguments.SequenceLength && tempResult > result )
                {
                    result = tempResult;
                }

                position++;
            }

            return result.ToString();
        }

        #endregion
    }
}
