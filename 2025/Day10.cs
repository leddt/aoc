using Microsoft.Z3;

namespace aoc_2025;

public class Day10() : Day(10)
{
    private const string Sample = """
                                  [.##.] (3) (1,3) (2) (2,3) (0,2) (0,1) {3,5,4,7}
                                  [...#.] (0,2,3,4) (2,3) (0,4) (0,1,2) (1,2,3,4) {7,5,12,7,2}
                                  [.###.#] (0,1,2,3,4) (0,3,4) (0,1,2,4,5) (1,2) {10,11,11,5,10,5}
                                  """;
    
    [Test]
    public void Part1()
    {
        Assert.That(Run(Sample), Is.EqualTo(7));
        Console.WriteLine(Run(Input));
        return;

        int Run(string data) => data
            .GetLines()
            .Select(ParseMachine)
            .Sum(SolveMachine);

        int SolveMachine(Machine machine)
        {
            var match = GetAllDistinctCombinations(machine.Buttons)
                .Where(machine.CheckIndicators)
                .First();

            return match.Length;
        }

        IEnumerable<Button[]> GetAllDistinctCombinations(Button[] buttons, int startLength = 1)
        {
            foreach (var combo in GetDistinctPermutations(buttons, startLength))
                yield return combo;

            if (startLength >= buttons.Length) throw new Exception("No solution");
            
            foreach (var combo in GetAllDistinctCombinations(buttons, startLength + 1))
                yield return combo;
        }

        IEnumerable<Button[]> GetDistinctPermutations(Button[] buttons, int length)
        {
            if (length == 0)
            {
                yield return [];
                yield break;
            }

            foreach (var button in buttons)
            {
                var others = buttons.Except([button]).ToArray();

                foreach (var subCombo in GetDistinctPermutations(others, length - 1))
                    yield return [button, ..subCombo];
            }
        }
    }

    [Test]
    public void Part2()
    {
        /* I was stuck for a long time trying complex brute force algorithms that went nowhere. I saw "Z3" mentioned on
         * Reddit, and so I looked into it. At first glance it looked really complicated, but after some reading I got
         * how to apply it to this problem. Pretty clean in the end! */
        
        Assert.That(Run(Sample), Is.EqualTo(33));
        Console.WriteLine(Run(Input));
        return;

        int Run(string data) => data
            .GetLines()
            .Select(ParseMachine)
            .Sum(SolveMachine);

        int SolveMachine(Machine machine)
        {
            using var ctx = new Context();
            using var optimizer = ctx.MkOptimize();

            // Declare a variable for each button (the number of times it is pressed)
            var buttonVariables = machine.Buttons
                .Select((_, i) => ctx.MkIntConst($"b{i}"))
                .ToArray<ArithExpr>();
            
            // Assert that each button is >= 0
            foreach (var variable in buttonVariables)
                optimizer.Assert(ctx.MkGe(variable, ctx.MkInt(0)));
            
            // For each target joltage in the machine
            for (var i = 0; i < machine.TargetJoltage.Length; i++)
            {
                // Figure out which buttons affect it
                var affectingButtons = buttonVariables
                    .Where((_, j) => machine.Buttons[j].Affects.Contains(i))
                    .ToArray();
                
                // Assert that the sum of those buttons is equal to the target joltage
                optimizer.Assert(ctx.MkEq(
                    ctx.MkInt(machine.TargetJoltage[i]),
                    ctx.MkAdd(affectingButtons)
                ));
            }

            // Minimize the sum of the button variables
            optimizer.MkMinimize(ctx.MkAdd(buttonVariables));
            
            // Make sure we have a solution
            var status = optimizer.Check();
            if (status != Status.SATISFIABLE) throw new Exception("No solution found");

            // Calculate the total number of presses
            return optimizer.Model.Consts.Sum(x => ((IntNum)x.Value).Int);
        }
    }
    
    Machine ParseMachine(string line)
    {
        // [.##.] (3) (1,3) (2) (2,3) (0,2) (0,1) {3,5,4,7}
        
        var parts = line.Split(' ');
        var indicatorsPart = parts[0].Trim('[', ']');
        var buttonParts = parts[1..^1];
        var joltagePart = parts[^1].Trim('{', '}');

        return new Machine(
            TargetIndicators: indicatorsPart.Select(x => x == '#').ToArray(), 
            TargetJoltage: joltagePart.SplitInts(), 
            Buttons: buttonParts.Select(x => new Button(x.Trim('(', ')').SplitInts())).ToArray()
        );
    }

    record Machine(bool[] TargetIndicators, int[] TargetJoltage, Button[] Buttons)
    {
        public bool CheckIndicators(Button[] combo)
        {
            var state = new bool[TargetIndicators.Length];
            
            foreach (var button in combo)
            foreach (var i in button.Affects)
                state[i] = !state[i];
            
            return TargetIndicators.SequenceEqual(state);
        }
    }

    record Button(int[] Affects)
    {
        public override string ToString() => $"({string.Join(',', Affects)})";
    }
}