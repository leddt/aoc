namespace helpers;

public readonly record struct V3(int X, int Y, int Z)
{
    public int XyzLength => Math.Abs(X) + Math.Abs(Y) + Math.Abs(Z);
    public double Length => Math.Sqrt(
        Math.Pow(X, 2) + 
        Math.Pow(Y, 2) + 
        Math.Pow(Z, 2)
    );
    
    public static V3 operator +(V3 a, V3 b) => new(a.X + b.X, a.Y + b.Y, a.Z + b.Z);
    public static V3 operator -(V3 a, V3 b) => new(a.X - b.X, a.Y - b.Y, a.Z - b.Z);
    public static V3 operator *(V3 a, int scale) => new(a.X * scale, a.Y * scale, a.Z * scale);

    public override string ToString() => $"V3({X},{Y},{Z})";
}