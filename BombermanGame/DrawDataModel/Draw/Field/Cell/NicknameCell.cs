using Engine.Shared.GraphicEngine.RamResources.Single;
using Vortice.Direct2D1;
using Vortice.DirectWrite;
using Vortice.Mathematics;
using Brush = Engine.Shared.GraphicEngine.RamResources.Single.Brush;

namespace BombermanGame.DrawDataModel.Draw.Field.Cell;

public static class NicknameCell
{
    public static TextFormat CreateTextFormatResource(
        string linkedResourceName, IDWriteFactory directWriteFactory, float fontSize)
    {
        return new TextFormat(
            $"{linkedResourceName}.{nameof(NicknameCell)}TextFormat",
            directWriteFactory.CreateTextFormat(
                "Times new roman", FontWeight.Bold, Vortice.DirectWrite.FontStyle.Italic,
                FontStretch.Normal, fontSize
            )
        );
    }
    
    public static Brush CreateForegroundBrushResource(string linkedResourceName, ID2D1HwndRenderTarget renderTarget)
    {
        return new Brush(
            $"{linkedResourceName}.{nameof(NicknameCell)}ForegroundBrush",
            renderTarget.CreateSolidColorBrush(new Color4(Color3.Red, 1f)
            )
        );
    }
    
    public static Brush CreateShadowBrushResource(string linkedResourceName, ID2D1HwndRenderTarget renderTarget)
    {
        return new Brush(
            $"{linkedResourceName}.{nameof(NicknameCell)}ShadowBrush",
            renderTarget.CreateSolidColorBrush(new Color4(Color3.Black, 1f))
            );
    }
}