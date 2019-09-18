using System;
using System.Collections.Generic;
using System.Linq;

namespace EulerProblems.Solutions
{
    /// <summary>
    /// Problem #206 (https://projecteuler.net/problem=206)
    /// </summary>
    public class Problem206 : IProblem<Problem206.Arguments>
    {
        #region Nested Types

        public class Arguments
        {
            #region Constructors

            public Arguments( params int[] digits )
            {
                Digits = digits;
            }

            #endregion

            #region Properties

            public int[] Digits { get; }

            #endregion
        }
        #endregion

        #region Methods

        public string Solve(Arguments arguments)
        {
            var powers10 = Enumerable.Range(0, 20).Select(i => (long)Math.Pow(10, i)).ToArray();

            var stack = new Stack<(long, int)>();

            stack.Push((0, 0));

            while (stack.Count > 0)
            {
                var (fixedPart, nextExponent) = stack.Pop();

                for (int d = 0; d < 10; d++)
                {
                    var part = powers10[nextExponent] * d + fixedPart;
                    int matching = Matches(part);

                    if (matching == arguments.Digits.Length)
                        return part.ToString();

                    if (matching <= arguments.Digits.Length && matching >= nextExponent + 1 && nextExponent < powers10.Length - 1)
                    {
                        stack.Push((part, nextExponent + 1));
                    }
                }
            }

            System.Diagnostics.Debug.Fail("No solution found!");

            return 0.ToString();

            int Matches(long part)
            {
                long n = part * part;
                int matching = 0;

                for (; matching < arguments.Digits.Length && (n > 0 || part == 0); n /= 10)
                {
                    if (arguments.Digits[matching] >= 0 && n % 10 != arguments.Digits[matching])
                        break;

                    matching++;
                }

                if (matching == arguments.Digits.Length && n > 0)
                {
                    // longer than the expected number
                    matching++;
                }

                return matching;
            }
        }

        #endregion
    }
}
