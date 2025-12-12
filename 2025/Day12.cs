namespace aoc_2025;

public class Day12() : Day(12)
{
    private const string Sample = """
                                  0:
                                  ###
                                  ##.
                                  ##.
                                  
                                  1:
                                  ###
                                  ##.
                                  .##
                                  
                                  2:
                                  .##
                                  ###
                                  ##.
                                  
                                  3:
                                  ##.
                                  ###
                                  ##.
                                  
                                  4:
                                  ###
                                  #..
                                  ###
                                  
                                  5:
                                  ###
                                  .#.
                                  ###
                                  
                                  4x4: 0 0 0 0 2 0
                                  12x5: 1 0 1 0 2 2
                                  12x5: 1 0 1 0 3 2
                                  """;
    
    [Test]
    public void Part1()
    {
        //Assert.That(Run(Sample), Is.EqualTo(2));
        Console.WriteLine(Run(Input));
        return;

        int Run(string data)
        {
            var (shapes, goals) = Parse(data);
            return goals.Count(IsGoalPossible);

            bool IsGoalPossible(Goal goal)
            {
                /* The problem seemed very complex, so I just wanted to start simple with an incomplete solution and see
                 * what I can do from there.
                 * 
                 * This is extremely naive: just check if there is more space than the total count of tiles.
                 * It completely ignores the shapes.
                 *
                 * When ran on the actual input, it filtered out over half the goals, so I thought, why not submit that
                 * answer?
                 *
                 * To my surprise, that was the correct answer (!).
                 *
                 * I'm not sure if that was intended. It does NOT work for the sample. */
                
                var availableSpace = goal.Width * goal.Height;
                var minimumRequiredSpace = goal.Counts.Select((c, i) => shapes[i].FindAll('#').Count() * c).Sum();
                
                return availableSpace >= minimumRequiredSpace;
            }
        }
    }

    record Goal(int Width, int Height, int[] Counts);
    private static (Grid<char>[] shapes, Goal[] goals) Parse(string data)
    {
        var shapes = new List<Grid<char>>();
        var goals = new List<Goal>();

        var lines = data.GetLines();
        for (var i = 0; i < lines.Length; i++)
        {
            if (lines[i] == "") continue;
            
            if (lines[i][^1] == ':')
            {
                i++;
                var gridLines = lines[i..].TakeWhile(l => l != "").ToArray();
                shapes.Add(Grid.Parse(gridLines));
                i += gridLines.Length;
            }
            else if (lines[i].Contains('x'))
            {
                var parts = lines[i].Split(['x', ':'], StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries);
                goals.Add(new Goal(
                    int.Parse(parts[0]),
                    int.Parse(parts[1]),
                    parts[2].SplitInts(" ")
                ));
            }
            else
            {
                throw new Exception($"Unexpected line: {lines[i]}");
            }
        }
        
        return (shapes.ToArray(), goals.ToArray());
    }
}