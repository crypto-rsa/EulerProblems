namespace Tools
{
    /// <summary>
    /// Contains methods for number manipulation
    /// </summary>
    public static class Numbers
    {
        #region Methods

        /// <summary>
        /// Performs fast exponentiation
        /// </summary>
        /// <param name="base">The base</param>
        /// <param name="exponent">The exponent</param>
        /// <returns><paramref name="base"/>^<paramref name="exponent"/></returns>
        public static long Pow( long @base, long exponent )
        {
            long result = 1;
            long temp = @base;
            long tempExponent = exponent;

            while( tempExponent > 0 )
            {
                if( (tempExponent & 1) == 1 )
                {
                    result *= temp;
                }

                temp *= temp;
                tempExponent >>= 1;
            }

            return result;
        }

        #endregion
    }
}
