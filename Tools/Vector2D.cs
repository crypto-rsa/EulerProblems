using System;

namespace Tools
{
    /// <summary>
    /// Represents a two-dimensional vector
    /// </summary>
    public struct Vector2D
    {
        #region Constructors

        /// <summary>
        /// Constructs an instance of the <see cref="Vector2D"/> struct
        /// </summary>
        /// <param name="x">The X coordinate of the vector</param>
        /// <param name="y">The Y coordinate of the vector</param>
        public Vector2D(double x, double y)
        {
            X = x;
            Y = y;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the X coordinate of the vector
        /// </summary>
        public double X { get; }

        /// <summary>
        /// Gets the Y coordinate of the vector
        /// </summary>
        public double Y { get; }

        /// <summary>
        /// Gets the angle of the vector measured CCW from the positive X axis, in interval [0, 2*pi)
        /// </summary>
        public double Angle
        {
            get
            {
                double angle = Math.Atan2(Y, X);

                if (angle < 0.0)
                {
                    angle += 2 * Math.PI;
                }

                return angle;
            }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Constructs a new vector from the given angle and length
        /// </summary>
        /// <param name="angle">The angle of the vector, measured CCW from the positive X axis (in [rad])</param>
        /// <param name="length">The length of the vector</param>
        /// <returns>The newly created vector</returns>
        public static Vector2D FromAngle(double angle, double length = 1.0)
            => length * new Vector2D(Math.Cos(angle), Math.Sin(angle));

        #endregion

        #region Operators

        public static Vector2D operator *(double factor, Vector2D vector)
            => new Vector2D(factor * vector.X, factor * vector.Y);

        public static Vector2D operator +(Vector2D first, Vector2D second)
            => new Vector2D(first.X + second.X, first.Y + second.Y);

        public static Vector2D operator -(Vector2D vector) => -1.0 * vector;

        #endregion
    }
}
