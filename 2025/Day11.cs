namespace aoc_2025;

public class Day11() : Day(11)
{
    private const string Sample1 = """
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

    private const string Sample2 = """
                                   svr: aaa bbb
                                   aaa: fft
                                   fft: ccc
                                   bbb: tty
                                   tty: ccc
                                   ccc: ddd eee
                                   ddd: hub
                                   hub: fff
                                   eee: dac
                                   dac: fff
                                   fff: ggg hhh
                                   ggg: out
                                   hhh: out
                                   """;
    
    [Test]
    public void Part1()
    {
        Assert.That(Run(Sample1), Is.EqualTo(5));
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
        Assert.That(Run(Sample2), Is.EqualTo(2));
        Console.WriteLine(Run(Input));
        return;

        long Run(string data)
        {
            var devices = Parse(data);
            Dictionary<string, long> cache = [];

            return CountPaths("svr");

            long CountPaths(string from, bool dac = false, bool fft = false)
            {
                var key = $"{from}:{dac}:{fft}";
                if (cache.TryGetValue(key, out var count)) return count;

                dac |= from == "dac";
                fft |= from == "fft";
                
                if (from == "out" && dac && fft) return 1;
                if (!devices.ContainsKey(from)) return 0;
                
                return cache[key] = devices[from].Sum(x => CountPaths(x, dac, fft));
            }
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