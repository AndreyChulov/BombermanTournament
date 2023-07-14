namespace Engine.Input.Extension;

internal static class PointExtension
{
    public static Point Multiplication(this Point value, float multiplicator)
    {
        return new Point((int)(value.X * multiplicator), (int)(value.Y * multiplicator));
    }
}