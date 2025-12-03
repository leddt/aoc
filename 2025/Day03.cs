namespace aoc_2025;

public class Day03() : Day(3)
{
    private const string Sample = """
                                  987654321111111
                                  811111111111119
                                  234234234234278
                                  818181911112111
                                  """;
    
    [Test]
    public void Part1()
    {
        Assert.That(Run(Sample), Is.EqualTo(357));
        Console.WriteLine(Run(Input));
        return;

        long Run(string data) => data.GetLines().Sum(bank => GetBestValue(bank, 2));
    }

    [Test]
    public void Part2()
    {
        Assert.That(Run(Sample), Is.EqualTo(3121910778619));
        Console.WriteLine(Run(Input));
        return;

        long Run(string data) => data.GetLines().Sum(bank => GetBestValue(bank, 12));
    }

    private static long GetBestValue(string bank, int batteryCount)
    {
        var batteries = bank.Select(c => c - '0').ToArray().AsSpan();
        var result = 0L;

        for (var batteryIndex = batteryCount; batteryIndex > 0; batteryIndex--)
        {
            var bestIndex = 0;
            var bestValue = 0;

            for (var i = 0; i < batteries.Length - (batteryIndex - 1); i++)
            {
                if (batteries[i] <= bestValue) continue;
                bestValue = batteries[i];
                bestIndex = i;
            }
                    
            result += bestValue * (long)Math.Pow(10, batteryIndex - 1);
            batteries = batteries[(bestIndex + 1)..];
        }

        return result;
    }
}