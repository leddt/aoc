using System.Text.RegularExpressions;

namespace aoc_2025;

public partial class Day02() : Day(2)
{
    private const string Sample = """
                                  11-22,95-115,998-1012,1188511880-1188511890,222220-222224,
                                  1698522-1698528,446443-446449,38593856-38593862,565653-565659,
                                  824824821-824824827,2121212118-2121212124
                                  """;
    
    [Test]
    public void Part1()
    {
        Assert.That(Run(Sample), Is.EqualTo(1227775554));
        Console.WriteLine(Run(Input));
        return;

        long Run(string data)
        {
            return GetAllIds(data).Where(IsInvalid).Sum();
            
            bool IsInvalid(long id)
            {
                var s = id.ToString();
                if (s.Length % 2 != 0) return false;
                return s[..(s.Length / 2)] == s[(s.Length / 2)..];
            }
        }
    }

    [Test]
    public void Part2()
    {
        Assert.That(Run(Sample), Is.EqualTo(4174379265));
        Console.WriteLine(Run(Input));
        return;

        long Run(string data)
        {
            return GetAllIds(data).Where(IsInvalid).Sum();
            
            bool IsInvalid(long id)
            {
                var s = id.ToString();
                
                for (var len = s.Length / 2; len >= 1; len--)
                {
                    if (s.Length % len != 0) continue;
                    
                    var part = s[..len];
                    var valid = false;
                    for (var i = len; i < s.Length; i += len)
                    {
                        if (s[i..(i + len)] == part) continue;
                            
                        valid = true; 
                        break;
                    }

                    if (!valid) return true;
                }
                
                return false;
            }
        }
    }

    [GeneratedRegex(@"^(.*)\1+$")]
    private partial Regex Part2Regex();

    [Test]
    [Ignore("It works fine, but is 4-5x slower")]
    public void Part2WithRegex()
    {
        Assert.That(Run(Sample), Is.EqualTo(4174379265));
        Console.WriteLine(Run(Input));
        return;

        long Run(string data)
        {
            return GetAllIds(data)
                .Where(IsInvalid)
                .Sum();

            bool IsInvalid(long id) => Part2Regex().IsMatch(id.ToString());
        }
    }

    private IEnumerable<long> GetAllIds(string data) => data
        .Split(',', StringSplitOptions.TrimEntries)
        .Select(x => x.Split('-'))
        .SelectMany(x =>
        {
            var start = long.Parse(x[0]);
            var end = long.Parse(x[1]);
            return Range(start, end);
        });

    private IEnumerable<long> Range(long start, long end)
    {
        while (start <= end) 
            yield return start++;
    }
}