namespace helpers;

public readonly record struct Rect(V2 A, V2 B)
{
    public int Left => Math.Min(A.X, B.X);
    public int Right => Math.Max(A.X, B.X);
    public int Top => Math.Min(A.Y, B.Y);
    public int Bottom => Math.Max(A.Y, B.Y);
        
    public long Area => (Right - Left + 1) * (Bottom - Top + 1);
}