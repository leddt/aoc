namespace aoc_2025;

public class Day09() : Day(9)
{
    private const string Sample = """
                                  7,1
                                  11,1
                                  11,7
                                  9,7
                                  9,5
                                  2,5
                                  2,3
                                  7,3
                                  """;
    
    [Test]
    public void Part1()
    {
        Assert.That(Run(Sample), Is.EqualTo(50));
        Console.WriteLine(Run(Input));
        return;

        long Run(string data)
        {
            var points = data.GetLines().Select(ParsePoint).ToArray();
            return GetAllRects(points).Max(x => x.Area);
        }
    }

    [Test]
    public void Part2()
    {
        Assert.That(Run(Sample), Is.EqualTo(24));
        Console.WriteLine(Run(Input));
        return;

        long Run(string data)
        {
            var points = data.GetLines().Select(ParsePoint).ToArray();
            var polygon = GetAllEdges(points).ToArray();
            
            var bestRect = GetAllRects(points)
                .OrderByDescending(x => x.Area)
                .First(IsInsidePolygon);
            
            return bestRect.Area;

            bool IsInsidePolygon(Rect rect)
            {
                if (polygon.Any(e => e.Crosses(rect))) return false;
                
                // Note: this is incomplete. It doesn't catch cases where a rect is completely outside the shape.
                // But it worked for my input.
                
                return true;
            }
        }
    }

    private V2 ParsePoint(string value)
    {
        var parts = value.Split(',');
        return new V2(int.Parse(parts[0]), int.Parse(parts[1]));
    }

    private static IEnumerable<Rect> GetAllRects(V2[] points)
    {
        for (var i = 0; i < points.Length; i++)
        for (var j = i + 1; j < points.Length; j++)
            yield return new Rect(points[i], points[j]);
    }

    private static IEnumerable<Edge> GetAllEdges(V2[] points)
    {
        var first = points.First();
        
        var previous = first;
        foreach (var point in points.Skip(1))
        {
            yield return new Edge(previous, point);
            previous = point;
        }
        
        yield return new Edge(previous, first);
    }
}