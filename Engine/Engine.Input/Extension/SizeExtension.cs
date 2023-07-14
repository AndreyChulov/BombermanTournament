namespace Engine.Input.Extension;

internal static class SizeExtension
{
    public static Size Multiplication(this Size value, float multiplicator)
    {
        return new Size((int)(value.Width * multiplicator), (int)(value.Height * multiplicator));
    }
    
    public static float Divide(this Size value, Size divider)
    {
        return (value.Width / (float)divider.Width + value.Height / (float)divider.Height) / 2.0f;
    }
}