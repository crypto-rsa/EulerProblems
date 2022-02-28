using System;

namespace EulerProblems.CLI
{
    class Program
    {
        static void Main( string[] args )
        {
            if( args.Length >= 2 && args[0] == "-p" )
            {
                Console.WriteLine($"Running problem {args[1]}");

                TryRunProblem(args[1]);

                return;
            }

            while( true )
            {
                Console.Write("Enter the number of the problem to solve (or 'q' to quit): ");

                var input = Console.ReadLine();
                if (string.Equals(input, "q", StringComparison.OrdinalIgnoreCase))
                    return;

                TryRunProblem(input);

                Console.WriteLine();
            }
        }

        private static void TryRunProblem(string input)
        {
            if (int.TryParse(input, out int number))
            {
                var stopWatch = System.Diagnostics.Stopwatch.StartNew();

                try
                {
                    var result = ProblemRunner.Solve(number);

                    Console.WriteLine($"Solution to problem #{number} is {result} (found in {stopWatch.ElapsedMilliseconds} ms)");
                }
                catch (NotImplementedException)
                {
                    Console.WriteLine($"ERROR: Problem #{number} is not implemented yet!");
                }
                catch (OutOfMemoryException)
                {
                    Console.WriteLine($"ERROR: Problem #{number} is too big for my little mind!");
                }
            }
            else
            {
                Console.WriteLine("Unknown input!");
            }
        }
    }
}
