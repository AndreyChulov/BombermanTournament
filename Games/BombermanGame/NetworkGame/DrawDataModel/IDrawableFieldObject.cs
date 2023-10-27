using Games.BombermanGame.ObsoleteGame.DrawDataModel.RamResources.Multi;
using Vortice.Direct2D1;

namespace Games.BombermanGame.NetworkGame.DrawDataModel;

internal interface IDrawableFieldObject
{
    RectangleF DrawRectangle { get; }
    void SetFieldResource(FieldResource resource);
    void Draw(ID2D1HwndRenderTarget renderTarget);
}