namespace aoc_2025;

public class Day04() : Day(4)
{
    private const string Sample = """
                                  ..@@.@@@@.
                                  @@@.@.@.@@
                                  @@@@@.@.@@
                                  @.@@@@..@.
                                  @@.@@@@.@@
                                  .@@@@@@@.@
                                  .@.@.@.@@@
                                  @.@@@.@@@@
                                  .@@@@@@@@.
                                  @.@.@@@.@.
                                  """;
    
    [Test]
    public void Part1()
    {
        Assert.That(Run(Sample), Is.EqualTo(13));
        Console.WriteLine(Run(Input));
        return;

        int Run(string data)
        {
            var grid = Grid.Parse(data);
            return grid.FindAll('@').Count(x => IsRemovable(grid, x));
        }
    }

    [Test]
    public void Part2()
    {
        Assert.That(Run(Sample), Is.EqualTo(43));
        Console.WriteLine(Run(Input));
        return;

        int Run(string data)
        {
            var grid = Grid.Parse(data);

            var removed = 0;

            while (true)
            {
                var didRemove = false;
                
                foreach (var roll in grid.FindAll('@'))
                {
                    if (!IsRemovable(grid, roll)) continue;
                    
                    grid[roll] = '.';
                    removed++;
                    didRemove = true;
                }
                
                if (!didRemove) break;
            }

            return removed;
        }
    }

    private static bool IsRemovable(Grid grid, V2 roll) => grid
        .Neighbors(roll, diag: true, n => n.val == '@')
        .Count() < 4;
}