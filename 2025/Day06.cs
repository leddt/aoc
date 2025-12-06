namespace aoc_2025;

public class Day06() : Day(6)
{
    private const string Sample = """
                                  123 328  51 64 
                                   45 64  387 23 
                                    6 98  215 314
                                  *   +   *   +  
                                  """;
    
    [Test]
    public void Part1()
    {
        Assert.That(Run(Sample), Is.EqualTo(4277556));
        Console.WriteLine(Run(Input));
        return;

        long Run(string data)
        {
            var lines = data.GetLines()
                .Select(x => x.Split(' ', StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries))
                .ToArray();
            
            var lastLine = lines[^1];
            var otherLines = lines[..^1].Select(l => l.Select(long.Parse).ToArray()).ToArray();
            var total = 0L;

            for (var i = 0; i < lastLine.Length; i++)
            {
                var op = lastLine[i];
                var values = otherLines.Select(x => x[i]).ToArray();
                
                total += Calculate(op[0], values);
            }
            
            return total;
        }
    }

    [Test]
    public void Part2()
    {
        Assert.That(Run(Sample), Is.EqualTo(3263827));
        Console.WriteLine(Run(Input));
        return;

        long Run(string data)
        {
            var lines = data.GetLines();
            var lastLine = lines[^1];
            var otherLines = lines[..^1];

            var total = 0L;
            var i = 0;
            
            while (true)
            {
                var start = i;
                var end = i + 1;
                
                while (end < lastLine.Length && lastLine[end] == ' ') end++;
                if (lastLine.Length > end) end--;
                
                var op = lastLine[start];
                var values = new List<long>();
                
                for (var c = start; c < end; c++)
                {
                    var value = "";
                    
                    foreach (var line in otherLines)
                    {
                        if (line[c] != ' ')
                            value += line[c];
                    }
                    
                    values.Add(long.Parse(value));
                }
                
                total += Calculate(op, values);

                if (end >= lastLine.Length) break;
                i = end + 1;
            }
            
            return total;
        }
    }

    private static long Calculate(char op, IEnumerable<long> values) => op switch
    {
        '+' => values.Sum(),
        '*' => values.Aggregate(1L, (x, y) => x * y),
        _ => 0
    };
}