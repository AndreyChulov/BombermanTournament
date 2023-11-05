using Games.BombermanGame.Shared.DrawDataModel.Field.Cell;
using Games.BombermanGame.Shared.DrawDataModel.Helpers;
using Games.BombermanGame.Shared.Interfaces;
using Games.BombermanGame.Shared.Interfaces.DrawableObject;
using Games.BombermanGame.Shared.RamResources.Multi;
using Vortice.Direct2D1;
using Vortice.DirectWrite;

namespace Games.BombermanGame.Shared.DrawDataModel.Field;

public abstract class BasePlayer : IDrawableFieldObject
{
    public RectangleF DrawRectangle { get; }
    public float NicknameFontSize { get; }
    
    protected internal IPlayer Player { get; }
    protected internal ID2D1Bitmap? PlayerBitmap { get; set; }
    protected internal ID2D1Brush? NicknameForegroundBrush { get; set; }
    protected internal ID2D1Brush? NicknameShadowBrush { get; set; } 
    protected internal IDWriteTextFormat? NicknameTextFormat { get; set; }

    protected BasePlayer(RectangleF drawRectangle, IPlayer player)
    {
        DrawRectangle = drawRectangle;
        Player = player;
        
        NicknameFontSize = drawRectangle.Height * 0.3f;
    }

    public virtual void SetFieldResource(FieldResource resource)
    {
        NicknameForegroundBrush = (ID2D1Brush)resource.NicknameForegroundBrushResource.Resource;
        NicknameShadowBrush = (ID2D1Brush)resource.NicknameShadowBrushResource.Resource;
        NicknameTextFormat = (IDWriteTextFormat)resource.NicknameTextFormatResource.Resource;
    }

    public void Draw(ID2D1HwndRenderTarget renderTarget)
    {
        PlayerDrawHelper.Draw(renderTarget, this);
    }
}