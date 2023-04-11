using System.Linq;

namespace EulerProblems.Solutions;

public class Problem836 : IProblem<EmptyProblemArgs>
{
    public string Solve(EmptyProblemArgs arguments)
    {
        var source = new System.Net.WebClient().DownloadString("https://projecteuler.net/problem=836");

        var matches = System.Text.RegularExpressions.Regex.Matches(source, "<b>(.*?)</b>");

        return string.Join(string.Empty, matches.SelectMany(m => m.Groups[1].Value.Split(' ').Select(s => s[0])));
    }
}
