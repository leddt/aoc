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

                total += op switch
                {
                    "+" => values.Sum(),
                    "*" => values.Aggregate(1L, (x, y) => x * y),
                    _ => 0
                };
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
            var start = 0;
            
            while (start < lastLine.Length)
            {
                var end = lastLine.IndexOfAny(['+', '*'], start + 1) - 1;
                if (end < 0) end = lastLine.Length;
                
                var op = lastLine[start];
                var subTotal = op == '*' ? 1 : 0L;
                
                for (var c = start; c < end; c++)
                {
                    var number = "";
                    
                    foreach (var line in otherLines)
                    {
                        if (line[c] != ' ')
                            number += line[c];
                    }

                    var value = long.Parse(number);
                    if (op == '+') subTotal += value;
                    else subTotal *= value;
                }
                
                total += subTotal;
                start = end + 1;
            }
            
            return total;
        }
    }
}