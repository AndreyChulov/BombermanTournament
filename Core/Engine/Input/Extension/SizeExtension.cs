namespace Core.Engine.Input.Extension;

internal static class SizeExtension
{
    public static Size Multiplication(this Size value, float multiplicator)
    {
        return new Size((int)(value.Width * multiplicator), (int)(value.Height * multiplicator));
    }
    
    public static PointF Divide(this Size value, Size divider)
    {
        return new PointF(value.Width / (float)divider.Width, value.Height / (float)divider.Height);
    }
}