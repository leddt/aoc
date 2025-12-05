namespace aoc_2025;

public class Day05() : Day(5)
{
    private const string Sample = """
                                  3-5
                                  10-14
                                  16-20
                                  12-18
                                  
                                  1
                                  5
                                  8
                                  11
                                  17
                                  32
                                  """;
    
    [Test]
    public void Part1()
    {
        Assert.That(Run(Sample), Is.EqualTo(3));
        Console.WriteLine(Run(Input));
        return;

        int Run(string data)
        {
            var lines = data.GetLines();
            var ranges = lines.TakeWhile(x => !string.IsNullOrWhiteSpace(x)).Select(Range.Parse).ToArray();
            var ingredients = lines.Skip(ranges.Length + 1).Select(long.Parse);

            return ingredients.Count(x => ranges.Any(r => r.Contains(x)));
        }
    }

    [Test]
    public void Part2()
    {
        Assert.That(Run(Sample), Is.EqualTo(14));
        Console.WriteLine(Run(Input));
        return;

        long Run(string data)
        {
            var ranges = data.GetLines()
                .TakeWhile(x => !string.IsNullOrWhiteSpace(x))
                .Select(Range.Parse)
                .OrderBy(x => x.Min)
                .ToList();

            for (var i = 0; i < ranges.Count; i++)
            {
                var a = ranges[i];
                
                for (var j = i + 1; j < ranges.Count; j++)
                {
                    var b = ranges[j];
                    if (!a.Overlaps(b)) continue;
                    
                    a.MergeWith(b);
                    ranges.RemoveAt(j--);
                }
            }
            
            return ranges.Sum(x => x.Size);
        }
    }

    [Test]
    public void Part2SinglePass()
    {
        Assert.That(Run(Sample), Is.EqualTo(14));
        Console.WriteLine(Run(Input));
        return;

        long Run(string data)
        {
            var ranges = data.GetLines().TakeWhile(x => !string.IsNullOrWhiteSpace(x)).Select(Range.Parse);
            var merged = new List<Range>();

            foreach (var range in ranges.OrderBy(x => x.Min))
            {
                var last = merged.LastOrDefault();
                if (last?.Overlaps(range) == true)
                    last.MergeWith(range);
                else
                    merged.Add(range);
            }
            
            return merged.Sum(x => x.Size);
        }
    }

    private class Range(long min, long max)
    {
        public long Min = min;
        public long Max = max;
        
        public long Size => Max - Min + 1;
        
        public bool Contains(long x) => x >= Min && x <= Max;
        public bool Overlaps(Range other) => Min <= other.Max && Max >= other.Min;
        
        public void MergeWith(Range other)
        {
            Min = Math.Min(Min, other.Min);
            Max = Math.Max(Max, other.Max);
        }
        
        public override string ToString() => $"{Min}-{Max} ({Size})";
        
        public static Range Parse(string line)
        {
            var split = line.IndexOf('-');
            return new Range(long.Parse(line[..split]), long.Parse(line[(split + 1)..]));
        }
    }
}