namespace aoc_2025;

public class Day07() : Day(7)
{
    private const string Sample = """
                                  .......S.......
                                  ...............
                                  .......^.......
                                  ...............
                                  ......^.^......
                                  ...............
                                  .....^.^.^.....
                                  ...............
                                  ....^.^...^....
                                  ...............
                                  ...^.^...^.^...
                                  ...............
                                  ..^...^.....^..
                                  ...............
                                  .^.^.^.^.^...^.
                                  ...............
                                  """;
    
    [Test]
    public void Part1()
    {
        Assert.That(Run(Sample), Is.EqualTo(21));
        Console.WriteLine(Run(Input));
        return;

        int Run(string data)
        {
            var grid = Grid.Parse(data);
            
            var splitters = grid.FindAll('^').ToHashSet();
            var hit = new HashSet<V2>();

            SimulateBeam(grid.FindFirst('S'));
            
            return hit.Count;

            void SimulateBeam(V2 beam)
            {
                while (true)
                {
                    beam += V2.Down;

                    if (splitters.Contains(beam))
                    {
                        if (!hit.Add(beam)) return;
                        
                        var left = beam + V2.Left;
                        var right = beam + V2.Right;
                        
                        if (grid.Contains(left)) SimulateBeam(left);
                        if (grid.Contains(right)) SimulateBeam(right);
                        return;
                    }
                    
                    if (!grid.Contains(beam)) return;
                }
            }
        }
    }

    [Test]
    public void Part2()
    {
        Assert.That(Run(Sample), Is.EqualTo(40));
        Console.WriteLine(Run(Input));
        return;

        long Run(string data)
        {
            var grid = Grid.Parse(data);
            var splitters = grid.FindAll('^').ToDictionary(x => x, _ => 0L);
            
            return GetTimelineCount(grid.FindFirst('S'));

            long GetTimelineCount(V2 pos)
            {
                while (true)
                {
                    pos += V2.Down;

                    if (splitters.TryGetValue(pos, out var cachedValue))
                    {
                        if (cachedValue > 0) return cachedValue;
                        
                        return splitters[pos] = GetTimelineCount(pos + V2.Left) + 
                                                GetTimelineCount(pos + V2.Right);
                    }
                    
                    if (!grid.Contains(pos)) return 1;
                }
            }
        }
    }
}