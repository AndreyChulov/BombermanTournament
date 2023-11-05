using Core.Engine.Shared.Objects.GraphicEngine.RamResources.Single;
using Games.BombermanGame.Shared.DrawDataModel.Field.Cell;
using Vortice.Direct2D1;
using Vortice.DirectWrite;
using Vortice.Mathematics;

namespace Games.BombermanGame.Shared.DrawDataModel.Scores;

public static class Score
{
    public static TextFormatResource CreateTextFormatResource(
        string linkedResourceName, IDWriteFactory directWriteFactory, float fontSize)
    {
        var textFormat = directWriteFactory.CreateTextFormat(
            "Comic Sans MS", FontWeight.Bold, Vortice.DirectWrite.FontStyle.Italic,
            FontStretch.Normal, fontSize
        );
        
        textFormat.TextAlignment = TextAlignment.Center;
        textFormat.ParagraphAlignment = ParagraphAlignment.Center;
        
        return new TextFormatResource($"{linkedResourceName}.{nameof(Score)}TextFormat", textFormat);
    }
    
    public static BrushResource CreateForegroundBrushResource(string linkedResourceName, ID2D1HwndRenderTarget renderTarget)
    {
        return new BrushResource(
            $"{linkedResourceName}.{nameof(Score)}ForegroundBrush",
            renderTarget.CreateSolidColorBrush(new Color4(Color3.Red, 1f)
            )
        );
    }
    
    public static BrushResource CreateShadowBrushResource(string linkedResourceName, ID2D1HwndRenderTarget renderTarget)
    {
        return new BrushResource(
            $"{linkedResourceName}.{nameof(Score)}ShadowBrush",
            renderTarget.CreateSolidColorBrush(new Color4(Color3.Black, 1f))
        );
    }

    public static BrushResource CreatePlayer1BackgroundBrushResource(string linkedResourceName, ID2D1HwndRenderTarget renderTarget)
    {
        return new BrushResource(
            $"{linkedResourceName}.{nameof(Score)}Player1BackgroundBrush",
            renderTarget.CreateSolidColorBrush(new Color4(Color3.Aqua, 0.3f))
        );
    }
    
    public static BrushResource CreatePlayer2BackgroundBrushResource(string linkedResourceName, ID2D1HwndRenderTarget renderTarget)
    {
        return new BrushResource(
            $"{linkedResourceName}.{nameof(Score)}Player1BackgroundBrush",
            renderTarget.CreateSolidColorBrush(new Color4(Color3.Aquamarine, 0.3f))
        );
    }
    
    public static BrushResource CreatePlayer3BackgroundBrushResource(string linkedResourceName, ID2D1HwndRenderTarget renderTarget)
    {
        return new BrushResource(
            $"{linkedResourceName}.{nameof(Score)}Player1BackgroundBrush",
            renderTarget.CreateSolidColorBrush(new Color4(Color3.Blue, 0.3f))
        );
    }
    
    public static BrushResource CreatePlayer4BackgroundBrushResource(string linkedResourceName, ID2D1HwndRenderTarget renderTarget)
    {
        return new BrushResource(
            $"{linkedResourceName}.{nameof(Score)}Player1BackgroundBrush",
            renderTarget.CreateSolidColorBrush(new Color4(Color3.Coral, 0.3f))
        );
    }
}