namespace helpers;

public static class Extensions
{
    extension(string value)
    {
        public string[] GetLines() => value
            .Split('\n')
            .Select(x => x.Replace("\r", ""))
            .ToArray();
        
        public int[] SplitInts(string sep = ",") => value
            .Split(sep)
            .Select(int.Parse)
            .ToArray();
    }

    private static readonly int DirCount = Enum.GetValues<Dir>().Length;
    extension(Dir dir)
    {
        public Dir TurnRight() => (Dir)(((int)dir + 1) % DirCount);
        public Dir TurnLeft() => (Dir)(((int)dir - 1 + DirCount) % DirCount);
        public char ToChar() => dir.ToString()[0];
    }
}