namespace helpers;

public static class AocHelper
{
    public static async Task<string> GetInput(int year, int day)
    {
        var session = Environment.GetEnvironmentVariable("AOC_SESSION");
        
        Directory.CreateDirectory("input");
        var cachePath = $"input/{year}.{day:00}.txt";
        
        if (File.Exists(cachePath))
            return (await File.ReadAllTextAsync(cachePath)).TrimEnd();
        
        using var httpClient = new HttpClient();
        httpClient.DefaultRequestHeaders.Add("Cookie", $"session={session}");
        var input = await httpClient.GetStringAsync($"https://adventofcode.com/{year}/day/{day}/input");
        
        await File.WriteAllTextAsync(cachePath, input);
        
        return input.TrimEnd();
    }
}