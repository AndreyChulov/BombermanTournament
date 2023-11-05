using Core.Engine.Shared.Objects.GraphicEngine.RamResources.Single;
using Vortice.Direct2D1;
using Vortice.DirectWrite;
using Vortice.Mathematics;

namespace Games.BombermanGame.Shared.DrawDataModel.Field.Cell;

public static class NicknameCell
{
    public static TextFormatResource CreateTextFormatResource(
        string linkedResourceName, IDWriteFactory directWriteFactory, float fontSize)
    {
        var textFormat = directWriteFactory.CreateTextFormat(
            "Times new roman", FontWeight.Bold, Vortice.DirectWrite.FontStyle.Italic,
            FontStretch.Normal, fontSize
        );
        
        textFormat.TextAlignment = TextAlignment.Center;
        
        return new TextFormatResource($"{linkedResourceName}.{nameof(NicknameCell)}TextFormat", textFormat);
    }
    
    public static BrushResource CreateForegroundBrushResource(string linkedResourceName, ID2D1HwndRenderTarget renderTarget)
    {
        return new BrushResource(
            $"{linkedResourceName}.{nameof(NicknameCell)}ForegroundBrush",
            renderTarget.CreateSolidColorBrush(new Color4(Color3.Red, 1f)
            )
        );
    }
    
    public static BrushResource CreateShadowBrushResource(string linkedResourceName, ID2D1HwndRenderTarget renderTarget)
    {
        return new BrushResource(
            $"{linkedResourceName}.{nameof(NicknameCell)}ShadowBrush",
            renderTarget.CreateSolidColorBrush(new Color4(Color3.Black, 1f))
            );
    }
}