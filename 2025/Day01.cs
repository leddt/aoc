namespace aoc_2025;

public class Day01() : Day(1)
{
    private const string Sample = """
                                  L68
                                  L30
                                  R48
                                  L5
                                  R60
                                  L55
                                  L1
                                  L99
                                  R14
                                  L82
                                  """;
    
    private const int Initial = 50;
    private const int Size = 100;
    
    [Test]
    public void Part1()
    {
        Assert.That(Run(Sample), Is.EqualTo(3));
        Console.WriteLine(Run(Input));
        return;

        int Run(string data)
        {
            var moves = data.GetLines().Select(CreateMove);

            var dial = Initial;
            var count = 0;
            foreach (var move in moves)
            {
                dial += move.Value;
                dial %= Size;
                if (dial < 0) dial += Size;
                if (dial == 0) count++;
            }

            return count;
        }
    }

    [Test]
    public void Part2()
    {
        Assert.That(Run(Sample), Is.EqualTo(6));
        Console.WriteLine(Run(Input));
        return;

        int Run(string data)
        {
            var moves = data.GetLines().Select(CreateMove);
        
            var dial = Initial;
            var count = 0;
        
            foreach (var move in moves)
            {
                var startedAtZero = dial == 0;
            
                dial += move.Value;

                if (dial is <= 0 or >= Size) count += Math.Abs(dial) / Size;
                if (dial <= 0 && !startedAtZero) count++;

                dial %= Size;
                if (dial < 0) dial += Size;
            }

            return count;
        }
    }

    private static Move CreateMove(string line) => new(line[0] == 'L', int.Parse(line[1..]));

    private record Move(bool TurnLeft, int Distance)
    {
        public override string ToString() => $"{(TurnLeft ? "L" : "R")}{Distance}";
        public int Value => Distance * (TurnLeft ? -1 : 1);
    }
}