namespace Tools
{
    /// <summary>
    /// Represents a point in two-dimensional plane
    /// </summary>
    public struct Point2D
    {
        #region Constructors

        /// <summary>
        /// Constructs an instance of the <see cref="Point2D"/> struct
        /// </summary>
        /// <param name="x">The X coordinate of the point</param>
        /// <param name="y">The Y coordinate of the point</param>
        public Point2D(double x, double y)
        {
            X = x;
            Y = y;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the X coordinate of the point
        /// </summary>
        public double X { get; }

        /// <summary>
        /// Gets the Y coordinate of the point
        /// </summary>
        public double Y { get; }

        #endregion

        #region Operators

        public static Vector2D operator -(Point2D end, Point2D start)
            => new Vector2D(end.X - start.X, end.Y - start.Y);

        public static Point2D operator +(Point2D start, Vector2D vector)
            => new Point2D(start.X + vector.X, start.Y + vector.Y);

        #endregion
    }
}
