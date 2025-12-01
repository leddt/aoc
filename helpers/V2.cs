namespace helpers;

public readonly record struct V2(int X, int Y)
{
    public int XyLength => Math.Abs(X) + Math.Abs(Y);
    
    public static V2 operator +(V2 a, V2 b) => new(a.X + b.X, a.Y + b.Y);
    public static V2 operator -(V2 a, V2 b) => new(a.X - b.X, a.Y - b.Y);
    public static V2 operator *(V2 a, int scale) => new(a.X * scale, a.Y * scale);
    
    public static readonly V2 Up = new(0, -1);
    public static readonly V2 Right = new(1, 0);
    public static readonly V2 Down = new(0, 1);
    public static readonly V2 Left = new(-1, 0);

    public static implicit operator V2(Dir dir) => FromDir(dir);

    public static V2 FromDir(Dir dir) => dir switch
    {
        Dir.Up => Up,
        Dir.Right => Right,
        Dir.Down => Down,
        Dir.Left => Left,
        _ => throw new ArgumentOutOfRangeException(nameof(dir), dir, null)
    };

    public override string ToString() => $"V2({X},{Y})";
}