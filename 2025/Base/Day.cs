namespace aoc_2025.Base;

public abstract class Day(int number)
{
    protected string Input { get; set; }
    
    [SetUp]
    public async Task Setup()
    {
        Input = await AocHelper.GetInput(2025, number);
    }
}