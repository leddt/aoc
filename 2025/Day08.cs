namespace aoc_2025;

public class Day08() : Day(8)
{
    private const string Sample = """
                                  162,817,812
                                  57,618,57
                                  906,360,560
                                  592,479,940
                                  352,342,300
                                  466,668,158
                                  542,29,236
                                  431,825,988
                                  739,650,466
                                  52,470,668
                                  216,146,977
                                  819,987,18
                                  117,168,530
                                  805,96,715
                                  346,949,466
                                  970,615,88
                                  941,993,340
                                  862,61,35
                                  984,92,344
                                  425,690,689
                                  """;
    
    [Test]
    public void Part1()
    {
        Assert.That(Run(Sample, 10), Is.EqualTo(40));
        Console.WriteLine(Run(Input, 1000));
        return;

        int Run(string data, int count)
        {
            var boxes = data.GetLines()
                .Select(x => x.Split(',').Select(int.Parse).ToArray())
                .Select(x => new V3(x[0], x[1], x[2]))
                .ToArray();

            var distances = GetDistances(boxes).OrderBy(x => x.d);

            var network = new Network();
            foreach (var (a, b, _) in distances.Take(count))
            {
                network.Connect(a, b);
            }

            return network.Circuits
                .OrderByDescending(x => x.Count)
                .Take(3)
                .Aggregate(1, (x, y) => x * y.Count);
        }
    }

    [Test]
    public void Part2()
    {
        Assert.That(Run(Sample), Is.EqualTo(25272));
        Console.WriteLine(Run(Input));
        return;

        long Run(string data)
        {
            var boxes = data.GetLines()
                .Select(x => x.Split(',').Select(int.Parse).ToArray())
                .Select(x => new V3(x[0], x[1], x[2]))
                .ToArray();

            var distances = GetDistances(boxes).OrderBy(x => x.d);

            var network = new Network();
            foreach (var (a, b, _) in distances)
            {
                network.Connect(a, b);

                if (network.Circuits.Count == 1 && network.Circuits[0].Count == boxes.Length)
                    return (long)a.X * b.X;
            }
            
            throw new Exception("No solution found");
        }
    }

    private static IEnumerable<(V3 a, V3 b, double d)> GetDistances(V3[] boxes)
    {
        for (var i = 0; i < boxes.Length; i++)
        for (var j = i + 1; j < boxes.Length; j++)
            yield return (boxes[i], boxes[j], (boxes[i] - boxes[j]).Length);
    }

    private class Network
    {
        public readonly List<HashSet<V3>> Circuits = [];
        
        public void Connect(V3 a, V3 b)
        {
            var cA = Circuits.FirstOrDefault(c => c.Contains(a));
            var cB = Circuits.FirstOrDefault(c => c.Contains(b));

            // Already connected
            if (cA?.Contains(b) == true) return;

            // Merge the two circuits
            if (cA != null && cB != null)
            {
                cA.UnionWith(cB);
                Circuits.Remove(cB);
            }
            // Add to existing circuit
            else if (cA != null) cA.Add(b);
            else if (cB != null) cB.Add(a);
            // Create new circuit
            else Circuits.Add([a, b]);
        }
    }
}