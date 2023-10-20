namespace Core.Engine.Input.Extension;

internal static class PointFExtension
{
    public static PointF FromFloat(this PointF _, float initValue) => new PointF(initValue, initValue);
}