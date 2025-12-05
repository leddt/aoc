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
            var lines = data.GetLines();
            var ranges = lines.TakeWhile(x => !string.IsNullOrWhiteSpace(x)).Select(Range.Parse).ToList();

            for (var i = 0; i < ranges.Count; i++)
            {
                var a = ranges[i];
                
                var hadMerge = false;
                for (var j = i + 1; j < ranges.Count; j++)
                {
                    var b = ranges[j];
                    if (!a.Overlaps(b)) continue;
                    
                    a.MergeWith(b);
                    ranges.RemoveAt(j--);
                    hadMerge = true;
                }
                
                // If we merged, we need to re-check the range
                if (hadMerge) i--;
            }
            
            return ranges.Sum(x => x.Size);
        }
    }

    private class Range(long min, long max)
    {
        private long _min = min;
        private long _max = max;
        
        public long Size => _max - _min + 1;
        
        public bool Contains(long x) => x >= _min && x <= _max;
        public bool Overlaps(Range other) => _min <= other._max && _max >= other._min;
        
        public void MergeWith(Range other)
        {
            _min = Math.Min(_min, other._min);
            _max = Math.Max(_max, other._max);
        }
        
        public override string ToString() => $"{_min}-{_max} ({Size})";
        
        public static Range Parse(string line)
        {
            var split = line.IndexOf('-');
            return new Range(long.Parse(line[..split]), long.Parse(line[(split + 1)..]));
        }
    }
}