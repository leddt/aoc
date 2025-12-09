namespace helpers;

public readonly record struct Edge(V2 A, V2 B)
{
    public int Left => Math.Min(A.X, B.X);
    public int Right => Math.Max(A.X, B.X);
    public int Top => Math.Min(A.Y, B.Y);
    public int Bottom => Math.Max(A.Y, B.Y);
        
    public bool Crosses(Rect rect)
    {
        var result = Left < rect.Right && Right > rect.Left &&
                     Top < rect.Bottom && Bottom > rect.Top;

        return result;
    }
}