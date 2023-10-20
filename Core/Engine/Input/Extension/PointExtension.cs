namespace Core.Engine.Input.Extension;

internal static class PointExtension
{
    public static Point Multiplication(this Point value, PointF multiplier)
    {
        return new Point((int)(value.X * multiplier.X), (int)(value.Y * multiplier.Y));
    }
}