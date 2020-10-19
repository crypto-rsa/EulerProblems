using System;

namespace EulerProblems.Solutions
{
    /// <summary>
    /// Problem #147 (https://projecteuler.net/problem=147)
    /// </summary>
    public class Problem147 : IProblem<Problem147.Arguments>
    {
        #region Nested Types

        public class Arguments
        {
            #region Constructors

            public Arguments(int rows, int columns)
            {
                Rows = rows;
                Columns = columns;
            }

            #endregion

            #region Properties

            public int Rows { get; }

            public int Columns { get; }

            #endregion
        }

        #endregion

        #region Methods

        public string Solve(Arguments arguments)
        {
            // The total number of rectangles in a grid of 'r' rows by 'c' columns is P(r, c) + Q(r, c)
            // where P is the number of axis-aligned rectangles and Q the number of diagonal ones.
            // For any r, c: P(r, c) = P(c, r) and Q(r, c) = Q(c, r)
            // For both types, we will calculate the number recursively by adding a new column to the existing grid.
            // The total number of rectangles in the new grid is
            //  the number of rectangles not using the first column (already calculated) +
            //  the number of rectangles not using the last column (already calculated) -
            //  the number of rectangles using neither the first nor the last column (already calculated, accounted for twice) +
            //  the number of rectangles using both the first and the last column (need to calculate this) = Z
            // 
            // For axis-aligned, Z is just the sum of 1, 2, ... 'r' (i.e. the number of positions of rectangles of height n, n - 1, ..., 1)
            // For diagonal ones, we will just calculate the compatible rectangles using their parameters

            int max = Math.Max(arguments.Rows, arguments.Columns);
            long[,] axisAligned = new long[max + 1, max + 1];

            for (int r = 1; r <= max; r++)
            {
                for (int c = r; c <= max; c++)
                {
                    if (r == 1 && c == 1)
                    {
                        axisAligned[r, c] = 1;
                    }
                    else
                    {
                        axisAligned[r, c] = 2 * axisAligned[r, c - 1] - axisAligned[r, c - 2] + r * (r + 1) / 2;
                    }

                    axisAligned[c, r] = axisAligned[r, c];
                }
            }

            long[,] diagonal = new long[max + 1, max + 1];

            for (int r = 1; r <= max; r++)
            {
                for (int c = r; c <= max; c++)
                {
                    if (r == 1 && c == 1)
                    {
                        diagonal[r, c] = 0;
                    }
                    else
                    {
                        diagonal[r, c] = 2 * diagonal[r, c - 1] - diagonal[r, c - 2] + GetDiagonalRectanglesSpanningFullWidth(r, c);
                    }

                    diagonal[c, r] = diagonal[r, c];
                }
            }

            long total = 0;

            for (int r = 1; r <= arguments.Rows; r++)
            {
                for (int c = 1; c <= arguments.Columns; c++)
                {
                    total += axisAligned[r, c] + diagonal[r, c];
                }
            }

            return total.ToString();

            static long GetDiagonalRectanglesSpanningFullWidth(int rows, int columns)
            {
                int rectangles = 0;

                // s = distance (in half-columns) of the leftmost point of the rectangle from the left side of the grid
                // t = distance (in half-rows) of the leftmost point of the rectangle from the top of the grid
                // a = length of the rectangle in the / direction
                // b = length of the rectangle in the \ direction
                for (int s = 0; s <= 1; s++)
                {
                    for (int t = s % 2 == 0 ? 2 : 1; t <= 2 * rows - 1; t += 2)
                    {
                        for (int a = 1; a <= t; a++)
                        {
                            for (int b = 1; b <= a; b++)
                            {
                                if (s + a + b < 2 * columns - 1 || s + a + b > 2 * columns || t + b > 2 * rows)
                                    continue;

                                bool isSymmetricalAroundVerticalAxis = a == b && s + a == columns;

                                rectangles += isSymmetricalAroundVerticalAxis ? 1 : 2;
                            }
                        }
                    }
                }

                return rectangles;
            }
        }

        #endregion
    }
}
