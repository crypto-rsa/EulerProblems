using System;
using System.Collections.Generic;
using System.Linq;
using Tools;

namespace EulerProblems.Solutions
{
    /// <summary>
    /// Problem #209 (https://projecteuler.net/problem=209)
    /// </summary>
    public class Problem209 : IProblem<EmptyProblemArgs>
    {
        #region Methods

        public string Solve(EmptyProblemArgs arguments)
        {
            // We construct a graph G with nodes represented by tuples (a, b, c, d, e, f) and edges between node 1 and node 2
            // iff node 1 represents T(a, b, c, d, e, f) and node 2 represents T(b, c, d, e, f, a XOR (b AND c)).
            // When T(a, b, c, d, e, f) AND T(b, c, d, e, f, a XOR (b AND c)) = 0, then either of the terms is zero.
            // So a valid assignment of T corresponds to a vertex cover in G (we'll just assign 0 as the value of T(n) for each node n
            // included in the cover.
            // Since there are no two distinct nodes which are connected to the same third node (from the properties of the terms),
            // the entire graph must consist of distinct cycles. The number of vertex cover sets of a cycle of length N is
            // F(N - 1) + F(N + 1), where F(i) is the i-th Fibonacci number.
            const int length = 6;
            var nodes = Enumerable.Range(0, (int)Numbers.Pow(2, length)).Select(i => Convert.ToString(i, 2).PadLeft(length, '0')).ToList();
            var transitions = nodes.ToDictionary(s => s, GetTransition);

            long total = 1;
            var visited = new HashSet<string>();

            foreach (var node in nodes.Where(s => !visited.Contains(s)))
            {
                int cycleLength = 0;

                string nextNode = node;
                do
                {
                    cycleLength++;
                    visited.Add(nextNode);
                    nextNode = transitions[nextNode];
                } while (!visited.Contains( nextNode ));

                if (cycleLength > 1)
                {
                    total *= Numbers.Fib(cycleLength - 1) + Numbers.Fib(cycleLength + 1);
                }
            }

            return total.ToString();

            string GetTransition(string s)
            {
                var a = s.Take(3).Select(c => int.Parse(c.ToString())).ToArray();
                var f = a[0] ^ (a[1] & a[2]);

                return s.Substring(1) + f.ToString();
            }
        }

        #endregion
    }
}
