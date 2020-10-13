using System;
using System.Linq;
using Tools;

namespace EulerProblems.Solutions
{
    /// <summary>
    /// Problem #144 (https://projecteuler.net/problem=144)
    /// </summary>
    public class Problem144 : IProblem<EmptyProblemArgs>
    {
        #region Methods

        public string Solve(EmptyProblemArgs arguments)
        {
            // Simply reflect the incoming ray about the normal and calculate the next reflection point.
            // If (p_x, p_y) is the current reflection point and (v_x, v_y) is the reflected ray, then
            // the intersections of this ray with the ellipse are given by
            // 4 * (p_x + k*v_x)^2 + (p_y + k*v_y)^2 == 100
            // One of the roots is k == 0 (represents the current reflection point), the other (nonzero)
            // represents the next reflection point.

            int reflections = 0;

            var prevPoint = new Point2D(0.0, 10.1);
            var nextPoint = new Point2D(1.4, -9.6);

            while(nextPoint.Y < 0.0 || nextPoint.X < -0.01 || nextPoint.X > +0.01)
            {
                reflections++;

                var reverseIncoming = prevPoint - nextPoint;
                var normal = -Vector2D.FromAngle(Math.Atan2(nextPoint.Y, 4 * nextPoint.X));

                double reflectedAngle = reverseIncoming.Angle + 2 * (normal.Angle - reverseIncoming.Angle);
                var reflected = Vector2D.FromAngle(reflectedAngle);

                double a = 4 * Math.Pow(reflected.X, 2) + Math.Pow(reflected.Y, 2);
                double b = 8 * nextPoint.X * reflected.X + 2 * nextPoint.Y * reflected.Y;
                double c = 4 * Math.Pow(nextPoint.X, 2) + Math.Pow(nextPoint.Y, 2) - 100.0;

                double k = Numbers.SolveQuadratic(a, b, c).First( d => d != 0.0 );

                prevPoint = nextPoint;
                nextPoint += k * reflected;
            }

            return reflections.ToString();
        }

        #endregion
    }
}
