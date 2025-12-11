namespace aoc_2025;

public class Day11() : Day(11)
{
    private const string Sample = """
                                  aaa: you hhh
                                  you: bbb ccc
                                  bbb: ddd eee
                                  ccc: ddd eee fff
                                  ddd: ggg
                                  eee: out
                                  fff: out
                                  ggg: out
                                  hhh: ccc fff iii
                                  iii: out
                                  """;
    
    [Test]
    public void Part1()
    {
        Assert.That(Run(Sample), Is.EqualTo(5));
        Console.WriteLine(Run(Input));
        return;

        long Run(string data)
        {
            var devices = Parse(data);
            Dictionary<string, long> cache = [];

            return CountPaths("you");

            long CountPaths(string from)
            {
                if (cache.TryGetValue(from, out var count)) return count;
                
                if (from == "out") return 1;
                return cache[from] = devices[from].Sum(CountPaths);
            }
        }
    }

    [Test]
    public void Part2()
    {
        Assert.That(Run(Sample), Is.EqualTo(0));
        Console.WriteLine(Run(Input));
        return;

        int Run(string data)
        {
            return 0;
        }
    }

    private static Dictionary<string, string[]> Parse(string data)
    {
        Dictionary<string, string[]> devices = [];
        foreach (var line in data.GetLines())
        {
            var parts = line.Split([':', ' '], StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries);
            devices[parts[0]] = parts[1..];
        }

        return devices;
    }
}